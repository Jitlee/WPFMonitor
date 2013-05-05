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

namespace WPFMonitor.Library.Dldz
{
    /// <summary>
    /// 电力电子
    /// </summary>
    public class Dldz09 : MonitorControl
    {
        private Canvas _canvas = new Canvas();

        Line _line1 = new Line();
        Line _line2 = new Line();

        Ellipse _Elli = new Ellipse();

        Path py = new Path();
        GeometryGroup gg = new GeometryGroup();
        PathGeometry _pathG = new PathGeometry();
        //棱形
        PathFigureCollection pfc = new PathFigureCollection();
        PathFigure pf = new PathFigure();
        PathSegmentCollection psc = new PathSegmentCollection();

        public Dldz09()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 53;

            _Elli.StrokeThickness = DLDZCommon.DLDZLineWidth;
            _Elli.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            _Elli.Fill = new SolidColorBrush(DLDZCommon.DLDZFilleColor2);
            _canvas.Children.Add(_Elli);

            _pathG.Figures = pfc;
            pfc.Add(pf);
            pf.Segments = psc;
            gg.FillRule = FillRule.Nonzero;
            gg.Children.Add(_pathG);
            py.Data = gg;
            py.StrokeThickness = DLDZCommon.DLDZLineWidth;
            py.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            _canvas.Children.Add(py);
            _canvas.Children.Add(_line1);
            _canvas.Children.Add(_line2);


            _line2.Stroke = _line1.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            _line2.StrokeThickness = _line1.StrokeThickness = DLDZCommon.DLDZLineWidth * 2;
            

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.53;
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
            }
            //Paint();
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
           "BackColor", "ForeColor", "Transparent","Translate"};
        // ,"DeviceName","Voltagelevel","CapacitiveColor","CapacitiveWidth","LineColor","LineWidth"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dldz09), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz09), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz09), new PropertyMetadata(0));
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


        private void Paint()
        {



            double _LineTop = this.Height / 2;

            //曲线Y点坐标
            double pyTopY=this.Height * (0.5 - 0.257);

            _line1.X1 = 0;
            _line1.X2 = this.Width * 0.17;
            _line1.Y2 = _line1.Y1 = _LineTop;
            

            //_Line2
            _line2.X1 = this.Width * (1 - 0.18);
            _line2.X2 = this.Width;
            _line2.Y1 = _line2.Y2 = _LineTop;

            //—_Elli
            _Elli.Height = this.Height;
            _Elli.Width = this.Width * 0.8;
            _Elli.SetValue(Canvas.LeftProperty, this.Width * 0.1);

            pf.StartPoint = new Point(this.Width * 0.17, _LineTop);
            psc.Clear();
            ArcSegment arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.31, pyTopY);
            arcs.Size = new Size { Width= this.Width * 0.14,
             Height= _LineTop - pyTopY  };
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.41, pyTopY);
            psc.Add(arcs);

            //0.554
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.554, this.Height*0.377 );
            arcs.Size = new Size()
            {
                Height = this.Height * 0.377 - pyTopY,
                Width = this.Width * 0.144
            };
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);
            //中间最右边线
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.554, this.Height * 0.377);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.554, this.Height * 0.528);
            psc.Add(arcs);
            //中间
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.512, this.Height * 0.589);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.472, this.Height * 0.55);
            psc.Add(arcs);
            //中间最左边线0.454   227  
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.452, this.Height * 0.494);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.452, this.Height * 0.407);
            psc.Add(arcs);

            //右边最上面的线
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.55, pyTopY);
            arcs.Size = new Size(this.Width * 0.1, this.Height * 0.407 - pyTopY);
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.64, pyTopY);
            psc.Add(arcs);

            //最后弯
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.82, _LineTop);
            arcs.Size = new Size() { Height=  _LineTop - pyTopY,
            Width= this.Width * 0.2};
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

        }

    }
}

