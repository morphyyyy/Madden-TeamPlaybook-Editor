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
    public class FormationVM : INotifyPropertyChanged
    {
        #region Instantiation

        public override string ToString()
        {
            return
                FORM + "\n";
        }

        public List<Madden.CustomPlaybook.CPFM> CPFM { get; set; }
        public List<Madden.TeamPlaybook.PBFM> PBFM { get; set; }
        public Madden.TeamPlaybook.FORM FORM { get; set; }

        public CustomPlaybook Playbook { get; set; }
        public ObservableCollection<SubFormationVM> SubFormations { get; set; }

        private bool _isExpanded { get; set; }
        private bool _isVisible { get; set; }
        private bool _isSelected { get; set; }

        public FormationVM(Madden.CustomPlaybook.CPFM cpfm, CustomPlaybook _Playbook, CustomPlaybook _PlaybookSource = null)
        {
            IsVisible = true;
            IsExpanded = false;
            Playbook = _Playbook;
            FORM = new FORM
            {
                rec = cpfm.rec,
                form = cpfm.FORM,
                FTYP = cpfm.FTYP,
                name = cpfm.name
            };
            List<Madden.CustomPlaybook.STID> _STID = new List<Madden.CustomPlaybook.STID>();
            List<Madden.CustomPlaybook.SETL> _SETL = Playbook.SETL.Where(set => set.FORM == cpfm.FORM).ToList();
            _SETL.ForEach(set => _STID.AddRange(Playbook.STID.Where(id => id.SETL == set.setl)));
            PBFM = new List<Madden.TeamPlaybook.PBFM>();
            foreach (Madden.CustomPlaybook.STID id in _STID)
            {
                foreach (Madden.CustomPlaybook.PBFM pbfm in Playbook.PBFM.Where(set => set.pbfm == id.PBFM))
                {
                    PBFM.Add(new Madden.TeamPlaybook.PBFM
                    {
                        rec = pbfm.rec,
                        FAU1 = pbfm.FAU1,
                        FAU2 = pbfm.FAU2,
                        FAU3 = pbfm.FAU3,
                        FAU4 = pbfm.FAU4,
                        FTYP = pbfm.FTYP,
                        grid = 0,
                        name = pbfm.name,
                        pbfm = pbfm.pbfm
                    });
                }
            }
            SubFormations = new ObservableCollection<SubFormationVM>();
            GetSubFormations();
        }

        #endregion

        #region Add/Get SubFormation

        public void GetSubFormations()
        {
            if (SubFormations.Count > 0) SubFormations.Clear();
            foreach (Madden.CustomPlaybook.SETL subFormation in Playbook.SETL.Where(form => form.FORM == FORM.form).ToList<Madden.CustomPlaybook.SETL>())
            {
                SubFormations.Add(new SubFormationVM(subFormation, this));
            }
            SubFormations = new ObservableCollection<SubFormationVM>(SubFormations.OrderBy(subFormation => subFormation.SETL.name));
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