using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Madden.Team;
using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.User_Controls;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class PlayVM : INotifyPropertyChanged
    {
        #region To String

        public override string ToString()
        {
            string PlayVM_string =
                PBPL.name + ":\t" + PlayType + "\n" +
                PBPL.ToString() + "\n" +
                PLYL.ToString() + "\n";
            if (PLPD != null)
            {
                PlayVM_string += "PLPD:" + "\n" + "\t" + PLPD.ToString() + "\n";
            }
            if (PLRD != null)
            {
                PlayVM_string += "PLRD:" + "\n" + "\t" + PLRD.ToString() + "\n";
            }
            if (SRFT != null)
            {
                foreach (SRFT front in SRFT) PlayVM_string += front.ToString() + "\n";
            }
            if (Situations != null)
            {
                foreach (PBAI situation in Situations)
                {
                    try { PlayVM_string +=
                            SubFormation.Formation.Playbook.Type == "Offense" ?
                            TeamPlaybook.SituationOff[situation.AIGR] + "\n" :
                            TeamPlaybook.SituationDef[situation.AIGR] + "\n"; }
                    catch { PlayType = ""; }
                }
            }

            foreach (PLYS assignment in PLYS) PlayVM_string += assignment.ToString() + "\n";

            foreach (PlayerVM player in Players)
            {
                PlayVM_string += player.ToString() + "\n";
            }

            return
                PlayVM_string;
        }

        #endregion

        #region Instantiation

        public SubFormationVM SubFormation { get; set; }

        private PBPL _PBPL { get; set; }
        public PBPL PBPL
        {
            get { return _PBPL; }
            set
            {
                if (_PBPL == value)
                    return;
                _PBPL = value;
                OnPropertyChanged("PBPL");
            }
        }
        private PLYL _PLYL { get; set; }
        public PLYL PLYL
        {
            get { return _PLYL; }
            set
            {
                if (_PLYL == value)
                    return;
                _PLYL = value;
                OnPropertyChanged("PLYL");
            }
        }
        public PLPD PLPD { get; set; }
        public PLRD PLRD { get; set; }
        public List<PBAI> Situations { get; set; }
        public List<PBAU> Audibles { get; set; }
        public List<PBCC> PBCC { get; set; }
        public List<PLCM> PLCM { get; set; }
        public List<PPCT> PPCT { get; set; }
        public List<SDEF> SDEF { get; set; }
        public List<SRFT> SRFT { get; set; }
        public List<PLYS> PLYS { get; set; }

        private ObservableCollection<PlayerVM> _Players;
        public ObservableCollection<PlayerVM> Players
        {
            get { return _Players; }
            set
            {
                if (_Players == value)
                    return;
                _Players = value;
                OnPropertyChanged("Players");
            }
        }
        [field: NonSerialized()]
        public ICollectionView PlayerPlayartView { get; set; }

        private bool _isShortAudible;
        private bool _isRunAudible;
        private bool _isDeepAudible;
        private bool _isFakeAudible;

        public static readonly Dictionary<int, string> Situation = new Dictionary<int, string>
        {
            {2,"Quick Pass"},
            {4,"Run"},
            {8,"Deep Pass"},
            {16,"Play Action"}
        };

        public string PlayType { get; set; }
        public string PlayArtFilePath { get; set; }
        public double AverageRouteDepth { get; set; }

        private bool _isExpanded;
        private bool _isVisible;
        private bool _isSelected;

        public PlayVM()
        {

        }

        public PlayVM(PBPL pbpl, SubFormationVM _SubFormation = null, SituationVM _Gameplan = null)
        {
            IsVisible = false;
            IsExpanded = false;
            PBPL = pbpl;
            SubFormation = _SubFormation;
            PLYL = SubFormation.Formation.Playbook.PLYL.Where(play => play.plyl == pbpl.PLYL).FirstOrDefault();
            if (PLYL == null)
            {
                PLYL = new PLYL();
                MessageBox.Show(SubFormation.Formation.PBFM.name + " - " + SubFormation.PBST.name + " - " + PBPL.name, "Missing PLYL!!!");
            }
            UpdatePlay();
            GetPlayers();

            try 
            { 
                PlayType = TeamPlaybook.PlayType[PLYL.PLYT]; 
            }
            catch 
            { 
                PlayType = "";
            }

            PlayArtFilePath = "pack://siteoforigin:,,,/playart/File" + pbpl.PLYL.ToString().PadLeft(5, '0') + ".PNG";
        }

        public void UpdatePlay()
        {
            PLPD = SubFormation.Formation.Playbook.PLPD.Where(play => play.PLYL == PBPL.PLYL).FirstOrDefault();
            PLRD = SubFormation.Formation.Playbook.PLRD.Where(play => play.PLYL == PBPL.PLYL).FirstOrDefault();
            GetAudibles();
            GetSituations();
            GetPBAU();
            GetPBCC();
            GetPLCM();
            GetPPCT();
            GetSDEF();
            GetSRFT();
        }

        public void AddHiddenSubFormation(PlayVM Play, TeamPlaybook Playbook)
        {
            SETL SETL = Play.SubFormation.Formation.Playbook.SETL.Where(set => set.setl == Play.PLYL.SETL).FirstOrDefault();
            Playbook.SETL.Add(SETL);

            List<SETP> SETP = Play.SubFormation.Formation.Playbook.SETP.Where(player => player.SETL == Play.PLYL.SETL).Cast<SETP>().ToList();
            Playbook.SETP.AddRange(SETP);

            List<SPKF> SPKF = Play.SubFormation.Formation.Playbook.SPKF.Where(package => package.SETL == Play.PLYL.SETL).Cast<SPKF>().ToList();
            List<SPKG> SPKG = new List<SPKG>();
            foreach (SPKF spfk in SPKF)
            {
                SPKG.AddRange(Play.SubFormation.Formation.Playbook.SPKG.Where(sub => sub.SPF_ == spfk.SPF_).Cast<SPKG>().ToList());
            }
            Playbook.SPKF.AddRange(SPKF);
            Playbook.SPKG.AddRange(SPKG);

            List<SGFM> SGFM = Play.SubFormation.Formation.Playbook.SGFM.Where(alignment => alignment.SETL == Play.PLYL.SETL).Cast<SGFM>().ToList();
            List<SETG> SETG = new List<SETG>();
            foreach (SGFM sgfm in SGFM)
            {
                SETG.AddRange(Play.SubFormation.Formation.Playbook.SETG.Where(alignment => alignment.SGF_ == sgfm.SGF_).Cast<SETG>().ToList());
            }
            Playbook.SGFM.AddRange(SGFM);
            Playbook.SETG.AddRange(SETG);
        }

        public void GetPlayers()
        {
            Players = new ObservableCollection<PlayerVM>();
            PLYS = new List<PLYS>();
            foreach (PLYS player in SubFormation.Formation.Playbook.PLYS.Where(play => play.PLYL == PBPL.PLYL))
            {
                PLYS.Add(player);
                Players.Add(new PlayerVM(player, this));
            }
            GetAverageRouteDepth();
            GetPlayerPlayartViewList();
        }

        public void GetAverageRouteDepth()
        {
            List<PlayerVM> _players = Players.Where(p => p.progression != null).ToList();
            AverageRouteDepth = _players.Count > 0 ?
                Math.Round(_players.Where(p => p.progression.per > 1)
                        .Select(p => p.RouteDepthPLPDnormalized)
                        .Sum()) / 10 :
                0;
        }

        public void UpdatePlayers()
        {
            foreach (PlayerVM player in Players)
            {
                player.UpdatePlayer();
            }
        }

        public void UpdatePLYL()
        {
            if (PBPL != null) PBPL.PLYL = PLYL.plyl;
            if (PLPD != null) PLPD.PLYL = PLYL.plyl;
            if (PLRD != null) PLRD.PLYL = PLYL.plyl;
            if (PLYS != null)
            {
                foreach (PLYS play in PLYS)
                {
                    play.PLYL = PLYL.plyl;
                }
            }
            if (PLCM != null)
            {
                foreach (PLCM play in PLCM)
                {
                    play.PLYL = PLYL.plyl;
                }
            }
            if (PPCT != null)
            {
                foreach (PPCT play in PPCT)
                {
                    play.plyl = PLYL.plyl;
                }
            }
            if (SRFT != null)
            {
                foreach (SRFT play in SRFT)
                {
                    play.PLYL = PLYL.plyl;
                }
            }
        }

        public void GetPlayerPlayartViewList()
        {
            PlayerPlayartView = CollectionViewSource.GetDefaultView(Players);
            PlayerPlayartView.SortDescriptions.Add(new SortDescription("artlColor.Order", ListSortDirection.Ascending));
        }

        public void GetSRFT()
        {
            if (SRFT == null)
            {
                SRFT = new List<SRFT>();
            }
            else
            {
                SRFT.Clear();
            }
            foreach (SRFT srft in SubFormation.Formation.Playbook.SRFT.Where(front => front.PLYL == PBPL.PLYL))
            {
                SRFT.Add(srft);
            }
            SRFT.OrderBy(front => front.PLYR);
        }

        public void GetSDEF()
        {
            if (SDEF == null)
            {
                SDEF = new List<SDEF>();
            }
            else
            {
                SDEF.Clear();
            }
            foreach (SDEF sdef in SubFormation.Formation.Playbook.SDEF.Where(def => def.PLYL == PBPL.PLYL))
            {
                SDEF.Add(sdef);
            }
        }

        public void GetSituations()
        {
            if (Situations == null)
            {
                Situations = new List<PBAI>();
            }
            else
            {
                Situations.Clear();
            }
            foreach (PBAI pbai in SubFormation.Formation.Playbook.PBAI.Where(situation => situation.PBPL == PBPL.pbpl))
            {
                Situations.Add(pbai);
            }
        }

        public void GetPBAU()
        {
            if (Audibles == null)
            {
                Audibles = new List<PBAU>();
            }
            else
            {
                Audibles.Clear();
            }
            foreach (PBAU pbau in SubFormation.Formation.Playbook.PBAU.Where(audible => audible.PBPL == PBPL.pbpl))
            {
                Audibles.Add(pbau);
            }
        }

        public void GetPBCC()
        {
            if (PBCC == null)
            {
                PBCC = new List<PBCC>();
            }
            else
            {
                PBCC.Clear();
            }
            foreach (PBCC pbcc in SubFormation.Formation.Playbook.PBCC.Where(play => play.PBPL == PBPL.pbpl))
            {
                PBCC.Add(pbcc);
            }
        }

        public void GetPLCM()
        {
            if (PLCM == null)
            {
                PLCM = new List<PLCM>();
            }
            else
            {
                PLCM.Clear();
            }
            foreach (PLCM plcm in SubFormation.Formation.Playbook.PLCM.Where(plcm => plcm.PLYL == PBPL.PLYL))
            {
                PLCM.Add(plcm);
            }
        }

        public void GetPPCT()
        {
            if (PPCT == null)
            {
                PPCT = new List<PPCT>();
            }
            else
            {
                PPCT.Clear();
            }
            foreach (PPCT ppct in SubFormation.Formation.Playbook.PPCT.Where(ppct => ppct.plyl == PBPL.PLYL))
            {
                PPCT.Add(ppct);
            }
        } 

        public Canvas ToPlayArtCanvas(int Scale)
        {
            Canvas cvsSave = new Canvas { Height = 750 * Scale, Width = 533 * Scale };
            foreach (PlayerVM player in PlayerPlayartView)
            {
                Playart playart = new Playart { Player = player, PSALView = true, Scale = 2 * Scale, AbsolutePositioning = true };
                Canvas.SetLeft(playart, (SubFormation.Formation.Playbook.LOS.X + player.XY.X) * Scale);
                Canvas.SetTop(playart, (SubFormation.Formation.Playbook.LOS.Y + player.XY.Y) * Scale);
                cvsSave.Children.Add(playart);
            }
            foreach (PlayerVM player in PlayerPlayartView)
            {
                PlayerIcon playart = new PlayerIcon { Player = player, ShowPosition = false, Scale = 2 * Scale, AbsolutePositioning = true };
                Canvas.SetLeft(playart, (SubFormation.Formation.Playbook.LOS.X + player.XY.X) * Scale);
                Canvas.SetTop(playart, (SubFormation.Formation.Playbook.LOS.Y + player.XY.Y) * Scale);
                cvsSave.Children.Add(playart);
            }

            var size = new Size(533 * Scale, 750 * Scale);
            cvsSave.Measure(size);
            cvsSave.Arrange(new Rect(size));
            cvsSave.UpdateLayout();

            return cvsSave;
        }

        public Canvas ToARTLCanvas(int Scale)
        {
            Canvas cvsSave = new Canvas { Height = 120 * Scale, Width = 180 * Scale };
            foreach (PlayerVM player in PlayerPlayartView)
            {
                Playart playart = new Playart { Player = player, PSALView = false, Scale = 1 * Scale, AbsolutePositioning = true };
                Canvas.SetLeft(playart, player.SETP.artx * Scale);
                Canvas.SetTop(playart, player.SETP.arty * Scale);
                cvsSave.Children.Add(playart);
            }
            foreach (PlayerVM player in PlayerPlayartView)
            {
                PlayerIcon playart = new PlayerIcon { Player = player, ShowPosition = false, Scale = 1 * Scale, AbsolutePositioning = true };
                Canvas.SetLeft(playart, player.SETP.artx * Scale);
                Canvas.SetTop(playart, player.SETP.arty * Scale);
                cvsSave.Children.Add(playart);
            }

            var size = new Size(180 * Scale, 120 * Scale);
            cvsSave.Measure(size);
            cvsSave.Arrange(new Rect(size));
            cvsSave.UpdateLayout();

            return cvsSave;
        }

        #endregion

        #region Audibles

        public void GetAudibles()
        {
            _isShortAudible = false;
            _isRunAudible = false;
            _isDeepAudible = false;
            _isFakeAudible = false;
            int _flag = PBPL.Flag;

            if ((_flag - 16) >= 0)
            {
                _isFakeAudible = true;
                _flag = _flag - 16;
            }
            if ((_flag - 8) >= 0)
            {
                _isDeepAudible = true;
                _flag = _flag - 8;
            }
            if ((_flag - 4) >= 0)
            {
                _isRunAudible = true;
                _flag = _flag - 4;
            }
            if ((_flag - 2) >= 0)
            {
                _isShortAudible = true;
                _flag = _flag - 2;
            }
        }

        public bool isShortAudible
        {
            get { return _isShortAudible; }
            set
            {
                if (value == false)
                {
                    _isShortAudible = false;
                    PBPL.Flag -= 2;
                    this.OnPropertyChanged("isShortAudible");
                }
                if (value == true)
                {
                    foreach (PlayVM play in SubFormation.Plays.Where(_play => _play.isShortAudible == true))
                    {
                        play.isShortAudible = false;
                    }
                    _isShortAudible = true;
                    PBPL.Flag += 2;
                    this.OnPropertyChanged("isShortAudible");
                }
            }
        }

        public bool isRunAudible
        {
            get { return _isRunAudible; }
            set
            {
                if (value == false)
                {
                    _isRunAudible = false;
                    PBPL.Flag -= 4;
                    this.OnPropertyChanged("isRunAudible");
                }
                if (value == true)
                {
                    foreach (PlayVM play in SubFormation.Plays.Where(_play => _play.isRunAudible == true))
                    {
                        play.isRunAudible = false;
                    }
                    _isRunAudible = true;
                    PBPL.Flag += 4;
                    this.OnPropertyChanged("isRunAudible");
                }
            }
        }

        public bool isDeepAudible
        {
            get { return _isDeepAudible; }
            set
            {
                if (value == false)
                {
                    _isDeepAudible = false;
                    PBPL.Flag -= 8;
                    this.OnPropertyChanged("isDeepAudible");
                }
                if (value == true)
                {
                    foreach (PlayVM play in SubFormation.Plays.Where(_play => _play.isDeepAudible == true))
                    {
                        play.isDeepAudible = false;
                    }
                    _isDeepAudible = true;
                    PBPL.Flag += 8;
                    this.OnPropertyChanged("isDeepAudible");
                }
            }
        }

        public bool isFakeAudible
        {
            get { return _isFakeAudible; }
            set
            {
                if (value == false)
                {
                    _isFakeAudible = false;
                    PBPL.Flag -= 16;
                    this.OnPropertyChanged("isFakeAudible");
                }
                if (value == true)
                {
                    foreach (PlayVM play in SubFormation.Plays.Where(_play => _play.isFakeAudible == true))
                    {
                        play.isFakeAudible = false;
                    }
                    _isFakeAudible = true;
                    PBPL.Flag += 16;
                    this.OnPropertyChanged("isFakeAudible");
                }
            }
        }

        #endregion

        #region IsExpanded

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

        #endregion // IsExpanded

        #region IsVisible

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    this.OnPropertyChanged("IsVisible");
                }
            }
        }

        #endregion // IsVisible

        #region IsSelected

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region INotifyPropertyChanged Members

        [field: NonSerializedAttribute()]
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged Members
    }
}