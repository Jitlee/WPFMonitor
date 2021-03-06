﻿using System;
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
using System.Windows.Media.Imaging;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 28	DetailSwitch	2	DetailSwitch.jpg	组态控件	普通开关
    /// </summary>
    public class DetailSwitch : MonitorControl
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
                    OpenOrNot = Common1.ConvertToBool(value);
                }
                else if (name == "IsRightDirect".ToUpper())
                {
                    IsRightDirect = Common1.ConvertToBool(value);
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
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor); 
        }

        private string[] _browsableProperties = new[] {"Width", "Height", "Left", "Top", "FontFamily", "FontSize", "Location", "Size", "Font", "ForeColor", 
            "OpenOrNot", "IsRightDirect", "Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
        typeof(int), typeof(DetailSwitch), new PropertyMetadata(0));
        private int _Transparent = 0;
        [DefaultValue(""), Description("透明属性"), Category("杂项")]
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
            typeof(Color), typeof(DetailSwitch), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));

        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set { this.SetValue(BackColorProperty, value); }
        }

        private static void BackColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DetailSwitch DetailSwitch = (DetailSwitch)element;
            DetailSwitch.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(DetailSwitch), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));
        [DefaultValue(""), Description("前景色"), Category("外观")]
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
            DetailSwitch DetailSwitch = (DetailSwitch)element;
            DetailSwitch.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
            SetForeground();
        }

        private static readonly DependencyProperty OpenOrNotProperty = DependencyProperty.Register("OpenOrNot",
            typeof(bool), typeof(DetailSwitch), new PropertyMetadata(default(bool), new PropertyChangedCallback(OpenOrNot_Changed)));

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
            DetailSwitch DetailSwitch = (DetailSwitch)element;
            DetailSwitch.OnOpenOrNotChanged((bool)e.OldValue,(bool)e.NewValue);
        }

        public void OnOpenOrNotChanged(bool oldValue, bool newValue)
        {
            Paint();
        }

        private static readonly DependencyProperty IsRightDirectProperty = DependencyProperty.Register("IsRightDirect",
            typeof(bool), typeof(DetailSwitch), new PropertyMetadata(default(bool), new PropertyChangedCallback(IsRightDirect_Changed)));

        [DefaultValue("false"), Description("正相与反相的确定.正相时1为打开图片"), Category("我的属性")]
        public bool IsRightDirect
        {
            get { return (bool)this.GetValue(IsRightDirectProperty); }
            set { this.SetValue(IsRightDirectProperty, value);
                SetAttrByName("IsRightDirect", value.ToString());
            }
        }

        private static void IsRightDirect_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DetailSwitch DetailSwitch = (DetailSwitch)element;
            DetailSwitch.OnIsRightDirectChanged((bool)e.OldValue,(bool)e.NewValue);
        }

        public void OnIsRightDirectChanged(bool oldValue, bool newValue)
        {
            Paint();
        }
        #endregion

        private Image _image = new Image();

        public DetailSwitch()
        {
            this.Content = _image;
            _image.Stretch = Stretch.Fill;

            SetForeground();

            PaintBackground();

            Paint();
        }

        private void SetForeground()
        {
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                this.Background = new SolidColorBrush();
            }
            else
            {
                this.Background = new SolidColorBrush(BackColor);
            }
        }

        private void Paint()
        {
            var iOpen = "/WPFMonitor;component/Resources/Images/ControlsImg/Open.jpg";
            var iClose = "/WPFMonitor;component/Resources/Images/ControlsImg/Close.jpg";
            
            _image.Source = new BitmapImage(new Uri(iOpen, UriKind.RelativeOrAbsolute));
            if (IsRightDirect)
            {
                if (OpenOrNot)
                {
                    _image.Source = new BitmapImage(new Uri(iOpen, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    _image.Source = new BitmapImage(new Uri(iClose, UriKind.RelativeOrAbsolute));
                }
            }
            else
            {
                if (OpenOrNot == false)
                {
                    _image.Source = new BitmapImage(new Uri(iOpen, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    _image.Source = new BitmapImage(new Uri(iClose, UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}
