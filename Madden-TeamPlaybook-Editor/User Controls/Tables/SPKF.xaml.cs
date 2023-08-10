using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SPKFTable : UserControl
    {
        public SPKFTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SPKFProperty =
            DependencyProperty.Register("SPKF", typeof(List<SPKF>), typeof(SPKFTable));

        public List<SPKF> SPKF
        {
            get { return GetValue(SPKFProperty) as List<SPKF>; }
            set { SetValue(SPKFProperty, value); }
        }
    }
}
