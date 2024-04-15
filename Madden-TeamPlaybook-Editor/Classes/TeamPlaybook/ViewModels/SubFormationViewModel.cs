using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.User_Controls;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class SubFormationVM : INotifyPropertyChanged
    {
        #region Package

        [Serializable]
        public class Package : INotifyPropertyChanged
        {
            public SPKF SPKF { get; set; }
            public List<SPKG> SPKG { get; set; }

            public Package(SPKF _SPKF, List<SPKG> _SPKG)
            {
                SPKF = _SPKF;
                SPKG = _SPKG;
            }

            public override string ToString()
            {
                string subs = SPKF.name + "\n";
                foreach (SPKG sub in SPKG) subs += "    " + sub.ToString() + "\n";
                if (subs.Length >= 2)
                {
                    subs.Substring(subs.Length - 2, 2);
                }
                return
                    subs;
            }

            #region INotifyPropertyChanged Members

            [field: NonSerializedAttribute()]
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion // INotifyPropertyChanged Members
        }

        #endregion

        #region Alignment

        [Serializable]
        public class Alignment : INotifyPropertyChanged
        {
            public static readonly Dictionary<string, string> Motions = new Dictionary<string, string>
            {
                {"Norm", "Normal"},
                {"M1ri", "Motion 1 Right"},
                {"M1le", "Motion 1 Left"},
                {"M2ri", "Motion 2 Right"},
                {"M2le", "Motion 2 Left"},
                {"M3ri", "Motion 3 Right"},
                {"M3le", "Motion 3 Left"},
                {"M4ri", "Motion 4 Right"},
                {"M4le", "Motion 4 Left"},
                {"M5ri", "Motion 5 Right"},
                {"M5le", "Motion 5 Left"},
                {"SM1r", "Second Motion 1 Right"},
                {"SM1l", "Second Motion 1 Left"},
                {"SM2r", "Second Motion 2 Right"},
                {"SM2l", "Second Motion 2 Left"},
                {"SM3r", "Second Motion 3 Right"},
                {"SM3l", "Second Motion 3 Left"},
                {"SM4r", "Second Motion 4 Right"},
                {"SM4l", "Second Motion 4 Left"},
                {"SM5r", "Second Motion 5 Right"},
                {"SM5l", "Second Motion 5 Left"}
            };
            public static readonly Dictionary<string, string> Alignments = new Dictionary<string, string>
            {
                {"Norm", "Normal"},
                {"DL_R", "Defensive Line Right"},
                {"DL_L", "Defensive Line Left"},
                {"DL_P", "Defensive Line Pinch"},
                {"DL_S", "Defensive Line Spread"},
                {"LB_R", "Linebackers Right"},
                {"LB_L", "Linebackers Left"},
                {"LB_P", "Linebackers Right"},
                {"LB_S", "Linebackers Left"},
                {"Tigh", "Press Coverage"},
                {"Loos", "Back Off Coverage"},
                {"Blit", "Show Blitz"}
            };
            public SGFM SGFM { get; set; }
            public List<SETG> SETG { get; set; }

            public Alignment(SGFM _SGFM, List<SETG> _SETG)
            {
                SGFM = _SGFM;
                SETG = _SETG;
            }

            public override string ToString()
            {
                string alignments = SGFM.name + "\n";
                foreach (SETG alignment in SETG) alignments += "    " + alignment.ToString() + "\n";
                if (alignments.Length >= 2)
                {
                    alignments.Substring(alignments.Length - 2, 2);
                }
                return
                    alignments;
            }

            #region INotifyPropertyChanged Members

            [field: NonSerializedAttribute()]
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion // INotifyPropertyChanged Members
        }

        #endregion

        #region Instantiation

        public override string ToString()
        {
            string SubFormationVM_string = PBST.name + "\n" + PBST + "\n" + SETL + "\n";
            foreach (SETP setp in CurrentPackage) SubFormationVM_string += setp.ToString() + "\n";
            foreach (Package package in Packages)
            {
                SubFormationVM_string += package.ToString();
            }
            foreach (Alignment alignment in Alignments)
            {
                SubFormationVM_string += alignment.ToString();
            }
            return SubFormationVM_string;
        }

        public PBST PBST { get; set; }
        public SETL SETL { get; set; }

        public List<Package> Packages { get; set; }
        public List<SETP> BasePackage { get; set; }
        public List<SETP> CurrentPackage { get; set; }

        public List<Alignment> Alignments { get; set; }
        public Alignment CurrentAlignment { get; set; }

        public FormationVM Formation { get; set; }
        public ObservableCollection<PlayVM> Plays { get; set; }
        private ObservableCollection<PlayerVM> _Players;
        public ObservableCollection<PlayerVM> Players
        {
            get { return _Players; }
            set
            {
                if (_Players == value)
                    return;
                _Players = value;
                OnPropertyChanged("PlayerPlayartView");
            }
        }

        [NonSerialized]
        private ICollectionView _PlayerPlayartView;
        public ICollectionView PlayerPlayartView
        {
            get { return _PlayerPlayartView; }
            set
            {
                if (_PlayerPlayartView == value)
                    return;
                _PlayerPlayartView = value;
                OnPropertyChanged("PlayerPlayartView");
            }
        }

        public string Position1name { get; set; }
        public string Position2name { get; set; }
        public string Position3name { get; set; }
        public int? Position1count { get; set; }
        public int? Position2count { get; set; }
        public int? Position3count { get; set; }

        private bool _isExpanded;
        private bool _isVisible;
        private bool _isSelected;

        public SubFormationVM()
        {

        }

        public SubFormationVM(PBST pbst, FormationVM _Formation)
        {
            IsVisible = false;
            IsExpanded = false;
            PBST = pbst;
            SETL = _Formation.Playbook.SETL.FirstOrDefault(set => set.setl == PBST.SETL);
            Plays = new ObservableCollection<PlayVM>();
            Packages = new List<Package>();
            CurrentPackage = new List<SETP>();
            Alignments = new List<Alignment>();
            GetFormation(_Formation);
            GetPackages();
            GetAlignments();
            GetPlayListFlags();
            //GetAlignment("Norm");
            GetPlayers();
            GetSubCount();
            GetPlays();
        }

        #endregion

        #region Remove Play

        public void RemovePlay(PlayVM Play, bool dbOnly = false)
        {
            #region PBPL

            for (int i = Play.PBPL.ord_; i < Plays.Count; i++)
            {
                Formation.Playbook.PBPL[Plays[i].PBPL.rec].ord_--;
            }
            PBPL dbPBPL = Formation.Playbook.PBPL.FirstOrDefault(play => play.rec == Play.PBPL.rec);
            Formation.Playbook.PBPL.Remove(dbPBPL);

            #endregion

            if (!Formation.Playbook.PBPL.Exists(play => play.PLYL == Play.PBPL.PLYL))
            {
                Formation.Playbook.PLYL.Remove(Play.PLYL);
                Formation.Playbook.PLPD.Remove(Play.PLPD);
                Formation.Playbook.PLRD.Remove(Play.PLRD);
                foreach (PLYS plys in Play.PLYS)
                {
                    Formation.Playbook.PLYS.Remove(plys);
                }
                List<PSAL> psals = new List<PSAL>();
                List<Madden.TeamPlaybook.ARTL> playart = new List<Madden.TeamPlaybook.ARTL>();
                foreach (PlayerVM player in Play.Players)
                {
                    psals.AddRange(player.PSAL);
                    playart.Add(player.ARTL);
                }
                foreach (PSAL psal in psals)
                {
                    if (!Formation.Playbook.IsUsing(psal))
                    {
                        Formation.Playbook.PSAL.Remove(psal);
                    }
                }
                foreach (ARTL artl in playart)
                {
                    if (!Formation.Playbook.IsUsing(artl))
                    {
                        Formation.Playbook.ARTL.Remove(artl);
                    }
                }
                foreach (PBAI pbai in Play.Situations)
                {
                    Formation.Playbook.PBAI.Remove(pbai);
                }
                foreach (PBAU pbau in Play.Audibles)
                {
                    Formation.Playbook.PBAU.Remove(pbau);
                }
                foreach (PBCC pbcc in Play.PBCC)
                {
                    Formation.Playbook.PBCC.Remove(pbcc);
                }
                foreach (PLCM plcm in Play.PLCM)
                {
                    Formation.Playbook.PLCM.Remove(plcm);
                }
                foreach (PPCT ppct in Play.PPCT)
                {
                    Formation.Playbook.PPCT.Remove(ppct);
                }
                foreach (SDEF sdef in Play.SDEF)
                {
                    Formation.Playbook.SDEF.Remove(sdef);
                }
                foreach (SRFT srft in Play.SRFT)
                {
                    Formation.Playbook.SRFT.Remove(srft);
                }
            }

            #region Hidden Sub-Formation Check

            try
            {
                //Check if Play has a hidden Sub-Formation and remove it
                SETL SETL = Formation.Playbook.SETL.FirstOrDefault(set => set.setl == Play.PLYL.SETL);
                if (Formation.Playbook.PBST.FirstOrDefault(set => set.SETL == SETL.setl) == null)
                {
                    Formation.RemoveHiddenSubFormation(SETL);
                }
            }
            catch
            {
            }

            #endregion

            Plays.Remove(Play);
        }

        #endregion

        #region Add Play

        public void AddPlay(PlayVM Play, int ord = 0, bool dbOnly = false)
        {
            #region PLYL

            if (Formation.Playbook.PLYL.Exists(play => play.plyl == Play.PLYL.plyl))
            {
                int? newPLYLid =
                    TeamPlaybook.NextAvailableID(Formation.Playbook.PLYL.Select(plyl => plyl.plyl).ToList(), 29900, 32767) ?? 
                    TeamPlaybook.NextAvailableID(Formation.Playbook.PLYL.Select(plyl => plyl.plyl).ToList(), 1, 32767);
                if (newPLYLid == null)
                {
                    MessageBox.Show(
                        "The maximum number of plays has been reached! Play not added",
                        "Maximum Plays Reached",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }
                Play.PLYL.plyl = newPLYLid ?? 0;
            }

            if (Play.PLPD != null)
            {
                Play.PLPD.PLYL = Play.PLYL.plyl;
            }
            if (Play.PLRD != null)
            {
                Play.PLRD.PLYL = Play.PLYL.plyl;
            }
            if (Play.PLYS != null)
            {
                foreach (PLYS play in Play.PLYS)
                {
                    play.PLYL = Play.PLYL.plyl;
                }
            }
            if (Play.PLCM != null)
            {
                foreach (PLCM play in Play.PLCM)
                {
                    play.PLYL = Play.PLYL.plyl;
                }
            }
            if (Play.PPCT != null)
            {
                foreach (PPCT play in Play.PPCT)
                {
                    play.plyl = Play.PLYL.plyl;
                }
            }
            if (Play.SRFT != null)
            {
                foreach (SRFT play in Play.SRFT)
                {
                    play.PLYL = Play.PLYL.plyl;
                }
            }
            
            Formation.Playbook.PLYL.Add(Play.PLYL);

            #endregion

            #region PBPL

            if (ord != 0)
            {
                for (int i = ord - 1; i < Plays.Count; i++)
                {
                    Plays[i].PBPL.ord_ += 1;
                }
            }

            Play.PBPL.pbpl = Formation.Playbook.PBPL.Exists(play => play.pbpl == Play.PBPL.pbpl) ? Formation.Playbook.PBPL.Select(pbpl => pbpl.pbpl).Max() + 1 : Play.PBPL.pbpl;
            Play.PBPL.PLYL = Play.PLYL.plyl;
            foreach (PBAI situation in Play.Situations)
            {
                situation.PBPL = Play.PBPL.pbpl;
            }
            foreach (PBAU audible in Play.Audibles)
            {
                audible.PBPL = Play.PBPL.pbpl;
            }

            Play.PBPL.Flag = 0;
            Formation.Playbook.PBPL.Add(Play.PBPL);
            Formation.Playbook.PBPL[Formation.Playbook.PBPL.Count - 1].rec = Formation.Playbook.PBPL.Count - 1;
            if (ord != 0)
            {
                Formation.Playbook.PBPL[Formation.Playbook.PBPL.Count - 1].ord_ = ord;
            }
            else
            {
                Formation.Playbook.PBPL[Formation.Playbook.PBPL.Count - 1].ord_ = Plays.Count + 1;
            }
            Formation.Playbook.PBPL[Formation.Playbook.PBPL.Count - 1].PBST = PBST.pbst;

            #endregion

            #region PLPD & PLRD

            if (Play.PLPD != null)
            {
                Formation.Playbook.PLPD.Add(Play.PLPD);
            }

            if (Play.PLRD != null)
            {
                Formation.Playbook.PLRD.Add(Play.PLRD);
            }

            #endregion

            #region PSAL & ARTL

            foreach (PlayerVM player in Play.Players)
            {
                Madden.TeamPlaybook.PSAL.DoesPSALExist PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.No;
                List<PSAL> PSAL = Formation.Playbook.PSAL.Where(psal => psal.psal == player.PSAL[0].psal).OrderBy(s => s.step).Cast<PSAL>().ToList();

                if (PSAL.Count > 0)
                {
                    if (PSAL.Count == player.PSAL.Count)
                    {
                        for (int i = 0; i < player.PSAL.Count; i++)
                        {
                            try
                            {
                                if (PSAL[i].IsIdentical(player.PSAL[i]))
                                {
                                    PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.IsIdentical;
                                }
                                else
                                {
                                    PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.Yes;
                                    break;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.No;
                                break;
                            }
                        }
                    }
                    else
                    {
                        PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.Yes;
                    }
                }

                if (PSALexists == Madden.TeamPlaybook.PSAL.DoesPSALExist.Yes)
                {
                    int nextAvailablePSALID = Formation.Playbook.PSAL.Select(psal => psal.psal).Max() + 1;
                    MessageBox.Show(
                        "Existing PSAL: " +
                        PSAL[0].psal +
                        "\n\n" +
                        "New PSAL ID: " +
                        nextAvailablePSALID
                    );
                    foreach (PSAL psal in player.PSAL)
                    {
                        psal.psal = nextAvailablePSALID;
                        psal.rec = Formation.Playbook.PSAL.Count + psal.step;
                    }
                    Play.PLYS[player.PLYS.poso].PSAL = nextAvailablePSALID;

                    Formation.Playbook.PSAL.AddRange(player.PSAL);
                }
                else if (PSALexists == Madden.TeamPlaybook.PSAL.DoesPSALExist.No)
                {
                    foreach (PSAL psal in player.PSAL)
                    {
                        psal.rec = Formation.Playbook.PSAL.Count + psal.step;
                    }
                    Formation.Playbook.PSAL.AddRange(player.PSAL);
                }

                ARTL ARTL = Formation.Playbook.ARTL.FirstOrDefault(artl => artl.IsIdentical(player.ARTL));

                if (ARTL == null)
                {
                    player.ARTL.rec = Formation.Playbook.ARTL.Count;
                    Formation.Playbook.ARTL.Add(player.ARTL);
                }
                else if (!ARTL.IsIdentical(player.ARTL))
                {
                    int nextAvailableARTLID = Formation.Playbook.ARTL.Select(artl => artl.artl).Max() + 1;
                    int oldARTLID = player.ARTL.artl;

                    player.ARTL.artl = nextAvailableARTLID;
                    Play.PLYS[player.PLYS.poso].ARTL = nextAvailableARTLID;
                    player.ARTL.rec = Formation.Playbook.ARTL.Count;

                    Formation.Playbook.ARTL.Add(player.ARTL);
                }
            }

            #endregion

            #region PLYS

            for (int i = 0; i < Play.PLYS.Count; i++)
            {
                Formation.Playbook.PLYS.Add(Play.PLYS[i]);
            }

            #endregion

            #region PBAI

            if (Play.Situations != null)
            {
                for (int i = 0; i < Play.Situations.Count; i++)
                {
                    Formation.Playbook.PBAI.Add(Play.Situations[i]);
                }
            }

            #endregion

            #region PBAU

            if (Play.Audibles != null)
            {
                for (int i = 0; i < Play.Audibles.Count; i++)
                {
                    Formation.Playbook.PBAU.Add(Play.Audibles[i]);
                }
            }

            #endregion

            #region PBCC

            if (Play.PBCC != null)
            {
                for (int i = 0; i < Play.PBCC.Count; i++)
                {
                    Formation.Playbook.PBCC.Add(Play.PBCC[i]);
                }
            }

            #endregion

            #region PLCM

            if (Play.PLCM != null)
            {
                for (int i = 0; i < Play.PLCM.Count; i++)
                {
                    Formation.Playbook.PLCM.Add(Play.PLCM[i]);
                }
            }

            #endregion

            #region PPCT

            if (Play.PPCT != null)
            {
                for (int i = 0; i < Play.PPCT.Count; i++)
                {
                    Formation.Playbook.PPCT.Add(Play.PPCT[i]);
                }
            }

            #endregion

            #region SDEF

            if (Play.SDEF != null)
            {
                for (int i = 0; i < Play.SDEF.Count; i++)
                {
                    Formation.Playbook.SDEF.Add(Play.SDEF[i]);
                }
            }

            #endregion

            #region SRFT

            if (Play.SRFT != null)
            {
                for (int i = 0; i < Play.SRFT.Count; i++)
                {
                    Formation.Playbook.SRFT.Add(Play.SRFT[i]);
                }
            }

            #endregion

            #region Hidden Formation Check

            SETL PBhiddenSETL = Formation.Playbook.SETL.FirstOrDefault(subFormation => subFormation.setl == Play.PLYL.SETL);
            SETL PlayhiddenSETL = Play.SubFormation.Formation.Playbook.SETL.FirstOrDefault(subFormation => subFormation.setl == Play.PLYL.SETL);
            FORM hiddenFORM = Formation.Playbook.FORM.FirstOrDefault(formation => formation.form == PlayhiddenSETL.FORM);

            //Check for Hidden Sub-Formation
            if (PBhiddenSETL == null)
            {
                Play.AddHiddenSubFormation(Play, Formation.Playbook);
            }

            //Check for Hidden Formation
            if (hiddenFORM == null)
            {
                Formation.Playbook.FORM.Add(Play.SubFormation.Formation.Playbook.FORM.FirstOrDefault(formation => formation.form == PlayhiddenSETL.FORM));
            }

            #endregion

            if (!dbOnly)
            {
                if (ord == 0)
                {
                    Plays.Add(new PlayVM(Formation.Playbook.PBPL[Formation.Playbook.PBPL.Count - 1], this));
                }
                else
                {
                    Plays.Insert(ord - 1, new PlayVM(Formation.Playbook.PBPL[Formation.Playbook.PBPL.Count - 1], this));
                }
            }
        }

        public void AddPlay(MaddenCustomPlaybookEditor.CustomPlaybookPLAY Play, int ord = 0)
        {
            #region PLYL

            if (Formation.Playbook.PLYL.Exists(play => play.plyl == Play.PBPL[0].PLYL))
            {
                int? newPLYLid =
                    TeamPlaybook.NextAvailableID(Formation.Playbook.PLYL.Select(plyl => plyl.plyl).ToList(), 29900, 32767) ??
                    TeamPlaybook.NextAvailableID(Formation.Playbook.PLYL.Select(plyl => plyl.plyl).ToList(), 1, 32767);
                if (newPLYLid == null)
                {
                    MessageBox.Show(
                        "The maximum number of plays has been reached! Play not added",
                        "Maximum Plays Reached",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation);
                    return;
                }
                Play.PBPL[0].PLYL = newPLYLid ?? 0;
            }

            if (Play.PLPD.Count > 0)
            {
                Play.PLPD[0].PLYL = Play.PBPL[0].PLYL;
            }
            if (Play.PLRD.Count > 0)
            {
                Play.PLRD[0].PLYL = Play.PBPL[0].PLYL;
            }
            if (Play.PLYS != null)
            {
                foreach (MaddenCustomPlaybookEditor.PLYS play in Play.PLYS)
                {
                    play.PLYL = Play.PBPL[0].PLYL;
                }
            }
            if (Play.PLCM.Count > 0)
            {
                foreach (MaddenCustomPlaybookEditor.PLCM play in Play.PLCM)
                {
                    play.PLYL = Play.PBPL[0].PLYL;
                }
            }
            if (Play.PPCT.Count > 0)
            {
                foreach (MaddenCustomPlaybookEditor.PPCT play in Play.PPCT)
                {
                    play.PLYL = Play.PBPL[0].PLYL;
                }
            }
            if (Play.SRFT.Count > 0)
            {
                foreach (MaddenCustomPlaybookEditor.SRFT play in Play.SRFT)
                {
                    play.PLYL = Play.PBPL[0].PLYL;
                }
            }

            PLYL CPBplyl = new PLYL
            {
                rec = Formation.Playbook.PLYL.Count,
                SETL = SETL.setl,
                plyl = Play.PBPL[0].PLYL,
                SRMM = Play.PBPL[0].SRMM,
                SITT = Play.PBPL[0].SITT,
                PLYT = Play.PBPL[0].PLYT,
                PLF_ = Play.PBPL[0].PLF_,
                name = Play.PBPL[0].name,
                risk = Play.PBPL[0].risk,
                motn = Play.PBPL[0].motn,
                phlp = 0,
                vpos = Play.PBPL[0].vpos
            };

            Formation.Playbook.PLYL.Add(CPBplyl);

            #endregion

            #region PBPL

            int pbplID = Formation.Playbook.PBPL.Select(pbpl => pbpl.pbpl).Max() + 1;

            if (ord == 0)
            {
                ord = Plays.Count + 1;
            }

            PBPL CPBpbpl = new PBPL
            {
                rec = Formation.Playbook.PBPL.Count,
                PKGE = 0,
                COMF = Play.PBPL[0].COMF,
                pbpl = pbplID,
                PLYL = Play.PBPL[0].PLYL,
                PBST = PBST.pbst,
                ord_ = ord,
                name = Play.PBPL[0].name,
                Flag = Play.PGPL[0].Flag
            };

            Formation.Playbook.PBPL.Add(CPBpbpl);

            #endregion

            #region PLPD

            if (Play.PLPD.Count > 0)
            {
                PLPD CPBplpd = new PLPD
                {
                    rec = Formation.Playbook.PLPD.Count,
                    PLYL = Play.PLPD[0].PLYL
                };

                CPBplpd.progressions = new List<Progression>();
                CPBplpd.progressions.Add(new Progression
                {
                    com = Play.PLPD[0].com1,
                    con = Play.PLPD[0].con1,
                    per = Play.PLPD[0].per1,
                    rcv = Play.PLPD[0].rcv1,
                    icx = 0,
                    icy = 0
                });
                CPBplpd.progressions.Add(new Progression
                {
                    com = Play.PLPD[0].com2,
                    con = Play.PLPD[0].con2,
                    per = Play.PLPD[0].per2,
                    rcv = Play.PLPD[0].rcv2,
                    icx = 0,
                    icy = 0
                });
                CPBplpd.progressions.Add(new Progression
                {
                    com = Play.PLPD[0].com3,
                    con = Play.PLPD[0].con3,
                    per = Play.PLPD[0].per3,
                    rcv = Play.PLPD[0].rcv3,
                    icx = 0,
                    icy = 0
                });
                CPBplpd.progressions.Add(new Progression
                {
                    com = Play.PLPD[0].com4,
                    con = Play.PLPD[0].con4,
                    per = Play.PLPD[0].per4,
                    rcv = Play.PLPD[0].rcv4,
                    icx = 0,
                    icy = 0
                });
                CPBplpd.progressions.Add(new Progression
                {
                    com = Play.PLPD[0].com5,
                    con = Play.PLPD[0].con5,
                    per = Play.PLPD[0].per5,
                    rcv = Play.PLPD[0].rcv5,
                    icx = 0,
                    icy = 0
                });

                Formation.Playbook.PLPD.Add(CPBplpd);

            }
            #endregion

            #region PLRD

            if (Play.PLRD.Count > 0)
            {
                PLRD CPBplrd = new PLRD
                {
                    rec = Formation.Playbook.PLRD.Count,
                    PLYL = Play.PLRD[0].PLYL,
                    hole = Play.PLRD[0].hole
                };

                Formation.Playbook.PLRD.Add(CPBplrd);
            }

            #endregion

            #region PLYS

            for (int i = 0; i < Play.PLYS.Count; i++)
            {
                PLYS CPBplys = new PLYS
                {
                    rec = Formation.Playbook.PLYS.Count,
                    PSAL = Play.PLYS[i].PSAL,
                    ARTL = Play.PLYS[i].ARTL,
                    PLYL = Play.PLYS[i].PLYL,
                    PLRR = Play.PLYS[i].PLRR,
                    poso = Play.PLYS[i].poso
                };

                Formation.Playbook.PLYS.Add(CPBplys);
            }

            #endregion

            #region PSAL

            for (int poso = 0; poso < Play.PSAL.Count; poso++)
            {
                Madden.TeamPlaybook.PSAL.DoesPSALExist PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.No;

                List<PSAL> pbPSAL = Formation.Playbook.PSAL.Where(psal => psal.psal == Play.PSAL[poso][0].psal).OrderBy(s => s.step).Cast<PSAL>().ToList();

                if (pbPSAL.Count > 0 && pbPSAL.Count == Play.PSAL[poso].Count)
                {
                    if (pbPSAL.Count == Play.PSAL[poso].Count)
                    {
                        for (int i = 0; i < Play.PSAL[poso].Count; i++)
                        {
                            try
                            {
                                if (pbPSAL[i].IsIdentical(Play.PSAL[poso][i]))
                                {
                                    PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.IsIdentical;
                                }
                                else
                                {
                                    PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.Yes;
                                    break;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.No;
                            }
                        }
                    }
                    else
                    {
                        PSALexists = Madden.TeamPlaybook.PSAL.DoesPSALExist.Yes;
                        break;
                    }
                }

                if (PSALexists == Madden.TeamPlaybook.PSAL.DoesPSALExist.Yes)
                {
                    int nextAvailablePSALID = Formation.Playbook.PSAL.Select(psal => psal.psal).Max() + 1;
                    MessageBox.Show(
                        "Existing PSAL: " +
                        Play.PLYS[poso].PSAL +
                        "\n\n" +
                        "New PSAL ID: " +
                        nextAvailablePSALID
                    );
                    Play.PLYS[poso].PSAL = nextAvailablePSALID;

                    foreach (MaddenCustomPlaybookEditor.PSAL step in Play.PSAL[poso])
                    {
                        PSAL CPBpsal = new PSAL
                        {
                            rec = Formation.Playbook.PSAL.Count,
                            val1 = step.val1,
                            val2 = step.val2,
                            val3 = step.val3,
                            psal = nextAvailablePSALID,
                            code = step.code,
                            step = step.step
                        };

                        Formation.Playbook.PSAL.Add(CPBpsal);
                    }
                }
                else if (PSALexists == Madden.TeamPlaybook.PSAL.DoesPSALExist.No)
                {
                    foreach (MaddenCustomPlaybookEditor.PSAL step in Play.PSAL[poso])
                    {
                        PSAL CPBpsal = new PSAL
                        {
                            rec = Formation.Playbook.PSAL.Count,
                            val1 = step.val1,
                            val2 = step.val2,
                            val3 = step.val3,
                            psal = step.psal,
                            code = step.code,
                            step = step.step
                        };

                        Formation.Playbook.PSAL.Add(CPBpsal);
                    }
                }
            }

            #endregion

            #region ARTL

            for (int i = 0; i < Play.ARTL.Count; i++)
            {
                ARTL ARTL = Formation.Playbook.ARTL.FirstOrDefault(artl => artl.artl == Play.ARTL[i].artl);

                if (ARTL == null)
                {
                    ARTL CPBartl = new ARTL
                    {
                        rec = Formation.Playbook.ARTL.Count,
                        artl = Play.ARTL[i].artl,
                        acnt = Play.ARTL[i].acnt
                    };
                    CPBartl.ARTList = new List<PlayArt>();
                    for (int n = 0; n < 12; n++)
                    {
                        CPBartl.ARTList.Add(new PlayArt
                        {
                            sp = Play.ARTL[i].sp[n],
                            ls = Play.ARTL[i].ls[n],
                            at = Play.ARTL[i].at[n],
                            ct = Play.ARTL[i].ct[n],
                            lt = Play.ARTL[i].lt[n],
                            au = Play.ARTL[i].au[n],
                            av = Play.ARTL[i].av[n],
                            ax = Play.ARTL[i].ax[n],
                            ay = Play.ARTL[i].ay[n]
                        });
                    }

                    Formation.Playbook.ARTL.Add(CPBartl);
                }
                else if (!ARTL.IsIdentical(Play.ARTL[i]))
                {
                    int nextAvailableARTLID = Formation.Playbook.ARTL.Select(artl => artl.artl).Max() + 1;
                    Play.PLYS[i].ARTL = nextAvailableARTLID;

                    ARTL CPBartl = new ARTL
                    {
                        rec = Formation.Playbook.ARTL.Count,
                        artl = nextAvailableARTLID,
                        acnt = Play.ARTL[i].acnt
                    };
                    CPBartl.ARTList = new List<PlayArt>();
                    for (int n = 0; n < 12; n++)
                    {
                        CPBartl.ARTList.Add(new PlayArt
                        {
                            sp = Play.ARTL[i].sp[n],
                            ls = Play.ARTL[i].ls[n],
                            at = Play.ARTL[i].at[n],
                            ct = Play.ARTL[i].ct[n],
                            lt = Play.ARTL[i].lt[n],
                            au = Play.ARTL[i].au[n],
                            av = Play.ARTL[i].av[n],
                            ax = Play.ARTL[i].ax[n],
                            ay = Play.ARTL[i].ay[n]
                        });
                    }

                    Formation.Playbook.ARTL.Add(CPBartl);
                }
            }

            #endregion

            #region PBAI

            //if (Play.PBAI.Count > 0)
            //{
            //    for (int i = 0; i < Play.PBAI.Count; i++)
            //    {
            //        PBAI CPBpbai = new PBAI
            //        {
            //            rec = Formation.Playbook.PBAI.Count,
            //            PBPL = CPBpbpl.pbpl,
            //            SETL = CPBplyl.SETL,
            //            AIGR = Play.PBAI[i].AIGR,
            //            PLYT = CPBplyl.plyl,
            //            PLF_ = CPBplyl.PLF_,
            //            Flag = 0,
            //            vpos = CPBplyl.vpos,
            //            prct = 10
            //        };

            //        Formation.Playbook.PBAI.Add(CPBpbai);
            //    }
            //}

            #endregion

            #region PBAU

            //if (Play.PBAU.Count > 0)
            //{
            //    for (int i = 0; i < Play.PBAU.Count; i++)
            //    {
            //        PBAU CPBpbau = new PBAU
            //        {
            //            rec = Formation.Playbook.PBAU.Count,
            //            PBPL = CPBpbpl.pbpl,
            //            FTYP = Play.PBAU[i].FTYP,
            //            pbau = Play.PBAU[i].pbau
            //        };

            //        Formation.Playbook.PBAU.Add(CPBpbau);
            //    }
            //}

            #endregion

            #region PLCM

            if (Play.PLCM.Count > 0)
            {
                for (int i = 0; i < Play.PLCM.Count; i++)
                {
                    PLCM CPBplcm = new PLCM
                    {
                        rec = Formation.Playbook.PLCM.Count,
                        per1 = Play.PLCM[i].per1,
                        dir1 = Play.PLCM[i].dir1,
                        ply1 = Play.PLCM[i].ply1,
                        per2 = Play.PLCM[i].per2,
                        dir2 = Play.PLCM[i].dir2,
                        ply2 = Play.PLCM[i].ply2,
                        per3 = Play.PLCM[i].per3,
                        dir3 = Play.PLCM[i].dir3,
                        ply3 = Play.PLCM[i].ply3,
                        per4 = Play.PLCM[i].per4,
                        dir4 = Play.PLCM[i].dir4,
                        ply4 = Play.PLCM[i].ply4,
                        per5 = Play.PLCM[i].per5,
                        dir5 = Play.PLCM[i].dir5,
                        ply5 = Play.PLCM[i].ply5,
                        PLYL = Play.PLCM[i].PLYL
                    };

                    Formation.Playbook.PLCM.Add(CPBplcm);
                }
            }

            #endregion

            #region PPCT

            if (Play.PPCT.Count > 0)
            {
                for (int i = 0; i < Play.PPCT.Count; i++)
                {
                    PPCT CPBppct = new PPCT
                    {
                        rec = Formation.Playbook.PPCT.Count,
                        plyl = Play.PPCT[i].PLYL,
                        conp = Play.PPCT[i].conp,
                        recr = Play.PPCT[i].recr
                    };

                    Formation.Playbook.PPCT.Add(CPBppct);
                }
            }

            #endregion

            #region SDEF

            if (Play.SDEF != null && Play.SDEF.Count > 0)
            {
                for (int i = 0; i < Play.SDEF.Count; i++)
                {
                    SDEF CPBsdef = new SDEF
                    {
                        rec = Formation.Playbook.SDEF.Count,
                        ATCA = Play.SDEF[i].ATCA,
                        PLYL = Play.SDEF[i].PLYL,
                        DFLP = Play.SDEF[i].DFLP,
                        STRP = Play.SDEF[i].STRP,
                        STRR = Play.SDEF[i].STRR
                    };

                    Formation.Playbook.SDEF.Add(CPBsdef);

                }
            }

            #endregion

            #region SRFT

            if (Play.SRFT != null && Play.SRFT.Count > 0)
            {
                for (int i = 0; i < Play.SRFT.Count; i++)
                {
                    SRFT CPBsrft = new SRFT
                    {
                        rec = Formation.Playbook.SRFT.Count,
                        SIDE = Play.SRFT[i].SIDE,
                        YOFF = Play.SRFT[i].YOFF,
                        TECH = Play.SRFT[i].TECH,
                        PLYL = Play.SRFT[i].PLYL,
                        STAN = Play.SRFT[i].STAN,
                        PLYR = Play.SRFT[i].PLYR,
                        PRIS = Play.SRFT[i].PRIS,
                        GAPS = Play.SRFT[i].GAPS,
                        ASSS = Play.SRFT[i].ASSS,
                        PRIW = Play.SRFT[i].PRIW,
                        GAPW = Play.SRFT[i].GAPW,
                        ASSW = Play.SRFT[i].ASSW
                    };

                    Formation.Playbook.SRFT.Add(CPBsrft);

                }
            }

            #endregion

            if (ord <= Plays.Count && ord != 0)
            {
                for (int i = ord - 1; i < Plays.Count; i++)
                {
                    Plays[i].PBPL.ord_ += 1;
                }
                Plays.Insert(ord - 1, new PlayVM(CPBpbpl, this));
            }
            else
            {
                Plays.Add(new PlayVM(CPBpbpl, this));
            }
            Plays[ord - 1].IsVisible = IsExpanded;
        }

        #endregion

        #region Get Plays

        public void GetPlays()
        {
            if (Plays.Count > 0) Plays.Clear();
            foreach (PBPL play in Formation.Playbook.PBPL.Where(set => set.PBST == PBST.pbst))
            {
                Plays.Add(new PlayVM(play, this));
            }
            Plays = new ObservableCollection<PlayVM>(Plays.OrderBy(play => play.PBPL.ord_));
        }

        #endregion

        #region Get Sub Count

        public void GetSubCount()
        {
            Position1count = Position2count = Position3count = 0;
            foreach (SETP position in CurrentPackage.Where(position => position.SETL == PBST.SETL))
            {
                switch (position.EPos)
                {
                    case 1://RB
                        Position1count++;
                        Position1name = "RB";
                        break;
                    case 2://FB
                        Position1count++;
                        Position1name = "RB";
                        break;
                    case 3://WR
                        Position3count++;
                        Position3name = "WR";
                        break;
                    case 4://TE
                        Position2count++;
                        Position2name = "TE";
                        break;
                    case 10://LE
                        Position1count++;
                        Position1name = "DL";
                        break;
                    case 11://RE
                        Position1count++;
                        Position1name = "DL";
                        break;
                    case 12://DT
                        Position1count++;
                        Position1name = "DL";
                        break;
                    case 13://LOLB
                        Position2count++;
                        Position2name = "LB";
                        break;
                    case 14://MLB
                        Position2count++;
                        Position2name = "LB";
                        break;
                    case 15://ROLB
                        Position2count++;
                        Position2name = "LB";
                        break;
                    case 16://CB
                        Position3count++;
                        Position3name = "DB";
                        break;
                    case 17://FS
                        Position3count++;
                        Position3name = "DB";
                        break;
                    case 18://SS
                        Position3count++;
                        Position3name = "DB";
                        break;
                    case 25://3RB
                        Position1count++;
                        Position1name = "RB";
                        break;
                    case 26://PRB
                        Position1count++;
                        Position1name = "RB";
                        break;
                    case 27://SWR
                        Position3count++;
                        Position3name = "WR";
                        break;
                    case 28://RLE
                        Position1count++;
                        Position1name = "DL";
                        break;
                    case 29://RRE
                        Position1count++;
                        Position1name = "DL";
                        break;
                    case 30://RDT
                        Position1count++;
                        Position1name = "DL";
                        break;
                    case 31://SLB
                        Position2count++;
                        Position2name = "LB";
                        break;
                    case 32://SCB
                        Position3count++;
                        Position3name = "DB";
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Get Formation

        public void GetFormation(FormationVM _Formation)
        {
            Formation = _Formation;
        }

        #endregion

        #region Set Generic Audibles

        public void SetGenericAudibles()
        {
            foreach (PlayVM Play in Plays)
            {
                if (Play.isShortAudible) Play.isShortAudible = false;
                if (Play.isRunAudible) Play.isRunAudible = false;
                if (Play.isDeepAudible) Play.isDeepAudible = false;
                if (Play.isFakeAudible) Play.isFakeAudible = false;
            }

            PlayVM ShortAudible =
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 101) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 102) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 103) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 159) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 4) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 159) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 103) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 102) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 101);

            if (ShortAudible == null)
            {
                ShortAudible = Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible);
            }
            if (ShortAudible != null) ShortAudible.isShortAudible = true;

            PlayVM RunAudible =
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 195) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 151) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 169) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 196) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 197) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 205) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 207) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 205) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 197) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 196) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 169) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 151) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 195);

            if (RunAudible == null)
            {
                RunAudible = Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible);
            }
            if (RunAudible != null) RunAudible.isRunAudible = true;

            PlayVM DeepAudible =
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 102) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 101) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 103) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 159) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 4) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 159) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 103) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 101) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 102);

            if (DeepAudible == null)
            {
                DeepAudible = Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible);
            }
            if (DeepAudible != null) DeepAudible.isDeepAudible = true;

            PlayVM FakeAudible =
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 4) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 101) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 102) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 103) == null ?
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 159) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 103) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 102) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 101) :
                Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible && play.PLYL.PLYT == 4);

            if (FakeAudible == null)
            {
                FakeAudible = Plays.FirstOrDefault(play => !play.isShortAudible && !play.isRunAudible && !play.isFakeAudible && !play.isDeepAudible);
            }
            if (FakeAudible != null) FakeAudible.isFakeAudible = true;

            bool audibles = true;
            foreach (PlayVM play in Plays)
            {
                if (
                    play.isShortAudible == false &&
                    play.isRunAudible == false &&
                    play.isDeepAudible == false &&
                    play.isFakeAudible == false
                    )
                {
                    audibles = true;
                }
            }

            if (audibles == false)
            {
                MessageBox.Show(PBST.name, "No Audibles Set");
            }
        }

        #endregion

        #region Packages

        public void UpdatePackage(Package package)
        {
            if (!Packages.Exists(p => p.SPKF == package.SPKF))
            {
                Formation.Playbook.SPKF.Add(package.SPKF);
                Formation.Playbook.SPKF[Formation.Playbook.SPKF.Count - 1].rec = Formation.Playbook.SPKF.Count - 1;
                Packages.Add(package);
            }
            else
            {
                foreach (SPKG set in package.SPKG)
                {
                    Formation.Playbook.SPKG.Add(set);
                    Formation.Playbook.SPKG[Formation.Playbook.SPKG.Count - 1].rec = Formation.Playbook.SPKG.Count - 1;
                }
            }
        }

        public void GetBasePackage()
        {
            BasePackage = new List<SETP>();
            foreach (SETP player in Formation.Playbook.SETP.Where(player => player.SETL == PBST.SETL))
                BasePackage.Add(player);
            BasePackage.OrderBy(player => player.poso);
            CurrentPackage = BasePackage.Select(p => new SETP
            {
                arti = p.arti,
                artx = p.artx,
                arty = p.arty,
                DPos = p.DPos,
                EPos = p.EPos,
                flas = p.flas,
                fmtx = p.fmtx,
                fmty = p.fmty,
                poso = p.poso,
                rec = p.rec,
                SETL = p.SETL,
                setp = p.setp,
                SGT_ = p.SGT_,
                tabo = p.tabo
            }).ToList();
        }

        public void GetPackage(Package Package)
        {
            GetBasePackage();
            if (Package.SPKF.name == "Normal") return;
            foreach (SPKG player in Package?.SPKG)
            {
                try
                {
                    CurrentPackage.SingleOrDefault(poso => poso.poso == player.poso).DPos = player.DPos;
                    CurrentPackage.SingleOrDefault(poso => poso.poso == player.poso).EPos = player.EPos;
                }
                catch { }
            }
        }

        public void GetPackages()
        {
            GetBasePackage();
            Packages.Clear();
            foreach (SPKF package in Formation.Playbook.SPKF.Where(package => package.SETL == PBST.SETL))
            {
                List<SPKG> SPKG = new List<SPKG>();
                foreach (SPKG sub in Formation.Playbook.SPKG.Where(sub => sub.SPF_ == package.SPF_))
                    SPKG.Add(sub);
                SPKG.OrderBy(sub => sub.poso);
                Packages.Add(new Package(package, SPKG));
            }
            Packages.OrderBy(package => package.SPKF.SPF_);
            Packages.Insert(0, new Package(new SPKF { name = "Normal" }, new List<SPKG>()));
        }

        #endregion

        #region Alignments

        public void AddAlignment(Alignment alignment)
        {
            Formation.Playbook.SGFM.Add(alignment.SGFM);
            Formation.Playbook.SGFM[Formation.Playbook.SGFM.Count - 1].rec = Formation.Playbook.SGFM.Count - 1;
            foreach (SETG poso in alignment.SETG)
            {
                Formation.Playbook.SETG.Add(poso);
                Formation.Playbook.SETG[Formation.Playbook.SETG.Count - 1].rec = Formation.Playbook.SETG.Count - 1;
            }
        }

        public void GetAlignment(SGFM alignment)
        {
            Alignment DefaultAlignment = Alignments.FirstOrDefault(a => a.SGFM.name == "Norm");
            if (DefaultAlignment.SETG.Count < 11)
            {
                MessageBox.Show("Default SETG Missing for " + PBST.name);
            }
            if (alignment.name == "Norm")
            {
                CurrentAlignment = DefaultAlignment;
                return;
            }
            else
            {
                Alignment targetAlignment = Alignments.FirstOrDefault(a => a.SGFM.name == alignment.name);
                CurrentAlignment = new Alignment(targetAlignment?.SGFM, new List<SETG>(targetAlignment?.SETG?.ToList()));
                foreach (SETG _setg in DefaultAlignment.SETG.Where(s => !CurrentAlignment.SETG.Contains(s)))
                {
                    CurrentAlignment.SETG.Add(_setg);
                }
                CurrentAlignment.SETG = CurrentAlignment.SETG.OrderBy(s => s.SETP).ToList();
            }
        }

        public void GetAlignments()
        {
            Alignments.Clear();
            List<SGFM> alignments = Formation.Playbook.SGFM.Where(a => a.SETL == PBST.SETL).ToList();
            if (alignments == null || alignments.Count == 0) return;
            List<string> MotionAlignments = Formation.Playbook.Type == "Offense" ? Alignment.Motions.Keys.ToList() : Alignment.Alignments.Keys.ToList();
            foreach (string key in MotionAlignments)
            {
                SGFM alignment = alignments?.FirstOrDefault(m => m.name == key) ?? 
                    new SGFM { 
                        name = key, 
                        dflt = 0, 
                        SETL = PBST.SETL, 
                        SGF_ = Formation.Playbook.SGFM.Select(m => m.SGF_).Max() + 1 };
                List<SETG> SETG = Formation.Playbook.SETG.Where(a => a.SGF_ == alignment?.SGF_).ToList();
                SETG.OrderBy(a => a.SETP);
                Alignments.Add(new Alignment(alignment, SETG));
            }
            Alignments = Alignments.OrderByDescending(alignment => alignment.SGFM.dflt).ThenBy(alignment => alignment.SGFM.name).ToList();

            GetAlignment(Alignments.FirstOrDefault(a => a.SGFM.name == "Norm")?.SGFM);
        }

        public void GetPlayers()
        {
            Players = new ObservableCollection<PlayerVM>();
            if (CurrentAlignment != null)
            {
                for (int i = 0; i < CurrentAlignment.SETG.Count; i++)
                {
                    int poso = CurrentPackage.FirstOrDefault(p => p.setp == CurrentAlignment.SETG[i].SETP).poso;
                    Players.Add(new PlayerVM
                    (
                        new PLYS
                        {
                            poso = poso
                        },
                        new PlayVM
                        {
                            SubFormation = this,
                            PLYS = new List<PLYS>
                            {
                                new PLYS
                                {
                                    poso = poso
                                }
                            },
                            SRFT = new List<SRFT>(),
                            PLYL = new PLYL
                            {
                                vpos = 0
                            }
                        }
                    ));
                }
            }
            GetPlayerPlayartViewList();
        }

        public void GetPlayerPlayartViewList()
        {
            PlayerPlayartView = CollectionViewSource.GetDefaultView(Players);
            PlayerPlayartView.SortDescriptions.Add(new SortDescription("PLYS.poso", ListSortDirection.Descending));
        }

        #endregion

        #region Play List Flags

        public void GetPlayListFlags()
        {
            if (SETL == null) return;

            _Bunch =
            _CanFlip =
            _Close =
            _DimebacKatPosition5 =
            _NonSymmetrical =
            _NoWideAlignment =
            _PassOriented =
            _PreventDefense =
            _PreventAudible =
            _ProwlDefense =
            _RunOriented =
            _SpecialOriented =
            _Tight = false;

            int _flag = SETL.SLF_;

            foreach (KeyValuePair<int, string> key in SETL.SetListFlag.Reverse().Where(k => k.Key <= SETL.SLF_))
            {
                if (_flag >= key.Key)
                {
                    this.GetType()?.GetProperty(key.Value)?.SetValue(this, true);
                    SETL.SLF_ = SETL.SLF_ - key.Key;
                    _flag -= key.Key;
                }
            }
        }

        private bool _Bunch;
        public bool Bunch
        {
            get { return _Bunch; }
            set
            {
                if (_Bunch == value)
                    return;
                _Bunch = value;
                this.SETL.SLF_ += _Bunch ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Bunch", StringComparison.OrdinalIgnoreCase)).Key :
                    -1* SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Bunch", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("Bunch");
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
                this.SETL.SLF_ += _CanFlip ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Can Flip", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Can Flip", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("CanFlip");
            }
        }
        private bool _Close;
        public bool Close
        {
            get { return _Close; }
            set
            {
                if (_Close == value)
                    return;
                _Close = value;
                this.SETL.SLF_ += _Close ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Close", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Close", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("Close");
            }
        }
        private bool _DimebacKatPosition5;
        public bool DimebacKatPosition5
        {
            get { return _DimebacKatPosition5; }
            set
            {
                if (_DimebacKatPosition5 == value)
                    return;
                _DimebacKatPosition5 = value;
                this.SETL.SLF_ += _DimebacKatPosition5 ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "DimebacK At Pos 5", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "DimebacK At Pos 5", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("DimebacKatPosition5");
            }
        }
        private bool _NonSymmetrical;
        public bool NonSymmetrical
        {
            get { return _NonSymmetrical; }
            set
            {
                if (_NonSymmetrical == value)
                    return;
                _NonSymmetrical = value;
                this.SETL.SLF_ += _NonSymmetrical ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Non Symmetrical", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Non Symmetrical", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("NonSymmetrical");
            }
        }
        private bool _NoWideAlignment;
        public bool NoWideAlignment
        {
            get { return _NoWideAlignment; }
            set
            {
                if (_NoWideAlignment == value)
                    return;
                _NoWideAlignment = value;
                this.SETL.SLF_ += _NoWideAlignment ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "No Wide Align", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "No Wide Align", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("NoWideAlignment");
            }
        }
        private bool _PassOriented;
        public bool PassOriented
        {
            get { return _PassOriented; }
            set
            {
                if (_PassOriented == value)
                    return;
                _PassOriented = value;
                this.SETL.SLF_ += _PassOriented ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Pass Oriented", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Pass Oriented", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PassOriented");
            }
        }
        private bool _PreventDefense;
        public bool PreventDefense
        {
            get { return _PreventDefense; }
            set
            {
                if (_PreventDefense == value)
                    return;
                _PreventDefense = value;
                this.SETL.SLF_ += _PreventDefense ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Prevent Defense", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Prevent Defense", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PreventDefense");
            }
        }
        private bool _PreventAudible;
        public bool PreventAudible
        {
            get { return _PreventAudible; }
            set
            {
                if (_PreventAudible == value)
                    return;
                _PreventAudible = value;
                this.SETL.SLF_ += _PreventAudible ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Prevent Audible", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Prevent Audible", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("PreventAudible");
            }
        }
        private bool _ProwlDefense;
        public bool ProwlDefense
        {
            get { return _ProwlDefense; }
            set
            {
                if (_ProwlDefense == value)
                    return;
                _ProwlDefense = value;
                this.SETL.SLF_ += _ProwlDefense ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Prowl Defense", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Prowl Defense", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("ProwlDefense");
            }
        }
        private bool _RunOriented;
        public bool RunOriented
        {
            get { return _RunOriented; }
            set
            {
                if (_RunOriented == value)
                    return;
                _RunOriented = value;
                this.SETL.SLF_ += _RunOriented ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Run Oriented", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Run Oriented", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("RunOriented");
            }
        }
        private bool _SpecialOriented;
        public bool SpecialOriented
        {
            get { return _SpecialOriented; }
            set
            {
                if (_SpecialOriented == value)
                    return;
                _SpecialOriented = value;
                this.SETL.SLF_ += _SpecialOriented ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Special Oriented", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Special Oriented", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("SpecialOriented");
            }
        }
        private bool _Tight;
        public bool Tight
        {
            get { return _Tight; }
            set
            {
                if (_Tight == value)
                    return;
                _Tight = value;
                this.SETL.SLF_ += _Tight ?
                    SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Tight", StringComparison.OrdinalIgnoreCase)).Key :
                    -1 * SETL.SetListFlag
                    .FirstOrDefault(k => String.Equals(k.Value, "Tight", StringComparison.OrdinalIgnoreCase)).Key;
                OnPropertyChanged("Tight");
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
                    foreach (SubFormationVM subFormation in Formation.SubFormations.Where(set => set.PBST != this.PBST)) subFormation.IsVisible = !value;
                    foreach (PlayVM play in Plays) play.IsVisible = value;
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
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}