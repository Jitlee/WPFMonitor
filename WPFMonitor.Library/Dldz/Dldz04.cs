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
using WPFMonitor.Model;
using System.ComponentModel;

namespace WPFMonitor.Library.Dldz
{
    public class Dldz04 : MonitorControl
    {
        /// <summary>
        /// 电力电子03
        /// </summary>
        private Canvas _canvas = new Canvas();
        Line _lineLeft = new Line();
        Line _lineRight = new Line();
        Line _lineY = new Line();
        Polyline py = new Polyline();

        Line _SortLine = new Line();
        Polygon pySJ = new Polygon();

        public Dldz04()
        {
            this.Content = _canvas;

            _canvas.Children.Add(_lineLeft);
            _canvas.Children.Add(_lineRight);
            _canvas.Children.Add(_lineY);

            _canvas.Children.Add(py);
            _canvas.Children.Add(_SortLine);

            pySJ.Fill = new SolidColorBrush(DLDZCommon.DLDZFilleColor);
            _canvas.Children.Add(pySJ);

            //线条宽度
            _lineLeft.StrokeThickness = _SortLine.StrokeThickness = pySJ.StrokeThickness
                = _lineRight.StrokeThickness = _lineY.StrokeThickness = py.StrokeThickness = DLDZCommon.DLDZLineWidth;
            //线条颜色
            _lineLeft.Stroke = _SortLine.Stroke = pySJ.Stroke
                = _lineRight.Stroke = _lineY.Stroke = py.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);
            this.Width = 100;
            this.Height = 57;
            this.SizeChanged += new SizeChangedEventHandler(DldzSizeChanged);
            Paint();
        }

        private void DldzSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.57;
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
                //if (name == "LeftOrNot".ToUpper())
                //{

                //}
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
           typeof(Color), typeof(Dldz04), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz04), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz04), new PropertyMetadata(0));
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
            double OtherHeight = this.Height * 0.8;

            _lineLeft.Y1 = _lineLeft.Y2 = this.Height / 2;
            _lineLeft.X1 = 0;
            _lineLeft.X2 = this.Width * 0.45;

            _lineRight.Y1 = _lineRight.Y2 = this.Height / 2;
            _lineRight.X1 = this.Width * 0.55;
            _lineRight.X2 = this.Width;


            _lineY.X1 = _lineY.X2 = this.Width * 0.55;
            _lineY.Y1 = this.Height* 0.1;
            _lineY.Y2 = this.Height* 0.9;

            double StartP = this.Height * 0.1;
            //其它
            py.Points.Clear();
            py.Points.Add(new Point(this.Width * 0.39, OtherHeight * 0.1 + StartP));
            py.Points.Add(new Point(this.Width * 0.45, OtherHeight * 0.3 + StartP));
            py.Points.Add(new Point(this.Width * 0.45, OtherHeight * 0.7 + StartP));
            py.Points.Add(new Point(this.Width * 0.39, OtherHeight * 0.9 + StartP));

            //箭头线
            _SortLine.X1 = this.Width * 0.253;
            _SortLine.Y1 = this.Height;
            _SortLine.X2 = this.Width * 0.73;
            _SortLine.Y2 = this.Height * 0.175;
            //箭头三角
            pySJ.Points.Clear();
            pySJ.Points.Add(new Point(this.Width * 0.69, this.Height * 0.087));
            pySJ.Points.Add(new Point(this.Width * 0.77, this.Height * 0.25));
            pySJ.Points.Add(new Point(this.Width * 0.813, 0));
        }

    }
}
