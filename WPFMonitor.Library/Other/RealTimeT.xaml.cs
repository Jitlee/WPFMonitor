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
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Linq;
using System.Windows.Media.Imaging;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;


namespace MonitorSystem.Other
{
    public partial class RealTimeT : MonitorControl
    {
        /// <summary>
        /// 背景框
        /// </summary>
        Canvas _CanvasGrid = new Canvas();//用于装背景
        public Canvas _CanvasPoint = new Canvas();
        
        public Canvas _CanvasLine = new Canvas();//用于装线
        List<RealTimeLineOR> _listRealTimeLine = new List<RealTimeLineOR>();

        /// <summary>
        /// 所有线信息
        /// </summary>
        public List<RealTimeLineOR> ListRealTimeLine
        {
            get { return _listRealTimeLine; }
            set { _listRealTimeLine = value; }
        }

        #region 初使化
        public RealTimeT()
        {
            InitializeComponent();

            this.BackColor = Common1.StringToColor("#FFD5D5FF");
            ForeColor = Common1.StringToColor("#FFD5D5FF");
            InitBG();
            //this.Width = 400;
            //this.Height = 400;

             this.inputYear.DataValue=DateTime.Now.Year;
             this.inputMonth.DataValue = DateTime.Now.Month;
            this.inputDay.DataValue=DateTime.Now.Day;

            this.inputHour.DataValue = DateTime.Now.Hour;
            this.inputEHour.DataValue = DateTime.Now.Hour;
        }
     
        public void SetLineValue(V_ScreenMonitorValue obj)
        {
            foreach (RealTimeLineOR line in this._listRealTimeLine)
            {
                if (line.LineInfo.DeviceID != null && line.LineInfo.ChannelNo != null)
                {
                    if (line.LineInfo.DeviceID == obj.DeviceID && line.LineInfo.ChannelNo == obj.ChannelNo
                        && line.LineInfo.ComputeStr == obj.ComputeStr)
                    {
                        line.SetYValue(obj.MonitorValue);
                    }
                }
            }
        }
        private void InitBasicinfo()
        {
            _CanvasZ.SetValue(Canvas.ZIndexProperty, 1);//轴
            _CanvasGrid.SetValue(Canvas.ZIndexProperty, 0);
            _CanvasPoint.SetValue(Canvas.ZIndexProperty, 3);
            _CanvasLine.SetValue(Canvas.ZIndexProperty, 2);

            _Canvas.Children.Add(_CanvasPoint);
            _Canvas.Children.Add(_CanvasGrid);
            _Canvas.Children.Add(_CanvasLine);

            this.SizeChanged += new SizeChangedEventHandler(RealTime_SizeChanged);
            _Canvas.SizeChanged += new SizeChangedEventHandler(Canvas_SizeChanged);
            gdMain.MouseLeftButtonUp += new MouseButtonEventHandler(RealTimeT_MouseLeftButtonUp);
        }


        int Number = 0;
        DateTime PriTime = DateTime.Now;
        private void RealTimeT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TimeSpan t = DateTime.Now - PriTime;
            if (t.Hours == 0 && t.Minutes == 0 && t.Seconds == 0 && t.Milliseconds <= 500)
            {
                Number++;
            }
            else
            {
                Number = 1;
            }
            PriTime = DateTime.Now;

            if (Number == 2)
            {
                RealtimeProperty rp = new RealtimeProperty();
                rp.RealTimeData = this;
                rp.Init();
                rp.Show();
                Number = 0;
            }
        }

        private void InitBG()
        {
            //ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri("../Images/RealtimeBG.jpg", UriKind.Relative));
            //imageBrush.Stretch = Stretch.Uniform;
            //Canvas _v = new Canvas();
            //_v.Background = imageBrush;
            //this.Content = _v;
        }

