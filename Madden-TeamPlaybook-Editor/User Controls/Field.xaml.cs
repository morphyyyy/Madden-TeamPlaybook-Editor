using MaddenTeamPlaybookEditor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Field : UserControl
    {
        public Field()
        {
            InitializeComponent();
        }

        public static DependencyProperty SubFormationProperty = DependencyProperty.Register("subFormation", typeof(SubFormationVM), typeof(Field));

        public SubFormationVM subFormation
        {
            get
            {
                SubFormationVM subFormationVM = GetValue(SubFormationProperty) as SubFormationVM;
                return subFormationVM;
            }
            set
            {
                var property = value;
                SetValue(SubFormationProperty, value);
            }
        }

        public static DependencyProperty PlayProperty = DependencyProperty.Register("play", typeof(PlayVM), typeof(Field));

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
