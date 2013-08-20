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
    /// 仪表10
    /// </summary>
    public class Meter10 : MonitorControl
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
            //base.Translate = ScreenElement.Transparent.Value;
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
            typeof(Color), typeof(Meter10), new PropertyMetadata(Colors.Blue));
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
            typeof(Color), typeof(Meter10), new PropertyMetadata(Colors.Blue));
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
            DependencyProperty.Register("Text", typeof(string), typeof(Meter10), new PropertyMetadata("仪表", TextPropertyChanged));

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
            var element = d as Meter10;
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter10), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter10;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter10), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter10;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter10), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter10;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter10), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter10;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter10), new PropertyMetadata(6, MainScalePropertyChanged));

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
            var element = d as Meter10;
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
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter10), new PropertyMetadata(4, ViceScalePropertyChanged));

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
            var element = d as Meter10;
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

        public Meter10()
        {
            var grid = new Grid();
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _label.Text = Text;
            _label.Foreground = new SolidColorBrush(ForeColor);

            _pointPath.Fill = new SolidColorBrush(Colors.Red);
            _pointPath.Stroke = new SolidColorBrush(Colors.Black);
            _pointPath.StrokeThickness = 1d;
            this.SizeChanged += Meter2_SizeChanged;
        }

        #endregion

        #region 私有方法

        private void Meter2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Height = e.NewSize.Height;
            this.Width = e.NewSize.Height * 518d / 649d;
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
                var brush4 = new SolidColorBrush(Color.FromArgb(0xff, 0x7C, 0x73, 0x63));
                var brush5 = new SolidColorBrush(Color.FromArgb(0xff, 0xBE, 0xB9, 0xB1));

                var border1 = new Rectangle();
                border1.Stroke = brush1;
                border1.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0x80, 0x80, 0x80));
                border1.Width = width;
                border1.Height = width;
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

                var x = width * 96d / 518d;
                var y = x;
                var ellipse1 = new Ellipse();
                ellipse1.SetValue(Canvas.LeftProperty, x);
                ellipse1.SetValue(Canvas.TopProperty, y);
                ellipse1.Width = ellipse1.Height = width * 326d / 518d;
                ellipse1.Stroke = brush1;
                ellipse1.StrokeThickness = 3.0d;
                ellipse1.Clip = new RectangleGeometry() { Rect = new Rect(0d, 0d, ellipse1.Width, width * 288d / 518d) };
                _canvas.Children.Add(ellipse1);

                x = width * 107d / 518d;
                y = x;
                var ellipse2 = new Ellipse();
                ellipse2.SetValue(Canvas.LeftProperty, x);
                ellipse2.SetValue(Canvas.TopProperty, y);
                ellipse2.Width = ellipse2.Height = width * 304d / 518d;
                ellipse2.Stroke = brush1;
                ellipse2.StrokeThickness = 2.0d;
                ellipse2.Fill = brush4;
                _canvas.Children.Add(ellipse2);

                var avgAngle = Math.PI / 10d;
                for (int i = 0; i < 20; i++)
                {
                    var angle = avgAngle * i;
                    var line = new Line();
                    line.Stroke = brush1;
                    line.StrokeThickness = 2d;
                    line.X1 = width * Math.Sin(angle) * 152d / 518d + width * 0.5d;
                    line.Y1 = width * Math.Cos(angle) * 152d / 518d + width * 0.5d;
                    line.X2 = width * Math.Sin(angle) * 139d / 518d + width * 0.5d;
                    line.Y2 = width * Math.Cos(angle) * 139d / 518d + width * 0.5d;
                    _canvas.Children.Add(line);
                }

                x = width * 120d / 518d;
                y = x;
                var ellipse3 = new Ellipse();
                ellipse3.SetValue(Canvas.LeftProperty, x);
                ellipse3.SetValue(Canvas.TopProperty, y);
                ellipse3.Width = ellipse3.Height = width * 278d / 518d;
                ellipse3.Stroke = brush1;
                ellipse3.StrokeThickness = 2.0d;
                ellipse3.Fill = brush5;
                _canvas.Children.Add(ellipse3);


                x = width * 131d / 518d;
                y = x;
                var ellipse4 = new Ellipse();
                ellipse4.SetValue(Canvas.LeftProperty, x);
                ellipse4.SetValue(Canvas.TopProperty, y);
                ellipse4.Width = ellipse4.Height = width * 256d / 518d;
                ellipse4.Stroke = brush1;
                ellipse4.StrokeThickness = 1.0d;
                _canvas.Children.Add(ellipse4);

                _label.Width = width;
                _label.FontSize = width * 34d / 518d;
                _label.SetValue(Canvas.TopProperty, width * 0.85d);
                _canvas.Children.Add(_label);

                PaintBottom(size);

                PaintCalibration(size);

                _canvas.Children.Add(_pointPath);
            }
            catch { }
        }

        private void PaintBottom(Size size)
        {
            var width = size.Width;
            var height = size.Height;
            var brush1 = new SolidColorBrush(Colors.Black);
            var brush2 = new SolidColorBrush(Colors.White);
            var brush3 = new SolidColorBrush(Color.FromArgb(0xff, 0x74, 0x6A, 0x59));
            var brush4 = new SolidColorBrush(Color.FromArgb(0xff, 0xAD, 0xA7, 0x9D));

            var line1 = new Line();
            line1.Y1 = width + 3.0d;
            line1.Y2 = height;
            line1.StrokeThickness = 3.0d;
            line1.Stroke = brush2;
            _canvas.Children.Add(line1);

            var line2 = new Line();
            line2.X2 = width;
            line2.Y1 = width + 3.0d;
            line2.Y2 = width + 3.0d;
            line2.StrokeThickness = 3.0d;
            line2.Stroke = brush2;
            _canvas.Children.Add(line2);

            var line3 = new Line();
            line3.X2 = width;
            line3.Y1 = height;
            line3.Y2 = height;
            line3.StrokeThickness = 3.0d;
            line3.Stroke = brush3;
            _canvas.Children.Add(line3);

            var line4 = new Line();
            line4.X1 = width;
            line4.X2 = width;
            line4.Y1 = width + 3.0d;
            line4.Y2 = height;
            line4.StrokeThickness = 3.0d;
            line4.Stroke = brush3;
            _canvas.Children.Add(line4);

            var line5 = new Line();
            line5.X1 = 3.0d;
            line5.X2 = 3.0d;
            line5.Y1 = width + 6.0d;
            line5.Y2 = height - 3.0d;
            line5.StrokeThickness = 3.0d;
            line5.Stroke = brush3;
            _canvas.Children.Add(line5);

            var line6 = new Line();
            line6.X1 = 3.0d;
            line6.X2 = height - width - 3.0d;
            line6.Y1 = width + 6.0d;
            line6.Y2 = width + 6.0d;
            line6.StrokeThickness = 3.0d;
            line6.Stroke = brush3;
            _canvas.Children.Add(line6);

            var line7 = new Line();
            line7.X1 = 3.0d;
            line7.X2 = height - width - 3.0d;
            line7.Y1 = height - 3.0d;
            line7.Y2 = height - 3.0d;
            line7.StrokeThickness = 3.0d;
            line7.Stroke = brush2;
            _canvas.Children.Add(line7);

            var line8 = new Line();
            line8.X1 = height - width - 3.0d;
            line8.X2 = height - width - 3.0d;
            line8.Y1 = width + 3.0d;
            line8.Y2 = height - 3.0d;
            line8.StrokeThickness = 3.0d;
            line8.Stroke = brush2;
            _canvas.Children.Add(line8);

            var rectangle1 = new Rectangle();
            rectangle1.SetValue(Canvas.LeftProperty, height - width + 3.0d);
            rectangle1.SetValue(Canvas.TopProperty, width + 6.0d);
            rectangle1.Width = 2.0d * width - height - 6.0d;
            rectangle1.Height = height - width - 6.0d;
            rectangle1.Fill = brush1;
            _canvas.Children.Add(rectangle1);

            var line9 = new Line();
            line9.X1 = height - width + 3.0d;
            line9.X2 = height - width + 3.0d;
            line9.Y1 = width + 6.0d;
            line9.Y2 = height - 3.0d;
            line9.StrokeThickness = 3.0d;
            line9.Stroke = brush3;
            _canvas.Children.Add(line9);

            var line10 = new Line();
            line10.X1 = height - width + 3.0d;
            line10.X2 = width - 3.0d;
            line10.Y1 = width + 6.0d;
            line10.Y2 = width + 6.0d;
            line10.StrokeThickness = 3.0d;
            line10.Stroke = brush3;
            _canvas.Children.Add(line10);

            var line11 = new Line();
            line11.X1 = height - width + 3.0d;
            line11.X2 = width - 3.0d;
            line11.Y1 = height - 3.0d;
            line11.Y2 = height - 3.0d;
            line11.StrokeThickness = 3.0d;
            line11.Stroke = brush2;
            _canvas.Children.Add(line11);

            var line12 = new Line();
            line12.X1 = width - 3.0d;
            line12.X2 = width - 3.0d;
            line12.Y1 = width + 3.0d;
            line12.Y2 = height - 3.0d;
            line12.StrokeThickness = 3.0d;
            line12.Stroke = brush2;
            _canvas.Children.Add(line12);

            var pathFigure1 = new PathFigure();
            pathFigure1.StartPoint = new Point(6.0d, width + (height - width) / 2.0d - 3.0d);
            pathFigure1.IsClosed = true;
            pathFigure1.IsFilled = true;
            var lineSegment1 = new LineSegment();
            lineSegment1.Point = new Point((height - width) / 2.0d, width + 6.0d);
            pathFigure1.Segments.Add(lineSegment1);
            var lineSegment2 = new LineSegment();
            lineSegment2.Point = new Point(height - width - 6.0d, width + (height - width) / 2.0d - 3.0d);
            pathFigure1.Segments.Add(lineSegment2);

            var pathFigure2 = new PathFigure();
            pathFigure2.StartPoint = new Point(6.0d, width + (height - width) / 2.0d + 3.0d);
            pathFigure2.IsClosed = true;
            pathFigure2.IsFilled = true;
            var lineSegment3 = new LineSegment();
            lineSegment3.Point = new Point((height - width) / 2.0d, height - 9.0d);
            pathFigure2.Segments.Add(lineSegment3);
            var lineSegment4 = new LineSegment();
            lineSegment4.Point = new Point(height - width - 6.0d, width + (height - width) / 2.0d + 3.0d);
            pathFigure2.Segments.Add(lineSegment4);

            var pathGeometry1 = new PathGeometry();
            pathGeometry1.Figures.Add(pathFigure1);
            pathGeometry1.Figures.Add(pathFigure2);

            var path1 = new Path();
            path1.Data = pathGeometry1;
            path1.Fill = brush4;
            _canvas.Children.Add(path1);

            var line13 = new Line();
            line13.X1 = 6.0d;
            line13.X2 = (height - width) / 2.0d;
            line13.Y1 = width + (height - width) / 2.0d - 3.0d;
            line13.Y2 = width + 9.0d;
            line13.StrokeThickness = 3.0d;
            line13.Stroke = brush2;
            _canvas.Children.Add(line13);

            var line14 = new Line();
            line14.X1 = (height - width) / 2.0d;
            line14.X2 = height - width - 6.0d;
            line14.Y1 = width + 9.0d;
            line14.Y2 = width + (height - width) / 2.0d - 3.0d;
            line14.StrokeThickness = 3.0d;
            line14.Stroke = brush3;
            _canvas.Children.Add(line14);

            var line15 = new Line();
            line15.X1 = 6.0d;
            line15.X2 = height - width - 6.0d;
            line15.Y1 = width + (height - width) / 2.0d - 3.0d;
            line15.Y2 = width + (height - width) / 2.0d - 3.0d;
            line15.StrokeThickness = 3.0d;
            line15.Stroke = brush3;
            _canvas.Children.Add(line15);

            var line16 = new Line();
            line16.X1 = 6.0d;
            line16.X2 = (height - width) / 2.0d;
            line16.Y1 = width + (height - width) / 2.0d + 3.0d;
            line16.Y2 = height - 9.0d;
            line16.StrokeThickness = 3.0d;
            line16.Stroke = brush2;
            _canvas.Children.Add(line16);

            var line17 = new Line();
            line17.X1 = (height - width) / 2.0d;
            line17.X2 = height - width - 6.0d;
            line17.Y1 = height - 9.0d;
            line17.Y2 = width + (height - width) / 2.0d + 3.0d;
            line17.StrokeThickness = 3.0d;
            line17.Stroke = brush3;
            _canvas.Children.Add(line17);

            var line18 = new Line();
            line18.X1 = 6.0d;
            line18.X2 = height - width - 6.0d;
            line18.Y1 = width + (height - width) / 2.0d + 3.0d;
            line18.Y2 = width + (height - width) / 2.0d + 3.0d;
            line18.StrokeThickness = 3.0d;
            line18.Stroke = brush2;
            _canvas.Children.Add(line18);
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
                var viceScale = ViceScale;
                var decimalDigits = DecimalDigits;  // 小数点位
                var beginAngle = 1.75d * Math.PI;
                var avgMainAngle = 1.5d * Math.PI / mainScale;
                var avgViceAngle = avgMainAngle / viceScale;
                var avg = (maximum - minimum) / (double)mainScale;
                var fontSize = width * 27d / 518d;

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
                var brush2 = new SolidColorBrush(Color.FromArgb(0xff, 0x7C, 0x73, 0x63));
                for (int i = 0; i <= mainScale; i++)
                {
                    var mainAngle = beginAngle - avgMainAngle * i;
                    var mainLine = new Line();
                    mainLine.Stroke = brush1;
                    mainLine.StrokeThickness = 2d;
                    mainLine.X1 = width * Math.Sin(mainAngle) * 163d / 518d + width * 0.5d;
                    mainLine.Y1 = width * Math.Cos(mainAngle) * 163d / 518d + width * 0.5d;
                    mainLine.X2 = width * Math.Sin(mainAngle) * 181d / 518d + width * 0.5d;
                    mainLine.Y2 = width * Math.Cos(mainAngle) * 181d / 518d + width * 0.5d;
                    _calibrationCanvas.Children.Add(mainLine);

                    for (int j = 1; j < viceScale && i < mainScale; j++)
                    {
                        var viceAngle = mainAngle - j * avgViceAngle;
                        var viceLine = new Line();
                        viceLine.Stroke = brush2;
                        viceLine.StrokeThickness = 1d;
                        viceLine.X1 = width * Math.Sin(viceAngle) * 163d / 518d + width * 0.5d;
                        viceLine.Y1 = width * Math.Cos(viceAngle) * 163d / 518d + width * 0.5d;
                        viceLine.X2 = width * Math.Sin(viceAngle) * 176d / 518d + width * 0.5d;
                        viceLine.Y2 = width * Math.Cos(viceAngle) * 176d / 518d + width * 0.5d;
                        _calibrationCanvas.Children.Add(viceLine);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    Size textSize = text.MeasureTextSize();
                    text.SetValue(Canvas.LeftProperty, width * Math.Sin(mainAngle) * 218d / 518d - textSize.Width / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, width * Math.Cos(mainAngle) * 218d / 518d - textSize.Height / 2d + width * 0.5d);
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
                pathFigure.StartPoint = new Point(width * Math.Sin(angle) * 120d / 518d + width * 0.5d, width * Math.Cos(angle) * 120d / 518d + width * 0.5d);
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
