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

namespace WPFMonitor.Library.MonitorSystemGlobal
{
    public class MonitorLine : MonitorControl
    {
        Border _mCanvas = new Border();
        public MonitorLine()
        {
            Content = _mCanvas;
        }

        #region 属性

       

        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
		}


        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","Translate", "ForeColor","Transparent",
            "LineType","LineWidth"};

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
         DependencyProperty.Register("Transparent",
         typeof(int), typeof(MonitorLine), new PropertyMetadata(0));
        private int _Transparent;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
                PainLine();
            }
        }


        private static readonly DependencyProperty ForeColorProperty = DependencyProperty.Register("ForeColor",
   typeof(int), typeof(MonitorLine), new PropertyMetadata(0));
        private Color _ForeColor = Colors.Black;
        [DefaultValue(""), Description("字体颜色"), Category("外观")]
        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;
                if (ScreenElement != null)
                    ScreenElement.ForeColor = value.ToString();
                PainLine();
            }
        }


        private static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth",
            typeof(int), typeof(MonitorLine), new PropertyMetadata(1));
        private int _LineWidth = 1;
        public int LineWidth
        {
            get { return _LineWidth; }
            set { _LineWidth = value; PainLine(); }
        }


        private static readonly DependencyProperty LineTypeProperty =
            DependencyProperty.Register("LineType",
            typeof(int), typeof(MonitorLine), new PropertyMetadata(0));
        private int _LineType=0;
        public int LineType
        {
            get { return _LineType; }
            set { _LineType = value; PainLine(); }
        }
        #endregion

        private void PainLine()
        {
           // Color ForColor=Colors.Green;
            _mCanvas.Child = null;
            if (_Transparent == 1)
            {
                _mCanvas.Background = new SolidColorBrush();
            }
            else
            {
                _mCanvas.Background = new SolidColorBrush(Colors.White);
            }

            if (_LineType == 0)
            {
                _mCanvas.Background = new SolidColorBrush(_ForeColor);
            }
            else if (_LineType == 1)
            {
                Grid _grid = new Grid();
                ColumnDefinition cd = new ColumnDefinition();
                _grid.ColumnDefinitions.Add(cd);
                ColumnDefinition cd1 = new ColumnDefinition();
                _grid.ColumnDefinitions.Add(cd1);

                Border mbor = new Border();
                mbor.Background = new SolidColorBrush(_ForeColor);
                _grid.Children.Add(mbor);
                _mCanvas.Child=_grid;
            }
            else if (_LineType == 2)
            {
                Line l = new Line();
                l.X1 = this.Width;
                l.Y1 = 0;

                l.X2 = 0;
                l.Y2 = this.Height;
                l.Width = this.Width;
                l.Height = this.Height;
                l.Stroke = new SolidColorBrush(_ForeColor);
                l.StrokeThickness = (double)_LineWidth;
                _mCanvas.Child = l;
            }
            else
            {
                //_mCanvas.Children.Clear();
                _mCanvas.Background = new SolidColorBrush();
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

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;
            }
        }

        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                if (pro.PropertyName == "LineWidth")
                {
                    _LineWidth = int.Parse(pro.PropertyValue);
                }
                else if (pro.PropertyName == "LineType")
                {
                    _LineType = int.Parse(pro.PropertyValue);
                }
                PainLine();
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;

             ForeColor = Common.StringToColor(ScreenElement.ForeColor);
             Transparent = ScreenElement.Transparent.Value;
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }
    }
}
 