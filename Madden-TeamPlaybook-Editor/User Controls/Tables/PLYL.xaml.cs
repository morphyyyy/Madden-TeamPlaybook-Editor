using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PLYLTable : UserControl
    {
        public PLYLTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PLYLProperty =
            DependencyProperty.Register("PLYL", typeof(List<PLYL>), typeof(PLYLTable));

        public List<PLYL> PLYL
        {
            get { return GetValue(PLYLProperty) as List<PLYL>; }
            set { SetValue(PLYLProperty, value); }
        }
    }
}
