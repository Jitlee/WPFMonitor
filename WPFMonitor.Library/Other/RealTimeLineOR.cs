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
using System.Windows.Threading;
using WPFMonitor.Model.ZTControls;

namespace MonitorSystem.Other
{

    #region 定义鼠标X，Y 坐标值，及该点坐标记录值、记录时间
    /// <summary>
    /// 定义鼠标X，Y 坐标值，及该点坐标记录值、记录时间
    /// </summary>
    struct CoordinatesValue
    {
        /// <summary>
        /// 缩放比例
        /// </summary>
        public double SFPer;
        public double X;
        public double Y;
        public double Value;
        //public Point PointPosi;
        public System.DateTime time;
    }
    #endregion

    public class RealTimeLineOR
    {
        t_Element_RealTimeLine _LineInfo;
        /// <summary>
        /// 线参数信息
        /// </summary>
        public t_Element_RealTimeLine LineInfo
        {
            get { return _LineInfo; }
            set { _LineInfo = value;
                GetTimeShowELen();
            }
        }

        RealTimeT _RealtimeObj;

        public RealTimeT RealtimeObj
        {
            get { return _RealtimeObj; }
            set { _RealtimeObj = value; }
        }

        DispatcherTimer timerSetValue = new DispatcherTimer();
        
        public RealTimeLineOR(t_Element_RealTimeLine mLine)
        {
            _LineInfo = mLine;
            GetTimeShowELen();

            _YValue = GetLineMinValue();

            _PolyLine.Stroke = new SolidColorBrush(Common1.StringToColor(_LineInfo.LineColor));
            _PolyLine.StrokeThickness = 0.5;
            _PolyLine.Points = pc;
            _PolyLine.SetValue(Canvas.ZIndexProperty, 999);
            _PolyLine.Name = _LineInfo.ID;
            SetPolyLine();
            //曲线点
            noteMessages = new CoordinatesValue[maxNote];
            timerSetValue.Interval = new TimeSpan(0, 0, GetCYTimeLen());
            timerSetValue.Tick += (sender, obj) =>
            {
                AddNewValue(_YValue);
                ShowCurve();
            }; 
            timerSetValue.Start();
        }

        public void SetYValue(double mYValue)
        {
            _YValue = mYValue;
        }

        public void Stop()
        {
            RemovePoint();
        }

        Random rd = new Random();
        public void SetPolyLine()
        {
            //LineStyle 样式
            DoubleCollection dc = new DoubleCollection();
            double _LienWidth = 0.5;
            switch (_LineInfo.LineStyle)
            {
                case 0:
                    break;
                case 1:
                    dc = new DoubleCollection() { 20, 5 };
                    break;
                case 2:
                    dc = new DoubleCollection() { 10, 4, 3, 4 };
                    break;
                case 3:
                    dc = new DoubleCollection() { 10, 4, 3, 4, 3, 4 };
                    break;
                case 4:
                    dc = new DoubleCollection() { 4, 3 };
                    break;
                case 5:
                    _LienWidth = 1;
                    break;
                case 6:
                    _LienWidth = 2;
                    break;
                case 7:
                    _LienWidth = 3;
                    break;
                case 8:
                    _LienWidth = 0;
                    break;

            }
            //new DoubleCollection { 2.0, 2.0 };
            _PolyLine.StrokeThickness = _LienWidth;

            _PolyLine.StrokeDashArray = dc;
            _PolyLine.Stroke = new SolidColorBrush(Common1.StringToColor(_LineInfo.LineColor));
            //_LineInfo.LineStyle
        }

        /// <summary>
        /// X轴最大时间
        /// </summary>
        private DateTime _XZMaxTime = DateTime.Now;
        /// <summary>
        /// 显示最大时间
        /// </summary>
        public DateTime XZMaxTime
        {
            get { return _XZMaxTime; }
            set { _XZMaxTime = value; }
        }
       

