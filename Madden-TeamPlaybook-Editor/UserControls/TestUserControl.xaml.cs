using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{

    public partial class TestUserControl : UserControl
    {
        public TestUserControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TestDependencyProperty = DependencyProperty.Register("TestProperty", typeof(string), typeof(TestUserControl));
        [Bindable(true)] public string TestProperty
        {
            get { return (string)GetValue(TestDependencyProperty); }
            set { SetValue(TestDependencyProperty, value); }
        }
    }
}
