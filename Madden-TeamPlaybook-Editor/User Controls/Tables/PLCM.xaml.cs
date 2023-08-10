using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PLCMTable : UserControl
    {
        public PLCMTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PLCMProperty =
            DependencyProperty.Register("PLCM", typeof(List<PLCM>), typeof(PLCMTable));

        public List<PLCM> PLCM
        {
            get { return GetValue(PLCMProperty) as List<PLCM>; }
            set { SetValue(PLCMProperty, value); }
        }
    }
}