        private bool _ISShowValue = true;
        /// <summary>
        /// 显示值,新添加值的时候，是否对曲线进行移动
        /// </summary>
        public bool ISShowValue
        {
            get { return _ISShowValue; }
            set { _ISShowValue = value; }
        }
       
       
        double _YValue = 0.0;
        /// <summary>
        /// 当前Y轴值
        /// </summary>
        public double YValue
        {
            get { return _YValue; }
            set { _YValue = value; }
        }

        
        RealLineShow _TitleShowInfo;
        /// <summary>
        /// 实时曲线基本信息显示
        /// </summary>
        public RealLineShow TitleShowInfo
        {
            get { return _TitleShowInfo; }
            set { _TitleShowInfo = value; }
        }


        #region 变量 -X轴信息|函数-设置Text值

        List<TextBlock> _TextList = new List<TextBlock>();
        /// <summary>
        /// X轴文本框显示信息
        /// </summary>
        public List<TextBlock> TextList
        {
            get { return _TextList; }
            set { _TextList = value; }
        }

        List<TextBlock> _TextYList = new List<TextBlock>();
        /// <summary>
        /// X轴文本框显示信息
        /// </summary>
        public List<TextBlock> TextYList
        {
            get { return _TextYList; }
            set { _TextYList = value; }
        }

        //开始时间
        private DateTime _StartTime = DateTime.Now;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }
     
        /// <summary>
        /// 面版可显示点数量,根据取值宽围/采样频率
        /// </summary>
        private int _PicShowPointNumber = 50;
        /// <summary>
        /// 定义曲线移动距离
        /// </summary>
        private double curveRemove = 5;
        /// <summary>
        /// 设置可显示点数，移动宽度
        /// </summary>
        public void SetPointReMovePx()
        {
            GetTimeShowELen();
            _PicShowPointNumber = _TimeShowELen / GetCYTimeLen();//取值宽围/采样频率
            curveRemove = _PicCurveWidth / _PicShowPointNumber;
           
        }

        /// <summary>
        /// 采样时间长度
        /// </summary>
        /// <returns></returns>
        private int GetCYTimeLen()
        {
            int len = 5;
            int CYZQLent= int.Parse(_LineInfo.LineCYZQLent);
            if (_LineInfo.LineCYZQType == "ss")
            {
                len = CYZQLent;
            }
            else if (_LineInfo.LineCYZQType == "mm")
            {
                len = CYZQLent * 60;
            }
            else if (_LineInfo.LineCYZQLent == "hh")
            {
                len = CYZQLent * 60 * 60;
            }
            return len;
        }

        /// <summary>
        /// 一秒移宽度
        /// </summary>
        private double _MinMoveWidth;
        /// <summary>
        /// 最小移动宽度
        /// </summary>
        public double MinMoveWidth
        {
            get { return _MinMoveWidth; }
            set { _MinMoveWidth = value; }
        }

        /// <summary>
        /// 时间显示,长度,(S)秒,即X轴显示多少秒的线
        /// </summary>
        int _TimeShowELen = 5;
        /// <summary>
        /// 时间显示长度
        /// </summary>
        /// <returns></returns>
        private int  GetTimeShowELen()
        {            
            if (_LineInfo.TimeLenType == "ss")
            {
                _TimeShowELen = _LineInfo.TimeLen;
            }
            else if (_LineInfo.TimeLenType == "mm")
            {
                _TimeShowELen = _LineInfo.TimeLen * 60;
            }
            else if (_LineInfo.TimeLenType == "hh")
            {
                _TimeShowELen = _LineInfo.TimeLen * 60 * 60;
            }
            _MinMoveWidth = PicCurveWidth / _TimeShowELen;
            return _TimeShowELen;
        }
        
        /// <summary>
        /// 设置X轴TextBox
        /// </summary>
        public void SetXZTextBoxValue()
        {
           _StartTime= DateTime.Now;
           if (XZMaxTime > _StartTime)
               return;
        
           if (_TextList == null)
               return;
           if (_TextList.Count == 0)
               return;

           int timeLenPer = _TimeShowELen / (_TextList.Count - 1);
           int txtCount = _TextList.Count;
        
           for (int i = 0; i < _TextList.Count; i++)
           {               
               DateTime dt = _StartTime.AddSeconds(-(timeLenPer * (txtCount-i-1)));
               _TextList[i].Text = dt.ToString(_LineInfo.ShowFormat);
           }
        }

