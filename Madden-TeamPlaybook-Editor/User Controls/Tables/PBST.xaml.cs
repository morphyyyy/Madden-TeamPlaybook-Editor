using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBSTTable : UserControl
    {
        public PBSTTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBSTProperty =
            DependencyProperty.Register("PBST", typeof(List<PBST>), typeof(PBSTTable));

        public List<PBST> PBST
        {
            get { return GetValue(PBSTProperty) as List<PBST>; }
            set { SetValue(PBSTProperty, value); }
        }
    }
}
