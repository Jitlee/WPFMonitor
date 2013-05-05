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
using System.Windows.Media.Imaging;

namespace WPFMonitor.Library.ZTControls
{
    /// <summary>
    /// 53	InputTextBox	2	Text.jpg	组态控件	输入框
    /// </summary>
    public class InputTextBox : MonitorControl
    {
        public override void SetChannelValue(float fValue)
        {
            _textBox.SetValue(TextBox.TextProperty, fValue.ToString());
        }

        public override void DesignMode()
        {
            if (!IsDesignMode)
            {
                AdornerLayer = new Adorner(this);
                AdornerLayer.Selected += OnSelected;

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
                this.SetValue(ContextMenuService.ContextMenuProperty, menu);
                AdornerLayer.Editabled = true;
                _grid.Children.Add(_moveImage);
            }
        }

        public override void UnDesignMode()
        {
            if (IsDesignMode)
            {
                AdornerLayer.Selected -= OnSelected;
                AdornerLayer.ClearValue(ContextMenuService.ContextMenuProperty);
                this.ClearValue(ContextMenuService.ContextMenuProperty);
                AdornerLayer.Dispose();
                AdornerLayer = null;
                _grid.Children.Remove(_moveImage);
            }
        }

        private void OnSelected(object sender, EventArgs e)
        {
            if (null != Selected)
            {
                Selected(this, RoutedEventArgs.Empty);
            }
        }

        #region 属性设置
        SetSingleProperty tpp = new SetSingleProperty();
        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tpp = new SetSingleProperty();

            tpp.Closing += tpp_Closing;
            tpp.DeviceID = this.ScreenElement.DeviceID.Value;
            tpp.ChanncelID = this.ScreenElement.ChannelNo.Value;
            tpp.LevelNo = this.ScreenElement.LevelNo.Value;
            tpp.ComputeStr = this.ScreenElement.ComputeStr;
            tpp.Init();
            tpp.Show();
        }