        /// <summary>
        /// X轴设置了最大值，根据X轴设置，设置
        /// </summary>
        public void SetXZTextBoxValueByMaxTime()
        {
            if (_TextList == null)
                return;
            if (_TextList.Count == 0)
                return;

            int timeLenPer = _TimeShowELen / (_TextList.Count - 1);
            int txtCount = _TextList.Count;
           
            for (int i = 0; i < _TextList.Count; i++)
            {
                DateTime dt = XZMaxTime.AddSeconds(-(timeLenPer * (txtCount - i - 1)));
                _TextList[i].Text = dt.ToString(_LineInfo.ShowFormat);
            }
            
        }

        #endregion

        /// <summary>
        /// 鼠标X，Y 坐标值，及该点坐标记录值、记录时间（数组）
        /// </summary>
        private CoordinatesValue[] noteMessages;

        /// <summary>
        /// 当前鼠标 X，Y坐标记录数组下标值
        /// </summary>
        private int noteNow = 0;
        /// <summary>
        /// 当前数据点
        /// </summary>
        public int NoteNow
        {
            get { return noteNow; }
            set { noteNow = value; }
        }

        /// <summary>
        /// 曲线节点数据最大存储量
        /// </summary>
        private int maxNote = 2000;
        

        //显示面版，宽
        private double _PicCurveWidth = 200;
        /// <summary>
        /// 显示Canvers宽度
        /// </summary>
        public double PicCurveWidth
        {
            get { return _PicCurveWidth; }
            set { _PicCurveWidth = value;
                SetPointReMovePx();
                GetTimeShowELen();
            }
        }

        private double _picCurveShowHeight = 0;
        /// <summary>
        /// 显示Canvers高度
        /// </summary>
        public double PicCurveShowHeight
        {
            get { return _picCurveShowHeight; }
            set { _picCurveShowHeight = value; }
        }

        private double _YZSFPer=1;
        /// <summary>
        /// Y轴缩放比例
        /// </summary>
        public double YZSFPer
        {
            get { return _YZSFPer; }
            set { _YZSFPer = value; }
        }
        
