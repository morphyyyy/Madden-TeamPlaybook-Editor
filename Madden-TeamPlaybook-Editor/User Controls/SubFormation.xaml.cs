using MaddenTeamPlaybookEditor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SubFormation : UserControl
    {
        public SubFormation()
        {
            InitializeComponent();
        }

        public static DependencyProperty SubFormationProperty = DependencyProperty.Register("subFormation", typeof(SubFormationVM), typeof(SubFormation));

        public SubFormationVM subFormation
        {
            get
            {
                return GetValue(SubFormationProperty) as SubFormationVM;
            }
            set
            {
                SetValue(SubFormationProperty, value);
            }
        }
    }
}
