using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WPFMonitor.Model;
using System.ComponentModel;
using MonitorSystem.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public partial class BackgroundControl : MonitorControl
    {
        private Grid _layoutRoot = new Grid();
        private Border _border = new Border();
        private Button _toolTipButton = new Button();
        public readonly Canvas BackgroundCanvas = new Canvas();

        #region 属性

        #region 背景

        #region 填充开始颜色

        private static readonly DependencyProperty FromColorProperty =
           DependencyProperty.Register("FromColor",
           typeof(Color), typeof(BackgroundControl), new PropertyMetadata(Color.FromArgb(0xff, 0x58, 0x50, 0x4E), FromColorPropertyChanged));

        [DefaultValue(""), Description("开始颜色"), Category("背景")]
        public Color FromColor
        {
            get { return (Color)GetValue(FromColorProperty); }
            set { SetValue(FromColorProperty, value); SetAttrByName("FromColor", value.ToString()); }
        }

        private static void FromColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnFromColorChanged((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void OnFromColorChanged(Color oldValue, Color newValue)
        {
            UpdateBackground();
        }

        #endregion

        #region 填充终止颜色

        private static readonly DependencyProperty ToColorProperty =
           DependencyProperty.Register("ToColor",
           typeof(Color), typeof(BackgroundControl), new PropertyMetadata(Color.FromArgb(0xff, 0x26, 0x29, 0x20), ToColorPropertyChanged));

        [DefaultValue(""), Description("终止颜色"), Category("背景")]
        public Color ToColor
        {
            get { return (Color)GetValue(ToColorProperty); }
            set { SetValue(ToColorProperty, value); SetAttrByName("ToColor", value.ToString()); }
        }

        private static void ToColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnToColorChanged((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void OnToColorChanged(Color oldValue, Color newValue)
        {
            UpdateBackground();
        }

        #endregion

        #region 颜色方向

        private static readonly DependencyProperty ColorDirectionProperty =
           DependencyProperty.Register("ColorDirection",
           typeof(Orientation), typeof(BackgroundControl), new PropertyMetadata(Orientation.Horizontal, ColorDirectionPropertyChanged));

        [DefaultValue(""), Description("颜色方向"), Category("背景")]
        public Orientation ColorDirection
        {
            get { return (Orientation)GetValue(ColorDirectionProperty); }
            set { SetValue(ColorDirectionProperty, value); SetAttrByName("ColorDirection", value == Orientation.Horizontal ? 0 : 1); }
        }

        private static void ColorDirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnColorDirectionChanged((Orientation)e.OldValue, (Orientation)e.NewValue);
            }
        }

        private void OnColorDirectionChanged(Orientation oldValue, Orientation newValue)
        {
            UpdateBackground();
        }

        #endregion

        #region 背景图片

        private static readonly DependencyProperty BackImageProperty =
           DependencyProperty.Register("BackImage",
           typeof(string), typeof(BackgroundControl), new PropertyMetadata(BackImagePropertyChanged));

        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("背景图片"), Category("背景")]
         public string BackImage
        {
            get { return (string)GetValue(BackImageProperty); }
            set { SetValue(BackImageProperty, value); SetAttrByName("BackImage", value.ToString()); }
        }

        private static void BackImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnBackImageChanged((string)e.OldValue, (string)e.NewValue);
            }
        }

        private void OnBackImageChanged(string oldValue, string newValue)
        {
            UpdateBackground();
        }

        #endregion

        private void UpdateBackground()
        {
            if (string.IsNullOrEmpty(BackImage))
            {
                var brush = new LinearGradientBrush();
                if (ColorDirection == Orientation.Horizontal)
                {
                    brush.StartPoint = new Point();
                    brush.EndPoint = new Point(0d, 1d);
                }
                else
                {
                    brush.StartPoint = new Point();
                    brush.EndPoint = new Point(1d, 0d);
                }
                brush.GradientStops.Add(new GradientStop() { Offset = 0d, Color = FromColor });
                brush.GradientStops.Add(new GradientStop() { Offset = 1d, Color = ToColor });
                _border.Background = brush;
            }
            else
            {
                _border.Background = new ImageBrush() {
                    Stretch= Stretch.UniformToFill,
                    //ImageSource = new BitmapImage(new Uri(string.Concat(AppDomain.CurrentDomain.BaseDirectory, string.Concat("../Upload/Pic/", BackImage.Trim('/')))))
                };
            }
        }

        #endregion

        #region 边线颜色

        private static readonly DependencyProperty StrokeProperty =
           DependencyProperty.Register("Stroke",
           typeof(Color), typeof(BackgroundControl), new PropertyMetadata(Color.FromArgb(0xff, 0x64, 0x64, 0x64), StrokePropertyChanged));

        [DefaultValue(""), Description("边线颜色"), Category("杂项")]
        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); SetAttrByName("Stroke", value.ToString()); }
        }

        private static void StrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnStrokeChanged((Color)e.OldValue, (Color)e.NewValue);
            }
        }

        private void OnStrokeChanged(Color oldValue, Color newValue)
        {
            _border.BorderBrush = new SolidColorBrush(newValue);
        }

        #endregion

        #region 边线粗细

        private static readonly DependencyProperty StrokeThicknessProperty =
           DependencyProperty.Register("StrokeThickness",
           typeof(double), typeof(BackgroundControl), new PropertyMetadata(1d, StrokeThicknessPropertyChanged));

        [DefaultValue(""), Description("边线粗细"), Category("杂项")]
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); SetAttrByName("StrokeThickness", value.ToString()); }
        }

        private static void StrokeThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnStrokeThicknessChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void OnStrokeThicknessChanged(double oldValue, double newValue)
        {
            _border.BorderThickness = new Thickness(newValue);
            UpdateClip();
        }

        #endregion

        #region 圆角度

        private static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius",
           typeof(double), typeof(BackgroundControl), new PropertyMetadata(10d, CornerRadiusPropertyChanged));

        [DefaultValue(""), Description("圆角度"), Category("杂项")]
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); SetAttrByName("CornerRadius", value.ToString()); }
        }

        private static void CornerRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnCornerRadiusChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void OnCornerRadiusChanged(double oldValue, double newValue)
        {
            this._border.CornerRadius = new CornerRadius(newValue);
            UpdateClip();
        }

        #endregion

        #region 透明度

        private static readonly DependencyProperty TransparentProperty =
           DependencyProperty.Register("Transparent",
           typeof(double), typeof(BackgroundControl), new PropertyMetadata(100d, TransparentPropertyChanged));

        [DefaultValue(""), Description("透明度(0~100)"), Category("杂项")]
        public double Transparent
        {
            get { return (double)GetValue(TransparentProperty); }
            set
            {
                SetValue(TransparentProperty, value);
                if (ScreenElement != null)
                    ScreenElement.Transparent = (int)value;
            }
        }

        private static void TransparentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as BackgroundControl;
            if (null != element)
            {
                element.OnTransparentChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private void OnTransparentChanged(double oldValue, double newValue)
        {
            this.Translate = newValue;
        }

        #endregion

        #endregion

        public BackgroundControl()
        {
            _border.CornerRadius = new CornerRadius(CornerRadius);
            _border.BorderThickness = new Thickness(StrokeThickness);
            _border.BorderBrush = new SolidColorBrush(Stroke);
            _toolTipButton.Style = Application.Current.Resources["ImageButtonStyle"] as Style;
            _layoutRoot.Children.Add(_border);
            _layoutRoot.Children.Add(BackgroundCanvas);
            _layoutRoot.Children.Add(_toolTipButton);
            UpdateBackground();
            Content = _layoutRoot;
            //Background = new SolidColorBrush(Colors.Red);
            this.SizeChanged += BackgroundControl_SizeChanged;
            this.Selected += BackgroundControl_Selected;
            _toolTipButton.Click += ToolTipButton_Click;
        }

        private void ToolTipButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != AdornerLayer)
            {
                AdornerLayer.ToggleToolTip();
            }
        }

        private void BackgroundControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateClip();
        }

        private void UpdateClip()
        {
            var cornerRadius = CornerRadius - StrokeThickness / 2d;
            if (cornerRadius < 0d)
            {
                cornerRadius = 0d;
            }
            BackgroundCanvas.Clip = new RectangleGeometry() { Rect = new Rect(StrokeThickness, StrokeThickness, Width - 2d * StrokeThickness, Height - 2d * StrokeThickness), RadiusX = cornerRadius, RadiusY = cornerRadius };
        }

        private void BackgroundControl_Selected(object sender, EventArgs e)
        {
            BackgroundControl.OnUpdatePropertyGrid(this.BrowsableProperties, this.GetRootControl());
        }

        public bool Contains(double left, double top)
        {
            var x = Canvas.GetLeft(this);
            var y = Canvas.GetTop(this);
            var w = this.Width;
            var h = this.Height;
            return new Rect(x, y, w, h).Contains(new Point(left, top));
        }

        public Point GetPosition()
        {
            return new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
        }

        #region 重载

        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    if (IsDesignMode && null != AdornerLayer)
        //    {
        //        AdornerLayer.IsSelected = true;
        //        AdornerLayer.OnSelected();
        //    }
        //    e.Handled = true;
        //    base.OnMouseLeftButtonDown(e);
        //}

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                this._border.IsHitTestVisible = false;
                AdornerLayer = new Adorner(this);
                if (AllowToolTip && null == this.ParentControl)
                {
                    this.SetValue(Canvas.ZIndexProperty, 10000);
                }
                else
                {
                    this.ClearValue(Canvas.ZIndexProperty);
                }
                //AdornerLayer.AllowMove = false;
                AdornerLayer.Selected += OnSelected;
                AdornerLayer.Unselected += OnUnselected;
                AdornerLayer.AllToolTip = false;

                var children = BackgroundCanvas.Children.OfType<MonitorControl>().ToArray();
                foreach (var child in children)
                {
                    var monitor = child as MonitorControl;
                    if (null != monitor)
                    {
                        monitor.DesignMode();
                        monitor.AllowToolTip = false;
                        monitor.ClearValue(Canvas.ZIndexProperty);
                        if (null != monitor.AdornerLayer)
                        {
                            monitor.AdornerLayer.AllToolTip = false;
                        }
                    }
                }
            }
        }

        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                this._border.IsHitTestVisible = true;
                this.ClearValue(Canvas.ZIndexProperty);
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.Unselected -= OnUnselected;
                AdornerLayer.Dispose();
                AdornerLayer = null;

                var children = BackgroundCanvas.Children.OfType<MonitorControl>().ToArray();
                foreach (var child in children)
                {
                    var monitor = child as MonitorControl;
                    if (null != monitor && monitor.IsDesignMode)
                    {
                        monitor.UnDesignMode();
                    }
                }
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            //this.SetValue(Canvas.ZIndexProperty, 10000);
            if (AllowToolTip)
            {
                _toolTipButton.Visibility = Visibility.Visible;
            }
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
                //foreach (var child in ToolTipCanvas.Children)
                //{
                //    if (child is MonitorControl)
                //    {
                //        (child as MonitorControl).DesignMode();
                //    }
                //}
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
            //this.ClearValue(Canvas.ZIndexProperty);
            _toolTipButton.Visibility = Visibility.Collapsed;
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;

                if (name == "FromColor".ToUpper())
                {
                    FromColor = Common1.StringToColor(value);
                }
                else if (name == "ToColor".ToUpper())
                {
                    ToColor = Common1.StringToColor(value);
                }
                else if (name == "FillDirection".ToUpper())
                {
                    int dirctionValue;
                    if (int.TryParse(value, out dirctionValue))
                    {
                        ColorDirection = dirctionValue == 0 ? Orientation.Horizontal : Orientation.Vertical;
                    }
                    else
                    {
                        ColorDirection = Orientation.Horizontal;
                    }
                }
                else if (name == "BackImage".ToUpper())
                {
                    BackImage = value;
                }
                else if (name == "Stroke".ToUpper())
                {
                    Stroke = Common1.StringToColor(value);
                }
                else if (name == "StrokeThickness".ToUpper())
                {
                    StrokeThickness = Convert.ToDouble(value);
                }
                else if (name == "CornerRadius".ToUpper())
                {
                    CornerRadius = Convert.ToDouble(value);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            Transparent = ScreenElement.Transparent.HasValue ? ScreenElement.Transparent.Value : 100d;
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        private string[] m_BrowsableProperties = new string[] { "Width", "Height", "FromColor", "ToColor", "ColorDirection", "BackImage", "Stroke", "StrokeThickness", "Transparent", "CornerRadius" };
        public override string[] BrowsableProperties
        {
            get
            {
                return m_BrowsableProperties;
            }
            set
            {
                m_BrowsableProperties = value;
            }
        }

        #endregion
    }
}
