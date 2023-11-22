using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class FORMTable : UserControl
    {
        public FORMTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty FORMProperty =
            DependencyProperty.Register("FORM", typeof(List<FORM>), typeof(FORMTable));

        public List<FORM> FORM
        {
            get { return GetValue(FORMProperty) as List<FORM>; }
            set { SetValue(FORMProperty, value); }
        }
    }
}
