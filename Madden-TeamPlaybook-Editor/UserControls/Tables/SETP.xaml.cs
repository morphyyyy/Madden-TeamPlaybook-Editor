using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SETPTable : UserControl
    {
        public SETPTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SETPProperty =
            DependencyProperty.Register("SETP", typeof(List<SETP>), typeof(SETPTable));

        public List<SETP> SETP
        {
            get { return GetValue(SETPProperty) as List<SETP>; }
            set { SetValue(SETPProperty, value); }
        }
    }
}
