using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PLRDTable : UserControl
    {
        public PLRDTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PLRDProperty =
            DependencyProperty.Register("PLRD", typeof(List<PLRD>), typeof(PLRDTable));

        public List<PLRD> PLRD
        {
            get { return GetValue(PLRDProperty) as List<PLRD>; }
            set { SetValue(PLRDProperty, value); }
        }
    }
}
