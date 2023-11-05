using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SubFormation : UserControl
    {
        public SubFormation()
        {
            InitializeComponent();
            if (showPackage)
            {
                pnlSubFormation.Visibility = Visibility.Visible;
            }
            else
            {
                pnlSubFormation.Visibility = Visibility.Collapsed;
            }
        }

        public static DependencyProperty SubFormationProperty = DependencyProperty.Register("subFormation", typeof(SubFormationVM), typeof(SubFormation));
        [Bindable(true)] public SubFormationVM subFormation
        {
            get { return GetValue(SubFormationProperty) as SubFormationVM; }
            set { SetValue(SubFormationProperty, value); }
        }

        public static DependencyProperty ShowPackageProperty = DependencyProperty.Register("showPackage", typeof(bool), typeof(SubFormation));
        [Bindable(true)] public bool showPackage
        {
            get { return (bool)GetValue(ShowPackageProperty); }
            set { SetValue(ShowPackageProperty, value); }
        }
    }
}
