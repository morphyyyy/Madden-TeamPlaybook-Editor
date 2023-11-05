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
        public ObservableCollection<PlayerVM> Players { get; set; }
        [field: NonSerializedAttribute()]
        public ICollectionView PlayerPlayartView { get; set; }

        public string Position1name { get; set; }
        public string Position2name { get; set; }
        public string Position3name { get; set; }
        public int Position1count { get; set; }
        public int Position2count { get; set; }
        public int Position3count { get; set; }

        private bool _isExpanded { get; set; }
        private bool _isVisible { get; set; }
        private bool _isSelected { get; set; }

        public SubFormationVM()
        {

        }

        public SubFormationVM(PBST pbst, FormationVM _Formation)
        {
            IsVisible = false;
            IsExpanded = false;
            PBST = pbst;
            SETL = _Formation.Playbook.SETL.Where(set => set.setl == PBST.SETL).FirstOrDefault();
            Plays = new ObservableCollection<PlayVM>();
            Packages = new List<Package>();
            CurrentPackage = new List<SETP>();
            Alignments = new List<Alignment>();
            GetFormation(_Formation);
            GetPackage();
            CurrentPackage = BasePackage;
            GetPackages();
            GetAlignments();
            GetAlignment();
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
            PBPL dbPBPL = Formation.Playbook.PBPL.Where(play => play.rec == Play.PBPL.rec).FirstOrDefault();
            Formation.Playbook.PBPL.RemoveAt(Formation.Playbook.PBPL.IndexOf(dbPBPL));

            #endregion

            dbPBPL = Formation.Playbook.PBPL.Where(play => play.PLYL == Play.PBPL.PLYL).FirstOrDefault();
            if (dbPBPL == null)
            {
                Formation.Playbook.PLYL.RemoveAt(Formation.Playbook.PLYL.IndexOf(Formation.Playbook.PLYL.Where(play => play.rec == Play.PLYL.rec).FirstOrDefault()));

                #region PLPD/PLRD

                if (Play.PLPD != null)
                {
                    Formation.Playbook.PLPD.RemoveAt(Formation.Playbook.PLPD.IndexOf(Formation.Playbook.PLPD.Where(play => play.rec == Play.PLPD.rec).FirstOrDefault()));
                }

                if (Play.PLRD != null)
                {
                    Formation.Playbook.PLRD.RemoveAt(Formation.Playbook.PLRD.IndexOf(Formation.Playbook.PLRD.Where(play => play.rec == Play.PLRD.rec).FirstOrDefault()));
                }

                #endregion

                #region PLYS

                for (int i = Play.PLYS.Count - 1; i >= 0; i--)
                {
                    Formation.Playbook.PLYS.RemoveAt(Formation.Playbook.PLYS.IndexOf(Formation.Playbook.PLYS.Where(play => play.rec == Play.PLYS[i].rec).FirstOrDefault()));
                }

                #endregion

                #region PSAL & ARTL

                List<PSAL> psals = new List<PSAL>();
                List<Madden.TeamPlaybook.ARTL> playart = new List<Madden.TeamPlaybook.ARTL>();
                foreach (PlayerVM player in Play.Players)
                {
                    psals.AddRange(player.PSAL);
                    playart.Add(player.ARTL);
                }
                psals = psals.OrderBy(s => s.rec).Distinct().ToList();
                playart = playart.OrderBy(s => s.rec).Distinct().ToList();

                for (int p = psals.Count - 1; p >= 0; p--)
                {
                    if (!Formation.Playbook.IsUsing(psals[p]))
                    {
                        PSAL psal = Formation.Playbook.PSAL.Where(play => play.rec == psals[p].rec).FirstOrDefault();
                        Formation.Playbook.PSAL.Remove(psal);
                    }
                }

                for (int p = playart.Count - 1; p >= 0; p--)
                {
                    if (!Formation.Playbook.IsUsing(playart[p]))
                    {
                        Madden.TeamPlaybook.ARTL artl = Formation.Playbook.ARTL.Where(play => play.rec == playart[p].rec).FirstOrDefault();
                        Formation.Playbook.ARTL.Remove(artl);
                    }
                }

                #endregion

                #region Situations

                Play.Situations.OrderBy(s => s.rec);
                for (int i = Play.Situations.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        Formation.Playbook.PBAI.RemoveAt(Formation.Playbook.PBAI.IndexOf(Formation.Playbook.PBAI.Where(play => play.rec == Play.Situations[i].rec).FirstOrDefault()));
                    }
                    catch (Exception)
                    {
                    }
                }

                #endregion

                #region Audibles

                Play.Audibles.OrderBy(s => s.rec);
                for (int i = Play.Audibles.Count - 1; i >= 0; i--)
                {
                    Formation.Playbook.PBAU.RemoveAt(Formation.Playbook.PBAU.IndexOf(Formation.Playbook.PBAU.Where(play => play.rec == Play.Audibles[i].rec).FirstOrDefault()));
                }

                #endregion

                #region PBCC

                if (Play.PBCC != null)
                {
                    Play.PBCC.OrderBy(s => s.rec);
                    for (int i = 0; i < Play.PBCC.Count; i++)
                    {
                        Formation.Playbook.PBCC.RemoveAt(Formation.Playbook.PBCC.IndexOf(Formation.Playbook.PBCC.Where(play => play.rec == Play.PBCC[i].rec).FirstOrDefault()));
                    }
                }

                #endregion

                #region PLCM

                Play.PLCM.OrderBy(s => s.rec);
                for (int i = Play.PLCM.Count - 1; i >= 0; i--)
                {
                    Formation.Playbook.PLCM.RemoveAt(Formation.Playbook.PLCM.IndexOf(Formation.Playbook.PLCM.Where(play => play.rec == Play.PLCM[i].rec).FirstOrDefault()));
                }

                #endregion

                #region PPCT

                Play.PPCT.OrderBy(s => s.rec);
                for (int i = Play.PPCT.Count - 1; i >= 0; i--)
                {
                    Formation.Playbook.PPCT.RemoveAt(Formation.Playbook.PPCT.IndexOf(Formation.Playbook.PPCT.Where(play => play.rec == Play.PPCT[i].rec).FirstOrDefault()));
                }

                #endregion

                #region SDEF

                if (Play.SDEF != null)
                {
                    Play.SDEF.OrderBy(s => s.rec);
                    for (int i = 0; i < Play.SDEF.Count; i++)
                    {
                        Formation.Playbook.SDEF.RemoveAt(Formation.Playbook.SDEF.IndexOf(Formation.Playbook.SDEF.Where(play => play.rec == Play.SDEF[i].rec).FirstOrDefault()));
                    }
                }

                #endregion

                #region SRFT

                if (Play.SRFT != null)
                {
                    Play.SRFT.OrderBy(s => s.rec);
                    for (int i = 0; i < Play.SRFT.Count; i++)
                    {
                        Formation.Playbook.SRFT.RemoveAt(Formation.Playbook.SRFT.IndexOf(Formation.Playbook.SRFT.Where(play => play.rec == Play.SRFT[i].rec).FirstOrDefault()));
                    }
                }

                #endregion
            }

            #region Hidden Sub-Formation Check

            try
            {
                //Check if Play has a hidden Sub-Formation and remove it
                SETL SETL = Formation.Playbook.SETL.Where(set => set.setl == Play.PLYL.SETL).FirstOrDefault();
                if (Formation.Playbook.PBST.Where(set => set.SETL == SETL.setl).FirstOrDefault() == null)
                {
                    Formation.RemoveHiddenSubFormation(Formation.Playbook.SETL.Where(set => set.setl == Play.PLYL.SETL).FirstOrDefault());
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

            if (Formation.Playbook.PLYL.Where(play => play.plyl == Play.PLYL.plyl).FirstOrDefault() != null)
            {
                Play.PLYL.plyl = TeamPlaybook.NextAvailableID((from plyl in Formation.Playbook.PLYL select plyl.plyl).ToList());
                if (Play.PLPD != null)
                {
                    Play.PLPD.PLYL = Play.PLYL.plyl;
                }
                if (Play.PLRD != null)
                {
                    Play.PLRD.PLYL = Play.PLYL.plyl;
                }
                foreach (PLYS play in Play.PLYS)
                {
                    play.PLYL = Play.PLYL.plyl;
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

            if (Formation.Playbook.PBPL.Where(play => play.pbpl == Play.PBPL.pbpl).FirstOrDefault() != null)
            {
                Play.PBPL.pbpl = TeamPlaybook.NextAvailableID((from pbpl in Formation.Playbook.PBPL select pbpl.pbpl).ToList());
                Play.PBPL.PLYL = Play.PLYL.plyl;
                foreach (PBAI situation in Play.Situations)
                {
                    situation.PBPL = Play.PBPL.pbpl;
                }
                foreach (PBAU audible in Play.Audibles)
                {
                    audible.PBPL = Play.PBPL.pbpl;
                }
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
                    int nextAvailablePSALID = TeamPlaybook.NextAvailableID((from _psal in Formation.Playbook.PSAL select _psal.psal).ToList());
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
                    int nextAvailableARTLID = TeamPlaybook.NextAvailableID((from _artl in Formation.Playbook.ARTL select _artl.artl).ToList());
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

            SETL PBhiddenSETL = Formation.Playbook.SETL.Where(subFormation => subFormation.setl == Play.PLYL.SETL).FirstOrDefault();
            SETL PlayhiddenSETL = Play.SubFormation.Formation.Playbook.SETL.Where(subFormation => subFormation.setl == Play.PLYL.SETL).FirstOrDefault();
            FORM hiddenFORM = Formation.Playbook.FORM.Where(formation => formation.form == PlayhiddenSETL.FORM).FirstOrDefault();

            //Check for Hidden Sub-Formation
            if (PBhiddenSETL == null)
            {
                Play.AddHiddenSubFormation(Play, Formation.Playbook);
            }

            //Check for Hidden Formation
            if (hiddenFORM == null)
            {
                Formation.Playbook.FORM.Add(Play.SubFormation.Formation.Playbook.FORM.Where(formation => formation.form == PlayhiddenSETL.FORM).FirstOrDefault());
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

            if (Formation.Playbook.PLYL.Where(play => play.plyl == Play.PBPL[0].PLYL).FirstOrDefault() != null)
            {
                Play.PBPL[0].PLYL = TeamPlaybook.NextAvailableID((from plyl in Formation.Playbook.PLYL select plyl.plyl).ToList());
                if (Play.PLPD.Count > 0)
                {
                    Play.PLPD[0].PLYL = Play.PBPL[0].PLYL;
                }
                if (Play.PLRD.Count > 0)
                {
                    Play.PLRD[0].PLYL = Play.PBPL[0].PLYL;
                }
                foreach (MaddenCustomPlaybookEditor.PLYS play in Play.PLYS)
                {
                    play.PLYL = Play.PBPL[0].PLYL;
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

            int pbplID = TeamPlaybook.NextAvailableID((from pbpl in Formation.Playbook.PBPL select pbpl.pbpl).ToList());

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
                    int nextAvailablePSALID = TeamPlaybook.NextAvailableID((from psal in Formation.Playbook.PSAL select psal.psal).ToList());
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
                ARTL ARTL = Formation.Playbook.ARTL.Where(artl => artl.artl == Play.ARTL[i].artl).FirstOrDefault();

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
                    int nextAvailableARTLID = TeamPlaybook.NextAvailableID((from artl in Formation.Playbook.ARTL select artl.artl).ToList());
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

        public void AddPackage(Package package)
        {
            Formation.Playbook.SPKF.Add(package.SPKF);
            Formation.Playbook.SPKF[Formation.Playbook.SPKF.Count - 1].rec = Formation.Playbook.SPKF.Count - 1;
            foreach (SPKG set in package.SPKG)
            {
                Formation.Playbook.SPKG.Add(set);
                Formation.Playbook.SPKG[Formation.Playbook.SPKG.Count - 1].rec = Formation.Playbook.SPKG.Count - 1;
            }
        }

        public void GetPackage()
        {
            BasePackage = new List<SETP>();
            foreach (SETP player in Formation.Playbook.SETP.Where(player => player.SETL == PBST.SETL))
                BasePackage.Add(player);
            BasePackage.OrderBy(player => player.poso);
        }

        public void GetPackage(Package Package)
        {
            CurrentPackage = new List<SETP>();
            foreach (SETP player in Formation.Playbook.SETP.Where(player => player.SETL == PBST.SETL))
            {
                CurrentPackage.Add(player);
                player.DPos = Package.SPKG.Where(poso => poso.poso == player.poso).FirstOrDefault().DPos;
                player.EPos = Package.SPKG.Where(poso => poso.poso == player.poso).FirstOrDefault().EPos;
            }
            CurrentPackage.OrderBy(player => player.poso);
        }

        public void GetPackages()
        {
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

        public void GetAlignment()
        {
            CurrentAlignment = Alignments.Where(poso => poso.SGFM.name == "Norm").FirstOrDefault();
        }

        public void GetAlignment(Alignment alignment)
        {
            CurrentAlignment = Alignments.Where(poso => poso.SGFM.name == "Norm").FirstOrDefault();
            for (int i = alignment.SETG.Count; i == 0; i--)
            {
                int index = CurrentAlignment.SETG.FindIndex(player => player.SETP == alignment.SETG[i].SETP);
                CurrentAlignment.SETG.RemoveAt(index);
                CurrentAlignment.SETG.Insert(index, alignment.SETG[i]);
            }
        }

        public void GetAlignments()
        {
            Alignments.Clear();
            List<SGFM> sets = Formation.Playbook.SGFM.Where(alignment => alignment.SETL == PBST.SETL).ToList();

            foreach (SGFM set in sets)
            {
                List<SETG> SETG = new List<SETG>();
                foreach (SETG alignment in Formation.Playbook.SETG.Where(alignment => alignment.SGF_ == set.SGF_))
                {
                    SETG.Add(alignment);
                }
                SETG.OrderBy(alignment => alignment.setg);
                Alignments.Add(new Alignment(set, SETG));
                if (set.name == "Norm")
                {
                    if (SETG.Count < 11)
                    {
                        MessageBox.Show("SETG Missing for " + PBST.name);
                    }
                    GetAlignment(Alignments[Alignments.Count - 1]);
                }
            }
            Alignments.OrderBy(alignment => alignment.SGFM.SGF_);
        }

        public void GetPlayers()
        {
            Players = new ObservableCollection<PlayerVM>();
            if (CurrentAlignment != null)
            {
                foreach (SETG alignment in CurrentAlignment.SETG)
                {
                    int poso = CurrentPackage.Where(_poso => _poso.setp == alignment.SETP).FirstOrDefault().poso;
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
                    foreach (SubFormationVM subFormation in Formation.SubFormations.Where(set => set.PBST != this.PBST)) subFormation.IsVisible = !value;
                    foreach (PlayVM play in Plays) play.IsVisible = value;
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