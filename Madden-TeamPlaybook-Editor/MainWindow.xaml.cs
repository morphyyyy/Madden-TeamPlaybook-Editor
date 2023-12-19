using MaddenTeamPlaybookEditor.User_Controls;
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
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor
{
    public partial class MainWindow : Window
    {
        public string filePath;
        public int OpenIndex = -1;
        public TeamPlaybook TeamPlaybook;
        public MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook CustomPlaybook;
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

        #region BindPlaybook

        public void BindPlaybook(TeamPlaybook Playbook)
        {
            wdwPlaybookEditor.Title = "Madden Team Playbook Editor - " + Path.GetFileName(Playbook.filePath);
            tvwPlaybook.DataContext = Playbook;
            cbxPLYT.DataContext = TeamPlaybook.PlayType.OrderBy(s => s.Value);
            if (Playbook.Type == "Offense")
            {
                lvwSituations.DataContext = TeamPlaybook.SituationOff.Select(p => new Madden.TeamPlaybook.PBAI { AIGR = p.Key, Name = p.Value }).ToList();
            }
            else if (Playbook.Type == "Defense")
            {
                lvwSituations.DataContext = TeamPlaybook.SituationDef.Select(p => new Madden.TeamPlaybook.PBAI { AIGR = p.Key, Name = p.Value }).ToList();
            }
            //lvwPlaysByRouteDepth.DataContext = TeamPlaybook.Plays.Where(p => p.PLPD != null).OrderByDescending(p => p.AverageRouteDepth);
            tclTables.DataContext = Playbook;
            tvwPSALs.DataContext = Playbook.GetPSALlist();
            //tabPlaybook.DataContext = Playbook;
        }

        public void BindPlaybook(MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook Playbook)
        {
            wdwPlaybookEditor.Title = "Madden Team Playbook Editor - " + Path.GetFileName(Playbook.filePath);
            tvwPlaybook.DataContext = Playbook;
            //lvwSituations.DataContext = Playbook;
            tclTables.DataContext = Playbook;
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
                }
                //else if (dictionaty.Except(MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook.Tables).Count() == 0)
                //{
                //    CustomPlaybook = new MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook(filePath);
                //    //TeamPlaybook = new TeamPlaybook();
                //    //foreach (Madden20CustomPlaybookEditor.ViewModels.FormationVM formation in CustomPlaybook.Formations)
                //    //{
                //    //    TeamPlaybook.AddFormation(formation, TeamPlaybook.Formations.Count);
                //    //}
                //    BindPlaybook(CustomPlaybook);
                //}
                tvwPlaybook.Items.Refresh();
                tvwPlaybook.UpdateLayout();
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

        #region Search

        private void searchTDB(object sender, RoutedEventArgs e)
        {
            int PSAL = 0;
            try
            {
                PSAL = ((Madden.TeamPlaybook.PSAL)((PSALTable)((TabItem)tclTables.Items[14]).Content).uclPSALTable.dgdPSAL.SelectedItem).psal;
            }
            catch
            {
                MessageBox.Show("Select a PSAL in the tables");
                return;
            }

            List<PlayVM> Plays = new List<PlayVM>();
            foreach (FormationVM formation in TeamPlaybook.Formations)
            {
                foreach (SubFormationVM subFormation in formation.SubFormations)
                {
                    foreach (PlayVM play in subFormation.Plays)
                    {
                        foreach (Madden.TeamPlaybook.PLYS assignment in play.PLYS)
                        {
                            if (assignment.PSAL == PSAL)
                            {
                                Plays.Add(play);
                                break;
                            }
                        }
                    }
                }
            }
            foreach (PlayVM play in Plays)
            {
                Console.WriteLine(String.Format("{0,-20} {1,-20} {2,-20}", play.SubFormation.Formation.PBFM.name, play.SubFormation.PBST.name, play.PBPL.name));
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
                xpdPlayModal.Visibility = Visibility.Collapsed;
                xpdSubFormationModal.Visibility = Visibility.Visible;
            }

            if (((TreeView)sender).SelectedItem is MaddenTeamPlaybookEditor.ViewModels.PlayVM)
            {
                uclPlayModal.play = (PlayVM)((TreeView)sender).SelectedItem;
                uclPlayModal.DataContext = (PlayVM)((TreeView)sender).SelectedItem;
                xpdSubFormationModal.Visibility = Visibility.Collapsed;
                xpdPlayModal.Visibility = Visibility.Visible;
                for (int i = 0; i < lvwSituations.Items.Count; i++)
                {
                    Madden.TeamPlaybook.PBAI _pbai = uclPlayModal.play.Situations.Where(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.Items[i]).AIGR).FirstOrDefault();
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
                //    lvwSituations.SelectedItems.Add(TeamPlaybook.Situations.Where(p => p.Key == situation.AIGR).FirstOrDefault());
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
            PlayerVM _player = uclPlayModal.play != null ? uclPlayModal.play.Players.Where(p => p.IsSelected).FirstOrDefault() : null;
            if (tvwPSALs.SelectedItem is PlayVM && _player != null)
            {
                _player.PLYS.PSAL = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.PSAL;
                _player.PLYS.ARTL = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.ARTL;
                _player.PLYS.PLRR = ((PlayVM)tvwPSALs.SelectedItem).Players[0].PLYS.PLRR;
                _player.UpdatePlayer();
                _player.Play.UpdatePlay();
                UserControl _play = UIHelper.FindChild<UserControl>(tvwPlaybook, "uclPlay", uclPlayModal.play);
                ItemsControl playart = UIHelper.FindChild<ItemsControl>(_play, "iclPlayarts");
                if (playart != null)
                {
                    playart.Items.Refresh();
                }
                uclPlayModal.iclPSALs.Items.Refresh();
                uclPlayModal.tabPlayer.Items.Refresh();
            }
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
                    Console.WriteLine(currentPosition);
                    Console.WriteLine(_lastMouseDown);

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
            if (_sourceItem.DataContext.GetType() == _targetItem.DataContext.GetType())
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
            }
        }

        private void btnRevampGameplan_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Revamp the Gameplan?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                TeamPlaybook.RevampGameplan();
                if (TeamPlaybook.PBAI.Count > 2000)
                {
                    int threshold = 1;
                    List<Madden.TeamPlaybook.PBAI> _pbai = TeamPlaybook.PBAI.Where(p => p.prct == threshold && TeamPlaybook.PBAI.Select(n => n.AIGR > threshold) != null).ToList();
                    if (MessageBox.Show("There are " + (2000 - TeamPlaybook.PBAI.Count).ToString() + " too many PBAI records and the game will crash.\nWould you like to remove " + _pbai.Count + " records with a 1 prct?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        TeamPlaybook.PBAI.RemoveAll(p => _pbai.Contains(p));
                    }
                }
                lvwSituations.Items.Refresh();
            }
        }

        private void lvwSituations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwSituations.SelectedItem != null)
            {
                List<PlayVM> _plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.IsExpanded || p.IsSelected).ToList();
                for (int i = 0; i < _plays.Count; i++)
                {
                    Madden.TeamPlaybook.PBAI _pbai = _plays[i].Situations.Where(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR).FirstOrDefault();
                    if (_pbai != null)
                    {
                        _pbai.prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct;
                    }
                    else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct > 0)
                    {
                        Madden.TeamPlaybook.PBAI newPBAI = new Madden.TeamPlaybook.PBAI
                        {
                            AIGR = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR,
                            Flag = _plays[i].PBPL.Flag,
                            PBPL = _plays[i].PBPL.pbpl,
                            PLF_ = _plays[i].PLYL.PLF_,
                            PLYT = _plays[i].PLYL.PLYT,
                            prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct,
                            rec = TeamPlaybook.NextAvailableID((from pbai in ((TeamPlaybook)tvwPlaybook.DataContext).PBAI select pbai.rec).ToList()),
                            SETL = _plays[i].SubFormation.SETL.setl,
                            vpos = _plays[i].PLYL.vpos
                        };
                        ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Add(newPBAI);
                        _plays[i].Situations.Add(newPBAI);
                    }
                    else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct == 0)
                    {
                        ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Remove(_pbai);
                        _plays[i].Situations.Remove(_pbai);
                    }
                }
                ((TeamPlaybook)tvwPlaybook.DataContext).PBAI = Madden.TeamPlaybook.PBAI.Sort(((TeamPlaybook)tvwPlaybook.DataContext).PBAI);
                uclPBAITable.dgdPBAI.Items.Refresh();

                List<SituationVM> PlayTypes = new List<SituationVM>();
                int playCount = 0;
                List<int> _plyl = ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Where(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR).Select(p => p.PLYT).Distinct().ToList();
                Random random255 = new Random();
                foreach (int playtype in _plyl)
                {
                    PlayTypes.Add(new SituationVM { Title = TeamPlaybook.PlayType[playtype], ColorBrush = new SolidColorBrush(Color.FromArgb((byte)160, (byte)random255.Next(0, 255), (byte)random255.Next(0, 255), (byte)random255.Next(0, 255))), Plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.PLYL.PLYT == playtype).ToList() });
                    playCount += PlayTypes[PlayTypes.Count() - 1].Plays.Count();
                }
                foreach (SituationVM playtype in PlayTypes)
                {
                    playtype.Percentage = (float)Math.Round(((double)playtype.Plays.Count / (double)playCount) * 100, 1);
                }
                PlayTypes = PlayTypes.OrderByDescending(p => p.Percentage).ToList();
                iclGameplanPercent.ItemsSource = PlayTypes;

                float angle = 0, prevAngle = 0;
                cvsGameplanPercent.Children.Clear();
                foreach (SituationVM playtype in PlayTypes)
                {
                    double line1X = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
                    double line1Y = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

                    angle = playtype.Percentage * (float)360 / 100 + prevAngle;
                    Debug.WriteLine(angle);

                    double arcX = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
                    double arcY = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

                    var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                    double arcWidth = (cvsGameplanPercent.Width / 2), arcHeight = (cvsGameplanPercent.Width / 2);
                    bool isLargeArc = playtype.Percentage > 50;
                    var arcSegment = new ArcSegment()
                    {
                        Size = new Size(arcWidth, arcHeight),
                        Point = new Point(arcX, arcY),
                        SweepDirection = SweepDirection.Clockwise,
                        IsLargeArc = isLargeArc,
                    };
                    var line2Segment = new LineSegment(new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)), false);

                    var pathFigure = new PathFigure(
                        new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)),
                        new List<PathSegment>()
                        {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                        },
                        true);

                    var pathFigures = new List<PathFigure>() { pathFigure, };
                    var pathGeometry = new PathGeometry(pathFigures);
                    var path = angle == 360 ?
                        new System.Windows.Shapes.Path()
                        {
                            Fill = playtype.ColorBrush,
                            Data = new EllipseGeometry(pathFigure.StartPoint, cvsGameplanPercent.Width / 2, cvsGameplanPercent.Height / 2),
                        } :
                        new System.Windows.Shapes.Path()
                        {
                            Fill = playtype.ColorBrush,
                            Data = pathGeometry,
                        };
                    cvsGameplanPercent.Children.Add(path);
                    prevAngle = angle;


                    // draw outlines
                    //var outline1 = new System.Windows.Shapes.Line()
                    //{
                    //    X1 = (cvsGameplanPercent.Width / 2),
                    //    Y1 = (cvsGameplanPercent.Height / 2),
                    //    X2 = line1Segment.Point.X,
                    //    Y2 = line1Segment.Point.Y,
                    //    Stroke = Brushes.Black,
                    //    StrokeThickness = 1,
                    //};
                    //var outline2 = new System.Windows.Shapes.Line()
                    //{
                    //    X1 = (cvsGameplanPercent.Width / 2),
                    //    Y1 = (cvsGameplanPercent.Height / 2),
                    //    X2 = arcSegment.Point.X,
                    //    Y2 = arcSegment.Point.Y,
                    //    Stroke = Brushes.Black,
                    //    StrokeThickness = 1,
                    //};

                    //cvsGameplanPercent.Children.Add(outline1);
                    //cvsGameplanPercent.Children.Add(outline2);
                }
            }
        }

        private void iclGameplanPercent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (iclGameplanPercent.SelectedItem != null)
            {
                foreach (PlayVM _play in ((TeamPlaybook)tvwPlaybook.DataContext).Plays)
                {
                    _play.IsSelected = false;
                    _play.IsExpanded = ((SituationVM)iclGameplanPercent.SelectedItem).Plays.Contains(_play);
                }
            }
        }

        private void lvwSituations_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lvwSituations.SelectedItem != null)
            {
                List<PlayVM> _plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.IsExpanded || p.IsSelected).ToList();
                for (int i = 0; i < _plays.Count; i++)
                {
                    Madden.TeamPlaybook.PBAI _pbai = _plays[i].Situations.Where(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR).FirstOrDefault();
                    if (_pbai != null)
                    {
                        _pbai.prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct;
                    }
                    else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct > 0)
                    {
                        Madden.TeamPlaybook.PBAI newPBAI = new Madden.TeamPlaybook.PBAI
                        {
                            AIGR = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR,
                            Flag = _plays[i].PBPL.Flag,
                            PBPL = _plays[i].PBPL.pbpl,
                            PLF_ = _plays[i].PLYL.PLF_,
                            PLYT = _plays[i].PLYL.PLYT,
                            prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct,
                            rec = TeamPlaybook.NextAvailableID((from pbai in ((TeamPlaybook)tvwPlaybook.DataContext).PBAI select pbai.rec).ToList()),
                            SETL = _plays[i].SubFormation.SETL.setl,
                            vpos = _plays[i].PLYL.vpos
                        };
                        ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Add(newPBAI);
                        _plays[i].Situations.Add(newPBAI);
                    }
                    else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct == 0)
                    {
                        ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Remove(_pbai);
                        _plays[i].Situations.Remove(_pbai);
                    }
                }
                ((TeamPlaybook)tvwPlaybook.DataContext).PBAI = Madden.TeamPlaybook.PBAI.Sort(((TeamPlaybook)tvwPlaybook.DataContext).PBAI);
                uclPBAITable.dgdPBAI.Items.Refresh();

                List<SituationVM> PlayTypes = new List<SituationVM>();
                int playCount = 0;
                List<int> _plyl = ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Where(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR).Select(p => p.PLYT).Distinct().ToList();
                Random random255 = new Random();
                foreach (int playtype in _plyl)
                {
                    PlayTypes.Add(new SituationVM { Title = TeamPlaybook.PlayType[playtype], ColorBrush = new SolidColorBrush(Color.FromArgb((byte)160, (byte)random255.Next(0, 255), (byte)random255.Next(0, 255), (byte)random255.Next(0, 255))), Plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.PLYL.PLYT == playtype).ToList() });
                    playCount += PlayTypes[PlayTypes.Count() - 1].Plays.Count();
                }
                foreach (SituationVM playtype in PlayTypes)
                {
                    playtype.Percentage = (float)Math.Round(((double)playtype.Plays.Count / (double)playCount) * 100, 1);
                }
                PlayTypes = PlayTypes.OrderByDescending(p => p.Percentage).ToList();
                iclGameplanPercent.ItemsSource = PlayTypes;

                float angle = 0, prevAngle = 0;
                cvsGameplanPercent.Children.Clear();
                foreach (SituationVM playtype in PlayTypes)
                {
                    double line1X = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
                    double line1Y = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

                    angle = playtype.Percentage * (float)360 / 100 + prevAngle;
                    Debug.WriteLine(angle);

                    double arcX = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
                    double arcY = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

                    var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
                    double arcWidth = (cvsGameplanPercent.Width / 2), arcHeight = (cvsGameplanPercent.Width / 2);
                    bool isLargeArc = playtype.Percentage > 50;
                    var arcSegment = new ArcSegment()
                    {
                        Size = new Size(arcWidth, arcHeight),
                        Point = new Point(arcX, arcY),
                        SweepDirection = SweepDirection.Clockwise,
                        IsLargeArc = isLargeArc,
                    };
                    var line2Segment = new LineSegment(new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)), false);

                    var pathFigure = new PathFigure(
                        new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)),
                        new List<PathSegment>()
                        {
                        line1Segment,
                        arcSegment,
                        line2Segment,
                        },
                        true);

                    var pathFigures = new List<PathFigure>() { pathFigure, };
                    var pathGeometry = new PathGeometry(pathFigures);
                    var path = angle == 360 ?
                        new System.Windows.Shapes.Path()
                        {
                            Fill = playtype.ColorBrush,
                            Data = new EllipseGeometry(pathFigure.StartPoint, cvsGameplanPercent.Width / 2, cvsGameplanPercent.Height / 2),
                        } :
                        new System.Windows.Shapes.Path()
                        {
                            Fill = playtype.ColorBrush,
                            Data = pathGeometry,
                        };
                    cvsGameplanPercent.Children.Add(path);
                    prevAngle = angle;


                    // draw outlines
                    //var outline1 = new System.Windows.Shapes.Line()
                    //{
                    //    X1 = (cvsGameplanPercent.Width / 2),
                    //    Y1 = (cvsGameplanPercent.Height / 2),
                    //    X2 = line1Segment.Point.X,
                    //    Y2 = line1Segment.Point.Y,
                    //    Stroke = Brushes.Black,
                    //    StrokeThickness = 1,
                    //};
                    //var outline2 = new System.Windows.Shapes.Line()
                    //{
                    //    X1 = (cvsGameplanPercent.Width / 2),
                    //    Y1 = (cvsGameplanPercent.Height / 2),
                    //    X2 = arcSegment.Point.X,
                    //    Y2 = arcSegment.Point.Y,
                    //    Stroke = Brushes.Black,
                    //    StrokeThickness = 1,
                    //};

                    //cvsGameplanPercent.Children.Add(outline1);
                    //cvsGameplanPercent.Children.Add(outline2);
                }
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