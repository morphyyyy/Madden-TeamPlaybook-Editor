using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PPCTTable : UserControl
    {
        public PPCTTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PPCTProperty =
            DependencyProperty.Register("PPCT", typeof(List<PPCT>), typeof(PPCTTable));

        public List<PPCT> PPCT
        {
            get { return GetValue(PPCTProperty) as List<PPCT>; }
            set { SetValue(PPCTProperty, value); }
        }
    }
}