        protected void tpp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tpp.IsOK)
            {
                this.ScreenElement.DeviceID = tpp.DeviceID;
                this.ScreenElement.ChannelNo = tpp.ChanncelID;
                this.ScreenElement.LevelNo = tpp.LevelNo;
                this.ScreenElement.ComputeStr = tpp.ComputeStr;
            }
        }

        #endregion

        public override FrameworkElement GetRootControl()
        {
            return this;
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


        public override void SetPropertyValue()
        {
            try
            {
                // 2009-1-17
                foreach (var pro in ListElementProp)
                {
                    string name = pro.PropertyName.Trim().ToUpper();
                    string value = pro.PropertyValue.Trim();
                    if (string.Compare(name, "BackColor", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        BackColor = Common.StringToColor(value);
                    }
                    else if (string.Compare(name, "Font", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                    }
                    else if (string.Compare(name, "ForeColor", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        ForeColor = Common.StringToColor(value);
                    }
                    else if (name == "MyScrollBars".ToUpper())
                    {
                        //SetScrollBar(value);
                        MyScrollBars = value;

                    }
                    else if (name == "MyText".ToUpper())
                    {
                        MyText = value;
                    }
                }
            }
            catch
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
        }

        private string[] _browsableProperties = new[] { "Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "Width", "Height", "Left", "Top", "FontFamily", "FontSize", "BackColor", "ForeColor", "MyText",
            "MyScrollBars","Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
       typeof(int), typeof(InputTextBox), new PropertyMetadata(0));
        private int _Transparent = 0;
        [DefaultValue(""), Description("透明"), Category("杂项")]
        public int Transparent
        {
            get { return _Transparent; }
            set
            {
                _Transparent = value;
                PaintBackground();
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }

        private static readonly DependencyProperty BackColorProperty =
            DependencyProperty.Register("BackColor",
            typeof(Color), typeof(InputTextBox), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));
        [DefaultValue(""), Description("背影色"), Category("外观")]
        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set { this.SetValue(BackColorProperty, value); SetAttrByName("BackColor", value); }
        }

        private static void BackColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            InputTextBox InputTextBox = (InputTextBox)element;
            InputTextBox.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(InputTextBox), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));
        [DefaultValue(""), Description("前景色"), Category("外观")]
        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set { this.SetValue(ForeColorProperty, value); SetAttrByName("ForeColor", value); }
        }

        private static void ForeColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            InputTextBox InputTextBox = (InputTextBox)element;
            InputTextBox.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
            SetForeground();
        }

        public static readonly DependencyProperty MyTextProperty =
            DependencyProperty.Register("MyText", typeof(string), typeof(InputTextBox), new PropertyMetadata(string.Empty));
        [DefaultValue(""), Description("文本内容"), Category("杂项")]
        public string MyText
        {
            get { return (string)_textBox.GetValue(TextBox.TextProperty); }
            set { _textBox.SetValue(TextBox.TextProperty, value); SetAttrByName("MyText", value); }
        }

        public static readonly DependencyProperty MyScrollBarsProperty =
            DependencyProperty.Register("MyScrollBars", typeof(string), typeof(InputTextBox), new PropertyMetadata("1", new PropertyChangedCallback(MyScrollBars_Changed)));

        [DefaultValue("1"), Description("1为无,2为垂直滚动条,3为水平滚动条,4为全部"), Category("我的属性")]
        public string MyScrollBars
        {
            get { return (string)this.GetValue(MyScrollBarsProperty); }
            set { this.SetValue(MyScrollBarsProperty, value); SetAttrByName("MyScrollBars", value); }
        }

        private static void MyScrollBars_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            InputTextBox InputTextBox = (InputTextBox)element;
            InputTextBox.OnMyScrollBarsChanged((string)e.OldValue, (string)e.NewValue);
        }

        public void OnMyScrollBarsChanged(string oldValue, string newValue)
        {
            SetScrollBar(newValue);
        }
        #endregion


        private Grid _grid = new Grid();
        private TextBox _textBox = new TextBox();
        private Image _moveImage = new Image() { Height = 32d, Width = 32d, Source = new BitmapImage(new Uri("/WPFMonitor.Library;component/Images/ControlsImg/can_move.png", UriKind.RelativeOrAbsolute)), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(3d), Cursor = Cursors.Hand };

        public InputTextBox()
        {
            this.Content = _grid;
            _grid.Children.Add(_textBox);

            SetForeground();

            PaintBackground();

            SetScrollBar(MyScrollBars);

            _textBox.GotFocus += (o, e) =>
            {
                if (null != AdornerLayer)
                {
                    AdornerLayer.IsSelected = true; AdornerLayer.OnSelected();
                }
                _textBox.TextChanged -= Text_Changed;
                _textBox.TextChanged += Text_Changed;
            };
            _textBox.LostFocus += (o, e) =>
            {
                _textBox.TextChanged -= Text_Changed;
            };
        }

        private void Text_Changed(object sender, TextChangedEventArgs e)
        {
            if (null != AdornerLayer)
            {
                AdornerLayer.OnSelected();
            }
            //SetAttrByName("MyText", MyText);
            MyText = this._textBox.Text;
        }

        private void SetForeground()
        {
            _textBox.Foreground = new SolidColorBrush(ForeColor);
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                _textBox.Background = new SolidColorBrush();
                _textBox.BorderBrush = new SolidColorBrush();
            }
            else
            {
                _textBox.Background = new SolidColorBrush(BackColor);
                
            }
        }

        public void SetScrollBar(string value)
        {
            if (value.ToUpper().Trim() == "3")
            {
                _textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                _textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            }
            else if (value.ToUpper().Trim() == "2")
            {
                _textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                _textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else if (value.ToUpper().Trim() == "4")
            {
                _textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
                _textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            }
            else if (value.ToUpper().Trim() == "1")
            {
                _textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                _textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }
    }
}
