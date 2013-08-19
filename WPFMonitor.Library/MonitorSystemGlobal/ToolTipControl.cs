using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonitorSystem.Controls;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public partial class ToolTipControl : MonitorControl
    {
        const double POINT_HEIGHT = 12d;
        const double POINT_WIDTH = 16d;

        private Grid _layoutRoot = new Grid();
        private Path _borderPath = new Path();
        private PathGeometry _borderPathGeometry = new PathGeometry();
        private PathFigure _borderPathFigure = new PathFigure();
        public readonly Canvas ToolTipCanvas = new Canvas();
        private readonly Canvas _borderCanvas = new Canvas();

        public PointPlace _pointPlace;

        public MonitorControl Target { get; private set; }

        public List<t_Element> ListAllElement { get; set; }

        #region 属性

        public PointPlace PointPlace
        {
            get { return _pointPlace; }
            set { _pointPlace = value; PaintBorder(); }
        }

        #region 是否打开

        private static readonly DependencyProperty IsOpenProperty =
           DependencyProperty.Register("IsOpen",
           typeof(bool), typeof(ToolTipControl), new PropertyMetadata(false, IsOpenPropertyChanged));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); SetAttrByName("IsOpen", value.ToString()); }
        }

        private static void IsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnIsOpenChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        private void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            this.Visibility = IsOpen ? Visibility.Visible : Visibility.Collapsed;
            if (null != AdornerLayer)
            {
                AdornerLayer.IsOpen = newValue;
            }
        }

        #endregion

        #region 填充开始颜色

        private static readonly DependencyProperty FromColorProperty =
           DependencyProperty.Register("FromColor",
           typeof(Color), typeof(ToolTipControl), new PropertyMetadata(Color.FromArgb(0xff, 0x58, 0x50, 0x4E), FromColorPropertyChanged));

        [DefaultValue(""), Description("开始颜色"), Category("背景")]
        public Color FromColor
        {
            get { return (Color)GetValue(FromColorProperty); }
            set { SetValue(FromColorProperty, value); SetAttrByName("FromColor", value.ToString()); }
        }

        private static void FromColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnFromColorChanged((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void OnFromColorChanged(Color oldValue, Color newValue)
        {
            UpdateBackground();
        }

        #endregion

        #region 填充终止颜色

        private static readonly DependencyProperty ToColorProperty =
           DependencyProperty.Register("ToColor",
           typeof(Color), typeof(ToolTipControl), new PropertyMetadata(Color.FromArgb(0xff, 0x26, 0x29, 0x20), ToColorPropertyChanged));

        [DefaultValue(""), Description("终止颜色"), Category("背景")]
        public Color ToColor
        {
            get { return (Color)GetValue(ToColorProperty); }
            set { SetValue(ToColorProperty, value); SetAttrByName("ToColor", value.ToString()); }
        }

        private static void ToColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnToColorChanged((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void OnToColorChanged(Color oldValue, Color newValue)
        {
            UpdateBackground();
        }

        #endregion

        #region 颜色方向

        private static readonly DependencyProperty ColorDirectionProperty =
           DependencyProperty.Register("ColorDirection",
           typeof(Orientation), typeof(ToolTipControl), new PropertyMetadata(Orientation.Horizontal, ColorDirectionPropertyChanged));

        [DefaultValue(""), Description("颜色方向"), Category("背景")]
        public Orientation ColorDirection
        {
            get { return (Orientation)GetValue(ColorDirectionProperty); }
            set { SetValue(ColorDirectionProperty, value); SetAttrByName("ColorDirection", value == Orientation.Horizontal ? 0 : 1); }
        }

        private static void ColorDirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnColorDirectionChanged((Orientation)e.OldValue, (Orientation)e.NewValue);
            }
        }

        private void OnColorDirectionChanged(Orientation oldValue, Orientation newValue)
        {
            UpdateBackground();
        }

        #endregion

        #region 背景图片

        private static readonly DependencyProperty BackImageProperty =
           DependencyProperty.Register("BackImage",
           typeof(string), typeof(ToolTipControl), new PropertyMetadata(BackImagePropertyChanged));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("背景图片"), Category("背景")]
        public string BackImage
        {
            get { return (string)GetValue(BackImageProperty); }
            set { SetValue(BackImageProperty, value); SetAttrByName("BackImage", value.ToString()); }
        }

        private static void BackImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnBackImageChanged((string)e.OldValue, (string)e.NewValue);
            }
        }

        private void OnBackImageChanged(string oldValue, string newValue)
        {
            UpdateBackground();
        }

        private void UpdateBackground()
        {
            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PIC", BackImage  ?? "undefined.tmp");
            if (string.IsNullOrEmpty(BackImage) || !System.IO.File.Exists(url))
            {
                var brush = new LinearGradientBrush();
                if (ColorDirection == Orientation.Horizontal)
                {
                    brush.StartPoint = new Point();
                    brush.EndPoint = new Point(0d, 1d);
                }
                else
                {
                    brush.StartPoint = new Point();
                    brush.EndPoint = new Point(1d, 0d);
                }
                brush.GradientStops.Add(new GradientStop() { Offset = 0d, Color = FromColor });
                brush.GradientStops.Add(new GradientStop() { Offset = 1d, Color = ToColor });
                _borderPath.Fill = brush;
            }
            else
            {
                _borderPath.Fill = new ImageBrush()
                {
                    Stretch = Stretch.UniformToFill,
                    ImageSource = new BitmapImage(new Uri(url, UriKind.Absolute)),
                };
            }
        }

        #endregion

        #region 边线颜色

        private static readonly DependencyProperty StrokeProperty =
           DependencyProperty.Register("Stroke",
           typeof(Color), typeof(ToolTipControl), new PropertyMetadata(Color.FromArgb(0xff, 0x64, 0x64, 0x64), StrokePropertyChanged));

        [DefaultValue(""), Description("边线颜色"), Category("杂项")]
        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); SetAttrByName("Stroke", value.ToString()); }
        }

        private static void StrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnStrokeChanged((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void OnStrokeChanged(Color oldValue, Color newValue)
        {
            _borderPath.Stroke = new SolidColorBrush(newValue);
        }

        #endregion

        #region 边线粗细

        private static readonly DependencyProperty StrokeThicknessProperty =
           DependencyProperty.Register("StrokeThickness",
           typeof(double), typeof(ToolTipControl), new PropertyMetadata(1d, StrokeThicknessPropertyChanged));

        [DefaultValue(""), Description("边线粗细"), Category("杂项")]
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); SetAttrByName("StrokeThickness", value.ToString()); }
        }

        private static void StrokeThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnStrokeThicknessChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void OnStrokeThicknessChanged(double oldValue, double newValue)
        {
            _borderPath.StrokeThickness = StrokeThickness;
            PaintBorder();
        }

        #endregion

        #region 圆角度

        private static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius",
           typeof(double), typeof(ToolTipControl), new PropertyMetadata(10d, CornerRadiusPropertyChanged));

        [DefaultValue(""), Description("圆角度"), Category("杂项")]
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); SetAttrByName("CornerRadius", value.ToString()); }
        }

        private static void CornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnCornerRadiusChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void OnCornerRadiusChanged(double oldValue, double newValue)
        {
            PaintBorder();
        }

        #endregion

        #region 透明度

        private static readonly DependencyProperty TransparentProperty =
           DependencyProperty.Register("Transparent",
           typeof(double), typeof(ToolTipControl), new PropertyMetadata(100d, TransparentPropertyChanged));

        [DefaultValue(""), Description("透明度(0~100)"), Category("杂项")]
        public double Transparent
        {
            get { return (double)GetValue(TransparentProperty); }
            set
            {
                SetValue(TransparentProperty, value);
                if (ScreenElement != null)
                    ScreenElement.Transparent = (int)value;
            }
        }

        private static void TransparentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as ToolTipControl;
            if (null != element)
            {
                element.OnTransparentChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void OnTransparentChanged(double oldValue, double newValue)
        {
            this.Translate = newValue;
        }

        #endregion

        #endregion

        public ToolTipControl(MonitorControl target)
        {
            Target = target;
            _borderPathFigure.IsClosed = true;
            _borderPathGeometry.Figures.Add(_borderPathFigure);
            _borderPath.IsHitTestVisible = false;
            _borderPath.Data = _borderPathGeometry;
            UpdateBackground();
            _borderPath.Stroke = new SolidColorBrush(Stroke);
            _borderPath.StrokeThickness = StrokeThickness;
            _borderCanvas.Children.Add(_borderPath);
            _layoutRoot.Children.Add(_borderCanvas);
            _layoutRoot.Children.Add(ToolTipCanvas);
            Content = _layoutRoot;
            this.ToolTipCanvas.SizeChanged += ToolTipCanvas_SizeChanged;
            this.Selected += ToolTipControl_Selected;
            base.IsToolTip = true;
        }

        private void ToolTipControl_Selected(object sender, EventArgs e)
        {
            MonitorControl.OnUpdatePropertyGrid(new string[0], null);
            MonitorControl.OnUpdatePropertyGrid(BrowsableProperties, GetRootControl()); 
        }

        private void ToolTipCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PaintBorder();
        }

        public bool Contains(double left, double top)
        {
            var margin = (Thickness)ToolTipCanvas.GetValue(MarginProperty);
            var x = Canvas.GetLeft(this) + margin.Left;
            var y = Canvas.GetTop(this) + margin.Top;
            var w = this.Width - margin.Left - margin.Right;
            var h = this.Height - margin.Top - margin.Bottom;
            return new Rect(x, y, w, h).Contains(new Point(left, top));
        }

        public Point GetPosition()
        {
            var margin = (Thickness)ToolTipCanvas.GetValue(MarginProperty);
            return new Point(Canvas.GetLeft(this) + margin.Left, Canvas.GetTop(this) + margin.Top);
        }

        public void SetPosition()
        {
            var parent = this.Parent as Canvas;
            if (null != Target && null != parent)
            {
                var targetLeft = Canvas.GetLeft(Target);
                var targetTop = Canvas.GetTop(Target);
                var targetWidth = Target.Width;
                var targetHeight = Target.Height;
                var tipWidth = this.Width;
                var tipHeight = this.Height;
                var parentWidth = parent.ActualWidth;
                var parentHeight = parent.ActualHeight;

                // 先确定纵坐标
                if (tipHeight < targetTop)    // 上
                {
                    this.SetValue(Canvas.TopProperty, targetTop - tipHeight);
                    SetTipLeft(true, targetLeft, tipWidth, targetWidth, parentWidth);
                }
                else if (targetTop + targetHeight + tipHeight < parentHeight)   // 下
                {
                    this.SetValue(Canvas.TopProperty, targetTop + targetHeight);
                    SetTipLeft(false, targetLeft, tipWidth, targetWidth, parentWidth);
                }
                else if (targetLeft + targetWidth + tipWidth < parentWidth) // 右
                {
                    SetTipTop(false, targetTop, tipHeight, targetHeight, parentHeight);
                    this.SetValue(Canvas.LeftProperty, targetLeft + targetWidth);
                }
                else if (tipWidth < targetLeft) // 左
                {
                    SetTipTop(true, targetTop, tipHeight, targetHeight, parentHeight);
                    this.SetValue(Canvas.LeftProperty, targetLeft - tipWidth);
                }
                else
                {
                    this.SetValue(Canvas.TopProperty, targetTop - tipHeight);
                    SetTipLeft(true, targetLeft, tipWidth, targetWidth, parentWidth);
                }

                if (IsDesignMode && null != this.AdornerLayer)
                {
                    var x = Canvas.GetLeft(this);
                    var y = Canvas.GetTop(this);
                    this.AdornerLayer.SynchroHost(x, y);
                }
            }
        }

        private void SetTipLeft(bool isTop, double targetLeft, double tipWidth, double targetWidth, double parentWidth)
        {
            if (targetLeft + targetWidth / 2d - tipWidth / 2d < 0d)
            {
                this.SetValue(Canvas.LeftProperty, targetLeft + targetWidth);
                this.PointPlace = isTop ? PointPlace.LeftBottom : PointPlace.TopLeft;
            }
            else if (targetLeft + targetWidth / 2d + tipWidth > parentWidth)
            {
                this.SetValue(Canvas.LeftProperty, targetLeft - tipWidth);
                this.PointPlace = isTop ? PointPlace.BottomRight : PointPlace.RightTop;
            }
            else
            {
                this.SetValue(Canvas.LeftProperty, targetLeft + targetWidth / 2d - tipWidth / 2d);
                this.PointPlace = isTop ? PointPlace.BottomMiddle : PointPlace.TopMiddle;
            }
        }

        private void SetTipTop(bool isLeft, double targetTop, double tipHeight, double targetHeight, double parentHeight)
        {
            if (targetTop + targetHeight / 2d - tipHeight / 2d < 0d)
            {
                this.SetValue(Canvas.TopProperty, targetTop + targetHeight);
                this.PointPlace = isLeft ? PointPlace.RightTop : PointPlace.TopLeft;
            }
            else if (targetTop + targetHeight / 2d + tipHeight / 2d > parentHeight)
            {
                this.SetValue(Canvas.TopProperty, targetTop - tipHeight);
                this.PointPlace = isLeft ? PointPlace.BottomRight : PointPlace.LeftBottom;
            }
            else
            {
                this.SetValue(Canvas.TopProperty, targetTop + targetHeight / 2d - tipHeight / 2d);
                this.PointPlace = isLeft ? PointPlace.RightMiddle : PointPlace.LeftMiddle;
            }
        }

        private void PaintBorder()
        {
            try
            {
                _borderPathFigure.Segments.Clear();
                var pointPlace = PointPlace;
                var cornerRadius = CornerRadius;
                var width = this.Width;
                var height = this.Height;
                var size = new Size(cornerRadius, cornerRadius);
                var thickness = StrokeThickness / 2d; // 估算值,不知道为什么

                var radius = CornerRadius - StrokeThickness / 2d;
                if (radius < 0d)
                {
                    radius = 0d;
                }

                switch (pointPlace)
                {
                    case MonitorSystemGlobal.PointPlace.TopLeft:
                        _borderPathFigure.StartPoint = new Point();
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_WIDTH * 2d / 3d + POINT_HEIGHT, POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - cornerRadius, POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width, POINT_HEIGHT * 0.75d + cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height - cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius, height), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_WIDTH / 3d + cornerRadius, height) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(POINT_WIDTH / 3d, height - cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_WIDTH / 3d, POINT_HEIGHT * 1.25d) });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - POINT_WIDTH / 3d - 2d * thickness, height - POINT_HEIGHT * 0.75d - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(POINT_WIDTH / 3d, POINT_HEIGHT * 0.75d, 0d, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.TopMiddle:
                        _borderPathFigure.StartPoint = new Point(cornerRadius, POINT_HEIGHT);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point((width - POINT_WIDTH) / 2d, POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width / 2d, 0d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point((width + POINT_WIDTH) / 2d, POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - cornerRadius, POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width, POINT_HEIGHT + cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height - cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius, height), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(cornerRadius, height) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(0d, height - cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0d, cornerRadius + POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius, POINT_HEIGHT), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - 2d * thickness, height - POINT_HEIGHT - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(0, POINT_HEIGHT, 0d, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.RightTop:
                        _borderPathFigure.StartPoint = new Point(cornerRadius, POINT_HEIGHT * 0.75d);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_WIDTH * 2d / 3d - POINT_HEIGHT, POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, 0d) });
                        //BorderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - POINT_WIDTH / 3d, POINT_HEIGHT * 0.75 + cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_WIDTH / 3d, POINT_HEIGHT * 1.25d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_WIDTH / 3d, height - cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius - POINT_WIDTH / 3d, height), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(cornerRadius, height) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(0d, height - cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0d, cornerRadius + POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius, POINT_HEIGHT * 0.75d), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - POINT_WIDTH / 3d - 2d * thickness, height - POINT_HEIGHT * 0.75d - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(0d, POINT_HEIGHT * 0.75d, 0d, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.RightMiddle:
                        _borderPathFigure.StartPoint = new Point(cornerRadius, 0d);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_HEIGHT - cornerRadius, 0d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - POINT_HEIGHT, cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_HEIGHT, (height - POINT_WIDTH) / 2d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height / 2d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_HEIGHT, (height + POINT_WIDTH) / 2d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_HEIGHT, height - cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius - POINT_HEIGHT, height), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(cornerRadius, height) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(0d, height - cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0d, cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius, 0), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - POINT_HEIGHT - 2d * thickness, height - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(0d, 0d, POINT_HEIGHT, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.BottomRight:
                        _borderPathFigure.StartPoint = new Point(cornerRadius, 0d);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - cornerRadius - POINT_WIDTH / 3d, 0d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - POINT_WIDTH / 3d, cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_WIDTH / 3d, height - POINT_HEIGHT * 1.25d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - POINT_WIDTH * 2d / 3d - POINT_HEIGHT, height - POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(cornerRadius, height - POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(0d, height - cornerRadius - POINT_HEIGHT * 0.75d), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0d, cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius, 0), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - POINT_WIDTH / 3d - 2d * thickness, height - POINT_HEIGHT * 0.75d - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(0d, 0d, 0d, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.BottomMiddle:
                        _borderPathFigure.StartPoint = new Point(cornerRadius, 0d);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - cornerRadius, 0d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width, cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height - cornerRadius - POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius, height - POINT_HEIGHT), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point((width + POINT_WIDTH) / 2d, height - POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width / 2d, height) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point((width - POINT_WIDTH) / 2d, height - POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(cornerRadius, height - POINT_HEIGHT) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(0d, height - cornerRadius - POINT_HEIGHT), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0d, cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius, 0), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - 2d * thickness, height - POINT_HEIGHT - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(0d, 0d, 0d, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.LeftBottom:
                        _borderPathFigure.StartPoint = new Point(cornerRadius + POINT_WIDTH / 3d, 0d);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - cornerRadius, 0d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width, cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height - cornerRadius - POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius, height - POINT_HEIGHT * 0.75d), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_WIDTH * 2d / 3d + POINT_HEIGHT, height - POINT_HEIGHT * 0.75d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0d, height) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_WIDTH / 3d, height - POINT_HEIGHT * 1.25d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_WIDTH / 3d, cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius + POINT_WIDTH / 3d, 0d), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - POINT_WIDTH / 3d - 2d * thickness, height - POINT_HEIGHT * 0.75d - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(POINT_WIDTH / 3d, 0d, 0d, 0d);
                        break;
                    case MonitorSystemGlobal.PointPlace.LeftMiddle:
                        _borderPathFigure.StartPoint = new Point(cornerRadius + POINT_HEIGHT, 0d);
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width - cornerRadius, 0d) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width, cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(width, height - cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(width - cornerRadius, height), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(cornerRadius + POINT_HEIGHT, height) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(POINT_HEIGHT, height - cornerRadius), Size = size, SweepDirection = SweepDirection.Clockwise });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_HEIGHT, (height + POINT_WIDTH) / 2d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(0, height / 2d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_HEIGHT, (height - POINT_WIDTH) / 2d) });
                        _borderPathFigure.Segments.Add(new LineSegment() { Point = new Point(POINT_HEIGHT, cornerRadius) });
                        _borderPathFigure.Segments.Add(new ArcSegment() { Point = new Point(cornerRadius + POINT_HEIGHT, 0), Size = size, SweepDirection = SweepDirection.Clockwise });
                        ToolTipCanvas.Clip = new RectangleGeometry() { RadiusX = radius, RadiusY = radius, Rect = new Rect(thickness, thickness, width - POINT_HEIGHT - 2d * thickness, height - 2d * thickness) };
                        ToolTipCanvas.Margin = new Thickness(POINT_HEIGHT, 0d, 0d, 0d);
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Property error occured at ToolTipControl!");
            }
        }

        #region 重载

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
                AdornerLayer.AllToolTip = false;
                AdornerLayer.IsOpen = IsOpen;
            }
        }

        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.Dispose();
                AdornerLayer = null;

                var children = ToolTipCanvas.Children.OfType<MonitorControl>();
                foreach (var monitor in children)
                {
                    if (null != monitor && monitor.IsDesignMode)
                    {
                        monitor.UnDesignMode();
                    }
                }
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }

        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;

                if (name == "FromColor".ToUpper())
                {
                    FromColor = Common1.StringToColor(value);
                }
                else if (name == "ToColor".ToUpper())
                {
                    ToColor = Common1.StringToColor(value);
                }
                else if (name == "FillDirection".ToUpper())
                {
                    int dirctionValue;
                    if (int.TryParse(value, out dirctionValue))
                    {
                        ColorDirection = dirctionValue == 0 ? Orientation.Horizontal : Orientation.Vertical;
                    }
                    else
                    {
                        ColorDirection = Orientation.Horizontal;
                    }
                }
                else if (name == "BackImage".ToUpper())
                {
                    BackImage = value;
                }
                else if (name == "Stroke".ToUpper())
                {
                    Stroke = Common1.StringToColor(value);
                }
                else if (name == "StrokeThickness".ToUpper())
                {
                    StrokeThickness = Convert.ToDouble(value);
                }
                else if (name == "CornerRadius".ToUpper())
                {
                    CornerRadius = Convert.ToDouble(value);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            //this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            //this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            Transparent = ScreenElement.Transparent.HasValue ? ScreenElement.Transparent.Value : 100d;
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        private string[] m_BrowsableProperties = new string[] { "Width", "Height", "FromColor", "ToColor", "ColorDirection", "BackImage", "Stroke", "StrokeThickness", "Transparent", "CornerRadius" };
        public override string[] BrowsableProperties
        {
            get
            {
                return m_BrowsableProperties;
            }
            set
            {
                m_BrowsableProperties = value;
            }
        }

        #endregion
    }

    public enum PointPlace
    {
        TopLeft,
        TopHalfLeft,
        TopMiddle,
        TopHalfRight,
        RightTop,
        RightHalfTop,
        RightMiddle,
        RightHalfBottom,
        BottomRight,
        BottomHalfRight,
        BottomMiddle,
        BottomHalfLeft,
        LeftBottom,
        LeftHalfBottom,
        LeftMiddle,
        LeftHalfTop
    }
}
