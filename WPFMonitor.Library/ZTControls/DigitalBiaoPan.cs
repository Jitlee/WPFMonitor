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

namespace WPFMonitor.Library.ZTControls
{
    /// <summary>
    /// 7	DigitalBiaoPan	2	Digital.jpg	组态控件	数字表盘
    /// </summary>
    public class DigitalBiaoPan : MonitorControl
    {
        public override void SetChannelValue(float fValue)
        {
            if (MyScale == null || MyScale == "")
                MyScale = "1";
            float temp = float.Parse(MyScale);
            if (temp <= 0)
                temp = 1;
            fValue = (float)(fValue / temp);

            MyNum = Convert.ToString(fValue);
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
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;

                if (name == "MyNum".ToUpper())
                {
                    MyNum = value;
                }
                else if (name == "RadixLen".ToUpper())
                {
                    RadixLen = int.Parse(value);
                }
                else if (name == "IntLen".ToUpper())
                {
                    IntLen = int.Parse(value);
                }
                else if (name == "MyScale".ToUpper())
                {
                    MyScale = value;
                }
            }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            this.Width = (double)ScreenElement.Width;
            this.Height = (double)ScreenElement.Height;
            Transparent = ScreenElement.Transparent.Value;
            ForeColor = Common.StringToColor(ScreenElement.ForeColor);
            BackColor = Common.StringToColor(ScreenElement.BackColor); 
        }

        private string[] _browsableProperties = new[] { "Width", "Height", "Left", "Top", "FontFamily", "FontSize",
            "BackColor", "ForeColor", "RadixLen", "IntLen", "MyNum","Transparent" };
        public override string[] BrowsableProperties
        {
            get { return _browsableProperties; }
            set { _browsableProperties = value; }
        }

