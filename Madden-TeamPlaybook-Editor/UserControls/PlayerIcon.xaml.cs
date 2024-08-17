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
        protected Boolean isDragging;
        private Point mousePosition;
        private Double prevX, prevY;
        private ItemsControl PlayerIcons;
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        public PlayerIcon()
        {
            InitializeComponent();
            Player = new PlayerVM { Play = new PlayVM { SubFormation = new SubFormationVM { Formation = new FormationVM { Playbook = new TeamPlaybook() } } } };
            SolidColorBrush playerIconBrush = IconColor ?? new SolidColorBrush(Madden.TeamPlaybook.ARTLColor.PlayerIconColor);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(UserControl_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(UserControl_MouseMove);
        }

        public PlayerIcon(PlayerVM player)
        {
            InitializeComponent();
            Player = player;
            SolidColorBrush playerIconBrush = IconColor ?? new SolidColorBrush(Madden.TeamPlaybook.ARTLColor.PlayerIconColor);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(UserControl_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(UserControl_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(UserControl_MouseMove);
        }

        protected override void OnRender(DrawingContext dc)
        {
            SolidColorBrush playerIconBrush = IconColor ?? new SolidColorBrush(Madden.TeamPlaybook.ARTLColor.PlayerIconColor);
            Pen iconPen = new Pen(playerIconBrush, 0);
            SolidColorBrush EPosBrush = new SolidColorBrush(Colors.Black);
            Pen EPosPen = new Pen(playerIconBrush, 0.3);
            TransformGroup tg = new TransformGroup();
            Point pos = !AbsolutePositioning ? new Point(Player.XY.X + Player.Play.SubFormation.Formation.Playbook.LOS.X, Player.XY.Y + Player.Play.SubFormation.Formation.Playbook.LOS.Y) : new Point(0, 0);

            tg.Children.Add(new TranslateTransform(pos.X, pos.Y));
            tg.Children.Add(new ScaleTransform(Scale, Scale));
            dc.PushTransform(tg);

            if (Player != null)
            {
                dc.DrawGeometry(playerIconBrush, iconPen, Player.Icon);
                if (ShowPosition)
                {
                    FormattedText EPos = new FormattedText(
                        Player.Label,
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

        private void UserControl_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            if (Player.Play.Players!= null)
            {
                foreach (PlayerVM player in Player.Play.Players) player.IsSelected = false;
                Player.IsSelected = true;
            }
            if (Draggable)
            {
                isDragging = true;
                var draggableControl = sender as UserControl;
                DependencyObject parent = VisualTreeHelper.GetParent(sender as DependencyObject);
                //Console.WriteLine(Canvas.GetLeft(sender as UIElement) + "," + Canvas.GetTop(sender as UIElement));
                mousePosition = e.GetPosition((UIElement)Parent);
                draggableControl.CaptureMouse();
                PlayerIcons = UIHelper.FindChild<ItemsControl>(UIHelper.FindVisualParent<PlayModal>(this), "iclPSALs");
            }
        }

        private void UserControl_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            if (Draggable)
            {
                isDragging = false;
                var draggableControl = sender as UserControl;
                var transform = draggableControl.RenderTransform as TranslateTransform;
                var currentPosition = e.GetPosition((UIElement)Parent);
                if (transform != null)
                {
                    prevX = transform.X;
                    prevY = transform.Y;
                    transform = new TranslateTransform();
                    draggableControl.RenderTransform = transform;
                }
                Player.UpdateXY(new Point(Player.XY.X + (prevX * 2), Player.XY.Y + (prevY * 2)));
                //Player.Play.UpdatePlayers();
                draggableControl.ReleaseMouseCapture();
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
                    if (Player.Play.PlayerPlayartView != null)
                    {
                        Player.UpdateXY(new Point(Player.XY.X + transform.X, Player.XY.Y + transform.Y));
                    }
                    if (PlayerIcons != null) PlayerIcons.Items.Refresh();
                }
            }
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

            public static T FindChild<T>(DependencyObject parent, string childName)
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
                        foundChild = FindChild<T>(child, childName);
                        // If the child is found, break so we do not overwrite the found child. 
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                        foundChild = FindChild<T>(child, childName);
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

        public List<PSAL> PathtoPSAL()
        {
            PathGeometry pathGeo = Player.PSALpath[0].Data as PathGeometry;
            PolyLineSegment polyLineSeg = pathGeo.Figures[0].Segments[0] as PolyLineSegment;
            List<PSAL> _psal = new List<PSAL>();
            for (int i = 0; i < polyLineSeg.Points.Count; i++)
            {
                if (i > 0)
                {
                    _psal.Add(PlayerVM.XYtoMoveDistDir(new Point(polyLineSeg.Points[i].X - polyLineSeg.Points[i - 1].X, -polyLineSeg.Points[i].Y - -polyLineSeg.Points[i - 1].Y)));
                }
                else
                {
                    _psal.Add(PlayerVM.XYtoMoveDistDir(new Point(polyLineSeg.Points[i].X, -polyLineSeg.Points[i].Y)));
                }
            }
            return _psal;
        }

        public static DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(double), typeof(PlayerIcon));
        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static DependencyProperty PlayerProperty = DependencyProperty.Register("Player", typeof(PlayerVM), typeof(PlayerIcon));
        public PlayerVM Player
        {
            get { return (PlayerVM)GetValue(PlayerProperty); }
            set { SetValue(PlayerProperty, value); }
        }

        public static DependencyProperty IconColorProperty = DependencyProperty.Register("IconColor", typeof(SolidColorBrush), typeof(PlayerIcon));
        public SolidColorBrush IconColor
        {
            get { return (SolidColorBrush)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        public static DependencyProperty ShowPositionProperty = DependencyProperty.Register("ShowPosition", typeof(bool), typeof(PlayerIcon));
        public bool ShowPosition
        {
            get { return (bool)GetValue(ShowPositionProperty); }
            set { SetValue(ShowPositionProperty, value); }
        }

        public static DependencyProperty AnimateProperty = DependencyProperty.Register("Animate", typeof(bool), typeof(PlayerIcon));
        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(AnimateProperty, value); }
        }

        public static DependencyProperty DraggableProperty = DependencyProperty.Register("Draggable", typeof(bool), typeof(PlayerIcon));
        public bool Draggable
        {
            get { return (bool)GetValue(DraggableProperty); }
            set { SetValue(DraggableProperty, value); }
        }

        public static DependencyProperty AbsolutePositioningProperty = DependencyProperty.Register("AbsolutePositioning", typeof(bool), typeof(PlayerIcon));
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
