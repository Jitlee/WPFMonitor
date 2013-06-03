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

namespace MonitorSystem.Dqfh
{
    /// <summary>
    /// 电气符号
    /// </summary>
    public class Dqfh02 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Path p = new Path();
        GeometryGroup gg = new GeometryGroup();

        Rectangle _RectCenter = new Rectangle();
        public Dqfh02()
        {
            this.Content = _canvas;
            _canvas.Children.Add(p);
            _canvas.Children.Add(_RectCenter);

            p.Data = gg;

            this.Width = 100;
            this.Height = 31;

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Dqfh02_SizeChanged);
        }

        private void Dqfh02_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.31; 


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

            BackColor = Common1.StringToColor(ScreenElement.BackColor);
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
        }



        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
           "BackColor", "ForeColor", "Transparent","Translate"
        ,"DeviceName","Voltagelevel","CapacitiveColor","CapacitiveWidth","LineColor","LineWidth"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dqfh02), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh02), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh02), new PropertyMetadata(0));
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
            gg.Children.Clear();
            double per = this.Width / 400d;
            double xP = 0;

            RectangleGeometry rg1 = new RectangleGeometry();
            rg1.Rect = new Rect() { Width = 75d * per, Height = 24d * per, X = 0, Y = ((124d - 24d) / 2d) * per };
            gg.Children.Add(rg1);
            xP += 75d * per;
            
            RectangleGeometry rg2 = new RectangleGeometry();
            rg2.Rect = new Rect() { Width = 25d * per, Height = 39d * per, X = xP, Y = ((124d - 39d) / 2d) * per };
            gg.Children.Add(rg2);
            xP += 25d * per;

            double CenterWidth=180d * per;
            double CenterHeight=124*per;

            RectangleGeometry rg3 = new RectangleGeometry();
            rg3.Rect = new Rect() { Width = CenterWidth, Height = 124d * per, X = xP, Y = 0 };
            gg.Children.Add(rg3);
            

            //中间设置
            _RectCenter.Width = CenterWidth * 0.8;
            _RectCenter.Height = CenterHeight * 0.8;
            _RectCenter.SetValue(Canvas.LeftProperty,xP+ CenterWidth *0.1);
            _RectCenter.SetValue(Canvas.TopProperty, CenterHeight * 0.1);
            _RectCenter.Stroke = new SolidColorBrush(Colors.Black);
            _RectCenter.StrokeThickness = 0.5;


            xP += 180d * per;

            RectangleGeometry rg4 = new RectangleGeometry();
            rg4.Rect = new Rect() { Width = 46d * per, Height = 23d * per, X = xP, Y = 23d * per };
            gg.Children.Add(rg4);

            RectangleGeometry rg5 = new RectangleGeometry();
            rg5.Rect = new Rect() { Width = 46d * per, Height = 23d * per, X = xP, Y = 70d * per };
            gg.Children.Add(rg5);


            xP += 46d * per;
            RectangleGeometry rg6 = new RectangleGeometry();
            rg6.Rect = new Rect() { Width = 74d * per, Height = 15d * per, X = xP, Y = (23d + 8d) * per };
            gg.Children.Add(rg6);

            RectangleGeometry rg7 = new RectangleGeometry();
            rg7.Rect = new Rect() { Width = 74d * per, Height = 15d * per, X = xP, Y = (70d + 8d) * per };
            gg.Children.Add(rg7);

            p.Stroke = new SolidColorBrush(Colors.Black);
            p.StrokeThickness = 0;
            p.Fill = new SolidColorBrush(Common1.StringToColor("#FF4B3E28"));

        }
    }
}
