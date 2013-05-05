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

namespace WPFMonitor.Library.MonitorSystemGlobal
{
    public class MonitorFoldLine: MonitorControl
    {
        Border _mBorder = new Border();
        public MonitorFoldLine()
        {
            Content = _mBorder;
            _mBorder.Background = new SolidColorBrush(Colors.White);
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
            "IsRightDirect","EdgeSize","EdgeArray","EdgeSize","lineWidth" };      

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(MonitorFoldLine), new PropertyMetadata(0));
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
                    _mBorder.Background = new SolidColorBrush();
                }
                else
                {
                    _mBorder.Background = new SolidColorBrush(Colors.White);
                }
                
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }

        }



        private static readonly DependencyProperty IsRightDirectProperty =
          DependencyProperty.Register("IsRightDirect",
          typeof(bool), typeof(MonitorText), new PropertyMetadata(true));
        private bool _IsRightDirect = false;
        public bool IsRightDirect
        {
            get { return _IsRightDirect; }
            set {
                _IsRightDirect = value;
                SetAttrByName("IsRightDirect", value);
            }
        }
        
        private static readonly DependencyProperty EdgeSizeProperty =
          DependencyProperty.Register("EdgeSize",
          typeof(int), typeof(MonitorText), new PropertyMetadata(3));
        private int _EdgeSize = 3;
        public int EdgeSize
        {
            get { return _EdgeSize; }
            set
            {
                _EdgeSize = value;
                SetAttrByName("EdgeSize", value);
                PaniFoldLin();
            }
        }

        

         private static readonly DependencyProperty EdgeArrayProperty =
          DependencyProperty.Register("EdgeArray",
          typeof(string), typeof(MonitorText), new PropertyMetadata("1,2,3,4"));
         private string _EdgeArray = "1,2,3,4";
         public string EdgeArray
         {
             get { return _EdgeArray; }
             set
             {
                 _EdgeArray = value;
                 SetAttrByName("EdgeArray", value);
                 PaniFoldLin();
             }
         }
      
         private static readonly DependencyProperty lineWidthProperty =
          DependencyProperty.Register("lineWidth",
          typeof(int), typeof(MonitorText), new PropertyMetadata(1));
         private int _lineWidth=1;
         public int lineWidth
         {
             get { return _lineWidth; }
             set {
                 _lineWidth = value; 
                 SetAttrByName("lineWidth", value);
                 PaniFoldLin();
             }
         }
        
        #endregion


         /// <summary>
         /// 根据条件显示边框
         /// </summary>
         private void PaniFoldLin()
         {
               //lineWidth,EdgeArray, EdgeSize
                 //this.lineWidth  
             if (_lineWidth == 0) _lineWidth = 0;
             
             Thickness thi = new Thickness();
             //_EdgeSize
             int Number = 0;
             string[] EdgArr = _EdgeArray.Split(',');
             if (EdgArr != null)
             {
                
                 foreach (string str in EdgArr)
                 {
                     if (_EdgeSize <= Number)
                     {
                         break;
                     }
                     switch (str)
                     {
                         case "1":
                             thi.Left = _lineWidth;
                             Number++;
                             break;
                         case "2":
                             thi.Top = _lineWidth;
                             Number++;
                             break;
                         case "3":
                             thi.Right = _lineWidth;
                             Number++;
                             break;
                         case "4":
                             thi.Bottom = _lineWidth;
                             Number++;
                             break;
                             
                     }
                 }
             }
             _mBorder.BorderThickness = thi;
             _mBorder.BorderBrush = new SolidColorBrush(Colors.Red);
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
                if (pro.PropertyName == "IsRightDirect")
                {
                    _IsRightDirect = Common.ConvertToBool(pro.PropertyValue);
                }
                else if (pro.PropertyName == "EdgeSize")
                {
                    _EdgeSize =int.Parse( pro.PropertyValue);
                }
                else if (pro.PropertyName == "EdgeArray")
                {
                    _EdgeArray = pro.PropertyValue;
                }
                else if (pro.PropertyName == "lineWidth")
                {
                    _lineWidth =int.Parse( pro.PropertyValue);
                }

                PaniFoldLin();
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
