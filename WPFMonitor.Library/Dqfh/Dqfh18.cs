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
    public class Dqfh18 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Line _Line1 = new Line();
        Line _Line2 = new Line();
        Line _Line3 = new Line();

        Rectangle _rect1 = new Rectangle();
        Rectangle _rect2 = new Rectangle();

        Polygon py = new Polygon();

        public Dqfh18()
        {
            this.Content = _canvas;
            this.Width = 58;
            this.Height = 100;

            _rect1.Stroke = _rect2.Stroke = py.Stroke =
            _Line1.Stroke = _Line2.Stroke = _Line3.Stroke = new SolidColorBrush(DQFHCommon.DQFHLineColor);
            _rect1.StrokeThickness = _rect2.StrokeThickness =py.StrokeThickness=
            _Line1.StrokeThickness = _Line2.StrokeThickness = _Line3.StrokeThickness = DQFHCommon.DQFHLineWidth;


            _canvas.Children.Add(_Line1);
            _canvas.Children.Add(_Line2);
            _canvas.Children.Add(_Line3);

            _canvas.Children.Add(_rect1);
            _canvas.Children.Add(_rect2);
            py.Fill = new SolidColorBrush(DQFHCommon.DQFHFilleColor2);
            _canvas.Children.Add(py);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Height * 0.58;
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
           typeof(Color), typeof(Dqfh18), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh18), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh18), new PropertyMetadata(0));
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
            _rect1.Width = _rect1.Height = _rect1.RadiusX = _rect1.RadiusY = this.Width;
            _rect2.Width = _rect2.Height = _rect2.RadiusX = _rect2.RadiusY = this.Width;
            _rect2.SetValue(Canvas.TopProperty, this.Height - this.Width);

            //三角
            PointCollection pc = new PointCollection();
            pc.Add(new Point(this.Width / 2, this.Height * 0.15));
            pc.Add(new Point(this.Width * 0.1436, this.Height * 0.36));
            pc.Add(new Point(this.Width * 0.833, this.Height * 0.36));
            py.Points = pc;

            //三线
            _Line1.X1 = _Line1.X2 = this.Width / 2;
            _Line1.Y1 = this.Height * 0.712;
            _Line1.Y2 = this.Height * 0.872;

            _Line2.X1 = this.Width / 2;
            _Line2.Y1 = this.Height * 0.705;
            _Line2.X2 = this.Width * 0.29;
            _Line2.Y2 = this.Height * 0.584;

            _Line3.X1 = this.Width / 2;
            _Line3.Y1 = this.Height * 0.705;
            _Line3.X2 = this.Width * 0.71;
            _Line3.Y2 = this.Height * 0.584;
        }
    }
}
