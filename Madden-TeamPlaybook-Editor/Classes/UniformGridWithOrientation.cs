#define INFOS

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MaddenTeamPlaybookEditor.Classes
{
    public class UniformGridOrientations : UniformGrid
    {
        #region Orientation (DP)  
        public System.Windows.Controls.Orientation Orientation
        {
            get { return (System.Windows.Controls.Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(System.Windows.Controls.Orientation), typeof(UniformGridOrientations),
                new FrameworkPropertyMetadata(
                    Orientation.Vertical,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion

        #region VerticalOrientation (DP)
        public VerticalOrientation VerticalOrientation
        {
            get { return (VerticalOrientation)GetValue(VerticalOrientationProperty); }
            set { SetValue(VerticalOrientationProperty, value); }
        }

        public static readonly DependencyProperty VerticalOrientationProperty =
            DependencyProperty.Register(
                "VerticalOrientation",
                typeof(VerticalOrientation),
                typeof(UniformGridOrientations),
                new FrameworkPropertyMetadata(
                    VerticalOrientation.Top,
                    FrameworkPropertyMetadataOptions.AffectsMeasure)
            );
        #endregion

        protected override Size MeasureOverride(Size constraint)
        {
            if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                this.UpdateComputedValues();
                return base.MeasureOverride(constraint);
            }
            else
            {
                this.UpdateComputedValues();
                Size availableSize = new Size(constraint.Width / ((double)this._columns), constraint.Height / ((double)this._rows));
                double width = 0.0;
                double height = 0.0;
                int num3 = 0;
                int count = base.InternalChildren.Count;
                while (num3 < count)
                {
                    UIElement element = base.InternalChildren[num3];
                    element.Measure(availableSize);
                    Size desiredSize = element.DesiredSize;
                    if (width < desiredSize.Width)
                    {
                        width = desiredSize.Width;
                    }
                    if (height < desiredSize.Height)
                    {
                        height = desiredSize.Height;
                    }
                    num3++;
                }
                return new Size(width * this._columns, height * this._rows);
            }
        }

        private int _columns;
        private int _rows;

        private void UpdateComputedValues()
        {
            this._columns = this.Columns;
            this._rows = this.Rows;
            if (this.FirstColumn >= this._columns)
            {
                this.FirstColumn = 0;
            }

            if (FirstColumn > 0)
                throw new NotImplementedException("There is no support for seting the FirstColumn (nor the FirstRow).");
            if ((this._rows == 0) || (this._columns == 0))
            {
                int num = 0;    // Visible children  
                int num2 = 0;
                int count = base.InternalChildren.Count;
                while (num2 < count)
                {
                    UIElement element = base.InternalChildren[num2];
                    if (element.Visibility != Visibility.Collapsed)
                    {
                        num++;
                    }
                    num2++;
                }
                if (num == 0)
                {
                    num = 1;
                }
                if (this._rows == 0)
                {
                    if (this._columns > 0)
                    {
                        this._rows = ((num + this.FirstColumn) + (this._columns - 1)) / this._columns;
                    }
                    else
                    {
                        this._rows = (int)Math.Sqrt((double)num);
                        if ((this._rows * this._rows) < num)
                        {
                            this._rows++;
                        }
                        this._columns = this._rows;
                    }
                }
                else if (this._columns == 0)
                {
                    this._columns = (num + (this._rows - 1)) / this._rows;
                }
            }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            // base calc
            Rect finalRect = new Rect(0.0, 0.0, arrangeSize.Width / ((double)this._columns), arrangeSize.Height / ((double)this._rows));
            double height = finalRect.Height;
            double numX = arrangeSize.Height - 1.0;
            finalRect.X += finalRect.Width * this.FirstColumn;
#if INFOS
            int index = 0;
#endif

            if (Orientation == System.Windows.Controls.Orientation.Horizontal)
            {
                if (VerticalOrientation == VerticalOrientation.Top)
                {
                    return base.ArrangeOverride(arrangeSize);
                }
                else
                {
                    double totalHeigh = arrangeSize.Height;
                    finalRect.Y = totalHeigh - height;
                    foreach (UIElement element in base.InternalChildren)
                    {
                        element.Arrange(finalRect);
#if INFOS
                        Console.WriteLine(index + " -> " + finalRect.SimpleString());
                        index++;
#endif
                        if (element.Visibility != Visibility.Collapsed)
                        {
                            finalRect.X += finalRect.Width;
                            if (finalRect.X >= arrangeSize.Width)
                            {
                                finalRect.X = 0.0;
                                finalRect.Y -= height;
                            }

                        }
                    }
                    return arrangeSize;
                }
            }
            else
            {
                if (VerticalOrientation == VerticalOrientation.Top)
                {
                    foreach (UIElement element in base.InternalChildren)
                    {
                        element.Arrange(finalRect);
#if INFOS
                        Console.WriteLine(index + " -> " + finalRect.SimpleString());
                        index++;
#endif

                        if (element.Visibility != Visibility.Collapsed)
                        {
                            finalRect.Y += height;
                            if (finalRect.Y >= numX)
                            {
                                finalRect.X += finalRect.Width;
                                finalRect.Y = 0.0;
                            }
                        }
                    }
                }
                else
                {
                    double totalHeigh = arrangeSize.Height;
                    finalRect.Y = totalHeigh - height;
                    foreach (UIElement element in base.InternalChildren)
                    {
                        element.Arrange(finalRect);
#if INFOS
                        Console.WriteLine(index + " -> " + finalRect.SimpleString());
                        index++;
#endif
                        if (element.Visibility != Visibility.Collapsed)
                        {
                            finalRect.Y -= height;
                            if (finalRect.Y < 0)
                            {
                                finalRect.X += finalRect.Width;
                                finalRect.Y = totalHeigh - height;
                            }
                        }
                    }
                }
                return arrangeSize;
            }
        }
    }

    public enum VerticalOrientation
    {
        Top,
        Bottom,
    }

    public static class UnfiFormGridExtension
    {
        public static string SimpleString(this Rect rect)
        {
            return "X = " + (int)rect.X + " Y = " + (int)rect.Y;
        }
    }
}