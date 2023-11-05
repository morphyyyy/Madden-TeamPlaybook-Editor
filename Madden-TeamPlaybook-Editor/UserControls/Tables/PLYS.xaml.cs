using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PLYSTable : UserControl
    {
        public PLYSTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PLYSProperty =
            DependencyProperty.Register("PLYS", typeof(List<PLYS>), typeof(PLYSTable));

        public List<PLYS> PLYS
        {
            get { return GetValue(PLYSProperty) as List<PLYS>; }
            set { SetValue(PLYSProperty, value); }
        }
    }
}
