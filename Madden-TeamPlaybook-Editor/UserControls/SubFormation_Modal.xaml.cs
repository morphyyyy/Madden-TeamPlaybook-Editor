using Madden.Team;
using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
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

        private void lvwAlignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox tabControl = sender as ComboBox;
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

        private void lvwPackagesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwPackages.SelectedItem == null) return;
            Package _package = lvwPackages.SelectedItem as Package;
            subFormation.GetPackage(_package);
            subFormation.GetPlayers();
        }

        private void iclIconMouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PlayerIcon player = sender as PlayerIcon;
            Popup popup = new Popup { StaysOpen = false, Placement = PlacementMode.MousePoint, DataContext = player.Player };
            WrapPanel wrap = new WrapPanel();
            popup.Child = wrap;

            ComboBox listEPos = new ComboBox { ItemsSource = TeamPlaybook.Positions, DisplayMemberPath = "Value", SelectedValuePath = "Key" };
            wrap.Children.Add(listEPos);
            Binding EPos = new Binding("EPos")
            {
                Source = player.Player.SETP,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            listEPos.SetBinding(ComboBox.SelectedIndexProperty, EPos);
            //listEPos.SelectedIndex = player.Player.SETP.EPos;
            listEPos.SelectionChanged += new SelectionChangedEventHandler(UpdatePackage);

            ComboBox listDPos = new ComboBox { ItemsSource = new List<int> { 1, 2, 3, 4, 5 } };
            wrap.Children.Add(listDPos);
            Binding DPos = new Binding("DPos")
            {
                Source = player.Player.SETP,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            listDPos.SetBinding(ComboBox.SelectedValueProperty, DPos);
            //listDPos.SelectedValue = player.Player.SETP.DPos;
            listDPos.SelectionChanged += new SelectionChangedEventHandler(UpdatePackage);

            popup.IsOpen = true;
        }

        public void UpdatePackage(object sender, SelectionChangedEventArgs e)
        {
            PlayerVM player = ((ComboBox)sender).DataContext as PlayerVM;
            SubFormationVM subFormation = player.Play.SubFormation;
            Package package = lvwPackages.SelectedItem as Package ?? subFormation.Packages.SingleOrDefault(p => p.SPKF.name == "Normal");
            SETP basePackageSETP = subFormation.BasePackage.SingleOrDefault(p => p.poso == player.SETP.poso);
            bool updateDEPos = basePackageSETP.DPos != player.SETP.DPos || basePackageSETP.EPos != player.SETP.EPos;

            if (package.SPKF.name == "Normal")
            {
                basePackageSETP.DPos = player.SETP.DPos;
                basePackageSETP.EPos = player.SETP.EPos;
            }
            else
            {
                SPKG spkg = package.SPKG.SingleOrDefault(p => p.poso == player.SETP.poso);
                if (updateDEPos)
                {
                    if (spkg == null)
                    {
                        spkg = new SPKG
                        {
                            DPos = player.SETP.DPos,
                            EPos = player.SETP.EPos,
                            poso = player.SETP.poso,
                            SPF_ = package.SPKF.SPF_,
                            rec = subFormation.Formation.Playbook.SPKG.Select(s => s.rec).Max() + 1
                        };
                        package.SPKG.Add(spkg);
                        subFormation.Formation.Playbook.SPKG.Add(spkg);
                    }
                    else
                    {
                        spkg.DPos = player.SETP.DPos;
                        spkg.EPos = player.SETP.EPos;
                    }
                }
                else
                {
                    package.SPKG.Remove(spkg);
                    subFormation.Formation.Playbook.SPKG.Remove(spkg);
                }
            }
            subFormation.GetPlayers();
        }

        private void PBST_name_changed(object sender, TextChangedEventArgs e)
        {
            subFormation.SETL.name = subFormation.PBST.name;
        }
    }
}
