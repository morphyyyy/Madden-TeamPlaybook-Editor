using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBPLTable : UserControl
    {
        public PBPLTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBPLProperty =
            DependencyProperty.Register("PBPL", typeof(List<PBPL>), typeof(PBPLTable));

        public List<PBPL> PBPL
        {
            get { return GetValue(PBPLProperty) as List<PBPL>; }
            set { SetValue(PBPLProperty, value); }
        }
    }
}
