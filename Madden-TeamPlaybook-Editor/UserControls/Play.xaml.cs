using MaddenTeamPlaybookEditor.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Play : UserControl
    {
        public Play()
        {
            InitializeComponent();
            //if (showAudibles)
            //{
            //    pnlAudibles.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    pnlAudibles.Visibility = Visibility.Collapsed;
            //}
        }

        public static DependencyProperty PlayProperty = DependencyProperty.Register("play", typeof(PlayVM), typeof(Play));
        [Bindable(true)] public PlayVM play
        {
            get { return GetValue(PlayProperty) as PlayVM; }
            set { SetValue(PlayProperty, value); }
        }

        public static DependencyProperty ShowAudiblesProperty = DependencyProperty.Register("showAudibles", typeof(bool), typeof(Play));
        [Bindable(true)] public bool showAudibles
        {
            get { return (bool)GetValue(ShowAudiblesProperty); }
            set { SetValue(ShowAudiblesProperty, value); }
        }
    }
}