        private double _MaxValue = 0;
        /// <summary>
        /// 显示值 最大值
        /// </summary>
        public double MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }

        private double _MinValue = 0;
        /// <summary>
        /// 显示值 最小值
        /// </summary>
        public double MinValue
        {
            get { return _MinValue; }
            set { _MinValue = value; }
        }
        
        #region 自动将最新采样数值添加到数组、显示
        /// <summary>
        /// 自动将最新采样数值添加到数组
        /// </summary>
        /// <param name="newValue">最新采样数值</param>
        public void AddNewValue(double newValue)
        {
            newValue = Math.Round(newValue, LineInfo.ValueDecimal); 
            //处理最大值，及最小值
            if (newValue > _MaxValue)
                _MaxValue = newValue;

            if (newValue < _MinValue)
                _MinValue = newValue;
            //先判断数组下标
            DateTime NowTime=DateTime.Now;

            double NowXValue = 0.0;
            if (NowTime > XZMaxTime && ISShowValue)//处理最大时间，但要在非移动的情况下才赋值
            {
                XZMaxTime = NowTime;
            }
            NowXValue = GetXValue(NowTime);
            if (this.noteNow >= this.maxNote - 1)
            {
                //数组已经存满数值
                for (int i = 0; i < this.noteNow; i++)
                {
                        this.noteMessages[i] = this.noteMessages[i + 1];
                        this.noteMessages[i].X = GetXValue(this.noteMessages[i].time);
                }
                this.noteMessages[this.noteNow].SFPer = _YZSFPer;
                this.noteMessages[this.noteNow].Value = newValue;
                this.noteMessages[this.noteNow].time = NowTime;
                this.noteMessages[this.noteNow].X = NowXValue;
                this.noteMessages[this.noteNow].Y = GetYValue(newValue);
            }
            else
            {
                this.noteMessages[this.noteNow].SFPer = _YZSFPer;
                this.noteMessages[this.noteNow].Value = newValue;
                this.noteMessages[this.noteNow].time = NowTime;
                this.noteMessages[this.noteNow].X = NowXValue;
                this.noteMessages[this.noteNow].Y = GetYValue(newValue);
               

                //数组未存满数值
                for (int i = this.noteNow - 1; i >= 0; i--)
                {
                    this.noteMessages[i].X = noteMessages[i + 1].X - curveRemove;
                }
                this.noteNow++;
            }
            if (_TitleShowInfo != null)
                _TitleShowInfo.SetDataValue(newValue);
        }


        /// <summary>
        /// 根据X最大时间来取值,
        /// 即：当前时间与最大时间的距离
        /// </summary>
        /// <param name="Time"></param>
        private double GetXValue(DateTime _Time)
        {
            TimeSpan t = _Time - XZMaxTime;
            int m = t.Hours * 60 * 60 + t.Minutes * 50 + t.Seconds;
            return this.PicCurveWidth + m * _MinMoveWidth;
        }
        #endregion

        /// <summary>
        /// 获取Y移动一单元，最小值，的高度
        /// </summary>
        /// <returns></returns>
        public double GetYMinPx()
        {
            return _picCurveShowHeight / (GetLineMaxValue() - GetLineMinValue());
        }
       
        #region 缩放处理

        /// <summary>
        /// 获取缩放值,
        /// 缩放后，处理X最大值，Y最大值
        /// </summary>
        /// <returns></returns>
        public double GetHalf()
        {
            double half = 0;
            if (_YZSFPer != 1)
                half = ((GetLineMaxValue() - GetLineMinValue()) * (_YZSFPer - 1)) / 2;
            return half;
        }
        /// <summary>
        /// 获取根据值获取Y轴的坐标
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private int GetYValue(double newValue)
        {
            double half = GetHalf();
            double max = GetLineMaxValue() + half;
            double min = GetLineMinValue() - half;

            double per = (newValue - min) / (max - min);
            double y = _picCurveShowHeight * (1 - per);
            return (int)y;
        }

        /// <summary>
        /// 重新计算Y轴坐标位置
        /// </summary>
        public void SetYZValue()
        {
            for (int i = 0; i <= this.noteNow - 1; i++)
            { 
                    noteMessages[i].SFPer = _YZSFPer;
                    noteMessages[i].Y = GetYValue(noteMessages[i].Value);
            }
        }
        #endregion

        #region 处理线的最大值和最小值
        /// <summary>
        /// Y轴移动大小
        /// </summary>
        double _MoveYValue = 0;
        /// <summary>
        /// Y轴移动大小
        /// </summary>
        public double MoveYValue
        {
            get { return _MoveYValue; }
            set { _MoveYValue = value; }
        }

        private double GetLineMaxValue()
        {

            return double.Parse(_LineInfo.MaxValue) + MoveYValue;
        }

        private double GetLineMinValue()
        {
            return double.Parse(_LineInfo.MinValue) + MoveYValue;
        }
        #endregion

        #region  获取游标值
        /// <summary>
        /// 根据当前游标X轴位值获取时间
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public string GetXCursorValue(double X)
        {
            double per = X / _PicCurveWidth;
            int TimeLen = (int)(_TimeShowELen * (1 - per));

            return XZMaxTime.AddSeconds(-TimeLen).ToString("HH:mm:ss");
        }
               
        /// <summary>
        /// 根据当前游标X轴位值获取时间
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public string GetYCursorValue(double Y)
        {
            double half = GetHalf();
            //if (_YZSFPer != 1)
            //    half = ((GetLineMaxValue() - GetLineMinValue()) * (_YZSFPer - 1)) / 2;
            double max = GetLineMaxValue() + half;
            double min = GetLineMinValue() - half;

            double per = Y / _picCurveShowHeight;

            var TimeLen = (max - min) * (1 - per);
            return ((int)(TimeLen + min)).ToString();
        }
        #endregion

        #region 移动
        public void HeadMove(double XLen, bool XIsAdd, double YLen)
        {
            //处理Y轴移动
            if (YLen > 1 || (YLen * -1) > 1)//大于一像素
            {
                double MoveNumber = YLen / GetYMinPx();
                MoveYValue += MoveNumber;
                SetYZValue();
            }
            //处理X轴移动           
            HeadXZMoveTime(XLen, XIsAdd);
            HeadXPosition();
        }

        public void HeadXZMoveTime(double xlen, bool XIsAdd)
        {
            //移动与当前显示宽度的占比
            double per = xlen / _PicCurveWidth;
            //应移动多长的位置
            int moveTimeLen = (int)(_TimeShowELen * per);
            if (XIsAdd)
                XZMaxTime = XZMaxTime.AddSeconds(moveTimeLen);
            else
                XZMaxTime = XZMaxTime.AddSeconds(-moveTimeLen);

            SetXZTextBoxValueByMaxTime();
        }
        #endregion

        #region 曲线
        Polyline _PolyLine = new Polyline();
        /// <summary>
        /// 画曲线
        /// </summary>
        public Polyline PolyLine
        {
            get { return _PolyLine; }
            set { _PolyLine = value; }
        }

        PointCollection pc = new PointCollection();
        /// <summary>
        /// 刷新背景网格线，显示曲线
        /// </summary>
        public void ShowCurve()
        {
            if (this.noteNow > 1)
            {
                
                pc.Clear();
                if (_LineInfo.LineStyle == 8)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i <= noteNow - 1; i++)
                    {

                        if (noteMessages[i].X > (PicCurveWidth + 50))
                            break;


                        if (this.noteMessages[i].X > 0 || ((i + 1) <= noteNow && this.noteMessages[i + 1].X > 0))
                        {
                            Point p = new Point(this.noteMessages[i].X, this.noteMessages[i].Y);
                            pc.Add(p);
                        }
                    }
                    //显示点
                    if (_LineInfo.LinePointBJ != 0)
                    {
                        RemovePoint();

                        for (int i = 0; i <= noteNow - 1; i++)
                        {
                            if (noteMessages[i].X > (PicCurveWidth + 50))
                                break;
                            if (this.noteMessages[i].X > 0 || ((i + 1) <= noteNow && this.noteMessages[i + 1].X > 0))
                            {
                                //画点
                                Rectangle ell = new Rectangle();
                                ell.Width = 3;
                                ell.Height = 3;
                                ell.Name = _LineInfo.ID + i.ToString();
                                ell.Fill = new SolidColorBrush(Colors.Red);
                                if (_LineInfo.LinePointBJ == 2)
                                {
                                    ell.RadiusX = ell.RadiusY = 3;
                                }

                                ell.SetValue(Canvas.LeftProperty, this.noteMessages[i].X - 1.5);
                                ell.SetValue(Canvas.TopProperty, this.noteMessages[i].Y - 1.5);
                                try
                                {
                                    RealtimeObj._CanvasPoint.Children.Add(ell);
                                }
                                catch (Exception) { }
                            }
                        }
                    }
                }
            }
            if (_ISShowValue)
                SetXZTextBoxValue();
        }

        public void RemovePoint()
        {
            //移出当前线，已添加的点
            int Count = _RealtimeObj._CanvasPoint.Children.Count;
            List<Rectangle> removeEll = new List<Rectangle>();
            for (int c = 0; c < _RealtimeObj._CanvasPoint.Children.Count; c++)
            {
                if (_RealtimeObj._CanvasPoint.Children[c] is Rectangle)
                {
                    Rectangle ee = _RealtimeObj._CanvasPoint.Children[c] as Rectangle;
                    if (ee.Name.IndexOf(_LineInfo.ID) == 0)
                    {
                        removeEll.Add(ee);
                    }
                }
            }

            foreach (Rectangle ee in removeEll)
            {
                _RealtimeObj._CanvasPoint.Children.Remove(ee);
            }
        }

        public string GetXValueList()
        {
            string Value = "";
            for (int i = 0; i <= noteNow - 1; i++)
            {
                Value += string.Format("x:{0}\r\n",noteMessages[i].X);
            }
            return Value;
        }


        //public void ShowCurve()
        //{
        //    ShowCurve(noteNow);
        //}

        /// <summary>
        /// 处理曲线，X轴显示坐标
        /// </summary>
        public void HeadXPosition()
        {
            noteMessages[noteNow - 1].X = GetXValue(noteMessages[noteNow - 1].time);

            for (int i = this.noteNow - 2; i >= 0; i--)
            {
                this.noteMessages[i].X = noteMessages[i+1].X - curveRemove;
            }
        }
        #endregion
    }
}
