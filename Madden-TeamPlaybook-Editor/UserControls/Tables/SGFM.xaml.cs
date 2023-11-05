using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SGFMTable : UserControl
    {
        public SGFMTable()
        {
            InitializeComponent();
        }

        public static DependencyProperty SGFMProperty =
            DependencyProperty.Register("SGFM", typeof(List<SGFM>), typeof(SGFMTable));

        public List<SGFM> SGFM
        {
            get { return GetValue(SGFMProperty) as List<SGFM>; }
            set { SetValue(SGFMProperty, value); }
        }
    }
}
