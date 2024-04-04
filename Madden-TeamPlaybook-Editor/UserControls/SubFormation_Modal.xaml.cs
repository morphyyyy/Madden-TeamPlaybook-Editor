using MaddenTeamPlaybookEditor.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static MaddenTeamPlaybookEditor.ViewModels.SubFormationVM;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class SubFormationModal : UserControl
    {
        public SubFormationModal()
        {
            InitializeComponent();
        }

        public static DependencyProperty SubFormationProperty = DependencyProperty.Register("subFormation", typeof(SubFormationVM), typeof(SubFormationModal));

        public SubFormationVM subFormation
        {
            get
            {
                return GetValue(SubFormationProperty) as SubFormationVM;
            }
            set
            {
                SetValue(SubFormationProperty, value);
            }
        }

        private void tabAlignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView tabControl = sender as ListView;
            if (tabControl.SelectedItem == null) return;
            if (tabControl.SelectedItem is Alignment)
            {
                this.subFormation.GetAlignment(((Alignment)tabControl.SelectedItem).SGFM);
                this.subFormation.GetPlayers();
            }
            else if (tabControl.SelectedItem is Package)
            { 

            }
        }
    }
}
