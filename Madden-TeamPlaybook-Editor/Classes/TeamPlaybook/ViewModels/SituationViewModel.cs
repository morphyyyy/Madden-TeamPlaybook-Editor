using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class SituationVM : INotifyPropertyChanged
    {
        public override string ToString()
        {
            string
            GameplanVM_string = PBAI + "\n";
            foreach (PlayVM play in Plays) GameplanVM_string += play.ToString() + "\n";
            return
                GameplanVM_string;
        }

        public TeamPlaybook Playbook { get; set; }
        public List<PBAI> PBAI { get; set; }
        public string Situation { get; set; }
        public ObservableCollection<PlayVM> Plays { get; set; }

        private bool _isExpanded { get; set; }
        private bool _isVisible { get; set; }

        public SituationVM(List<PBAI> pbai, TeamPlaybook playbook)
        {
            IsVisible = true;
            Playbook = playbook;
            PBAI = pbai;
            Situation = Playbook.Situations[PBAI[0].AIGR];
            Plays = new ObservableCollection<PlayVM>();
            GetPlays();
        }

        public void GetPlays()
        {
            foreach (PBAI pbai in PBAI)
            {
                foreach (FormationVM formation in Playbook.Formations)
                {
                    foreach (SubFormationVM subFormation in formation.SubFormations)
                    {
                        foreach (PlayVM play in subFormation.Plays)
                        {
                            if (play.PBPL.pbpl == pbai.PBPL)
                            {
                                Plays.Add(play);
                            }
                        }
                    }
                }
            }
        }

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