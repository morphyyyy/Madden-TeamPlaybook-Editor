using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Situation : UserControl
    {
        protected Boolean isDragging;

        public Situation()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext dc)
        {

            tbkName.Visibility = showPlayName ? Visibility.Collapsed : Visibility.Visible;
            tbkPlayName.Visibility = !showPlayName ? Visibility.Collapsed : Visibility.Visible;

        }

        public static DependencyProperty SituationProperty = DependencyProperty.Register("situation", typeof(PBAI), typeof(Situation));

        public PBAI situation
        {
            get
            {
                return GetValue(SituationProperty) as PBAI;
            }
            set
            {
                SetValue(SituationProperty, value);
            }
        }

        public static DependencyProperty ShowPlayNameProperty = DependencyProperty.Register("showPlayName", typeof(bool), typeof(Situation));

        public bool showPlayName
        {
            get { return (bool)GetValue(ShowPlayNameProperty); }
            set { SetValue(ShowPlayNameProperty, value); }
        }

        private void pbrSituation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow _mainWindow = UIHelper.FindVisualParent<MainWindow>(this);
            PlayVM _play = _mainWindow?.TeamPlaybook.Plays.SingleOrDefault(p => p.PBPL.pbpl == situation.PBPL);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                pbrSituation.Value = (Math.Round(e.GetPosition((UIElement)pbrSituation).X / 10)) * 10;
                //pbrSituation.Value = e.GetPosition((UIElement)pbrSituation).X;
                if (!_play.Situations.Contains(situation))
                {
                    _play.AddSituation(situation);
                }
                _mainWindow.lvwPlaysBySituation.Items.Filter = _mainWindow.SituationFilter;

                isDragging = true;
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                pbrSituation.Value = 0;
                _play.RemoveSituation(situation);
            }
        }

        private void pbrSituation_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                //pbrSituation.Value = e.GetPosition((UIElement)pbrSituation).X;
            }
        }

        private void pbrSituation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void pbrSituation_MouseLeave(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        public static class UIHelper
        {
            public static T FindVisualParent<T>(DependencyObject control)
            where T : DependencyObject
            {
                // get parent item
                DependencyObject parentObject = VisualTreeHelper.GetParent(control);

                // we’ve reached the end of the tree
                if (parentObject == null) return null;

                // check if the parent matches the type we’re looking for
                T parent = parentObject as T;
                if (parent != null)
                {
                    return parent;
                }
                else
                {
                    // use recursion to proceed with next level
                    return FindVisualParent<T>(parentObject);
                }
            }

            public static T FindChild<T>(DependencyObject parent, string childName, PlayVM _play = null)
            where T : DependencyObject
            {
                // Confirm parent and childName are valid. 
                if (parent == null) return null;
                T foundChild = null;
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i);
                    // If the child is not of the request child type child
                    T childType = child as T;
                    if (childType == null)
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName, _play);
                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName && frameworkElement is Play && _play != null)
                        {
                            if (frameworkElement.DataContext == _play)
                            {
                                foundChild = (T)child;
                                break;
                            }
                        }
                        else if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                        foundChild = FindChild<T>(child, childName, _play);
                    }
                    else
                    {
                        // child element found.
                        foundChild = (T)child;
                        break;
                    }
                }
                return foundChild;
            }
        }
    }
}
