﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Madden.TeamPlaybook;
using MaddenCustomPlaybookEditor.ViewModels;
using MaddenTeamPlaybookEditor.User_Controls;
using static MaddenTeamPlaybookEditor.ViewModels.SubFormationVM;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class FormationVM : INotifyPropertyChanged
    {
        #region Instantiation

        public override string ToString()
        {
            return
                PBFM.name + "\n" + PBFM + "\n" + FORM;
        }

        public PBFM PBFM { get; set; }
        public FORM FORM { get; set; }

        public TeamPlaybook Playbook { get; set; }
        public ObservableCollection<SubFormationVM> SubFormations { get; set; }

        private bool _isExpanded { get; set; }
        private bool _isVisible { get; set; } = true;
        private bool _isSelected { get; set; }

        public FormationVM()
        {

        }

        public FormationVM(PBFM pbfm, FORM form, TeamPlaybook _Playbook, TeamPlaybook _PlaybookSource = null)
        {
            IsVisible = true;
            IsExpanded = false;
            Playbook = _Playbook;
            PBFM = pbfm;
            FORM = form;
            SubFormations = new ObservableCollection<SubFormationVM>();
            GetSubFormations();
        }

        #endregion

        #region Remove Sub Formation

        public void RemoveSubFormation(SubFormationVM SubFormation, bool dbOnly = false)
        {
            #region Remove Plays

            for (int i = SubFormation.Plays.Count - 1; i >= 0; i--)
            {
                SubFormation.RemovePlay(SubFormation.Plays[i]);
            }

            #endregion

            #region Playbook

            for (int i = SubFormation.PBST.ord_; i < SubFormations.Count; i++)
            {
                Playbook.PBST[SubFormations[i].PBST.rec].ord_--;
            }
            Playbook.PBST.Remove(SubFormation.PBST);

            #endregion

            #region Database

            Playbook.SETL.Remove(SubFormation.SETL);

            #endregion

            #region Packages

            for (int i = SubFormation.BasePackage.Count - 1; i >= 0; i--)
            {
                Playbook.SETP.Remove(SubFormation.BasePackage[i]);
            }

            foreach (Package package in SubFormation.Packages)
            {
                Playbook.SPKF.Remove(package.SPKF);
                foreach (SPKG spkg in package.SPKG)
                {
                    Playbook.SPKG.Remove(spkg);
                }
            }

            #endregion

            #region Alignments

            foreach (Alignment alignment in SubFormation.Alignments)
            {
                Playbook.SGFM.Remove(alignment.SGFM);
                foreach (SETG setg in alignment.SETG)
                {
                    Playbook.SETG.Remove(setg);
                }
            }

            #endregion

            SubFormations.Remove(SubFormation);
        }

        public void RemoveHiddenSubFormation(SETL SubFormation)
        {
            Playbook.SETL.RemoveAt(Playbook.SETL.IndexOf(Playbook.SETL.FirstOrDefault(set => set.rec == SubFormation.rec)));

            #region Package

            List<SETP> SETP = Playbook.SETP.Where(set => set.SETL == SubFormation.setl).Cast<SETP>().OrderBy(s => s.rec).ToList();
            for (int i = SETP.Count - 1; i >= 0; i--)
            {
                Playbook.SETP.RemoveAt(Playbook.SETP.IndexOf(SETP[i]));
            }

            List<SPKF> SPKF = Playbook.SPKF.Where(set => set.SETL == SubFormation.setl).Cast<SPKF>().OrderBy(s => s.rec).ToList();
            List<SPKG> SPKG = new List<SPKG>();
            foreach (SPKF set in SPKF)
            {
                SPKG.AddRange(Playbook.SPKG.Where(sub => sub.SPF_ == set.SPF_).Cast<SPKG>().ToList());
            }
            SPKG = SPKG.OrderBy(s => s.rec).Cast<SPKG>().ToList();

            for (int i = SPKF.Count - 1; i >= 0; i--)
            {
                Playbook.SPKF.RemoveAt(Playbook.SPKF.IndexOf(SPKF[i]));
            }

            for (int i = SPKG.Count - 1; i >= 0; i--)
            {
                Playbook.SPKG.RemoveAt(Playbook.SPKG.IndexOf(SPKG[i]));
            }

            #endregion

            #region Alignment

            foreach (SGFM alignment in Playbook.SGFM.Where(alignment => alignment.SETL == SubFormation.setl))
            {
                Playbook.SGFM.Remove(alignment);
                foreach (SETG setg in Playbook.SETG.Where(a => a.SGF_ == alignment.SGF_).ToList())
                {
                    Playbook.SETG.Remove(setg);
                }
            }

            #endregion

            #region Hidden Formation Check

            try
            {
                //Check if Sub-Formation has a hidden Formation and remove it
                FORM FORM = Playbook.FORM.FirstOrDefault(form => form.form == SubFormation.FORM);
                Playbook.FORM.Remove(FORM);
            }
            catch
            {
            }

            #endregion
        }

        #endregion

        #region Add/Get SubFormation

        public void AddSubFormation(SubFormationVM SubFormation, int ord = 0, bool dbOnly = false)
        {
            if (Playbook.PBST.FirstOrDefault(set => set.SETL == SubFormation.PBST.SETL) == null)
            {
                #region Order

                if (ord != 0)
                {
                    SubFormation.PBST.ord_ = ord;
                    for (int i = ord - 1; i < SubFormations.Count; i++)
                    {
                        SubFormations[i].PBST.ord_ += 1;
                    }
                }
                else
                {
                    SubFormation.PBST.ord_ = SubFormations.Count + 1;
                }

                #endregion

                #region PBST

                SubFormation.PBST.PBFM = PBFM.pbfm;
                Playbook.PBST.Add(SubFormation.PBST);

                #endregion

                #region SETL

                if (SubFormation.SETL != null)
                {
                    Playbook.SETL.Add(SubFormation.SETL);
                    Playbook.SETL[Playbook.SETL.Count - 1].FORM = FORM.form;
                }

                #endregion

                #region Packages

                foreach (MaddenTeamPlaybookEditor.ViewModels.SubFormationVM.Package package in SubFormation.Packages)
                {
                    Playbook.SPKF.Add(package.SPKF);
                    foreach (SPKG set in package.SPKG)
                    {
                        Playbook.SPKG.Add(set);
                    }
                }
                foreach (SETP sub in SubFormation.BasePackage)
                {
                    Playbook.SETP.Add(sub);
                }

                #endregion

                #region Alignments

                foreach (MaddenTeamPlaybookEditor.ViewModels.SubFormationVM.Alignment alignment in SubFormation.Alignments)
                {
                    Playbook.SGFM.Add(alignment.SGFM);
                    foreach (SETG poso in alignment.SETG)
                    {
                        Playbook.SETG.Add(poso);
                    }
                }

                #endregion

                #region DB Only

                if (!dbOnly)
                {
                    if (ord == 0)
                    {
                        SubFormations.Add(new SubFormationVM(Playbook.PBST[Playbook.PBST.Count - 1], this));
                        SubFormations[SubFormations.Count - 1].IsVisible = true;
                    }
                    else
                    {
                        SubFormations.Insert(ord - 1, new SubFormationVM(Playbook.PBST[Playbook.PBST.Count - 1], this));
                        SubFormations[ord - 1].IsVisible = true;
                    }
                }

                #endregion

                #region Add Plays

                foreach (PlayVM play in SubFormation.Plays)
                {
                    FormationVM Formation = Playbook.Formations.FirstOrDefault(formation => formation.PBFM.pbfm == SubFormation.PBST.PBFM);
                    SubFormationVM newSubFormation = Formation.SubFormations.FirstOrDefault(subFormation => subFormation.PBST.pbst == SubFormation.PBST.pbst);
                    newSubFormation.AddPlay(play, dbOnly: false);
                }

                #endregion
            }
            else
            {
                MessageBox.Show(SubFormation.PBST.name, "Sub-Formation Already Exists!");
            }
        }

        public void AddSubFormation(MaddenCustomPlaybookEditor.CustomPlaybookSubFormation SubFormation, int ord = 0)
        {
            if (Playbook.PBST.FirstOrDefault(set => set.SETL == SubFormation.PGPL[0].SETL) == null)
            {
                #region PBST

                if (Playbook.PBST.FirstOrDefault(set => set.pbst == SubFormation.PGPL[0].PBST) != null)
                {
                    SubFormation.PGPL[0].PBST = Playbook.PBST.Select(pbst => pbst.pbst).Max() + 1;
                }

                if (Playbook.SETL.FirstOrDefault(set => set.setl == SubFormation.PGPL[0].SETL) != null)
                {
                    SubFormation.PGPL[0].SETL = Playbook.SETL.Select(setl => setl.setl).Max() + 1;
                }

                if (ord == 0)
                {
                    ord = SubFormations.Count + 1;
                }

                PBST CPBpbst = new PBST
                {
                    rec = Playbook.PBST.Count,
                    SETL = SubFormation.PGPL[0].SETL,
                    PBFM = PBFM.pbfm,
                    pbst = SubFormation.PGPL[0].PBST,
                    SPF_ = 0,
                    ord_ = ord,
                    name = SubFormation.SETL[0].name
                };

                Playbook.PBST.Add(CPBpbst);

                #endregion

                #region SETL

                Playbook.SETL.Add(new SETL
                {
                    rec = Playbook.SETL.Count,
                    setl = CPBpbst.SETL,
                    FORM = SubFormation.SETL[0].FORM,
                    MOTN = SubFormation.SETL[0].MOTN,
                    CLAS = SubFormation.SETL[0].CLAS,
                    SETT = SubFormation.SETL[0].SETT,
                    SITT = SubFormation.SETL[0].SITT,
                    SLF_ = SubFormation.SETL[0].SLF_,
                    name = SubFormation.SETL[0].name,
                    poso = SubFormation.SETL[0].poso
                });

                #endregion

                #region SPKF

                if (Playbook.PBST.FirstOrDefault(set => set.pbst == SubFormation.PGPL[0].PBST) != null)
                {
                    SubFormation.PGPL[0].PBST = Playbook.PBST.Select(pbst => pbst.pbst).Max() + 1;
                }

                for (int i = 0; i < SubFormation.SPKF.Count; i++)
                {
                    Playbook.SPKF.Add(new SPKF
                    {
                        rec = Playbook.SETL.Count,
                        SETL = CPBpbst.SETL,
                        SPF_ = SubFormation.SPKF[i].SPF_,
                        name = SubFormation.SPKF[i].name
                    });
                }

                #endregion

                #region SPKG

                for (int i = 0; i < SubFormation.SPKG.Count; i++)
                {
                    Playbook.SPKG.Add(new SPKG
                    {
                        rec = Playbook.SPKG.Count,
                        SPF_ = SubFormation.SPKG[i].SPF_,
                        poso = SubFormation.SPKG[i].poso,
                        DPos = SubFormation.SPKG[i].DPos,
                        EPos = SubFormation.SPKG[i].EPos
                    });
                }

                #endregion

                #region SGFM

                for (int i = 0; i < SubFormation.SGFF.Count; i++)
                {
                    Playbook.SGFM.Add(new SGFM
                    {
                        rec = Playbook.SGFM.Count,
                        SETL = CPBpbst.SETL,
                        SGF_ = SubFormation.SGFF[i].SGF_,
                        name = SubFormation.SGFF[i].name,
                        dflt = SubFormation.SGFF[i].dflt
                    });
                }

                #endregion

                #region SETP

                for (int i = 0; i < SubFormation.SETP.Count; i++)
                {
                    Playbook.SETP.Add(new SETP
                    {
                        rec = Playbook.SETP.Count,
                        SETL = CPBpbst.SETL,
                        setp = SubFormation.SETP[i].setp,
                        SGT_ = SubFormation.SETP[i].SGT_,
                        arti = SubFormation.SETP[i].arti,
                        tabo = SubFormation.SETP[i].tabo,
                        poso = SubFormation.SETP[i].poso,
                        flas = SubFormation.SETP[i].flas,
                        DPos = SubFormation.SETP[i].DPos,
                        EPos = SubFormation.SETP[i].EPos,
                        fmtx = SubFormation.SETP[i].fmtx,
                        artx = SubFormation.SETP[i].artx,
                        fmty = SubFormation.SETP[i].fmty,
                        arty = SubFormation.SETP[i].arty,
                    });
                }

                #endregion

                #region SETG

                for (int i = 0; i < SubFormation.SETG.Count; i++)
                {
                    int setgID;

                    if (Playbook.SETG.Count > 0)
                    {
                        setgID = Playbook.SETG.Select(setg => setg.setg).Max() + 1;
                    }
                    else
                    {
                        setgID = 1;
                    }

                    Playbook.SETG.Add(new SETG
                    {
                        rec = Playbook.SETG.Count,
                        setg = setgID,
                        SETP = SubFormation.SETG[i].SETP,
                        SGF_ = SubFormation.SETG[i].SGF_,
                        SF__ = SubFormation.SETG[i].SF__,
                        x___ = SubFormation.SETG[i].x___,
                        y___ = SubFormation.SETG[i].y___,
                        fx__ = SubFormation.SETG[i].fx__,
                        fy__ = SubFormation.SETG[i].fy__,
                        anm_ = SubFormation.SETG[i].anm_,
                        dir_ = SubFormation.SETG[i].dir_,
                        fanm = SubFormation.SETG[i].fanm,
                        fdir = SubFormation.SETG[i].fdir,
                    });
                }

                #endregion

                #region Add SubFormation

                SubFormationVM subFormation = new SubFormationVM(CPBpbst, this);

                foreach (MaddenCustomPlaybookEditor.CustomPlaybookPLAY play in SubFormation.Plays)
                {
                    subFormation.AddPlay(play);
                }

                if (ord <= SubFormations.Count && ord != 0)
                {
                    for (int i = ord - 1; i < SubFormations.Count; i++)
                    {
                        SubFormations[i].PBST.ord_ += 1;
                    }

                    SubFormations.Insert(ord - 1, subFormation);
                }
                else
                {
                    SubFormations.Add(subFormation);
                }
                SubFormations[ord - 1].IsVisible = IsExpanded;
                SubFormations[ord - 1].IsExpanded = false;

                #endregion
            }
            else
            {
                MessageBox.Show(SubFormation.SETL[0].name, "Sub-Formation Already Exists!");
            }
        }

        public void GetSubFormations()
        {
            if (SubFormations.Count > 0) SubFormations.Clear();
            foreach (PBST subFormation in Playbook.PBST.Where(form => form.PBFM == PBFM.pbfm))
            {
                SubFormations.Add(new SubFormationVM(subFormation, this));
            }
            SubFormations = new ObservableCollection<SubFormationVM>(SubFormations.OrderBy(subFormation => subFormation.PBST.ord_));
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
                    foreach (FormationVM formation in Playbook.Formations.Where(form => form.PBFM != this.PBFM)) formation.IsVisible = !value;
                    foreach (SubFormationVM subFormation in SubFormations) subFormation.IsVisible = value;
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