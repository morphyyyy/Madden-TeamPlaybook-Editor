using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Linq;
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

        public static DependencyProperty PlayProperty = DependencyProperty.Register("play", typeof(PlayVM), typeof(PlayModal));

        public PlayVM play
        {
            get
            {
                return GetValue(PlayProperty) as PlayVM;
            }
            set
            {
                SetValue(PlayProperty, value);
            }
        }

        private void iclIcons_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tabPlayer.DataContext = play.Players.Where(player => player.IsSelected).FirstOrDefault();
        }

        private void dgdPSALupdated(object sender, EventArgs e)
        {
            PlayerVM player = play.Players.Where(p => p.IsSelected).FirstOrDefault();
            Console.WriteLine(player.PSALpath);
            player.UpdatePlayer();
            Console.WriteLine(player.PSALpath);
            iclPSALs.InvalidateVisual();
        }
    }
}