        #region 属性
        private static readonly DependencyProperty TransparentProperty =
          DependencyProperty.Register("Transparent",
          typeof(int), typeof(DigitalBiaoPan), new PropertyMetadata(0));
        private int _Transparent;
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
            typeof(Color), typeof(DigitalBiaoPan), new PropertyMetadata(Colors.White, new PropertyChangedCallback(BackColor_Changed)));

        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set { this.SetValue(BackColorProperty, value);
            
            }
        }

        private static void BackColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DigitalBiaoPan DigitalBiaoPan = (DigitalBiaoPan)element;
            DigitalBiaoPan.OnBackColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnBackColorChanged(Color oldValue, Color newValue)
        {
            PaintBackground();
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(DigitalBiaoPan), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(ForeColor_Changed)));

        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set { this.SetValue(ForeColorProperty, value); 
                if (ScreenElement != null)
                ScreenElement.ForeColor = value.ToString();
            }
        }

        private static void ForeColor_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DigitalBiaoPan DigitalBiaoPan = (DigitalBiaoPan)element;
            DigitalBiaoPan.OnForeColorChanged((Color)e.NewValue, (Color)e.OldValue);
        }

        public void OnForeColorChanged(Color oldValue, Color newValue)
        {
            _path.Fill = new SolidColorBrush(ForeColor);
        }

        private static readonly DependencyProperty MyNumProperty =  DependencyProperty.Register("MyNum",
            typeof(string), typeof(DigitalBiaoPan), new PropertyMetadata("136.23", new PropertyChangedCallback(MyNum_Changed)));

        [DefaultValue("136.23"), Description("需要显示的数字"), Category("我的属性")]
        public string MyNum
        {
            get { return (string)this.GetValue(MyNumProperty); }
            set { this.SetValue(MyNumProperty, value);
            SetAttrByName("MyNum", value);
            }
        }

        private static void MyNum_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DigitalBiaoPan DigitalBiaoPan = (DigitalBiaoPan)element;
            DigitalBiaoPan.OnMyNumChanged((string)e.NewValue, (string)e.OldValue);
        }

        public void OnMyNumChanged(string oldValue, string newValue)
        {
            Paint(DesiredSize);
        }

        private static readonly DependencyProperty RadixLenProperty = DependencyProperty.Register("RadixLen",
           typeof(int), typeof(DigitalBiaoPan), new PropertyMetadata(2, new PropertyChangedCallback(RadixLen_Changed)));

        [DefaultValue("2"), Description("小数长度"), Category("我的属性")]
        public int RadixLen
        {
            get { return (int)this.GetValue(RadixLenProperty); }
            set { this.SetValue(RadixLenProperty, value);
                    SetAttrByName("RadixLen", value);
            }
        }

        private static void RadixLen_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DigitalBiaoPan DigitalBiaoPan = (DigitalBiaoPan)element;
            DigitalBiaoPan.OnRadixLenChanged((int)e.NewValue, (int)e.OldValue);
        }

        public void OnRadixLenChanged(int oldValue, int newValue)
        {
            Paint(DesiredSize);
        }

        private static readonly DependencyProperty IntLenProperty = DependencyProperty.Register("IntLen",
           typeof(int), typeof(DigitalBiaoPan), new PropertyMetadata(3, new PropertyChangedCallback(IntLen_Changed)));

        [DefaultValue("3"), Description("整数长度"), Category("我的属性")]
        public int IntLen
        {
            get { return (int)this.GetValue(IntLenProperty); }
            set { this.SetValue(IntLenProperty, value);
                    SetAttrByName("IntLen", value);
            }
        }

        private static void IntLen_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DigitalBiaoPan DigitalBiaoPan = (DigitalBiaoPan)element;
            DigitalBiaoPan.OnIntLenChanged((int)e.NewValue, (int)e.OldValue);
        }

        public void OnIntLenChanged(int oldValue, int newValue)
        {
            Paint(DesiredSize);
        }

        private static readonly DependencyProperty MyScaleProperty = DependencyProperty.Register("MyScale",
            typeof(String), typeof(DigitalBiaoPan), new PropertyMetadata("1", new PropertyChangedCallback(MyScale_Changed)));

        private string myScale;
        [DefaultValue("1"), Description("显示比例"), Category("我的属性")]
        public string MyScale
        {
            get { return myScale; }
            set
            {
                myScale = value;
                SetAttrByName("MyScale", value);
            }
        }
        //[DefaultValue("1"), Description("显示比例"), Category("我的属性")]
        //public double MyScale
        //{
        //    get { return (double)this.GetValue(MyScaleProperty); }
        //    set { this.SetValue(MyScaleProperty, value);
            
        //    }
        //}

        private static void MyScale_Changed(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            DigitalBiaoPan DigitalBiaoPan = (DigitalBiaoPan)element;
            DigitalBiaoPan.OnMyScaleChanged((double)e.NewValue, (double)e.OldValue);
        }

        public void OnMyScaleChanged(double oldValue, double newValue)
        {
            
        }

        #endregion

        private Canvas _canvas = new Canvas();
        private Path _path = new Path();
        private PathGeometry _pathGeometry = new PathGeometry();

        public DigitalBiaoPan()
        {
            this.Content = _canvas;
            _canvas.Children.Add(_path);
            _path.Data = _pathGeometry;
            _path.Fill = new SolidColorBrush(ForeColor);

            PaintBackground();
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                _canvas.Background = new SolidColorBrush();
            }
            else
            {
                _canvas.Background = new SolidColorBrush(BackColor);
            }
        }

        private void Paint(Size finalSize)
        {
            var myNum = MyNum;
            var m_radixLen = RadixLen;
            var m_intLen = IntLen;
            if (myNum == null || myNum.Trim() == "")
                myNum = "0";

            string[] myNums = myNum.Split('.');
            string strInt = "";
            string strRadix = "";

            string formatNum;

            if (myNums.Length == 1)
            {
                // strRadix=String.Format("%
                strInt = myNums[0];
                System.Console.WriteLine(myNums[0]);
                for (int i = 0; i < m_radixLen; i++) strRadix += "0";


                if (strInt.Length > m_intLen)
                    strInt = strInt.Substring(strInt.Length - m_intLen, m_intLen);
                else
                {
                    int k = m_intLen - strInt.Length;
                    // 2009-8-17:去掉前面的0
                    //for (int i = 0; i < k; i++) 
                    //    strInt = "0" + strInt;
                }
                formatNum = strInt + "." + strRadix;

            }
            else
            {
                System.Console.WriteLine(myNums[0] + "---" + myNums[1]);
                strInt = myNums[0];
                strRadix = myNums[1];
                if (strInt.Length > m_intLen)
                    strInt = strInt.Substring(strInt.Length - m_intLen, m_intLen);
                else
                {
                    int k = m_intLen - strInt.Length;
                    // 2009-8-17:去掉前面的0
                    //for (int i = 0; i < k; i++) 
                    //    strInt = "0" + strInt;
                }


                if (strRadix.Length > m_radixLen) strRadix = strRadix.Substring(0, m_radixLen);
                else
                {
                    int k = m_radixLen - strRadix.Length;
                    for (int i = 0; i < k; i++) strRadix = strRadix + "0";
                }

                formatNum = strInt + "." + strRadix;
            }

            //float fScale = Math.Min(ClientSize.Width / sizef.Width, ClientSize.Height / sizef.Height);
            //sizef = ssd.MeasureString(formatNum, font);
            //SolidBrush sbrush1 = new SolidBrush(this.ForeColor);
            //ssd.DrawString(formatNum, font, sbrush1, (ClientSize.Width - sizef.Width) / 2, (ClientSize.Height - sizef.Height) / 2);

            var ssd = new BeautyFont(_pathGeometry);
            //var text = new TextBlock();
            //text.Text = "8";
            //text.FontSize = 10.0d;
            var sizef = ssd.MeasureString(formatNum, 12d * 0.5d);
            var fScale = Math.Min(finalSize.Width / sizef.X, finalSize.Height / sizef.Y) * 0.75d;
            sizef.X *= fScale;
            sizef.Y *= fScale;
            ssd.DrawString(formatNum, sizef.Y, (finalSize.Width - sizef.X * 1.45d) / 2d, (finalSize.Height - sizef.Y * 1.35d) / 2d);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Paint(availableSize);
            return base.MeasureOverride(availableSize);
        }
    }
}
