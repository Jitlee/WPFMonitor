using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonitorSystem.Controls;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 45	PicBox	2	Text.jpg		窗口式背景控件
    /// </summary>
    public class PicBox: MonitorControl
    {
        Canvas mRect = new Canvas();
        public PicBox()
        {
           this.Content = mRect;
            mRect.MouseLeftButtonDown += new MouseButtonEventHandler(BackgroundRect_MouseRightButtonDown);
            mRect.MouseLeftButtonUp += new MouseButtonEventHandler(BackgroundRect_MouseRightButtonUp);
            mRect.Background = new SolidColorBrush(Colors.White);

            this.SetValue(Canvas.ZIndexProperty, 0);
        }

        #region 双击事件
        DateTime MousedownTime = DateTime.Now;
        int DownNumber = 0;
        private void BackgroundRect_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DownNumber == 1)
            {
               TimeSpan ts= DateTime.Now - MousedownTime;
               if (ts.Minutes > 0 || ts.Seconds > 0 || ts.Milliseconds > 600)
                   DownNumber = 0;
            }
            MousedownTime = DateTime.Now;
        }
        private void BackgroundRect_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan ts = DateTime.Now - MousedownTime;
            if (ts.Minutes == 0 || ts.Seconds == 0 || ts.Milliseconds > 500)
                DownNumber ++;
            if (DownNumber >= 2)
            {
                DownNumber = 0;
                //右双击事件
                AlertWindow();
            }
        }
        /// <summary>
        /// 打开多曲线窗口
        /// </summary>
        private void AlertWindow()
        {
            //HtmlPage.Window.Invoke("ShowDoubleCurve"); 
            throw new Exception();
        }
        #endregion

        #region 属性、设设置

        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                if (name == "BackImageName".ToUpper())
                {
                    _BackImageName =value;
                }
                else if (name == "OpenOrNot".ToUpper())
                {
                    _OpenOrNot =Common1.ConvertToBool(value);
                }
                else if (name == "PicInBack".ToUpper())
                {
                    _PicInBack = Common1.ConvertToBool(value);
                }
            }
            FullRect();
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            Transparent = ScreenElement.Transparent.Value;
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
            "Transparent", "Foreground","Transparent"
            ,"BackImageName","OpenOrNot","PicInBack" };
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(PicBox), new PropertyMetadata(0));
        private int _Transparent;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
               
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }

        private static readonly DependencyProperty BackImageNameProperty = DependencyProperty.Register("BackImageName",
          typeof(string), typeof(PicBox), new PropertyMetadata(""));
        private string _BackImageName ="";
        [ImageAttribute("PIC")]
        [DefaultValue(""), Description("背景图片名字\r\n注意：\r\n背景图片一定要放在程序\r\n所在目录的\\Upload\\PIC下\r\n必须带后缀名"), Category("我的属性")]        
        public string BackImageName
        {
            get { return _BackImageName; }
            set
            {
                _BackImageName = value;
                SetAttrByName("BackImageName", value);
                FullRect();
            }
        }

         private static readonly DependencyProperty OpenOrNotProperty =DependencyProperty.Register("OpenOrNot",
          typeof(bool), typeof(PicBox), new PropertyMetadata(false));
         private bool _OpenOrNot =false;
        [DefaultValue(""), Description("打开或关闭"), Category("我的属性")]
         public bool OpenOrNot
         {
             get { return _OpenOrNot; }
             set
             {
                 _OpenOrNot = value;
                 SetAttrByName("OpenOrNot", value);
                 FullRect();
             }
         }

         private static readonly DependencyProperty PicInBackProperty =DependencyProperty.Register("PicInBack",
          typeof(bool), typeof(PicBox), new PropertyMetadata(false));
         private bool _PicInBack = false;
        [DefaultValue(""), Description("置于底层"), Category("我的属性")]
         public bool PicInBack
         {
             get { return _PicInBack; }
             set { _PicInBack = value; SetAttrByName("PicInBack", value);
             FullRect();
             }
         }
        
        #endregion

         #region 控件公共
         public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


         public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
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

        public List<t_ElementProperty> GetProperty()
        {
            return ListElementProp;
        }
        #endregion


        private void FullRect()
        {
            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PIC\\ButtonImage", _BackImageName  ?? "undefined.tmp");
            if (!string.IsNullOrEmpty(_BackImageName) && System.IO.File.Exists(url))
            {
                BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.Absolute));
                ImageBrush img = new ImageBrush();
                img.ImageSource = bitmap;
                img.Stretch = Stretch.Fill;
                mRect.Background = img;
            }
        }
        

       
    }
}
