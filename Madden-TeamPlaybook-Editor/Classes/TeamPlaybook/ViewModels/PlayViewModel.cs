using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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
            PLYL = SubFormation.Formation.Playbook.PLYL.FirstOrDefault(play => play.plyl == pbpl.PLYL);
            if (PLYL == null)
            {
                PLYL = new PLYL();
                MessageBox.Show(SubFormation.Formation.PBFM.name + " - " + SubFormation.PBST.name + " - " + PBPL.name, "Missing PLYL!!!");
            }
            UpdatePlay();
            GetPlayers();

            PlayType = TeamPlaybook.PlayType.Keys.Contains(PLYL.PLYT) ? TeamPlaybook.PlayType[PLYL.PLYT] : "";

            PlayArtFilePath = "pack://siteoforigin:,,,/playart/File" + pbpl.PLYL.ToString().PadLeft(5, '0') + ".PNG";
        }

        public void UpdatePlay()
        {
            PLPD = SubFormation.Formation.Playbook.PLPD.FirstOrDefault(play => play.PLYL == PBPL.PLYL);
            PLRD = SubFormation.Formation.Playbook.PLRD.FirstOrDefault(play => play.PLYL == PBPL.PLYL);
            GetAudibles();
            GetPlayListFlags();
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
            SETL SETL = Play.SubFormation.Formation.Playbook.SETL.FirstOrDefault(set => set.setl == Play.PLYL.SETL);
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
            Situations = SubFormation.Formation.Playbook.PBAI.Where(s => s.PBPL == PBPL.pbpl).ToList();
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

        public Canvas ToCanvas(int Scale, bool PSALView)
        {
            Canvas cvsSave = new Canvas { Width = 512 * Scale, Height = 512 * Scale};
            foreach (PlayerVM player in PlayerPlayartView)
            {
                Playart playart = new Playart { Player = player, PSALView = PSALView, Scale = PSALView ? 2 : 1 * Scale, AbsolutePositioning = true };
                Canvas.SetLeft(playart, PSALView ? (SubFormation.Formation.Playbook.LOS.X + player.XY.X) : player.SETP.artx * Scale);
                Canvas.SetTop(playart, PSALView ? (450 + player.XY.Y) : player.SETP.arty * Scale);
                cvsSave.Children.Add(playart);
            }
            foreach (PlayerVM player in PlayerPlayartView)
            {
                PlayerIcon playart = new PlayerIcon { Player = player, ShowPosition = false, Scale = PSALView ? 2 : 1 * Scale, AbsolutePositioning = true };
                Canvas.SetLeft(playart, PSALView ? (SubFormation.Formation.Playbook.LOS.X + player.XY.X) : player.SETP.artx * Scale);
                Canvas.SetTop(playart, PSALView ? (450 + player.XY.Y) : player.SETP.arty * Scale);
                cvsSave.Children.Add(playart);
            }
            TransformGroup tg = new TransformGroup();
            double scale = PSALView ? .9 : 460.8 / 180;
            tg.Children.Add(new ScaleTransform(scale, scale));
            Point offset = PSALView ? new Point(533 * .05, 533 * .05) : new Point(9.0 * scale, 40.0 * scale);
            tg.Children.Add(new TranslateTransform(offset.X, offset.Y));
            cvsSave.RenderTransform = tg;
            Size size = new Size(cvsSave.Width, cvsSave.Height);
            cvsSave.Measure(size);
            cvsSave.Arrange(new Rect(size));
            cvsSave.UpdateLayout();
            //Window window = new Window
            //{
            //    Title = "PSAL Editor",
            //    Content = cvsSave,
            //    Background = Brushes.Black,
            //    SizeToContent = SizeToContent.WidthAndHeight,
            //    ResizeMode = ResizeMode.NoResize
            //};
            //window.ShowDialog();

            return cvsSave;
        }

        #endregion

        public void AddPLPD()
        {
            if (PLPD == null)
            {
                SubFormation.Formation.Playbook.PLPD.Add(new PLPD
                {
                    rec = SubFormation.Formation.Playbook.PLPD.Select(p => p.rec).Max() + 1,
                    PLYL = PLYL.plyl,
                    progressions = new List<Progression>
                    {
                        new Progression(),
                        new Progression(),
                        new Progression(),
                        new Progression(),
                        new Progression()
                    }
                });
            }
        }

        #region Audibles

        public void GetAudibles()
        {
            _isShortAudible =
            _isRunAudible =
            _isDeepAudible =
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

        private bool _isShortAudible;
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
        private bool _isRunAudible;
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
        private bool _isDeepAudible;
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
        private bool _isFakeAudible;
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

        #region Play List Flags

        public void GetPlayListFlags()
        {
            _BlockAndRelease =
            _BlockLeft =
            _BlockRight =
            _CanFlip =
            _ContainLeft =
            _ContainRight =
            _Cover2Defense =
            _DisableLockOn =
            _EndComplement =
            _IsBoosted =
            _NoWideAlign =
            _OptionStop =
            _OutsideRunStop =
            _PassLeft =
            _PassLong =
            _PassMedium =
            _PassMiddle =
            _PassRight =
            _PassShort =
            _PuntLeft =
            _PuntRight =
            _PuntSky =
            _QBScrambleStop =
            _RunLeft =
            _RunMiddle =
            _RunRight =
            _SnapToVIP =
            _StartComplement = false;

            foreach (KeyValuePair<int, string> key in PLYL.PlayListFlag.Reverse().Where(k => k.Key <= PLYL.PLF_))
            {
                this.GetType()?.GetProperty(key.Value)?.SetValue(this, true);
                PLYL.PLF_ = PLYL.PLF_ - key.Key;
            }
        }

        private bool _BlockAndRelease;
        public bool BlockAndRelease
        {
            get { return _BlockAndRelease; }
            set
            {
                if (_BlockAndRelease == value)
                    return;
                _BlockAndRelease = value;
                this.PLYL.PLF_ += _BlockAndRelease ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "BlockAndRelease", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "BlockAndRelease", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("BlockAndRelease");
            }
        }
        private bool _BlockLeft;
        public bool BlockLeft
        {
            get { return _BlockLeft; }
            set
            {
                if (_BlockLeft == value)
                    return;
                _BlockLeft = value;
                this.PLYL.PLF_ += _BlockLeft ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "BlockLeft", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "BlockLeft", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("BlockLeft");
            }
        }
        private bool _BlockRight;
        public bool BlockRight
        {
            get { return _BlockRight; }
            set
            {
                if (_BlockRight == value)
                    return;
                _BlockRight = value;
                this.PLYL.PLF_ += _BlockRight ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "BlockRight", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "BlockRight", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("BlockRight");
            }
        }
        private bool _CanFlip;
        public bool CanFlip
        {
            get { return _CanFlip; }
            set
            {
                if (_CanFlip == value)
                    return;
                _CanFlip = value;
                this.PLYL.PLF_ += _CanFlip ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "CanFlip", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "CanFlip", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("CanFlip");
            }
        }
        private bool _ContainLeft;
        public bool ContainLeft
        {
            get { return _ContainLeft; }
            set
            {
                if (_ContainLeft == value)
                    return;
                _ContainLeft = value;
                this.PLYL.PLF_ += _ContainLeft ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "ContainLeft", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "ContainLeft", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("ContainLeft");
            }
        }
        private bool _ContainRight;
        public bool ContainRight
        {
            get { return _ContainRight; }
            set
            {
                if (_ContainRight == value)
                    return;
                _ContainRight = value;
                this.PLYL.PLF_ += _ContainRight ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "ContainRight", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "ContainRight", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("ContainRight");
            }
        }
        private bool _Cover2Defense;
        public bool Cover2Defense
        {
            get { return _Cover2Defense; }
            set
            {
                if (_Cover2Defense == value)
                    return;
                _Cover2Defense = value;
                this.PLYL.PLF_ += _Cover2Defense ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Cover2Defense", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Cover2Defense", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("Cover2Defense");
            }
        }
        private bool _CoverageShell;
        public bool CoverageShell
        {
            get { return _CoverageShell; }
            set
            {
                if (_CoverageShell == value)
                    return;
                _CoverageShell = value;
                this.PLYL.PLF_ += _CoverageShell ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "CoverageShell", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "CoverageShell", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("CoverageShell");
            }
        }
        private bool _DisableLockOn;
        public bool DisableLockOn
        {
            get { return _DisableLockOn; }
            set
            {
                if (_DisableLockOn == value)
                    return;
                _DisableLockOn = value;
                this.PLYL.PLF_ += _DisableLockOn ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "DisableLockOn", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "DisableLockOn", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("DisableLockOn");
            }
        }
        private bool _EndComplement;
        public bool EndComplement
        {
            get { return _EndComplement; }
            set
            {
                if (_EndComplement == value)
                    return;
                _EndComplement = value;
                this.PLYL.PLF_ += _EndComplement ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "EndComplement", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "EndComplement", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("EndComplement");
            }
        }
        private bool _IsBoosted;
        public bool IsBoosted
        {
            get { return _IsBoosted; }
            set
            {
                if (_IsBoosted == value)
                    return;
                _IsBoosted = value;
                this.PLYL.PLF_ += _IsBoosted ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "IsBoosted", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "IsBoosted", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("IsBoosted");
            }
        }
        private bool _NoPlaymaker;
        public bool NoPlaymaker
        {
            get { return _NoPlaymaker; }
            set
            {
                if (_NoPlaymaker == value)
                    return;
                _NoPlaymaker = value;
                this.PLYL.PLF_ += _NoPlaymaker ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "NoPlaymaker", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "NoPlaymaker", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("NoPlaymaker");
            }
        }
        private bool _NoWideAlign;
        public bool NoWideAlign
        {
            get { return _NoWideAlign; }
            set
            {
                if (_NoWideAlign == value)
                    return;
                _NoWideAlign = value;
                this.PLYL.PLF_ += _NoWideAlign ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "NoWideAlign", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "NoWideAlign", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("NoWideAlign");
            }
        }
        private bool _OptionStop;
        public bool OptionStop
        {
            get { return _OptionStop; }
            set
            {
                if (_OptionStop == value)
                    return;
                _OptionStop = value;
                this.PLYL.PLF_ += _OptionStop ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "OptionStop", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "OptionStop", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("OptionStop");
            }
        }
        private bool _OutsideRunStop;
        public bool OutsideRunStop
        {
            get { return _OutsideRunStop; }
            set
            {
                if (_OutsideRunStop == value)
                    return;
                _OutsideRunStop = value;
                this.PLYL.PLF_ += _OutsideRunStop ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "OutsideRunStop", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "OutsideRunStop", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("OutsideRunStop");
            }
        }
        private bool _PassLeft;
        public bool PassLeft
        {
            get { return _PassLeft; }
            set
            {
                if (_PassLeft == value)
                    return;
                _PassLeft = value;
                this.PLYL.PLF_ += _PassLeft ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassLeft", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassLeft", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassLeft");
            }
        }
        private bool _PassLong;
        public bool PassLong
        {
            get { return _PassLong; }
            set
            {
                if (_PassLong == value)
                    return;
                _PassLong = value;
                this.PLYL.PLF_ += _PassLong ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassLong", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassLong", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassLong");
            }
        }
        private bool _PassMedium;
        public bool PassMedium
        {
            get { return _PassMedium; }
            set
            {
                if (_PassMedium == value)
                    return;
                _PassMedium = value;
                this.PLYL.PLF_ += _PassMedium ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassMedium", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassMedium", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassMedium");
            }
        }
        private bool _PassMiddle;
        public bool PassMiddle
        {
            get { return _PassMiddle; }
            set
            {
                if (_PassMiddle == value)
                    return;
                _PassMiddle = value;
                this.PLYL.PLF_ += _PassMiddle ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassMiddle", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassMiddle", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassMiddle");
            }
        }
        private bool _PassRight;
        public bool PassRight
        {
            get { return _PassRight; }
            set
            {
                if (_PassRight == value)
                    return;
                _PassRight = value;
                this.PLYL.PLF_ += _PassRight ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassRight", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassRight", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassRight");
            }
        }
        private bool _PassShort;
        public bool PassShort
        {
            get { return _PassShort; }
            set
            {
                if (_PassShort == value)
                    return;
                _PassShort = value;
                this.PLYL.PLF_ += _PassShort ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassShort", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PassShort", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassShort");
            }
        }
        private bool _PuntLeft;
        public bool PuntLeft
        {
            get { return _PuntLeft; }
            set
            {
                if (_PuntLeft == value)
                    return;
                _PuntLeft = value;
                this.PLYL.PLF_ += _PuntLeft ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PuntLeft", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PuntLeft", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PuntLeft");
            }
        }
        private bool _PuntRight;
        public bool PuntRight
        {
            get { return _PuntRight; }
            set
            {
                if (_PuntRight == value)
                    return;
                _PuntRight = value;
                this.PLYL.PLF_ += _PuntRight ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PuntRight", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PuntRight", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PuntRight");
            }
        }
        private bool _PuntSky;
        public bool PuntSky
        {
            get { return _PuntSky; }
            set
            {
                if (_PuntSky == value)
                    return;
                _PuntSky = value;
                this.PLYL.PLF_ += _PuntSky ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PuntSky", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "PuntSky", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PuntSky");
            }
        }
        private bool _QBScrambleStop;
        public bool QBScrambleStop
        {
            get { return _QBScrambleStop; }
            set
            {
                if (_QBScrambleStop == value)
                    return;
                _QBScrambleStop = value;
                this.PLYL.PLF_ += _QBScrambleStop ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "QBScrambleStop", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "QBScrambleStop", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("QBScrambleStop");
            }
        }
        private bool _RunLeft;
        public bool RunLeft
        {
            get { return _RunLeft; }
            set
            {
                if (_RunLeft == value)
                    return;
                _RunLeft = value;
                this.PLYL.PLF_ += _RunLeft ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "RunLeft", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "RunLeft", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("RunLeft");
            }
        }
        private bool _RunMiddle;
        public bool RunMiddle
        {
            get { return _RunMiddle; }
            set
            {
                if (_RunMiddle == value)
                    return;
                _RunMiddle = value;
                this.PLYL.PLF_ += _RunMiddle ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "RunMiddle", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "RunMiddle", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("RunMiddle");
            }
        }
        private bool _RunRight;
        public bool RunRight
        {
            get { return _RunRight; }
            set
            {
                if (_RunRight == value)
                    return;
                _RunRight = value;
                this.PLYL.PLF_ += _RunRight ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "RunRight", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "RunRight", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("RunRight");
            }
        }
        private bool _SnapToVIP;
        public bool SnapToVIP
        {
            get { return _SnapToVIP; }
            set
            {
                if (_SnapToVIP == value)
                    return;
                _SnapToVIP = value;
                this.PLYL.PLF_ += _SnapToVIP ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "SnapToVIP", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "SnapToVIP", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("SnapToVIP");
            }
        }
        private bool _StartComplement;
        public bool StartComplement
        {
            get { return _StartComplement; }
            set
            {
                if (_StartComplement == value)
                    return;
                _StartComplement = value;
                this.PLYL.PLF_ += _StartComplement ?
                    PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "StartComplement", StringComparison.OrdinalIgnoreCase)).Key :
                    -1*PLYL.PlayListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "StartComplement", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("StartComplement");
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