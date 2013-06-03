using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using MonitorSystem.MonitorSystemGlobal;
using MonitorSystem.ZTControls;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.Gallery.Meter
{
    /// <summary>
    /// 仪表2
    /// </summary>
    public class Meter2 : MonitorControl
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
                else if (name == "Scale")
                {
                    Scale = int.Parse(value);
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

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum", "Minimum", "Scale", 
            "FontFamily", "ForeColor","ScaleColor" };

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
            typeof(Color), typeof(Meter2), new PropertyMetadata(Colors.Blue));
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
            typeof(Color), typeof(Meter2), new PropertyMetadata(Colors.Blue));
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
            DependencyProperty.Register("Text", typeof(string), typeof(Meter2), new PropertyMetadata("仪表", TextPropertyChanged));

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
            var element = d as Meter2;
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter2), new PropertyMetadata(60d, ValuePropertyChanged));


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
            var element = d as Meter2;
            if (null != element)
            {
                element.Value_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Value_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 最大值

        private static DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter2), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter2;
            if (null != element)
            {
                element.Maximum_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Maximum_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 最小值

        private static DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter2), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter2;
            if (null != element)
            {
                element.Minimum_Changed((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void Minimum_Changed(double oldValue, double newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 刻度数

        private static DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(int), typeof(Meter2), new PropertyMetadata(10, ScalePropertyChanged));

        [DefaultValue(0d), Description("刻度数"), Category("里程")]
        public int Scale
        {
            get { return (int)GetValue(ScaleProperty); }
            set 
            {
                SetValue(ScaleProperty, value);
                SetAttrByName("Scale", value.ToString());
            }
        }

        private static void ScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter2;
            if (null != element)
            {
                element.Scale_Changed((int)e.OldValue, (int)e.NewValue);
            }
        }

        private void Scale_Changed(int oldValue, int newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #endregion

        #region 变量

        /// <summary>
        /// 长宽锁定比例
        /// </summary>
        const double LOCK_SCALE = 0.6948356807511737d;
        private Grid _root = new Grid();
        private Canvas _canvas = new Canvas();
        private Canvas _calibrationCanvas = new Canvas();
        private Path _pointPath = new Path();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };
        private Polygon _boder2 = new Polygon();

        #endregion

        #region 构造方法

        public Meter2()
        {
            //var borderBrush = new LinearGradientBrush();
            //borderBrush.StartPoint = new Point(0d, 0d);
            //borderBrush.EndPoint = new Point(LOCK_SCALE, 1d / LOCK_SCALE);
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0d });
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0.4999d });
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x80, 0x80, 0x80), Offset = 0.5d });
            //borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x80, 0x80, 0x80), Offset = 1d });
            //var border = new Rectangle();
            //border.Fill = borderBrush;
            //border.Stroke = new SolidColorBrush(Colors.Black);
            //border.StrokeThickness = 1d;
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
            this.Height = e.NewSize.Width * LOCK_SCALE;
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

                var lineBrush1 = new SolidColorBrush(Color.FromArgb(0xff, 0x7C, 0x73, 0x63));
                var lineBrush2 = new SolidColorBrush(Colors.White);

                var x1 = width * 0.0781710914454277d;
                var y1 = height * 0.6039387308533917d;
                var x2 = width - x1;
                var y2 = height * 0.8468271334792123d;

                var line1 = new Line();
                line1.X1 = x1;
                line1.X2 = x2;
                line1.Y1 = line1.Y2 = y1;
                line1.Stroke = lineBrush1;
                _canvas.Children.Add(line1);

                var line2 = new Line();
                line2.X1 = line2.X2 = x1;
                line2.Y1 = y1;
                line2.Y2 = y2;
                line2.Stroke = lineBrush1;
                _canvas.Children.Add(line2);

                var line3 = new Line();
                line3.X1 = x1;
                line3.X2 = x2;
                line3.Y1 = line3.Y2 = y2;
                line3.Stroke = lineBrush2;
                _canvas.Children.Add(line3);

                var line4 = new Line();
                line4.X1 = line4.X2 = x2;
                line4.Y1 = y1;
                line4.Y2 = y2;
                line4.Stroke = lineBrush2;
                _canvas.Children.Add(line4);

                _label.Width = width;
                _label.FontSize = width * 0.0929203539823009d;
                _label.SetValue(Canvas.TopProperty, height * 0.633019693654267d);
                _canvas.Children.Add(_label);

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

                var scale = Scale;

                if (scale < 1)
                {
                    scale = 1;
                }

                var x1 = width * 0.0781710914454277d;
                var y1 = height * 0.1312910284463895d;
                var x2 = width - x1;
                var y2 = height * 0.512035010940919d;
                var gap = width * 0.0176991150442478d;
                var cellWidth = ((x2 - x1) + gap) / scale;

                _calibrationCanvas.Children.Clear();

                var lineBrush1 = new SolidColorBrush(Color.FromArgb(0xff, 0x7C, 0x73, 0x63));
                var lineBrush2 = new SolidColorBrush(Colors.White);
                var fillBrush = new SolidColorBrush(Colors.Black);
                for (int i = 0; i < scale; i++)
                {

                    var line1 = new Line();
                    line1.X1 = x1 + i * cellWidth;
                    line1.X2 = line1.X1 + cellWidth - gap;
                    line1.Y1 = line1.Y2 = y1;
                    line1.Stroke = lineBrush1;
                    _calibrationCanvas.Children.Add(line1);

                    var line2 = new Line();
                    line2.X1 = line2.X2 = line1.X1;
                    line2.Y1 = y1;
                    line2.Y2 = y2;
                    line2.Stroke = lineBrush1;
                    _calibrationCanvas.Children.Add(line2);

                    var line3 = new Line();
                    line3.X1 = line1.X1;
                    line3.X2 = line1.X2;
                    line3.Y1 = line3.Y2 = y2;
                    line3.Stroke = lineBrush2;
                    _calibrationCanvas.Children.Add(line3);

                    var line4 = new Line();
                    line4.X1 = line4.X2 = line1.X2;
                    line4.Y1 = y1;
                    line4.Y2 = y2;
                    line4.Stroke = lineBrush2;
                    _calibrationCanvas.Children.Add(line4);
                }

                var valueScale = (int)Math.Ceiling(value * scale / (maximum - minimum));

                for (int i = 0; i < valueScale; i++)
                {
                    var rect = new Rectangle();
                    rect.SetValue(Canvas.LeftProperty, x1 + i * cellWidth);
                    rect.SetValue(Canvas.TopProperty, y1);
                    rect.Width = cellWidth - gap - 1d;
                    rect.Height = y2 - y1 - 1d;
                    rect.Fill = fillBrush;
                    _calibrationCanvas.Children.Add(rect);
                }
            }
            catch { }
        }

        #endregion
    }
}
