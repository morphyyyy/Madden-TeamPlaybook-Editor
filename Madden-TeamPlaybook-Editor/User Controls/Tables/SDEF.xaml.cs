using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SDEFTable : UserControl
    {
        public SDEFTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SDEFProperty =
            DependencyProperty.Register("SDEF", typeof(List<SDEF>), typeof(SDEFTable));

        public List<SDEF> SDEF
        {
            get { return GetValue(SDEFProperty) as List<SDEF>; }
            set { SetValue(SDEFProperty, value); }
        }
    }
}
