using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SRFTTable : UserControl
    {
        public SRFTTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SRFTProperty =
            DependencyProperty.Register("SRFT", typeof(List<SRFT>), typeof(SRFTTable));

        public List<SRFT> SRFT
        {
            get { return GetValue(SRFTProperty) as List<SRFT>; }
            set { SetValue(SRFTProperty, value); }
        }
    }
}
