using MaddenTeamPlaybookEditor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PlayModal : UserControl
    {
        public PlayModal()
        {
            InitializeComponent();
        }

        public static DependencyProperty SubFormationProperty = DependencyProperty.Register("subFormation", typeof(SubFormationVM), typeof(PlayModal));

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

        public static DependencyProperty PlayProperty = DependencyProperty.Register("play", typeof(PlayVM), typeof(PlayModal));

        public PlayVM play
        {
            get
            {
                PlayVM playVM = GetValue(PlayProperty) as PlayVM;
                return playVM;
            }
            set
            {
                var property = value;
                SetValue(PlayProperty, value);
            }
        }
    }
}
