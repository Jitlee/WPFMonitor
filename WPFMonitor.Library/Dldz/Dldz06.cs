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
    public class Dldz06 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        private Line _LineX1 = new Line();
        private Line _LineX2 = new Line();
        private Ellipse _Rect1 = new Ellipse();
        private Ellipse _Rect2 = new Ellipse();

        //弧线
        Path py = new Path();
        PathGeometry pg = new PathGeometry();
        PathFigureCollection pfc = new PathFigureCollection();
        PathFigure pf = new PathFigure();
        PathSegmentCollection psc = new PathSegmentCollection();

        public Dldz06()
        {
            
            this.Content = _canvas;
            
            //线
            _canvas.Children.Add(_LineX1);
            _canvas.Children.Add(_LineX2);
            //随圆
            _Rect1.Fill = _Rect2.Fill = new SolidColorBrush(DLDZCommon.DLDZFilleColor);
            _canvas.Children.Add(_Rect1);
            _canvas.Children.Add(_Rect2);

            //弧
            py.Data = pg;
            pg.Figures = pfc;
            pfc.Add(pf);
            pf.Segments = psc;           
            _canvas.Children.Add(py);

            _Rect1.StrokeThickness = _Rect2.StrokeThickness=
            py.StrokeThickness = _LineX1.StrokeThickness = _LineX2.StrokeThickness 
                = _Rect1.StrokeThickness = _Rect2.StrokeThickness = DLDZCommon.DLDZLineWidth;

            _Rect1.Stroke = _Rect2.Stroke =
            py.Stroke = _LineX1.Stroke = _LineX2.Stroke
                = _Rect1.Stroke = _Rect2.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);

            

            this.Width = 100;
            this.Height = 30;
            Paint();

            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.3;
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
           typeof(Color), typeof(Dldz06), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz06), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz06), new PropertyMetadata(0));
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
            double _LineLength = (this.Width - this.Height) / 2;

            //两边线
            _LineX1.X1 = 0;
            _LineX1.X2 = this.Width* 0.3;
            _LineX1.Y1 = _LineX1.Y2 = this.Height / 2;

            _LineX2.X1 = this.Width * 0.7;
            _LineX2.X2 = this.Width;
            _LineX2.Y1 = _LineX2.Y2 = this.Height / 2;

            //画两个随圆
            double _rectWith = this.Width * 0.05;
            double _rectHeight = this.Height * 0.47;
            double  _RectTop=this.Height / 2 - _rectHeight / 2;

            _Rect1.SetValue(Canvas.LeftProperty, this.Width * 0.3);
            _Rect1.SetValue(Canvas.TopProperty, _RectTop);

            _Rect2.SetValue(Canvas.LeftProperty, this.Width * 0.65);
            _Rect2.SetValue(Canvas.TopProperty, _RectTop);

            _Rect2.Width = _Rect1.Width = _rectWith;
            _Rect2.Height = _Rect1.Height = _rectHeight;

            //弧
            psc.Clear();
            double pfStart=this.Width * 0.33;
            pf.StartPoint = new Point(pfStart, _RectTop);

            ArcSegment arcs = new ArcSegment();
            arcs.Point = new Point(pfStart + this.Width * 0.08, 0);
            arcs.Size = new Size() {  Width= this.Width* 0.08, Height= _RectTop };
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

            //中间线的点
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.485, _RectTop);
            arcs.Size = new Size() { Width = this.Width * 0.08, Height = _RectTop };
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);
            //最下面第一个点
            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.55, this.Height);
            arcs.Size = new Size(this.Width * 0.07, this.Height - _RectTop);
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.58, this.Height);
            //arcs.Size = BouutomSize;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.64, this.Height * (1- 0.17));
            //arcs.Size = BouutomSize;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.656, this.Height *(1- 0.353));
            //arcs.Size = BouutomSize;
            psc.Add(arcs);

        }

    }
}

