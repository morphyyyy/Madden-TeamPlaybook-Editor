using MaddenTeamPlaybookEditor;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace Madden.TeamPlaybook.Editor.UserControls
{
    /// <summary>
    /// Interaction logic for ColorTheme.xaml
    /// </summary>
    public partial class ColorTheme : UserControl
    {
        public ColorTheme()
        {
            InitializeComponent();
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            ColorPicker color = sender as ColorPicker;
            Application.Current.Resources[color.Name] = color.SelectedColor;
        }

        private void UseTeamColors(object sender, RoutedEventArgs e)
        {
            var UseTeamColors = Application.Current.Resources["UseTeamColors"];
            Application.Current.Resources["UseTeamColors"] = cbxUseTeamColors.IsChecked;
        }

        private void lvwTeamColors_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (lvwTeamColors.SelectedValue == null) return;
            List<string> teamColors = lvwTeamColors.SelectedValue as List<string>;
            MainWindow.SetTeamColors(teamColors);
        }
    }
}
