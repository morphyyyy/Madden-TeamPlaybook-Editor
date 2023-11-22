using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PSALTable : UserControl
    {
        public PSALTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PSALProperty =
            DependencyProperty.Register("PSAL", typeof(List<PSAL>), typeof(PSALTable));

        public List<PSAL> PSAL
        {
            get { return GetValue(PSALProperty) as List<PSAL>; }
            set { SetValue(PSALProperty, value); }
        }
    }
}
