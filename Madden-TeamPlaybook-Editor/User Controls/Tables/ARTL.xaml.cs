using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class ARTLTable : UserControl
    {
        public ARTLTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty ARTLProperty =
            DependencyProperty.Register("ARTL", typeof(List<ARTL>), typeof(ARTLTable));

        public List<ARTL> ARTL
        {
            get
            {
                return GetValue(ARTLProperty) as List<ARTL>;
            }
            set
            {
                SetValue(ARTLProperty, value);
            }
        }
    }
}
