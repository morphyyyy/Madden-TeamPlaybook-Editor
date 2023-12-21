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

        private void dataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Type type = Data?.GetType().GetProperty("Item").PropertyType;
            cbxFilter.ItemsSource = type?.GetProperties().Select(o => o.Name);
            dataGrid.Items.Filter = Filter;
            dataGrid.Items.IsLiveFiltering = true;
        }

        private bool Filter(object obj)
        {
            Type type = obj?.GetType();
            PropertyInfo pInfo = type.GetProperty(cbxFilter.SelectedValue.ToString());
            string value = pInfo.GetValue(obj).ToString();
            return value.IndexOf(tbxValue.Text,StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void tbxValueChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.Items.Filter = Filter;
        }
    }
}
