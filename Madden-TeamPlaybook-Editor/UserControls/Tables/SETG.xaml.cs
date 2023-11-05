using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SETGTable : UserControl
    {
        public SETGTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SETGProperty =
            DependencyProperty.Register("SETG", typeof(List<SETG>), typeof(SETGTable));

        public List<SETG> SETG
        {
            get { return GetValue(SETGProperty) as List<SETG>; }
            set { SetValue(SETGProperty, value); }
        }
    }
}
