using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Formation : UserControl
    {
        public Formation()
        {
            InitializeComponent();
        }

        public static DependencyProperty FormationProperty = DependencyProperty.Register("formation", typeof(FormationVM), typeof(Formation));
        [Bindable(true)] public FormationVM formation
        {
            get { return GetValue(FormationProperty) as FormationVM; }
            set { SetValue(FormationProperty, value); }
        }
    }
}
