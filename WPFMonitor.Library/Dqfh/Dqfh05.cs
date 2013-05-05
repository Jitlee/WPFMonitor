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
    public class Dqfh05 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Line _Line1 = new Line();
        Line _Line2 = new Line();
        Line _Line3 = new Line();
        Line _Line4 = new Line();
        Line _Line5 = new Line();
        Line _Line6 = new Line();
        Line _Line7 = new Line();
        Line _Line8 = new Line();

        Rectangle _rect1 = new Rectangle();
        Rectangle _rect2 = new Rectangle();
        Rectangle _rect3 = new Rectangle();
        Rectangle _rect4 = new Rectangle();

        TextBlock tb1 = new TextBlock();
        TextBlock tb2 = new TextBlock();
        TextBlock tb3 = new TextBlock();
        public Dqfh05()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 100;
            //线
            _rect1.Stroke=_rect2.Stroke= _rect3.Stroke= _rect4.Stroke=
            _Line1.Stroke = _Line2.Stroke = _Line3.Stroke = _Line4.Stroke = _Line5.Stroke =
                 _Line6.Stroke = _Line7.Stroke = _Line8.Stroke = new SolidColorBrush(DQFHCommon.DQFHLineColor);

            _rect1.StrokeThickness = _rect2.StrokeThickness = _rect3.StrokeThickness = _rect4.StrokeThickness =
            _Line1.StrokeThickness = _Line2.StrokeThickness = _Line3.StrokeThickness =
                _Line4.StrokeThickness = _Line5.StrokeThickness = _Line6.StrokeThickness=
                 _Line7.StrokeThickness = _Line8.StrokeThickness = DQFHCommon.DQFHLineWidth;


            _canvas.Children.Add(_Line1);
            _canvas.Children.Add(_Line2);
            _canvas.Children.Add(_Line3);
            _canvas.Children.Add(_Line4);
            _canvas.Children.Add(_Line5);
            _canvas.Children.Add(_Line6);
            _canvas.Children.Add(_Line7);
            _canvas.Children.Add(_Line8);

            _canvas.Children.Add(_rect1);
            _canvas.Children.Add(_rect2);
            _canvas.Children.Add(_rect3);
            _canvas.Children.Add(_rect4);

            tb1.Text = "H";
            _canvas.Children.Add(tb1);
            tb2.Text = "O";
            _canvas.Children.Add(tb2);
            tb3.Text = "A";
            _canvas.Children.Add(tb3);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Height;
            this.Height = e.NewSize.Height;
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
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dqfh05), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh05), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh05), new PropertyMetadata(0));
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
            double RectWidth = this.Width * 0.1675;
            double _LineLeftWidth = this.Width * 0.21;//84
            double _RightRectX=this.Width - RectWidth- _LineLeftWidth;
            _Line1.X1 = 0;
            _Line1.X2 = _LineLeftWidth;
            _Line1.Y2 = _Line1.Y1 = this.Height - RectWidth / 2;
            _rect1.Width = _rect1.Height = _rect1.RadiusX = _rect1.RadiusY = RectWidth;
            _rect1.SetValue(Canvas.LeftProperty, _LineLeftWidth);
            _rect1.SetValue(Canvas.TopProperty, this.Height - RectWidth);

            _Line2.X1 = this.Width * 0.79;
            _Line2.X2 = this.Width;
            _Line2.Y2 = _Line2.Y1 = this.Height - RectWidth / 2;
            _rect2.Width = _rect2.Height = _rect2.RadiusX = _rect2.RadiusY = RectWidth;
            _rect2.SetValue(Canvas.LeftProperty, _RightRectX);
            _rect2.SetValue(Canvas.TopProperty, this.Height - RectWidth);

            double TopLineY = this.Height * (1 - 0.5786);
            double TopRectY = this.Height * (1 - 0.6675);

            _Line3.X1 = 0;
            _Line3.X2 = _LineLeftWidth;
            _Line3.Y1 = _Line3.Y2 = TopLineY;
            _rect3.Width = _rect3.Height = _rect3.RadiusX = _rect3.RadiusY = RectWidth;
            _rect3.SetValue(Canvas.LeftProperty, _LineLeftWidth);
            _rect3.SetValue(Canvas.TopProperty, TopRectY);

            _Line4.X1 = this.Width * 0.79;
            _Line4.X2 = this.Width;
            _Line4.Y1 = _Line4.Y2 = TopLineY;
            _rect4.Width = _rect4.Height = _rect4.RadiusX = _rect4.RadiusY = RectWidth;
            _rect4.SetValue(Canvas.LeftProperty, _RightRectX);
            _rect4.SetValue(Canvas.TopProperty, TopRectY);

            //中间线
            double centerLineX1 = this.Width* 0.3255;
            double centerLineX2 = this.Width * 0.665;
            double _bottomLineY=this.Height * 0.83;
            _Line5.X1 = centerLineX1;
            _Line5.X2 = centerLineX2;
            _Line5.Y1 = _Line5.Y2 = _bottomLineY;

            _Line6.X1 = centerLineX1;
            _Line6.X2 = centerLineX2;
            _Line6.Y1 = _Line6.Y2 = this.Height * (1- 0.6575);

            //最中间的线
             double Lin7Y1 = this.Height * (0.83- 0.645);
            _Line7.X1 = _Line7.X2 = this.Width / 2;
            _Line7.Y1 = Lin7Y1;
            _Line7.Y2 = _bottomLineY;

            //弯线
            _Line8.X1 = this.Width / 2 - this.Width * 0.005;
            _Line8.Y1 = Lin7Y1+ this.Height * 0.02;
            _Line8.X2 = this.Width / 2 + this.Width * 0.07;
            _Line8.Y2 = Lin7Y1 - this.Height * 0.035;

            //HOA
            double tb13Top= this.Height * 0.17;
            tb1.SetValue(Canvas.TopProperty, tb13Top);
            tb1.SetValue(Canvas.LeftProperty, this.Width * 0.315);

            tb2.SetValue(Canvas.LeftProperty, this.Height * .47);
            tb3.SetValue(Canvas.TopProperty, this.Height * 0.1);

            tb3.SetValue(Canvas.TopProperty, tb13Top);
            tb3.SetValue(Canvas.LeftProperty, this.Width * 0.605);
            tb1.FontSize = tb2.FontSize =
            tb3.FontSize = this.Width * 0.15;
            
        }
    }
}
