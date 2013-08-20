using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using System.ComponentModel;
using MonitorSystem.ZTControls;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.Gallery.Meter
{
    /// <summary>
    /// 仪表7
    /// </summary>
    public class Meter7 : MonitorControl
    {

        #region 重载

        public override void SetChannelValue(float fValue)
        {
            Value = (double)fValue;
        }

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
                AdornerLayer.IsLockScale = true;
                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }

        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.ClearValue(ContextMenuService.ContextMenuProperty);
                AdornerLayer.Dispose();
                AdornerLayer = null;
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
                string name = pro.PropertyName.Trim();
                string value = pro.PropertyValue.Trim();
                if (name == "Text")
                {
                    Text = value;
                }
                else if (name == "Value")
                {
                    Value = double.Parse(value);
                }
                else if (name == "Maximum")
                {
                    Maximum = double.Parse(value);
                }
                else if (name == "Minimum")
                {
                    Minimum = double.Parse(value);
                }
                else if (name == "DecimalDigits")
                {
                    DecimalDigits = int.Parse(value);
                }
                else if (name == "MainScale")
                {
                    MainScale = int.Parse(value);
                }
                else if (name == "ViceScale")
                {
                    ViceScale = int.Parse(value);
                }
                else if (name == "ScaleColor")
                {
                    ScaleColor = Common1.StringToColor(value);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);

            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;

            //BackColor = Common1.StringToColor(ScreenElement.BackColor);
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
            //Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum", "Minimum",
            "DecimalDigits", "MainScale", "ViceScale","FontFamily", "ForeColor","ScaleColor" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #endregion

        #region 属性
        #region 刻度颜色
        private static readonly DependencyProperty ScaleColorProperty =
            DependencyProperty.Register("ScaleColor",
            typeof(Color), typeof(Meter7), new PropertyMetadata(Colors.Blue));
        [DefaultValue(""), Description("刻度颜色"), Category("外观")]
        public Color ScaleColor
        {
            get { return (Color)this.GetValue(ScaleColorProperty); }
            set
            {
                this.SetValue(ScaleColorProperty, value);
                SetAttrByName("ScaleColor", value.ToString());

            }
        }
        #endregion
        #region 颜色

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(Meter7), new PropertyMetadata(Colors.Blue));
        [DefaultValue(""), Description("前景色"), Category("外观")]
        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set
            {
                this.SetValue(ForeColorProperty, value);
                if (ScreenElement != null)
                    ScreenElement.ForeColor = value.ToString();

                _label.Foreground = new SolidColorBrush(value);
            }
        }

        #endregion

        #region 标签

        private static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Meter7), new PropertyMetadata("仪表", TextPropertyChanged));

        [DefaultValue("仪表"), Description("文本"), Category("标签")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                SetAttrByName("Text", value.ToString());
            }
        }

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.Text_Changed((string)e.OldValue, (string)e.NewValue);
            }
        }

        private void Text_Changed(string oldValue, string newValue)
        {
            _label.Text = newValue;
        }

        #endregion

        #region 当前值

        private static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Meter7), new PropertyMetadata(0d, ValuePropertyChanged));


        [DefaultValue(0d), Description("当前值"), Category("里程")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);

                SetAttrByName("Value", value.ToString());
            }
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.Value_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Value_Changed(double oldValue, double newValue)
        {
            PaintPoint(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 最大值

        private static DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter7), new PropertyMetadata(60d, MaximumPropertyChanged));


        [DefaultValue(60d), Description("最大值"), Category("里程")]
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set
            {
                SetValue(MaximumProperty, value);
                SetAttrByName("Maximum", value.ToString());
            }
        }

        private static void MaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.Maximum_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Maximum_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
            PaintPoint(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 最小值

        private static DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter7), new PropertyMetadata(0d, MinimumPropertyChanged));


        [DefaultValue(0d), Description("最小值"), Category("里程")]
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set
            {
                SetValue(MinimumProperty, value);
                SetAttrByName("Minimum", value.ToString());
            }
        }

        private static void MinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.Minimum_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Minimum_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
            PaintPoint(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 小数位数

        private static DependencyProperty DecimalDigitsProperty =
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter7), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

        [DefaultValue(0d), Description("小数位数(范围0-7)"), Category("里程")]
        public int DecimalDigits
        {
            get { return (int)GetValue(DecimalDigitsProperty); }
            set
            {
                SetValue(DecimalDigitsProperty, value);
                SetAttrByName("DecimalDigits", value.ToString());
            }
        }

        private static void DecimalDigitsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.DecimalDigits_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void DecimalDigits_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 主刻度

        private static DependencyProperty MainScaleProperty =
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter7), new PropertyMetadata(3, MainScalePropertyChanged));

        [DefaultValue(3d), Description("主刻度"), Category("里程")]
        public int MainScale
        {
            get { return (int)GetValue(MainScaleProperty); }
            set 
            {
                SetValue(MainScaleProperty, value);
                SetAttrByName("MainScale", value.ToString());
            }
        }

        private static void MainScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.MainScale_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void MainScale_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 副刻度

        private static DependencyProperty ViceScaleProperty =
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter7), new PropertyMetadata(4, ViceScalePropertyChanged));

        [DefaultValue(0d), Description("副刻度"), Category("里程")]
        public int ViceScale
        {
            get { return (int)GetValue(ViceScaleProperty); }
            set 
            {
                SetValue(ViceScaleProperty, value);
                SetAttrByName("ViceScale", value.ToString());
            }
        }

        private static void ViceScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter7;
            if (null != element)
            {
                element.ViceScale_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void ViceScale_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #endregion

        #region 变量

        private Canvas _canvas = new Canvas();
        private Canvas _calibrationCanvas = new Canvas();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };
        private Line _pointLine = new Line() { StrokeThickness = 2d, Stroke = new SolidColorBrush(Colors.Red) };

        #endregion

        #region 构造方法

        public Meter7()
        {
            var grid = new Grid();
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _label.Text = Text;
            _label.Foreground = new SolidColorBrush(ForeColor);
            this.SizeChanged += Meter_SizeChanged;
        }

        #endregion

        #region 私有方法

        private void Meter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width;
            Paint(new Size(this.ActualWidth, this.ActualHeight));
        }

        private void Paint(Size size)
        {
            try
            {
                _canvas.Children.Clear();
                var width = size.Width;

                var brush1 = new SolidColorBrush(Colors.Black);
                var brush2 = new SolidColorBrush(Colors.White);
                var brush3 = new SolidColorBrush(Color.FromArgb(0xff, 0x64, 0x58, 0x45));

                var border1 = new Rectangle();
                border1.Stroke = brush1;
                border1.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x80, 0x80, 0x80));
                border1.Width = width;
                border1.Height = width * 433d / 657d;
                _canvas.Children.Add(border1);

                var border2 = new Polygon();
                border2.Fill = new SolidColorBrush(Colors.White);
                border2.Points.Clear();
                border2.Points.Add(new Point(1d, 1d));
                border2.Points.Add(new Point(1d, border1.Height - 2d));
                border2.Points.Add(new Point(width -2d, 1d));
                _canvas.Children.Add(border2);

                var background1 = new Rectangle();
                background1.Width = width - 10d;
                background1.Height = border1.Height - 10d;
                background1.SetValue(Canvas.LeftProperty, 5d);
                background1.SetValue(Canvas.TopProperty, 5d);
                background1.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0));
                _canvas.Children.Add(background1);

                #region path1

                var pathFigure1 = new PathFigure();
                pathFigure1.StartPoint = new Point(Math.Sin(236.25d * Math.PI / 180d) * width * 346d / 657d + width * 0.5d, Math.Cos(236.25d * Math.PI / 180d) * width * 346d / 657d + width * 469d / 657d);
                pathFigure1.IsClosed = true;
                pathFigure1.IsFilled = true;

                var arcSegment11 = new ArcSegment();
                arcSegment11.Point = new Point(Math.Sin(123.75d * Math.PI / 180d) * width * 346d / 657d + width * 0.5d, Math.Cos(123.75d * Math.PI / 180d) * width * 346d / 657d + width * 469d / 657d);
                arcSegment11.Size = new Size(width * 346d / 657d, width * 346d / 657d);
                arcSegment11.SweepDirection = SweepDirection.Clockwise;
                pathFigure1.Segments.Add(arcSegment11);

                var lineSegment1 = new LineSegment();
                lineSegment1.Point = new Point(Math.Sin(123.75d * Math.PI / 180d) * width * 303d / 657d + width * 0.5d, Math.Cos(123.75d * Math.PI / 180d) * width * 303d / 657d + width * 469d / 657d);
                pathFigure1.Segments.Add(lineSegment1);

                var arcSegment12 = new ArcSegment();
                arcSegment12.Point = new Point(Math.Sin(236.25d * Math.PI / 180d) * width * 303d / 657d + width * 0.5d, Math.Cos(236.25d * Math.PI / 180d) * width * 303d / 657d + width * 469d / 657d);
                arcSegment12.Size = new Size(width * 303d / 657d, width * 303d / 657d);
                arcSegment12.SweepDirection = SweepDirection.Counterclockwise;
                pathFigure1.Segments.Add(arcSegment12);

                var pathGeometry1 = new PathGeometry();
                pathGeometry1.Figures.Add(pathFigure1);
                var path1 = new Path();
                path1.Fill = new SolidColorBrush(Colors.Red);
                path1.Data = pathGeometry1;
                _canvas.Children.Add(path1);

                #endregion

                #region path2

                var pathFigure2 = new PathFigure();
                pathFigure2.StartPoint = new Point(Math.Sin(221.25d * Math.PI / 180d) * width * 346d / 657d + width * 0.5d, Math.Cos(221.25d * Math.PI / 180d) * width * 346d / 657d + width * 469d / 657d);
                pathFigure2.IsClosed = true;
                pathFigure2.IsFilled = true;

                var arcSegment21 = new ArcSegment();
                arcSegment21.Point = new Point(Math.Sin(138.75d * Math.PI / 180d) * width * 346d / 657d + width * 0.5d, Math.Cos(138.75d * Math.PI / 180d) * width * 346d / 657d + width * 469d / 657d);
                arcSegment21.Size = new Size(width * 346d / 657d, width * 346d / 657d);
                arcSegment21.SweepDirection = SweepDirection.Clockwise;
                pathFigure2.Segments.Add(arcSegment21);

                var lineSegment2 = new LineSegment();
                lineSegment2.Point = new Point(Math.Sin(138.75d * Math.PI / 180d) * width * 303d / 657d + width * 0.5d, Math.Cos(138.75d * Math.PI / 180d) * width * 303d / 657d + width * 469d / 657d);
                pathFigure2.Segments.Add(lineSegment2);

                var arcSegment22 = new ArcSegment();
                arcSegment22.Point = new Point(Math.Sin(221.25d * Math.PI / 180d) * width * 303d / 657d + width * 0.5d, Math.Cos(221.25d * Math.PI / 180d) * width * 303d / 657d + width * 469d / 657d);
                arcSegment22.Size = new Size(width * 303d / 657d, width * 303d / 657d);
                arcSegment22.SweepDirection = SweepDirection.Counterclockwise;
                pathFigure2.Segments.Add(arcSegment22);

                var pathGeometry2 = new PathGeometry();
                pathGeometry2.Figures.Add(pathFigure2);
                var path2 = new Path();
                path2.Fill = new SolidColorBrush(Colors.Yellow);
                path2.Data = pathGeometry2;
                _canvas.Children.Add(path2);

                #endregion

                #region path3

                var pathFigure3 = new PathFigure();
                pathFigure3.StartPoint = new Point(Math.Sin(213.75d * Math.PI / 180d) * width * 346d / 657d + width * 0.5d, Math.Cos(213.75d * Math.PI / 180d) * width * 346d / 657d + width * 469d / 657d);
                pathFigure3.IsClosed = true;
                pathFigure3.IsFilled = true;

                var arcSegment31 = new ArcSegment();
                arcSegment31.Point = new Point(Math.Sin(146.25d * Math.PI / 180d) * width * 346d / 657d + width * 0.5d, Math.Cos(146.25d * Math.PI / 180d) * width * 346d / 657d + width * 469d / 657d);
                arcSegment31.Size = new Size(width * 346d / 657d, width * 346d / 657d);
                arcSegment31.SweepDirection = SweepDirection.Clockwise;
                pathFigure3.Segments.Add(arcSegment31);

                var lineSegment3 = new LineSegment();
                lineSegment3.Point = new Point(Math.Sin(146.25d * Math.PI / 180d) * width * 303d / 657d + width * 0.5d, Math.Cos(146.25d * Math.PI / 180d) * width * 303d / 657d + width * 469d / 657d);
                pathFigure3.Segments.Add(lineSegment3);

                var arcSegment32 = new ArcSegment();
                arcSegment32.Point = new Point(Math.Sin(213.75d * Math.PI / 180d) * width * 303d / 657d + width * 0.5d, Math.Cos(213.75d * Math.PI / 180d) * width * 303d / 657d + width * 469d / 657d);
                arcSegment32.Size = new Size(width * 303d / 657d, width * 303d / 657d);
                arcSegment32.SweepDirection = SweepDirection.Counterclockwise;
                pathFigure3.Segments.Add(arcSegment32);

                var pathGeometry3 = new PathGeometry();
                pathGeometry3.Figures.Add(pathFigure3);
                var path3 = new Path();
                path3.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xFF, 0x00));
                path3.Data = pathGeometry3;
                _canvas.Children.Add(path3);

                #endregion

                PaintCalibration(size);

                _pointLine.X1 = width * 0.5d;
                _pointLine.Y1 = width * 469d / 657d;

                _canvas.Children.Add(_pointLine);

                var background2 = new Rectangle();
                background2.StrokeThickness = 3d;
                background2.Stroke = brush3;
                background2.Fill = brush1;
                background2.SetValue(Canvas.TopProperty, border1.Height - 1d);
                background2.Width = width;
                background2.Height = width - border1.Height + 1d;
                _canvas.Children.Add(background2);

                var innerAxisFill = new RadialGradientBrush();
                innerAxisFill.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0xAD, 0xA7, 0x9D), Offset = 0d });
                innerAxisFill.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x32, 0x23, 0x0A), Offset = 1d });
                var ellipse = new Ellipse();
                ellipse.Fill = innerAxisFill;
                ellipse.SetValue(Canvas.LeftProperty, width * 276.5d / 657d);
                ellipse.SetValue(Canvas.TopProperty, width * 417d / 657d);
                ellipse.Width = ellipse.Height = width * 104d / 657d;
                _canvas.Children.Add(ellipse);

                var line = new Line();
                line.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0xBE, 0xB9, 0xB1));
                line.StrokeThickness = 3d;
                line.X1 = width * 300d / 644d;
                line.X2 = width * 348d / 644d;
                line.Y1 = width * 435d / 644d;
                line.Y2 = width * 484d / 644d;
                _canvas.Children.Add(line);

            }
            catch { }
        }

        /// <summary>
        /// 绘制刻度
        /// </summary>
        /// <param name="size"></param>
        private void PaintCalibration(Size size)
        {
            try
            {
                var width = size.Width;

                var maximum = Maximum;
                var minimum = Minimum;
                var mainScale = MainScale;
                var viceScale = ViceScale + 1;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 236.25d * Math.PI / 180d;
                var avgMainAngle = 112.5 * Math.PI / (180d * mainScale);
                var avgViceAngle = avgMainAngle / (viceScale);
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 36d / 657d;

                if (decimalDigits < 0)
                {
                    decimalDigits = 0;
                }
                else if (decimalDigits > 7)
                {
                    decimalDigits = 7;
                }

                _calibrationCanvas.Children.Clear();

                var brush = new SolidColorBrush(Colors.Black);
                for (int i = 0; i <= mainScale; i++)
                {
                    var mainAngle = beginAngle - avgMainAngle * i;
                    var mainLine = new Line();
                    mainLine.Stroke = brush;
                    mainLine.StrokeThickness = 2d;
                    mainLine.X1 = Math.Sin(mainAngle) * width * 346d / 657d + width * 0.5d;
                    mainLine.Y1 = Math.Cos(mainAngle) * width * 346d / 657d + width * 469d / 657d;
                    mainLine.X2 = Math.Sin(mainAngle) * width * 303d / 657d + width * 0.5d;
                    mainLine.Y2 = Math.Cos(mainAngle) * width * 303d / 657d + width * 469d / 657d;
                    _calibrationCanvas.Children.Add(mainLine);

                    for (int j = 1; j < viceScale && i < mainScale; j++)
                    {
                        var viceAngle = mainAngle - j * avgViceAngle;
                        var viceLine = new Line();
                        viceLine.Stroke = brush;
                        viceLine.StrokeThickness = 1d;
                        viceLine.X1 = Math.Sin(viceAngle) * width * 342d / 657d + width * 0.5d;
                        viceLine.Y1 = Math.Cos(viceAngle) * width * 342d / 657d + width * 469d / 657d;
                        viceLine.X2 = Math.Sin(viceAngle) * width * 303d / 657d + width * 0.5d;
                        viceLine.Y2 = Math.Cos(viceAngle) * width * 303d / 657d + width * 469d / 657d;
                        _calibrationCanvas.Children.Add(viceLine);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = brush;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    Size textSize = text.MeasureTextSize();
                    text.SetValue(Canvas.LeftProperty, Math.Sin(mainAngle) * width * 380d / 657d - textSize.Width / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, Math.Cos(mainAngle) * width * 380d / 657d - textSize.Height / 2d + width * 469d / 657d);
                    _calibrationCanvas.Children.Add(text);
                }

                PaintPoint(size);
            }
            catch { }
        }

        /// <summary>
        /// 绘制当前值
        /// </summary>
        private void PaintPoint(Size size)
        {
            //_valueCanvas.Width = size.Width;
            //_valueCanvas.Height = size.Height;
            //_valueCanvas.Children.Clear();

            try
            {
                var width = size.Width;

                var value = Value;
                var maximum = Maximum;
                var minimum = Minimum;
                if (value < minimum)
                {
                    value = minimum;
                }
                if (value > maximum)
                {
                    value = maximum;
                }
                value -= minimum;
                var mainScale = MainScale;
                var viceScale = ViceScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 236.25d * Math.PI / 180d;
                var angle = beginAngle - 112.5 * Math.PI * value / ( 180d * (maximum - minimum));

                _pointLine.X2 = Math.Sin(angle) * width * 380d / 657d + width * 0.5d;
                _pointLine.Y2 = Math.Cos(angle) * width * 380d / 657d + width * 469d / 657d;
            }
            catch { }
        }

        #endregion
    }
}
