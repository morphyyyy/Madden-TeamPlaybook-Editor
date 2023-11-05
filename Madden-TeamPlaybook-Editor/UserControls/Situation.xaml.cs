using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Situation : UserControl
    {
        public Situation()
        {
            InitializeComponent();
        }

        public static DependencyProperty SituationProperty = DependencyProperty.Register("situation", typeof(SituationVM), typeof(Situation));

        public SituationVM situation
        {
            get
            {
                return GetValue(SituationProperty) as SituationVM;
            }
            set
            {
                SetValue(SituationProperty, value);
            }
        }
    }
}
