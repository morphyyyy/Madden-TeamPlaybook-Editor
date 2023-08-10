using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBAUTable : UserControl
    {
        public PBAUTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBAUProperty =
            DependencyProperty.Register("PBAU", typeof(List<PBAU>), typeof(PBAUTable));

        public List<PBAU> PBAU
        {
            get { return GetValue(PBAUProperty) as List<PBAU>; }
            set { SetValue(PBAUProperty, value); }
        }
    }
}
