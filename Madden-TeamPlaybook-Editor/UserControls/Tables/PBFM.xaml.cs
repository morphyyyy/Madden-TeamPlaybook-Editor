using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBFMTable : UserControl
    {
        public PBFMTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBFMProperty =
            DependencyProperty.Register("PBFM", typeof(List<PBFM>), typeof(PBFMTable));

        public List<PBFM> PBFM
        {
            get { return GetValue(PBFMProperty) as List<PBFM>; }
            set { SetValue(PBFMProperty, value); }
        }
    }
}
