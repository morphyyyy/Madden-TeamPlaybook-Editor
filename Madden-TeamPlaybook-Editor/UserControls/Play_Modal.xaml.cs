﻿using Madden.Team;
using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PlayModal : UserControl
    {
        private bool showPlayerText = false;
        private bool PSALView = false;
        public PlayModal()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)lvwSituations.Items).CollectionChanged += lvwSituations_CollectionChanged;
            ((INotifyCollectionChanged)dgdPSALs.Items).CollectionChanged += DataGrid_CollectionChanged;
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
                lvwSituations.ItemsSource = play.SubFormation.Formation.Playbook.Situations
                    .Select(s => play.Situations.Exists(p => p.AIGR == s.Key) ? play.Situations.FirstOrDefault(p => p.AIGR == s.Key) : new Madden.TeamPlaybook.PBAI
                    {
                        AIGR = s.Key ?? 0,
                        Flag = play.PBPL.Flag,
                        PBPL = play.PBPL.pbpl,
                        PLF_ = play.PLYL.PLF_,
                        PLYT = play.PLYL.PLYT,
                        prct = 0,
                        SETL = play.PLYL.SETL,
                        vpos = play.PLYL.vpos
                    });
            }
        }

        private void lvwSituations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                // scroll the new item into view   
                lvwSituations.ScrollIntoView(e.NewItems[0]);
            }
        }

        private void iclIcons_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GetPlayer();
        }

        private void iclPSALs_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GetPlayer();
        }

        public void GetPlayer()
        {
            PlayerVM _player = play.Players.FirstOrDefault(player => player.IsSelected);
            tabPlayer.DataContext = _player;
            foreach (FormationVM _psalType in _player.Play.SubFormation.Formation.Playbook.PSALs)
            {
                _psalType.IsExpanded = false;
                _psalType.IsVisible = false;
                foreach (SubFormationVM _psalPlayer in _psalType.SubFormations)
                {
                    _psalPlayer.IsExpanded = false;
                    _psalPlayer.IsVisible = false;
                    foreach (PlayVM _psal in _psalPlayer.Plays)
                    {
                        PlayVM _psalPlay = _psal.Players.SingleOrDefault(player => player.PSAL.Intersect(_player.PSAL)?.Count() > 0 )?.Play;
                        if (_psalPlay != null)
                        {
                            _psalPlay.SubFormation.Formation.IsExpanded = true;
                            _psalPlay.SubFormation.Formation.IsVisible = true;
                            _psalPlay.SubFormation.IsExpanded = true;
                            _psalPlay.SubFormation.IsVisible = true;
                            _psalPlay.IsSelected = true;
                        }
                    }
                }
            }
            Play _play = tvwPSALs.SelectedItem is Play ? ((Play)tvwPSALs.SelectedItem) : null;
            _play?.BringIntoView();
        }

        private void dgdPSALupdated(object sender, EventArgs e)
        {
            play?.Players?.FirstOrDefault(p => p.IsSelected)?.UpdatePlayer();
            iclPSALs.Items.Refresh();
        }

        private void txtPlayerUpdated(object sender, TextChangedEventArgs e)
        {
            PlayerVM player = play.Players.FirstOrDefault(p => p.IsSelected);
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

                SaveCanvasToFile(play.ToCanvas(1, true), 96, filename);
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
            //cbxPLYL_SRMM.Items.Refresh();
            play.UpdatePLYL();
        }

        private void plylChanged(object sender, SelectionChangedEventArgs e)
        {
            play.UpdatePLYL();
        }

        private void addPLPD(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            play.AddPLPD();
            play.UpdatePlay();
        }

        private void tvwPSALs_Selected(object sender, RoutedEventArgs e)
        {
            //Working Code
            PlayerVM _player = play != null ? play.Players.FirstOrDefault(p => p.IsSelected) : null;
            if (tvwPSALs.SelectedItem is PlayVM && _player != null)
            {
                _player.PLYS.PSAL = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.PSAL;
                _player.PLYS.ARTL = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.ARTL;
                _player.PLYS.PLRR = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.PLRR;
                _player.UpdatePlayer();
                _player.Play.UpdatePlay();
                MainWindow mainWindow = UIHelper.FindVisualParent<MainWindow>(this);
                TreeView _treeView = UIHelper.FindChild<TreeView>(mainWindow, "tvwPlaybook");
                UserControl _play = UIHelper.FindChild<UserControl>(_treeView, "uclPlay", uclPlayModal.play);
                ItemsControl playart = UIHelper.FindChild<ItemsControl>(_play, "iclPlayarts");
                playart?.Items.Refresh();
                iclPSALs.Items.Refresh();
                //tabPlayer.Items.Refresh();
            }

            //Old Code
            //TreeViewItem tvi = e.OriginalSource as TreeViewItem;

            //if (tvi == null || e.Handled) return;

            //tvi.IsExpanded = !tvi.IsExpanded;
            //if (tvi.IsSelected)
            //{
            //    tvi.IsSelected = false;
            //    e.Handled = true;
            //}
        }

        void DataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Execute your logic here
        }

        private void PSALinsertStep(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            foreach (PSAL psal in play.SubFormation.Formation.Playbook.PSAL.Where(p => p.psal == ((PSAL)dgdPSALs.SelectedItem).psal && p.step >= ((PSAL)dgdPSALs.SelectedItem).step))
            {
                psal.step++;
            }
            play.SubFormation.Formation.Playbook.PSAL.Add(new PSAL { rec = play.SubFormation.Formation.Playbook.PSAL.Count(), step = ((PSAL)dgdPSALs.SelectedItem).step - 1, psal = ((PSAL)dgdPSALs.SelectedItem).psal });
            PlayerVM player = play.Players.FirstOrDefault(p => p.IsSelected);
            player.UpdatePlayer();
            dgdPSALs.Items.Refresh();
            GetPlayer();
        }

        private void PSALappendStep(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            play.SubFormation.Formation.Playbook.PSAL.Add(new PSAL { rec = play.SubFormation.Formation.Playbook.PSAL.Count(), step = ((PSAL)dgdPSALs.SelectedItem).step + 1, psal = ((PSAL)dgdPSALs.SelectedItem).psal });
            PlayerVM player = play.Players.FirstOrDefault(p => p.IsSelected);
            player.UpdatePlayer();
            dgdPSALs.Items.Refresh();
            GetPlayer();
        }

        private void PSALdeleteStep(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            foreach (PSAL psal in play.SubFormation.Formation.Playbook.PSAL.Where(p => p.psal == ((PSAL)dgdPSALs.SelectedItem).psal && p.step > ((PSAL)dgdPSALs.SelectedItem).step))
            {
                psal.step--;
            }
            play.SubFormation.Formation.Playbook.PSAL.Remove(((PSAL)dgdPSALs.SelectedItem));
            PlayerVM player = play.Players.FirstOrDefault(p => p.IsSelected);
            player.UpdatePlayer();
            dgdPSALs.Items.Refresh();
            GetPlayer();
        }

        private void AddNewPSAL(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
