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
using System.ComponentModel;
using WPFMonitor.Model;
using WPFMonitor.Library.ZTControls;
using WPFMonitor.Library.MonitorSystemGlobal;

namespace WPFMonitor.Library.ZTControls
{
    /// <summary>
    /// 34	ColorText	2	Text.jpg	组态控件	状态文字
    /// </summary>
    public class ColorText : MonitorControl
    {

        public override void SetChannelValue(float fValue)
        {
            OpenOrNot = Common.ConvertToBool(fValue.ToString());
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
                //AdornerLayer.IsLockScale = true;
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
            try
            {
                foreach (var ep in ListElementProp)
                {
                    string name = ep.PropertyName.Trim().ToUpper();
                    string value = ep.PropertyValue.Trim();
                    if (name == "OpenOrNot".ToUpper())
                    {
                        OpenOrNot = Common.ConvertToBool(value);
                    }
                    else if (name == "OpenText".ToUpper())
                    {
                        OpenText = value;
                    }
                    else if (name == "CloseText".ToUpper())
                    {
                        CloseText = value;
                    }
                    // 2009-6-29
                    else if (name == "TrueColor".ToUpper())
                    {
                        TrueColor = Common.StringToColor(value);
                    }
                    else if (name == "FalseColor".ToUpper())
                    {
                        FalseColor = Common.StringToColor(value);
                    }
                }
                //this.Invalidate();
            }
            catch
            {
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
            Transparent = ScreenElement.Transparent.Value;

            ForeColor = Common.StringToColor(ScreenElement.ForeColor);
            BackColor = Common.StringToColor(ScreenElement.BackColor);
        }

        private string[] _browsableProperties = new string[] {"Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "Location", "Size", "Font", "OpenOrNot", "OpenText", "CloseText", "TrueColor", "FalseColor", "Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
        typeof(int), typeof(ColorText), new PropertyMetadata(0));
        private int _Transparent = 0;
        [DefaultValue(""), Description("透明属性"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                if (value == 1)
                {
                    _background.Background = new SolidColorBrush();
                }
                else
                {
                    _background.Background = new SolidColorBrush(BackColor);
                }
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }

        private static readonly DependencyProperty OpenOrNotProperty =
            DependencyProperty.Register("OpenOrNot",
            typeof(bool), typeof(ColorText), new PropertyMetadata(default(bool), new PropertyChangedCallback(OpenOrNot_Changed)));

        /// <summary>
        /// 开关是不是开状态
        /// </summary>
        [DefaultValue("false"), Description("开关是不是开状态"), Category("我的属性")]
        public bool OpenOrNot
        {
            get { return (bool)this.GetValue(OpenOrNotProperty); }
            set
            {
                this.SetValue(OpenOrNotProperty, value);
                SetAttrByName("OpenOrNot", value);
            }
        }

        private static void OpenOrNot_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnOpenOrNotChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        public void OnOpenOrNotChanged(bool oldValue, bool newValue)
        {
            ShowText();
        }

        private static readonly DependencyProperty OpenTextProperty =
            DependencyProperty.Register("OpenText",
            typeof(string), typeof(ColorText), new PropertyMetadata("正常", new PropertyChangedCallback(OpenText_Changed)));

        /// <summary>
        /// 开关是不是开状态
        /// </summary>
        [DefaultValue("正常"), Description("开状态时显示的文本"), Category("我的属性")]
        public string OpenText
        {
            get { return (string)this.GetValue(OpenTextProperty); }
            set
            {
                this.SetValue(OpenTextProperty, value);
                SetAttrByName("OpenText", value);
            }
        }

        private static void OpenText_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnOpenTextChanged((string)e.NewValue, (string)e.OldValue);
        }

        public void OnOpenTextChanged(string oldValue, string newValue)
        {
            ShowText();
        }

        private static readonly DependencyProperty CloseTextProperty =
            DependencyProperty.Register("CloseText",
            typeof(string), typeof(ColorText), new PropertyMetadata("报警", new PropertyChangedCallback(CloseText_Changed)));

        /// <summary>
        /// 关状态显示的文本
        /// </summary>
        [DefaultValue("报警"), Description("关状态时显示的文本"), Category("我的属性")]
        public string CloseText
        {
            get { return (string)this.GetValue(CloseTextProperty); }
            set
            {
                this.SetValue(CloseTextProperty, value);
                SetAttrByName("CloseText", value);
            }
        }

        private static void CloseText_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnCloseTextChanged((string)e.NewValue, (string)e.OldValue);
        }

