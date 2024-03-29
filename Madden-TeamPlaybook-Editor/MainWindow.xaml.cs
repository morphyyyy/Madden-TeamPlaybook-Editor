﻿using MaddenTeamPlaybookEditor.User_Controls;
using MaddenTeamPlaybookEditor.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows.Input;
using MaddenCustomPlaybookEditor;
using System.Windows.Media;
using MaddenTeamPlaybookEditor.Classes;
using System.Windows.Documents;
using System.Runtime.InteropServices;
using System.Linq;
using System.Windows.Media.Imaging;
using IDataObject = System.Windows.IDataObject;
using Madden.CustomPlaybook;
using System.Reflection;

namespace MaddenTeamPlaybookEditor
{
    public partial class MainWindow : Window
    {
        public string filePath;
        public int OpenIndex = -1;
        public TeamPlaybook TeamPlaybook;
        public MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook CustomPlaybook;
        public bool CustomPlaybookSupport = true;
        private Point _lastMouseDown;
        public TreeViewItem draggedItem, _target;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        private DragAdorner adorner;

        bool CtrlPressed
        {
            get
            {
                return System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl);
            }
        }

        #region Initialization

        public MainWindow()
        {
            InitializeComponent();

            filePath = "E:\\Software\\MMC_Editor\\Madden 24\\All_Legacy_Files\\common\\database\\playbooks\\madden_saints.db.DB";

            if (File.Exists(filePath))
            {
                OpenTDB(filePath);
            }
            else
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "DB Files | *.db";
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() != false || openFileDialog1.FileName != "")
                {
                    OpenTDB(openFileDialog1.FileName);
                }
            }
        }

        #endregion

        #region Open

        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open Team Playbook Database";
            openFileDialog1.Filter = "DB Files | *.db";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != false || openFileDialog1.FileName != "")
            {
                OpenTDB(openFileDialog1.FileName);
            }
        }

        public void OpenTDB(string filePath)
        {
            if (OpenIndex > -1)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Would you like to save " + Path.GetFileName(TeamPlaybook.filePath) + " ?", "Save?", MessageBoxButton.YesNoCancel);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    TeamPlaybook.SaveTDBTables(OpenIndex);
                    if (!TDBAccess.TDB.TDBSave(OpenIndex))
                    {
                        MessageBox.Show("Error Saving");
                    }
                    TDBAccess.TDB.TDBClose(OpenIndex);
                }
                else if (messageBoxResult == MessageBoxResult.No)
                {
                    TDBAccess.TDB.TDBClose(OpenIndex);
                }
                else if (messageBoxResult == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            OpenIndex = TDBAccess.TDB.TDBOpen(filePath);

            if (OpenIndex != -1)
            {
                Dictionary<int, string> dictionaty = TDBAccess.TableNames.GetTables().ToDictionary(t => t.rec, t => t.name);
                if (dictionaty.Except(MaddenTeamPlaybookEditor.ViewModels.TeamPlaybook.Tables).Count() == 0)
                {
                    TeamPlaybook = new TeamPlaybook(filePath);
                    BindPlaybook(TeamPlaybook);
                    tclTeamPlaybook.Visibility = Visibility.Visible;
                    tclCustomPlaybook.Visibility = Visibility.Collapsed;
                    tvwPlaybook.Items.Refresh();
                    tvwPlaybook.UpdateLayout();
                    int sum = TeamPlaybook.PBAI.Sum(x => x.prct);
                }
                else if (dictionaty.Except(MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook.Tables).Count() == 0)
                {
                    if (!CustomPlaybookSupport)
                    {
                        var rgch = "\U0001F61B".ToCharArray();
                        var str = rgch[0] + "" + rgch[1];
                        MessageBox.Show("Custom Playbooks are not supported, yet! " + str);
                        return;
                    }

                    CustomPlaybook = new MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook(filePath);
                    BindPlaybook(CustomPlaybook);
                    tclCustomPlaybook.Visibility = Visibility.Visible;
                    tclTeamPlaybook.Visibility = Visibility.Collapsed;

                    //Window codePopup = new Window { Title = "Create Playbook", Height = 200, Width = 300, SizeToContent = SizeToContent.WidthAndHeight };
                    //ComboBox listUnit = new ComboBox { DisplayMemberPath = "Key", SelectedValuePath = "Value", ItemsSource = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Offense", "Madden_"), new KeyValuePair<string, string>("Defense", "Madden_Def_") } };
                    //ComboBox listTeam = new ComboBox { ItemsSource = CustomPlaybook.PBFI.Select(p => p.name.Substring(p.name.LastIndexOf('_') + 1)).Distinct().OrderBy(p => p) };
                    //StackPanel content = new StackPanel { CanVerticallyScroll = true };
                    //content.Children.Add(listUnit);
                    //content.Children.Add(listTeam);
                    //codePopup.Content = content;
                    //codePopup.ShowDialog();

                    //PBFI BOKL = CustomPlaybook.PBFI.FirstOrDefault(p => p.name == (string)listUnit.SelectedValue + (string)listTeam.SelectedValue);

                    //TeamPlaybook = new TeamPlaybook();
                    //foreach (MaddenCustomPlaybookEditor.ViewModels.FormationVM formation in CustomPlaybook.Formations)
                    //{
                    //    TeamPlaybook.AddFormation(formation);
                    //}
                }
            }
            else
            {
                MessageBox.Show("Unable to open the database", "Error");
            }
        }

        private void mnuLoadRoster_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open Team Roster Database";
            openFileDialog1.Filter = "DB Files | *.db";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() != false || openFileDialog1.FileName != "")
            {
                int RosterOpenIndex = TDBAccess.TDB.TDBOpen(openFileDialog1.FileName);
                TeamPlaybook.GetRoster();
                TeamPlaybook.BuildPlaybook();
                TDBAccess.TDB.TDBClose(RosterOpenIndex);
            }
        }

        #endregion

        #region BindPlaybook

        public void BindPlaybook(TeamPlaybook Playbook)
        {
            wdwPlaybookEditor.Title = "Madden Team Playbook Editor - " + Path.GetFileName(Playbook.filePath);
            tvwPlaybook.DataContext = Playbook;
            cbxPLYT.DataContext = TeamPlaybook.PlayType.Where(x => TeamPlaybook.PLYL.Any(y => y.PLYT == x.Key)).OrderBy(s => s.Value);
            if (Playbook.Type == "Offense")
            {
                lvwSituations.DataContext = TeamPlaybook.SituationOff.Select(p => new Madden.TeamPlaybook.PBAI { AIGR = p.Key }).ToList();
            }
            else if (Playbook.Type == "Defense")
            {
                lvwSituations.DataContext = TeamPlaybook.SituationDef.Select(p => new Madden.TeamPlaybook.PBAI { AIGR = p.Key }).ToList();
            }
            lvwPlaysByRouteDepth.DataContext = TeamPlaybook.Plays;
            lvwPlaysByRouteDepth.Items.Filter = PlayListFilter;
            tclTables.DataContext = Playbook;
            //tvwPSALs.DataContext = Playbook.GetPSALlist();
            //tabPlaybook.DataContext = Playbook;
        }

        private bool PlayListFilter(object obj)
        {
            return cbxPLYT.SelectedValue == null ? false : ((PlayVM)obj).PLYL.PLYT == (int)cbxPLYT.SelectedValue;
        }

        public void BindPlaybook(MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook Playbook)
        {
            wdwPlaybookEditor.Title = "Madden Team Playbook Editor - " + Path.GetFileName(Playbook.filePath);
            tclCustomPlaybook.DataContext = Playbook;
        }

        #endregion

        #region Copy/Paste

        private void CopyToClipboard(object sender, ExecutedRoutedEventArgs e)
        {
            Clipboard.SetDataObject(((TreeView)sender).SelectedItem, true);
        }

        private void PasteFromClipboard(object sender, ExecutedRoutedEventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();

            if (sender is TreeView)
            {
                if (iData.GetDataPresent(typeof(FormationVM)))
                {
                    if (((TreeView)sender).SelectedItem is FormationVM)
                    {
                        ((FormationVM)((TreeView)sender).SelectedItem).Playbook.AddFormation((FormationVM)iData.GetData(typeof(FormationVM)), ((FormationVM)((TreeView)sender).SelectedItem).PBFM.ord_);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                }

                if (iData.GetDataPresent(typeof(SubFormationVM)))
                {
                    if (((TreeView)sender).SelectedItem is FormationVM)
                    {
                        ((FormationVM)((TreeView)sender).SelectedItem).AddSubFormation((SubFormationVM)iData.GetData(typeof(SubFormationVM)));
                        ((FormationVM)((TreeView)sender).SelectedItem).SubFormations[((FormationVM)((TreeView)sender).SelectedItem).SubFormations.Count - 1].IsVisible = true;
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                    if (((TreeView)sender).SelectedItem is SubFormationVM)
                    {
                        ((SubFormationVM)((TreeView)sender).SelectedItem).Formation.AddSubFormation((SubFormationVM)iData.GetData(typeof(SubFormationVM)), ((SubFormationVM)((TreeView)sender).SelectedItem).PBST.ord_);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                }

                if (iData.GetDataPresent(typeof(PlayVM)))
                {
                    if (((TreeView)sender).SelectedItem is SubFormationVM)
                    {
                        PlayVM newPlay = (PlayVM)iData.GetData(typeof(PlayVM));
                        ((SubFormationVM)((TreeView)sender).SelectedItem).AddPlay(newPlay);
                        ((SubFormationVM)((TreeView)sender).SelectedItem).Plays[((SubFormationVM)((TreeView)sender).SelectedItem).Plays.Count - 1].IsVisible = true;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = null;
                        tclTables.DataContext = TeamPlaybook;
                    }
                    if (((TreeView)sender).SelectedItem is PlayVM)
                    {
                        ((PlayVM)((TreeView)sender).SelectedItem).SubFormation.AddPlay((PlayVM)iData.GetData(typeof(PlayVM)), ((PlayVM)((TreeView)sender).SelectedItem).PBPL.ord_);
                        ((PlayVM)((TreeView)sender).SelectedItem).SubFormation.Plays[((PlayVM)((TreeView)sender).SelectedItem).PBPL.ord_ - 2].IsVisible = true;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = null;
                        tclTables.DataContext = TeamPlaybook;
                    }
                }

                if (iData.GetDataPresent(typeof(CustomPlaybookPLAY)))
                {
                    CustomPlaybookPLAY CPBplay = (CustomPlaybookPLAY)iData.GetData(typeof(CustomPlaybookPLAY));

                    if (((TreeView)sender).SelectedItem is SubFormationVM)
                    {
                        ((SubFormationVM)((TreeView)sender).SelectedItem).AddPlay(CPBplay, ((SubFormationVM)((TreeView)sender).SelectedItem).Plays.Count + 1);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                    if (((TreeView)sender).SelectedItem is PlayVM)
                    {
                        ((PlayVM)((TreeView)sender).SelectedItem).SubFormation.AddPlay(CPBplay, ((PlayVM)((TreeView)sender).SelectedItem).PBPL.ord_);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                }

                if (iData.GetDataPresent(typeof(CustomPlaybookSubFormation)))
                {
                    CustomPlaybookSubFormation CPBsubFormation = (CustomPlaybookSubFormation)iData.GetData(typeof(CustomPlaybookSubFormation));

                    if (((TreeView)sender).SelectedItem is FormationVM)
                    {
                        ((FormationVM)((TreeView)sender).SelectedItem).AddSubFormation(CPBsubFormation, ((FormationVM)((TreeView)sender).SelectedItem).SubFormations.Count + 1);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                    if (((TreeView)sender).SelectedItem is SubFormationVM)
                    {
                        ((SubFormationVM)((TreeView)sender).SelectedItem).Formation.AddSubFormation(CPBsubFormation, ((SubFormationVM)((TreeView)sender).SelectedItem).PBST.ord_);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                }

                if (iData.GetDataPresent(typeof(CustomPlaybookFormation)))
                {
                    CustomPlaybookFormation CPBformation = (CustomPlaybookFormation)iData.GetData(typeof(CustomPlaybookFormation));

                    if (((TreeView)sender).SelectedItem is FormationVM)
                    {
                        ((FormationVM)((TreeView)sender).SelectedItem).Playbook.AddFormation(CPBformation, ((FormationVM)((TreeView)sender).SelectedItem).PBFM.ord_);
                        tclTables.DataContext = null;
                        TeamPlaybook.ReIndexTables();
                        tclTables.DataContext = TeamPlaybook;
                    }
                }
            }
            else if (sender is MainWindow)
            {
                if (iData.GetDataPresent(typeof(FormationVM)))
                {
                    var obj = iData.GetData(typeof(FormationVM));

                    TeamPlaybook.AddFormation((FormationVM)iData.GetData(typeof(FormationVM)), (TeamPlaybook.Formations.Where(form => form.PBFM.FTYP == 1).ToList()).Count + 1);
                    tclTables.DataContext = null;
                    TeamPlaybook.ReIndexTables();
                    tclTables.DataContext = TeamPlaybook;
                }
            }
        }


        #endregion

        #region Delete

        private void deletePBAI(object sender, RoutedEventArgs e)
        {
            TeamPlaybook.PBAI.Clear();
            TeamPlaybook.ReIndexTables();
            tclTables.DataContext = null;
            tclTables.DataContext = TeamPlaybook;
        }

        private void deleteAllFormations(object sender, RoutedEventArgs e)
        {
            for (int i = TeamPlaybook.Formations.Count - 1; i >= 0; i--)
            {
                TeamPlaybook.RemoveFormation(TeamPlaybook.Formations[i]);
            }
            tclTables.Items.Refresh();
        }

        private void deleteAllSubFormations(object sender, RoutedEventArgs e)
        {
            foreach (FormationVM formation in TeamPlaybook.Formations)
            {
                for (int i = formation.SubFormations.Count - 1; i >= 0; i--)
                {
                    formation.RemoveSubFormation(formation.SubFormations[i]);
                }
            }
        }

        private void deleteAllPlays(object sender, RoutedEventArgs e)
        {
            foreach (FormationVM formation in TeamPlaybook.Formations)
            {
                foreach (SubFormationVM subFormation in formation.SubFormations)
                {
                    for (int i = subFormation.Plays.Count - 1; i >= 0; i--)
                    {
                        subFormation.RemovePlay(subFormation.Plays[i]);
                    }
                }
            }
        }

        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            if (((TreeView)sender).SelectedItem is FormationVM)
            {
                FormationVM Formation = ((FormationVM)((TreeView)sender).SelectedItem);
                Formation.Playbook.RemoveFormation(Formation);
                TeamPlaybook.ReIndexTables();
                tclTables.DataContext = null;
                tclTables.DataContext = TeamPlaybook;
                foreach (FormationVM formation in Formation.Playbook.Formations)
                {
                    formation.IsVisible = true;
                }
            }
            if (((TreeView)sender).SelectedItem is SubFormationVM)
            {
                SubFormationVM SubFormation = ((SubFormationVM)((TreeView)sender).SelectedItem);
                SubFormation.Formation.RemoveSubFormation(SubFormation);
                TeamPlaybook.ReIndexTables();
                tclTables.DataContext = null;
                tclTables.DataContext = TeamPlaybook;
                foreach (FormationVM formation in SubFormation.Formation.Playbook.Formations)
                {
                    if (formation.PBFM.pbfm != SubFormation.Formation.PBFM.pbfm)
                    {
                        formation.IsVisible = false;
                    }
                    else
                    {
                        formation.IsExpanded = true;
                    }
                }
            }
            if (((TreeView)sender).SelectedItem is PlayVM)
            {
                PlayVM Play = ((PlayVM)((TreeView)sender).SelectedItem);
                Play.SubFormation.RemovePlay(Play);
                TeamPlaybook.ReIndexTables();
                tclTables.DataContext = null;
                tclTables.DataContext = TeamPlaybook;
                foreach (FormationVM formation in Play.SubFormation.Formation.Playbook.Formations)
                {
                    if (formation.PBFM.pbfm != Play.SubFormation.Formation.PBFM.pbfm)
                    {
                        formation.IsVisible = false;
                    }
                    else
                    {
                        formation.IsExpanded = true;
                        foreach (SubFormationVM subFormation in formation.SubFormations)
                        {
                            if (subFormation.PBST.pbst != Play.SubFormation.PBST.pbst)
                            {
                                subFormation.IsVisible = false;
                            }
                            else
                            {
                                subFormation.IsExpanded = true;
                            }
                        }
                    }
                }
            }
       }

        #endregion

        #region Save

        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            TeamPlaybook.ReIndexTables();
            TeamPlaybook.SaveTDBTables(OpenIndex);
            if (!TDBAccess.TDB.TDBSave(OpenIndex))
            {
                MessageBox.Show("Error Saving");
            }
        }

        #endregion

        #region Set Generic Audibles

        private void setGenericAudibles(object sender, RoutedEventArgs e)
        {
            foreach (FormationVM formation in TeamPlaybook.Formations)
            {
                foreach (SubFormationVM subFormation in formation.SubFormations)
                {
                    subFormation.SetGenericAudibles();
                }
            }

            foreach (Madden.TeamPlaybook.PBAI pbai in TeamPlaybook.PBAI)
            {
                pbai.Flag = TeamPlaybook.PBPL.FirstOrDefault(play => play.pbpl == pbai.PBPL).Flag;
            }

            tclTables.DataContext = null;
            TeamPlaybook.ReIndexTables();
            tclTables.DataContext = TeamPlaybook;
        }

        #endregion

        #region PSAL Editor

        private void PSAL_Editor(object sender, RoutedEventArgs e)
        {
            Window window = new Window
            {
                Title = "PSAL Editor",
                Content = new PSAL_Editor(),
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.CanResize
            };

            window.ShowDialog();
        }

        #endregion

        #region UI Events

        #region Playbook TreeView Events

        private void tvwPlaybook_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (((TreeView)sender).SelectedItem is MaddenTeamPlaybookEditor.ViewModels.FormationVM)
            {

            }

            if (((TreeView)sender).SelectedItem is MaddenTeamPlaybookEditor.ViewModels.SubFormationVM)
            {
                uclSubFormationModal.subFormation = (SubFormationVM)((TreeView)sender).SelectedItem;
                uclSubFormationModal.DataContext = (SubFormationVM)((TreeView)sender).SelectedItem;
                uclPlayModal.Visibility = Visibility.Collapsed;
                uclSubFormationModal.Visibility = Visibility.Visible;
            }

            if (((TreeView)sender).SelectedItem is MaddenTeamPlaybookEditor.ViewModels.PlayVM)
            {
                uclPlayModal.play = (PlayVM)((TreeView)sender).SelectedItem;
                uclPlayModal.DataContext = (PlayVM)((TreeView)sender).SelectedItem;
                uclSubFormationModal.Visibility = Visibility.Collapsed;
                uclPlayModal.Visibility = Visibility.Visible;
                for (int i = 0; i < lvwSituations.Items.Count; i++)
                {
                    Madden.TeamPlaybook.PBAI _pbai = uclPlayModal.play.Situations.FirstOrDefault(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.Items[i]).AIGR);
                    if (_pbai != null)
                    {
                        ((Madden.TeamPlaybook.PBAI)lvwSituations.Items[i]).prct = _pbai.prct;
                    }
                    else
                    {
                        ((Madden.TeamPlaybook.PBAI)lvwSituations.Items[i]).prct = 0;
                    }
                }
                lvwSituations.Items.Refresh();
                //lvwSituations.SelectedItems.Clear();
                //foreach (Madden.TeamPlaybook.PBAI situation in uclPlayModal.play.Situations)
                //{
                //    lvwSituations.SelectedItems.Add(TeamPlaybook.Situations.FirstOrDefault(p => p.Key == situation.AIGR));
                //    Console.WriteLine(situation);
                //}
            }
        }

        private void tvwPlaybook_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void tvwPlaybook_Selected(object sender, RoutedEventArgs e)
        {
            //TreeViewItem tvi = e.OriginalSource as TreeViewItem;

            //if (tvi == null || e.Handled) return;

            //if (tvi.DataContext is PlayVM)
            //{
            //    tvi.IsExpanded = !tvi.IsExpanded;
            //    e.Handled = true;
            //}
        }

        private void tvwPSALs_Selected(object sender, RoutedEventArgs e)
        {
            //Working Code
            //PlayerVM _player = uclPlayModal.play != null ? uclPlayModal.play.Players.FirstOrDefault(p => p.IsSelected) : null;
            //if (tvwPSALs.SelectedItem is PlayVM && _player != null)
            //{
            //    _player.PLYS.PSAL = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.PSAL;
            //    _player.PLYS.ARTL = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.ARTL;
            //    _player.PLYS.PLRR = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.PLRR;
            //    _player.UpdatePlayer();
            //    _player.Play.UpdatePlay();
            //    UserControl _play = UIHelper.FindChild<UserControl>(tvwPlaybook, "uclPlay", uclPlayModal.play);
            //    ItemsControl playart = UIHelper.FindChild<ItemsControl>(_play, "iclPlayarts");
            //    if (playart != null)
            //    {
            //        playart.Items.Refresh();
            //    }
            //    uclPlayModal.iclPSALs.Items.Refresh();
            //    uclPlayModal.tabPlayer.Items.Refresh();
            //}


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

        #endregion

        private void treeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _lastMouseDown = e.GetPosition(tvwPlaybook);
            }
        }

        private void treeView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed && !(e.OriginalSource is TextBox))
                {
                    Point currentPosition = e.GetPosition(tvwPlaybook);

                    if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) || (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
                    {
                        draggedItem = !(GetNearestContainer(e.OriginalSource as UIElement).IsExpanded) ? GetNearestContainer(e.OriginalSource as UIElement) : null;

                        if (draggedItem != null)
                        {
                            //UIElement uiElement = e.OriginalSource as UIElement;
                            //adorner = new DragAdorner(uiElement, e.GetPosition(uiElement));
                            //AdornerLayer.GetAdornerLayer(uiElement).Add(adorner);

                            DragDropEffects finalDropEffect = DragDrop.DoDragDrop(tvwPlaybook, draggedItem, DragDropEffects.Move);

                            //Checking target is not null and item is dragging(moving)
                            if ((finalDropEffect == DragDropEffects.Move) && (_target != null))
                            {
                                // A Move drop was accepted
                                if (draggedItem != _target)
                                {
                                    MoveItem(draggedItem, _target);
                                }
                                _target = null;
                                draggedItem = null;
                                //AdornerLayer.GetAdornerLayer(uiElement).Remove(adorner);
                            }
                        }
                    }
                    _lastMouseDown = currentPosition;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("treeView_MouseMove failed");
            }
        }

        private void treeView_MouseUp(object sender, MouseEventArgs e)
        {
            AdornerLayer.GetAdornerLayer((UIElement)e.OriginalSource).Remove(adorner);
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                Point currentPosition = e.GetPosition(tvwPlaybook);

                if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) || (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
                {
                    // Verify that this is a valid drop and then store the drop target
                    TreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);
                    if (CheckDropTarget(draggedItem, item))
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                    }
                }
                e.Handled = true;
            }
            catch (Exception)
            {
                Console.WriteLine("treeView_DragOver failed");
            }
        }

        private void treeView_Drop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;

                // Verify that this is a valid drop and then store the drop target
                TreeViewItem TargetItem = GetNearestContainer(e.OriginalSource as UIElement);
                if (TargetItem != null && draggedItem != null)
                {
                    _target = TargetItem;
                    e.Effects = DragDropEffects.Move;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("treeView_Drop failed");
            }
        }

        private void treeView_PreviewGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (adorner != null)
            {
                var pos = ((UIElement)e.OriginalSource).PointFromScreen(GetMousePosition());
                adorner.UpdatePosition(pos);
            }
        }

        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }

        private bool CheckDropTarget(TreeViewItem _sourceItem, TreeViewItem _targetItem)
        {
            //Check whether the target item is meeting your condition
            bool _isEqual = false;
            if (_sourceItem?.DataContext?.GetType() == _targetItem?.DataContext?.GetType())
            {
                _isEqual = true;
                if (_sourceItem.DataContext is FormationVM)
                {
                    if (((FormationVM)_sourceItem.DataContext).PBFM.FTYP != ((FormationVM)_targetItem.DataContext).PBFM.FTYP)
                    {
                        _isEqual = false;
                    }
                }
            }
            return _isEqual;
        }

        private void MoveItem(TreeViewItem _sourceItem, TreeViewItem _targetItem)
        {
            try
            {
                //Move Formation
                if (_targetItem.DataContext is FormationVM && _sourceItem.DataContext is FormationVM && MessageBox.Show(((FormationVM)_sourceItem.DataContext).PBFM.name, "Move Formation?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    FormationVM sourceFormation = _sourceItem.DataContext as FormationVM;
                    FormationVM _targetFormation = _targetItem.DataContext as FormationVM;
                    int newIndex = _targetFormation.PBFM.ord_ - 1;
                    if (sourceFormation.Playbook == _targetFormation.Playbook)
                    {
                        ((FormationVM)_sourceItem.DataContext).Playbook.Formations.Remove((FormationVM)_sourceItem.DataContext);
                        ((FormationVM)_targetItem.DataContext).Playbook.Formations.Insert(newIndex, sourceFormation);

                        for (int i = 0; i < _targetFormation.Playbook.Formations.Count; i++)
                        {
                            if (_targetFormation.Playbook.Formations[i].PBFM.FTYP == sourceFormation.PBFM.FTYP)
                            {
                                _targetFormation.Playbook.Formations[i].PBFM.ord_ = i + 1;
                            }
                        }
                    }
                }

                //Move SubFormation
                if (_targetItem.DataContext is SubFormationVM && _sourceItem.DataContext is SubFormationVM && MessageBox.Show(((SubFormationVM)_sourceItem.DataContext).PBST.name, "Move Formation?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SubFormationVM sourceSubFormation = _sourceItem.DataContext as SubFormationVM;
                    SubFormationVM _targetSubFormation = _targetItem.DataContext as SubFormationVM;
                    int newIndex = _targetSubFormation.PBST.ord_ - 1;
                    if (sourceSubFormation.Formation == _targetSubFormation.Formation)
                    {
                        ((SubFormationVM)_sourceItem.DataContext).Formation.SubFormations.Remove((SubFormationVM)_sourceItem.DataContext);
                        ((SubFormationVM)_targetItem.DataContext).Formation.SubFormations.Insert(newIndex, sourceSubFormation);

                        for (int i = 0; i < _targetSubFormation.Formation.SubFormations.Count; i++)
                        {
                            _targetSubFormation.Formation.SubFormations[i].PBST.ord_ = i + 1;
                        }
                    }
                }

                //Move Play
                if (_targetItem.DataContext is PlayVM && _sourceItem.DataContext is PlayVM && MessageBox.Show(((PlayVM)_sourceItem.DataContext).PBPL.name, "Move Formation?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PlayVM sourcePlay = _sourceItem.DataContext as PlayVM;
                    PlayVM _targetPlay = _targetItem.DataContext as PlayVM;
                    int newIndex = _targetPlay.PBPL.ord_ - 1;
                    if (sourcePlay.SubFormation == _targetPlay.SubFormation)
                    {
                        ((PlayVM)_sourceItem.DataContext).SubFormation.Plays.Remove((PlayVM)_sourceItem.DataContext);
                        ((PlayVM)_targetItem.DataContext).SubFormation.Plays.Insert(newIndex, sourcePlay);

                        for (int i = 0; i < _targetPlay.SubFormation.Plays.Count; i++)
                        {
                            _targetPlay.SubFormation.Plays[i].PBPL.ord_ = i + 1;
                        }
                    }
                }

                tclTables.DataContext = null;
                TeamPlaybook.ReIndexTables();
                tclTables.DataContext = TeamPlaybook;
            }
            catch
            {
                Console.WriteLine("MoveItem failed");
            }
        }

	    public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private void cbxPLYT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxPLYT.SelectedValue != null)
            {
                List<PlayVM> _plays = new List<PlayVM>();
                foreach (PlayVM _play in ((TeamPlaybook)tvwPlaybook.DataContext).Plays)
                {
                    _play.IsSelected = false;
                    _play.IsExpanded = _play.PLYL.PLYT == (int)cbxPLYT.SelectedValue;
                }
                uclPlayModal.UpdateLayout();
                lvwPlaysByRouteDepth.Items.Filter = PlayListFilter;
            }
        }

        private void btnRevampGameplan_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Run Sabo's Gameplan Revamp?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    TeamPlaybook.RevampGameplan();
                    lvwSituations.Items.Refresh();
                }
                catch (Exception)
                {
                    throw;
                }
                if (MessageBox.Show("Would you like to adjust the gameplan based on this team's 2023 Regular Season tendencies? This will shorten the average route depth to the league average and match the run/pass ratio per team.\n\nThe in game lua offense script will need to be modified so that every redzone situation uses the 16-20 situation, since this will combine all of those plays into the 16-20 Situation.", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        TeamPlaybook.RedDobeRevampGameplan();
                        lvwSituations.Items.Refresh();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        private void lvwSituations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwSituations.SelectedItem != null)
            {
                //GetSituation();
                List<PlayVM> _plays = new List<PlayVM>();
                foreach (PlayVM _play in TeamPlaybook.Plays)
                {
                    _play.IsSelected = false;
                    _play.IsExpanded = TeamPlaybook.PBAI.Where(p => p.PBPL == _play.PBPL.pbpl && p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedValue).AIGR).Count() > 0;
                }
                uclPlayModal.UpdateLayout();
            }
        }

        private void iclGameplanPercent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (iclGameplanPercent.SelectedItem != null)
            //{
            //    foreach (PlayVM _play in ((TeamPlaybook)tvwPlaybook.DataContext).Plays)
            //    {
            //        _play.IsSelected = false;
            //        _play.IsExpanded = ((SituationVM)iclGameplanPercent.SelectedItem).Plays.Contains(_play);
            //    }
            //}
        }

        private void lvwSituations_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lvwSituations.SelectedItem != null)
            {
                //GetSituation();
            }
        }

        //private void GetSituation()
        //{
        //    List<PlayVM> _plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.IsExpanded || p.IsSelected).ToList();
        //    for (int i = 0; i < _plays.Count; i++)
        //    {
        //        Madden.TeamPlaybook.PBAI _pbai = _plays[i].Situations.FirstOrDefault(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR);
        //        if (_pbai != null)
        //        {
        //            _pbai.prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct;
        //        }
        //        else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct > 0)
        //        {
        //            Madden.TeamPlaybook.PBAI newPBAI = new Madden.TeamPlaybook.PBAI
        //            {
        //                AIGR = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR,
        //                Flag = _plays[i].PBPL.Flag,
        //                PBPL = _plays[i].PBPL.pbpl,
        //                PLF_ = _plays[i].PLYL.PLF_,
        //                PLYT = _plays[i].PLYL.PLYT,
        //                prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct,
        //                rec = TeamPlaybook.NextAvailableID((from pbai in ((TeamPlaybook)tvwPlaybook.DataContext).PBAI select pbai.rec).ToList()),
        //                SETL = _plays[i].SubFormation.SETL.setl,
        //                vpos = _plays[i].PLYL.vpos
        //            };
        //            ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Add(newPBAI);
        //            _plays[i].Situations.Add(newPBAI);
        //        }
        //        else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct == 0)
        //        {
        //            ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Remove(_pbai);
        //            _plays[i].Situations.Remove(_pbai);
        //        }
        //    }
        //    ((TeamPlaybook)tvwPlaybook.DataContext).PBAI = Madden.TeamPlaybook.PBAI.Sort(((TeamPlaybook)tvwPlaybook.DataContext).PBAI);
        //    uclPBAITable.dataGrid.Items.Refresh();

        //    List<SituationVM> PlayTypes = new List<SituationVM>();
        //    //int playCount = 0;
        //    int SelectedSituation = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR;
        //    int TotalSitWeight = TeamPlaybook.PBAI.Where(p => p.AIGR == SelectedSituation).Sum(p => p.prct);
        //    int Weight = 0;
        //    List<int> _plyl = ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Where(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR).Select(p => p.PLYT).Distinct().ToList();
        //    foreach (int playtype in _plyl)
        //    {
        //        List<Color> colors = typeof(Colors)
        //            .GetProperties()
        //            .Where(c =>
        //                ((Color)ColorConverter.ConvertFromString(c.Name)).R > 64 &&
        //                ((Color)ColorConverter.ConvertFromString(c.Name)).R < 248 &&
        //                ((Color)ColorConverter.ConvertFromString(c.Name)).G > 0 &&
        //                ((Color)ColorConverter.ConvertFromString(c.Name)).G < 192 &&
        //                ((Color)ColorConverter.ConvertFromString(c.Name)).B > 64 &&
        //                ((Color)ColorConverter.ConvertFromString(c.Name)).B < 248)
        //            .Select(c => ((Color)ColorConverter.ConvertFromString(c.Name)))
        //            .OrderBy(c => c.ToString())
        //            .ToList();
        //        //PlayTypes.Add(new SituationVM
        //        //{
        //        //    Title = TeamPlaybook.PlayType[playtype],
        //        //    ColorBrush = new SolidColorBrush(colors[PlayTypes.Count]),
        //        //    Plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.PLYL.PLYT == playtype).ToList()
        //        //});
        //        //playCount += PlayTypes[PlayTypes.Count() - 1].Plays.Count();
        //        PlayTypes.Add(new SituationVM
        //        {
        //            Title = TeamPlaybook.PlayType[playtype],
        //            ColorBrush = new SolidColorBrush(colors[PlayTypes.Count]),
        //            Plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.PLYL.PLYT == playtype).ToList(),
        //            Weight = TeamPlaybook.PBAI.Where(p => p.PLYT == playtype && p.AIGR == SelectedSituation).Sum(p => p.prct),
        //            Percentage = (float)Math.Round(((double)Weight / (double)TotalSitWeight) * 100, 1)
        //        });
        //    }
        //    foreach (SituationVM playtype in PlayTypes)
        //    {
        //        playtype.Percentage = (float)Math.Round(((double)playtype.Plays.Count / (double)playCount) * 100, 1);
        //    }
        //    PlayTypes = PlayTypes.OrderByDescending(p => p.Percentage).ToList();
        //    iclGameplanPercent.ItemsSource = PlayTypes;

        //    float angle = 0, prevAngle = 0;
        //    cvsGameplanPercent.Children.Clear();
        //    foreach (SituationVM playtype in PlayTypes)
        //    {
        //        double line1X = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
        //        double line1Y = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

        //        angle = playtype.Percentage * (float)360 / 100 + prevAngle;
        //        Debug.WriteLine(angle);

        //        double arcX = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
        //        double arcY = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

        //        var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
        //        double arcWidth = (cvsGameplanPercent.Width / 2), arcHeight = (cvsGameplanPercent.Width / 2);
        //        bool isLargeArc = playtype.Percentage > 50;
        //        var arcSegment = new ArcSegment()
        //        {
        //            Size = new Size(arcWidth, arcHeight),
        //            Point = new Point(arcX, arcY),
        //            SweepDirection = SweepDirection.Clockwise,
        //            IsLargeArc = isLargeArc,
        //        };
        //        var line2Segment = new LineSegment(new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)), false);

        //        var pathFigure = new PathFigure(
        //            new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)),
        //            new List<PathSegment>()
        //            {
        //            line1Segment,
        //            arcSegment,
        //            line2Segment,
        //            },
        //            true);

        //        var pathFigures = new List<PathFigure>() { pathFigure, };
        //        var pathGeometry = new PathGeometry(pathFigures);
        //        var path = angle == 360 && PlayTypes.Count == 1 ?
        //            new System.Windows.Shapes.Path()
        //            {
        //                Fill = playtype.ColorBrush,
        //                Data = new EllipseGeometry(pathFigure.StartPoint, cvsGameplanPercent.Width / 2, cvsGameplanPercent.Height / 2),
        //            } :
        //            new System.Windows.Shapes.Path()
        //            {
        //                Fill = playtype.ColorBrush,
        //                Data = pathGeometry,
        //            };
        //        cvsGameplanPercent.Children.Add(path);
        //        prevAngle = angle;


        //        // draw outlines
        //        //var outline1 = new System.Windows.Shapes.Line()
        //        //{
        //        //    X1 = (cvsGameplanPercent.Width / 2),
        //        //    Y1 = (cvsGameplanPercent.Height / 2),
        //        //    X2 = line1Segment.Point.X,
        //        //    Y2 = line1Segment.Point.Y,
        //        //    Stroke = Brushes.Black,
        //        //    StrokeThickness = 1,
        //        //};
        //        //var outline2 = new System.Windows.Shapes.Line()
        //        //{
        //        //    X1 = (cvsGameplanPercent.Width / 2),
        //        //    Y1 = (cvsGameplanPercent.Height / 2),
        //        //    X2 = arcSegment.Point.X,
        //        //    Y2 = arcSegment.Point.Y,
        //        //    Stroke = Brushes.Black,
        //        //    StrokeThickness = 1,
        //        //};

        //        //cvsGameplanPercent.Children.Add(outline1);
        //        //cvsGameplanPercent.Children.Add(outline2);
        //    }
        //}

        private void SaveAllPlayart(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Save Here"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG File (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                //progressBar.Value = 0;
                for (int n = 0; n < TeamPlaybook.Plays.Count; n++)
                {
                    // Save document
                    string filename = Path.GetDirectoryName(dlg.FileName) + "\\" + TeamPlaybook.Plays[n].PLYL.plyl.ToString() + Path.GetExtension(dlg.FileName);
                    SaveCanvasToFile(TeamPlaybook.Plays[n].ToCanvas(1, false), 96, filename);
                    //progressBar.Value = n / (TeamPlaybook.Plays.Count - 1);
                }
                //progressBar.Value = 0;
            }
        }

        private void SaveAllARTL(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Save Here"; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG File (.png)|*.png"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                //progressBar.Value = 0;
                for (int n = 0; n < TeamPlaybook.Plays.Count; n++)
                {
                    // Save document
                    string filename = Path.GetDirectoryName(dlg.FileName) + "\\" + TeamPlaybook.Plays[n].PLYL.plyl.ToString() + Path.GetExtension(dlg.FileName);
                    SaveCanvasToFile(TeamPlaybook.Plays[n].ToCanvas(1, false), 96, filename);
                    //progressBar.Value = n / (TeamPlaybook.Plays.Count - 1);
                }
                //progressBar.Value = 0;
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

        #endregion

        #region Hyperlink Request Navigate

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // for .NET Core you need to add UseShellExecute = true
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        #endregion
    }
}