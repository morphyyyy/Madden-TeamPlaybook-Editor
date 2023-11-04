using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PlayerIcon : UserControl
    {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        public bool draggable = true;

        public PlayerIcon()
        {
            InitializeComponent();
            this.ContextMenu.DataContext = this.Player;
            this.ToolTip = this.Player;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(UserControl_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(UserControl_MouseMove);
        }

        public PlayerIcon(PlayerVM player)
        {
            InitializeComponent();
            Player = player;
            this.ContextMenu.DataContext = this.Player;
            this.ToolTip = this.Player;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(UserControl_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(UserControl_MouseMove);
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

            tg = new TransformGroup();
            pos = AbsolutePositioning ? new Point(Player.XY.X + TeamPlaybook.LOS.X, Player.XY.Y + TeamPlaybook.LOS.Y) : new Point(0, 0);
            tg.Children.Add(new TranslateTransform(pos.X, pos.Y));
            dc.PushTransform(tg);
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

            if (PSALView && Player.progression != null && ShowProgression)
            {
                tg = new TransformGroup();
                tg.Children.Add(new ScaleTransform(4, 4));
                tg.Children.Add(new TranslateTransform(Player.progression.icx * 10, Player.progression.icy * -10));
                dc.PushTransform(tg);
                dc.DrawGeometry(progressionBrush, null, Player.Icon);
                dc.Pop();
            }

            SolidColorBrush playerIconBrush = new SolidColorBrush(Madden.TeamPlaybook.ARTLColor.PlayerIconColor);
            Pen iconPen = new Pen(playerIconBrush, 0);
            SolidColorBrush EPosBrush = new SolidColorBrush(Colors.Black);
            Pen EPosPen = new Pen(playerIconBrush, 0.3);

            tg = new TransformGroup();
            tg.Children.Add(new ScaleTransform(Scale, Scale));
            dc.PushTransform(tg);

            if (Player != null)
            {
                dc.DrawGeometry(playerIconBrush, iconPen, Player.Icon);
                if (ShowPosition)
                {
                    string label = Player.DCHT == null ? Player.EPos + (Int32.Parse(Player.DPos) > 1 ? " " + Player.DPos : "") : Player.Number.ToString();
                    FormattedText EPos = new FormattedText(
                        label,
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface(new FontFamily("Tahoma"), FontStyles.Normal, FontWeights.Black, FontStretches.Normal),
                        6.75,
                        EPosBrush
                        );
                    PathGeometry EPosPath = EPos.BuildGeometry(new Point(EPos.Width * -.5, EPos.Height * -.5)).GetFlattenedPathGeometry();
                    dc.DrawGeometry(EPosBrush, EPosPen, EPosPath);
                }
                if (Animate)
                {
                    double yardsPerSec = 40 / 4.3;
                    double secPerYard = 1 / yardsPerSec;
                    double routeLength = GetLengthOfGeo(Player.PSALpath[0].Data.GetFlattenedPathGeometry());
                    double routeTime = (secPerYard * (routeLength / 10)) / (Player.Speed / 100.0);
                    MatrixAnimationUsingPath pointAnimation = new MatrixAnimationUsingPath
                    {
                        PathGeometry = Player.PSALpath[0].Data.GetFlattenedPathGeometry(),
                        Duration = TimeSpan.FromSeconds(routeTime),
                    };
                    pointAnimation.AccelerationRatio = .15;
                    MatrixTransform transform = new MatrixTransform();
                    RenderTransform = transform;
                    transform.BeginAnimation(MatrixTransform.MatrixProperty, pointAnimation);
                }
            }
            else
            {
                dc.DrawGeometry(playerIconBrush, iconPen, new EllipseGeometry(new Rect(new Point(-4, -4), new Size(8, 8))));
            }

            dc.Pop();
        }

        public Playart GetPlayart()
        {
            Playart playart = new Playart();
            foreach (UIElement child in ((Canvas)this.Parent).Children)
            {
                if (child is Playart)
                {
                    if (((Playart)child).Player == Player)
                    {
                        playart = child as Playart;
                    }
                }
            }
            return playart;
        }

        private void UserControl_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            if (draggable)
            {
                ContextMenu.IsOpen = false;
                isDragging = true;
                var draggableControl = sender as UserControl;
                DependencyObject parent = VisualTreeHelper.GetParent(sender as DependencyObject);
                Console.WriteLine(Canvas.GetLeft(sender as UIElement) + "," + Canvas.GetTop(sender as UIElement));
                mousePosition = e.GetPosition((UIElement)Parent);
                draggableControl.CaptureMouse();
                //playerPlayart = GetPlayart();

                //Convert PSALPath back to DistDir to check PSALEditor

                //PathGeometry pathGeo = Player.PSALpath[0].Data as PathGeometry;
                //PolyLineSegment polyLineSeg = pathGeo.Figures[0].Segments[0] as PolyLineSegment;
                //for (int i = 0; i < polyLineSeg.Points.Count; i++)
                //{
                //    if (i > 0)
                //    {
                //        Console.WriteLine(PlayerVM.XYtoMoveDistDir(new Point(polyLineSeg.Points[i].X - polyLineSeg.Points[i-1].X, -polyLineSeg.Points[i].Y - -polyLineSeg.Points[i - 1].Y)));
                //    }
                //    else
                //    {
                //        Console.WriteLine(PlayerVM.XYtoMoveDistDir(new Point(polyLineSeg.Points[i].X, -polyLineSeg.Points[i].Y)));
                //    }
                //}
            }
        }

        private void UserControl_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            if (draggable)
            {
                isDragging = false;
                var draggable = sender as UserControl;
                var transform = draggable.RenderTransform as TranslateTransform;
                var currentPosition = e.GetPosition((UIElement)Parent);
                if (transform != null)
                {
                    prevX = transform.X;
                    prevY = transform.Y;
                }
                Player.Play.UpdatePlayers();
                draggable.ReleaseMouseCapture();
                if (playerPlayart != null)
                {
                    playerPlayart.InvalidateVisual();
                }
            }
        }

        private void UserControl_MouseMove(Object sender, MouseEventArgs e)
        {
            if (draggable)
            {
                var draggableControl = sender as UserControl;
                if (isDragging && draggableControl != null)
                {
                    var currentPosition = e.GetPosition((UIElement)Parent);
                    var transform = draggableControl.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        draggableControl.RenderTransform = transform;
                    }
                    transform.X = currentPosition.X - mousePosition.X + prevX;
                    transform.Y = currentPosition.Y - mousePosition.Y + prevY;
                    //Player.UpdateXY(new Point(currentPosition.X - TeamPlaybook.LOS.X, currentPosition.Y - TeamPlaybook.LOS.Y));
                    //if (playerPlayart != null)
                    //{
                    //    playerPlayart.InvalidateVisual();
                    //}
                    Console.WriteLine(currentPosition);
                    Console.WriteLine("SETP.fmtx:{0}\tSETP.fmty:{1}\tSETP.artx:{2}\tSETP.arty:{3}\t", Player.SETP.fmtx, Player.SETP.fmty, Player.SETP.artx, Player.SETP.arty);
                }

            }
        }

        protected Boolean isDragging;
        private Point mousePosition;
        private Double prevX, prevY;
        private Playart playerPlayart;

        public static DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(PlayerIcon));
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static DependencyProperty PlayerProperty =
            DependencyProperty.Register("Player", typeof(PlayerVM), typeof(PlayerIcon));
        public PlayerVM Player
        {
            get { return (PlayerVM)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public static DependencyProperty ShowPositionProperty =
            DependencyProperty.Register("ShowPosition", typeof(bool), typeof(PlayerIcon));
        public bool ShowPosition
        {
            get { return (bool)GetValue(ShowPositionProperty); }
            set { SetValue(ShowPositionProperty, value); }
        }

        public static DependencyProperty AnimateProperty =
            DependencyProperty.Register("Animate", typeof(bool), typeof(PlayerIcon));
        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }

        public static DependencyProperty ShowProgressionProperty =
            DependencyProperty.Register("ShowProgression", typeof(bool), typeof(Playart));
        public bool ShowProgression
        {
            get { return (bool)GetValue(ShowProgressionProperty); }
            set { SetValue(ShowProgressionProperty, value); }
        }

        public static DependencyProperty PSALViewProperty =
            DependencyProperty.Register("PSALView", typeof(bool), typeof(Playart));
        public bool PSALView
        {
            get { return (bool)GetValue(PSALViewProperty); }
            set { SetValue(PSALViewProperty, value); }
        }

        public static DependencyProperty AbsolutePositioningProperty =
            DependencyProperty.Register("AbsolutePositioning", typeof(bool), typeof(PlayerIcon));
        public bool AbsolutePositioning
        {
            get { return (bool)GetValue(AbsolutePositioningProperty); }
            set { SetValue(AbsolutePositioningProperty, value); }
        }

        public static double GetLengthOfGeo(PathGeometry geo)
        {
            Point p; Point tp;
            geo.GetPointAtFractionLength(0.0001, out p, out tp);
            if (geo.Figures.Count > 0)
            {
                return (geo.Figures[0].StartPoint - p).Length * 10000;
            }
            else
            {
                return 0;
            }
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
