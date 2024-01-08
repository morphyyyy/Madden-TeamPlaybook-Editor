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
            if (((Alignment)((TabControl)sender).SelectedItem) != null)
            {
                this.subFormation.CurrentAlignment = this.subFormation.Alignments.FirstOrDefault(alignment => alignment == ((Alignment)((TabControl)sender).SelectedItem));
                this.subFormation.GetPlayers();
            }
        }
    }
}
