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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 24	DrawLine	2	LeakageMonitor.jpg	组态控件	漏水绳
    /// </summary>
    public class DrawLine : MonitorControl
    {
        Canvas _Canv = new Canvas();
        public DrawLine()
        {
            this.Content = _Canv;

            this.Height = _Canv.Height = 20;
            this.Width = _Canv.Width = 150;

            this.SizeChanged += new SizeChangedEventHandler(DrawLine_SizeChanged);
        }

        private void DrawLine_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _Canv.Width = e.NewSize.Width;
            _Canv.Height = e.NewSize.Height;
            RectangleGeometry r = new RectangleGeometry();

            Rect rect = new Rect();
            rect.Width = e.NewSize.Width;
            rect.Height = e.NewSize.Height;
            r.Rect = rect;
            _Canv.Clip = r;
            DrawLine_Paint();
        }

        #region 属性设置
        SetSinglePropertyDrawLine tpp = new SetSinglePropertyDrawLine();
        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            tpp = new SetSinglePropertyDrawLine();
            if (ScreenElement != null)
            {
                tpp.Closing += tpp_Closing;
                tpp.DeviceID = this.ScreenElement.DeviceID.Value;
                tpp.ChanncelID = this.ScreenElement.ChannelNo.Value;
                tpp.LevelNo = this.ScreenElement.LevelNo.Value;
                tpp.ComputeStr = this.ScreenElement.ComputeStr;

                tpp.Method = this.ScreenElement.Method;
                tpp.MinFloat = this.ScreenElement.MinFloat.Value;
                tpp.MaxFloat = this.ScreenElement.MaxFloat.Value;
            }
            tpp.Init();
            tpp.Show();
        }

        protected void tpp_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tpp.IsOK && ScreenElement != null)
            {
                this.ScreenElement.DeviceID = tpp.DeviceID;
                this.ScreenElement.ChannelNo = tpp.ChanncelID;
                this.ScreenElement.LevelNo = tpp.LevelNo;
                this.ScreenElement.ComputeStr = tpp.ComputeStr;

                this.ScreenElement.Method = tpp.Method;
                this.ScreenElement.MinFloat = tpp.MinFloat;
                this.ScreenElement.MaxFloat = tpp.MaxFloat;
            }
        }

        #endregion

        #region 控件公共属性
        public override event EventHandler Selected;
		
		public override event EventHandler Unselected;

		private void OnUnselected(object sender, EventArgs e)
		{
			if(null != Unselected)
			{
				Unselected(this, RoutedEventArgs.Empty);
			}
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

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize",
            "Transparent","Translate","LeftOrNot","GoodOrNot","LineWidths","Beeline","EdgeSize","EdgeArray","IsRightDirect","BackColor"};

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        public override void SetCommonPropertyValue()
        {
            if (ScreenElement != null)
            {
                this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
                this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
                Transparent = ScreenElement.Transparent.Value;

                _BackColor = Common1.StringToColor(ScreenElement.BackColor);

                this.Width = _Canv.Width = (double)ScreenElement.Width;
                this.Height = _Canv.Height = (double)ScreenElement.Height;
            }
        }

        public List<t_ElementProperty> GetProperty()
        {
            return ListElementProp;
        }

        public override FrameworkElement GetRootControl()
        {
            return this;
        }

        #endregion

        #region 属性
        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;

                if (name == "LeftOrNot".ToUpper())
                {
                    leftOrNot = Common1.ConvertToBool(value);
                }
                else if (name == "GoodOrNot".ToUpper())
                {
                    goodOrNot = Common1.ConvertToBool(value);
                }
                else if (name == "LineWidths".ToUpper())
                {
                    lineWidths = int.Parse(value);
                }
                if (name == "Beeline".ToUpper())
                {
                    beeline = Common1.ConvertToBool(value);
                }
                else if (name == "EdgeSize".ToUpper())
                {
                    edgeSize = int.Parse(value);
                }
                else if (name == "EdgeArray".ToUpper())
                {
                    edgeArray = value;
                }

                else if (name == "IsRightDirect".ToUpper())
                {
                    isRightDirect = Common1.ConvertToBool(value);
                }
            }

            DrawLine_Paint();

        }

        private static readonly DependencyProperty TransparentProperty =
         DependencyProperty.Register("Transparent",
         typeof(int), typeof(DrawLine), new PropertyMetadata(0));
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
                    _Canv.Background = new SolidColorBrush();
                }
                else
                {
                    _Canv.Background = new SolidColorBrush(_BackColor);
                }
                if (ScreenElement != null)
                    ScreenElement.Transparent = value;
            }
        }
        #endregion

        public override void SetChannelValue(float fValue, float dValue)
        {
            if (fValue == 0)
            {
                goodOrNot = false;
                m_innerChangeStatus++;
                if (m_innerChangeStatus > 3)
                    m_innerChangeStatus = 1;
                digitalValue = fValue;
            }
            else
            {
                goodOrNot = true;
                m_innerChangeStatus++;
                if (m_innerChangeStatus > 3)
                    m_innerChangeStatus = 1;
                digitalValue = fValue;
            }
            DrawLine_Paint();
        }

        public int m_innerChangeStatus = 0;

        //2011-3-14 xxy增加
        public float digitalValue = 0f;//记录表盘的值，即当前漏水的具体位置
        public int m_flashStyle = 0;
        /// <summary>
        /// 起点是不是左边开始
        /// </summary>
        private bool leftOrNot;
        public bool LeftOrNot
        {
            get { return leftOrNot; }
            set
            {
                leftOrNot = value;
                SetAttrByName("LeftOrNot", value.ToString());
                DrawLine_Paint();
            }
        }
        private bool goodOrNot;
        public bool GoodOrNot
        {
            get { return goodOrNot; }
            set
            {
                goodOrNot = value;
                SetAttrByName("GoodOrNot", value.ToString());
                DrawLine_Paint();
            }
        }

        private static readonly DependencyProperty BackColorProperty = DependencyProperty.Register("BackColor", typeof(int), typeof(MyLine), new PropertyMetadata(0));
        private Color _BackColor = Colors.Black;
        [DefaultValue(""), Description("背景色"), Category("外观")]
        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                if (ScreenElement != null)
                    ScreenElement.BackColor = value.ToString();
            }
        }


        // 2009-1-17
        private bool beeline;
        // 边的粗细
        [DefaultValue("3"), Description("边的流水状态"), Category("我的属性")]
        private int edgeSize = 3;
        public int EdgeSize
        {
            get { return edgeSize; }
            set
            {
                edgeSize = value;
                SetAttrByName("EdgeSize", value.ToString());
                DrawLine_Paint();
            }
        }
        [DefaultValue("10"), Description("设置漏水绳的高"), Category("我的属性")]
        private int lineWidths = 10;

        public int LineWidths
        {
            get { return lineWidths; }
            set
            {
                lineWidths = value;
                SetAttrByName("LineWidths", value.ToString());
                DrawLine_Paint();
            }
        }

        // 边数
        [DefaultValue("3,4"), Description("绳的画法"), Category("我的属性")]
        private string edgeArray = "1,2,3,4";
        public string EdgeArray
        {
            get { return edgeArray; }
            set
            {
                edgeArray = value;
                SetAttrByName("EdgeArray", value.ToString());
                DrawLine_Paint();
            }
        }

        // 2009-6-29
        bool isRightDirect=false;  // 正相还是反相？若是正相，则1用红色表示，否则用绿色表示。
        /// <summary>
        /// 正相还是反相 // 2009-6-29
        /// </summary>
        [DefaultValue("true"), Description("正相与反相的确定.正相时1为蓝色"), Category("我的属性")]
        public bool IsRightDirect
        {
            get { return isRightDirect; }
            set
            {
                isRightDirect = value;
                SetAttrByName("IsRightDirect", value.ToString());
                DrawLine_Paint();
            }
        }

        private void DrawLine_Paint()
        {
            _Canv.Children.Clear();

            if (_Transparent == 1)
            {
                _Canv.Background = new SolidColorBrush();
            }
            else
            {
                _Canv.Background = new SolidColorBrush(_BackColor);
            }

            string gbUrl = "/MonitorSystem.ZTControls;component/ZTControls/Near2.jpg";
            BitmapImage bitmap = new BitmapImage(new Uri(gbUrl, UriKind.Relative));
            ImageBrush near = new ImageBrush();
            near.ImageSource = bitmap;


            beeline = false;

            //位置0
            double ZerroPosi = 0;
            Rectangle rect = new Rectangle();
            rect.Width = lineWidths;
            rect.Height = lineWidths;
            rect.SetValue(Canvas.LeftProperty, ZerroPosi);
            rect.SetValue(Canvas.TopProperty, ZerroPosi);
            rect.SetValue(Canvas.ZIndexProperty, 500);
            _Canv.Children.Add(rect);

            Rectangle rect2 = new Rectangle();
            //rect2.Y = this.Height - lineWidths;
            rect2.Width = lineWidths;
            //rect2.X = 0;
            rect2.Height = lineWidths;
            rect2.SetValue(Canvas.LeftProperty, ZerroPosi);
            rect2.SetValue(Canvas.TopProperty, this.Height - lineWidths);
            rect2.SetValue(Canvas.ZIndexProperty, 500);
            _Canv.Children.Add(rect2);

            Rectangle rect3 = new Rectangle();
            rect3.Width = lineWidths;
            rect3.Height = lineWidths;
            rect3.SetValue(Canvas.LeftProperty, this.Width - lineWidths);
            rect3.SetValue(Canvas.TopProperty, ZerroPosi);
            rect3.SetValue(Canvas.ZIndexProperty, 500);
            _Canv.Children.Add(rect3);

            Rectangle rect4 = new Rectangle();
            rect4.Width = lineWidths;
            rect4.Height = lineWidths;
            rect4.SetValue(Canvas.LeftProperty, this.Width - lineWidths);
            rect4.SetValue(Canvas.TopProperty, this.Height - lineWidths);
            rect4.SetValue(Canvas.ZIndexProperty, 500);
            _Canv.Children.Add(rect4);

            if (beeline == false)
            {
                // 2009-1-17
                string[] allStr = edgeArray.Split(',');
                int lineSize = edgeSize < this.Width ? (int)edgeSize : (int)this.Width;
                Brush myBrush = new SolidColorBrush(Colors.Green);// Brushes.Green;
                if (!goodOrNot)
                {
                    // 2009-7-17取消闪动
                    m_flashStyle = 0;
                    // 2009-7-6
                    if (m_flashStyle == 0)
                    {
                        m_flashStyle = 1;

                        if (isRightDirect)
                            myBrush = new SolidColorBrush(Colors.Red);// Brushes.Red;
                        else
                            myBrush = new SolidColorBrush(Colors.Green);// Brushes.Green;
                    }
                    else
                    {
                        m_flashStyle = 0;
                        myBrush = new SolidColorBrush(Colors.Yellow);// Brushes.Yellow;
                    }
                }
                else
                {
                    if (!isRightDirect)
                    {
                        if (ScreenElement.Method == 0)
                            myBrush = new SolidColorBrush(Colors.Red);//Brushes.Red;
                        else if (ScreenElement.Method == 1 && ScreenElement.MinFloat < digitalValue && digitalValue <= ScreenElement.MaxFloat)
                            myBrush = new SolidColorBrush(Colors.Red);//Brushes.Red;
                        else
                            myBrush = new SolidColorBrush(Colors.Green);//Brushes.Green;
                    }
                    else
                        myBrush = new SolidColorBrush(Colors.Blue);//Brushes.Blue;
                }

                //此处可修改myBrush的相关属性
                //Pen tempPen = new Pen(Brushes.Red, 4);
                //Pen tempPen = new Pen(myBrush, lineWidths);
                foreach (string s in allStr)
                {
                    //循环画的计数
                    int count;
                    //小边的每段长度
                    int lineWidth = 5;
                    //小边的流水线初始位置
                    int i = 10;
                    //间隔画小边的标志位
                    bool flag = true;
                    if (s == "1")
                    {
                        count = 0;
                        if (m_innerChangeStatus == 1)
                        {
                            //小边的流水线初始位置，三种状态下各有不同的初始位置，为配合整个矩形的情况
                            i = 0;
                        }
                        if (m_innerChangeStatus == 2)
                        {
                            i = (int)((this.Width / lineWidth) / 2);
                        }
                        if (m_innerChangeStatus == 3)
                        {
                            i = (int)(this.Width / lineWidth);
                        }
                        rect2.Fill = near;
                        for (int j = 0; count < ((this.Width / lineWidth) - 3); count++)
                        {
                            j = i;
                            i += lineWidth;
                            if (flag)
                            {
                                if (i > this.Width)
                                    break;
                                Line li = new Line();
                                li.X1 = j + 10;
                                li.X2 = (double)i;
                                li.Y1 = li.Y2 = lineWidths / 2;
                                li.Stroke = myBrush;
                                li.StrokeThickness = lineWidths;
                                _Canv.Children.Add(li);
                                flag = false;
                            }
                            else flag = true;
                        }

                    }
                    else if (s == "2")
                    {
                        count = 0;
                        if (m_innerChangeStatus == 1)
                        {
                            i = (int)(this.Width / lineWidth);
                        }
                        if (m_innerChangeStatus == 2)
                        {
                            i = (int)((this.Width / lineWidth) / 2);
                        }
                        if (m_innerChangeStatus == 3)
                        {
                            i = 0;
                        }
                        rect4.Fill = near;
                        for (int j = 0; count < (this.Height / lineWidth) - 3; count++)
                        {
                            j = i;
                            i += lineWidth;
                            if (flag)
                            {
                                if (i > this.Height)
                                    break;
                                Line li = new Line();
                                li.Y1 = (double)j;
                                li.X1 = li.X2 = lineWidths / 2;
                                li.Y2 = (double)i;
                                li.Stroke = myBrush;
                                li.StrokeThickness = lineWidths;
                                _Canv.Children.Add(li);
                                flag = false;
                            }
                            else flag = true;
                        }

                    }
                    else if (s == "3")
                    {
                        count = 0;
                        if (m_innerChangeStatus == 1)
                        {
                            i = (int)(this.Width / lineWidth) / 2;

                        }
                        if (m_innerChangeStatus == 2)
                        {
                            i = (int)(this.Width / lineWidth);
                        }
                        if (m_innerChangeStatus == 3)
                        {
                            i = 0;
                        }
                        rect.Fill = near;
                        for (int j = 0; count < (this.Height / lineWidth) - 3; count++)
                        {
                            j = i;
                            i += lineWidth;
                            if (flag)
                            {
                                if (i > this.Height)
                                    break;

                                Line li = new Line();
                                //li.X1 = this.Width;
                                li.Y1 = (double)j;
                                li.X1 = li.X2 = this.Width - (lineWidths / 2);
                                li.Y2 = (double)i;
                                li.Stroke = myBrush;
                                li.StrokeThickness = lineWidths;
                                _Canv.Children.Add(li);
                                flag = false;
                            }
                            else flag = true;
                        }


                    }
                    else if (s == "4")//
                    {
                        count = 0;
                        if (m_innerChangeStatus == 1)
                        {
                            i = 0;
                        }
                        if (m_innerChangeStatus == 2)
                        {
                            i = (int)((this.Width / lineWidth) / 2);
                        }
                        if (m_innerChangeStatus == 3)
                        {
                            i = (int)(this.Width / lineWidth);
                        }
                        rect3.Fill = near;
                        for (int j = 0; count < ((this.Width / lineWidth) - 4); count++)
                        {
                            j = i;
                            i += lineWidth;
                            if (flag)
                            {
                                if (i > this.Width)
                                    break;
                                Line li = new Line();
                                li.X1 = (double)(j + 10);
                                //li.Y1 = this.Height;
                                li.X2 = (double)i;
                                li.Y1 = li.Y2 = this.Height - (lineWidths / 2);
                                li.Stroke = myBrush;
                                li.StrokeThickness = lineWidths;
                                //ToolTipService.SetToolTip(li, string.Format("44x1={0},y1={1},x2={2},y2={3}", li.X1, li.Y1, li.X2, li.Y2));
                                _Canv.Children.Add(li);

                                flag = false;
                            }
                            else flag = true;
                        }//for
                    }//4
                }
            }
        }

    }
}
