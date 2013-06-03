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
    public class Dlfh05 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Line _LineXL = new Line();
        Line _LineJD1 = new Line();
        Line _LineJD2 = new Line();
        Line _LineJD3 = new Line();
        public Dlfh05()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_LineXL);
            _canvas.Children.Add(_LineJD1);
            _canvas.Children.Add(_LineJD2);
            _canvas.Children.Add(_LineJD3);

            this.Width = 30;
            this.Height = 60;
            Paint();

            this.SizeChanged += new SizeChangedEventHandler(Dlfh05_SizeChanged);
        }

        private void Dlfh05_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Height = e.NewSize.Height;
            this.Width = e.NewSize.Height / 2;
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
                else if (name == "GroundWireColor".ToUpper())
                {
                    _GroundWireColor = Common1.StringToColor(value);
                }
                else if (name == "GroundWireWidth".ToUpper())
                {
                    _GroundWireWidth = Convert.ToDouble(value);
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
           typeof(Color), typeof(Dlfh05), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dlfh05), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dlfh05), new PropertyMetadata(0));
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
        typeof(int), typeof(Dlfh05), new PropertyMetadata(0));
        string _DeviceName;
        [DefaultValue("接地线"), Description("透明"), Category("我的属性")]
        public string DeviceName
        {
            get { return _DeviceName; }
            set
            {
                _DeviceName = value;
                SetAttrByName("DeviceName", value);
            }
        }



        private static readonly DependencyProperty GroundWireColorProperty = DependencyProperty.Register("GroundWireColor",
      typeof(int), typeof(Dlfh05), new PropertyMetadata(0));
        Color _GroundWireColor = Common1.StringToColor("#FFFA0000");
        [DefaultValue("#FFFA0000"), Description("接地线颜色"), Category("我的属性")]
        public Color GroundWireColor
        {
            get { return _GroundWireColor; }
            set
            {
                _GroundWireColor = value;
                SetAttrByName("GroundWireColor", value);
                Paint();
            }
        }


        private static readonly DependencyProperty GroundWireWidthProperty = DependencyProperty.Register("GroundWireWidth",
      typeof(int), typeof(Dlfh05), new PropertyMetadata(0));
        double _GroundWireWidth = 1;
        [DefaultValue(2), Description("接地线宽度"), Category("我的属性")]
        public double GroundWireWidth
        {
            get { return _GroundWireWidth; }
            set
            {
                _GroundWireWidth = value;
                SetAttrByName("GroundWireWidth", value);
                Paint();
            }
        }


        private static readonly DependencyProperty LineColorProperty = DependencyProperty.Register("LineColor",
      typeof(int), typeof(Dlfh05), new PropertyMetadata(0));
        Color _LineColor = Colors.Black;
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
      typeof(int), typeof(Dlfh05), new PropertyMetadata(0));
        double _LineWith = 1;
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
            
            _LineXL.X1 = _LineXL.X2 = this.Width /2;
            _LineXL.Y1 = this.Height * 0.33;
            _LineXL.Y2 =this.Height;
            _LineXL.StrokeThickness = Convert.ToDouble(_LineWith);
            _LineXL.Stroke = new SolidColorBrush(_LineColor);
            //40--50--60
            _LineJD3.Y1 = _LineJD3.Y2 = 0;
            _LineJD3.X1 = this.Width * 0.25;
            _LineJD3.X2 = this.Width * 0.75;

            _LineJD2.Y1 = _LineJD2.Y2 = this.Height * (1 - 5d / 6d);
            _LineJD2.X1 = this.Width * 0.1;
            _LineJD2.X2 = this.Width * 0.9;


            _LineJD1.Y1 = _LineJD1.Y2 = this.Height*(1 - 0.67);
            _LineJD1.X1 = 0;
            _LineJD1.X2 = this.Width;

            _LineJD3.StrokeThickness = _LineJD2.StrokeThickness = _LineJD1.StrokeThickness = Convert.ToDouble(_GroundWireWidth);
            _LineJD2.Stroke = _LineJD3.Stroke = _LineJD1.Stroke = new SolidColorBrush(_GroundWireColor);
        }
    }
}
