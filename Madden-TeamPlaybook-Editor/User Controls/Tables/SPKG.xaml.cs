using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SPKGTable : UserControl
    {
        public SPKGTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SPKGProperty =
            DependencyProperty.Register("SPKG", typeof(List<SPKG>), typeof(SPKGTable));

        public List<SPKG> SPKG
        {
            get { return GetValue(SPKGProperty) as List<SPKG>; }
            set { SetValue(SPKGProperty, value); }
        }
    }
}
