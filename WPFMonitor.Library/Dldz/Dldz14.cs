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

namespace MonitorSystem.Dldz
{
    /// <summary>
    /// 电力电子14
    /// </summary>
    public class Dldz14 : MonitorControl
    {
        private Canvas _canvas = new Canvas();

        Line _line1 = new Line();
        Line _line2 = new Line();        
        Line _line3 = new Line();

        Rectangle _rect1 = new Rectangle();
        Rectangle _rect2 = new Rectangle();
        public Dldz14()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 25;

            _line1.StrokeThickness = _line2.StrokeThickness = _line3.StrokeThickness =
            _rect1.StrokeThickness = _rect2.StrokeThickness = DLDZCommon.DLDZLineWidth;
            _line1.Stroke = _line2.Stroke = _line3.Stroke =
                _rect1.Stroke = _rect2.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            _rect1.Fill = _rect2.Fill = new SolidColorBrush(DLDZCommon.DLDZFilleColor);

            _canvas.Children.Add(_line1);
            _canvas.Children.Add(_line2);
            _canvas.Children.Add(_line3);
            _canvas.Children.Add(_rect1);
            _canvas.Children.Add(_rect2);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.25;
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

            BackColor = Common1.StringToColor(ScreenElement.BackColor);
            ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
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
           typeof(Color), typeof(Dldz14), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz14), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz14), new PropertyMetadata(0));
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
            double _lineY = this.Height * 0.88;
            double RectWidth = this.Height * 0.24;
            //Lin1
            _line1.X1 = 0;
            _line1.Y2 = _line1.Y1 = _lineY;
            _line1.X2 = 0.334 * this.Width;

            //Lin2
            _line2.X1 = 0.364 * this.Width;
            _line2.X2 = this.Width * 0.672;
            _line2.Y1 = _lineY;            
            _line2.Y2 = 0;

            _line3.X1 = this.Width * 0.67;
            _line3.X2 = this.Width;
            _line3.Y2 = _line3.Y1 = _lineY;

            _rect1.Width = _rect1.Height = _rect1.RadiusX = _rect1.RadiusY = this.Width * 0.06;
            _rect2.Width = _rect2.Height = _rect2.RadiusX = _rect2.RadiusY = this.Width * 0.06;

            _rect1.SetValue(Canvas.TopProperty, this.Height - RectWidth);
            _rect2.SetValue(Canvas.TopProperty, this.Height - RectWidth);

            _rect1.SetValue(Canvas.LeftProperty, 0.334 * this.Width);
            _rect2.SetValue(Canvas.LeftProperty, this.Width * 0.61);

        }

    }
}

