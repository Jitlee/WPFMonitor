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
    public class Dldz25 : MonitorControl
    {
        private Canvas _canvas = new Canvas();
        Polyline pl = new Polyline();
        public Dldz25()
        {
            this.Content = _canvas;
            this.Width = 100;
            this.Height = 17;

            _canvas.Children.Add(pl);
            pl.StrokeThickness = DLDZCommon.DLDZLineWidth;
            pl.Stroke = new SolidColorBrush(DLDZCommon.DLDZLineColor);

            this.SizeChanged += new SizeChangedEventHandler(DldzSizeChanged);
        }

        private void DldzSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width * 0.17;
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
           typeof(Color), typeof(Dldz25), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(Dldz25), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(Dldz25), new PropertyMetadata(0));
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



            double _aLinePer = 0.21;//两直线，分别占总长度比例
            //线
            double _LineY = this.Height / 2;//横线的Y轴位置
            double _LineLength = this.Width * _aLinePer;
            
            double dbPointNum = 3;//单边点数量
            double dWidth = this.Width * (1- _aLinePer*2);//弯曲长度            
            double minWidth = dWidth / (dbPointNum * 4);//第一个点位置之间的宽度(点数*4分之一宽度)
            double _shPointHeight = this.Height * 0.5; //横线到上下点的高度

            PointCollection pc = new PointCollection();
            //前直线1
            pc.Add(new Point(0, _LineY));
            pc.Add(new Point(_LineLength, _LineY));
            //上下三个点
            for (int i = 0; i < dbPointNum * 2; i++)
            {
                if (i == 0)
                {
                    pc.Add(new Point(_LineLength + minWidth, _LineY - _shPointHeight));
                }
                int mod = i % 2;
                if (mod == 1)
                {
                    pc.Add(new Point(_LineLength + minWidth * (i * 2 + 1), _LineY + _shPointHeight));
                }
                else
                {
                    pc.Add(new Point(_LineLength + minWidth * (i * 2 + 1), _LineY - _shPointHeight));
                }
            }
            //直线2
            pc.Add(new Point(this.Width - _LineLength, _LineY));
            pc.Add(new Point(this.Width, _LineY));
            pl.Points = pc;
        }

    }
}
