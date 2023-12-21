using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class FilterableTable : UserControl
    {
        public FilterableTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(IEnumerable), typeof(FilterableTable));

        public IEnumerable Data
        {
            get
            {
                return GetValue(DataProperty) as IEnumerable;
            }
            set
            {
                SetValue(DataProperty, value);
            }
        }

        private bool Filter(object obj)
        {
            Type oType = obj?.GetType();
            PropertyInfo pInfo = oType.GetProperty(cbxFilter.SelectedValue.ToString());
            Type pType = pInfo.PropertyType;
            object value = pInfo.GetValue(obj);
            if (tbxValue.Text == "")
            {
                return true;
            }
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
            Type type = Data?.GetType().GetProperty("Item").PropertyType;
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
