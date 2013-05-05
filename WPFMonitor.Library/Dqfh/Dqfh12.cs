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
    public class Dqfh12 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Line _Line1 = new Line();
        Line _Line2 = new Line();
        Line _Line3 = new Line();
        Line _Line4 = new Line();
        Line _Line5 = new Line();

        Rectangle _rect1 = new Rectangle();
        Rectangle _rect2 = new Rectangle();
        Rectangle _rect3 = new Rectangle();
        Rectangle _rect4 = new Rectangle();
        Rectangle _rect5 = new Rectangle();
        Rectangle _rect6 = new Rectangle();



        public Dqfh12()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 39;

            _rect1.Stroke = _rect2.Stroke = _rect3.Stroke =
            _rect4.Stroke = _rect5.Stroke = _rect6.Stroke =
            _Line1.Stroke = _Line2.Stroke = _Line3.Stroke =
            _Line4.Stroke = _Line5.Stroke = new SolidColorBrush(DQFHCommon.DQFHLineColor);

            _rect1.StrokeThickness = _rect2.StrokeThickness = _rect3.StrokeThickness =
           _rect4.StrokeThickness = _rect5.StrokeThickness = _rect6.StrokeThickness =
            _Line1.StrokeThickness = _Line2.StrokeThickness =                _Line3.StrokeThickness =
            _Line4.StrokeThickness = _Line5.StrokeThickness = DQFHCommon.DQFHLineWidth;



            _canvas.Children.Add(_Line1);
            _canvas.Children.Add(_Line2);
            _canvas.Children.Add(_Line3);
            _canvas.Children.Add(_Line4);
            _canvas.Children.Add(_Line5);

            _canvas.Children.Add(_rect1);
            _canvas.Children.Add(_rect2);
            _canvas.Children.Add(_rect3);
            _canvas.Children.Add(_rect4);
            _canvas.Children.Add(_rect5);
            _canvas.Children.Add(_rect6);
            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.39;
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
           typeof(Color), typeof(Dqfh12), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh12), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh12), new PropertyMetadata(0));
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
            double RectWidth = this.Width * 0.11;
            double RectJG = this.Width * 0.28;//圆间隔位置

            _rect1.Width = _rect1.Height = _rect1.RadiusX = _rect1.RadiusY = RectWidth;
            _rect2.Width = _rect2.Height = _rect2.RadiusX = _rect2.RadiusY = RectWidth;
            _rect3.Width = _rect3.Height = _rect3.RadiusX = _rect3.RadiusY = RectWidth;
            _rect4.Width = _rect4.Height = _rect4.RadiusX = _rect4.RadiusY = RectWidth;
            _rect5.Width = _rect5.Height = _rect5.RadiusX = _rect5.RadiusY = RectWidth;
            _rect6.Width = _rect6.Height = _rect6.RadiusX = _rect6.RadiusY = RectWidth;

            
            double TopStartRectPosition = this.Width * 0.12;//线面圆开始位置

            //上面三个圆
            _rect1.SetValue(Canvas.LeftProperty, TopStartRectPosition);
            _rect2.SetValue(Canvas.LeftProperty, TopStartRectPosition + RectJG);
            _rect3.SetValue(Canvas.LeftProperty, TopStartRectPosition + RectJG * 2);
            
            //下面三个圆
            double BotStratPosition = 0;
            double BomY = this.Height - RectWidth;
            _rect4.SetValue(Canvas.LeftProperty, BotStratPosition);
            _rect4.SetValue(Canvas.TopProperty, BomY);

            _rect5.SetValue(Canvas.LeftProperty, BotStratPosition + RectJG);
            _rect5.SetValue(Canvas.TopProperty, BomY);

            _rect6.SetValue(Canvas.LeftProperty, BotStratPosition + RectJG * 2);
            _rect6.SetValue(Canvas.TopProperty, BomY);


            double LineStarX1 = this.Width * 0.126;//线条开的X位置
            double LineStarX2=  this.Width * 0.272;//线条开的X2位置
            double LineY1 = this.Height * 0.812;
            double LineY2 = this.Height * 0.138;

            _Line1.X1 = LineStarX1;
            _Line1.X2 = LineStarX2;
            _Line1.Y1 = LineY1;
            _Line1.Y2 = LineY2;


            _Line2.X1 = LineStarX1 + RectJG;
            _Line2.X2 = LineStarX2 + RectJG;
            _Line2.Y1 = LineY1;
            _Line2.Y2 = LineY2;


            _Line3.X1 = LineStarX1 + RectJG * 2;
            _Line3.X2 = LineStarX2 + RectJG * 2;
            _Line3.Y1 = LineY1;
            _Line3.Y2 = LineY2;

            _Line4.X1 = this.Width * 0.94;
            _Line4.Y1 = this.Height / 2;
            _Line4.X2 = this.Width;
            _Line4.Y2 = LineY2;

            _Line5.X1 = this.Width * 0.192;
            _Line5.X2 = this.Width * 0.94;
            _Line5.Y1 = _Line5.Y2 = this.Height / 2;
        }
    }
}
