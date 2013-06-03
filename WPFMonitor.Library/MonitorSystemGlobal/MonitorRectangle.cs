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
using WPFMonitor.Model;
using System.Collections.Generic;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public class MonitorRectangle : MonitorControl
    {

        Rectangle _mRect = new Rectangle();
        public MonitorRectangle()
        {
            _mRect.StrokeThickness = 1;
            _mRect.Stroke = new SolidColorBrush(Colors.Black);
            _mRect.Fill = new SolidColorBrush(Colors.White);

            Content = _mRect;
        }
        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


        #region 属性
        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","Translate", "Foreground","Transparent",
            "Degrees"};

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(MonitorRectangle), new PropertyMetadata(0));
        private int _Transparent;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                if (value == 1)
                {

                    _mRect.Fill = new SolidColorBrush();
                }
                else
                {
                    _mRect.Fill = new SolidColorBrush(Colors.White);                    
                }
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }

        }

        private static readonly DependencyProperty DegreesProperty =
          DependencyProperty.Register("Degrees",
          typeof(int), typeof(MonitorText), new PropertyMetadata(0));
        private int _Degrees = 0;
        public int Degrees
        {
            get { return _Degrees; }
            set
            {
                _Degrees = value;
                _mRect.RadiusX = _mRect.RadiusY = _Degrees;
                SetAttrByName("Degrees", value);
            }
        }
        #endregion

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
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

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }
        
        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                if (pro.PropertyName == "Degrees")
                {
                    Degrees = int.Parse(pro.PropertyValue);
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            Transparent = ScreenElement.Transparent.Value;
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
        }

        public List<t_ElementProperty> GetProperty()
        {
            return ListElementProp;
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }
    }
}

        
    