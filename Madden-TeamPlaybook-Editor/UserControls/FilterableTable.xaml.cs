using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class FilterableTable : UserControl
    {
        public FilterableTable()
        {
            InitializeComponent();
        }

        private bool Filter(object obj)
        {
            if (tbxValue.Text == "" || cbxFilter.SelectedValue == null)
            {
                return true;
            }
            Type oType = obj?.GetType();
            PropertyInfo pInfo = oType.GetProperty(cbxFilter.SelectedValue?.ToString());
            Type pType = pInfo.PropertyType;
            object value = pInfo.GetValue(obj);

            switch (pType.Name)
            {
                case "Int32":
                    int filter = 0;
                    bool isInt = Int32.TryParse(tbxValue.Text, out filter);
                    return isInt ? (int)value == filter : false;
                case "String":
                    return value.ToString().IndexOf(tbxValue.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                case "Single":
                    return value.ToString() == tbxValue.Text.ToString();
                default:
                    return false;
            }
        }

        private void dataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Type type = DataContext?.GetType().GetProperty("Item").PropertyType;
            cbxFilter.ItemsSource = type?.GetProperties().Select(o => o.Name);
            dataGrid.Items.Filter = Filter;
        }

        private void tbxValueChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.Items.Filter = Filter;
        }

        private void cbxValueChanged(object sender, SelectionChangedEventArgs e)
        {
            dataGrid.Items.Filter = Filter;
        }
    }
}
