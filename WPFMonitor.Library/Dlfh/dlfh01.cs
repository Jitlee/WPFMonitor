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
using WPFMonitor.Model;
using System.ComponentModel;

namespace WPFMonitor.Library.Dlfh
{
    /// <summary>
    /// 电力符号
    /// </summary>
    public class Dlfh01 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        private Line _LineX1 = new Line();
        private Line _LineX2 = new Line();
        private Line _LineY1 = new Line();
        private Line _LineY2 = new Line();
        public Dlfh01()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_LineX1);
            _canvas.Children.Add(_LineX2);
            _canvas.Children.Add(_LineY1);
            _canvas.Children.Add(_LineY2);
            this.Width = 60;
            this.Height = 30;
            Paint();

            this.SizeChanged +=new SizeChangedEventHandler(Dlfh01_SizeChanged);
        }

        private void Dlfh01_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.5;
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
                    _DeviceName=value;
                }
                else if (name == "Voltagelevel".ToUpper())
                {
                    _Voltagelevel = int.Parse(value);
                }
                 else if (name == "CapacitiveColor".ToUpper())
                {
                    _CapacitiveColor = Common.StringToColor(value);
                }
                 else if (name == "CapacitiveWidth".ToUpper())
                {
                    _CapacitiveWidth = Convert.ToDouble(value);
                }
                 else if (name == "LineColor".ToUpper())
                {
                      _LineColor=Common.StringToColor(value);
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

            BackColor = Common.StringToColor(ScreenElement.BackColor);
            ForeColor = Common.StringToColor(ScreenElement.ForeColor);
        }



        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
           "BackColor", "ForeColor", "Transparent","Translate"
        ,"DeviceName","Voltagelevel","CapacitiveColor","CapacitiveWidth","LineColor","LineWidth"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dlfh01), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dlfh01), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
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
        typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
        string _DeviceName;
        [DefaultValue("电容器"), Description("透明"), Category("我的属性")]
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value;
            SetAttrByName("DeviceName", value);    
            }
        }

        private static readonly DependencyProperty VoltagelevelProperty = DependencyProperty.Register("Voltagelevel",
       typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
        int _Voltagelevel;
        [DefaultValue("10"), Description("电压等级"), Category("我的属性")]
        public int Voltagelevel
        {
            get { return _Voltagelevel; }
            set { _Voltagelevel = value;
            SetAttrByName("Voltagelevel", value);
            }
        }

        private static readonly DependencyProperty CapacitiveColorProperty = DependencyProperty.Register("CapacitiveColor",
      typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
        Color _CapacitiveColor = Common.StringToColor("#FFFA0000");
        [DefaultValue("#FFFA0000"), Description("电容颜色"), Category("我的属性")]
        public Color CapacitiveColor
        {
            get { return _CapacitiveColor; }
            set { _CapacitiveColor = value;
            SetAttrByName("CapacitiveColor", value);
                  Paint();
            }
        }


        private static readonly DependencyProperty CapacitiveWidthProperty = DependencyProperty.Register("CapacitiveWidth",
      typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
        double _CapacitiveWidth=1;
        [DefaultValue(2), Description("电容宽度"), Category("我的属性")]
        public double CapacitiveWidth
        {
            get { return _CapacitiveWidth; }
            set { _CapacitiveWidth = value;
            SetAttrByName("CapacitiveWidth", value);
            Paint();
            }
        }


        private static readonly DependencyProperty LineColorProperty = DependencyProperty.Register("LineColor",
      typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
        Color _LineColor=Colors.Black;
        [DefaultValue(""), Description("线路颜色"), Category("我的属性")]
        public Color LineColor
        {
            get { return _LineColor; }
            set { 
                _LineColor = value;
                SetAttrByName("LineColor", value);
                Paint();
            }
        }


        private static readonly DependencyProperty LineWithProperty = DependencyProperty.Register("LineWidth",
      typeof(int), typeof(Dlfh01), new PropertyMetadata(0));
        double _LineWith=0.5d;
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
            //中间10%;
           //第一条线的位置
            _LineX2.Y2 = _LineX2.Y1 = _LineX1.Y2 = _LineX1.Y1 = this.Height / 2;

            _LineX1.X1 = 0;
            _LineX1.X2 = this.Width * 0.45;
            //第二条线
            _LineX2.X1 = this.Width * 0.55;
            _LineX2.X2 = this.Width;
            //属性设置
            _LineX2.Stroke = _LineX1.Stroke = new SolidColorBrush(_LineColor);
            _LineX1.StrokeThickness = _LineX2.StrokeThickness = Convert.ToDouble(this._LineWith);

            //两竖线
            _LineY1.Y1 = _LineY2.Y1 = 0;
            _LineY1.Y2 = _LineY2.Y2 = this.Height;

            _LineY1.X1 = _LineY1.X2 = this.Width * 0.45;
            _LineY2.X1 = _LineY2.X2 = this.Width * 0.55;

            _LineY1.Stroke = _LineY2.Stroke = new SolidColorBrush(_CapacitiveColor);
            _LineY1.StrokeThickness = _LineY2.StrokeThickness = Convert.ToDouble(this._CapacitiveWidth);

        }
    }
}
