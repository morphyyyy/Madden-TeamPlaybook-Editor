using MaddenTeamPlaybookEditor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Play : UserControl
    {
        public Play()
        {
            InitializeComponent();
        }

        public static DependencyProperty PlayProperty = DependencyProperty.Register("play", typeof(PlayVM), typeof(Play));

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