        private void RealTime_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!(e.NewSize.Width > 0 && e.NewSize.Height > 0))
                return;

            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Height;

            //if (this.Width < 300 || this.Height < 300)
            //{
            //    InitBG();
            //    return;
            //}


            if (double.IsNaN(_Canvas.Width))
                return;

            double mwidth = e.NewSize.Width - (95 + 80 + 20 + 70 + 4);
            double mheight = e.NewSize.Height - (23 + 60 + 65);
            if (mwidth > 0 && mheight > 0)
            {
                _Canvas.Width = mwidth;
                _Canvas.Height = mheight;

                _CanvasZ.Width = mwidth;
                _CanvasZ.Height = mheight;

                PaintBasicInfo();
                Paint();
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (this.Width < 300 || this.Height < 300)
            //{
            //    InitBG();
            //    return;
            //}

            _Canvas.Width = e.NewSize.Width;
            _Canvas.Height = e.NewSize.Height;

            _CanvasZ.Width = e.NewSize.Width;
            _CanvasZ.Height = e.NewSize.Height;

            PaintBasicInfo();

            Paint();
        }
        /// <summary>
        /// 背景信息
        /// </summary>
        private void Paint()
        {
            
            SolidColorBrush BgColor =new SolidColorBrush( ForeColor);// new SolidColorBrush(Common1.StringToColor("RGB(213,213,255)"));
            //ForeColor
            TopTit.Points.Clear();
            TopTit.Points.Add(new Point(0, 0));
            TopTit.Points.Add(new Point(this.Width * 0.2, 0));
            TopTit.Points.Add(new Point(this.Width * 0.33, 20));
            TopTit.Points.Add(new Point(0, 20));
            TopTit.Fill = BgColor;

            bgLeft.Background = BgColor;
            GridMain.Background = BgColor;
            GridCZFY.Background = BgColor;
            GridSF.Background = BgColor;

            gridBouutomSet.Background = BgColor;


            //滚动条属性设置
            jTControl1.BGColor = BgColor;
            jTControl2.BGColor = BgColor;
            jtCenter.BGColor = BgColor;
            jTControl3.BGColor = BgColor;
            jTControl4.BGColor = BgColor;

            jTXControl1.BGColor = BgColor;
            jTXControl2.BGColor = BgColor;
            jtXCenter.BGColor = BgColor;
            jTXControl3.BGColor = BgColor;
            jTXControl4.BGColor = BgColor;
        }
        #endregion

        #region 公共
        #region 函数
        public override event EventHandler Selected;

        public override event EventHandler Unselected;

        private void OnUnselected(object sender, EventArgs e)
        {
            if (null != Unselected)
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

                var menu = new ContextMenu();
                var menuItem = new MenuItem() { Header = "属性" };
                menuItem.Click += PropertyMenuItem_Click;
                menu.Items.Add(menuItem);
                AdornerLayer.SetValue(ContextMenuService.ContextMenuProperty, menu);
            }
        }
        private void PropertyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RealtimeProperty rp = new RealtimeProperty();
            rp.RealTimeData = this;
            rp.Init();
            rp.Show();
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
                //X轴	
                if (name == "XISSGShow".ToUpper())//是否X轴栅格显示
                {
                    XISSGShow = Convert.ToBoolean(value);
                }
                else if (name == "XMainNumber".ToUpper())//X主分度数
                {
                    XMainNumber = Convert.ToInt32(value);
                }
                else if (name == "XMainColor".ToUpper())//X主分度颜色
                {
                    XMainColor = Common1.StringToColor(value);
                }
                else if (name == "XPriNumber".ToUpper())//X次分度数
                {
                    XPriNumber = Convert.ToInt32(value);
                }
                else if (name == "XPriColor".ToUpper())//X次分度颜色
                {
                    XPriColor = Common1.StringToColor(value);
                }
                //Y轴
                else if (name == "YISSGShow".ToUpper())//是否Y轴栅格显示
                {
                    YISSGShow = Convert.ToBoolean(value);
                }
                else if (name == "YMainNumber".ToUpper())//Y主分度数
                {
                    YMainNumber = Convert.ToInt32(value);
                }
                else if (name == "YMainColor".ToUpper())//Y主分度颜色
                {
                    YMainColor = Common1.StringToColor(value);
                }
                else if (name == "YPriNumber".ToUpper())//Y次分度数
                {
                    YPriNumber = Convert.ToInt32(value);
                }
                else if (name == "YPriColor".ToUpper())//Y次分度颜色
                {
                    YPriColor = Common1.StringToColor(value);
                }
                //其它颜色
                else if (name == "ISShowBorder".ToUpper())//显示边框
                {
                    ISShowBorder = Convert.ToBoolean(value);
                }
                else if (name == "BorderColor".ToUpper())//边框颜色
                {
                    BorderColor = Common1.StringToColor(value);
                }
                else if (name == "ISShowGridBack".ToUpper())//显示背景
                {
                    ISShowGridBack = Convert.ToBoolean(value);
                }
                else if (name == "GridBackColor".ToUpper())//背景颜色
                {
                    GridBackColor = Common1.StringToColor(value);
                }
                else if (name == "ISShowCursor".ToUpper())//显示游标
                {
                    ISShowCursor = Convert.ToBoolean(value);
                }
                else if (name == "CursorColor".ToUpper())//游标颜色
                {
                    CursorColor = Common1.StringToColor(value);
                }
                else if (name == "ISShowTime".ToUpper())//显示时间
                {
                    ISShowTime = Convert.ToBoolean(value);
                }
                else if (name == "TimeColor".ToUpper())//时间颜色
                {
                    TimeColor = Common1.StringToColor(value);
                }
                else if (name == "UsePerZB".ToUpper())//采用百分比坐标
                {
                    UsePerZB = Convert.ToBoolean(value);
                }
                else if (name == "NoUseDataMove".ToUpper())//无效数据移出
                {
                    NoUseDataMove = Convert.ToBoolean(value);
                }
                else if (name == "DoubleClickShowSet".ToUpper())//双击显示设置框
                {
                    DoubleClickShowSet = Convert.ToBoolean(value);
                }
                else if (name == "RightShowYZB".ToUpper())//右显示Y轴坐标
                {
                    RightShowYZB = Convert.ToBoolean(value);
                }
                else if (name == "MultiXZShow".ToUpper())//多X轴显示
                {
                    MultiXZShow = Convert.ToBoolean(value);
                }
                else if (name == "MultiYZShow".ToUpper())//多Y轴显示
                {
                    MultiYZShow = Convert.ToBoolean(value);
                }
                else if (name == "IsShowLegend".ToUpper())//显示图例
                {
                    IsShowLegend = Convert.ToBoolean(value);
                }
                else if (name == "ShowLegend".ToUpper())//显示图例
                {
                    ShowLegend = value.ToString();
                }
                else if (name == "InfoLWidth".ToUpper())//信息栏宽度
                {
                    InfoLWidth = int.Parse(value);
                }
                //放缩设置
                else if (name == "MouseDrawEnlare".ToUpper())//鼠标拖动放大
                {
                    MouseDrawEnlare = Convert.ToBoolean(value);
                }
                else if (name == "XZEnlare".ToUpper())//X轴放大
                {
                    XZEnlare = Convert.ToBoolean(value);
                }
                else if (name == "YZEnlare".ToUpper())//Y轴放大
                {
                    YZEnlare = Convert.ToBoolean(value);
                }
                else if (name == "MouseDrawMove".ToUpper())//鼠标拖动移动
                {
                    MouseDrawMove = Convert.ToBoolean(value);
                }
                else if (name == "XZMove".ToUpper())//X轴移动
                {
                    XZMove = Convert.ToBoolean(value);
                }
                else if (name == "YZMove".ToUpper())//Y轴移动
                {
                    YZMove = Convert.ToBoolean(value);
                }
            }

            //属性实例化完，加载直线
            //把这个放在这里，因为，在图库里，双击也会弹出属性框来
            InitBasicinfo();
            InitLine();
            PaintBasicInfo();
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
           "BackColor", "ForeColor", "Transparent","Translate"};
        public override string[] BrowsableProperties
        {
            get { return m_BrowsableProperties; }
            set { m_BrowsableProperties = value; }
        }


        private static readonly DependencyProperty BackColorProperty =
           DependencyProperty.Register("BackColor",
           typeof(Color), typeof(RealTimeT), new PropertyMetadata(Common1.StringToColor("#FFEBE8D9")
               ));
        [DefaultValue(""), Description("背景色"), Category("外观")]
        public Color BackColor
        {
            get { return (Color)this.GetValue(BackColorProperty); }
            set
            {
                this.SetValue(BackColorProperty, value);
                if (ScreenElement != null)
                    ScreenElement.BackColor = value.ToString();
                SetBackColor(value);
            }
        }

        private void SetBackColor(Color cor)
        {
            LayoutRoot.Background = new SolidColorBrush(cor);
        }

        private static readonly DependencyProperty ForeColorProperty =
            DependencyProperty.Register("ForeColor",
            typeof(Color), typeof(RealTimeT), new PropertyMetadata(Common1.StringToColor("#FFD5D5FF")));
        [DefaultValue(""), Description("前景色"), Category("外观")]
        public Color ForeColor
        {
            get { return (Color)this.GetValue(ForeColorProperty); }
            set
            {
                this.SetValue(ForeColorProperty, value);
                if (ScreenElement != null)
                {
                    ScreenElement.ForeColor = value.ToString();
                    Paint();
                }
            }
        }


        private static readonly DependencyProperty TransparentProperty = DependencyProperty.Register("Transparent",
        typeof(int), typeof(RealTimeT), new PropertyMetadata(0));
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
        #endregion

        #endregion

        #region X轴
        private bool _XISSGShow = true;
        /// <summary>
        /// 是否X轴栅格显示
        /// </summary>
        public bool XISSGShow
        {
            get { return _XISSGShow; }
            set
            {
                _XISSGShow = value;
                SetAttrByName("XISSGShow", value);
            }
        }

        private int _XMainNumber = 3;
        /// <summary>
        /// X主线度数
        /// </summary>
        public int XMainNumber
        {
            get { return _XMainNumber; }
            set { _XMainNumber = value; SetAttrByName("XMainNumber", value); }
        }

        private Color _XMainColor = Colors.Black;
        /// <summary>
        /// X主线颜色
        /// </summary>
        public Color XMainColor
        {
            get { return _XMainColor; }
            set { _XMainColor = value; SetAttrByName("XMainColor", value); }
        }


        private int _XPriNumber = 3;
        /// <summary>
        /// X次分度数
        /// </summary>
        public int XPriNumber
        {
            get { return _XPriNumber; }
            set { _XPriNumber = value; SetAttrByName("XPriNumber", value); }
        }

        private Color _XPriColor = Colors.Black;
        /// <summary>
        /// X次分颜色
        /// </summary>
        public Color XPriColor
        {
            get { return _XPriColor; }
            set { _XPriColor = value; SetAttrByName("XPriColor", value); }
        }

        #endregion Y轴

        #region  Y轴

        private bool _YISSGShow = true;
        /// <summary>
        /// 是否Y轴栅格显示
        /// </summary>
        public bool YISSGShow
        {
            get { return _YISSGShow; }
            set { _YISSGShow = value; SetAttrByName("YISSGShow", value); }
        }

        private int _YMainNumber = 3;
        /// <summary>
        /// Y主线度数
        /// </summary>
        public int YMainNumber
        {
            get { return _YMainNumber; }
            set { _YMainNumber = value; SetAttrByName("YMainNumber", value); }
        }

        private Color _YMainColor = Colors.Black;
        /// <summary>
        /// Y主线颜色
        /// </summary>
        public Color YMainColor
        {
            get { return _YMainColor; }
            set { _YMainColor = value; SetAttrByName("YMainColor", value); }
        }


        private int _YPriNumber = 3;
        /// <summary>
        /// Y次分度数
        /// </summary>
        public int YPriNumber
        {
            get { return _YPriNumber; }
            set { _YPriNumber = value; SetAttrByName("YPriNumber", value); }
        }

        private Color _YPriColor = Colors.Black;
        /// <summary>
        /// Y次分颜色
        /// </summary>
        public Color YPriColor
        {
            get { return _YPriColor; }
            set { _YPriColor = value; SetAttrByName("YPriColor", value); }
        }

        #endregion

        #region 其它设置

        #region 颜色设置

        private bool _ISShowBorder = true;
        /// <summary>
        /// 显示边框
        /// </summary>
        public bool ISShowBorder
        {
            get { return _ISShowBorder; }
            set { _ISShowBorder = value; SetAttrByName("ISShowBorder", value); }
        }


        private Color _BorderColor = Colors.Black;
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; SetAttrByName("BorderColor", value); }
        }

        private bool _ISShowGridBack = true;
        /// <summary>
        /// 显示背景
        /// </summary>
        public bool ISShowGridBack
        {
            get { return _ISShowGridBack; }
            set { _ISShowGridBack = value; SetAttrByName("ISShowGridBack", value); }
        }


        private Color _GridBackColor = Colors.Cyan;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color GridBackColor
        {
            get { return _GridBackColor; }
            set { _GridBackColor = value; SetAttrByName("GridBackColor", value); }
        }

        private bool _ISShowCursor = false;
        /// <summary>
        /// 显示游标
        /// </summary>
        public bool ISShowCursor
        {
            get { return _ISShowCursor; }
            set { _ISShowCursor = value; SetAttrByName("ISShowCursor", value); }
        }

        private Color _CursorColor;
        /// <summary>
        /// 游标颜色
        /// </summary>
        public Color CursorColor
        {
            get { return _CursorColor; }
            set { _CursorColor = value; SetAttrByName("CursorColor", value); }
        }

        private bool _ISShowTime = true;
        /// <summary>
        /// 显示时间
        /// </summary>
        public bool ISShowTime
        {
            get { return _ISShowTime; }
            set { _ISShowTime = value; SetAttrByName("ISShowTime", value); }
        }

        private Color _TimeColor;
        /// <summary>
        /// 时间颜色
        /// </summary>
        public Color TimeColor
        {
            get { return _TimeColor; }
            set { _TimeColor = value; SetAttrByName("TimeColor", value); }
        }
        #endregion



        private bool _UsePerZB;
        /// <summary>
        /// 采用百分比坐标
        /// </summary>
        public bool UsePerZB
        {
            get { return _UsePerZB; }
            set { _UsePerZB = value; SetAttrByName("UsePerZB", value); }
        }

        private bool _NoUseDataMove;
        /// <summary>
        /// 无效数据移出
        /// </summary>
        public bool NoUseDataMove
        {
            get { return _NoUseDataMove; }
            set { _NoUseDataMove = value; SetAttrByName("NoUseDataMove", value); }
        }

        private bool _DoubleClickShowSet;
        /// <summary>
        /// 双击显示设置框
        /// </summary>
        public bool DoubleClickShowSet
        {
            get { return _DoubleClickShowSet; }
            set { _DoubleClickShowSet = value; SetAttrByName("DoubleClickShowSet", value); }
        }

        private bool _RightShowYZB;
        /// <summary>
        /// 右轴显示Y轴坐标
        /// </summary>
        public bool RightShowYZB
        {
            get { return _RightShowYZB; }
            set { _RightShowYZB = value; SetAttrByName("RightShowYZB", value); }
        }

        private bool _MultiXZShow = false;
        /// <summary>
        /// 多X轴显示
        /// </summary>
        public bool MultiXZShow
        {
            get { return _MultiXZShow; }
            set { _MultiXZShow = value; SetAttrByName("MultiXZShow", value); }
        }

        private bool _MultiYZShow;
        /// <summary>
        /// 多Y轴显示
        /// </summary>
        public bool MultiYZShow
        {
            get { return _MultiYZShow; }
            set { _MultiYZShow = value; SetAttrByName("MultiYZShow", value); }
        }

        private bool _IsShowLegend;
        /// <summary>
        /// 是否显示图例
        /// </summary>
        public bool IsShowLegend
        {
            get { return _IsShowLegend; }
            set { _IsShowLegend = value; SetAttrByName("IsShowLegend", value); }
        }

        private string _ShowLegend;
        /// <summary>
        /// 显示图例(
        /// 显示数值、显示变量描述、显示表达试、显示变量最大值、
        ///显示变量最小值、显示变量平均值、显示变量统计值、显示时间最小值、
        ///显示时间最大值)
        /// </summary>
        public string ShowLegend
        {
            get { return _ShowLegend; }
            set { _ShowLegend = value; SetAttrByName("ShowLegend", value); }
        }

        private int _infoLWidth;
        /// <summary>
        /// 信息栏宽度
        /// </summary>
        public int InfoLWidth
        {
            get { return _infoLWidth; }
            set { _infoLWidth = value; SetAttrByName("InfoLWidth", value); }
        }
        #endregion

        #region 放缩设置
        private bool _MouseDrawEnlare;
        /// <summary>
        /// 鼠标移动放大
        /// </summary>
        public bool MouseDrawEnlare
        {
            get { return _MouseDrawEnlare; }
            set { _MouseDrawEnlare = value; SetAttrByName("MouseDrawEnlare", value); }
        }

        private bool _XZEnlare;
        /// <summary>
        /// X轴放大
        /// </summary>
        public bool XZEnlare
        {
            get { return _XZEnlare; }
            set { _XZEnlare = value; SetAttrByName("XZEnlare", value); }
        }

        private bool _YZEnlare;
        /// <summary>
        /// Y轴放大
        /// </summary>
        public bool YZEnlare
        {
            get { return _YZEnlare; }
            set { _YZEnlare = value; SetAttrByName("YZEnlare", value); }
        }


        private bool _MouseDrawMove;
        /// <summary>
        /// 鼠标拖动移动
        /// </summary>
        public bool MouseDrawMove
        {
            get { return _MouseDrawMove; }
            set { _MouseDrawMove = value; SetAttrByName("MouseDrawMove", value); }
        }

        private bool _XZMove;
        /// <summary>
        /// X轴移动
        /// </summary>
        public bool XZMove
        {
            get { return _XZMove; }
            set { _XZMove = value; SetAttrByName("XZMove", value); }
        }

        private bool _YZMove;
        /// <summary>
        /// Y轴移动
        /// </summary>
        public bool YZMove
        {
            get { return _YZMove; }
            set { _YZMove = value; SetAttrByName("YZMove", value); }
        }
        #endregion


        /// <summary>
        /// 显示基本信息
        /// </summary>
        public void PaintBasicInfo()
        {
            

            if (double.IsNaN(_Canvas.Width))
                return;
            _Canvas.Children.Clear();
            _Canvas.Children.Add(_CanvasPoint);
            _Canvas.Children.Add(_CanvasGrid);
            _Canvas.Children.Add(_CanvasLine);

            //实时线网格
            SetXYStartPosition();
            PainGrid();
            if (LineCanversHeight < 0 || LineCanversWidth < 0)
                return;
            DrawLine();
            //ShowValue();
            PainXYZ();

            //处理游标
            if (_ISShowCursor)
            {
                _CanvasGrid.Children.Add(_Cursor);
                _Cursor.Visibility = Visibility.Collapsed;

                _Cursor.LineColor = _CursorColor;
                _Cursor.Width = LineCanversWidth;
                _Cursor.Height = LineCanversHeight - _YZStartPosition;

                // _CanvasGrid 换成  _CanvasLine
                _CanvasGrid.MouseMove += new MouseEventHandler(_CanvasGrid_MouseMove);
                _CanvasGrid.MouseLeftButtonDown += new MouseButtonEventHandler(_CanvasGrid_MouseLeftButtonDown);
                _CanvasGrid.MouseLeftButtonUp += new MouseButtonEventHandler(_CanvasGrid_MouseLeftButtonUp);
                //_CanvasGrid.MouseLeave+=new MouseEventHandler(_CanvasGrid_MouseLeave);
              //  _CanvasGrid.MouseRightButtonUp+=new MouseButtonEventHandler(_CanvasGrid_MouseRightButtonUp);
            }
            //else
            //{
            //    _CanvasGrid.MouseMove -= new MouseEventHandler(_CanvasGrid_MouseMove);
            //    _CanvasGrid.MouseLeftButtonDown -= new MouseButtonEventHandler(_CanvasGrid_MouseLeftButtonDown);
            //    _CanvasGrid.MouseLeftButtonUp -= new MouseButtonEventHandler(_CanvasGrid_MouseLeftButtonUp);
            //}
        }
        #region 处理游标\移动
        /// <summary>
        /// 游标控件 
        /// </summary>
        CurveControl _Cursor = new CurveControl();

        /// <summary>
        /// 第一个曲线,用于获取游标的值
        /// </summary>
        RealTimeLineOR FirstLineOR = null;

        /// <summary>
        /// 是否移动了
        /// </summary>
        bool CanvasISMove = false;
        /// <summary>
        /// 游标是否已经显示了
        /// </summary>
        bool CursorISHaveShow = false;
        /// <summary>
        /// 鼠标按下坐标点位置
        /// </summary>
        Point _CoursorStartPoint = new Point();        
        /// <summary>
        /// 鼠标是否按下
        /// </summary>
        bool MouseISDown = false;
        protected void _CanvasGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _CanvasGrid.CaptureMouse();
            _CoursorStartPoint = e.GetPosition(_CanvasGrid);
            MouseISDown = true;
            CanvasISMove = false;
        }
        protected void _CanvasGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _CanvasGrid.ReleaseMouseCapture();
            if (!CanvasISMove)//单击,鼠标没有移动
            {
                if (_ISShowCursor)
                {
                    if (!CursorISHaveShow)
                    {
                        _Cursor.Visibility = Visibility.Visible;
                        CursorISHaveShow = true;
                    }
                    else
                    {
                        CursorISHaveShow = false;
                        _Cursor.Visibility = Visibility.Collapsed;
                    }
                }
            }
            MouseISDown = false;
        }
        //protected void _CanvasGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    foreach (RealTimeLineOR obj in _listRealTimeLine)
        //    {
        //        obj.ISShowValue = true;
        //    }
        //}

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                obj.ISShowValue = true;
            }
        }

        /// <summary>
        /// 滚动条单击事件
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        protected void GDClick(object Sender, EventArgs e)
        {
            if (Sender is JTControl)
            {
                JTControl obj = Sender as JTControl;
                if (obj.JTType == 4)
                {
                    return;
                }

                bool isAdd = false;
                double mWidth = 0.0;
                double mHeight = 0.0;
               
                //Y轴处理
                if (obj.JTType == 0)
                {
                    mHeight = LineCanversHeight;
                }
                else if (obj.JTType == 1)
                {
                    mHeight = LineCanversHeight * 0.2;
                }
                else if (obj.JTType == 2)
                {
                    mHeight = LineCanversHeight * -0.2;
                }
                else if (obj.JTType == 3)
                {
                    mHeight = LineCanversHeight * -1.0;
                }//X轴
                else if (obj.JTType == 5)
                {
                    isAdd = true;
                    mWidth = LineCanversWidth;
                }
                else if (obj.JTType == 6)
                {
                    isAdd = true;
                    mWidth = LineCanversWidth* 0.2;
                }
                else if (obj.JTType == 7)
                {   
                    mWidth = LineCanversWidth * 0.2;
                }
                else if (obj.JTType == 8)
                {
                    mWidth = LineCanversWidth;
                }

                foreach (RealTimeLineOR _Line in _listRealTimeLine)
                {
                    _Line.ISShowValue = false;
                    _Line.HeadMove(mWidth, isAdd, mHeight);
                    ShowYText(_Line);
                    _Line.ShowCurve();
                }
            }
        }

        protected void _CanvasGrid_MouseMove(object sender, MouseEventArgs e)
        {
            CanvasISMove = true;
            Point pMove = e.GetPosition(_CanvasGrid);
            if (CursorISHaveShow)//游标已经显示,处理游标位置
            {
                _Cursor.SetPosition(pMove);
                if (FirstLineOR != null)
                {
                    _Cursor.SetXShow(FirstLineOR.GetXCursorValue(pMove.X));
                    _Cursor.SetYShow(FirstLineOR.GetYCursorValue(pMove.Y));
                }
            }

            //处理拖动情况,X、YX
            if (MouseISDown && this._MouseDrawMove) //处理是否移动 
            {

                //是否有移动,移动的距离不足，不移动
                bool isMove = false;
                double mWidth = 0.0;
                if (XZMove)//X轴移动
                    mWidth = _CoursorStartPoint.X - pMove.X;
                double mHeight = 0.0;
                if (YZMove)
                {
                    mHeight = pMove.Y - _CoursorStartPoint.Y;
                }

                bool isAdd = false;
                if (mWidth <= 0)
                {
                    isAdd = false;
                    mWidth *= -1;
                }
                else
                {
                    isAdd = true;
                }
                if (FirstLineOR != null)
                {
                    if (mWidth > FirstLineOR.MinMoveWidth)
                    {
                        isMove = true;
                    }
                }
                if (mHeight > 1 || (mHeight * -1) > 1)
                {
                    isMove = true;
                }

                if (isMove)
                {
                    _CoursorStartPoint = pMove;
                    foreach (RealTimeLineOR obj in _listRealTimeLine)
                    {
                        obj.ISShowValue = false;
                        obj.HeadMove(mWidth, isAdd, mHeight);
                        ShowYText(obj);
                        obj.ShowCurve();
                    }
                }
            }
        }

        //protected void _CanvasGrid_MouseLeave(object sender, MouseEventArgs e)
        //{
           
        //}
        #endregion

        List<t_Element_RealTimeLine> _lines;
        /// <summary>
        /// 初使化曲线
        /// </summary>
        private void InitLine()
        {
            if (null == _lines)
            {
                _lines = new ElementRealTimeLineDA().selectAllDate();

            }
            var vLine = _lines.Where(a => a.ElementID == this.ScreenElement.ElementID);
            if (vLine.Count() > 0)
            {
                foreach (t_Element_RealTimeLine tLine in vLine)
                {
                    RealTimeLineOR obj = new RealTimeLineOR(tLine);
                    _listRealTimeLine.Add(obj);
                }
                FirstLineOR = _listRealTimeLine.First();
            }
            else
            {
                t_Element_RealTimeLine objLine = new t_Element_RealTimeLine();
                objLine.ID = "N" + Guid.NewGuid().ToString("N");
                objLine.LineName = "ll";
                objLine.MaxValue = "100";
                objLine.MinValue = "0";
                objLine.LineCZ = 0;
                objLine.LineCYZQLent = "1";
                objLine.LineCYZQType = "ss";
                objLine.TimeLen = 50;
                objLine.TimeLenType = "ss";
                objLine.LineShowType = 0;
                objLine.LineStyle = 0;
                objLine.LinePointBJ = 0;
                objLine.ShowFormat = "mm:ss";
                objLine.LineColor = Colors.Blue.ToString();

                RealTimeLineOR obj = new RealTimeLineOR(objLine);
                _listRealTimeLine.Add(obj);
            }           
        }
        /// <summary>
        /// 显示曲线
        /// </summary>
        private void DrawLine()
        {
            gdLineDefine.RowDefinitions.Clear();
            gdLineDefine.Children.Clear();

            _CanvasLine.Children.Clear();
            _CanvasPoint.Children.Clear();
            if (IsShowLegend)
            {
                LineList.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                LineList.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (IsShowLegend)
            {
                for (int i = 0; i < _listRealTimeLine.Count; i++)
                {
                    gdLineDefine.RowDefinitions.Add(new RowDefinition());
                }
            }
            int index = 0;
            foreach (RealTimeLineOR objOR in _listRealTimeLine)
            {
                RealLineShow obj = new RealLineShow(objOR);
                obj.Margin = new Thickness(5);

                objOR.TitleShowInfo = obj;
                objOR.PicCurveShowHeight = LineCanversHeight -_YZStartPosition;
                objOR.PicCurveWidth = LineCanversWidth;
                objOR.SetPolyLine();
                objOR.RealtimeObj = this;

                obj.ChangeLineShow += new EventHandler(RealLineShow_ChangeLineShow);
                if (IsShowLegend)
                {
                    gdLineDefine.Children.Add(obj);
                    obj.SetValue(Grid.RowProperty, index);
                }
                //添加线到界面                
                _CanvasLine.Children.Add(objOR.PolyLine);
                index++;
            }
        }
        #region 画背景网格

        //Y开始坐标
        double _YStart = 0;
        //X开始坐标
        double _XStart = 100;
        /// <summary>
        /// 网格的宽度
        /// </summary>
        double xGridWidth = 0;
        /// <summary>
        /// 网络的宽度
        /// </summary>
        double yGridHeight = 0;

        /// <summary>
        /// 显示曲线的宽度
        /// </summary>
        double LineCanversWidth = 0;
        /// <summary>
        /// 显示曲线的高度
        /// </summary>
        double LineCanversHeight = 0;

        /// <summary>
        /// 主线宽度
        /// </summary>
        double GridMainLineWidth = 1;
        //次线宽度过
        double GridPriLineWidth = 0.5;

        /// <summary>
        /// 曲线与边框的间隔
        /// </summary>
        double _YZStartPosition = 15;


        /// <summary>
        /// 显示背景框，和其它初使值
        /// </summary>
        private void PainGrid()
        {
            //边框
            Line _Line1 = new Line();
            Line _Line3 = new Line();
            _Line1.SetValue(Canvas.ZIndexProperty, 5);
            _Line3.SetValue(Canvas.ZIndexProperty, 5);
            _CanvasGrid.Children.Clear();
            //处理背景显示
            if (ISShowGridBack)
            {
                _CanvasGrid.Background = new SolidColorBrush(_GridBackColor);
            }
            else
            {
                _CanvasGrid.Background = new SolidColorBrush();
            }
            
            SetXYStartPosition();

            double yXStartP = (YZLineWidth + YZTxtWidth);

            LineCanversWidth = _Canvas.Width - _XStart;
            LineCanversHeight = _Canvas.Height - _YStart;

            if (LineCanversHeight < 0 || LineCanversWidth < 0)
                return;

            Rect rect = new Rect();
            rect.Width = LineCanversWidth;
            rect.Height = LineCanversHeight;// -_YZStartPosition;
            rect.X = 0;

            RectangleGeometry r = new RectangleGeometry();
            r.Rect = rect;
            _CanvasLine.Clip = r;

            r = new RectangleGeometry();
            r.Rect = rect;
            _CanvasPoint.Clip = r; 

            _CanvasPoint.Width = _CanvasGrid.Width = _CanvasLine.Width = LineCanversWidth;

            //_CanvasLine.Height = LineCanversHeight;
            _CanvasLine.Height = _CanvasPoint.Height = _CanvasGrid.Height = LineCanversHeight-_YZStartPosition;
            _CanvasGrid.SetValue(Canvas.TopProperty, _YZStartPosition);
            _CanvasPoint.SetValue(Canvas.TopProperty, _YZStartPosition);
            _CanvasLine.SetValue(Canvas.TopProperty, _YZStartPosition);
            if (_RightShowYZB)
            {
                _CanvasPoint.SetValue(Canvas.LeftProperty, 0d);
                _CanvasGrid.SetValue(Canvas.LeftProperty, 0d);
                _CanvasLine.SetValue(Canvas.LeftProperty, 0d);
            }
            else
            {
                _CanvasPoint.SetValue(Canvas.LeftProperty, _XStart);
                _CanvasGrid.SetValue(Canvas.LeftProperty, _XStart);
                _CanvasLine.SetValue(Canvas.LeftProperty, _XStart);
            }

            xGridWidth = _Canvas.Width - _XStart;
            yGridHeight = _Canvas.Height - _YStart;// -XZHeight;  

            //边框
            if (ISShowBorder)
            {
                _CanvasGrid.Children.Add(_Line1);
                _CanvasGrid.Children.Add(_Line3);
                _Line1.X1 = 0;
                _Line1.Y1 = 0;
                _Line1.X2 = xGridWidth;
                _Line1.Y2 = 0;

                //右边显示Y轴
                if (RightShowYZB)
                {
                    _Line3.X2 = _Line3.X1 = 0;
                }
                else
                {
                    _Line3.X2 = _Line3.X1 = xGridWidth;
                }
                _Line3.Y1 = 0;
                _Line3.Y2 = yGridHeight - _YZStartPosition;

                _Line3.Stroke = _Line1.Stroke = new SolidColorBrush(_BorderColor);
                _Line3.StrokeThickness = _Line1.StrokeThickness = 2;
            }
            //Y网格
            if (_YISSGShow)
            {
                double _YStartDoble = 0;// _YZStartPosition;

                double aYMainSize = (yGridHeight - _YZStartPosition) / (this._YMainNumber + 1);
                double aYPerSize = aYMainSize / (this._YPriNumber + 1);

                for (int imain = 0; imain < _YMainNumber; imain++)
                {
                    PainPriGrid(_YStartDoble, aYPerSize, true);
                    double y = _YStartDoble + aYMainSize;
                    _YStartDoble = y;
                    _CanvasGrid.Children.Add(AddMainLine(0, xGridWidth, y, y, _YMainColor, GridMainLineWidth));
                }
                PainPriGrid(_YStartDoble, aYPerSize, true);
            }
            //X网格
            if (_XISSGShow)
            {
                double _XStartDoble = 0;
                double aXMainSize = xGridWidth / (this._XMainNumber + 1);
                double aXPerSize = aXMainSize / (this._XPriNumber + 1);

                for (int imain = 0; imain < _XMainNumber; imain++)
                {
                    PainPriGrid(_XStartDoble, aXPerSize, false);
                    double X = _XStartDoble + aXMainSize;
                    _XStartDoble = X;

                    _CanvasGrid.Children.Add(AddMainLine(X, X, 0, yGridHeight - _YZStartPosition, _XMainColor, GridMainLineWidth));
                }
                PainPriGrid(_XStartDoble, aXPerSize, false);
            }
        }

        private void PainPriGrid(double startPosition, double aPerSize, bool ISAddY)
        {
            if (ISAddY)//y轴
            {
                for (int i = 0; i < this._YPriNumber; i++)
                {
                    double priy = startPosition + aPerSize;
                    startPosition = priy;
                    _CanvasGrid.Children.Add(AddPrimLine(0, xGridWidth, priy, priy, _YPriColor, GridPriLineWidth));
                }
            }
            else//x轴
            {
                for (int i = 0; i < this._XPriNumber; i++)
                {
                    double priX = startPosition + aPerSize;
                    startPosition = priX;
                    _CanvasGrid.Children.Add(AddPrimLine(priX, priX, 0, yGridHeight - _YZStartPosition, _XPriColor, GridPriLineWidth));
                }
            }
        }

        #region 添加线
        private Line AddMainLine(double _x1, double _x2, double _y1, double _y2, Color _Color, double lineSize)
        {
            Line _Line = new Line();
            _Line.X1 = _x1;
            _Line.X2 = _x2;
            _Line.Y1 = _y1;
            _Line.Y2 = _y2;
            _Line.Stroke = new SolidColorBrush(_Color);
            _Line.StrokeThickness = lineSize;
            return _Line;
        }
        private Line AddPrimLine(double _x1, double _x2, double _y1, double _y2, Color _Color, double lineSize)
        {
            Line _Line = new Line();
            _Line.X1 = _x1;
            _Line.X2 = _x2;
            _Line.Y1 = _y1;
            _Line.Y2 = _y2;
            _Line.Stroke = new SolidColorBrush(_Color);
            _Line.StrokeThickness = lineSize;
            _Line.StrokeDashArray = new DoubleCollection { 2.0, 2.0 };
            return _Line;
        }
        #endregion

        #endregion
        #region 画X、Y轴
        /// <summary>
        /// 画X、Y轴
        /// </summary>
        private void PainXYZ()
        {
            _CanvasZ.Children.Clear();

            //X轴处理 
            if (_MultiXZShow)
            {
                int Index = 0;
                foreach (RealTimeLineOR obj in _listRealTimeLine)
                {
                    PaintX(obj, Index);
                    Index++;
                }
            }
            else
            {
                if (_listRealTimeLine.Count > 0)
                    PaintX(_listRealTimeLine.First(), 0);
            }
            //画Y轴
            if (_MultiYZShow)
            {
                int Index = 0;
                if (RightShowYZB)
                {
                    foreach (RealTimeLineOR obj in _listRealTimeLine)
                    {
                        PaintY(obj, Index);
                        Index++;
                    }
                }
                else
                {
                    int LineNumber = _listRealTimeLine.Count;
                    for (int i = LineNumber - 1; i >= 0; i--)
                    {
                        PaintY(_listRealTimeLine[i], LineNumber - (i + 1));
                        Index++;
                    }
                }
            }
            else
            {
                if (_listRealTimeLine.Count > 0)
                    PaintY(_listRealTimeLine.First(), 0);
            }
        }
        /// <summary>
        /// X轴线长度
        /// </summary>
        int XZLineHeight = 5;
        /// <summary>
        /// X轴文本显示高度
        /// </summary>
        int XZTxtHeight = 15;
        /// <summary>
        /// 设置XY轴开始位置
        /// </summary>
        private void SetXYStartPosition()
        {
            if (MultiXZShow)
            {
                _YStart = (XZLineHeight + XZTxtHeight) * _listRealTimeLine.Count;
            }
            else
            {
                _YStart = XZLineHeight + XZTxtHeight;
            }

            if (MultiYZShow)
            {

                _XStart = (YZLineWidth + YZTxtWidth) * _listRealTimeLine.Count;
            }
            else
            {
                _XStart = YZLineWidth + YZTxtWidth;
            }
        }

        /// <summary>
        /// 画X轴
        /// </summary>
        private void PaintX(RealTimeLineOR objOR, int LineNumber)
        {
            double Axzheight = XZTxtHeight + XZLineHeight;

            double xYStartP = _Canvas.Height - _YStart;
            xYStartP += Axzheight * LineNumber;

            double axzMainSize = xGridWidth / (this._XMainNumber + 1);
            //double xzY = yGridHeight;
            //x轴
            Line mXZ = new Line();
            mXZ.X1 = _XStart;
            mXZ.X2 = _XStart + xGridWidth;
            if (RightShowYZB)
            {
                mXZ.X1 = 0;
                mXZ.X2 = xGridWidth;
            }
            mXZ.Y1 = mXZ.Y2 = xYStartP;
            mXZ.Stroke = new SolidColorBrush(Common1.StringToColor(objOR.LineInfo.LineColor));
            mXZ.StrokeThickness = 2;
            _CanvasZ.Children.Add(mXZ);

            objOR.TextList.Clear();
            for (int imain = 0; imain < _XMainNumber + 2; imain++)
            {
                double _x = _XStart + imain * axzMainSize;
                if (RightShowYZB)
                {
                    _x = imain * axzMainSize;
                }
                Line mkd = new Line();
                mkd.X1 = mkd.X2 = _x;
                mkd.Y1 = xYStartP;
                mkd.Y2 = xYStartP + XZLineHeight;
                mkd.Stroke = new SolidColorBrush(Common1.StringToColor(objOR.LineInfo.LineColor));
                mkd.StrokeThickness = 1;
                _CanvasZ.Children.Add(mkd);

                //添加X轴标签                
                TextBlock tb = new TextBlock();
                //tb.Text = DateTime.Now.ToString("hh:MM:ss");
                objOR.TextList.Add(tb);
                tb.Foreground = new SolidColorBrush(Common1.StringToColor(objOR.LineInfo.LineColor));
                tb.SetValue(Canvas.LeftProperty, _x - 24);
                tb.SetValue(Canvas.TopProperty, xYStartP + XZLineHeight);
                _CanvasZ.Children.Add(tb);
            }
            objOR.SetXZTextBoxValue();
        }
        /// <summary>
        /// X轴线长度
        /// </summary>
        int YZLineWidth = 3;
        /// <summary>
        /// X轴文本显示高度
        /// </summary>
        int YZTxtWidth = 25;
        /// <summary>
        /// 画Y轴
        /// </summary>
        private void PaintY(RealTimeLineOR objOR, int LineNumber)
        {
            ///Y轴X坐标
            double yXStartP = (YZLineWidth + YZTxtWidth) * (LineNumber + 1);
            //右边显示Y轴
            double ayzMainSize = (yGridHeight - _YZStartPosition) / (this._YMainNumber + 1);

            if (_RightShowYZB)
            {
                yXStartP = xGridWidth + (YZLineWidth + YZTxtWidth) * LineNumber;
            }

            //Y轴
            Line mXZ = new Line();
            mXZ.X2 = mXZ.X1 = yXStartP;
            mXZ.Y1 = _YZStartPosition;
            mXZ.Y2 = yGridHeight;
            mXZ.Stroke = new SolidColorBrush(Common1.StringToColor(objOR.LineInfo.LineColor));
            mXZ.StrokeThickness = 2;
            _CanvasZ.Children.Add(mXZ);

            

            //文本显示位置
            double txtPosintionStart = (YZLineWidth + YZTxtWidth) * (LineNumber);
            if (RightShowYZB)
                txtPosintionStart = txtPosintionStart + xGridWidth + YZLineWidth;
            txtPosintionStart += 3;
            List<TextBlock> listYTxt = new List<TextBlock>();
            for (int imain = 0; imain < _YMainNumber + 2; imain++)
            {
                double _y = imain * ayzMainSize + _YZStartPosition;
                double _X = _YZStartPosition;

                if (RightShowYZB)
                    _X = yXStartP + YZLineWidth;
                else
                    _X = yXStartP - YZLineWidth;
                //y轴刻度线
                Line mkd = new Line();
                mkd.X1 = _X;
                mkd.X2 = yXStartP;
                mkd.Y1 = mkd.Y2 = _y;
                mkd.Stroke = new SolidColorBrush(Common1.StringToColor(objOR.LineInfo.LineColor));
                mkd.StrokeThickness = 1;
                _CanvasZ.Children.Add(mkd);

                //添加Y轴标签
                TextBlock tb = new TextBlock();
                tb.Foreground = new SolidColorBrush(Common1.StringToColor(objOR.LineInfo.LineColor));
               

                if (!_RightShowYZB)
                    _X = _X + YZTxtWidth + 2;
                
                tb.SetValue(Canvas.LeftProperty, txtPosintionStart);
                tb.SetValue(Canvas.TopProperty, _y - 5);
                _CanvasZ.Children.Add(tb);
                listYTxt.Add(tb);
            }
            objOR.TextYList = listYTxt;
            ShowYText(objOR);
        }

        private void ShowYText(RealTimeLineOR objOR)
        {
            string[] arrText = null;
            arrText = GetYText(objOR);
            if (objOR.TextYList != null && objOR.TextYList.Count > 0)
            {
                for (int imain = 0; imain < _YMainNumber + 2; imain++)
                {
                    objOR.TextYList[imain].Text = arrText[imain];
                }
            }
        }

        /// <summary>
        /// 获取Y轴文本值
        /// </summary>
        /// <returns></returns>
        private string[] GetYText(RealTimeLineOR obj)
        {
            string[] ytextArr = new string[_YMainNumber + 2];
            double half = obj.GetHalf();
            double max = int.Parse(obj.LineInfo.MaxValue) + half;
            double min = int.Parse(obj.LineInfo.MinValue) - half;

            double v = max - min;
            double vPer = v / (_YMainNumber + 1);
            ytextArr[0] = (Math.Round(max + obj.MoveYValue, obj.LineInfo.ValueDecimal)).ToString();
            for (int i = 1; i <= _YMainNumber; i++)
            {
                double val = Math.Round((max - (i * vPer) + obj.MoveYValue), obj.LineInfo.ValueDecimal);
                ytextArr[i] = val.ToString();
            }
            ytextArr[_YMainNumber + 1] = (Math.Round((min + obj.MoveYValue), obj.LineInfo.ValueDecimal)).ToString();
            return ytextArr;
        }
        private string[] GetYText()
        {
            string[] ytextArr = new string[_YMainNumber + 2];
            for (int i = 0; i < _YMainNumber + 2; i++)
            {
                ytextArr[i] = (i * 10).ToString();
            }
            return ytextArr;
        }
        #endregion
        
        #region 缩放处理
        private void HeadSF(double _SFper)
        {
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                if (obj.YZSFPer == _SFper)
                    continue;
                obj.YZSFPer = _SFper;
                PainXYZ();
                obj.SetYZValue();
                obj.ShowCurve();
            }
        }
        /// <summary>
        /// 原始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYS_Click(object sender, RoutedEventArgs e)
        {
            HeadSF(1);
        }
        /// <summary>
        /// 2倍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2B_Click(object sender, RoutedEventArgs e)
        {
            HeadSF(2);
        }
        /// <summary>
        /// 4倍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4B_Click(object sender, RoutedEventArgs e)
        {
            HeadSF(4);
        }
        /// <summary>
        /// 8倍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn8B_Click(object sender, RoutedEventArgs e)
        {
            HeadSF(8);
        }
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSF_Click(object sender, RoutedEventArgs e)
        {
            double sfPer = Convert.ToDouble(intputSFPer.DataValue);
            if (sfPer <= 0)
            {
                MessageBox.Show("请设置大于“1”的缩放倍数！");
                return;
            }
            HeadSF(sfPer);
        }
        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHF_Click(object sender, RoutedEventArgs e)
        {
            HeadSF(1);
        }
        #endregion

        #region 曲线显示隐藏处理
        private void RealLineShow_ChangeLineShow(object sender, EventArgs e)
        {
            if (e == null)
            {
                ShowOrHideAllLine(true);
                return;
            }
            RealLineArgs obj = (RealLineArgs)e;
            ShowOrHideAllLine(false);
            ShowALine(obj.Name);
        }
        /// <summary>
        ///  隐藏曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHide_Click(object sender, RoutedEventArgs e)
        {
            if (!HideALine(qxname.DataValue.ToString()))
                MessageBox.Show("找不到曲线：" + qxname.DataValue);
        }

        private RealTimeLineOR GetRealtimeLine()
        {
            string LineName = qxname.DataValue.ToString();
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                if (obj.LineInfo.LineName == LineName)
                {
                    return obj;
                }
            }
            return null;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           
            RealTimeLineOR selectLine = null;
            selectLine = GetRealtimeLine();
            if (selectLine == null)
            {
                 MessageBox.Show("找不到曲线：" + qxname.DataValue);
                 return;
            }
            _CanvasLine.Children.Remove(selectLine.PolyLine);
            _listRealTimeLine.Remove(selectLine);
            DrawLine();
        }
        

        /// <summary>
        /// 显示指定曲线
        /// </summary>
        /// <param name="LineName"></param>
        /// <returns></returns>
        private bool HideALine(string LineName)
        {
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                if (obj.LineInfo.LineName == LineName)
                {
                    _CanvasLine.Children.Remove(obj.PolyLine);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 显示指定曲线
        /// </summary>
        /// <param name="LineName"></param>
        /// <returns></returns>
        private bool ShowALine(string LineName)
        {
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                if (obj.LineInfo.LineName == LineName)
                {
                    if (_CanvasLine.FindName(obj.LineInfo.ID) == null)
                    {
                        _CanvasLine.Children.Add(obj.PolyLine);
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 显示或隐藏所有曲线
        /// </summary>
        /// <param name="LineName"></param>
        /// <returns></returns>
        private bool ShowOrHideAllLine(bool isShow)
        {
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                if (isShow)//显示
                {
                    if (_CanvasLine.FindName(obj.LineInfo.ID) == null)
                    {
                        _CanvasLine.Children.Add(obj.PolyLine);
                    }
                }
                else//隐藏
                {
                    if (_CanvasLine.FindName(obj.LineInfo.ID) != null)
                    {
                        _CanvasLine.Children.Remove(obj.PolyLine);
                    }
                }

            }
            return false;
        }

        /// <summary>
        /// 显示曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (!ShowALine(qxname.DataValue.ToString()))
            {
                MessageBox.Show("找不到曲线：" + qxname.DataValue);
            }
        }
        #endregion

        #region 最下面其它设置属性
        private void btnQxSX_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(qxname.DataValue.ToString()))
            {
                MessageBox.Show("请输入曲线名称。");
                return;
            }
            RealTimeLineOR selectLine = null;
            selectLine = GetRealtimeLine();
            if (selectLine == null)
            {
                MessageBox.Show("找不到曲线：" + qxname.DataValue);
                return;
            }

            CWLineProperty cwline = new CWLineProperty(selectLine,this);
            cwline.Show();
        }

        private void btnQXSJ_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(qxname.DataValue.ToString()))
            {
                MessageBox.Show("请输入曲线名称。");
                return;
            }
            RealTimeLineOR selectLine = null;
            selectLine = GetRealtimeLine();
            if (selectLine == null)
            {
                MessageBox.Show("找不到曲线：" + qxname.DataValue);
                return;
            }

            CWLineProperty cwline = new CWLineProperty(selectLine,true);
            cwline.Show();
        }

        #endregion

        #region 处理显示时间长度
        private void HeadTimeLen(int Len, string type)
        {
            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                if (obj.LineInfo.TimeLen == Len && type == obj.LineInfo.TimeLenType)
                {
                    continue;
                }
                
                obj.LineInfo.TimeLenType = type;
                obj.LineInfo.TimeLen = Len;
                obj.SetPointReMovePx();
                obj.SetXZTextBoxValueByMaxTime();

                obj.HeadXPosition();
                obj.ShowCurve();
            }            
        }

        private void btnMi1_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(1, "mm");
        }

        private void btnMi5_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(5, "mm");
        }

        private void btnMi15_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(15, "mm");
        }

        private void btnMi30_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(30, "mm");
        }

        private void btnhh1_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(1, "hh");
        }

        private void btnhh6_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(6, "hh");
        }

        private void btnhh24_Click(object sender, RoutedEventArgs e)
        {
            HeadTimeLen(24, "hh");
        }

        #endregion

        private void btnSD_Click(object sender, RoutedEventArgs e)
        {
            string strStart = string.Format("{0}-{1}-{2} {3}:{4}:{5}",

            Convert.ToInt32(this.inputYear.DataValue),
            Convert.ToInt32(this.inputMonth.DataValue),
            Convert.ToInt32(this.inputDay.DataValue),

            Convert.ToInt32(this.inputHour.DataValue),
            Convert.ToInt32(this.inputMi.DataValue),
            Convert.ToInt32(this.inputSec.DataValue));

            string strendTime = string.Format("{0}-{1}-{2} {3}:{4}:{5}",
            Convert.ToInt32(this.inputYear.DataValue),
            Convert.ToInt32(this.inputMonth.DataValue),
            Convert.ToInt32(this.inputDay.DataValue),
            Convert.ToInt32(this.inputEHour.DataValue),
            Convert.ToInt32(this.inputEMi.DataValue),
            Convert.ToInt32(this.inputESec.DataValue));

            DateTime StartTime = Convert.ToDateTime(strStart);
            DateTime EndTime = Convert.ToDateTime(strendTime);
            if (StartTime > EndTime)
            {
                ShowMsg("开始时间不能大于，结束时间");
                return;
            }

            TimeSpan t = DateTime.Now - StartTime;
            if (t.Days > 1)
            {
                ShowMsg("请设置正确的时间区间，不能超过24小时！");
            }

            foreach (RealTimeLineOR obj in _listRealTimeLine)
            {
                obj.ISShowValue = false;
                obj.XZMaxTime = EndTime;
                obj.LineInfo.TimeLen = (t.Hours * 60 + t.Minutes) * 60 + t.Seconds;
                obj.LineInfo.TimeLenType = "s";
                obj.HeadXPosition();
                obj.ShowCurve();
            }
            
        }

        private void ShowMsg(string str)
        {
            MessageBox.Show(str);

           
        }
    }
}
