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
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.ZTControls
{
    /// <summary>
    /// 3	MyLine	2	Line.jpg	组态控件	曲线
    /// </summary>
    public class MyLine : MonitorControl
    {
        Canvas picCurveShow = new Canvas();
        public MyLine()
        {
            this.Content = picCurveShow;
            this.Width = 300;
            this.Height = 400;
            
            this.SizeChanged += new SizeChangedEventHandler(MyLine_SizeChanged);

            //MyLine_Paint();
        }
        private void MyLine_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            picCurveShow.Width = e.NewSize.Width;
            picCurveShow.Height = e.NewSize.Height;
            MyLine_Paint();
            DrawLine();
        }


        
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

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize","ForeColor","BackColor", "Transparent", 
            "MyData","Title","SetXTitle","Range","SetYTitle","SetMinValue","SetMaxValue","DataZone","DottedLine","LineColor"};

        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }

        public override void SetCommonPropertyValue()
        {
            this.SetValue(Canvas.LeftProperty, (double)ScreenElement.ScreenX);
            this.SetValue(Canvas.TopProperty, (double)ScreenElement.ScreenY);
            Transparent = ScreenElement.Transparent.Value;
            picCurveShow.Width = this.Width = (double)ScreenElement.Width;
            picCurveShow.Height = this.Height = (double)ScreenElement.Height;
            _ForeColor = Common1.StringToColor(ScreenElement.ForeColor);
            _BackColor = Common1.StringToColor(ScreenElement.BackColor);
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
        public override void SetPropertyValue()
        {
            foreach (t_ElementProperty pro in ListElementProp)
            {
                string name = pro.PropertyName.ToUpper();
                string value = pro.PropertyValue;
                if (name == "SetXTitle".ToUpper())
                {
                    xTitle = value;
                }
                else if (name == "SetYTitle".ToUpper())
                {
                    yTitle = value;
                }
                else if (name == "SetMinValue".ToUpper())
                {
                    minValue = float.Parse(value);
                }
                else if (name == "SetMaxValue".ToUpper())
                {
                    maxValue = float.Parse(value);
                }
                else if (name == "DataZone".ToUpper())
                {
                    _dataZone= Common1.StringToColor(value);
                }
                else if (name == "DottedLine".ToUpper())
                {
                    dottedLine = Common1.ConvertToBool(value);
                }
                else if (name == "LineColor".ToUpper())
                {
                    _lineColor = Common1.StringToColor(value);
                }
                else if (name == "Range".ToUpper())
                {
                    range = int.Parse(value);
                }
            }
            DrawLine();
        }
        #region 属性
        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
         typeof(int), typeof(MyLine), new PropertyMetadata(0));
        private int _Transparent=0;
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

        private static readonly DependencyProperty BackColorProperty = DependencyProperty.Register("BackColor",
    typeof(int), typeof(MyLine), new PropertyMetadata(0));
        private Color _BackColor = Colors.Black;
        [DefaultValue(""), Description("背景色"),Category("外观")]
        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                PaintBackground();
                if (ScreenElement != null)
                    ScreenElement.BackColor = value.ToString(); 
            }
        }

        private static readonly DependencyProperty ForeColorProperty = DependencyProperty.Register("ForeColor",
    typeof(int), typeof(MyLine), new PropertyMetadata(0));
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
            }
        }
        private static readonly DependencyProperty DataZoneProperty = DependencyProperty.Register("DataZone",
        typeof(Color), typeof(MyLine), new PropertyMetadata(Colors.White));
        private Color _dataZone = Colors.White;
        [DefaultValue(""), Description("数据区域颜色"), Category("我的属性")]
        public Color DataZone
        {
            get { return _dataZone; }
            set
            {
                _dataZone = value;
                SetAttrByName("DataZone", value.ToString());
                MyLine_Paint();
            }
        }

        private static readonly DependencyProperty LineColorProperty = DependencyProperty.Register("LineColor",
       typeof(Color), typeof(MyLine), new PropertyMetadata(Colors.White));
        private Color _lineColor;
        [DefaultValue(""), Description("网格线的颜色"), Category("我的属性")]
        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                _lineColor = value;
                SetAttrByName("LineColor", value.ToString());
                MyLine_Paint();
            }
        }

        private static readonly DependencyProperty SetYTitleProperty = DependencyProperty.Register("SetYTitle",
       typeof(string), typeof(MyLine), new PropertyMetadata(""));
       private string yTitle = "纵标题";
        [DefaultValue(""), Description("曲线的小标题"), Category("我的属性")]
        public string SetYTitle
        {
            get { return yTitle; }
            set
            {
                yTitle = value;
                SetAttrByName("SetYTitle", value.ToString());
                MyLine_Paint();
                MyLine_Paint();
            }
        }

      //  private static readonly DependencyProperty SetMinValueProperty = DependencyProperty.Register("SetMinValue",
      //typeof(float), typeof(MyLine), new PropertyMetadata(0));
        private float minValue = 0;
        [DefaultValue(""), Description("纵坐标默认最小值"), Category("我的属性")]
        public float SetMinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                SetAttrByName("SetMinValue", value.ToString());
                MyLine_Paint();
            }
        }

      //  private static readonly DependencyProperty SetMaxValueProperty = DependencyProperty.Register("SetMaxValue",
      //typeof(float), typeof(MyLine), new PropertyMetadata((double)100));
        private float maxValue = 100;
        [DefaultValue(""), Description("纵坐标默认最大值"), Category("我的属性")]
        public float SetMaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                SetAttrByName("SetMaxValue", value.ToString());
                MyLine_Paint();
            }
        }

        private static readonly DependencyProperty RangeProperty = DependencyProperty.Register("Range",
typeof(int), typeof(MyLine), new PropertyMetadata(3));
        private int range=3;
        [DefaultValue("3"), Description("渐变度(0-10)"), Category("我的属性")]
        public int Range
        {
            get { return range; }
            set
            {
                range = value;
                SetAttrByName("Range", value.ToString());
                MyLine_Paint();
            }
        }

        private static readonly DependencyProperty DottedLineProperty = DependencyProperty.Register("DottedLine",
typeof(bool), typeof(MyLine), new PropertyMetadata(true));
        private bool dottedLine=true;
        [DefaultValue("true"), Description("网格线是否为虚线"), Category("我的属性")]
        public bool DottedLine
        {
            get { return dottedLine; }
            set
            {
                dottedLine = value;
                SetAttrByName("DottedLine", value.ToString());
                MyLine_Paint();
            }
        }

        private static readonly DependencyProperty SetXTitleProperty = DependencyProperty.Register("SetXTitle",
typeof(string), typeof(MyLine), new PropertyMetadata("横坐标"));
        private string xTitle = "横坐标";
        [DefaultValue("横坐标"), Description("曲线的横坐标名称"), Category("我的属性")]
        public string SetXTitle
        {
            get { return xTitle; }
            set
            {
                xTitle = value;
                SetAttrByName("SetXTitle", value.ToString());
                MyLine_Paint();
            }
        }
        #endregion
        /// <summary>
        /// 记录X轴的值
        /// </summary>
        private float myXValue;
        /// <summary>
        /// 第一格附近有多少个点
        /// </summary>
        private int countInFirst = 0;
        /// <summary>
        /// 当前已经有多少个点
        /// </summary>
        private int nowPointCount = 0;
        /// <summary>
        /// 数据列表
        /// </summary>
        List<RowItem> myData = new List<RowItem>();
        /// <summary>
        /// 曲线可以表示的最多点数
        /// </summary>
        private int pointCount = 300;

        public void SetPointCount(int count)
        {
            pointCount = count;
        }

        public void SetMyPoint(double xValue, double yValue)
        {
            if (myData != null && myData.Count >= pointCount)
            {
                myData.RemoveAt(0); 
            }
            RowItem myRow =new RowItem();
            myRow.X = xValue;
            myRow.Y = yValue;
            myData.Add(myRow);
            countInFirst++; 
            countInFirst = countInFirst % 60;
            if (myData != null)
            {
                nowPointCount = myData.Count;
            }
            DrawLine();
        }
        public override void SetChannelValue(float yValue)
        {
            SetMyPoint(myXValue, yValue);
            myXValue++;
            if (myXValue > 24) myXValue = 24;
        }

        public override void SetChannelValue(float xValue, float yValue)
        {
            SetMyPoint(xValue, yValue);
        }

        //用于设置只有一个坐标值的情况（该值作为纵坐标）
        public void SetMyPoint(double yValue)
        {
            if (myData != null && myData.Count >= pointCount)
            {
                myData.RemoveAt(0);
            }
            RowItem myRow = new RowItem();
            myRow.X = 0;
            myRow.Y = yValue;
            myData.Add(myRow);
            countInFirst++;
            countInFirst = countInFirst % 60;
            if (myData != null)
            {
                nowPointCount = myData.Count;
            }
            
        }

        public void ClearPoint()
        {
            if (this.myData != null && this.myData.Count != 0)
            {
                this.myData.Clear();
            }
        }
       
        /// <summary>
        /// 字体高度
        /// </summary>
        const int FontHeight=14;
        private void MyLine_Paint()
        {

            PaintBackground();

            picCurveShow.Children.Clear();
            if (maxValue - minValue >= -0.0000001 && maxValue - minValue <= 0.0000001)
            {
                maxValue = (int)maxValue + 50;
                minValue = maxValue - 100;
            }
            while (myData.Count > pointCount)
            {
                myData.RemoveAt(0);
            }

            //Graphics g = e.Graphics;
            //g.Clear(this.BackColor);
            if (range < 0)
            {
                range = 0;
            }
            else
                if (range > 10)
                {
                    range = 10;
                }
            Color BackColor=Colors.Red;
            Color end = BackColor;

            Rectangle mrect = new Rectangle();
            mrect.Fill = new SolidColorBrush(_dataZone);
            mrect.Width = this.Width - 50;
            mrect.Height = this.Height - 50;
            mrect.SetValue(Canvas.LeftProperty, (double)40);
            mrect.SetValue(Canvas.TopProperty, (double)10);
            mrect.SetValue(Canvas.ZIndexProperty, 0);
            picCurveShow.Children.Add(mrect);

            //Font myFont = new Font("宋体", 10);
            int len = ((xTitle.Trim().Length) * (FontHeight)) / 2;
            //g.DrawString(xTitle.Trim(), myFont, Brushes.Black, (Width - len) / 2, Height - myFont.Height);
            TextBlock txtXTitle = new TextBlock();
            txtXTitle.Text = xTitle.Trim();
            txtXTitle.Foreground = new SolidColorBrush(_ForeColor);

            txtXTitle.SetValue(Canvas.LeftProperty, (Width - len) / 2);
            txtXTitle.SetValue(Canvas.TopProperty, Height - FontHeight);
            txtXTitle.SetValue(Canvas.ZIndexProperty, 500);
            picCurveShow.Children.Add(txtXTitle);


            double currentY = (Height - yTitle.Trim().Length * FontHeight) / 2;
            for (int i = 0; i < yTitle.Trim().Length; i++)
            {
                //g.DrawString(yTitle[i].ToString(), myFont, Brushes.Black, 0, currentY);
                currentY += FontHeight;
                TextBlock txtyTitle = new TextBlock();
                txtyTitle.Text = yTitle[i].ToString();
                txtyTitle.Foreground = new SolidColorBrush(_ForeColor);

                txtyTitle.SetValue(Canvas.LeftProperty, (double)0);
                txtyTitle.SetValue(Canvas.TopProperty,currentY);
                txtyTitle.SetValue(Canvas.ZIndexProperty, 0);
                picCurveShow.Children.Add(txtyTitle);
            }
            

            //帽峰

            Line lixLine = new Line();
            lixLine.StrokeThickness = 1;
            //liGrid.StrokeDashArray = new DoubleCollection() { 2.0, 2.0 };
            lixLine.Stroke = new SolidColorBrush(_lineColor);
            lixLine.X1 = 40;
            lixLine.X2 = Width - 10;
            lixLine.Y1 = lixLine.Y2 = Height - 40;
            lixLine.SetValue(Canvas.ZIndexProperty, 1);
            picCurveShow.Children.Add(lixLine);

            Line liGrid1 = new Line();
            liGrid1.StrokeThickness = 1;
            //liGrid1.StrokeDashArray = new DoubleCollection() { 2.0, 2.0 };
            liGrid1.Stroke = new SolidColorBrush(_lineColor);
            liGrid1.X1 = 40;
            liGrid1.X2 = 40;
            liGrid1.Y1 = Height - 40;
            liGrid1.Y2 = 10;
            liGrid1.SetValue(Canvas.ZIndexProperty, 1);
            picCurveShow.Children.Add(liGrid1);

            //绘制时间轴的虚线
            for (int i = 0; i < 5; i++)
            {
                //g.DrawLine(myAsixPen, Width - 10 - (this.Width - 50) * (countInFirst + i * 60) / 300, Height - 40,
                //Width - 10 - (this.Width - 50) * (countInFirst + i * 60) / 300, 10);
                Line liAsixPen = new Line();
                liAsixPen.StrokeThickness = 0.5;
                if (dottedLine)
                    liAsixPen.StrokeDashArray = new DoubleCollection() { 3.0, 3.0 };
                liAsixPen.Stroke = new SolidColorBrush(_lineColor);
                liAsixPen.X2 = liAsixPen.X1 = Width - 10 - (this.Width - 50) * (countInFirst + i * 60) / 300;
                //liAsixPen.X2 = Width - 10 - (this.Width - 50) * (countInFirst + i * 60) / 300;
                liAsixPen.Y1 = Height - 40;
                liAsixPen.Y2 = 10;
                liAsixPen.SetValue(Canvas.ZIndexProperty, 1);
                picCurveShow.Children.Add(liAsixPen);

                
                DateTime myTime = DateTime.Now.AddSeconds(-countInFirst - 60 * i);//用作时间轴，即横坐标
                string xvalue = myTime.ToShortTimeString();
                //g.DrawString(xvalue, myFont, Brushes.Black, 
                //new PointF(Width - 10 - (this.Width - 50) * (countInFirst + i * 60) / 300 - xvalue.Trim().Length * myFont.Height / 2, this.Height - 38));
                TextBlock txtValue = new TextBlock();
                txtValue.Text = xvalue;
                txtValue.Foreground = new SolidColorBrush(_ForeColor);
                double x = Width - 10 - (this.Width - 50) * (countInFirst + i * 60) / 300 - xvalue.Trim().Length * FontHeight / 2;
                txtValue.SetValue(Canvas.LeftProperty, x);
                txtValue.SetValue(Canvas.TopProperty, this.Height - 38);
                txtValue.SetValue(Canvas.ZIndexProperty, 500);
                picCurveShow.Children.Add(txtValue);
            }

            double yvalue = minValue;
            double yPlace = Height - 40;

            TextBlock txtyvalue = new TextBlock();
            txtyvalue.Text = yvalue.ToString();
            txtyvalue.Foreground = new SolidColorBrush(_ForeColor);
           
            txtyvalue.SetValue(Canvas.LeftProperty, (double)15.0);
            txtyvalue.SetValue(Canvas.TopProperty, yPlace - FontHeight / 2);
            txtyvalue.SetValue(Canvas.ZIndexProperty, 500);
            picCurveShow.Children.Add(txtyvalue);

            for (int i = 1; i <= 10; i++)
            {
                yvalue = minValue + i * (maxValue - minValue) / 10;

                Line liAsixPen = new Line();
                liAsixPen.StrokeThickness = 0.5;
                if (dottedLine)
                    liAsixPen.StrokeDashArray = new DoubleCollection() { 3.0, 3.0 };
                liAsixPen.Stroke = new SolidColorBrush(_lineColor);
                liAsixPen.X1 = 40;
                liAsixPen.X2 = this.Width - 10;
                liAsixPen.Y1=liAsixPen.Y2 = Height - 40 - (this.Height - 50) * i / 10;

                liAsixPen.SetValue(Canvas.ZIndexProperty, 1);
                picCurveShow.Children.Add(liAsixPen);
                
                TextBlock txtValue = new TextBlock();
                txtValue.Text = yvalue.ToString();
                txtValue.Foreground = new SolidColorBrush(_ForeColor);
                txtValue.SetValue(Canvas.LeftProperty, (double)15.0);
                txtValue.SetValue(Canvas.TopProperty, yPlace - i * (Height - 60) / 10 - FontHeight / 2);
                txtValue.SetValue(Canvas.ZIndexProperty, 500);
                picCurveShow.Children.Add(txtValue);
            }
        }

        public void DrawLine()
        {
            if (myData != null && myData.Count > 0)
            {
                double caY = maxValue - minValue;
                Point[] pp = new Point[this.myData.Count];
                for (int i = 0; i < pp.Length; i++)
                {
                    pp[i].X = (int)(Width - 10 - (this.Width - 50) * (pp.Length - i) / 300);
                    pp[i].Y = (int)(Height - 40 - (Height - 50) * (Double.Parse(myData[i].Y.ToString()) - minValue) / caY);
                }
                if (myData != null && myData.Count == 1)
                {                    
                    Ellipse e = new Ellipse();
                    e.SetValue(Canvas.LeftProperty, pp[0].X - 2);
                    e.SetValue(Canvas.TopProperty, pp[0].Y - 2);
                    e.Width = 4;
                    e.Height = 4;
                    picCurveShow.Children.Add(e);
                }
                else
                    if (myData.Count > 1)
                    {                       
                        PointCollection pc = new PointCollection();
                        foreach (Point p in pp)
                        {
                            pc.Add(p);
                        }
                        Polyline pl = new Polyline();
                        pl.Stroke = new SolidColorBrush(Colors.Yellow);
                        pl.StrokeThickness = 2;
                        pl.Points = pc;
                        if (this.ScreenElement.ElementID != 0)
                        {
                            pl.Name = "ShowLinePolyline" + this.ScreenElement.ElementID;
                            var v = picCurveShow.FindName("ShowLinePolyline" + this.ScreenElement.ElementID);
                            picCurveShow.Children.Remove((Polyline)v);
                        }
                        picCurveShow.Children.Add(pl);
                    }
            }
        }

        private void PaintBackground()
        {
            if (_Transparent == 1)
            {
                picCurveShow.Background = new SolidColorBrush();
            }
            else
            {
                picCurveShow.Background = new SolidColorBrush(_BackColor);
            }
        }

    }

    
    /// <summary>
    /// 定义一个数据结构，存放图形上的数据
    /// </summary>
    class RowItem
    {
        public double X=0.0;
        public double Y = 0.0;
    }
}
