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
using WPFMonitor.Library.MonitorSystemGlobal;
using WPFMonitor.Model;
using System.ComponentModel;
using WPFMonitor.Library.ZTControls;

namespace WPFMonitor.Library.Gallery.Meter
{
    /// <summary>
    /// 仪表5
    /// </summary>
    public class Meter5 : MonitorControl
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

        SetSingleProperty tpp = new SetSingleProperty();
        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tpp = new SetSingleProperty();

            tpp.Closing += tpp_Closing;
            tpp.DeviceID = this.ScreenElement.DeviceID.Value;
            tpp.ChanncelID = this.ScreenElement.ChannelNo.Value;
            tpp.LevelNo = this.ScreenElement.LevelNo.Value;
            tpp.ComputeStr = this.ScreenElement.ComputeStr;
            tpp.Init();
            tpp.Show();
        }

        protected void tpp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tpp.IsOK)
            {
                this.ScreenElement.DeviceID = tpp.DeviceID;
                this.ScreenElement.ChannelNo = tpp.ChanncelID;
                this.ScreenElement.LevelNo = tpp.LevelNo;
                this.ScreenElement.ComputeStr = tpp.ComputeStr;
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
                if (name == "Value")
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
                else if (name == "ScaleColor")
                {
                    ScaleColor = Common.StringToColor(value);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);

            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;

            //BackColor = Common.StringToColor(ScreenElement.BackColor);
            //ForeColor = Common.StringToColor(ScreenElement.ForeColor);
            //Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new string[] { "Value", "Maximum", "Minimum", "DecimalDigits",
            "MainScale", "FontFamily", "ForeColor" ,"ScaleColor"};
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
            typeof(Color), typeof(Meter5), new PropertyMetadata(Colors.Blue));
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
        #region 当前值

        private static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Meter5), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter5;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter5), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter5;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter5), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter5;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter5), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter5;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter5), new PropertyMetadata(6, MainScalePropertyChanged));

        [DefaultValue(0d), Description("主刻度"), Category("里程")]
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
            var element = d as Meter5;
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

        #endregion

        #region 变量

        private Canvas _canvas = new Canvas();
        private Canvas _calibrationCanvas = new Canvas();
        private Path _pointPath = new Path();

        #endregion

        #region 构造方法

        public Meter5()
        {

            var borderBrush = new LinearGradientBrush();
            borderBrush.StartPoint = new Point();
            borderBrush.EndPoint = new Point(1d, 1d);
            borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0d });
            borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0.4999d });
            borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x80, 0x80, 0x80), Offset = 0.5d });
            borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x80, 0x80, 0x80), Offset = 1d });
            var border = new Rectangle();
            border.Fill = borderBrush;
            border.Stroke = new SolidColorBrush(Colors.Black);
            border.StrokeThickness = 1d;

            var background = new Rectangle();
            background.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0));
            background.Margin = new Thickness(4d);

            var grid = new Grid();
            grid.Children.Add(border);
            grid.Children.Add(background);
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _pointPath.Fill = new SolidColorBrush(Colors.Red);
            _pointPath.Stroke = new SolidColorBrush(Colors.Black);
            _pointPath.StrokeThickness = 1d;
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
                var width = size.Width; // _canvas 有margin 所以要减去
                _canvas.Children.Clear();

                var brush1 = new SolidColorBrush(Colors.Black);
                var brush2 = new SolidColorBrush(Colors.Red);
                var brush3 = new SolidColorBrush(Color.FromArgb(0xff, 0xAD, 0xA7, 0x9D));
                var brush4 = new SolidColorBrush(Colors.White);

                #region Ellipse and Rectangle

                var ellipseX = width * 34d / 633d;
                var ellipseWidth = width * 63d / 633d;
                var rectangleX = width * 59d / 633d;
                var rectangleWidth = width * 12d / 633d;

                var ellipse1 = new Ellipse();
                ellipse1.SetValue(Canvas.LeftProperty, ellipseX);
                ellipse1.SetValue(Canvas.TopProperty, ellipseX);
                ellipse1.Width = ellipseWidth;
                ellipse1.Height = ellipseWidth;
                ellipse1.StrokeThickness = 1.0;
                ellipse1.Stroke = brush1;
                ellipse1.Fill = brush2;
                _canvas.Children.Add(ellipse1);

                var rectangle1 = new Rectangle();
                rectangle1.SetValue(Canvas.LeftProperty, rectangleX);
                rectangle1.SetValue(Canvas.TopProperty, ellipseX + 1d);
                rectangle1.Width = rectangleWidth;
                rectangle1.Height = ellipseWidth - 2d;
                rectangle1.StrokeThickness = 1.0;
                rectangle1.Stroke = brush1;
                rectangle1.Fill = brush3;
                _canvas.Children.Add(rectangle1);

                var ellipse2 = new Ellipse();
                ellipse2.SetValue(Canvas.LeftProperty, width - ellipseX - ellipseWidth);
                ellipse2.SetValue(Canvas.TopProperty, ellipseX);
                ellipse2.Width = ellipseWidth;
                ellipse2.Height = ellipseWidth;
                ellipse2.StrokeThickness = 1.0;
                ellipse2.Stroke = brush1;
                ellipse2.Fill = brush2;
                _canvas.Children.Add(ellipse2);

                var rectangle2 = new Rectangle();
                rectangle2.SetValue(Canvas.LeftProperty, width - rectangleX - rectangleWidth);
                rectangle2.SetValue(Canvas.TopProperty, ellipseX + 1d);
                rectangle2.Width = rectangleWidth;
                rectangle2.Height = ellipseWidth - 2d;
                rectangle2.StrokeThickness = 1.0;
                rectangle2.Stroke = brush1;
                rectangle2.Fill = brush3;
                _canvas.Children.Add(rectangle2);

                var ellipse3 = new Ellipse();
                ellipse3.SetValue(Canvas.LeftProperty, width - ellipseX - ellipseWidth);
                ellipse3.SetValue(Canvas.TopProperty, width - ellipseX - ellipseWidth);
                ellipse3.Width = ellipseWidth;
                ellipse3.Height = ellipseWidth;
                ellipse3.StrokeThickness = 1.0;
                ellipse3.Stroke = brush1;
                ellipse3.Fill = brush2;
                _canvas.Children.Add(ellipse3);

                var rectangle3 = new Rectangle();
                rectangle3.SetValue(Canvas.LeftProperty, width - rectangleX - rectangleWidth);
                rectangle3.SetValue(Canvas.TopProperty, width - ellipseX - ellipseWidth + 1d);
                rectangle3.Width = rectangleWidth;
                rectangle3.Height = ellipseWidth - 2d;
                rectangle3.StrokeThickness = 1.0;
                rectangle3.Stroke = brush1;
                rectangle3.Fill = brush3;
                _canvas.Children.Add(rectangle3);

                var ellipse4 = new Ellipse();
                ellipse4.SetValue(Canvas.LeftProperty, ellipseX);
                ellipse4.SetValue(Canvas.TopProperty, width - ellipseX - ellipseWidth);
                ellipse4.Width = ellipseWidth;
                ellipse4.Height = ellipseWidth;
                ellipse4.StrokeThickness = 1.0;
                ellipse4.Stroke = brush1;
                ellipse4.Fill = brush2;
                _canvas.Children.Add(ellipse4);

                var rectangle4 = new Rectangle();
                rectangle4.SetValue(Canvas.LeftProperty, rectangleX);
                rectangle4.SetValue(Canvas.TopProperty, width - ellipseX - ellipseWidth + 1d);
                rectangle4.Width = rectangleWidth;
                rectangle4.Height = ellipseWidth - 2d;
                rectangle4.StrokeThickness = 1.0;
                rectangle4.Stroke = brush1;
                rectangle4.Fill = brush3;
                _canvas.Children.Add(rectangle4);

                #endregion

                var x1 = width * 94d / 633d;
                var ellipse = new Ellipse();
                ellipse.SetValue(Canvas.LeftProperty, x1);
                ellipse.SetValue(Canvas.TopProperty, x1);
                ellipse.Width = ellipse.Height = width * 445d / 633d;
                ellipse.Fill = brush4;
                ellipse.Stroke = brush1;
                ellipse.StrokeThickness = 4d;
                _canvas.Children.Add(ellipse);

                _canvas.Children.Add(_pointPath);

                PaintCalibration(size);
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
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var avgMainAngle = 1.5d * Math.PI / mainScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 37d / 633d;

                if (decimalDigits < 0)
                {
                    decimalDigits = 0;
                }
                else if (decimalDigits > 7)
                {
                    decimalDigits = 7;
                }

                _calibrationCanvas.Children.Clear();

                var brush1 = new SolidColorBrush(Colors.Black);
                var brush2 = new SolidColorBrush(Colors.White);
                for (int i = 0; i <= mainScale; i++)
                {
                    var mainAngle = beginAngle - avgMainAngle * i;
                    var mainLine = new Line();
                    mainLine.Stroke = brush1;
                    mainLine.StrokeThickness = 2d;
                    mainLine.X1 = width * Math.Sin(mainAngle) * 222.5d / 633d + width * 0.5d;
                    mainLine.Y1 = width * Math.Cos(mainAngle) * 222.5d / 633d + width * 0.5d;
                    mainLine.X2 = width * Math.Sin(mainAngle) * 197.5d / 633d + width * 0.5d;
                    mainLine.Y2 = width * Math.Cos(mainAngle) * 197.5d / 633d + width * 0.5d;
                    _calibrationCanvas.Children.Add(mainLine);

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = brush2;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, width * Math.Sin(mainAngle) * 267d / 633d - text.ActualWidth / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, width * Math.Cos(mainAngle) * 267d / 633d - text.ActualHeight / 2d + width * 0.5d);
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
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var angle = beginAngle - 1.5d * Math.PI * value / (maximum - minimum);

                var pathFigure = new PathFigure();
                pathFigure.IsClosed = true;
                pathFigure.IsFilled = true;
                pathFigure.StartPoint = new Point(width * Math.Sin(angle) * 165d / 633d + width * 0.5d, width * Math.Cos(angle) * 165d / 633d + width * 0.5d);
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * Math.Sin(angle - 0.7d * Math.PI) * 20d / 611d + width * 0.5d, width * Math.Cos(angle - 0.7d * Math.PI) * 20d / 611d + width * 0.5d) });
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * Math.Sin(angle - Math.PI) * 20d / 611d + width * 0.5d, width * Math.Cos(angle - Math.PI) * 20d / 611d + width * 0.5d) });
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * Math.Sin(angle + 0.7d * Math.PI) * 20d / 611d + width * 0.5d, width * Math.Cos(angle + 0.7d * Math.PI) * 20d / 611d + width * 0.5d) });

                var pathGemometry = new PathGeometry();
                pathGemometry.Figures.Add(pathFigure);

                _pointPath.Data = pathGemometry;
            }
            catch { }
        }

        #endregion
    }
}
