using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SETLTable : UserControl
    {
        public SETLTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SETLProperty =
            DependencyProperty.Register("SETL", typeof(List<SETL>), typeof(SETLTable));

        public List<SETL> SETL
        {
            get { return GetValue(SETLProperty) as List<SETL>; }
            set { SetValue(SETLProperty, value); }
        }
    }
}
