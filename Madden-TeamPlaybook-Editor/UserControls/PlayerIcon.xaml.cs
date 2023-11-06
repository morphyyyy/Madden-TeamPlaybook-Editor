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

        public PlayerIcon()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(UserControl_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(UserControl_MouseMove);
        }

        public PlayerIcon(PlayerVM player)
        {
            InitializeComponent();
            Player = player;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(UserControl_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(UserControl_MouseMove);
        }

        protected override void OnRender(DrawingContext dc)
        {
            SolidColorBrush playerIconBrush = new SolidColorBrush(Madden.TeamPlaybook.ARTLColor.PlayerIconColor);
            Pen iconPen = new Pen(playerIconBrush, 0);
            SolidColorBrush EPosBrush = new SolidColorBrush(Colors.Black);
            Pen EPosPen = new Pen(playerIconBrush, 0.3);
            TransformGroup tg = new TransformGroup();
            Point pos = !AbsolutePositioning ? new Point(Player.XY.X + TeamPlaybook.LOS.X, Player.XY.Y + TeamPlaybook.LOS.Y) : new Point(0, 0);

            tg.Children.Add(new TranslateTransform(pos.X, pos.Y));
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
            if (Draggable)
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
            if (Draggable)
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
                Player.UpdateXY(new Point(Player.XY.X + prevX, Player.XY.Y + prevY));
                //Player.Play.UpdatePlayers();
                draggable.ReleaseMouseCapture();
                if (playerPlayart != null)
                {
                    playerPlayart.InvalidateVisual();
                }
            }
        }

        private void UserControl_MouseMove(Object sender, MouseEventArgs e)
        {
            if (Draggable)
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
                    transform.X = currentPosition.X - mousePosition.X;
                    transform.Y = currentPosition.Y - mousePosition.Y;
                    //Player.UpdateXY(new Point(Player.XY.X + transform.X, Player.XY.Y + transform.Y));
                    if (playerPlayart != null)
                    {
                        playerPlayart.InvalidateVisual();
                    }
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

        public static DependencyProperty DraggableProperty =
            DependencyProperty.Register("Draggable", typeof(bool), typeof(PlayerIcon));
        public bool Draggable
        {
            get { return (bool)GetValue(DraggableProperty); }
            set { SetValue(DraggableProperty, value); }
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
    }
}
