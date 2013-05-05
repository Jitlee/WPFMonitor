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
    public class Dldz05 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        private Line _LineX1 = new Line();
        private Line _LineX2 = new Line();
        private Rectangle _Rect = new Rectangle();

        private Polyline pl = new Polyline();

        public Dldz05()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_Rect);
            _canvas.Children.Add(_LineX1);
            _canvas.Children.Add(_LineX2);

            _canvas.Children.Add(pl);
            pl.StrokeThickness = _LineX1.StrokeThickness =
                _LineX2.StrokeThickness = _Rect.StrokeThickness = DLDZCommon.DLDZLineWidth;

            pl.Stroke = _LineX1.Stroke = _LineX2.Stroke = _Rect.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);


            this.Width = 100;
            this.Height = 24;
            Paint();

            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.24;
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
           typeof(Color), typeof(Dldz05), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz05), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz05), new PropertyMetadata(0));
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
            _LineX1.X2 = _LineLength;

            _LineX2.X1 = this.Width - _LineLength;
            _LineX2.X2 = this.Width;
            double Y = this.Height / 2;
            _LineX1.Y1 = _LineX1.Y2 = _LineX2.Y1 = _LineX2.Y2 = Y;

            //圆
            _Rect.Width = _Rect.Height = _Rect.RadiusX = _Rect.RadiusY = this.Height;
            _Rect.SetValue(Canvas.LeftProperty, _LineLength);


            double centerY = this.Height / 2;
            double ylHeight = this.Height * 0.59;
            //一半高度
            double ybHeight = ylHeight / 2;

            pl.Points.Clear();
            pl.Points.Add(new Point(this.Width * 0.432, this.Height / 2 + ybHeight));
            pl.Points.Add(new Point(this.Width * 0.432, this.Height / 2 - ybHeight));

            //190 + 47=237/500
            pl.Points.Add(new Point(this.Width * 0.474, this.Height / 2 - ybHeight));
            pl.Points.Add(new Point(this.Width * 0.474, this.Height / 2 + ybHeight));

            //190+71=261
            pl.Points.Add(new Point(this.Width * 0.522, this.Height / 2 + ybHeight));
            pl.Points.Add(new Point(this.Width * 0.522, this.Height / 2 - ybHeight));

            //190+96=286
            pl.Points.Add(new Point(this.Width * 0.572, this.Height / 2 - ybHeight));
            pl.Points.Add(new Point(this.Width * 0.572, this.Height / 2 + ybHeight));

        }

    }
}

