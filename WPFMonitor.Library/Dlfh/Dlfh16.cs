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
    /// <summary>
    /// 电力符号 16 等值系统6
    /// </summary>
    public class Dlfh16 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        //圆
        private Rectangle _Rect = new Rectangle();
        //小圆
        Path py = new Path();
        PathGeometry pg = new PathGeometry();
        PathFigureCollection pfc = new PathFigureCollection();
        PathFigure pf = new PathFigure();
        PathSegmentCollection psc = new PathSegmentCollection();
        public Dlfh16()
        {
            this.Width = 99;
            this.Height = 56;

            this.Content = _canvas;

            _Rect.StrokeDashArray = new DoubleCollection() { 3.0, 2.0 }; 
            _canvas.Children.Add(_Rect);

            _canvas.Children.Add(py);

            //3/4圆
            py.Data = pg;
            pg.Figures = pfc;
            pfc.Add(pf);
            pf.Segments = psc;

            Paint();
            PaintNormal();
            this.SizeChanged += new SizeChangedEventHandler(Dlfh16_SizeChanged);
        }



        private void Dlfh16_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width;
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
                else if (name == "Voltagelevel".ToUpper())
                {
                    _Voltagelevel = int.Parse(value);
                }
                else if (name == "LineColor".ToUpper())
                {
                    _LineColor = Common1.StringToColor(value);
                }               
            }
            Paint();
            PaintNormal();
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
        ,"DeviceName","Voltagelevel","LineColor"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty VoltagelevelProperty = DependencyProperty.Register("Voltagelevel",
     typeof(int), typeof(Dlfh16), new PropertyMetadata(0));
        int _Voltagelevel = 10;
        [DefaultValue("10"), Description("电压等级"), Category("我的属性")]
        public int Voltagelevel
        {
            get { return _Voltagelevel; }
            set
            {
                _Voltagelevel = value;
                SetAttrByName("Voltagelevel", value);
            }
        }



        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dlfh16), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dlfh16), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dlfh16), new PropertyMetadata(0));
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
        typeof(int), typeof(Dlfh16), new PropertyMetadata(0));
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
      typeof(int), typeof(Dlfh16), new PropertyMetadata(0));
        Color _LineColor = Common1.StringToColor("#FFED1212");
        [DefaultValue(""), Description("发电机颜色"), Category("我的属性")]
        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                SetAttrByName("LineColor", value);
                PaintNormal();
            }
        }
        #endregion

        /// <summary>
        /// 设置线的宽度、颜色
        /// </summary>
        private void PaintNormal()
        {
            py.StrokeThickness = 1.5;
            _Rect.StrokeThickness = 1;
            py.Stroke = _Rect.Stroke = new SolidColorBrush(_LineColor);
            
        }

        private void Paint()
        {

            _Rect.Width = _Rect.Height = _Rect.RadiusX = _Rect.RadiusY = this.Width;

            double miniZJ = (this.Width - this.Width * (2 * 0.08)) / 2;//小圆直径
            double miniBJ = miniZJ / 2;//小圆半径
            double CenterY = this.Height / 2;

            Size miniSzie = new Size(miniBJ, miniBJ);

            double xStart = this.Width * 0.08;


            psc.Clear();
            pf.StartPoint = new Point(xStart, CenterY);

            //上面点
            ArcSegment arcs = new ArcSegment();
            arcs.Point = new Point(xStart + miniBJ, CenterY - miniBJ);
            arcs.Size = miniSzie;
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

            //中间点
            arcs = new ArcSegment();
            arcs.Point = new Point(xStart + miniBJ * 2, CenterY);
            arcs.Size = miniSzie;
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);
            //最右边那个点
            arcs = new ArcSegment();
            arcs.Point = new Point(xStart + miniBJ * 3, CenterY + miniBJ);
            arcs.Size = miniSzie;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(xStart + miniBJ * 4, CenterY);
            arcs.Size = miniSzie;
            psc.Add(arcs);

        }
    }
}
