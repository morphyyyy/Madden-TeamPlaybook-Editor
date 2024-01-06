using Madden.CustomPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PlayModal : UserControl
    {
        private bool showPlayerText = false;
        private bool PSALView = false;
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
            tabPlayControls.SelectedIndex = 1;
        }

        private void iclPSALs_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tabPlayer.DataContext = play.Players.Where(player => player.IsSelected).FirstOrDefault();
            tabPlayControls.SelectedIndex = 1;
        }

        private void bdrField_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                tabPlayControls.SelectedIndex = 0;
            }
        }

        private void dgdPSALupdated(object sender, EventArgs e)
        {
            PlayerVM player = play.Players.FirstOrDefault(p => p.IsSelected);
            player?.UpdatePlayer();
            iclPSALs.Items.Refresh();
        }

        private void txtPlayerUpdated(object sender, TextChangedEventArgs e)
        {
            PlayerVM player = play.Players.Where(p => p.IsSelected).FirstOrDefault();
            player.UpdatePlayer();
            player.Play.UpdatePlay();
            MainWindow window = UIHelper.FindVisualParent<MainWindow>(this);
            TreeView playbook = UIHelper.FindChild<TreeView>(window, "tvwPlaybook");
            UserControl _play = UIHelper.FindChild<UserControl>(playbook, "uclPlay", play);
            ItemsControl playart = UIHelper.FindChild<ItemsControl>(_play, "iclPlayarts");
            if (playart != null)
            {
                playart.Items.Refresh();
            }
            iclPSALs.Items.Refresh();
        }

        public static class UIHelper
        {
            public static T FindVisualParent<T>(DependencyObject control)
            where T : DependencyObject
            {
                // get parent item
                DependencyObject parentObject = VisualTreeHelper.GetParent(control);

                // we’ve reached the end of the tree
                if (parentObject == null) return null;

                // check if the parent matches the type we’re looking for
                T parent = parentObject as T;
                if (parent != null)
                {
                    return parent;
                }
                else
                {
                    // use recursion to proceed with next level
                    return FindVisualParent<T>(parentObject);
                }
            }

            public static T FindChild<T>(DependencyObject parent, string childName, PlayVM _play = null)
            where T : DependencyObject
            {
                // Confirm parent and childName are valid. 
                if (parent == null) return null;
                T foundChild = null;
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    // If the child is not of the request child type child
                    T childType = child as T;
                    if (childType == null)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName, _play);
                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName && frameworkElement is Play && _play != null)
                        {
                            if (frameworkElement.DataContext == _play)
                            {
                                foundChild = (T)child;
                                break;
                            }
                        }
                        else if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                        foundChild = FindChild<T>(child, childName, _play);
                    }
                    else
                    {
                        // child element found.
                        foundChild = (T)child;
                        break;
                    }
                }
                return foundChild;
            }
        }

        private void savePlayart(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = play.PLYL.plyl.ToString(); // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG File (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;

                SaveCanvasToFile(play.ToPlayArtCanvas(1), 96, filename);
            }
        }

        public void SaveCanvasToFile(Canvas canvas, int dpi, string filename)
        {
            var rtb = new RenderTargetBitmap(
                (int)canvas.Width, //width
                (int)canvas.Height, //height
                dpi, //dpi x
                dpi, //dpi y
                PixelFormats.Pbgra32 // pixelformat
                );
            rtb.Render(canvas);

            var enc = new PngBitmapEncoder();
            BitmapFrame btf = BitmapFrame.Create(rtb);
            enc.Frames.Add(btf);
            using (var stm = System.IO.File.Create(filename))
            {
                enc.Save(stm);
            }
        }

        private void plylChanged(object sender, TextChangedEventArgs e)
        {
            play.UpdatePLYL();
            tabPlay.Items.Refresh();
        }
    }
}
