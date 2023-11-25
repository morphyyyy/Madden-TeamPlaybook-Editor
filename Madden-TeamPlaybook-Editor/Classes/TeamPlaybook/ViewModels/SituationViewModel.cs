using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.ViewModels
{
    [Serializable]
    public class SituationVM
    {
        public float Percentage { get; set; }
        public string Title { get; set; }
        public Brush ColorBrush { get; set; }
        public List<PlayVM> Plays { get; set; }

        public SituationVM()
        {
        }
    }
}