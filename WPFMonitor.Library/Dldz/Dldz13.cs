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
    /// 电力电子
    /// </summary>
    public class Dldz13 : MonitorControl
    {
        private Canvas _canvas = new Canvas();

        Line _linex1 = new Line();
        Line _linex2 = new Line();
        Line _liney1 = new Line();
        Line _liney2 = new Line();
        Line _linexy = new Line();

        public Dldz13()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 39;

            _linex1.StrokeThickness = _linex2.StrokeThickness =_linexy.StrokeThickness =
            _liney1.StrokeThickness = _liney2.StrokeThickness = DLDZCommon.DLDZLineWidth;
            _linex1.Stroke = _linex2.Stroke = _linexy.Stroke = 
                _liney1.Stroke = _liney2.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);

            _canvas.Children.Add(_linex1);
            _canvas.Children.Add(_linex2);
            _canvas.Children.Add(_liney1);
            _canvas.Children.Add(_liney2);
            _canvas.Children.Add(_linexy);

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
           typeof(Color), typeof(Dldz13), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz13), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz13), new PropertyMetadata(0));
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
            _linex1.X1 = 0;
            _linex1.X2 = this.Width * 0.45;
            _linex1.Y1 = _linex1.Y2 = this.Height / 2;

            _linex2.X1 = this.Width * 0.55;
            _linex2.X2 = this.Width;
            _linex2.Y1 = _linex2.Y2 = this.Height / 2;

            _liney1.X1 = _liney1.X2 = this.Width * 0.45;
            _liney1.Y1 = this.Height * 0.17;
            _liney1.Y2 = this.Height * 0.81;

            _liney2.X1 = _liney2.X2 = this.Width * 0.55;
            _liney2.Y1 = 0;
            _liney2.Y2 = this.Height;

            _linexy.X1 = this.Width * 0.276;
            _linexy.Y1 = this.Height;
            _linexy.Y2 = 0;
            _linexy.X2 = this.Width * 0.78;
        }

    }
}

