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
    /// 仪表9
    /// </summary>
    public class Meter9 : MonitorControl
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
            "DecimalDigits", "MainScale", "ViceScale" ,"FontFamily", "ForeColor","ScaleColor"};
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
            typeof(Color), typeof(Meter9), new PropertyMetadata(Colors.Blue));
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
            typeof(Color), typeof(Meter9), new PropertyMetadata(Colors.Blue));
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

        #region 当前值

        private static DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Meter9), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter9;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter9), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter9;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter9), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter9;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter9), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter9;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter9), new PropertyMetadata(6, MainScalePropertyChanged));

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
            var element = d as Meter9;
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
        private Line _pointLine = new Line();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };

        #endregion

        #region 构造方法

        public Meter9()
        {
            var grid = new Grid();
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _label.Text = Value.ToString();
            _label.Foreground = new SolidColorBrush(ForeColor);

            _pointLine.Stroke = new SolidColorBrush(Colors.Red);
            _pointLine.StrokeThickness = 2d;

            this.SizeChanged += Meter2_SizeChanged;
        }

        #endregion

        #region 私有方法

        private void Meter2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 640d / 852d;
            Paint(new Size(this.ActualWidth, this.ActualHeight));
        }

        private void Paint(Size size)
        {
            try
            {
                var width = size.Width;
                var height = size.Height;
                _canvas.Children.Clear();

                var brush1 = new SolidColorBrush(Colors.Black);
                var brush2 = new SolidColorBrush(Colors.White);
                var brush3 = new SolidColorBrush(Color.FromArgb(0xff, 0x64, 0x58, 0x45));

                var border1 = new Rectangle();
                border1.Stroke = brush1;
                border1.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x80, 0x80, 0x80));
                border1.Width = width;
                border1.Height = height;
                _canvas.Children.Add(border1);

                var border2 = new Polygon();
                border2.Fill = new SolidColorBrush(Colors.White);
                border2.Points.Clear();
                border2.Points.Add(new Point(1d, 1d));
                border2.Points.Add(new Point(1d, border1.Height - 2d));
                border2.Points.Add(new Point(width - 2d, 1d));
                _canvas.Children.Add(border2);

                var background1 = new Rectangle();
                background1.Width = width - 10d;
                background1.Height = border1.Height - 10d;
                background1.SetValue(Canvas.LeftProperty, 5d);
                background1.SetValue(Canvas.TopProperty, 5d);
                background1.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xc0, 0xc0, 0xc0));
                _canvas.Children.Add(background1);

                var background2 = new Border();
                background2.BorderThickness = new Thickness(1d, 1d, 1d, 0d);
                background2.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x74, 0x6A, 0x59));
                background2.Background = new SolidColorBrush(Colors.White);
                background2.SetValue(Canvas.LeftProperty, width * 50d / 852d);
                background2.SetValue(Canvas.TopProperty, width * 41d / 852d);
                background2.Width = width * 762d / 852d;
                background2.Height = width * 407d / 852d;
                _canvas.Children.Add(background2);

                var brush = new SolidColorBrush(Colors.Black);
                var ellipse1 = new Ellipse();
                ellipse1.SetValue(Canvas.LeftProperty, width * 1d / 852d);
                ellipse1.SetValue(Canvas.TopProperty, width * 131d / 852d);
                ellipse1.Width = ellipse1.Height = width * 850d / 852d;
                ellipse1.Stroke = brush;
                ellipse1.StrokeThickness = 3d;
                ellipse1.Clip = new RectangleGeometry() { Rect = new Rect(0d, 0d, ellipse1.Width, width * 164d / 852d) };
                _canvas.Children.Add(ellipse1);

                PaintCalibration(size);

                _canvas.Children.Add(_pointLine);

                var ellipse2 = new Ellipse();
                ellipse2.SetValue(Canvas.LeftProperty, width * 272.5d / 852d);
                ellipse2.SetValue(Canvas.TopProperty, width * 401d / 852d);
                ellipse2.Width = ellipse2.Height = width * 310d / 852d;
                ellipse2.Fill = brush;
                ellipse2.Clip = new RectangleGeometry() { Rect = new Rect(0d, 0d, ellipse2.Width, width * 50d / 852d) };
                _canvas.Children.Add(ellipse2);

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
                var beginAngle = 1.25d * Math.PI;
                var avgMainAngle = 0.5d * Math.PI / mainScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 35d / 852d;

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
                    mainLine.X1 = Math.Sin(mainAngle) * width * 425d / 852d + width * 0.5d;
                    mainLine.Y1 = Math.Cos(mainAngle) * width * 425d / 852d + width * 556d / 852d;
                    mainLine.X2 = Math.Sin(mainAngle) * width * 380d / 852d + width * 0.5d;
                    mainLine.Y2 = Math.Cos(mainAngle) * width * 380d / 852d + width * 556d / 852d;
                    _calibrationCanvas.Children.Add(mainLine);

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = brush;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, Math.Sin(mainAngle) * width * 490d / 852d - text.ActualWidth / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, Math.Cos(mainAngle) * width * 490d / 852d - text.ActualHeight / 2d + width * 556d / 852d);
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
                var beginAngle = 1.25d * Math.PI;
                var angle = beginAngle - 0.5d * Math.PI * value / (maximum - minimum);

                _pointLine.X1 = Math.Sin(angle) * width * 158d / 852d + width * 0.5d;
                _pointLine.Y1 = Math.Cos(angle) * width * 158d / 852d + width * 556d / 852d;
                _pointLine.X2 = Math.Sin(angle) * width * 400d / 852d + width * 0.5d;
                _pointLine.Y2 = Math.Cos(angle) * width * 400d / 852d + width * 556d / 852d;
            }
            catch { }
        }

        #endregion
    }
}
