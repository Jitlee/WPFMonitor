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
    /// 仪表4
    /// </summary>
    public class Meter4 : MonitorControl
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
            //ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
            //Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new string[] { "Text", "Value", "Maximum", "Minimum", 
            "DecimalDigits", "MainScale", "ViceScale" ,"FontFamily", "ForeColor","ScaleColor"};
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
            typeof(Color), typeof(Meter4), new PropertyMetadata(Colors.Blue));
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter4), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter4;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter4), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter4;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter4), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter4;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter4), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter4;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter4), new PropertyMetadata(6, MainScalePropertyChanged));

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
            var element = d as Meter4;
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

        public Meter4()
        {
            var grid = new Grid();
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
                var x1 = width * 94d / 611d;
                var width1 = width - 2 * x1;

                _canvas.Children.Clear();

                var outerBorderEllipse = new Ellipse();
                outerBorderEllipse.Fill = new SolidColorBrush(Colors.Black);
                outerBorderEllipse.Width = width;
                outerBorderEllipse.Height = width;

                _canvas.Children.Add(outerBorderEllipse);

                var innerBorderEllipse = new Ellipse();
                innerBorderEllipse.Fill = new SolidColorBrush(Colors.White);
                innerBorderEllipse.SetValue(Canvas.LeftProperty, x1);
                innerBorderEllipse.SetValue(Canvas.TopProperty, x1);
                innerBorderEllipse.Width = width1;
                innerBorderEllipse.Height = width1;

                _canvas.Children.Add(innerBorderEllipse);

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
                var width = size.Width; // _canvas 有margin 所以要减去

                var maximum = Maximum;
                var minimum = Minimum;
                var mainScale = MainScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var avgMainAngle = 1.5d * Math.PI / mainScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 36d / 611d;

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
                var foreground = new SolidColorBrush(Colors.White);
                for (int i = 0; i <= mainScale; i++)
                {
                    var mainAngle = beginAngle - avgMainAngle * i;
                    var mainLine = new Line();
                    mainLine.Stroke = brush;
                    mainLine.StrokeThickness = 2d;
                    mainLine.X1 = width * Math.Sin(mainAngle) * 213d / 611d  + width * 0.5d;
                    mainLine.Y1 = width * Math.Cos(mainAngle) * 213d / 611d + width * 0.5d;
                    mainLine.X2 = width * Math.Sin(mainAngle) * 189d / 611d + width * 0.5d;
                    mainLine.Y2 = width * Math.Cos(mainAngle) * 189d / 611d + width * 0.5d;
                    _calibrationCanvas.Children.Add(mainLine);

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = foreground;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, width * Math.Sin(mainAngle) * 258d / 611d - text.ActualWidth / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, width * Math.Cos(mainAngle) * 258d / 611d - text.ActualHeight / 2d + width * 0.5d);
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
                pathFigure.StartPoint = new Point(width * Math.Sin(angle) * 172d / 611d + width * 0.5d, width * Math.Cos(angle) * 172d / 611d + width * 0.5d);
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * Math.Sin(angle - 0.5d * Math.PI) * 20d / 611d + width * 0.5d, width * Math.Cos(angle - 0.5d * Math.PI) * 20d / 611d + width * 0.5d) });
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * Math.Sin(angle - Math.PI) * 20d / 611d + width * 0.5d, width * Math.Cos(angle - Math.PI) * 20d / 611d + width * 0.5d) });
                pathFigure.Segments.Add(new LineSegment() { Point = new Point(width * Math.Sin(angle + 0.5d * Math.PI) * 20d / 611d + width * 0.5d, width * Math.Cos(angle + 0.5d * Math.PI) * 20d / 611d + width * 0.5d) });

                var pathGemometry = new PathGeometry();
                pathGemometry.Figures.Add(pathFigure);

                _pointPath.Data = pathGemometry;
            }
            catch { }
        }

        #endregion
    }
}
