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
    public class Dqfh22 : MonitorControl
    {
        private Canvas _canvas = new Canvas();


        Rectangle _rect1 = new Rectangle();
        Rectangle _rect2 = new Rectangle();
        Rectangle _rect3 = new Rectangle();

        

        Polygon py1 = new Polygon();
        Polygon py2 = new Polygon();
        Polygon py3 = new Polygon();

        public Dqfh22()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 95;

            _rect1.Stroke = _rect2.Stroke = _rect3.Stroke =
            py1.Stroke = py2.Stroke = py3.Stroke = new SolidColorBrush(DQFHCommon.DQFHLineColor);

            _rect1.StrokeThickness = _rect2.StrokeThickness = _rect3.StrokeThickness =
            py1.StrokeThickness = py2.StrokeThickness = py3.StrokeThickness = DQFHCommon.DQFHLineWidth;


            _canvas.Children.Add(py1);
            _canvas.Children.Add(py2);
            _canvas.Children.Add(py3);

            _canvas.Children.Add(_rect1);
            _canvas.Children.Add(_rect2);
            _canvas.Children.Add(_rect3);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.95;
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
           typeof(Color), typeof(Dqfh22), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh22), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh22), new PropertyMetadata(0));
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
            double RectWidth = this.Width * 0.54;

            //圆坐标
            double yX = this.Width / 2 - RectWidth / 2;
            double yY = 0;

            _rect1.Width = _rect1.Height = _rect1.RadiusX = _rect1.RadiusY = RectWidth;
            _rect1.SetValue(Canvas.LeftProperty, yX);
            py1.Points = GetPoints(RectWidth, yX, yY);


            yX = 0;
            yY = this.Height - RectWidth;
            _rect2.Width = _rect2.Height = _rect2.RadiusX = _rect2.RadiusY = RectWidth;
            _rect2.SetValue(Canvas.TopProperty, yY);
            py2.Points = GetPoints(RectWidth, yX, yY);

            yX = this.Width - RectWidth;
            yY = this.Height - RectWidth;
            _rect3.Width = _rect3.Height = _rect3.RadiusX = _rect3.RadiusY = RectWidth;
            _rect3.SetValue(Canvas.LeftProperty, yX);
            _rect3.SetValue(Canvas.TopProperty, yY);
            py3.Points = GetPoints(RectWidth, yX, yY);
        }

        /// <summary>
        /// 根据圆中的信息获取三角坐标
        /// </summary>
        /// <param name="RectWidth"></param>
        /// <param name="yX"></param>
        /// <param name="yY"></param>
        /// <returns></returns>
        private PointCollection GetPoints(double RectWidth,double yX,double yY)
        {
            //三角
            //找到圆的中心点
            double sjHeithg = this.Height * 0.172;//三角的高度
            double sjWidth = this.Width * 0.32;//三角长度
            //圆信息
          

            //三解的顶点x,y
            double sjX = yX + RectWidth / 2;
            double sjY = yY + RectWidth / 2 - sjHeithg / 2;
            //下面二个坐标位置 
            double sjBottY = sjY + sjHeithg;
            double sjBottomX1 = sjX - sjWidth / 2;
            double sjBottomX2 = sjX + sjWidth / 2;

            PointCollection pc = new PointCollection();
            pc.Add(new Point(sjX, sjY));
            pc.Add(new Point(sjBottomX1, sjBottY));
            pc.Add(new Point(sjBottomX2, sjBottY));
            return pc;
        }
    }
}
