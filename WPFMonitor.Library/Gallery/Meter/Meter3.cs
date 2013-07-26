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
    /// 仪表3
    /// </summary>
    public class Meter3 : MonitorControl
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
                //AdornerLayer.IsLockScale = true;
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

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum",
            "Minimum", "DecimalDigits", "MainScale", "ViceScale","FontFamily", "ForeColor","ScaleColor" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Paint(availableSize);
            return base.MeasureOverride(availableSize);
        }

        #endregion

        #region 属性
        #region 刻度颜色
        private static readonly DependencyProperty ScaleColorProperty =
            DependencyProperty.Register("ScaleColor",
            typeof(Color), typeof(Meter3), new PropertyMetadata(Colors.Blue));
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
            typeof(Color), typeof(Meter3), new PropertyMetadata(Colors.Blue));
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
            DependencyProperty.Register("Text", typeof(string), typeof(Meter3), new PropertyMetadata("指示表", TextPropertyChanged));

        [DefaultValue("指示表"), Description("文本"), Category("标签")]
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
            var element = d as Meter3;
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter3), new PropertyMetadata(100d, ValuePropertyChanged));


        [DefaultValue(100d), Description("当前值"), Category("里程")]
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
            var element = d as Meter3;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter3), new PropertyMetadata(100d, MaximumPropertyChanged));


        [DefaultValue(100d), Description("最大值"), Category("里程")]
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
            var element = d as Meter3;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter3), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter3;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter3), new PropertyMetadata(1, DecimalDigitsPropertyChanged));

        [DefaultValue(1d), Description("小数位数(范围0-7)"), Category("里程")]
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
            var element = d as Meter3;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter3), new PropertyMetadata(5, MainScalePropertyChanged));

        [DefaultValue(5d), Description("主刻度"), Category("里程")]
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
            var element = d as Meter3;
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
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter3), new PropertyMetadata(3, ViceScalePropertyChanged));

        [DefaultValue(3d), Description("副刻度"), Category("里程")]
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
            var element = d as Meter3;
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
        private Grid _root = new Grid();
        private Canvas _calibrationCanvas = new Canvas();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };
        private Polygon _boder2 = new Polygon();
        private Rectangle _valueRect = new Rectangle();

        #endregion

        #region 构造方法

        public Meter3()
        {

            //var borderBrush = new LinearGradientBrush();
            //borderBrush.StartPoint = new Point();
            //borderBrush.EndPoint = new Point(1d, 1d);
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0d });
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0.4999d });
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x80, 0x80, 0x80), Offset = 0.5d });
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x80, 0x80, 0x80), Offset = 1d });
            //var border = new Rectangle();
            //border.Fill = borderBrush;
            //border.Stroke = new SolidColorBrush(Colors.Black);
            //border.StrokeThickness = 1d;

            //var background = new Rectangle();
            //background.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0));
            //background.Margin = new Thickness(4d);

            //var grid = new Grid();
            //grid.Children.Add(border);
            //grid.Children.Add(background);
            //grid.Children.Add(_canvas);
            //grid.Children.Add(_calibrationCanvas);

            //this.Content = grid;

            var border1 = new Rectangle();
            border1.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x80, 0x80, 0x80));

            _boder2.Fill = new SolidColorBrush(Colors.White);

            var background = new Rectangle();
            background.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0));
            background.Margin = new Thickness(4d);

            //_root.Children.Add(border);
            _root.Children.Add(border1);
            _root.Children.Add(_boder2);
            _root.Children.Add(background);
            _root.Children.Add(_canvas);
            _root.Children.Add(_calibrationCanvas);

            this.Content = _root;

            _label.Text = Text;
            _label.Foreground = new SolidColorBrush(ForeColor);

            _valueRect.Fill = new SolidColorBrush(Colors.Red);
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
                var height = size.Height;

                _boder2.Points.Clear();
                _boder2.Points.Add(new Point(0, 0));
                _boder2.Points.Add(new Point(0, height));
                _boder2.Points.Add(new Point(width, 0));

                _canvas.Children.Clear();

                var x1 = width * 170d / 1046d;
                var x2 = width * 257d / 1046d;
                //var x3 = width * 304d / 1046d;
                //var x4 = width * 363d / 1046d;
                //var x5 = width * 450d / 1046d;
                //var x6 = width * 509d / 1046d;
                var y1 = height * 62d / 628d;
                var y2 = height * 492d / 628d;

                var borderBrush1 = new SolidColorBrush(Colors.Black);
                var borderBrush2 = new SolidColorBrush(Colors.White);

                var line1 = new Line();
                line1.X1 = x1;
                line1.Y1 = y1;
                line1.X2 = x1;
                line1.Y2 = y2;
                line1.Stroke = borderBrush1;
                _canvas.Children.Add(line1);

                var line2 = new Line();
                line2.X1 = x1;
                line2.Y1 = y2;
                line2.X2 = x2;
                line2.Y2 = y2;
                line2.Stroke = borderBrush2;
                _canvas.Children.Add(line2);

                var line3 = new Line();
                line3.X1 = x2;
                line3.Y1 = y1;
                line3.X2 = x2;
                line3.Y2 = y2;
                line3.Stroke = borderBrush2;
                _canvas.Children.Add(line3);

                var line4 = new Line();
                line4.X1 = x1;
                line4.Y1 = y1;
                line4.X2 = x2;
                line4.Y2 = y1;
                line4.Stroke = borderBrush1;
                _canvas.Children.Add(line4);

                PaintCalibration(size);

                _label.Width = width;
                _label.FontSize = height * 49d / 628d;
                _label.SetValue(Canvas.TopProperty, height * 539d / 638d);
                _canvas.Children.Add(_label);

                _canvas.Children.Add(_valueRect);
                _valueRect.Width = x2 - x1 - 2d;
                _valueRect.SetValue(Canvas.LeftProperty, x1 + 1d);
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
                var width = size.Width; // _canvas 有margin 所以要减去
                var height = size.Height;

                var maximum = Maximum;
                var minimum = Minimum;
                var mainScale = MainScale;
                var viceScale = ViceScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var avg = (maximum - minimum) / mainScale;

                //var x1 = width * 170d / 1046d;
                //var x2 = width * 257d / 1046d;
                var x3 = width * 304d / 1046d;
                var x4 = width * 363d / 1046d;
                var x5 = width * 450d / 1046d;
                var x6 = width * 509d / 1046d;
                var y1 = height * 62d / 628d;
                var y2 = height * 492d / 628d;
                var fontSize = height * 29d / 628d;
                var step1 = (y2 - y1) / mainScale;
                var step2 = step1 / (viceScale + 1);

                if (decimalDigits < 0)
                {
                    decimalDigits = 0;
                }
                else if (decimalDigits > 7)
                {
                    decimalDigits = 7;
                }

                _calibrationCanvas.Children.Clear();

                var borderBrush1 = new SolidColorBrush(Colors.Black);
                var borderBrush2 = new SolidColorBrush(Colors.White);

                for (int i = 0; i <= mainScale; i++)
                {
                    var mainLine1 = new Line();
                    mainLine1.Stroke = borderBrush1;
                    mainLine1.StrokeThickness = 1d;
                    mainLine1.X1 = x3;
                    mainLine1.Y1 = y1 + step1 * i - 1d;
                    mainLine1.X2 = x5;
                    mainLine1.Y2 = mainLine1.Y1;
                    _calibrationCanvas.Children.Add(mainLine1);

                    var mainLine2 = new Line();
                    mainLine2.Stroke = borderBrush2;
                    mainLine2.StrokeThickness = 1d;
                    mainLine2.X1 = x3;
                    mainLine2.Y1 = y1 + step1 * i + 1d;
                    mainLine2.X2 = x5;
                    mainLine2.Y2 = mainLine2.Y1;
                    _calibrationCanvas.Children.Add(mainLine2);

                    for (int j = 1; j <= viceScale && i < mainScale; j++)
                    {
                        var viceLine1 = new Line();
                        viceLine1.Stroke = borderBrush1;
                        viceLine1.StrokeThickness = 1d;
                        viceLine1.X1 = x3;
                        viceLine1.Y1 = y1 + step1 * i + j * step2 - 1d;
                        viceLine1.X2 = x4;
                        viceLine1.Y2 = viceLine1.Y1;
                        _calibrationCanvas.Children.Add(viceLine1);

                        var viceLine2 = new Line();
                        viceLine2.Stroke = borderBrush2;
                        viceLine2.StrokeThickness = 1d;
                        viceLine2.X1 = x3;
                        viceLine2.Y1 = y1 + step1 * i + j * step2 + 1d;
                        viceLine2.X2 = x4;
                        viceLine2.Y2 = viceLine2.Y1;
                        _calibrationCanvas.Children.Add(viceLine2);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = borderBrush1;
                    text.Text = Math.Round(maximum - i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, x6);
                    text.SetValue(Canvas.TopProperty, y1 + step1 * i - text.ActualHeight / 2d);
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
            try
            {
                var width = size.Width; // _canvas 有margin 所以要减去
                var height = size.Height;

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
                //if (value < minimum)
                //{
                //    value = minimum;
                //}
                //else if (value > maximum)
                //{
                //    value = maximum;
                //}

                //var x1 = width * 170d / 1046d;
                //var x2 = width * 257d / 1046d;
                //var x3 = width * 304d / 1046d;
                //var x4 = width * 363d / 1046d;
                //var x5 = width * 450d / 1046d;
                //var x6 = width * 509d / 1046d;
                var y1 = height * 62d / 628d;
                var y2 = height * 492d / 628d;

                _valueRect.Height = value * (y2 - y1 - 2d) / (maximum - minimum);
                _valueRect.SetValue(Canvas.TopProperty, y2 - _valueRect.Height - 1d);
            }
            catch { }
        }

        #endregion
    }
}