        public void OnCloseTextChanged(string oldValue, string newValue)
        {
            ShowText();
        }

        private static readonly DependencyProperty TrueColorProperty =
            DependencyProperty.Register("TrueColor",
            typeof(Color), typeof(ColorText), new PropertyMetadata(Color.FromArgb(0xff, 0xb1, 0xff, 0xff), new PropertyChangedCallback(TrueColor_Changed)));

        /// <summary>
        /// 逻辑真颜色
        /// </summary>
        [DefaultValue("RGB(177,255,255)"), Description("逻辑真颜色"), Category("我的属性")]
        public Color TrueColor
        {
            get { return (Color)this.GetValue(TrueColorProperty); }
            set
            {
                this.SetValue(TrueColorProperty, value);
                SetAttrByName("TrueColor", value.ToString());
            }
        }

        private static void TrueColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnTrueColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnTrueColorChanged(Color oldValue, Color newValue)
        {
            ShowText();
        }

        private static readonly DependencyProperty FalseColorProperty =
            DependencyProperty.Register("FalseColor",
            typeof(Color), typeof(ColorText), new PropertyMetadata(Color.FromArgb(0xff, 0xff, 0x64, 0x00), new PropertyChangedCallback(FalseColor_Changed)));

        /// <summary>
        /// 逻辑假颜色
        /// </summary>
        [DefaultValue("RGB(255,100,0)"), Description("逻辑假颜色"), Category("我的属性")]
        public Color FalseColor
        {
            get { return (Color)this.GetValue(FalseColorProperty); }
            set
            {
                this.SetValue(FalseColorProperty, value);

                SetAttrByName("FalseColor", FalseColor.ToString());
            }
        }

        private static void FalseColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnFalseColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnFalseColorChanged(Color oldValue, Color newValue)
        {
            ShowText();
        }

        private static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor",
            typeof(Color), typeof(ColorText), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));

        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set
            {
                this.SetValue(BackColorProperty, value);

                if (ScreenElement != null)
                    ScreenElement.BackColor = value.ToString();
            }
        }

        private static void BackColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(ColorText), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));

        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set
            {
                this.SetValue(ForeColorProperty, value);
                if (ScreenElement != null)
                    ScreenElement.ForeColor = value.ToString();
            }
        }

        private static void ForeColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            ColorText ColorText = (ColorText)element;
            ColorText.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
        }

        #endregion

        private Grid _background = new Grid();
        private TextBlock _text = new TextBlock() { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left };
        public ColorText()
        {
            this.Content = _background;
            _background.Children.Add(_text);

            ShowText();

            PaintBackground();
        }

        private void PaintBackground()
        {
         //   _background.Background = new SolidColorBrush(BackColor);
        }

        private void ShowText()
        {
            if (_Transparent == 1)
            {
                _background.Background = new SolidColorBrush();
            }
            else
            {
                _background.Background = new SolidColorBrush(BackColor);
            }
            if (OpenOrNot)
            {
                _text.Foreground = new SolidColorBrush(TrueColor);//如一SolidBrush
                _text.Text = OpenText;
            }
            else
            {
                _text.Foreground = new SolidColorBrush(FalseColor);//如一SolidBrush   
                _text.Text = CloseText;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (availableSize.Width / availableSize.Height != 2d)
            {
                availableSize.Width = availableSize.Height * 2d;
            }
            return base.MeasureOverride(availableSize);
        }
    }
}