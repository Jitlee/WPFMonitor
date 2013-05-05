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
using WPFMonitor.Library.MonitorSystemGlobal;
using System.ComponentModel;
using WPFMonitor.Model;

namespace WPFMonitor.Library.ZTControls
{
    /// <summary>
    /// 20	SignalSwitch	2	SignalSwitch.jpg	组态控件	图形开关
    /// </summary>
    public class SignalSwitch: MonitorControl
    {
        public override void SetChannelValue(float fValue)
        {
            OpenOrNot = Common.ConvertToBool(fValue.ToString());
            if (IsFlash)
            {
                if (OpenOrNot == FlashLogic)
                {
                    m_bFlashTimers = !m_bFlashTimers;
                }
            }
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

                if (name == "TrueColor".ToUpper())
                {
                    TrueColor = Common.StringToColor(value);
                }
                else if (name == "FalseColor".ToUpper())
                {
                    FalseColor = Common.StringToColor( value);
                }
                else if (name == "IsFlash".ToUpper())
                {
                    IsFlash = Common.ConvertToBool(value);
                }
                else if (name == "FlashLogic".ToUpper())
                {
                    FlashLogic = Common.ConvertToBool(value);
                }
                else if (name == "Style".ToUpper())
                {
                    style =int.Parse( value);
                }
                else if (name == "OpenOrNot".ToUpper())
                {
                    OpenOrNot =Common.ConvertToBool(value);
                }
            }
            
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
            _Transparent = ScreenElement.Transparent.Value;
        }

