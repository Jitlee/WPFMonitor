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
    public class Dldz16 : MonitorControl
    {
        private Canvas _canvas = new Canvas();

        Polyline pl = new Polyline();
        Polyline plC = new Polyline();
        public Dldz16()
        {
            
            this.Height = 100;
            this.Width = 57;

            this.Content = _canvas;
            _canvas.Children.Add(pl);
            _canvas.Children.Add(plC);

            plC.StrokeThickness = pl.StrokeThickness = DLDZCommon.DLDZLineWidth;
            plC.Stroke = pl.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Control_SizeChanged);
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Height * 0.57;
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
           typeof(Color), typeof(Dldz16), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz16), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz16), new PropertyMetadata(0));
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
           
           
            PointCollection pc=new PointCollection();
            pc.Add(new Point(0, this.Height * 0.75));
            pc.Add(new Point(0, this.Height * 0.224));

            pc.Add(new Point(this.Width * 0.0385, this.Height * 0.15));
            pc.Add(new Point(this.Width * 0.263, this.Height * 0.03));
            //上面直线
            pc.Add(new Point(this.Width * 0.438, 0));
            pc.Add(new Point(this.Width * 0.586, 0));

            pc.Add(new Point(this.Width * 0.754, this.Height * 0.03));
            pc.Add(new Point(this.Width * 0.968,this.Height * 0.144));
            //右边竖线
            pc.Add(new Point(this.Width, this.Height *  0.22));
            pc.Add(new Point(this.Width, this.Height * 0.75));

            pc.Add(new Point(0, this.Height * 0.75));
            pl.Points = pc;

            PointCollection pcc = new PointCollection();
            //左边竖线
            pcc.Add(new Point(this.Width * 0.263, this.Height));
            pcc.Add(new Point(this.Width * 0.263, this.Height* 0.332));

            pcc.Add(new Point(this.Width * 0.295, this.Height * 0.244));
            pcc.Add(new Point(this.Width * 0.35, this.Height * 0.2));

            //上面直线
            pcc.Add(new Point(this.Width * 0.435, this.Height * 0.182));
            pcc.Add(new Point(this.Width * 0.565, this.Height * 0.182));

            pcc.Add(new Point(this.Width * 0.635, this.Height * 0.2));
            pcc.Add(new Point(this.Width * 0.72, this.Height * 0.294));  
            //右边竖线
            pcc.Add(new Point(this.Width * 0.74, this.Height * 0.37));
            pcc.Add(new Point(this.Width * 0.74, this.Height));
            plC.Points = pcc;
        }

    }
}

