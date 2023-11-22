using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            tabPlayControls.SelectedIndex = 1;
        }

        private void iclPSALs_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tabPlayer.DataContext = play.Players.Where(player => player.IsSelected).FirstOrDefault();
            tabPlayControls.SelectedIndex = 1;
        }

        private void bdrField_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tabPlayControls.SelectedIndex = 0;
        }

        private void dgdPSALupdated(object sender, EventArgs e)
        {
            PlayerVM player = play.Players.Where(p => p.IsSelected).FirstOrDefault();
            Console.WriteLine(player.PSALpath);
            player.UpdatePlayer();
            Console.WriteLine(player.PSALpath);
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
    }
}
