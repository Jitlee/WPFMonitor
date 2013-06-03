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
using System.ComponentModel;
using WPFMonitor.Model;
using WPFMonitor;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    public class Switch : MonitorControl
    {
        public override void SetChannelValue(float fValue)
        {
            OpenOrNot = Common1.ConvertToBool(fValue.ToString());
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

        #region 属性设置
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

        #endregion

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
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                if (name == "OpenOrNot".ToUpper())
                {
                   
                        if (value == "1" || value.ToUpper()=="TRUE")
                            OpenOrNot = true;
                        else
                            OpenOrNot = false;
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
            Transparent = ScreenElement.Transparent.Value;

            BackColor = Common1.StringToColor(ScreenElement.BackColor);
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor); 
        }

        private string[] _browsableProperties = new[] { "Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "BackColor", "ForeColor", "OpenOrNot","Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
         typeof(int), typeof(Switch), new PropertyMetadata(0));
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
            typeof(Color), typeof(Switch), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));
        [DefaultValue(""), Description("背景色"), Category("外观")]
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
                Switch Switch = (Switch)element;
                Switch.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
            }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(Switch), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));
        [DefaultValue(""), Description("前景色"), Category("外观")]
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
            Switch Switch = (Switch)element;
            Switch.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
            SetForeground();
        }

        private static readonly DependencyProperty OpenOrNotProperty = DependencyProperty.Register("OpenOrNot",
            typeof(bool), typeof(Switch), new PropertyMetadata(default(bool), new PropertyChangedCallback(OpenOrNot_Changed)));

        [DefaultValue("false"), Description("开关是不是开状态"), Category("我的属性")]
        public bool OpenOrNot
        {
            get { return (bool)this.GetValue(OpenOrNotProperty); }
            set { this.SetValue(OpenOrNotProperty, value);
            SetAttrByName("OpenOrNot", value.ToString());
            }
        }

        private static void OpenOrNot_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            Switch Switch = (Switch)element;
            Switch.OnOpenOrNotChanged((bool)e.OldValue,(bool)e.NewValue);
        }

        public void OnOpenOrNotChanged(bool oldValue, bool newValue)
        {
            Paint(DesiredSize);
        }

        #endregion

        private Canvas _canvas = new Canvas();
        private Rectangle _rect1 = new Rectangle();
        private Ellipse _ellipse1 = new Ellipse();
        private Polygon _polygon = new Polygon();
        private Rectangle _rect2 = new Rectangle();
        private Ellipse _ellipse2 = new Ellipse();

        public Switch()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_rect1);
            _canvas.Children.Add(_ellipse1);
            _canvas.Children.Add(_polygon);
            _canvas.Children.Add(_rect2);
            _canvas.Children.Add(_ellipse2);
            this.Width = 20;
            this.Height = 20;

            SetForeground();

            PaintBackground();
        }

        private void SetForeground()
        {
            var brush = new SolidColorBrush(ForeColor);
            _rect1.Fill = brush;
            _ellipse1.Fill = brush;
            _polygon.Fill = brush;
            _rect2.Fill = brush;
            _ellipse2.Fill = brush;
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                _canvas.Background = new SolidColorBrush();
            }
            else
            {
                _canvas.Background = new SolidColorBrush(BackColor);
            }
        }

        private void Paint(Size finalSize)
        {
            var circle = finalSize.Width / 20d;
            _rect1.SetValue(Canvas.LeftProperty, finalSize.Width / 2d - 1d);
            _rect1.SetValue(Canvas.TopProperty, 0d);
            _rect1.SetValue(WidthProperty, 2d);
            _rect1.SetValue(HeightProperty, finalSize.Height / 3d);

            _ellipse1.SetValue(Canvas.LeftProperty, finalSize.Width / 2d - circle);
            _ellipse1.SetValue(Canvas.TopProperty, finalSize.Height / 3d - circle);
            _ellipse1.SetValue(WidthProperty, 2 * circle);
            _ellipse1.SetValue(HeightProperty, 2 * circle);
            //g.FillRectangle(Brushes.Black, new Rectangle(this.Width / 2 - 1, 0, 2, this.Height / 3));
            //g.FillEllipse(Brushes.Black, this.Width / 2 - circle, this.Height / 3 - circle, 2 * circle, 2 * circle);
            if (OpenOrNot)
            {
                var pp = new PointCollection();
                pp.Add(new Point( 2d * this.Width / 3d,this.Height / 3d));
                
                pp.Add(new Point( 2d * this.Width / 3d + 2d,this.Height / 3d));

                pp.Add(new Point(this.Width / 2d + 1d, 2d * this.Height / 3d));

                pp.Add(new Point(this.Width / 2d - 1d, 2d * this.Height / 3d));

                _polygon.Points = pp;
            }
            else
            {
                var pp = new PointCollection();
                pp.Add(new Point(this.Width / 2d - 1d, this.Height / 3d));

                pp.Add(new Point(this.Width / 2d + 1d, this.Height / 3d));

                pp.Add(new Point(this.Width / 2d + 1d, 2d * this.Height / 3d));

                pp.Add(new Point(this.Width / 2d - 1d, 2d * this.Height / 3d));

                _polygon.Points = pp;
            }
            //g.FillRectangle(Brushes.Black, new Rectangle(this.Width / 2 - 1, 2 * this.Height / 3, 2, this.Height / 3));
            //g.FillEllipse(Brushes.Black, this.Width / 2 - circle, 2 * this.Height / 3 - circle, 2 * circle, 2 * circle);
            
            _rect2.SetValue(Canvas.LeftProperty, finalSize.Width / 2d - 1d);
            _rect2.SetValue(Canvas.TopProperty, 2 * this.Height / 3);
            _rect2.SetValue(WidthProperty, 2d);
            _rect2.SetValue(HeightProperty, finalSize.Height / 3d);

            _ellipse2.SetValue(Canvas.LeftProperty, finalSize.Width / 2d - circle);
            _ellipse2.SetValue(Canvas.TopProperty, 2d * finalSize.Height / 3d - circle);
            _ellipse2.SetValue(WidthProperty, 2 * circle);
            _ellipse2.SetValue(HeightProperty, 2 * circle);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Paint(availableSize);
            return base.MeasureOverride(availableSize);
        }
    }
}
