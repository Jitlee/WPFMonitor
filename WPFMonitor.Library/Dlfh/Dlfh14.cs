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
    /// 电力符号 12 电抗器2
    /// </summary>
    public class Dlfh14 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        private Line _Line = new Line();
        Polyline plLeft = new Polyline();
        Polyline plRight = new Polyline();

        //3/4圆
        Path py = new Path();
        PathGeometry pg = new PathGeometry();
        PathFigureCollection pfc = new PathFigureCollection();
        PathFigure pf = new PathFigure();
        PathSegmentCollection psc = new PathSegmentCollection();
        public Dlfh14()
        {
            this.Width = 99;
            this.Height = 56;

            this.Content = _canvas;
            _canvas.Children.Add(_Line);
            _canvas.Children.Add(plLeft);
            _canvas.Children.Add(plRight);
            _canvas.Children.Add(py);

            //3/4圆
            py.Data = pg;
            pg.Figures = pfc;
            pfc.Add(pf);
            pf.Segments = psc;



            Paint();
            PaintNormal();
            this.SizeChanged += new SizeChangedEventHandler(Dlfh14_SizeChanged);
        }



        private void Dlfh14_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.56;
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
                    _LineColor = Common.StringToColor(value);
                }
                else if (name == "LineWidth".ToUpper())
                {
                    _LineWith = Convert.ToDouble(value);
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

            BackColor = Common.StringToColor(ScreenElement.BackColor);
            ForeColor = Common.StringToColor(ScreenElement.ForeColor);
        }



        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
           "BackColor", "ForeColor", "Transparent","Translate"
        ,"DeviceName","Voltagelevel","LineColor","LineWidth"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty VoltagelevelProperty = DependencyProperty.Register("Voltagelevel",
     typeof(int), typeof(Dlfh14), new PropertyMetadata(0));
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
           typeof(Color), typeof(Dlfh14), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dlfh14), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dlfh14), new PropertyMetadata(0));
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
        typeof(int), typeof(Dlfh14), new PropertyMetadata(0));
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
      typeof(int), typeof(Dlfh14), new PropertyMetadata(0));
        Color _LineColor = Common.StringToColor("#FFED1212");
        [DefaultValue(""), Description("电抗器颜色"), Category("我的属性")]
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


        private static readonly DependencyProperty LineWithProperty = DependencyProperty.Register("LineWidth",
      typeof(int), typeof(Dlfh14), new PropertyMetadata(0));
        double _LineWith = 2;
        [DefaultValue(2), Description("电抗器宽度"), Category("我的属性")]
        public double LineWidth
        {
            get { return _LineWith; }
            set
            {
                _LineWith = value;
                SetAttrByName("LineWidth", value);
                PaintNormal();
            }
        }
        #endregion

        /// <summary>
        /// 设置线的宽度、颜色
        /// </summary>
        private void PaintNormal()
        {
            py.StrokeThickness =
            plLeft.StrokeThickness = _Line.StrokeThickness = plRight.StrokeThickness = _LineWith;
            py.Stroke =
            plLeft.Stroke = plRight.Stroke = _Line.Stroke = new SolidColorBrush(_LineColor);
        }

        private void Paint()
        {

            _Line.X1 = _Line.X2 = this.Width / 2; ;
            _Line.Y1 = this.Height * 0.85;
            _Line.Y2 = this.Height;

            double plY = this.Height * 0.41;

            PointCollection pcLeft = new PointCollection();
            pcLeft.Add(new Point(0, plY));
            pcLeft.Add(new Point(this.Width * 0.4, plY));
            pcLeft.Add(new Point(this.Width * 0.4, 0));
            plLeft.Points = pcLeft;

            PointCollection pcRight = new PointCollection();
            pcRight.Add(new Point(this.Width, plY));
            pcRight.Add(new Point(this.Width * 0.61, plY));
            pcRight.Add(new Point(this.Width * 0.61,0));
            plRight.Points = pcRight;


            Size topSize = new Size(this.Width * (0.75 - 0.5), this.Height * (0.59 - 0.15));
            Size BouutomSize = new Size(this.Width * (0.75 - 0.61), this.Height * (1 - 0.59));
          
            psc.Clear();
            pf.StartPoint = new Point(this.Width * 0.4, 0);

            ArcSegment arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.24, plY);
            arcs.Size = BouutomSize;
            psc.Add(arcs);

            //最上面的那下点
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.5, this.Height * 0.85);
            arcs.Size = topSize;            
            psc.Add(arcs);
            //最右边那个点
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.75, plY);
            arcs.Size = topSize;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.61, 0);
            arcs.Size = BouutomSize;
            psc.Add(arcs);

        }
    }
}
