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
using Microsoft.Expression.Controls;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.Other
{
    /// <summary>
    /// 其它控件云
    /// </summary>
    public class CalloutControl: MonitorControl
    {
        private Callout _canvas = new Callout();
       
        public CalloutControl()
        {
            _canvas.CalloutStyle = Microsoft.Expression.Media.CalloutStyle.Cloud;
            _canvas.AnchorPoint = new Point(-100, -500);
            base.Content= _canvas;
            this.Width = 100;
            this.Height = 70;

            Paint();
            this.SizeChanged += new SizeChangedEventHandler(Callout_SizeChanged);
        }

        private void Callout_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Width; 
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
                if (name == "CStroke".ToUpper())
                {
                    CStroke = Common1.StringToColor(value);
                }
                else if (name == "CStrokeThickness".ToUpper())
                {
                    CStrokeThickness = Convert.ToInt32(value);
                }
                else if (name == "CFillColor".ToUpper())
                {
                    CFillColor = Common1.StringToColor(value);
                }
                else if (name == "CText".ToUpper())
                {
                    CText = value;
                }
            }
            Paint();
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
           "BackColor", "ForeColor", "Transparent","Translate"
        ,"CStroke","CStrokeThickness","CFillColor","CText"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(CalloutControl), new PropertyMetadata(Colors.White));
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
            typeof(Color), typeof(CalloutControl), new PropertyMetadata(Colors.Black));
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
        typeof(int), typeof(CalloutControl), new PropertyMetadata(0));
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

        private static readonly DependencyProperty CStrokeProperty = DependencyProperty.Register("CStroke",
        typeof(int), typeof(CalloutControl), new PropertyMetadata(0));
        private Color _CStroke = Colors.Black;
        [DefaultValue(""), Description("边框颜色"), Category("我的属性")]
        public Color CStroke
        {
            get { return _CStroke; }
            set
            {
                _CStroke = value;
                SetAttrByName("CStroke", value);
                Paint();
            }
        }


        private static readonly DependencyProperty CStrokeThicknessProperty = DependencyProperty.Register("CStrokeThickness",
        typeof(int), typeof(CalloutControl), new PropertyMetadata(0));
        private int _CStrokeThickness = 1;
        [DefaultValue(""), Description("边框大小"), Category("我的属性")]
        public int CStrokeThickness
        {
            get { return _CStrokeThickness; }
            set
            {
                _CStrokeThickness = value;
                SetAttrByName("CStrokeThickness", value);
                Paint();
            }
        }
        private static readonly DependencyProperty CFillColorProperty = DependencyProperty.Register("CFillColor",
        typeof(int), typeof(CalloutControl), new PropertyMetadata(0));
        private Color _CFillColor = Colors.Yellow;
        [DefaultValue(""), Description("边框大小"), Category("我的属性")]
        public Color CFillColor
        {
            get { return _CFillColor; }
            set
            {
                _CFillColor = value;
                SetAttrByName("CFillColor", value);
                Paint();
            }
        }


        private static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
        typeof(int), typeof(CalloutControl), new PropertyMetadata(0));
        private string _CText = "Callount";
        [DefaultValue(""), Description("内容"), Category("我的属性")]
        public string CText
        {
            get { return _CText; }
            set
            {
                _CText = value;
                SetAttrByName("CText", value);
                Paint();
            }
        }

        #endregion

        #endregion


        private void Paint()
        {
           _canvas.Stroke
             = new SolidColorBrush(CStroke);
            _canvas.StrokeThickness = CStrokeThickness;
            _canvas.Fill = new SolidColorBrush(CFillColor);
            _canvas.Content = CText;
        }
    }
}
