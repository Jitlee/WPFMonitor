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
using WPFMonitor.Model;
using System.Linq;
using MonitorSystem.MonitorSystemGlobal;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 温度计实体类
    /// </summary>
    public class Temprary : MonitorControl
    {
        public override void SetChannelValue(float fValue)
        {
            MyTemp = (int)fValue;
        }

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
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
                string name = pro.PropertyName.Trim().ToUpper();
                string value = pro.PropertyValue.Trim();
                if (name == "MaxValue".ToUpper())
                {
                    MaxValue = Int32.Parse(value);
                }
                else if (name == "MinValue".ToUpper())
                {
                    MinValue = Int32.Parse(value);
                }
                else if (name == "MyTemp".ToUpper())
                {
                    MyTemp = Int32.Parse(value);
                }
                else if (name == "BlankColor".ToUpper())
                {
                    BlankColor = Common1.StringToColor(value);
                }
                else if (name == "DataZoneColor".ToUpper())
                {
                    DataZoneColor = Common1.StringToColor(value);
                }
                else if (name == "Range".ToUpper())
                {
                    Range = int.Parse(value);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;

            BackColor = Common1.StringToColor(ScreenElement.BackColor);
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
            Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new[] { "Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "BackColor", "ForeColor", "BlankColor", "DataZoneColor", "MinValue", "MaxValue", "MyTemp", "Range","Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
       typeof(int), typeof(Temprary), new PropertyMetadata(0));
        private int _Transparent = 0;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                PaintBackground();
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }

        private static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor",
            typeof(Color), typeof(Temprary), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));
        [DefaultValue(30), Description("背景色"), Category("外观")]
        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set { this.SetValue(BackColorProperty, value);
            if (ScreenElement != null)
                ScreenElement.BackColor = value.ToString();
            }
        }

        private static void BackColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(Temprary), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));
        [DefaultValue(30), Description("前景色"), Category("外观")]
        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set { this.SetValue(ForeColorProperty, value);
            if (ScreenElement != null)
                ScreenElement.ForeColor = value.ToString();
            }
        }

        private static void ForeColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty BlankColorProperty =
            DependencyProperty.Register("BlankColor",
            typeof(Color), typeof(Temprary), new PropertyMetadata(Colors.Red, new PropertyChangedCallback(BlankColor_Changed)));
        [Category("我的属性")]
        public Color BlankColor
        {
            get { return (Color)this.GetValue(BlankColorProperty); }
            set { this.SetValue(BlankColorProperty, value);
            SetAttrByName("BlankColor", value.ToString());
            }
        }

        private static void BlankColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnBlankColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBlankColorChanged(Color oldValue, Color newValue)
        {
            PaintBlankColor();
        }

        private static readonly DependencyProperty DataZoneColorProperty =
            DependencyProperty.Register("DataZoneColor",
            typeof(Color), typeof(Temprary), new PropertyMetadata(Colors.White, new PropertyChangedCallback(DataZoneColor_Changed)));
        [Category("我的属性")]
        public Color DataZoneColor
        {
            get { return (Color)this.GetValue(DataZoneColorProperty); }
            set { this.SetValue(DataZoneColorProperty, value);
                SetAttrByName("DataZoneColor", value.ToString()); 
            }
        }

        private static void DataZoneColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnDataZoneColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnDataZoneColorChanged(Color oldValue, Color newValue)
        {
            PaintDataZoneColor();
        }

        private static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue",
            typeof(int), typeof(Temprary), new PropertyMetadata(0, new PropertyChangedCallback(MinValue_Changed)));
        [DefaultValue(0), Description("温度计的最小刻度值"), Category("我的属性")]
        public int MinValue
        {
            get { return (int)this.GetValue(MinValueProperty); }
            set { this.SetValue(MinValueProperty, value);
            SetAttrByName("MimValue", value.ToString());
            }
        }

        private static void MinValue_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnMinValueChanged((int)e.NewValue, (int)e.OldValue);
        }

        public void OnMinValueChanged(int oldValue, int newValue)
        {
            Paint(DesiredSize);
        }

        private static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue",
            typeof(int), typeof(Temprary), new PropertyMetadata(100, new PropertyChangedCallback(MaxValue_Changed)));
        [DefaultValue(100), Description("温度计的最大刻度值"), Category("我的属性")]
        public int MaxValue
        {
            get { return (int)this.GetValue(MaxValueProperty); }
            set { this.SetValue(MaxValueProperty, value);
            SetAttrByName("MaxValue", value.ToString());
            }
        }

        private static void MaxValue_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnMaxValueChanged((int)e.NewValue, (int)e.OldValue);
        }

        public void OnMaxValueChanged(int oldValue, int newValue)
        {
            Paint(DesiredSize);
        }

        private static readonly DependencyProperty MyTempProperty =
            DependencyProperty.Register("MyTemp",
            typeof(int), typeof(Temprary), new PropertyMetadata(30, new PropertyChangedCallback(MyTemp_Changed)));
        [DefaultValue(30), Description("温度计当前值"), Category("我的属性")]
        public int MyTemp
        {
            get { return (int)this.GetValue(MyTempProperty); }
            set { this.SetValue(MyTempProperty, value);
            SetAttrByName("MyTemp", value.ToString());
            }
        }

        private static void MyTemp_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnMyTempChanged((int)e.NewValue, (int)e.OldValue);
        }

        public void OnMyTempChanged(int oldValue, int newValue)
        {
            SetText();
            SetBlank(DesiredSize.Width);
        }

        private static readonly DependencyProperty RangeProperty =
            DependencyProperty.Register("Range",
            typeof(int), typeof(Temprary), new PropertyMetadata(10, new PropertyChangedCallback(Range_Changed)));
        [Category("我的属性")]
        public int Range
        {
            get { return (int)this.GetValue(RangeProperty); }
            set { this.SetValue(RangeProperty, value);
            SetAttrByName("Range", value.ToString());
            }
        }

        private static void Range_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Temprary temprary = (Temprary)element;
            temprary.OnRangeChanged((int)e.NewValue, (int)e.OldValue);
        }

        public void OnRangeChanged(int oldValue, int newValue)
        {
            PaintBackground();
        }

        #endregion

        private Canvas _canvas = new Canvas();
        private Rectangle _datazoneRectangle = new Rectangle();
        private Ellipse _datazoneEllipse = new Ellipse();
        private Rectangle _blankRectangle = new Rectangle();
        private Canvas _calibrationCanvas = new Canvas();
        private TextBlock _text = new TextBlock() { Foreground = new SolidColorBrush(Colors.Red) };

        public Temprary()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_datazoneRectangle);
            _canvas.Children.Add(_datazoneEllipse);
            _canvas.Children.Add(_blankRectangle);
            _canvas.Children.Add(_calibrationCanvas);
            _canvas.Children.Add(_text);

            SetText();
            PaintBackground();
            PaintDataZoneColor();
            PaintBlankColor();
        }

        private void SetText()
        {
            _text.Text = string.Format("{0}度", MyTemp);
        }

        private void PaintDataZoneColor()
        {
            _datazoneEllipse.Fill = _datazoneRectangle.Fill = new SolidColorBrush(DataZoneColor);
        }

        private void PaintBlankColor()
        {
            _blankRectangle.Fill = new SolidColorBrush(BlankColor);
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                _canvas.Background = new SolidColorBrush();
            }
            else
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.EndPoint = new Point(0, 1);
                brush.GradientStops.Add(new GradientStop() { Offset = 0, Color = BackColor });
                brush.GradientStops.Add(new GradientStop() { Offset = 1, Color = GetEndColor() });
                _canvas.Background = brush;
            }
        }

        private Color GetEndColor()
        {
            var range = Range;
            if (range < 0)
            {
                range = 0;
            }
            else if (range > 10)
            {
                range = 10;
            }

            return Color.FromArgb(0xff,
                (byte)(this.BackColor.R + range * (this.ForeColor.R - this.BackColor.R) / 10),
                (byte)(this.BackColor.G + range * (this.ForeColor.G - this.BackColor.G) / 10),
                (byte)(this.BackColor.B + range * (this.ForeColor.B - this.BackColor.B) / 10));
        }

        private void Paint(Size finalSize)
        {
            var width = finalSize.Width;
            var height = finalSize.Height;
            var circle = width / 3d;
            var cudu = 2d * circle / 5d;
            var standLength = 3d;
            var standWidth = 8d;
            var keduLength = 1d;
            var keduWidth = 5d;

            if (MinValue >= MaxValue)
                return;
            //if (myTemp < minValue || myTemp > maxValue)
            //{
            //    myTemp = (minValue + maxValue) / 2;
            //}
            circle = finalSize.Width / 4;
            if (cudu >= circle)
            {
                cudu = 2 * circle / 3;
            }
            int valuePerKeDu = (MaxValue - MinValue) / 10;  //每个大刻度之间的值

            _datazoneRectangle.SetValue(Canvas.LeftProperty, width / 4 - cudu);
            _datazoneRectangle.SetValue(Canvas.TopProperty, 0d);
            _datazoneRectangle.SetValue(WidthProperty, 2 * cudu);
            _datazoneRectangle.SetValue(HeightProperty, 4 * height / 5);

            //绘制表盘
            _datazoneEllipse.SetValue(Canvas.LeftProperty, width / 4d - circle);
            _datazoneEllipse.SetValue(Canvas.TopProperty, 4d * height / 5d - circle);
            _datazoneEllipse.SetValue(WidthProperty, 2d * circle);
            _datazoneEllipse.SetValue(HeightProperty, 2d * circle);

            _text.SetValue(Canvas.LeftProperty, width / 4d - _text.ActualWidth / 2d);
            _text.SetValue(Canvas.TopProperty, 9d * height / 10d - _text.ActualHeight + 10d);

            _calibrationCanvas.Children.Clear();
            var calibrationBrush = new SolidColorBrush(Colors.Green);
            var calibrationLeft = width / 4d + cudu;
            //绘制刻度标记
            for (int x = 0; x < 101; x++)
            {
                //g.FillRectangle(Brushes.Green, new Rectangle(this.Width / 4 + cudu, 4 * this.Height / 5 - circle - x * (4 * this.Height / 5 - circle) / 100, keduWidth, keduLength));
                var calibrationLine = new Rectangle();
                calibrationLine.SetValue(Canvas.LeftProperty, calibrationLeft);
                calibrationLine.SetValue(Canvas.TopProperty, 4d * height / 5d - circle - x * (4d * height / 5d - circle) / 100d);
                calibrationLine.SetValue(WidthProperty, keduWidth);
                calibrationLine.SetValue(HeightProperty, keduLength);
                calibrationLine.Fill = calibrationBrush;
                _calibrationCanvas.Children.Add(calibrationLine);
            }

            int speed = MinValue;
            var labelBrush = new SolidColorBrush(Colors.Red);
            //绘制刻度值
            for (int x = 0; x < 11; x++)
            {
                var lableLine = new Rectangle();
                lableLine.SetValue(Canvas.LeftProperty, calibrationLeft);
                lableLine.SetValue(Canvas.TopProperty, 4d * height / 5d - circle - x * (4d * height / 5d - circle) / 10d);
                lableLine.SetValue(WidthProperty, standWidth);
                lableLine.SetValue(HeightProperty, standLength);
                lableLine.Fill = labelBrush;
                _calibrationCanvas.Children.Add(lableLine);

                //绘制数值
                var label = new TextBlock();
                label.SetValue(Canvas.LeftProperty, calibrationLeft + standWidth);
                label.SetValue(Canvas.TopProperty, 4d * height / 5d - circle - x * (4d * height / 5d - circle) / 10d);
                label.Foreground = calibrationBrush;
                label.Text = speed.ToString();
                _calibrationCanvas.Children.Add(label);

                speed += valuePerKeDu;
            }
            //把温度计上面空余部分填充白色
            var trueValue = MyTemp;
            if (MyTemp < MinValue)
            {
                trueValue = MinValue;
            }
            else if (MyTemp > MaxValue)
            {
                trueValue = MaxValue;
            }

            SetBlank(width);
        }

        private void SetBlank(double width)
        {
            var circle = width / 3d;
            var cudu = 2d * circle / 5d;

            var trueValue = MyTemp;
            if (MyTemp < MinValue)
            {
                trueValue = MinValue;
            }
            else if (MyTemp > MaxValue)
            {
                trueValue = MaxValue;
            }

            _blankRectangle.SetValue(Canvas.LeftProperty, width / 4d - cudu);
            _blankRectangle.SetValue(Canvas.TopProperty, 0d);
            _blankRectangle.SetValue(WidthProperty, 2d * cudu);
            var height = (MaxValue - trueValue) * (4d * this.Height / 5 - circle) / (MaxValue - MinValue);
            if(height < 0d)
            {
                height = 0d;
            }
            _blankRectangle.SetValue(HeightProperty, height);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Paint(availableSize);
            return base.MeasureOverride(availableSize);
        }
    }
}
