using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using Madden.TeamPlaybook;
using TDBAccess;

namespace Madden20CustomPlaybookEditor.ViewModels
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
                foreach (Madden.TeamPlaybook.SRFT front in SRFT) PlayVM_string += front.ToString() + "\n";
            }

            foreach (Madden.TeamPlaybook.PLYS assignment in PLYS) PlayVM_string += assignment.ToString() + "\n";

            foreach (PlayerVM player in Players)
            {
                PlayVM_string += player.ToString() + "\n";
            }

            return
                PlayVM_string;
        }

        #endregion

        #region Instantiation

        public Madden.TeamPlaybook.PBPL PBPL { get; set; }
        public Madden.TeamPlaybook.PLYL PLYL { get; set; }

        public SubFormationVM SubFormation { get; set; }

        public Madden.TeamPlaybook.PLPD PLPD { get; set; }
        public Madden.TeamPlaybook.PLRD PLRD { get; set; }
        public List<Madden.TeamPlaybook.PLCM> PLCM { get; set; }
        public List<Madden.TeamPlaybook.PPCT> PPCT { get; set; }
        public List<Madden.TeamPlaybook.SDEF> SDEF { get; set; }
        public List<Madden.TeamPlaybook.SRFT> SRFT { get; set; }
        public List<Madden.TeamPlaybook.PLYS> PLYS { get; set; }

        public ObservableCollection<PlayerVM> Players { get; set; }
        [field: NonSerializedAttribute()] 
        public ICollectionView PlayerPlayartView { get; set; }

        private bool _isShortAudible { get; set; }
        private bool _isRunAudible { get; set; }
        private bool _isDeepAudible { get; set; }
        private bool _isFakeAudible { get; set; }

        public string PlayType { get; set; }
        public string PlayArtFilePath { get; set; }

        private bool _isExpanded { get; set; }
        private bool _isVisible { get; set; }
        private bool _isSelected { get; set; }

        public PlayVM(Madden.CustomPlaybook.PBPL pbpl, SubFormationVM _SubFormation = null)
        {
            IsVisible = false;
            IsExpanded = false;
            PBPL = new Madden.TeamPlaybook.PBPL
            {
                rec = pbpl.rec,
                COMF = pbpl.COMF,
                Flag = 0,
                name = pbpl.name,
                ord_ = 0,
                pbpl = 0,
                PBST = 0,
                PKGE = 0,
                PLYL = pbpl.PLYL
            };
            SubFormation = _SubFormation;
            Madden.CustomPlaybook.PIDX _PIDX = SubFormation.Formation.Playbook.PIDX.Where(play => play.PLYL == pbpl.PLYL).FirstOrDefault();
            PLYL = new PLYL
            {
                rec = _PIDX.rec,
                motn = _PIDX.motn,
                name = _PIDX.name,
                phlp = 0,
                PLF_ = _PIDX.PLF_,
                plyl = _PIDX.PLF_,
                PLYT = _PIDX.PLYT,
                risk = _PIDX.risk,
                SETL = _PIDX.SETL,
                SITT = _PIDX.SITT,
                SRMM = _PIDX.SRMM,
                vpos = _PIDX.vpos
            };
            if (PLYL == null)
            {
                PLYL = new PLYL();
                MessageBox.Show(SubFormation.Formation.FORM.name + " - " + SubFormation.SETL.name + " - " + PBPL.name, "Missing PLYL!!!");
            }
            Madden.CustomPlaybook.PLPD _PLPD = SubFormation.Formation.Playbook.PLPD.Where(play => play.PLYL == pbpl.PLYL).FirstOrDefault();
            if (_PLPD != null)
            {
                PLPD = new Madden.TeamPlaybook.PLPD
                {
                    rec = _PLPD.rec,
                    PLYL = _PLPD.PLYL
                };

                PLPD.progressions = new List<Progression>();
                PLPD.progressions.Add(new Progression
                {
                    com = _PLPD.com1,
                    con = _PLPD.con1,
                    per = _PLPD.per1,
                    rcv = _PLPD.rcv1,
                    icx = 0,
                    icy = 0
                });
                PLPD.progressions.Add(new Progression
                {
                    com = _PLPD.com2,
                    con = _PLPD.con2,
                    per = _PLPD.per2,
                    rcv = _PLPD.rcv2,
                    icx = 0,
                    icy = 0
                });
                PLPD.progressions.Add(new Progression
                {
                    com = _PLPD.com3,
                    con = _PLPD.con3,
                    per = _PLPD.per3,
                    rcv = _PLPD.rcv3,
                    icx = 0,
                    icy = 0
                });
                PLPD.progressions.Add(new Progression
                {
                    com = _PLPD.com4,
                    con = _PLPD.con4,
                    per = _PLPD.per4,
                    rcv = _PLPD.rcv4,
                    icx = 0,
                    icy = 0
                });
                PLPD.progressions.Add(new Progression
                {
                    com = _PLPD.com5,
                    con = _PLPD.con5,
                    per = _PLPD.per5,
                    rcv = _PLPD.rcv5,
                    icx = 0,
                    icy = 0
                });
            }
            else
            {
                PLPD = new Madden.TeamPlaybook.PLPD();
            }
            Madden.CustomPlaybook.PLRD _PLRD = SubFormation.Formation.Playbook.PLRD.Where(play => play.PLYL == pbpl.PLYL).FirstOrDefault();
            if (_PLRD != null)
            {
                PLRD = new Madden.TeamPlaybook.PLRD
                {
                    rec = _PLRD.rec,
                    hole = _PLRD.hole,
                    PLYL = _PLRD.PLYL
                };
            }
            else
            {
                PLRD = new Madden.TeamPlaybook.PLRD();
            }
            GetPLCM();
            GetPPCT();
            GetSDEF();
            GetSRFT();
            GetPlayers();

            try { PlayType = CustomPlaybook.PlayType[PLYL.PLYT]; }
            catch { PlayType = ""; }

            PlayArtFilePath = "pack://siteoforigin:,,,/playart/File" + pbpl.PLYL.ToString().PadLeft(5, '0') + ".PNG";
        }

        public void GetPlayers()
        {
            Players = new ObservableCollection<PlayerVM>();
            PLYS = new List<Madden.TeamPlaybook.PLYS>();
            foreach (Madden.CustomPlaybook.PLYS player in SubFormation.Formation.Playbook.PLYS.Where(play => play.PLYL == PBPL.PLYL))
            {
                PLYS.Add(new Madden.TeamPlaybook.PLYS
                {
                    rec = player.rec,
                    ARTL = player.ARTL,
                    PLRR = player.PLRR,
                    PLYL = player.PLYL,
                    poso = player.poso,
                    PSAL = player.PSAL
                });
                Players.Add(new PlayerVM(player, this));
            }
            PlayerPlayartView = CollectionViewSource.GetDefaultView(Players);
            PlayerPlayartView.SortDescriptions.Add(new SortDescription("artlColor.Order", ListSortDirection.Ascending));
        }

        public void GetSRFT()
        {
            if (SRFT == null)
            {
                SRFT = new List<Madden.TeamPlaybook.SRFT>();
            }
            else
            {
                SRFT.Clear();
            }
            foreach (Madden.CustomPlaybook.SRFT srft in SubFormation.Formation.Playbook.SRFT.Where(front => front.PLYL == PBPL.PLYL))
            {
                SRFT.Add(new Madden.TeamPlaybook.SRFT
                {
                    rec = srft.rec,
                    ASSS = srft.ASSS,
                    ASSW = srft.ASSW,
                    GAPS = srft.GAPS,
                    GAPW = srft.GAPW,
                    STAN = srft.STAN,
                    PLYL = srft.PLYL,
                    PLYR = srft.PLYR,
                    PRIS = srft.PRIS,
                    PRIW = srft.PRIW,
                    SIDE = srft.SIDE,
                    TECH = srft.TECH,
                    YOFF = srft.YOFF
                });
            }
            SRFT = SRFT.OrderBy(front => front.PLYR).ToList();
        }

        public void GetSDEF()
        {
            if (SDEF == null)
            {
                SDEF = new List<Madden.TeamPlaybook.SDEF>();
            }
            else
            {
                SDEF.Clear();
            }
            foreach (Madden.CustomPlaybook.SDEF sdef in SubFormation.Formation.Playbook.SDEF.Where(def => def.PLYL == PBPL.PLYL))
            {
                SDEF.Add(new Madden.TeamPlaybook.SDEF
                {
                    rec = sdef.rec,
                    STRP = sdef.STRP,
                    STRR = sdef.STRR,
                    ATCA = sdef.ATCA,
                    DFLP = sdef.DFLP,
                    PLYL = sdef.PLYL
                });
            }
        }

        public void GetPLCM()
        {
            if (PLCM == null)
            {
                PLCM = new List<Madden.TeamPlaybook.PLCM>();
            }
            else
            {
                PLCM.Clear();
            }
            foreach (Madden.CustomPlaybook.PLCM plcm in SubFormation.Formation.Playbook.PLCM.Where(plcm => plcm.PLYL == PBPL.PLYL))
            {
                PLCM.Add(new Madden.TeamPlaybook.PLCM
                {
                    rec = plcm.rec,
                    dir1 = plcm.dir1,
                    dir2 = plcm.dir2,
                    dir3 = plcm.dir3,
                    dir4 = plcm.dir4,
                    dir5 = plcm.dir5,
                    per1 = plcm.per1,
                    per2 = plcm.per2,
                    per3 = plcm.per3,
                    per4 = plcm.per4,
                    per5 = plcm.per5,
                    ply1 = plcm.ply1,
                    ply2 = plcm.ply2,
                    ply3 = plcm.ply3,
                    ply4 = plcm.ply4,
                    ply5 = plcm.ply5,
                    PLYL = plcm.PLYL
                });
            }
        }

        public void GetPPCT()
        {
            if (PPCT == null)
            {
                PPCT = new List<Madden.TeamPlaybook.PPCT>();
            }
            else
            {
                PPCT.Clear();
            }
            foreach (Madden.CustomPlaybook.PPCT ppct in SubFormation.Formation.Playbook.PPCT.Where(ppct => ppct.plyl == PBPL.PLYL))
            {
                PPCT.Add(new Madden.TeamPlaybook.PPCT
                {
                    rec = ppct.rec,
                    conp = ppct.conp,
                    plyl = ppct.plyl,
                    recr = ppct.recr
                });
            }
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