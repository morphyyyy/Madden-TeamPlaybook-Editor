using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Madden.TeamPlaybook;

namespace MaddenCustomPlaybookEditor.ViewModels
{
    [Serializable]
    public class SubFormationVM : INotifyPropertyChanged
    {
        #region Package

        [Serializable]
        public class Package : INotifyPropertyChanged
        {
            public Madden.CustomPlaybook.SPKF SPKF { get; set; }
            public List<Madden.CustomPlaybook.SPKG> SPKG { get; set; }

            public Package(Madden.CustomPlaybook.SPKF _SPKF, List<Madden.CustomPlaybook.SPKG> _SPKG)
            {
                SPKF = _SPKF;
                SPKG = _SPKG;
            }

            public override string ToString()
            {
                string subs = SPKF.name + "\n";
                foreach (Madden.CustomPlaybook.SPKG sub in SPKG) subs += "    " + sub.ToString() + "\n";
                subs.Substring(subs.Length - 2, 2);
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
            public Madden.CustomPlaybook.SGFF SGFM { get; set; }
            public List<Madden.CustomPlaybook.SETG> SETG { get; set; }

            public Alignment(Madden.CustomPlaybook.SGFF _SGFM, List<Madden.CustomPlaybook.SETG> _SETG)
            {
                SGFM = _SGFM;
                SETG = _SETG;
            }

            public override string ToString()
            {
                string alignments = SGFM.name + "\n";
                foreach (Madden.CustomPlaybook.SETG alignment in SETG) alignments += "    " + alignment.ToString() + "\n";
                alignments.Substring(alignments.Length - 2, 2);
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
            string SubFormationVM_string = SETL + "\n";
            foreach (Madden.TeamPlaybook.SETP setp in CurrentPackage) SubFormationVM_string += setp.ToString() + "\n";
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

        public List<Madden.TeamPlaybook.PBST> PBST { get; set; }
        public Madden.TeamPlaybook.SETL SETL { get; set; }

        public List<Package> Packages { get; set; }
        public List<Madden.TeamPlaybook.SETP> BasePackage { get; set; }
        public List<Madden.TeamPlaybook.SETP> CurrentPackage { get; set; }

        public List<Alignment> Alignments { get; set; }
        public Alignment CurrentAlignment { get; set; }

        public FormationVM Formation { get; set; }
        public ObservableCollection<PlayVM> Plays { get; set; }

        public string Position1name { get; set; }
        public string Position2name { get; set; }
        public string Position3name { get; set; }
        public int Position1count { get; set; }
        public int Position2count { get; set; }
        public int Position3count { get; set; }

        private bool _isExpanded { get; set; }
        private bool _isVisible { get; set; }
        private bool _isSelected { get; set; }

        public SubFormationVM(Madden.CustomPlaybook.SETL _SETL, FormationVM _Formation)
        {
            IsVisible = false;
            IsExpanded = false;
            SETL = new Madden.TeamPlaybook.SETL
            {
                rec = _SETL.rec,
                FORM = _SETL.FORM,
                CLAS = _SETL.CLAS,
                MOTN = _SETL.MOTN,
                name = _SETL.name,
                poso = _SETL.poso,
                setl = _SETL.setl,
                SETT = _SETL.SETT,
                SITT = _SETL.SITT,
                SLF_ = _SETL.SLF_
            };
            PBST = new List<Madden.TeamPlaybook.PBST>();
            _Formation.Playbook.STID.Where(set => set.SETL == _SETL.setl).ToList().ForEach(set => PBST.Add(new Madden.TeamPlaybook.PBST
            {
                rec = set.rec,
                name = SETL.name,
                PBFM = set.PBFM,
                pbst = set.PBST,
                SETL = set.SETL,
                SPF_ = set.SPF_
            }));
            Plays = new ObservableCollection<PlayVM>();
            Packages = new List<Package>();
            CurrentPackage = new List<Madden.TeamPlaybook.SETP>();
            Alignments = new List<Alignment>();
            GetFormation(_Formation);
            GetPackage();
            CurrentPackage = BasePackage;
            GetPackages();
            GetAlignments();
            GetAlignment();
            GetSubCount();
            GetPlays();
        }

        #endregion

        #region Get Plays

        public void GetPlays()
        {
            if (Plays.Count > 0) Plays.Clear();
            foreach (Madden.CustomPlaybook.PBPL play in Formation.Playbook.PBPL.Where(set => set.SETL == SETL.setl))
            {
                Plays.Add(new PlayVM(play, this));
            }
            Plays = new ObservableCollection<PlayVM>(Plays.OrderBy(play => play.PBPL.name));
        }

        #endregion

        #region Get Sub Count

        public void GetSubCount()
        {
            Position1count = Position2count = Position3count = 0;
            foreach (Madden.TeamPlaybook.SETP position in CurrentPackage.Where(position => position.SETL == SETL.setl))
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

        #region Packages

        public void GetPackage()
        {
            BasePackage = new List<Madden.TeamPlaybook.SETP>();
            foreach (Madden.CustomPlaybook.SETP player in Formation.Playbook.SETP.Where(player => player.SETL == SETL.setl))
                BasePackage.Add(new Madden.TeamPlaybook.SETP
                {
                    rec = player.rec,
                    arti = player.arti,
                    artx = player.artx,
                    arty = player.arty,
                    DPos = player.DPos,
                    EPos = player.EPos,
                    flas = player.flas,
                    fmtx = player.fmtx,
                    fmty = player.fmty,
                    poso = player.poso,
                    SETL = player.SETL,
                    setp = player.setp,
                    SGT_ = player.SGT_,
                    tabo = player.tabo
                });
            BasePackage = BasePackage.OrderBy(player => player.poso).ToList();
        }

        public void GetPackage(Package Package)
        {
            CurrentPackage = new List<Madden.TeamPlaybook.SETP>();
            foreach (Madden.CustomPlaybook.SETP player in Formation.Playbook.SETP.Where(player => player.SETL == SETL.setl))
            {
                CurrentPackage.Add(new Madden.TeamPlaybook.SETP
                {
                    rec = player.rec,
                    arti = player.arti,
                    artx = player.artx,
                    arty = player.arty,
                    DPos = player.DPos,
                    EPos = player.EPos,
                    flas = player.flas,
                    fmtx = player.fmtx,
                    fmty = player.fmty,
                    poso = player.poso,
                    SETL = player.SETL,
                    setp = player.setp,
                    SGT_ = player.SGT_,
                    tabo = player.tabo
                });
                player.DPos = Package.SPKG.FirstOrDefault(poso => poso.poso == player.poso).DPos;
                player.EPos = Package.SPKG.FirstOrDefault(poso => poso.poso == player.poso).EPos;
            }
            CurrentPackage = CurrentPackage.OrderBy(player => player.poso).ToList();
        }

        public void GetPackages()
        {
            Packages.Clear();
            foreach (Madden.CustomPlaybook.SPKF package in Formation.Playbook.SPKF.Where(package => package.SETL == SETL.setl))
            {
                List<Madden.CustomPlaybook.SPKG> SPKG = new List<Madden.CustomPlaybook.SPKG>();
                foreach (Madden.CustomPlaybook.SPKG sub in Formation.Playbook.SPKG.Where(sub => sub.SPF_ == package.SPF_))
                    SPKG.Add(new Madden.CustomPlaybook.SPKG
                    {
                        rec = sub.rec,
                        DPos = sub.DPos,
                        EPos = sub.EPos,
                        poso = sub.poso,
                        SPF_ = sub.SPF_
                    });
                SPKG = SPKG.OrderBy(sub => sub.poso).ToList();
                Packages.Add(new Package(package, SPKG));
            }
            Packages = Packages.OrderBy(package => package.SPKF.SPF_).ToList();
        }

        #endregion

        #region Alignments

        public void GetAlignment()
        {
            CurrentAlignment = Alignments.FirstOrDefault(poso => poso.SGFM.name == "Norm");
        }

        public void GetAlignment(Alignment alignment)
        {
            CurrentAlignment = Alignments.FirstOrDefault(poso => poso.SGFM.name == "Norm");
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
            List<Madden.CustomPlaybook.SGFF> sets = Formation.Playbook.SGFF.Where(alignment => alignment.SETL == SETL.setl).ToList();

            foreach (Madden.CustomPlaybook.SGFF set in sets)
            {
                List<Madden.CustomPlaybook.SETG> SETG = new List<Madden.CustomPlaybook.SETG>();
                foreach (Madden.CustomPlaybook.SETG alignment in Formation.Playbook.SETG.Where(alignment => alignment.SGF_ == set.SGF_))
                {
                    SETG.Add(alignment);
                }
                SETG = SETG.OrderBy(player => player.SETP).ThenBy(alignment => alignment.SGF_).ToList();
                Alignments.Add(new Alignment(set, SETG));
                if (set.name == "Norm")
                {
                    if (SETG.Count < 11)
                    {
                        MessageBox.Show("SETG Missing for " + SETL.name);
                    }
                    GetAlignment(Alignments[Alignments.Count - 1]);
                }
            }
            Alignments = Alignments.OrderBy(alignment => alignment.SGFM.SGF_).ToList();
        }

        #endregion

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
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