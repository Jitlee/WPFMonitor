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
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.Dldz
{
    /// <summary>
    /// 电力电子
    /// </summary>
    public class Dldz08 : MonitorControl
    {
        private Canvas _canvas = new Canvas();

        Line _line1 = new Line();
        Line _line2 = new Line();
        Line _line3 = new Line();//最中间的线
        Path py = new Path();
        GeometryGroup gg = new GeometryGroup();
        //四边形和棱形
        RectangleGeometry _rectG = new RectangleGeometry();
        PathGeometry _pathG= new PathGeometry();
        //棱形
        PathFigureCollection pfc = new PathFigureCollection();
        PathFigure pf = new PathFigure();
        PathSegmentCollection psc = new PathSegmentCollection();

        public Dldz08()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 53;

            _pathG.Figures = pfc;
            pfc.Add(pf);
            pf.Segments = psc;

            gg.FillRule = FillRule.Nonzero;
            gg.Children.Add(_rectG);
            gg.Children.Add(_pathG);
            py.Data = gg;

            py.Fill = new SolidColorBrush(DLDZCommon.DLDZFilleColor2);
            py.StrokeThickness = DLDZCommon.DLDZLineWidth;
            py.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            _canvas.Children.Add(py);

            _canvas.Children.Add(_line1);
            _canvas.Children.Add(_line2);
            _canvas.Children.Add(_line3);

            _line3.Stroke = _line2.Stroke = _line1.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            _line2.StrokeThickness = _line1.StrokeThickness = DLDZCommon.DLDZLineWidth;
            _line3.StrokeThickness = DLDZCommon.DLDZLineWidth * 2;

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

            BackColor = Common1.StringToColor(ScreenElement.BackColor);
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
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
           typeof(Color), typeof(Dldz08), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz08), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz08), new PropertyMetadata(0));
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
            
            //四边形最上面位置
            double _rectTop=this.Height * 0.219;
            //四边形宽
            double _rectWidth = this.Width * 0.63;
            //开始线长度
            double _LineWidth = this.Width * 0.27;
            //路径四边形高度
            double _RectHeight = this.Height * 0.566;

            //设置线
            double _LineStrtY = this.Height * 0.36;

            _line1.X1 = 0;
            _line1.Y2 = _line1.Y1 = _LineStrtY;
            _line1.X2 = _LineWidth;

            //_Line2
            _line2.X1 = 0;
            _line2.X2 = _LineWidth;
            _line2.Y1 = _line2.Y2 = _LineStrtY + _RectHeight / 2;


            //_line3
            _line3.X1 = _LineWidth + this.Width * 0.01;
            _line3.X2 = this.Width;
            _line3.Y1 = _line3.Y2 = this.Height/2;


            
            _rectG.Rect = new Rect
            {
                Height= _RectHeight,
                Width= _rectWidth,
                X = _LineWidth,
                Y= _rectTop
            };

            double PathstartX=_LineWidth+ _rectWidth/2;
            Point pStartP = new Point(PathstartX, _rectTop);
            pf.StartPoint = pStartP;

            psc.Clear();
            ArcSegment arcs = new ArcSegment();
            arcs.Point = new Point(this.Width,0);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width, this.Height);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(PathstartX, _rectTop + _RectHeight);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = pStartP;
            psc.Add(arcs);
        }

    }
}

