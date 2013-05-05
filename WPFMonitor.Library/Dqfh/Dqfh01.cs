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
    public class Dqfh01: MonitorControl
    {
        private Canvas _canvas = new Canvas();
        private Line _LineX1 = new Line();
        private Line _LineX2 = new Line();
        private Line _LineY1 = new Line();
        private Line _LineY2 = new Line();
        private Rectangle _Rect = new Rectangle();
        public Dqfh01()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_Rect);
            _canvas.Children.Add(_LineX1);
            _canvas.Children.Add(_LineX2);
            _canvas.Children.Add(_LineY1);
            _canvas.Children.Add(_LineY2);
            this.Width = 100;
            this.Height = 100;
            Paint();

            this.SizeChanged += new SizeChangedEventHandler(Dqfh01_SizeChanged);
        }

        private void Dqfh01_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Height= this.Width = e.NewSize.Width;           
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
           typeof(Color), typeof(Dqfh01), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dqfh01), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dqfh01), new PropertyMetadata(0));
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
            //中间10%;           
            double _LineWith = 0.5;//线宽度
            SolidColorBrush _LineStyle = new SolidColorBrush(Colors.Black);           
            //相隔距离
            double _jl = 0.15;

            _LineX1.X1 = _LineX2.X1 = this.Width * _jl;
            _LineX1.X2 = _LineX2.X2 = this.Width * (1 - _jl);

            _LineX1.Y1 = _LineX1.Y2 = this.Height * 0.4;
            _LineX2.Y1 = _LineX2.Y2 = this.Height * 0.6;
            _LineX2.Stroke = _LineX1.Stroke = _LineStyle;
            _LineX1.StrokeThickness = _LineX2.StrokeThickness = _LineWith;

            //线路                     
            _LineY1.X1 = _LineY1.X2 = _LineY2.X1 = _LineY2.X2 = this.Width / 2;
            _LineY1.Y1 = this.Height * _jl;
            _LineY1.Y2 = this.Height * 0.4;

            _LineY2.Y1 = this.Height * 0.6;
            _LineY2.Y2 = this.Height * (1 - _jl);
            _LineY1.Stroke = _LineY2.Stroke = _LineStyle;
            _LineY1.StrokeThickness = _LineY2.StrokeThickness = _LineWith;

            //圆
            _Rect.Width = _Rect.Height = _Rect.RadiusX = _Rect.RadiusY = this.Width;
            _Rect.Fill = new SolidColorBrush();
            _Rect.StrokeThickness = _LineWith;
            _Rect.Stroke = _LineStyle;

        }
    }
}
