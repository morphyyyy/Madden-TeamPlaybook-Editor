using Madden.TeamPlaybook;
using MaddenTeamPlaybookEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TDBAccess;

namespace MaddenTeamPlaybookEditor.User_Controls
{
    public partial class PSAL_Editor : UserControl
    {
        public bool editing = false;
        Path PSALpath = new Path();
        PathGeometry RouteGeo = new PathGeometry();
        PathFigure RouteFigure = new PathFigure();
        PathSegmentCollection RouteSegments = new PathSegmentCollection();
        PointCollection RoutePoints = new PointCollection();
        PolyLineSegment line_segment = new PolyLineSegment();
        public List<PSAL> PSAL = new List<PSAL>();

        public PSAL_Editor()
        {
            InitializeComponent();

            RouteFigure.Segments = RouteSegments;
            RouteGeo.Figures.Add(RouteFigure);
            PSALpath.Data = RouteGeo;
            RouteSegments.Add(line_segment);
            line_segment.Points = RoutePoints;
            line_segment.IsSmoothJoin = true;

            PSAL_Canvas.Children.Add(PSALpath);
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point pos1, tangent1;
            double angleInRadians;
            double angleInDegrees;
            TransformGroup tg;
            SolidColorBrush RouteBrush = new SolidColorBrush { Color = ARTLColor.PrimaryRoute.Color };
            Pen routePen = new Pen(RouteBrush, 8);
            Pen endCapPen = new Pen(RouteBrush, 0);
            PathGeometry Route;
            Route = PSALpath.Data.GetFlattenedPathGeometry();

            if (RoutePoints.Count > 0)
            {
                dc.DrawGeometry(null, routePen, Route);

                Route.GetPointAtFractionLength(1, out pos1, out tangent1);
                angleInRadians = Math.Atan2(tangent1.Y, tangent1.X);
                angleInDegrees = angleInRadians * 180 / Math.PI;
                tg = new TransformGroup();
                tg.Children.Add(new RotateTransform(angleInDegrees));
                tg.Children.Add(new ScaleTransform(2, 2));
                tg.Children.Add(new TranslateTransform(pos1.X, pos1.Y));
                dc.PushTransform(tg);
                dc.DrawGeometry(RouteBrush, endCapPen, ARTL.Arrow);
                dc.Pop();
            }
        }

        private void PSAL_Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (editing)
                {
                    if (RoutePoints.Count > 0)
                    {
                        RoutePoints.RemoveAt(RoutePoints.Count - 1);
                    }
                    this.InvalidateVisual();
                    editing = false;

                    //for (int step = 0; step < RoutePoints.Count; step++)
                    //{
                    //    Console.WriteLine((int)(RoutePoints[step].X - RouteFigure.StartPoint.X) + ", " + -(int)(RoutePoints[step].Y - RouteFigure.StartPoint.Y));
                    //    Console.WriteLine(PSAL[step]);
                    //}

                    Clipboard.Clear();
                    Clipboard.SetText(string.Join("\n", PSAL.Select(t => t)));

                    MessageBox.Show("PSAL List Copied to Clipboard!" + "\n" + "\n" + string.Join("\n", PSAL.Select(t => t)));
                }
                else
                {
                    RoutePoints.Clear();
                    PSAL.Clear();
                    this.InvalidateVisual();
                }
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (RoutePoints.Count == 0)
                {
                    RouteFigure.StartPoint = e.GetPosition(this);
                    editing = true;
                }
                else
                {
                    if (editing)
                    {
                        RoutePoints.Add(e.GetPosition(this));
                        this.InvalidateVisual();
                        Point XY = new Point();
                        if (RoutePoints.Count > 2)
                        {
                            XY.X = (int)(RoutePoints[RoutePoints.Count - 2].X - RoutePoints[RoutePoints.Count - 3].X);
                            XY.Y = -(int)(RoutePoints[RoutePoints.Count - 2].Y - RoutePoints[RoutePoints.Count - 3].Y);
                        }
                        else
                        {
                            XY.X = (int)(RoutePoints[RoutePoints.Count - 1].X - RouteFigure.StartPoint.X);
                            XY.Y = -(int)(RoutePoints[RoutePoints.Count - 1].Y - RouteFigure.StartPoint.Y);
                        }
                        PSAL.Add(PlayerVM.XYtoMoveDistDir(XY));
                        PSAL[PSAL.Count - 1].step = PSAL.Count - 1;
                        PSAL[PSAL.Count - 1].val3 = 254;
                        PSAL[PSAL.Count - 1].code = 8;
                    }
                    else
                    {
                        RoutePoints.Clear();
                        PSAL.Clear();
                        this.InvalidateVisual();
                    }
                }
            }
            this.InvalidateVisual();
        }

        private void PSAL_Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (editing)
            {
                if (RoutePoints.Count > 0)
                {
                    RoutePoints.RemoveAt(RoutePoints.Count - 1);
                }
                RoutePoints.Add(e.GetPosition(this));
            }
            this.InvalidateVisual();
        }
    }
}
