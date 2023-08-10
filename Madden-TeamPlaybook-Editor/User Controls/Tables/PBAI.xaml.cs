using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBAITable : UserControl
    {
        public PBAITable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBAIProperty =
            DependencyProperty.Register("PBAI", typeof(List<PBAI>), typeof(PBAITable));

        public List<PBAI> PBAI
        {
            get { return GetValue(PBAIProperty) as List<PBAI>; }
            set { SetValue(PBAIProperty, value); }
        }
    }
}