        private string[] _browsableProperties = new[] {"Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "Location", "Size", "Font", "OpenOrNot", "style", "TrueColor", "FalseColor", "IsFlash", "FlashLogic", "Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
         typeof(int), typeof(SignalSwitch), new PropertyMetadata(0));
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
            typeof(Color), typeof(SignalSwitch), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));
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
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(SignalSwitch), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));
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
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {

        }

        private static readonly DependencyProperty styleProperty =
           DependencyProperty.Register("style",
           typeof(int), typeof(SignalSwitch), new PropertyMetadata(0, new PropertyChangedCallback(Style_Changed)));

        [DefaultValue(0), Description("开关的形状，０为圆，其他为正方形"), Category("我的属性")]
        public int style
        {
            get { return (int)this.GetValue(styleProperty); }
            set { this.SetValue(styleProperty, value);
            SetAttrByName("Style", value.ToString());
            }
        }

        private static void Style_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnStyleChanged((int)e.OldValue, (int)e.NewValue);
        }

        public void OnStyleChanged(int oldValue, int newValue)
        {
            Paint(RenderSize);
        }

        private static readonly DependencyProperty TrueColorProperty =
           DependencyProperty.Register("TrueColor",
           typeof(Color), typeof(SignalSwitch), new PropertyMetadata(Color.FromArgb(0xff, 0xb1, 0xff, 0xff), new PropertyChangedCallback(TrueColor_Changed)));

        [DefaultValue(typeof(Color), "RGB(177,255,255)"), Description("逻辑真颜色"), Category("我的属性")]
        public Color TrueColor
        {
            get { return (Color)this.GetValue(TrueColorProperty); }
            set { this.SetValue(TrueColorProperty, value);
                SetAttrByName("TrueColor", value.ToString());
            }
        }

        private static void TrueColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnTrueColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnTrueColorChanged(Color oldValue, Color newValue)
        {
            SetForeground();
        }

        private static readonly DependencyProperty FalseColorProperty =
            DependencyProperty.Register("FalseColor",
            typeof(Color), typeof(SignalSwitch), new PropertyMetadata(Color.FromArgb(0xff, 0xff, 0x64, 0x00), new PropertyChangedCallback(FalseColor_Changed)));

        [DefaultValue(typeof(Color), "RGB(255, 100, 0)"), Description("逻辑假颜色"), Category("我的属性")]
        public Color FalseColor
        {
            get { return (Color)this.GetValue(FalseColorProperty); }
            set { this.SetValue(FalseColorProperty, value);
                SetAttrByName("FalseColor", value.ToString());
            }
        }

        private static void FalseColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnFalseColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnFalseColorChanged(Color oldValue, Color newValue)
        {
            SetForeground();
        }

        private static readonly DependencyProperty OpenOrNotProperty = DependencyProperty.Register("OpenOrNot",
            typeof(bool), typeof(SignalSwitch), new PropertyMetadata(default(bool), new PropertyChangedCallback(OpenOrNot_Changed)));

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
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnOpenOrNotChanged((bool)e.OldValue,(bool)e.NewValue);
        }

        public void OnOpenOrNotChanged(bool oldValue, bool newValue)
        {
            SetForeground();
        }

        private static readonly DependencyProperty IsFlashProperty = DependencyProperty.Register("IsFlash",
           typeof(bool), typeof(SignalSwitch), new PropertyMetadata(default(bool), new PropertyChangedCallback(IsFlash_Changed)));

        [DefaultValue("false"), Description("是否闪动效果"), Category("我的属性")]
        public bool IsFlash
        {
            get { return (bool)this.GetValue(IsFlashProperty); }
            set { this.SetValue(IsFlashProperty, value);
            SetAttrByName("IsFlash", value.ToString());
            }
        }

        private static void IsFlash_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnIsFlashChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        public void OnIsFlashChanged(bool oldValue, bool newValue)
        {
            SetForeground();
        }

        private static readonly DependencyProperty FlashLogicProperty = DependencyProperty.Register("FlashLogic",
           typeof(bool), typeof(SignalSwitch), new PropertyMetadata(default(bool), new PropertyChangedCallback(FlashLogic_Changed)));

        [DefaultValue("false"), Description("逻辑真还是逻辑假时闪动"), Category("我的属性")]
        public bool FlashLogic
        {
            get { return (bool)this.GetValue(FlashLogicProperty); }
            set { this.SetValue(FlashLogicProperty, value);
                SetAttrByName("FlashLogic", value.ToString());
            }
        }

        private static void FlashLogic_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            SignalSwitch SignalSwitch = (SignalSwitch)element;
            SignalSwitch.OnFlashLogicChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        public void OnFlashLogicChanged(bool oldValue, bool newValue)
        {
            SetForeground();
        }

        #endregion

        private Canvas _canvas = new Canvas();
        private Path _path = new Path();

        public bool m_bThreadStart = false;
        public bool m_bStop = false;
        public bool m_bFlashTimers = false;

        public SignalSwitch()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_path);

            SetForeground();

            PaintBackground();
        }

        private void SetForeground()
        {
            SolidColorBrush myLineBrush = null;
            if (IsFlash && FlashLogic == OpenOrNot)
            {
                 if (m_bFlashTimers)
                    myLineBrush = new SolidColorBrush(FalseColor);
                else
                    myLineBrush = new SolidColorBrush(TrueColor);
            }
            else if (OpenOrNot)
            {
                myLineBrush = new SolidColorBrush(TrueColor);
            }
            else
            {
                 myLineBrush = new SolidColorBrush(FalseColor);
            }
            _path.Fill = myLineBrush;
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
            //Graphics g = e.Graphics;
            //SolidColorBrush myLineBrush = null;
            //Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            //if (IsFlash && FlashLogic == OpenOrNot)
            //{

                //if (m_bFlashTimers)
                //    myLineBrush = new SolidColorBrush(FalseColor);
                //else
                //    myLineBrush = new SolidColorBrush(TrueColor);


                if (style == 0)
                    //g.FillEllipse(myLineBrush, 0, 0, this.Width, this.Height);
                    _path.Data = new EllipseGeometry() { Center = new Point(finalSize.Width / 2d, finalSize.Height / 2d), RadiusX = finalSize.Width / 2d, RadiusY = finalSize.Height / 2d };
                else
                    //g.FillRectangle(myLineBrush, 0, 0, this.Width, this.Height);
                    _path.Data = new RectangleGeometry() { Rect = new Rect(0d,0d, finalSize.Width, finalSize.Height) };
                //return;
            //}

            //if (penOrNot)
            //    myLineBrush = new SolidBrush(trueColor);
            //else
            //    myLineBrush = new SolidBrush(falseColor);

            //if (Style == 0)
            //    g.FillEllipse(myLineBrush, 0, 0, this.Width, this.Height);
            //else
            //    g.FillRectangle(myLineBrush, 0, 0, this.Width, this.Height);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Paint(availableSize);
            return base.MeasureOverride(availableSize);
        }
    }
}
