using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PLPDTable : UserControl
    {
        public PLPDTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty PLPDProperty =
            DependencyProperty.Register("PLPD", typeof(List<PLPD>), typeof(PLPDTable));

        public List<PLPD> PLPD
        {
            get { return GetValue(PLPDProperty) as List<PLPD>; }
            set { SetValue(PLPDProperty, value); }
        }
    }
}
