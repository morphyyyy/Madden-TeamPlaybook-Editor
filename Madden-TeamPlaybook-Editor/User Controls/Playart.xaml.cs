using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    /// <summary>
    /// Interaction logic for Playart.xaml
    /// </summary>
    public partial class Playart : UserControl
    {
        public Playart()
        {
            InitializeComponent();
            this.ContextMenu.DataContext = this.Player;
        }

        public Playart(PlayerVM player, bool PSALView)
        {
            InitializeComponent();
            Player = player;
            this.ContextMenu.DataContext = this.Player;
            Scale = 1.5;
            AbsolutePositioning = true;
            this.PSALView = PSALView;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point pos, tangent;
            double angleInRadians;
            double angleInDegrees;
            TransformGroup tg;
            SolidColorBrush RouteBrush = null;
            SolidColorBrush optionRouteBrush = null;
            RadialGradientBrush progressionBrush = null;
            if (Player.artlColor != null)
            {
                RouteBrush = new SolidColorBrush { Color = Player.artlColor.Color };
                optionRouteBrush = new SolidColorBrush { Color = Player.artlColor.Color, Opacity = 0.5 };
                progressionBrush = new RadialGradientBrush();
                progressionBrush.GradientOrigin = new Point(0.5, 0.5);
                progressionBrush.Center = new Point(0.5, 0.5);
                progressionBrush.RadiusX = 0.5;
                progressionBrush.RadiusX = 0.5;
                progressionBrush.GradientStops.Add(new GradientStop(Player.artlColor.Color, 0.0));
                progressionBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
            }
            Pen routePen = new Pen(RouteBrush, Scale * 4);
            Pen endCapPen = new Pen(RouteBrush, 0);
            Pen optionRoutePen = new Pen(optionRouteBrush, Scale * 4);
            Pen optionEndCapPen = new Pen(optionRouteBrush, 0);
            PathGeometry basePath;
            PathGeometry option1Path;
            PathGeometry option2Path;

            if (Route == null || Route.Count == 0)
            {
                basePath = new PathGeometry();
                option1Path = new PathGeometry();
                option2Path = new PathGeometry();
            }
            else
            {
                basePath = Route[0].Data.GetFlattenedPathGeometry();
                option1Path = Route.Count > 1 ? Route[1].Data.GetFlattenedPathGeometry() : new PathGeometry();
                option2Path = Route.Count == 3 ? Route[2].Data.GetFlattenedPathGeometry() : new PathGeometry();
            }

            if (AbsolutePositioning)
            {
                tg = new TransformGroup();
                tg.Children.Add(new TranslateTransform(Player.XY.X + TeamPlaybook.LOS.X, Player.XY.Y + TeamPlaybook.LOS.Y));
                dc.PushTransform(tg);
            }

            dc.DrawGeometry(null, routePen, basePath);

            if (basePath.Bounds.Size != new Size(0, 0) && basePath.Bounds.Size != Size.Empty)
            {
                basePath.GetPointAtFractionLength(1, out pos, out tangent);
                angleInRadians = Math.Atan2(tangent.Y, tangent.X);
                angleInDegrees = angleInRadians * 180 / Math.PI;
                tg = new TransformGroup();
                if (!Player.artlColor.Equals(ARTLColor.DeepZone) &&
                    !Player.artlColor.Equals(ARTLColor.CloudFlat) &&
                    !Player.artlColor.Equals(ARTLColor.CurlFlat) &&
                    !Player.artlColor.Equals(ARTLColor.HardFlat) &&
                    !Player.artlColor.Equals(ARTLColor.QuarterFlat) &&
                    !Player.artlColor.Equals(ARTLColor.SeamFlat) &&
                    !Player.artlColor.Equals(ARTLColor.SoftSquat) &&
                    !Player.artlColor.Equals(ARTLColor.HookCurl) &&
                    !Player.artlColor.Equals(ARTLColor.ThreeReceiverHook) &&
                    !Player.artlColor.Equals(ARTLColor.VertHook) &&
                    !Player.artlColor.Equals(ARTLColor.MidRead) &&
                    !Player.artlColor.Equals(ARTLColor.QBSpy))
                {
                    tg.Children.Add(new RotateTransform(angleInDegrees));
                    tg.Children.Add(new ScaleTransform(Scale, Scale));
                }
                else
                {
                    if (PSALView)
                    {
                        tg.Children.Add(new ScaleTransform(Scale * 1.25, Scale * 1.5));
                    }
                    else
                    {
                        tg.Children.Add(new ScaleTransform(Scale, Scale));
                    }
                    RouteBrush.Opacity = 0.666;
                }
                basePath.GetPointAtFractionLength(0.98, out pos, out tangent);
                tg.Children.Add(new TranslateTransform(pos.X, pos.Y));
                dc.PushTransform(tg);
                if (Player.PSAL.Exists(x => (x.code == 47) == true))
                {
                    RouteBrush.Color = ARTLColor.MotionRoute.Color;
                    RouteBrush.Opacity = 1;
                }
                dc.DrawGeometry(RouteBrush, endCapPen, Player.RouteCap);
                dc.Pop();
            }

            if (option1Path.Figures.Count > 0)
            {
                dc.DrawGeometry(null, optionRoutePen, option1Path);

                if (option1Path.Bounds.Size != new Size(0, 0) && option1Path.Bounds.Size != Size.Empty)
                {
                    option1Path.GetPointAtFractionLength(1, out pos, out tangent);
                    angleInRadians = Math.Atan2(tangent.Y, tangent.X);
                    angleInDegrees = angleInRadians * 180 / Math.PI;
                    tg = new TransformGroup();
                    tg.Children.Add(new ScaleTransform(Scale, Scale));
                    tg.Children.Add(new RotateTransform(angleInDegrees));
                    option1Path.GetPointAtFractionLength(1, out pos, out tangent);
                    tg.Children.Add(new TranslateTransform(pos.X, pos.Y));
                    dc.PushTransform(tg);
                    dc.DrawGeometry(optionRouteBrush, optionEndCapPen, Player.RouteCap);
                    dc.Pop();
                }
            }

            if (option2Path.Figures.Count > 0)
            {
                dc.DrawGeometry(null, optionRoutePen, option2Path);

                if (option2Path.Bounds.Size != new Size(0, 0) && option2Path.Bounds.Size != Size.Empty)
                {
                    option2Path.GetPointAtFractionLength(1, out pos, out tangent);
                    angleInRadians = Math.Atan2(tangent.Y, tangent.X);
                    angleInDegrees = angleInRadians * 180 / Math.PI;
                    tg = new TransformGroup();
                    tg.Children.Add(new ScaleTransform(Scale, Scale));
                    tg.Children.Add(new RotateTransform(angleInDegrees));
                    option2Path.GetPointAtFractionLength(1, out pos, out tangent);
                    tg.Children.Add(new TranslateTransform(pos.X, pos.Y));
                    dc.PushTransform(tg);
                    dc.DrawGeometry(optionRouteBrush, optionEndCapPen, Player.RouteCap);
                    dc.Pop();
                }
            }

            if (PSALView && Player.progression != null)
            {
                tg = new TransformGroup();
                tg.Children.Add(new ScaleTransform(4, 4));
                tg.Children.Add(new TranslateTransform(Player.progression.icx * 10, Player.progression.icy * -10));
                dc.PushTransform(tg);
                dc.DrawGeometry(progressionBrush, null, Player.Icon);
            }

            //if (basePath.Bounds.Size != new Size(0, 0))
            //{
            //    MatrixAnimationUsingPath pointAnimation = new MatrixAnimationUsingPath
            //    {
            //        PathGeometry = basePath,
            //        Duration = TimeSpan.FromSeconds(3),
            //        RepeatBehavior = RepeatBehavior.Forever
            //    };

            //    MatrixTransform transform = new MatrixTransform();
            //    transform.BeginAnimation(MatrixTransform.MatrixProperty, pointAnimation);
            //    dc.DrawGeometry(routeRunningBrush, routeRunningPen, routeRunningHighlight);
            //}
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            //TODO: Consider creating the Pen once when Stroke and StrokeThickness are set
            if (Route == null || Route.Count == 0)
            {
                return new Size(0, 0);
            }
            else
            {
                SolidColorBrush RouteBrush = Player.artlColor is null ? null : new SolidColorBrush { Color = Player.artlColor.Color };
                return Route[0].Data.GetFlattenedPathGeometry().GetRenderBounds(new Pen(RouteBrush, Scale * 4)).Size;
            }
        }

        public static DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(Playart));
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static DependencyProperty PlayerProperty =
            DependencyProperty.Register("Player", typeof(PlayerVM), typeof(Playart));
        public PlayerVM Player
        {
            get
            {
                return (PlayerVM)GetValue(PlayerProperty);
            }
            set { SetValue(PlayerProperty, value); }
        }

        public static DependencyProperty PSALViewProperty =
            DependencyProperty.Register("PSALView", typeof(bool), typeof(Playart));
        public bool PSALView
        {
            get { return (bool)GetValue(PSALViewProperty); }
            set { SetValue(PSALViewProperty, value); }
        }

        public static DependencyProperty AbsolutePositioningProperty =
            DependencyProperty.Register("AbsolutePositioning", typeof(bool), typeof(Playart));
        public bool AbsolutePositioning
        {
            get { return (bool)GetValue(AbsolutePositioningProperty); }
            set { SetValue(AbsolutePositioningProperty, value); }
        }

        private List<Path> Route
        {
            get
            {
                if (PSALView)
                    return Player.PSALpath;
                else
                    return Player.ARTLpath;
            }
            set
            {
                if (PSALView)
                    Route = Player.PSALpath;
                else
                    Route = Player.ARTLpath;
            }
        }
    }
}
