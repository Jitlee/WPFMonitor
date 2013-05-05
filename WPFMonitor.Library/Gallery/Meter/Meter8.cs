using System;
using System.ComponentModel;
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
using WPFMonitor.Library.ZTControls;
using WPFMonitor.Model;

namespace WPFMonitor.Library.Gallery.Meter
{
    /// <summary>
    /// 仪表8
    /// </summary>
    public class Meter8 : MonitorControl
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
                else if (name == "ViceScale")
                {
                    ViceScale = int.Parse(value);
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
            ForeColor = Common.StringToColor(ScreenElement.ForeColor);
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
            typeof(Color), typeof(Meter8), new PropertyMetadata(Colors.Blue));
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
            typeof(Color), typeof(Meter8), new PropertyMetadata(Colors.Blue));
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
            DependencyProperty.Register("Value", typeof(double), typeof(Meter8), new PropertyMetadata(0d, ValuePropertyChanged));


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
            var element = d as Meter8;
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
            DependencyProperty.Register("Maximum", typeof(double), typeof(Meter8), new PropertyMetadata(60d, MaximumPropertyChanged));


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
            var element = d as Meter8;
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
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter8), new PropertyMetadata(0d, MinimumPropertyChanged));


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
            var element = d as Meter8;
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
            DependencyProperty.Register("DecimalDigits", typeof(int), typeof(Meter8), new PropertyMetadata(0, DecimalDigitsPropertyChanged));

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
            var element = d as Meter8;
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
            DependencyProperty.Register("MainScale", typeof(int), typeof(Meter8), new PropertyMetadata(6, MainScalePropertyChanged));

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
            var element = d as Meter8;
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
            DependencyProperty.Register("ViceScale", typeof(int), typeof(Meter8), new PropertyMetadata(4, ViceScalePropertyChanged));

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
            var element = d as Meter8;
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
        private Line _pointLine = new Line();
        private TextBlock _label = new TextBlock() { TextAlignment = TextAlignment.Center };

        #endregion

        #region 构造方法

        public Meter8()
        {

            var borderBrush = new LinearGradientBrush();
            borderBrush.StartPoint = new Point();
            borderBrush.EndPoint = new Point(1d, 1d);
            borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0d });
            borderBrush.GradientStops.Add(new GradientStop() { Color = Colors.White, Offset = 0.4999d });
            borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x84, 0x7B, 0x6C), Offset = 0.5d });
            borderBrush.GradientStops.Add(new GradientStop() { Color = Color.FromArgb(0xff, 0x84, 0x7B, 0x6C), Offset = 1d });
            var border = new Rectangle();
            border.Fill = borderBrush;
            //border.Stroke = new SolidColorBrush(Colors.Black);
            //border.StrokeThickness = 1d;

            var background = new Rectangle();
            background.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xAD, 0xA7, 0x9D));
            background.Margin = new Thickness(4d);

            var grid = new Grid();
            grid.Children.Add(border);
            grid.Children.Add(background);
            grid.Children.Add(_canvas);
            grid.Children.Add(_calibrationCanvas);

            this.Content = grid;

            _label.Text = Value.ToString();
            _label.Foreground = new SolidColorBrush(ForeColor);

            _pointLine.Stroke = new SolidColorBrush(Colors.Red);
            _pointLine.StrokeThickness = 2d;
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

                var background1 = new Border();
                background1.BorderThickness = new Thickness(1d, 1d, 1d, 0d);
                background1.BorderBrush = new SolidColorBrush(Color.FromArgb(0xff, 0x74, 0x6A, 0x59));
                background1.Background = new SolidColorBrush(Colors.White);
                background1.SetValue(Canvas.LeftProperty, width * 39d / 657d);
                background1.SetValue(Canvas.TopProperty, width * 42d / 657d);
                background1.Width = width * 579d / 657d;
                background1.Height = width * 343d / 657d;
                _canvas.Children.Add(background1);

                var brush1 = new SolidColorBrush(Colors.White);
                var brush2 = new SolidColorBrush(Color.FromArgb(0xff, 0x84, 0x7B, 0x6C));

                PaintBorder1(width, brush1, brush2);

                PaintBorder2(width, brush1, brush2);

                PaintBorder3(width, brush1, brush2);

                PaintBorder4(width, brush1, brush2);

                PaintBorder5(width, brush1, brush2);

                PaintBorder6(width, brush1, brush2);

                PaintBorder7(width, brush1, brush2);


                var ellipse1 = new Ellipse();
                ellipse1.SetValue(Canvas.LeftProperty, width * 3.5d / 657d);
                ellipse1.SetValue(Canvas.TopProperty, width * 136d / 657d);
                ellipse1.Width = ellipse1.Height = width * 650d / 657d;
                ellipse1.Stroke = new SolidColorBrush(Color.FromArgb(0xff, 0xAF, 0xAF, 0x00));
                ellipse1.StrokeThickness = 3d;
                ellipse1.Clip = new RectangleGeometry() { Rect = new Rect(0d, 0d, ellipse1.Width, width * 120d / 657d) };
                _canvas.Children.Add(ellipse1);

                PaintCalibration(size);

                _canvas.Children.Add(_pointLine);

                var ellipse2 = new Ellipse();
                ellipse2.SetValue(Canvas.LeftProperty, width * 199.5d / 657d);
                ellipse2.SetValue(Canvas.TopProperty, width * 332d / 657d);
                ellipse2.Width = ellipse2.Height = width * 258d / 657d;
                ellipse2.Fill = new SolidColorBrush(Colors.Black);
                ellipse2.Clip = new RectangleGeometry() { Rect = new Rect(0d, 0d, ellipse2.Width, width * 50d / 657d) };
                _canvas.Children.Add(ellipse2);

            }
            catch { }
        }

        private void PaintBorder7(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var rectangle = new Rectangle();
            rectangle.SetValue(Canvas.LeftProperty, width * 170d / 657d + 1d);
            rectangle.SetValue(Canvas.TopProperty, width * 471d / 657d + 1d);
            rectangle.Width = width * 320d / 657d - 2d;
            rectangle.Height = width * 132d / 657d - 2d;
            rectangle.Fill = new SolidColorBrush(Colors.Blue);
            _canvas.Children.Add(rectangle);
        }

        private void PaintBorder6(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var borderLine21 = new Line();
            borderLine21.X1 = width * 170d / 657d;
            borderLine21.X2 = borderLine21.X1;
            borderLine21.Y1 = width * 471d / 657d;
            borderLine21.Y2 = width * 603d / 657d;
            borderLine21.StrokeThickness = 2d;
            borderLine21.Stroke = brush2;
            _canvas.Children.Add(borderLine21);

            var borderLine22 = new Line();
            borderLine22.X1 = borderLine21.X1;
            borderLine22.X2 = width * 490d / 657d;
            borderLine22.Y1 = borderLine21.Y2;
            borderLine22.Y2 = borderLine21.Y2;
            borderLine22.StrokeThickness = 2d;
            borderLine22.Stroke = brush1;
            _canvas.Children.Add(borderLine22);

            var borderLine23 = new Line();
            borderLine23.X1 = borderLine22.X2;
            borderLine23.X2 = borderLine22.X2;
            borderLine23.Y1 = borderLine21.Y1;
            borderLine23.Y2 = borderLine21.Y2;
            borderLine23.StrokeThickness = 2d;
            borderLine23.Stroke = brush1;
            _canvas.Children.Add(borderLine23);

            var borderLine24 = new Line();
            borderLine24.X1 = borderLine21.X1;
            borderLine24.X2 = borderLine22.X2;
            borderLine24.Y1 = borderLine21.Y1;
            borderLine24.Y2 = borderLine21.Y1;
            borderLine24.StrokeThickness = 2d;
            borderLine24.Stroke = brush2;
            _canvas.Children.Add(borderLine24);
        }

        private void PaintBorder5(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var borderLine17 = new Line();
            borderLine17.X1 = width * 580d / 657d;
            borderLine17.X2 = borderLine17.X1;
            borderLine17.Y1 = width * 471d / 657d;
            borderLine17.Y2 = width * 603d / 657d;
            borderLine17.StrokeThickness = 1d;
            borderLine17.Stroke = brush2;
            _canvas.Children.Add(borderLine17);

            var borderLine18 = new Line();
            borderLine18.X1 = borderLine17.X1;
            borderLine18.X2 = width * 610d / 657d;
            borderLine18.Y1 = borderLine17.Y2;
            borderLine18.Y2 = borderLine17.Y2;
            borderLine18.StrokeThickness = 1d;
            borderLine18.Stroke = brush1;
            _canvas.Children.Add(borderLine18);

            var borderLine19 = new Line();
            borderLine19.X1 = borderLine18.X2;
            borderLine19.X2 = borderLine18.X2;
            borderLine19.Y1 = borderLine17.Y1;
            borderLine19.Y2 = borderLine17.Y2;
            borderLine19.StrokeThickness = 1d;
            borderLine19.Stroke = brush1;
            _canvas.Children.Add(borderLine19);

            var borderLine20 = new Line();
            borderLine20.X1 = borderLine17.X1;
            borderLine20.X2 = borderLine18.X2;
            borderLine20.Y1 = borderLine17.Y1;
            borderLine20.Y2 = borderLine17.Y1;
            borderLine20.StrokeThickness = 1d;
            borderLine20.Stroke = brush2;
            _canvas.Children.Add(borderLine20);
        }

        private void PaintBorder4(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var borderLine13 = new Line();
            borderLine13.X1 = width * 520d / 657d;
            borderLine13.X2 = borderLine13.X1;
            borderLine13.Y1 = width * 471d / 657d;
            borderLine13.Y2 = width * 603d / 657d;
            borderLine13.StrokeThickness = 1d;
            borderLine13.Stroke = brush2;
            _canvas.Children.Add(borderLine13);

            var borderLine14 = new Line();
            borderLine14.X1 = borderLine13.X1;
            borderLine14.X2 = width * 550d / 657d;
            borderLine14.Y1 = borderLine13.Y2;
            borderLine14.Y2 = borderLine13.Y2;
            borderLine14.StrokeThickness = 1d;
            borderLine14.Stroke = brush1;
            _canvas.Children.Add(borderLine14);

            var borderLine15 = new Line();
            borderLine15.X1 = borderLine14.X2;
            borderLine15.X2 = borderLine14.X2;
            borderLine15.Y1 = borderLine13.Y1;
            borderLine15.Y2 = borderLine13.Y2;
            borderLine15.StrokeThickness = 1d;
            borderLine15.Stroke = brush1;
            _canvas.Children.Add(borderLine15);

            var borderLine16 = new Line();
            borderLine16.X1 = borderLine13.X1;
            borderLine16.X2 = borderLine14.X2;
            borderLine16.Y1 = borderLine13.Y1;
            borderLine16.Y2 = borderLine13.Y1;
            borderLine16.StrokeThickness = 1d;
            borderLine16.Stroke = brush2;
            _canvas.Children.Add(borderLine16);
        }

        private void PaintBorder3(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var borderLine9 = new Line();
            borderLine9.X1 = width * 110d / 657d;
            borderLine9.X2 = borderLine9.X1;
            borderLine9.Y1 = width * 471d / 657d;
            borderLine9.Y2 = width * 603d / 657d;
            borderLine9.StrokeThickness = 1d;
            borderLine9.Stroke = brush2;
            _canvas.Children.Add(borderLine9);

            var borderLine10 = new Line();
            borderLine10.X1 = borderLine9.X1;
            borderLine10.X2 = width * 140d / 657d;
            borderLine10.Y1 = borderLine9.Y2;
            borderLine10.Y2 = borderLine9.Y2;
            borderLine10.StrokeThickness = 1d;
            borderLine10.Stroke = brush1;
            _canvas.Children.Add(borderLine10);

            var borderLine11 = new Line();
            borderLine11.X1 = borderLine10.X2;
            borderLine11.X2 = borderLine10.X2;
            borderLine11.Y1 = borderLine9.Y1;
            borderLine11.Y2 = borderLine9.Y2;
            borderLine11.StrokeThickness = 1d;
            borderLine11.Stroke = brush1;
            _canvas.Children.Add(borderLine11);

            var borderLine12 = new Line();
            borderLine12.X1 = borderLine9.X1;
            borderLine12.X2 = borderLine10.X2;
            borderLine12.Y1 = borderLine9.Y1;
            borderLine12.Y2 = borderLine9.Y1;
            borderLine12.StrokeThickness = 1d;
            borderLine12.Stroke = brush2;
            _canvas.Children.Add(borderLine12);
        }

        private void PaintBorder2(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var borderLine5 = new Line();
            borderLine5.X1 = width * 50d / 657d;
            borderLine5.X2 = borderLine5.X1;
            borderLine5.Y1 = width * 471d / 657d;
            borderLine5.Y2 = width * 603d / 657d;
            borderLine5.StrokeThickness = 1d;
            borderLine5.Stroke = brush2;
            _canvas.Children.Add(borderLine5);

            var borderLine6 = new Line();
            borderLine6.X1 = borderLine5.X1;
            borderLine6.X2 = width * 80d / 657d;
            borderLine6.Y1 = borderLine5.Y2;
            borderLine6.Y2 = borderLine5.Y2;
            borderLine6.StrokeThickness = 1d;
            borderLine6.Stroke = brush1;
            _canvas.Children.Add(borderLine6);

            var borderLine7 = new Line();
            borderLine7.X1 = borderLine6.X2;
            borderLine7.X2 = borderLine6.X2;
            borderLine7.Y1 = borderLine5.Y1;
            borderLine7.Y2 = borderLine5.Y2;
            borderLine7.StrokeThickness = 1d;
            borderLine7.Stroke = brush1;
            _canvas.Children.Add(borderLine7);

            var borderLine8 = new Line();
            borderLine8.X1 = borderLine5.X1;
            borderLine8.X2 = borderLine6.X2;
            borderLine8.Y1 = borderLine5.Y1;
            borderLine8.Y2 = borderLine5.Y1;
            borderLine8.StrokeThickness = 1d;
            borderLine8.Stroke = brush2;
            _canvas.Children.Add(borderLine8);
        }

        private void PaintBorder1(double width, SolidColorBrush brush1, SolidColorBrush brush2)
        {
            var borderLine1 = new Line();
            borderLine1.X1 = borderLine1.X2 = width * 39d / 657d;
            borderLine1.Y1 = width * 462d / 657d;
            borderLine1.Y2 = width * 611d / 657d;
            borderLine1.StrokeThickness = 3d;
            borderLine1.Stroke = brush1;
            _canvas.Children.Add(borderLine1);

            var borderLine2 = new Line();
            borderLine2.X1 = borderLine1.X1;
            borderLine2.X2 = width * 618d / 657d;
            borderLine2.Y1 = borderLine1.Y2;
            borderLine2.Y2 = borderLine1.Y2;
            borderLine2.StrokeThickness = 3d;
            borderLine2.Stroke = brush2;
            _canvas.Children.Add(borderLine2);

            var borderLine3 = new Line();
            borderLine3.X1 = borderLine3.X2 = borderLine2.X2;
            borderLine3.Y1 = borderLine1.Y1;
            borderLine3.Y2 = borderLine1.Y2;
            borderLine3.StrokeThickness = 3d;
            borderLine3.Stroke = brush2;
            _canvas.Children.Add(borderLine3);

            var borderLine4 = new Line();
            borderLine4.X1 = borderLine1.X1;
            borderLine4.X2 = borderLine2.X2;
            borderLine4.Y1 = borderLine1.Y1;
            borderLine4.Y2 = borderLine1.Y1;
            borderLine4.StrokeThickness = 3d;
            borderLine4.Stroke = brush1;
            _canvas.Children.Add(borderLine4);
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
                var beginAngle = 1.25d * Math.PI;
                var avgMainAngle = 0.5d * Math.PI / mainScale;
                var avgViceAngle = avgMainAngle / viceScale;
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
                    mainLine.X1 = Math.Sin(mainAngle) * width * 325d / 657d + width * 0.5d;
                    mainLine.Y1 = Math.Cos(mainAngle) * width * 325d / 657d + width * 461d / 657d;
                    mainLine.X2 = Math.Sin(mainAngle) * width * 293d / 657d + width * 0.5d;
                    mainLine.Y2 = Math.Cos(mainAngle) * width * 293d / 657d + width * 461d / 657d;
                    _calibrationCanvas.Children.Add(mainLine);

                    for (int j = 1; j < viceScale && i < mainScale; j++)
                    {
                        var viceAngle = mainAngle - j * avgViceAngle;
                        var viceLine = new Line();
                        viceLine.Stroke = brush;
                        viceLine.StrokeThickness = 1d;
                        viceLine.X1 = Math.Sin(viceAngle) * width * 320d / 657d + width * 0.5d;
                        viceLine.Y1 = Math.Cos(viceAngle) * width * 320d / 657d + width * 461d / 657d;
                        viceLine.X2 = Math.Sin(viceAngle) * width * 300d / 657d + width * 0.5d;
                        viceLine.Y2 = Math.Cos(viceAngle) * width * 300d / 657d + width * 461d / 657d;
                        _calibrationCanvas.Children.Add(viceLine);
                    }

                    var text = new TextBlock();
                    text.FontSize = fontSize;
                    text.Foreground = brush;
                    text.Text = Math.Round(minimum + i * avg, decimalDigits).ToString();
                    text.SetValue(Canvas.LeftProperty, Math.Sin(mainAngle) * width * 367d / 657d - text.ActualWidth / 2d + width * 0.5d);
                    text.SetValue(Canvas.TopProperty, Math.Cos(mainAngle) * width * 367d / 657d - text.ActualHeight / 2d + width * 461d / 657d);
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
                var beginAngle = 1.25d * Math.PI;
                var angle = beginAngle - 0.5d * Math.PI * value / (maximum - minimum);

                _pointLine.X1 = Math.Sin(angle) * width * 130d / 657d + width * 0.5d;
                _pointLine.Y1 = Math.Cos(angle) * width * 130d / 657d + width * 461d / 657d;
                _pointLine.X2 = Math.Sin(angle) * width * 324d / 657d + width * 0.5d;
                _pointLine.Y2 = Math.Cos(angle) * width * 324d / 657d + width * 461d / 657d;
            }
            catch { }
        }

        #endregion
    }
}
