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
using System.Collections.Generic;
using System.Linq;
using WPFMonitor.Model;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public class MonitorText : MonitorControl
    {
        TextBox _mTxt = new TextBox();
        public MonitorText()
        {
            Content = _mTxt;
            //Stretch = Stretch.Fill;

            MyText = "";
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
        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
            "Transparent", "Foreground","Transparent","MyText","LinearChange","FromColor","ToColor" };      

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(MonitorText), new PropertyMetadata(0));
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
                    _mTxt.Background=new SolidColorBrush();
                    _mTxt.BorderBrush = new SolidColorBrush();
                }
                else
                {
                    _mTxt.Background = new SolidColorBrush(Colors.White);
                    
                }
                
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }

        }


      
        //MyText
        private static readonly DependencyProperty StretchProperty =
          DependencyProperty.Register("MyText",
          typeof(string), typeof(MonitorText), new PropertyMetadata(""));

        public string MyText
        {
            get { return _mTxt.Text; }
            set { _mTxt.Text = value;
            if (ScreenElement != null)
                ScreenElement.TxtInfo = value; 
            }
        }


        private static readonly DependencyProperty LinearChangeProperty =
          DependencyProperty.Register("LinearChange",
          typeof(bool), typeof(MonitorText), new PropertyMetadata(true));
        private bool _LinearChange = false;
         public bool LinearChange
        {
            get { return _LinearChange; }
            set {
                _LinearChange = value;
                SetLinearChange(value);
                SetAttrByName("LinearChange", value);
            }
        }



         private static readonly DependencyProperty FromColorProperty =
          DependencyProperty.Register("FromColor",
          typeof(string), typeof(MonitorText), new PropertyMetadata("RGB(177,255,255)"));
         private string _FromColor;
         public string FromColor
         {
             get { return _FromColor; }
             set
             {
                 _FromColor = value;
                 SetAttrByName("FromColor", value);
             }
         }

         private static readonly DependencyProperty ToColorProperty =
          DependencyProperty.Register("ToColor",
          typeof(string), typeof(MonitorText), new PropertyMetadata("RGB(16,200,250)"));
         private string _ToColor;
         public string ToColor
         {
             get { return _ToColor; }
             set { _ToColor = value; SetAttrByName("ToColor", value); }
         }
        
        #endregion

         public void SetLinearChange(bool IsEnable)
         {
             if (IsEnable)
             {

             }
         }


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
                if (pro.PropertyName == "LinearChange")
                {
                    LinearChange = Common1.ConvertToBool(pro.PropertyValue);
                }
                else if (pro.PropertyName == "FromColor")
                {
                    FromColor = pro.PropertyValue;
                }
                else if (pro.PropertyName == "ToColor")
                {
                    ToColor = pro.PropertyValue;
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
