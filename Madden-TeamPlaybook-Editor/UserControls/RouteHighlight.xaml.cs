using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class RouteHighlight : UserControl
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (Player != null)
            {
                MatrixAnimationUsingPath pointAnimation = new MatrixAnimationUsingPath
                {
                    PathGeometry = Player.PSALpath[0].Data.GetFlattenedPathGeometry(),
                    Duration = TimeSpan.FromSeconds(3),
                    RepeatBehavior = RepeatBehavior.Forever
                };

                MatrixTransform transform = new MatrixTransform();
                elpHighlight.RenderTransform = transform;
                transform.BeginAnimation(MatrixTransform.MatrixProperty, pointAnimation);
            }
        }

        public RouteHighlight()
        {
            InitializeComponent();
        }

        public static DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(PlayerVM), typeof(RouteHighlight));
        public PlayerVM Player
        {
            get { return (PlayerVM)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public static double GetLengthOfGeo(PathGeometry geo)
        {
            Point p; Point tp;
            geo.GetPointAtFractionLength(0.0001, out p, out tp);
            return (geo.Figures[0].StartPoint - p).Length * 10000;
        }
    }
}
