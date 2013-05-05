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
using System.Windows.Shapes;
using WPFMonitor.Library.MonitorSystemGlobal;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;


namespace WPFMonitor.Library.ZTControls
{
    /// <summary>
    /// 32	BackgroundRect	2	Text.jpg	组态控件	背景框
    /// Degrees	1.0	矩形圆周半径  FromColor	222,222,222	背景色  ToColor	177,224,224	前景色
    /// </summary>
    public class BackgroundRect: MonitorControl
    {
        Rectangle mRect = new Rectangle();
        public BackgroundRect()
        {
           this.Content = mRect;

            mRect.Width = 80;
            mRect.Height = 50;
            mRect.MouseLeftButtonDown += new MouseButtonEventHandler(BackgroundRect_MouseRightButtonDown);
            mRect.MouseLeftButtonUp += new MouseButtonEventHandler(BackgroundRect_MouseRightButtonUp);
            

            this.SizeChanged += new SizeChangedEventHandler(zedGraphCtrl_SizeChanged);
            this.SetValue(Canvas.ZIndexProperty, 0);
        }
        private void zedGraphCtrl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mRect.Width = e.NewSize.Width;
            mRect.Height = e.NewSize.Height;
        }
        #region 又双击事件
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
            HtmlPage.Window.Invoke("ShowDoubleCurve"); 
        }
        #endregion

        #region 属性、设设置

        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                if (name == "Degrees".ToUpper())
                {
                    _Degrees = double.Parse(value);
                }
                else if (name == "FromColor".ToUpper())
                {
                    _FromColor = Common.StringToColor(value);
                }
                else if (name == "ToColor".ToUpper())
                {
                    _ToColor = Common.StringToColor(value);
                }
                FullRect();
            }
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
            ,"Degrees","FromColor","ToColor" };
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(BackgroundRect), new PropertyMetadata(0));
        private int _Transparent;
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

        private static readonly DependencyProperty DegreesProperty = DependencyProperty.Register("Degrees",
          typeof(double), typeof(MonitorText), new PropertyMetadata(0.0));
        private double _Degrees =0.0;
        public double Degrees
        {
            get { return _Degrees; }
            set
            {
                _Degrees = value;
                SetAttrByName("Degrees", value);
                FullRect();
            }
        }

         private static readonly DependencyProperty FromColorProperty =DependencyProperty.Register("FromColor",
          typeof(Color), typeof(MonitorText), new PropertyMetadata(Colors.White));
         private Color _FromColor = Colors.White;
         public Color FromColor
         {
             get { return _FromColor; }
             set
             {
                 _FromColor = value;
                 SetAttrByName("FromColor", value);
                 FullRect();
             }
         }

         private static readonly DependencyProperty ToColorProperty =DependencyProperty.Register("ToColor",
          typeof(Color), typeof(MonitorText), new PropertyMetadata(Colors.White));
         private Color _ToColor = Colors.White;
         public Color ToColor
         {
             get { return _ToColor; }
             set { _ToColor = value; SetAttrByName("ToColor", value);
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
            PaintBackground();
            mRect.RadiusX = mRect.RadiusY = _Degrees;
        }
        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                mRect.Fill = new SolidColorBrush();
            }
            else
            {
                LinearGradientBrush lg = new LinearGradientBrush();
                lg.StartPoint = new Point(0, 0);
                lg.EndPoint = new Point(1, 1);

                GradientStop gstart = new GradientStop();
                gstart.Offset = 0;
                gstart.Color = _FromColor;

                GradientStop gEnd = new GradientStop();
                gEnd.Offset = 1.0;
                gEnd.Color = _ToColor;

                lg.GradientStops.Clear();
                lg.GradientStops.Add(gstart);
                lg.GradientStops.Add(gEnd);
                mRect.Fill = lg;
            }
        }

       
    }
}
