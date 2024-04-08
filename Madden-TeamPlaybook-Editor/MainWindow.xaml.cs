using Madden.Team;
using Madden.TeamPlaybook;
using MaddenCustomPlaybookEditor;
using MaddenTeamPlaybookEditor.Classes;
using MaddenTeamPlaybookEditor.User_Controls;
using MaddenTeamPlaybookEditor.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using IDataObject = System.Windows.IDataObject;

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

            filePath = "E:\\Software\\MMC_Editor\\Madden 24\\All_Legacy_Files\\common\\database\\playbooks\\teams\\madden_49ers.db.DB";

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
            SetTeamColors(this.TeamPlaybook.TeamColor.Value);

            wdwPlaybookEditor.Title = "Madden Team Playbook Editor - " + Path.GetFileName(Playbook.filePath);
            tvwPlaybook.DataContext = Playbook;
            List<KeyValuePair<int?, string>> playTypes = TeamPlaybook.PlayType.Where(x => TeamPlaybook.PLYL.Any(y => y.PLYT == x.Key)).OrderBy(s => s.Value).ToList();
            playTypes.Insert(0, new KeyValuePair<int?, string>(null, "None"));
            //cbxPLYT.DataContext = playTypes;
            List<KeyValuePair<int?, string>> situations = new List<KeyValuePair<int?, string>>();
            if (Playbook.Type == "Offense")
            {
                situations = TeamPlaybook.SituationOff.OrderBy(s => s.Value).ToList();
            }
            else if (Playbook.Type == "Defense")
            {
                situations = TeamPlaybook.SituationDef.OrderBy(s => s.Value).ToList();
            }
            //situations.Insert(0, new KeyValuePair<int?, string>(null, "None"));
            lvwSituations.ItemsSource = situations;
            lvwPlaysBySituation.ItemsSource = TeamPlaybook.PBAI;
            lvwPlaysBySituation.Items.Filter = SituationFilter;
            {
                lvwPlaysBySituation.Items.SortDescriptions.Clear();
                lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Type", System.ComponentModel.ListSortDirection.Ascending));
                lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("prct", System.ComponentModel.ListSortDirection.Descending));
                lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("PlayName", System.ComponentModel.ListSortDirection.Ascending));
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvwPlaysBySituation.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Type");
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(groupDescription);
            }
            //lvw1stDown.ItemsSource = TeamPlaybook.SitOff_1stDown;
            //lvw2ndandShort.ItemsSource = TeamPlaybook.SitOff_2ndandShort;
            //lvw2ndandMed.ItemsSource = TeamPlaybook.SitOff_2ndandMed;
            //lvw2ndandLong.ItemsSource = TeamPlaybook.SitOff_2ndandLong;
            //lvw3rdandShort.ItemsSource = TeamPlaybook.SitOff_3rdandShort;
            //lvw3rdandMed.ItemsSource = TeamPlaybook.SitOff_3rdandMed;
            //lvw3rdandLong.ItemsSource = TeamPlaybook.SitOff_3rdandLong;
            //lvw3rdandXLong.ItemsSource = TeamPlaybook.SitOff_3rdandXLong;
            //lvw4thandShort.ItemsSource = TeamPlaybook.SitOff_4thandShort;
            //lvw4thandMed.ItemsSource = TeamPlaybook.SitOff_4thandMed;
            //lvw4thandLong.ItemsSource = TeamPlaybook.SitOff_4thandLong;
            //lvw4thandXLong.ItemsSource = TeamPlaybook.SitOff_4thandXLong;
            //lvwRZ21to25.ItemsSource = TeamPlaybook.SitOff_RZ21to25;
            //lvwRZ16to20.ItemsSource = TeamPlaybook.SitOff_RZ16to20;
            //lvwRZ11to15.ItemsSource = TeamPlaybook.SitOff_RZ11to15;
            //lvwRZ6to10.ItemsSource = TeamPlaybook.SitOff_RZ6to10;
            //lvwRZ3to5.ItemsSource = TeamPlaybook.SitOff_RZ3to5;
            //lvwRedZone.ItemsSource = TeamPlaybook.SitOff_RedZone;
            //lvwInsideFive.ItemsSource = TeamPlaybook.SitOff_InsideFive;
            //lvwGoalLine.ItemsSource = TeamPlaybook.SitOff_GoalLine;
            //lvwGoalLinePass.ItemsSource = TeamPlaybook.SitOff_GoalLinePass;
            //lvwCM2min.ItemsSource = TeamPlaybook.SitOff_CM2min;
            //lvwCM4min.ItemsSource = TeamPlaybook.SitOff_CM4min;
            //lvwCMKneel.ItemsSource = TeamPlaybook.SitOff_CMKneel;
            //lvwCMStopClock.ItemsSource = TeamPlaybook.SitOff_CMStopClock;
            //lvwCMStopClockUser.ItemsSource = TeamPlaybook.SitOff_CMStopClockUser;
            //lvwCMStopClockFakeUser.ItemsSource = TeamPlaybook.SitOff_CMStopClockFakeUser;
            //lvwCMWasteTime.ItemsSource = TeamPlaybook.SitOff_CMWasteTime;
            //lvwSTExtraPoint.ItemsSource = TeamPlaybook.SitOff_STExtraPoint;
            //lvwSTFakeFG.ItemsSource = TeamPlaybook.SitOff_STFakeFG;
            //lvwSTFakePunt.ItemsSource = TeamPlaybook.SitOff_STFakePunt;
            //lvwSTKickoff.ItemsSource = TeamPlaybook.SitOff_STKickoff;
            //lvwSTKickoffOnside.ItemsSource = TeamPlaybook.SitOff_STKickoffOnside;
            //lvwSTKickoffSafety.ItemsSource = TeamPlaybook.SitOff_STKickoffSafety;
            //lvwSTPunt.ItemsSource = TeamPlaybook.SitOff_STPunt;
            //lvwSTPuntMaxProtect.ItemsSource = TeamPlaybook.SitOff_STPuntMaxProtect;
            //lvwSTSquib.ItemsSource = TeamPlaybook.SitOff_STSquib;
            //lvwMisc1stPlay.ItemsSource = TeamPlaybook.SitOff_Misc1stPlay;
            //lvwMiscGoforTwo.ItemsSource = TeamPlaybook.SitOff_MiscGoforTwo;
            //lvwMiscHailMary.ItemsSource = TeamPlaybook.SitOff_MiscHailMary;
            //lvwMiscLastPlay.ItemsSource = TeamPlaybook.SitOff_MiscLastPlay;
            //lvwMiscMax.ItemsSource = TeamPlaybook.SitOff_MiscMax;
            //lvwMiscPlayAction.ItemsSource = TeamPlaybook.SitOff_MiscPlayAction;
            //lvwMiscSigniaturePlays.ItemsSource = TeamPlaybook.SitOff_MiscSigniaturePlays;
            //lvwMiscSuddenChange.ItemsSource = TeamPlaybook.SitOff_MiscSuddenChange;
            tclTables.DataContext = Playbook;
            //tabPlaybook.DataContext = Playbook;
            //IEnumerable<PlayVM> playsWithoutCode58 = TeamPlaybook.Plays.Where(p => TeamPlaybook.Gameplan.Run.Contains(p.PLYL.PLYT) && !p.Players.FirstOrDefault(r => r.PLYS.poso == 0).PSAL.Exists(r => r.code == 58));
        }

        public bool SituationFilter(object obj)
        {
            return ((Madden.TeamPlaybook.PBAI)obj).AIGR == (int?)lvwSituations.SelectedValue;

            //((Madden.TeamPlaybook.PBAI)lvw1stDown.SelectedItem)?.AIGR;
        }

        private bool PlayListFilter(object obj)
        {
            //if (cbxPLYT.SelectedValue != null && lvwSituations.SelectedValue != null)
            //{
            //    return ((PlayVM)obj).Situations.Exists(pbai => pbai.PLYT == (int)cbxPLYT.SelectedValue) && ((PlayVM)obj).Situations.Exists(pbai => pbai.AIGR == (int)lvwSituations.SelectedValue);
            //}
            //else if (cbxPLYT.SelectedValue != null && lvwSituations.SelectedValue == null)
            //{
            //    return ((PlayVM)obj).Situations.Exists(pbai => pbai.PLYT == (int)cbxPLYT.SelectedValue);
            //}
            //else if (cbxPLYT.SelectedValue == null && lvwSituations.SelectedValue != null)
            //{
            //    return ((PlayVM)obj).Situations.Exists(pbai => pbai.AIGR == (int)lvwSituations.SelectedValue);
            //}

            if (lvwSituations.SelectedValue != null)
            {
                return ((PlayVM)obj).Situations.Exists(pbai => pbai.AIGR == (int)lvwSituations.SelectedValue);
            }
            return false;
        }

        public void BindPlaybook(MaddenCustomPlaybookEditor.ViewModels.CustomPlaybook Playbook)
        {
            wdwPlaybookEditor.Title = "Madden Team Playbook Editor - " + Path.GetFileName(Playbook.filePath);
            tclCustomPlaybook.DataContext = Playbook;
        }

        public void SetTeamColors(List<string> TeamColors)
        {
            if (TeamColors == null) return;

            List<Color> TeamColorsHex = new List<Color>();
            foreach (string hex in TeamColors)
            {
                Color color = (Color)ColorConverter.ConvertFromString(hex);
                TeamColorsHex.Add(color);
            }
            if (TeamColors[0] == "#010101")
            {
                TeamColorsHex[0] = (Color)ColorConverter.ConvertFromString(TeamColors[1]);
                TeamColorsHex[1] = TeamColors.Count() > 3 ? (Color)ColorConverter.ConvertFromString(TeamColors[3]) : (Color)ColorConverter.ConvertFromString(TeamColors[2]);
            }
            Application.Current.Resources["Primary"] = TeamColorsHex[0];
            Application.Current.Resources["Secondary"] = TeamColorsHex[1];
            Application.Current.Resources["Tertiary"] = TeamColorsHex[2];
            Application.Current.Resources["Quaternary"] = TeamColorsHex.Count() > 3 ? TeamColorsHex[3] : TeamColorsHex[0];
            Application.Current.Resources["Quinary"] = TeamColorsHex.Count() > 4 ? TeamColorsHex[4] : TeamColorsHex[1];
            Application.Current.Resources["Senary"] = TeamColorsHex.Count() > 5 ? TeamColorsHex[5] : TeamColorsHex[2];

            //ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/" + TeamPlaybook.TeamColor.Key + ".png"));
            //imageBrush.Viewport = new Rect(0, 0, 384, 384);
            //imageBrush.ViewportUnits = BrushMappingMode.Absolute;
            //imageBrush.TileMode = TileMode.None;
            //imageBrush.Stretch = Stretch.UniformToFill;
            //imageBrush.AlignmentX = AlignmentX.Center;
            //imageBrush.AlignmentY = AlignmentY.Center;

            //cvsTeamHelmet.Background = imageBrush;
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
                Console.WriteLine(uclPlayModal.play.PBPL.Flag);
                uclPlayModal.DataContext = (PlayVM)((TreeView)sender).SelectedItem;
                uclSubFormationModal.Visibility = Visibility.Collapsed;
                uclPlayModal.Visibility = Visibility.Visible;
                if (!uclPlayModal.play.Players.ToList().Exists(p => p.IsSelected))
                {
                    uclPlayModal.play.Players[0].IsSelected = true;
                }
                uclPlayModal.tabPlayer.DataContext = uclPlayModal.play.Players.FirstOrDefault(player => player.IsSelected);
                uclPlayModal.GetPlayer();
                //for (int i = 0; i < lvwSituations.Items.Count; i++)
                //{
                //    Madden.TeamPlaybook.PBAI _pbai = uclPlayModal.play.Situations.FirstOrDefault(p => p.AIGR == ((KeyValuePair<int, string>)lvwSituations.Items[i]).Key);
                //}
                //lvwSituations.Items.Refresh();
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

        #endregion

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
            if (/*cbxPLYT.SelectedValue != null && */lvwPlaysBySituation != null)
            {
                //foreach (PlayVM _play in TeamPlaybook.Plays)
                //{
                //    _play.IsSelected = false;
                //    _play.IsExpanded = _play.Situations.Exists(pbai => pbai.PLYT == (int)cbxPLYT.SelectedValue);
                //}
                //uclPlayModal.UpdateLayout();
                lvwPlaysBySituation.Items.Filter = SituationFilter;
            }
        }

        private void btnRevampGameplan_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Run Sabo's Gameplan Revamp?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    TeamPlaybook.RevampGameplan();
                    lvwPlaysBySituation.ItemsSource = TeamPlaybook.PBAI;
                    {
                        lvwPlaysBySituation.Items.SortDescriptions.Clear();
                        lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Type", System.ComponentModel.ListSortDirection.Ascending));
                        lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("prct", System.ComponentModel.ListSortDirection.Descending));
                        lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("PlayName", System.ComponentModel.ListSortDirection.Ascending));
                        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvwPlaysBySituation.ItemsSource);
                        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Type");
                        view.GroupDescriptions.Clear();
                        view.GroupDescriptions.Add(groupDescription);
                    }

                    lvwPlaysBySituation.Items.Refresh();
                }
                catch (Exception)
                {
                    throw;
                }
                if (TeamPlaybook.Type == "Offense")
                {
                    if (MessageBox.Show("Would you like to adjust the gameplan based on this team's 2023 Regular Season tendencies? This will shorten the average route depth to the league average and match the run/pass ratio per team.\n\nThe in game lua offense script will need to be modified so that every redzone situation uses the 16-20 situation, since this will combine all of those plays into the 16-20 Situation.", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            TeamPlaybook.RevampGameplanRedDobe();
                            lvwPlaysBySituation.ItemsSource = TeamPlaybook.PBAI;
                            {
                                lvwPlaysBySituation.Items.SortDescriptions.Clear();
                                lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Type", System.ComponentModel.ListSortDirection.Ascending));
                                lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("prct", System.ComponentModel.ListSortDirection.Descending));
                                lvwPlaysBySituation.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("PlayName", System.ComponentModel.ListSortDirection.Ascending));
                                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvwPlaysBySituation.ItemsSource);
                                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Type");
                                view.GroupDescriptions.Clear();
                                view.GroupDescriptions.Add(groupDescription);
                            }

                            lvwPlaysBySituation.Items.Refresh();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        private void lvwSituations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwSituations.SelectedItem != null && lvwPlaysBySituation != null)
            {
                GetPlayPercentages();
                //foreach (PlayVM _play in TeamPlaybook.Plays)
                //{
                //    _play.IsSelected = false;
                //    _play.IsExpanded = TeamPlaybook.PBAI.Where(p => p.PBPL == _play.PBPL.pbpl && p.AIGR == ((KeyValuePair<int, string>)lvwSituations.SelectedItem).Key).Count() > 0;
                //}
                //uclPlayModal.UpdateLayout();
                //lvwPlaysBySituation.DataContext = TeamPlaybook.Plays.OrderBy(p => p.Situations.FirstOrDefault(s => s.AIGR == ((KeyValuePair<int?, string>)(lvwSituations.SelectedItem)).Key)?.prct);
                lvwPlaysBySituation.Items.Filter = SituationFilter;
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
            //GetPlayPercentages();
        }

        private void GetPlayPercentages()
        {
            //List<PlayVM> _plays = TeamPlaybook.Plays.Where(p => p.IsExpanded || p.IsSelected).ToList();
            //for (int i = 0; i < _plays.Count; i++)
            //{
            //    Madden.TeamPlaybook.PBAI _pbai = _plays[i].Situations.FirstOrDefault(p => p.AIGR == ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR);
            //    if (_pbai != null)
            //    {
            //        _pbai.prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct;
            //    }
            //    else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct > 0)
            //    {
            //        Madden.TeamPlaybook.PBAI newPBAI = new Madden.TeamPlaybook.PBAI
            //        {
            //            AIGR = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).AIGR,
            //            Flag = _plays[i].PBPL.Flag,
            //            PBPL = _plays[i].PBPL.pbpl,
            //            PLF_ = _plays[i].PLYL.PLF_,
            //            PLYT = _plays[i].PLYL.PLYT,
            //            prct = ((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct,
            //            rec = TeamPlaybook.NextAvailableID((from pbai in TeamPlaybook.PBAI select pbai.rec).ToList()) ?? TeamPlaybook.PBAI.Select(p => p.rec).Max() + 1,
            //            SETL = _plays[i].SubFormation.SETL.setl,
            //            vpos = _plays[i].PLYL.vpos
            //        };
            //        ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Add(newPBAI);
            //        _plays[i].Situations.Add(newPBAI);
            //    }
            //    else if (((Madden.TeamPlaybook.PBAI)lvwSituations.SelectedItem).prct == 0)
            //    {
            //        ((TeamPlaybook)tvwPlaybook.DataContext).PBAI.Remove(_pbai);
            //        _plays[i].Situations.Remove(_pbai);
            //    }
            //}
            //((TeamPlaybook)tvwPlaybook.DataContext).PBAI = Madden.TeamPlaybook.PBAI.Sort(((TeamPlaybook)tvwPlaybook.DataContext).PBAI);
            //uclPBAITable.dataGrid.Items.Refresh();

            //int SelectedSituation = ((KeyValuePair<int?, string>)lvwSituations.SelectedItem).Key ?? -1;
            //if (lvwSituations.SelectedItem == null || /*cbxPLYT.SelectedItem == null || */SelectedSituation == -1)
            //{
            //    return;
            //}
            //List<SituationVM> PlayTypes = new List<SituationVM>();
            //int playCount = 0;
            //int TotalSitWeight = TeamPlaybook.PBAI.Where(p => p.AIGR == SelectedSituation).Sum(p => p.prct);
            //int Weight = 0;
            //List<int> _plyl = TeamPlaybook.PBAI.Where(p => p.AIGR == SelectedSituation).Select(p => p.PLYT).Distinct().ToList();
            //foreach (int playtype in _plyl)
            //{
            //    List<Color> colors = typeof(Colors)
            //        .GetProperties()
            //        .Where(c =>
            //            ((Color)ColorConverter.ConvertFromString(c.Name)).R > 64 &&
            //            ((Color)ColorConverter.ConvertFromString(c.Name)).R < 248 &&
            //            ((Color)ColorConverter.ConvertFromString(c.Name)).G > 0 &&
            //            ((Color)ColorConverter.ConvertFromString(c.Name)).G < 192 &&
            //            ((Color)ColorConverter.ConvertFromString(c.Name)).B > 64 &&
            //            ((Color)ColorConverter.ConvertFromString(c.Name)).B < 248)
            //        .Select(c => ((Color)ColorConverter.ConvertFromString(c.Name)))
            //        .OrderBy(c => c.ToString())
            //        .ToList();
            //    //PlayTypes.Add(new SituationVM
            //    //{
            //    //    Title = TeamPlaybook.PlayType[playtype],
            //    //    ColorBrush = new SolidColorBrush(colors[PlayTypes.Count]),
            //    //    Plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.PLYL.PLYT == playtype).ToList()
            //    //});
            //    PlayTypes.Add(new SituationVM
            //    {
            //        Title = TeamPlaybook.PlayType.Keys.Contains(playtype) ? TeamPlaybook.PlayType[playtype] : "Play Type " + playtype + " Not Found!",
            //        ColorBrush = new SolidColorBrush(colors[PlayTypes.Count]),
            //        Plays = ((TeamPlaybook)tvwPlaybook.DataContext).Plays.Where(p => p.PLYL.PLYT == playtype).ToList(),
            //        Weight = TeamPlaybook.PBAI.Where(p => p.PLYT == playtype && p.AIGR == SelectedSituation).Sum(p => p.prct),
            //        Percentage = (float)Math.Round(((double)Weight / (double)TotalSitWeight) * 100)
            //    });
            //    playCount += PlayTypes[PlayTypes.Count() - 1].Plays.Count();
            //}
            //foreach (SituationVM playtype in PlayTypes)
            //{
            //    playtype.Percentage = Math.Round(((float)playtype.Plays.Count / (float)playCount) * 100);
            //}
            //PlayTypes = PlayTypes.OrderByDescending(p => p.Percentage).ToList();
            //iclGameplanPercent.ItemsSource = PlayTypes;

            //float angle = -90, prevAngle = -90;
            //cvsGameplanPercent.Children.Clear();
            //foreach (SituationVM playtype in PlayTypes)
            //{
            //    double line1X = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
            //    double line1Y = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

            //    angle = (float)playtype.Percentage * (float)360 / 100 + prevAngle;

            //    double arcX = ((cvsGameplanPercent.Width / 2) * Math.Cos(angle * Math.PI / 180)) + (cvsGameplanPercent.Width / 2);
            //    double arcY = ((cvsGameplanPercent.Width / 2) * Math.Sin(angle * Math.PI / 180)) + (cvsGameplanPercent.Height / 2);

            //    var line1Segment = new LineSegment(new Point(line1X, line1Y), false);
            //    double arcWidth = (cvsGameplanPercent.Width / 2), arcHeight = (cvsGameplanPercent.Width / 2);
            //    bool isLargeArc = playtype.Percentage > 50;
            //    var arcSegment = new ArcSegment()
            //    {
            //        Size = new Size(arcWidth, arcHeight),
            //        Point = new Point(arcX, arcY),
            //        SweepDirection = SweepDirection.Clockwise,
            //        IsLargeArc = isLargeArc,
            //    };
            //    var line2Segment = new LineSegment(new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)), false);

            //    var pathFigure = new PathFigure(
            //        new Point((cvsGameplanPercent.Width / 2), (cvsGameplanPercent.Height / 2)),
            //        new List<PathSegment>()
            //        {
            //        line1Segment,
            //        arcSegment,
            //        line2Segment,
            //        },
            //        true);

            //    var pathFigures = new List<PathFigure>() { pathFigure, };
            //    var pathGeometry = new PathGeometry(pathFigures);
            //    var path = PlayTypes.Count == 1 ?
            //        new System.Windows.Shapes.Path()
            //        {
            //            Fill = playtype.ColorBrush,
            //            Data = new EllipseGeometry(pathFigure.StartPoint, cvsGameplanPercent.Width / 2, cvsGameplanPercent.Height / 2),
            //        } :
            //        new System.Windows.Shapes.Path()
            //        {
            //            Fill = playtype.ColorBrush,
            //            Data = pathGeometry,
            //        };
            //    cvsGameplanPercent.Children.Add(path);
            //    prevAngle = angle;


            //    // draw outlines
            //    //var outline1 = new System.Windows.Shapes.Line()
            //    //{
            //    //    X1 = (cvsGameplanPercent.Width / 2),
            //    //    Y1 = (cvsGameplanPercent.Height / 2),
            //    //    X2 = line1Segment.Point.X,
            //    //    Y2 = line1Segment.Point.Y,
            //    //    Stroke = Brushes.Black,
            //    //    StrokeThickness = 1,
            //    //};
            //    //var outline2 = new System.Windows.Shapes.Line()
            //    //{
            //    //    X1 = (cvsGameplanPercent.Width / 2),
            //    //    Y1 = (cvsGameplanPercent.Height / 2),
            //    //    X2 = arcSegment.Point.X,
            //    //    Y2 = arcSegment.Point.Y,
            //    //    Stroke = Brushes.Black,
            //    //    StrokeThickness = 1,
            //    //};

            //    //cvsGameplanPercent.Children.Add(outline1);
            //    //cvsGameplanPercent.Children.Add(outline2);
            //}
        }

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

        private void sitPlay_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Situation)sender).ToolTip = TeamPlaybook.Plays.FirstOrDefault(p => p.Situations.Contains(((Situation)sender).situation))?.ToCanvas(.333, false);
        }

        private void sitPlay_GetToolTip(object sender, ToolTipEventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void sitPlay_DumpToolTip(object sender, ToolTipEventArgs e)
        {
            Console.WriteLine(sender);
        }

        private void sitPlay_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Situation)sender).ToolTip = null;
        }

        private void tclTeamPlaybook_TabChanged(object sender, SelectionChangedEventArgs e)
        {
            //if ((string)((TabItem)tclTeamPlaybook.SelectedItem).Header == "Gameplan")
            //{
            //    //lvwPlaysBySituation.Items.Filter = SituationFilter;
            //}
        }

        private void btnSelectView(object sender, RoutedEventArgs e)
        {
            tclTeamPlaybook.SelectedItem = tclTeamPlaybook.Items.OfType<TabItem>().SingleOrDefault(n => (string)n.Header == (string)((MenuItem)sender).Header);
            if (tclTeamPlaybook.SelectedItem == null)
            {
                return;
            }
            if ((string)(((TabItem)tclTeamPlaybook.SelectedItem).Header) == "Gameplan")
            {
                lvwPlaysBySituation.Items.Filter = SituationFilter;
            }
        }

        private void lvwTeamColors_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (lvwTeamColors.SelectedValue == null) return;
            List<string> teamColors = lvwTeamColors.SelectedValue as List<string>;
            SetTeamColors(teamColors);
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