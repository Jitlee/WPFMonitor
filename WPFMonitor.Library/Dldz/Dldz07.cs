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
    public class Dldz07 : MonitorControl
    {
        private Canvas _canvas = new Canvas();


        Polyline pyLeft = new Polyline();
        Polyline pyRight = new Polyline();

        Ellipse _Elli = new Ellipse();

        public Dldz07()
        {

            this.Content = _canvas;
            this.Width = 100;
            this.Height = 49;

            _canvas.Children.Add(pyLeft);
            _canvas.Children.Add(pyRight);
            _canvas.Children.Add(_Elli);

            _Elli.StrokeThickness =pyLeft.StrokeThickness = pyRight.StrokeThickness = DLDZCommon.DLDZLineWidth;
            _Elli.Stroke =pyLeft.Stroke = pyRight.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            
            Paint();

            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.49;
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
           typeof(Color), typeof(Dldz07), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz07), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz07), new PropertyMetadata(0));
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
            double pyHeight = this.Height * 0.32;

            double pyWidth = this.Width * 0.21;

            double pyTopY = (this.Height - pyHeight) / 2;

            pyLeft.Points.Clear();
            pyLeft.Points.Add(new Point(pyWidth, pyTopY));
            pyLeft.Points.Add(new Point(0, pyTopY));
            pyLeft.Points.Add(new Point(0, pyTopY + pyHeight));
            pyLeft.Points.Add(new Point(pyWidth, pyTopY + pyHeight));

            pyRight.Points.Clear();
            pyRight.Points.Add(new Point(this.Width- pyWidth, pyTopY));
            pyRight.Points.Add(new Point(this.Width, pyTopY));
            pyRight.Points.Add(new Point(this.Width, pyTopY + pyHeight));
            pyRight.Points.Add(new Point(this.Width - pyWidth, pyTopY + pyHeight));


            //中间tu圆
            double _ElliWidth=this.Width * 0.63;
            _Elli.Width = _ElliWidth;
            _Elli.Height = this.Height - DLDZCommon.DLDZLineWidth * 2;

            _Elli.SetValue(Canvas.LeftProperty,(this.Width- _ElliWidth)/2);

        }

    }
}

