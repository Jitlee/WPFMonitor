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
    /// 仪表12
    /// </summary>
    public class Meter12 : MonitorControl
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
                else if (name == "LabelColor")
                {
                    LabelColor = Common1.StringToColor(value);
                }
                else if (name == "DialPlateBackColor")
                {
                    DialPlateBackColor = Common1.StringToColor(value);
                }
                else if (name == "CalibrationColor")
                {
                    CalibrationColor = Common1.StringToColor(value);
                }
                else if (name == "BackColor")
                {
                    BackColor = Common1.StringToColor(value);
                }
                else if (name == "DialPlateBorlderColor")
                {
                    DialPlateBorlderColor = Common1.StringToColor(value);
                }
                else if (name == "CalibrationStroke")
                {
                    CalibrationStroke = Common1.StringToColor(value);
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
            //ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
            //Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum", "Minimum", "DecimalDigits", "MainScale", "ViceScale",
            "BackColor",
            "LabelColor",
            "DialPlateBackColor",
            "DialPlateBorlderColor",
            "CalibrationColor",
            "CalibrationStroke","FontFamily", "ForeColor","ScaleColor"};

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
            typeof(Color), typeof(Meter12), new PropertyMetadata(Colors.Blue));
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

        #region 标签

        private static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Meter12), new PropertyMetadata("仪表", TextPropertyChanged));

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
            var element = d as Meter12;
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter12), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter12;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter12), new PropertyMetadata(100d, MaximumPropertyChanged));


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
            var element = d as Meter12;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter12), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter12;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter12), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter12;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter12), new PropertyMetadata(10, MainScalePropertyChanged));

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
            var element = d as Meter12;
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
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter12), new PropertyMetadata(1, ViceScalePropertyChanged));

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
            var element = d as Meter12;
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

        #region 背景颜色

        private static DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor", typeof(Color), typeof(Meter12), new PropertyMetadata(Color.FromArgb(0xff, 0xCE, 0xCB, 0xC5), BackColorPropertyChanged));

        [Description("背景颜色"), Category("画笔")]
        public Color BackColor
        {
            get { return (Color)GetValue(BackColorProperty); }
            set
            {
                SetValue(BackColorProperty, value);
                SetAttrByName("BackColor", value.ToString());
            }
        }

        private static void BackColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter12;
            if (null != element)
            {
                element.BackColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void BackColor_Changed(Color oldValue, Color newValue)
        {
            _canvas.Background = new SolidColorBrush(newValue);
        }

        #endregion

        #region 标签颜色

        private static DependencyProperty LabelColorProperty =
            DependencyProperty.Register("LabelColor", typeof(Color), typeof(Meter12), new PropertyMetadata(Colors.Black, LabelColorPropertyChanged));

        [Description("标签颜色"), Category("画笔")]
        public Color LabelColor
        {
            get { return (Color)GetValue(LabelColorProperty); }
            set
            {
                SetValue(LabelColorProperty, value);
                SetAttrByName("LabelColor", value.ToString());
            }
        }

        private static void LabelColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter12;
            if (null != element)
            {
                element.LabelColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void LabelColor_Changed(Color oldValue, Color newValue)
        {
            _label.Foreground = new SolidColorBrush(newValue);
        }

        #endregion

        #region 表盘颜色

        private static DependencyProperty DialPlateBackColorProperty =
            DependencyProperty.Register("DialPlateBackColor", typeof(Color), typeof(Meter12), new PropertyMetadata(Colors.White, DialPlateBackColorPropertyChanged));

        [Description("表盘颜色"), Category("画笔")]
        public Color DialPlateBackColor
        {
            get { return (Color)GetValue(DialPlateBackColorProperty); }
            set
            {
                SetValue(DialPlateBackColorProperty, value);
                SetAttrByName("DialPlateBackColor", value.ToString());
            }
        }

        private static void DialPlateBackColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter12;
            if (null != element)
            {
                element.DialPlateBackColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void DialPlateBackColor_Changed(Color oldValue, Color newValue)
        {
            _dialPlateEllipse.Fill = new SolidColorBrush(newValue);
        }

        #endregion

        #region 表盘轮廓颜色

        private static DependencyProperty DialPlateBorlderColorProperty =
            DependencyProperty.Register("DialPlateBorlderColor", typeof(Color), typeof(Meter12), new PropertyMetadata(Color.FromArgb(0xff, 0x00, 0x80, 0x40), DialPlateBorlderColorPropertyChanged));

        [Description("表盘轮廓颜色"), Category("画笔")]
        public Color DialPlateBorlderColor
        {
            get { return (Color)GetValue(DialPlateBorlderColorProperty); }
            set
            {
                SetValue(DialPlateBorlderColorProperty, value);
                SetAttrByName("DialPlateBorlderColor", value.ToString());
            }
        }

        private static void DialPlateBorlderColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter12;
            if (null != element)
            {
                element.DialPlateBorlderColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void DialPlateBorlderColor_Changed(Color oldValue, Color newValue)
        {
            _dialPlateBorder.Fill = new SolidColorBrush(newValue);
        }

        #endregion

        #region 刻度文本颜色

        private static DependencyProperty CalibrationColorProperty =
            DependencyProperty.Register("CalibrationColor", typeof(Color), typeof(Meter12), new PropertyMetadata(Colors.Blue, CalibrationColorPropertyChanged));

        [Description("刻度文本颜色"), Category("画笔")]
        public Color CalibrationColor
        {
            get { return (Color)GetValue(CalibrationColorProperty); }
            set
            {
                SetValue(CalibrationColorProperty, value);
                SetAttrByName("CalibrationColor", value.ToString());
            }
        }

        private static void CalibrationColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter12;
            if (null != element)
            {
                element.CalibrationColor_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void CalibrationColor_Changed(Color oldValue, Color newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #region 刻度颜色

        private static DependencyProperty CalibrationStrokeProperty =
            DependencyProperty.Register("CalibrationStroke", typeof(Color), typeof(Meter12), new PropertyMetadata(Colors.Black, CalibrationStrokePropertyChanged));

        [Description("刻度颜色"), Category("画笔")]
        public Color CalibrationStroke
        {
            get { return (Color)GetValue(CalibrationStrokeProperty); }
            set
            {
                SetValue(CalibrationStrokeProperty, value);
                SetAttrByName("CalibrationStroke", value.ToString());
            }
        }

        private static void CalibrationStrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Meter12;
            if (null != element)
            {
                element.CalibrationStroke_Changed((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void CalibrationStroke_Changed(Color oldValue, Color newValue)
        {
            PaintCalibration(new Size(this.ActualWidth, this.ActualHeight));
        }

        #endregion

        #endregion

        #region 变量

        private Canvas _canvas = new Canvas();
        private Canvas _calibrationCanvas = new Canvas();
        private Line _pointLine = new Line();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };

        private Ellipse _dialPlateBorder = new Ellipse();
        private Ellipse _dialPlateEllipse = new Ellipse();

        #endregion

        #region 构造方法

        public Meter12()
        {
            var grid = new Grid();
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _label.Text = Text;
            _label.Foreground = new SolidColorBrush(LabelColor);

            _pointLine.Stroke = new SolidColorBrush(Colors.Red);
            _pointLine.StrokeThickness = 3d;

            _canvas.Background = new SolidColorBrush(BackColor);

            _dialPlateEllipse.Fill = new SolidColorBrush(DialPlateBackColor);
            _dialPlateEllipse.Stroke = new SolidColorBrush(Colors.Black);
            _dialPlateEllipse.StrokeThickness = 1d;

            _dialPlateBorder.Fill = new SolidColorBrush(DialPlateBorlderColor);
            _dialPlateBorder.Stroke = new SolidColorBrush(Colors.Black);
            _dialPlateBorder.StrokeThickness = 1d;
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
                var width = size.Width;
                _canvas.Children.Clear();

                PaintBorder(size);

                PaintEllipse(size);

                _dialPlateBorder.SetValue(Canvas.LeftProperty, width * 48d / 638d);
                _dialPlateBorder.SetValue(Canvas.TopProperty, width * 48d / 638d);
                _dialPlateBorder.Width = _dialPlateBorder.Height = width * 542d / 638d;
                _canvas.Children.Add(_dialPlateBorder);

                _dialPlateEllipse.SetValue(Canvas.LeftProperty, width * 64d / 638d);
                _dialPlateEllipse.SetValue(Canvas.TopProperty, width * 64d / 638d);
                _dialPlateEllipse.Width = _dialPlateEllipse.Height = width * 514d / 638d;
                _canvas.Children.Add(_dialPlateEllipse);

                var ellipse1 = new Ellipse();
                ellipse1.Width = ellipse1.Height = width * 32d / 638d;
                ellipse1.SetValue(Canvas.LeftProperty, width * 303d / 638d);
                ellipse1.SetValue(Canvas.TopProperty, width * 303d / 638d);
                ellipse1.Fill = new SolidColorBrush(Colors.Black);
                _canvas.Children.Add(ellipse1);

                PaintCalibration(size);

                _label.Width = width;
                _label.FontSize = width * 50d / 638d;
                _label.SetValue(Canvas.TopProperty, width * 381d / 638d);
                _canvas.Children.Add(_label);

                _pointLine.X1 = _pointLine.Y1 = width * 0.5d;

                _canvas.Children.Add(_pointLine);
            }
            catch { }
        }

        private void PaintEllipse(Size size)
        {
            var width = size.Width;
            var size1 = width * 52d / 638d;
            var size2 = width * 32d / 638d;
            var brush1=  new SolidColorBrush(Colors.Black);
            var brush2 = new SolidColorBrush(Color.FromArgb(0xff, 0x9D, 0x96, 0x8A));

            var ellipse1 = new Ellipse();
            ellipse1.Width = ellipse1.Height = size1;
            ellipse1.SetValue(Canvas.LeftProperty, width * 22d / 638d);
            ellipse1.SetValue(Canvas.TopProperty, width * 22d / 638d);
            //ellipse1.StrokeThickness = 1d;
            //ellipse1.Stroke = brush1;
            ellipse1.Fill = brush2;
            _canvas.Children.Add(ellipse1);

            var ellipse2 = new Ellipse();
            ellipse2.Width = ellipse2.Height = size1;
            ellipse2.SetValue(Canvas.LeftProperty, width * 564d / 638d);
            ellipse2.SetValue(Canvas.TopProperty, width * 22d / 638d);
            //ellipse2.StrokeThickness = 1d;
            //ellipse2.Stroke = brush1;
            ellipse2.Fill = brush2;
            _canvas.Children.Add(ellipse2);

            var ellipse3 = new Ellipse();
            ellipse3.Width = ellipse3.Height = size1;
            ellipse3.SetValue(Canvas.LeftProperty, width * 564d / 638d);
            ellipse3.SetValue(Canvas.TopProperty, width * 564d / 638d);
            //ellipse3.StrokeThickness = 1d;
            //ellipse3.Stroke = brush1;
            ellipse3.Fill = brush2;
            _canvas.Children.Add(ellipse3);

            var ellipse4 = new Ellipse();
            ellipse4.Width = ellipse4.Height = size1;
            ellipse4.SetValue(Canvas.LeftProperty, width * 22d / 638d);
            ellipse4.SetValue(Canvas.TopProperty, width * 564d / 638d);
            //ellipse4.StrokeThickness = 1d;
            //ellipse4.Stroke = brush1;
            ellipse4.Fill = brush2;
            _canvas.Children.Add(ellipse4);

            var ellipse5 = new Ellipse();
            ellipse5.Width = ellipse5.Height = size2;
            ellipse5.SetValue(Canvas.LeftProperty, width * 32d / 638d);
            ellipse5.SetValue(Canvas.TopProperty, width * 32d / 638d);
            //ellipse5.StrokeThickness = 1d;
            //ellipse5.Stroke = brush1;
            ellipse5.Fill = brush1;
            _canvas.Children.Add(ellipse5);

            var ellipse6 = new Ellipse();
            ellipse6.Width = ellipse6.Height = size2;
            ellipse6.SetValue(Canvas.LeftProperty, width * 574d / 638d);
            ellipse6.SetValue(Canvas.TopProperty, width * 32d / 638d);
            //ellipse6.StrokeThickness = 1d;
            //ellipse6.Stroke = brush1;
            ellipse6.Fill = brush1;
            _canvas.Children.Add(ellipse6);

            var ellipse7 = new Ellipse();
            ellipse7.Width = ellipse7.Height = size2;
            ellipse7.SetValue(Canvas.LeftProperty, width * 574d / 638d);
            ellipse7.SetValue(Canvas.TopProperty, width * 574d / 638d);
            //ellipse7.StrokeThickness = 1d;
            //ellipse7.Stroke = brush1;
            ellipse7.Fill = brush1;
            _canvas.Children.Add(ellipse7);

            var ellipse8 = new Ellipse();
            ellipse8.Width = ellipse8.Height = size2;
            ellipse8.SetValue(Canvas.LeftProperty, width * 32d / 638d);
            ellipse8.SetValue(Canvas.TopProperty, width * 574d / 638d);
            //ellipse8.StrokeThickness = 1d;
            //ellipse8.Stroke = brush1;
            ellipse8.Fill = brush1;
            _canvas.Children.Add(ellipse8);
        }

        private void PaintBorder(Size size)
        {
            var width = size.Width;

            var brush1 = new SolidColorBrush(Color.FromArgb(0xff, 0xE3, 0xE3, 0xE3));
            var brush2 = new SolidColorBrush(Colors.White);
            var brush3 = new SolidColorBrush(Color.FromArgb(0xff, 0x69, 0x69, 0x69));
            var brush4 = new SolidColorBrush(Color.FromArgb(0xff, 0xA0, 0xA0, 0xA0));

            var line1 = new Line();
            line1.X2 = width;
            line1.Stroke = brush1;
            line1.StrokeThickness = 1d;
            _canvas.Children.Add(line1);

            var line2 = new Line();
            line2.Y2 = width;
            line2.Stroke = brush1;
            line2.StrokeThickness = 1d;
            _canvas.Children.Add(line2);

            var line3 = new Line();
            line3.X1 = 1d;
            line3.X2 = width - 1d;
            line3.Y1 = 1d;
            line3.Y2 = 1d;
            line3.Stroke = brush2;
            line3.StrokeThickness = 1d;
            _canvas.Children.Add(line3);

            var line4 = new Line();
            line4.X1 = 1d;
            line4.X2 = 1d;
            line4.Y1 = 1d;
            line4.Y2 = width -1d;
            line4.Stroke = brush2;
            line4.StrokeThickness = 1d;
            _canvas.Children.Add(line4);

            var line5 = new Line();
            line5.X2 = width;
            line5.Y1 = width;
            line5.Y2 = width;
            line5.Stroke = brush3;
            line5.StrokeThickness = 1d;
            _canvas.Children.Add(line5);

            var line6 = new Line();
            line6.X1 = width;
            line6.X2 = width;
            line6.Y2 = width;
            line6.Stroke = brush3;
            line6.StrokeThickness = 1d;
            _canvas.Children.Add(line6);

            var line7 = new Line();
            line7.X1 = 1d;
            line7.X2 = width - 1d;
            line7.Y1 = width - 1d;
            line7.Y2 = width - 1d;
            line7.Stroke = brush4;
            line7.StrokeThickness = 1d;
            _canvas.Children.Add(line7);

            var line8 = new Line();
            line8.X1 = width - 1d;
            line8.X2 = width - 1d;
            line8.Y1 = 1d;
            line8.Y2 = width - 1d;
            line8.Stroke = brush4;
            line8.StrokeThickness = 1d;
            _canvas.Children.Add(line8);
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
                var beginAngle = 1.75d * Math.PI;
                var avgMainAngle = 1.5d * Math.PI / mainScale;
                var avgViceAngle = avgMainAngle / viceScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 27d / 638d;
                var foreground = new SolidColorBrush(CalibrationColor);

                if (decimalDigits < 0)
                {
                    decimalDigits = 0;
                }
                else if (decimalDigits > 7)
                {
                    decimalDigits = 7;
                }

                _calibrationCanvas.Children.Clear();

                var brush = new SolidColorBrush(CalibrationStroke);
                for (int i = 0; i <= mainScale; i++)
                {
                    var mainAngle = beginAngle - avgMainAngle * i;
                    var mainLine = new Line();
                    mainLine.Stroke = brush;
                    mainLine.StrokeThickness = 2d;
                    mainLine.X1 = width * Math.Sin(mainAngle) * 242d / 638d + width * 0.5d;
                    mainLine.Y1 = width * Math.Cos(mainAngle) * 242d / 638d + width * 0.5d;
                    mainLine.X2 = width * Math.Sin(mainAngle) * 219d / 638d + width * 0.5d;
                    mainLine.Y2 = width * Math.Cos(mainAngle) * 219d / 638d + width * 0.5d;
                    _calibrationCanvas.Children.Add(mainLine);

                    for (int j = 1; j < viceScale && i < mainScale; j++)
                    {
                        var viceAngle = mainAngle - j * avgViceAngle;
                        var viceLine = new Line();
                        viceLine.Stroke = brush;
                        viceLine.StrokeThickness = 1d;
                        viceLine.X1 = width * Math.Sin(viceAngle) * 242d / 638d + width * 0.5d;
                        viceLine.Y1 = width * Math.Cos(viceAngle) * 242d / 638d + width * 0.5d;
                        viceLine.X2 = width * Math.Sin(viceAngle) * 219d / 638d + width * 0.5d;
                        viceLine.Y2 = width * Math.Cos(viceAngle) * 219d / 638d + width * 0.5d;
                        _calibrationCanvas.Children.Add(viceLine);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = foreground;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, width * Math.Sin(mainAngle) * 186d / 638d - text.ActualWidth / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, width * Math.Cos(mainAngle) * 186d / 638d - text.ActualHeight / 2d + width * 0.5d);
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
                var mainScale = MainScale;
                var viceScale = ViceScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var angle = beginAngle - 1.5d * Math.PI * value / (maximum - minimum);

                _pointLine.X2 = Math.Sin(angle) * height * 205d / 638d + width * 0.5d;
                _pointLine.Y2 = Math.Cos(angle) * height * 205d / 638d + width * 0.5d;
            }
            catch { }
        }

        #endregion
    }
}
