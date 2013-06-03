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
using WPFMonitor.Model.ZTControls;
using System.ComponentModel;

namespace MonitorSystem.Dlfh
{
    
    public class Dlfh08: MonitorControl
    {
        private Canvas _canvas = new Canvas();
        private Line _LineY1 = new Line();
        private Line _LineY2 = new Line();
        private Rectangle _Rect = new Rectangle();
        public Dlfh08()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_Rect);
            
            _canvas.Children.Add(_LineY1);
            _canvas.Children.Add(_LineY2);
            this.Width = 60;
            this.Height = 60;
            
            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Dlfh08_SizeChanged);
        }

        private void Dlfh08_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Height = this.Width = e.NewSize.Width;
            Paint();
        }

        #region 公共
        #region 函数
        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
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

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
                AdornerLayer.IsLockScale = true;
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

        public override void SetChannelValue(float fValue, float dValue)
        {

        }
        #endregion

        #region 属性
        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                if (name == "DeviceName".ToUpper())
                {
                    _DeviceName = value;
                }
                else if (name == "LineColor".ToUpper())
                {
                    _LineColor = Common1.StringToColor(value);
                }
                else if (name == "LineWidth".ToUpper())
                {
                    _LineWith = Convert.ToDouble(value);
                }
            }
            Paint();
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



        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
           "BackColor", "ForeColor", "Transparent","Translate"
        ,"DeviceName","GroundWireColor","GroundWireWidth","LineColor","LineWidth"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dlfh08), new PropertyMetadata(Colors.White));
        [DefaultValue(""), Description("背景色"), Category("外观")]
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

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(Dlfh08), new PropertyMetadata(Colors.Black));
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


        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
        typeof(int), typeof(Dlfh08), new PropertyMetadata(0));
        private int _Transparent = 0;
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
        #endregion

        #endregion
        #region 自定义属性
        private static readonly DependencyProperty DeviceNameProperty = DependencyProperty.Register("DeviceName",
        typeof(int), typeof(Dlfh08), new PropertyMetadata(0));
        string _DeviceName;
        [DefaultValue(""), Description("透明"), Category("我的属性")]
        public string DeviceName
        {
            get { return _DeviceName; }
            set
            {
                _DeviceName = value;
                SetAttrByName("DeviceName", value);
            }
        }


        private static readonly DependencyProperty LineColorProperty = DependencyProperty.Register("LineColor",
      typeof(int), typeof(Dlfh08), new PropertyMetadata(0));
        Color _LineColor = Common1.StringToColor("#FFED1212");
        [DefaultValue(""), Description("线路颜色"), Category("我的属性")]
        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                SetAttrByName("LineColor", value);
                Paint();
            }
        }


        private static readonly DependencyProperty LineWithProperty = DependencyProperty.Register("LineWidth",
      typeof(int), typeof(Dlfh08), new PropertyMetadata(0));
        double _LineWith = 2;
        [DefaultValue(2), Description("线路宽度"), Category("我的属性")]
        public double LineWidth
        {
            get { return _LineWith; }
            set
            {
                _LineWith = value;
                SetAttrByName("LineWidth", value);
                Paint();
            }
        }
        #endregion

        private void Paint()
        {
            //相隔距离
            double _jl = 0.23;
            double topJL = 0.18;

            _LineY1.X1 = _LineY1.X2 = this.Width * 0.38;
            _LineY1.Y1 = this.Height * topJL;
            _LineY1.Y2 = this.Height * 0.78;


            _LineY2.X1 = _LineY2.X2 = this.Width * (0.38+_jl);
            _LineY2.Y1 = this.Height * topJL;
            _LineY2.Y2 = this.Height * 0.78;
            

            //圆
            _Rect.Width = _Rect.Height = _Rect.RadiusX = _Rect.RadiusY = this.Width;
            _Rect.Fill = new SolidColorBrush();
            _LineY1.StrokeThickness = _LineY2.StrokeThickness = _Rect.StrokeThickness = _LineWith;
            _LineY1.Stroke = _LineY2.Stroke = _Rect.Stroke = new SolidColorBrush(_LineColor); 
        }
    }
}
