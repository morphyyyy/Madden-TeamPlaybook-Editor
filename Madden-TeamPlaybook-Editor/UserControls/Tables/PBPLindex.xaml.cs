using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Madden.TeamPlaybook;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PBPLindex : UserControl
    {
        public PBPLindex()
        {
            InitializeComponent();
        }

        public static DependencyProperty PBPLProperty = DependencyProperty.Register("PBPL", typeof(PBPL), typeof(PBPLindex));

        public PBPL PBPL
        {
            get 
            { 
                return GetValue(PBPLProperty) as PBPL; 
            }
            set 
            {
                SetValue(PBPLProperty, value);
            }
        }

        public List<PBPL> PBPLlist
        {
            get 
            {
                List<PBPL> _PBPLlist = new List<PBPL>();
                if (PBPL == null) return null;
                _PBPLlist.Add(PBPL);
                return _PBPLlist; 
            }
            set 
            {
                List<PBPL> _PBPLlist = new List<PBPL>();
                if (PBPL == null) PBPLlist = null;
                _PBPLlist.Add(PBPL);
                PBPLlist = _PBPLlist;
            }
        }
    }
}
