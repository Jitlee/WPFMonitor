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

namespace WPFMonitor.Library.Dqfh
{
    /// <summary>
    /// 电气符号
    /// </summary>
    public class Dqfh17 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Line _Line1 = new Line();
        Line _Line2 = new Line();

        //弧线
        Path py = new Path();
        PathGeometry pg = new PathGeometry();
        PathFigureCollection pfc = new PathFigureCollection();
        PathFigure pf = new PathFigure();
        PathSegmentCollection psc = new PathSegmentCollection();
        public Dqfh17()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 13;

            _Line1.Stroke = _Line2.Stroke = py.Stroke = new SolidColorBrush(DQFHCommon.DQFHLineColor);
            _Line1.StrokeThickness = _Line2.StrokeThickness = py.StrokeThickness = DQFHCommon.DQFHLineWidth;
            
            
            _canvas.Children.Add(_Line1);
            _canvas.Children.Add(_Line2);

            //弧
            py.Data = pg;
            pg.Figures = pfc;
            pfc.Add(pf);
            pf.Segments = psc;
            _canvas.Children.Add(py);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.13;
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
            }
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
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dqfh17), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh17), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh17), new PropertyMetadata(0));
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
            
            _Line1.X1 = 0;
            _Line1.X2 = this.Width * 0.32;
            _Line1.Y1 = _Line1.Y2 = this.Height;

            _Line2.X1 = this.Width * 0.68;
            _Line2.X2 = this.Width;
            _Line2.Y1 = _Line2.Y2 = this.Height;

            //弧
            psc.Clear();
            pf.StartPoint = new Point(this.Width * 0.36, this.Height * 0.74);

            ArcSegment arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.37, this.Height * 0.6); 
            psc.Add(arcs);


            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.46, 0);
            arcs.Size = new Size()
            {
                Width = this.Width * 0.1,
                Height = this.Height * 0.64
            };
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.54, 0);
            arcs.SweepDirection = SweepDirection.Clockwise;
            psc.Add(arcs);

            

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.634, this.Height * 0.6);
            arcs.SweepDirection = SweepDirection.Clockwise;
            arcs.Size = new Size()
            {
                Width = this.Width * 0.1,
                Height = this.Height * 0.64
            };
            psc.Add(arcs);

            arcs = new ArcSegment();
            arcs.Point = new Point(this.Width * 0.644, this.Height * 0.74);
            psc.Add(arcs);

        }
    }
}
