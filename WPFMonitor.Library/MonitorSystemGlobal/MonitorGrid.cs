using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.Library.MonitorSystemGlobal
{
    public class MonitorGrid: MonitorControl
    {
        Border _border = new Border();
        public MonitorGrid()
        {
            _border.Background = new SolidColorBrush(Colors.White);
            Content = _border;
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
            "RowCount","GridColor","ColumnCount","LineWidth" };

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(MonitorGrid), new PropertyMetadata(0));
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
                    _border.Background = new SolidColorBrush();
                    
                }
                else
                {
                    _border.Background = new SolidColorBrush(Colors.White);
                }
                
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }

        private static readonly DependencyProperty RowCountProperty =
          DependencyProperty.Register("RowCount",
          typeof(int), typeof(MonitorGrid), new PropertyMetadata(0));
        private int _RowCount = 0;
        public int RowCount
        {
            get { return _RowCount; }
            set
            {
                _RowCount = value;
                SetAttrByName("RowCount", value);
                PaintGrid();
            }
        }

        private static readonly DependencyProperty GridColorProperty =
         DependencyProperty.Register("GridColor",
         typeof(string), typeof(MonitorGrid), new PropertyMetadata(""));
        private string _GridColor = "";
        public string GridColor
        {
            get { return _GridColor; }
            set
            {
                _GridColor = value;
                SetAttrByName("GridColor", value);
            }
        }

        private static readonly DependencyProperty ColumnCountProperty =
          DependencyProperty.Register("ColumnCount",
          typeof(int), typeof(MonitorGrid), new PropertyMetadata(0));
        private int _ColumnCount = 0;
        public int ColumnCount
        {
            get { return _ColumnCount; }
            set
            {
                _ColumnCount = value;
                SetAttrByName("ColumnCount", value);
                PaintGrid();
            }
        }

        private static readonly DependencyProperty LineWidthProperty =
          DependencyProperty.Register("LineWidth",
          typeof(int), typeof(MonitorGrid), new PropertyMetadata(0));
        private int _LineWidth = 0;
        public int LineWidth
        {
            get { return _LineWidth; }
            set
            {
                _LineWidth = value;
                SetAttrByName("LineWidth", value);
                PaintGrid();
            }
        }
        
        #endregion

        /// <summary>
        /// 画Grid
        /// </summary>
        private void  PaintGrid()
        {
            //_LineWidth  _ColumnCount  _GridColor  _RowCount

            if (_RowCount < 0) _RowCount = 0;
            if (_ColumnCount < 0) _ColumnCount = 0;

            Grid _grid = new Grid();
            for (int i = 0; i < _RowCount; i++)
            {
                RowDefinition rf=new RowDefinition();
                _grid.RowDefinitions.Add(rf);
            }

            for (int i = 0; i < _ColumnCount; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                _grid.ColumnDefinitions.Add(cd);
            }
                        
            for (int i = 0; i < _RowCount; i++)
            {
                for (int j = 0; j < _ColumnCount; j++)
                {
                    Border mbor = new Border();
                    mbor.SetValue(Grid.RowProperty, i);
                    mbor.SetValue(Grid.ColumnProperty, j);
                    mbor.BorderBrush = new SolidColorBrush(Colors.Red);
                    mbor.BorderThickness = new Thickness((double)_LineWidth);
                    
                    _grid.Children.Add(mbor);
                    
                   
                }
            }
           _border.BorderBrush = new SolidColorBrush(Colors.Red);
           _border.BorderThickness = new Thickness((double)_LineWidth);
           //_border.Child.ClearValue();
            _border.Child = _grid;
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
                if (pro.PropertyName == "RowCount")
                {

                    _RowCount = int.Parse(pro.PropertyValue);
                }
                else if (pro.PropertyName == "ColumnCount")
                {
                    _ColumnCount = int.Parse(pro.PropertyValue);
                }
                else if (pro.PropertyName == "LineWidth")
                {
                    _LineWidth = int.Parse(pro.PropertyValue);
                }
                else if (pro.PropertyName == "GridColor")
                {
                    GridColor = pro.PropertyValue;
                }
            }
            PaintGrid();
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
