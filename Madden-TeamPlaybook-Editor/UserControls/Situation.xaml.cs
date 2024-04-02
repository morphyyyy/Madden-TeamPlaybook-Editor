﻿using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class Situation : UserControl
    {
        protected Boolean isDragging;

        public Situation()
        {
            InitializeComponent();
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

        private void pbrSituation_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                pbrSituation.Value = (Math.Round(e.GetPosition((UIElement)pbrSituation).X / 10)) * 10;
                //pbrSituation.Value = e.GetPosition((UIElement)pbrSituation).X;
                isDragging = true;
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                pbrSituation.Value = 0;
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
    }
}
