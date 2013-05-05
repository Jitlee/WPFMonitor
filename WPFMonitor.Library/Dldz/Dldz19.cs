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
    /// 电力电子19
    /// </summary>
    public class Dldz19 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Line _Line1 = new Line();
        Line _Line2 = new Line();
        Line _Line3 = new Line();
        Line _Line4 = new Line();
        Line _Line5 = new Line();
        Line _Line6 = new Line();
        Line _Line7 = new Line();

        
        public Dldz19()
        {
            this.Content = _canvas;
 this.Width = 100;
            this.Height = 34;
            //线
            _Line1.Stroke = _Line2.Stroke = _Line3.Stroke = _Line4.Stroke = _Line5.Stroke =
                _Line7.Stroke = _Line6.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);

            _Line1.StrokeThickness = _Line2.StrokeThickness = _Line3.StrokeThickness =
                _Line4.StrokeThickness = _Line5.StrokeThickness = _Line6.StrokeThickness
                = _Line7.StrokeThickness = DLDZCommon.DLDZLineWidth;

            
            _canvas.Children.Add(_Line1);
            _canvas.Children.Add(_Line2);
            _canvas.Children.Add(_Line3);
            _canvas.Children.Add(_Line4);
            _canvas.Children.Add(_Line5);
            _canvas.Children.Add(_Line6);
            _canvas.Children.Add(_Line7);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.34;
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
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(Dldz19), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz19), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz19), new PropertyMetadata(0));
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
            double _RectWidth = this.Width * 0.05;

            double _LineY = this.Height /2;
            
            _Line1.X1 = 0;
            _Line1.X2 = this.Width * 0.46;
            _Line1.Y1 = _Line1.Y2 = _LineY;
            
            _Line2.X1 = this.Width * 0.54;
            _Line2.X2 = this.Width;
            _Line2.Y1 = _Line2.Y2 = _LineY;

            _Line3.X1 = _Line3.X2 = this.Width * 0.46;
            _Line3.Y1 = 0;
            _Line3.Y2 = this.Height;
            
            _Line4.X1 = _Line4.X2 = this.Width * 0.54;
            _Line4.Y1 = this.Height * 0.15;//
            _Line4.Y2 = this.Height * 0.85;
          
            _Line5.X1 = this.Width * 0.3;
            _Line5.X2 = this.Width * 0.4;
            _Line5.Y1 = _Line5.Y2 = this.Height * 0.2;

            _Line6.X1 = this.Width * 0.575;
            _Line6.X2 = this.Width * 0.675;
            _Line6.Y1 = _Line6.Y2 = this.Height * 0.2;

            double sxY1=this.Height * 0.066;
            _Line7.X2 = _Line7.X1 = this.Width * 0.35;
            _Line7.Y1 = sxY1;
            _Line7.Y2 = sxY1 + this.Width * 0.1;

        }

    }
}

