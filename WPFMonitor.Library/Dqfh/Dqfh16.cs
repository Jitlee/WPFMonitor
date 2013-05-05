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
    public class Dqfh16 : MonitorControl
    {
        private Canvas _canvas = new Canvas();

        Polyline pl = new Polyline();
        Polyline plCen = new Polyline();

        Line _Line1 = new Line();
        Line _Line2 = new Line();
        Line _Line3 = new Line();
        Line _Line4 = new Line();

        Rectangle _rect = new Rectangle();
        public Dqfh16()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 56;

            _Line1.Stroke = _Line2.Stroke = _Line3.Stroke =
            _Line4.Stroke = _rect.Stroke = plCen.Stroke = pl.Stroke = new SolidColorBrush(DQFHCommon.DQFHLineColor);
            _Line1.StrokeThickness = _Line2.StrokeThickness = _Line3.StrokeThickness 
            = _Line4.StrokeThickness= _rect.StrokeThickness = pl.StrokeThickness = DQFHCommon.DQFHLineWidth;
            plCen.StrokeThickness = DQFHCommon.DQFHLineWidth * 2;
            _rect.Fill = new SolidColorBrush(Colors.Black);
            _canvas.Children.Add(pl);
            _canvas.Children.Add(plCen);
            _canvas.Children.Add(_Line1);
            _canvas.Children.Add(_Line2);
            _canvas.Children.Add(_Line3);
            _canvas.Children.Add(_Line4);
            _canvas.Children.Add(_rect);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
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
           typeof(Color), typeof(Dqfh16), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh16), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh16), new PropertyMetadata(0));
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

            #region  下面弯线
            double _aLinePer = 0.17;//两直线，分别占总长度比例
            //线
            double _LineY = this.Height *(1-0.1153) ;//横线的Y轴位置
            double _LineLength = this.Width * _aLinePer;

            double dbPointNum = 3;//单边点数量
            double dWidth = this.Width * 0.31;//弯曲长度            
            double minWidth = dWidth / (dbPointNum * 4);//第一个点位置之间的宽度(点数*4分之一宽度)
            double _shPointHeight = this.Height * 0.1153; //横线到上下点的高度

            PointCollection pc = new PointCollection();
            //前直线1
            pc.Add(new Point(0, _LineY));
            pc.Add(new Point(_LineLength, _LineY));
            //上下三个点
            for (int i = 0; i < dbPointNum * 2; i++)
            {
                if (i == 0)
                {
                    pc.Add(new Point(_LineLength + minWidth, _LineY - _shPointHeight));
                }
                int mod = i % 2;
                if (mod == 1)
                {
                    pc.Add(new Point(_LineLength + minWidth * (i * 2 + 1), _LineY + _shPointHeight));
                }
                else
                {
                    pc.Add(new Point(_LineLength + minWidth * (i * 2 + 1), _LineY - _shPointHeight));
                }
            }
            //直线2
            pc.Add(new Point(this.Width * 0.48, _LineY));
            pc.Add(new Point(this.Width* 0.58, _LineY));
            pl.Points = pc;
            #endregion
            //pyCen
            PointCollection pccen = new PointCollection();
            pccen.Add(new Point(this.Width * 0.48, this.Height * 0.2423));
            pccen.Add(new Point(this.Width * 0.55, this.Height * 0.2423));
            pccen.Add(new Point(this.Width * 0.55, this.Height * 0.627));
            pccen.Add(new Point(this.Width * 0.48, this.Height * 0.627));
            plCen.Points = pccen;

            double LineY = this.Height * 0.538;

            _Line1.X1 = this.Width * 0.25;
            _Line1.X2 = this.Width * 0.55;
            _Line1.Y1 = _Line1.Y2 = LineY;

            _Line2.X1 = this.Width * 0.644;
            _Line2.X2 = this.Width;
            _Line2.Y1 = _Line2.Y2 = LineY;

            _Line3.X1 = _Line3.X2 = this.Width * 0.644;
            _Line3.Y1=this.Height * 0.13;
            _Line3.Y2 = this.Height * 0.7461;

            _Line4.X1 = this.Width * 0.396;
            _Line4.Y1 = this.Height * 0.7576;
            _Line4.X2 = this.Width * 0.714;
            _Line4.Y2 = this.Height * 0.119;

                _rect.Width = _rect.Height = _rect.RadiusX = _rect.RadiusY = this.Width * 0.08;
            _rect.SetValue(Canvas.LeftProperty,this.Width * 0.704);

        }
    }
}
