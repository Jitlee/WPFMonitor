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
    /// 仪表1
    /// </summary>
    public class Meter1 : MonitorControl
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
            return this._canvas;
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
                else if (name == "FontFamily")
                {
                    FontFamily = new FontFamily(Common1.GetFontEn(value));
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

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum", 
            "Minimum", "DecimalDigits", "MainScale", "ViceScale" ,"FontFamily", "ForeColor","ScaleColor"};
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
            typeof(Color), typeof(Meter1), new PropertyMetadata(Colors.Blue));
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
            typeof(Color), typeof(Meter1), new PropertyMetadata(Colors.Blue));
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
            DependencyProperty.Register("Text", typeof(string), typeof(Meter1), new PropertyMetadata("仪表", TextPropertyChanged));

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
            var element = d as Meter1;
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter1), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter1;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter1), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter1;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter1), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter1;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter1), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter1;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter1), new PropertyMetadata(6, MainScalePropertyChanged));

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
            var element = d as Meter1;
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
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter1), new PropertyMetadata(4, ViceScalePropertyChanged));

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
            var element = d as Meter1;
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
        private Path _pointPath = new Path();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };

        #endregion

        #region 构造方法

        public Meter1()
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

            _label.Text = Text;
            _label.Foreground = new SolidColorBrush(ForeColor);
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

                var outerBorderEllipse = new Ellipse();
                outerBorderEllipse.Stroke = new SolidColorBrush(Colors.Black);
                outerBorderEllipse.StrokeThickness = 1d;
                outerBorderEllipse.SetValue(Canvas.LeftProperty, width * 0.2d);
                outerBorderEllipse.SetValue(Canvas.TopProperty, width * 0.2d);
                outerBorderEllipse.Width = width * 0.6d;
                outerBorderEllipse.Height = width * 0.6d;

                _canvas.Children.Add(outerBorderEllipse);

                var innerBorderEllipse = new Ellipse();
                innerBorderEllipse.Stroke = new SolidColorBrush(Colors.White);
                innerBorderEllipse.StrokeThickness = 1d;
                innerBorderEllipse.SetValue(Canvas.LeftProperty, width * 0.2d + 1d);
                innerBorderEllipse.SetValue(Canvas.TopProperty, width * 0.2d + 1d);
                innerBorderEllipse.Width = width * 0.6d - 2d;
                innerBorderEllipse.Height = width * 0.6d - 2d;

                _canvas.Children.Add(innerBorderEllipse);

                var backgroundEllipse = new Ellipse();
                backgroundEllipse.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x7C, 0x73, 0x63));
                backgroundEllipse.SetValue(Canvas.LeftProperty, width * 0.2d + 2d);
                backgroundEllipse.SetValue(Canvas.TopProperty, width * 0.2d + 2d);
                backgroundEllipse.Width = width * 0.6d - 4d;
                backgroundEllipse.Height = width * 0.6d - 4d;

                _canvas.Children.Add(backgroundEllipse);

                _canvas.Children.Add(_pointPath);

                var outerAxis = new Ellipse();
                outerAxis.Fill = new SolidColorBrush(Colors.Blue);
                outerAxis.Stroke = new SolidColorBrush(Colors.Black);
                outerAxis.StrokeThickness = 1d;
                outerAxis.SetValue(Canvas.LeftProperty, width * 0.4d);
                outerAxis.SetValue(Canvas.TopProperty, width * 0.4d);
                outerAxis.Width = width * 0.2d;
                outerAxis.Height = width * 0.2d;
                _canvas.Children.Add(outerAxis);

                var innerAxisFill = new RadialGradientBrush();
                innerAxisFill.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x84, 0x7B, 0x6C), Offset = 0d });
                innerAxisFill.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x12, 0x00, 0x00), Offset = 1d });
                var innerAxis = new Ellipse();
                innerAxis.Fill = innerAxisFill;
                innerAxis.Stroke = new SolidColorBrush(Colors.Black);
                innerAxis.StrokeThickness = 1d;
                innerAxis.SetValue(Canvas.LeftProperty, width * 0.42d);
                innerAxis.SetValue(Canvas.TopProperty, width * 0.42d);
                innerAxis.Width = width * 0.16d;
                innerAxis.Height = width * 0.16d;
                _canvas.Children.Add(innerAxis);

                PaintCalibration(size);

                _label.Width = width;
                _label.FontSize = width * 0.0562913907284768d;
                _label.SetValue(Canvas.TopProperty, width * 0.85d);
                _canvas.Children.Add(_label);
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
                _canvas.Height = width;
                _canvas.Width = width;
                var maximum = Maximum;
                var minimum = Minimum;
                var mainScale = MainScale;
                var viceScale = ViceScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var avgMainAngle = 1.5d * Math.PI / mainScale;
                var avgViceAngle = avgMainAngle / viceScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 0.0562913907284768d;

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
                    mainLine.X1 = width * 0.3375d * Math.Sin(mainAngle) + width * 0.5d;
                    mainLine.Y1 = width * 0.3375d * Math.Cos(mainAngle) + width * 0.5d;
                    mainLine.X2 = width * 0.3d * Math.Sin(mainAngle) + width * 0.5d;
                    mainLine.Y2 = width * 0.3d * Math.Cos(mainAngle) + width * 0.5d;
                    _calibrationCanvas.Children.Add(mainLine);

                    for (int j = 1; j < viceScale && i < mainScale; j++)
                    {
                        var viceAngle = mainAngle - j * avgViceAngle;
                        var viceLine = new Line();
                        viceLine.Stroke = brush;
                        viceLine.StrokeThickness = 1d;
                        viceLine.X1 = width * 0.33d * Math.Sin(viceAngle) + width * 0.5d;
                        viceLine.Y1 = width * 0.33d * Math.Cos(viceAngle) + width * 0.5d;
                        viceLine.X2 = width * 0.3d * Math.Sin(viceAngle) + width * 0.5d;
                        viceLine.Y2 = width * 0.3d * Math.Cos(viceAngle) + width * 0.5d;
                        _calibrationCanvas.Children.Add(viceLine);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    Size textSize = text.MeasureTextSize();
                    text.SetValue(Canvas.LeftProperty, width * 0.38d * Math.Sin(mainAngle) - textSize.Width / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, width * 0.38d * Math.Cos(mainAngle) - textSize.Height / 2d + width * 0.5d);
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
                var beginAngle = 1.75d * Math.PI;
                var angle = beginAngle - 1.5d * Math.PI * value / (maximum - minimum);

                var pathFigure = new PathFigure();
                pathFigure.IsClosed = true;
                pathFigure.IsFilled = true;
                pathFigure.StartPoint = new Point(width * 0.25d * Math.Sin(angle) + width * 0.5d, width * 0.25d * Math.Cos(angle) + width * 0.5d);
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * 0.1d * Math.Sin(angle - 0.05 * Math.PI) + width * 0.5d, width * 0.1d * Math.Cos(angle - 0.05 * Math.PI) + width * 0.5d) });
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * 0.1d * Math.Sin(angle + 0.05 * Math.PI) + width * 0.5d, width * 0.1d * Math.Cos(angle + 0.05 * Math.PI) + width * 0.5d) });

                var pathGemometry = new PathGeometry();
                pathGemometry.Figures.Add(pathFigure);

                _pointPath.Data = pathGemometry;
            }
            catch { }
        }

        #endregion
    }
}
