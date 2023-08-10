using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBCCTable : UserControl
    {
        public PBCCTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBCCProperty =
            DependencyProperty.Register("PBCC", typeof(List<PBCC>), typeof(PBCCTable));

        public List<PBCC> PBCC
        {
            get { return GetValue(PBCCProperty) as List<PBCC>; }
            set { SetValue(PBCCProperty, value); }
        }
    }
}
