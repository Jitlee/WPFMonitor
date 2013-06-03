using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;

namespace MonitorSystem.Other
{
    public partial class RealtimeProperty : Window
    {
        public RealtimeProperty()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        RealTimeT _RealTimeData;
        /// <summary>
        /// 
        /// </summary>
        public RealTimeT RealTimeData
        {
            get { return _RealTimeData; }
            set { _RealTimeData = value; }
        }

        ObservableCollection<t_Element_RealTimeLine> _ListEletement = new ObservableCollection<t_Element_RealTimeLine>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<t_Element_RealTimeLine> ListEletement
        {
            get { return _ListEletement; }
            set { _ListEletement = value; }
        }

        public void Init()
        {
            if (_RealTimeData == null)
                return;
            //X轴
            this.XISSGShow.IsChecked = _RealTimeData.XISSGShow;         //是否X轴栅格显示
            this.XMainNumber.SelectedIndex = _RealTimeData.XMainNumber; //X主分度数
            this.XMainColor.Color = _RealTimeData.XMainColor;           //X主分度颜色
            this.XPriNumber.SelectedIndex = _RealTimeData.XPriNumber;   //X次分度数
            this.XPriColor.Color = _RealTimeData.XPriColor;             //X度次分颜色

            //Y轴
            this.YISSGShow.IsChecked = _RealTimeData.YISSGShow;         //是否Y轴栅格显示
            this.YMainNumber.SelectedIndex = _RealTimeData.YMainNumber; //Y主分度数
            this.YMainColor.Color = _RealTimeData.YMainColor;           //Y主分度颜色
            this.YPriNumber.SelectedIndex = _RealTimeData.YPriNumber;   //Y次分度数
            this.YPriColor.Color = _RealTimeData.YPriColor;             //Y度次分颜色

            //其它颜色		
            this.ISShowBorder.IsChecked = _RealTimeData.ISShowBorder; //显示边框
            this.BorderColor.Color = _RealTimeData.BorderColor;      //边框

            this.ISShowGridBack.IsChecked = _RealTimeData.ISShowGridBack; //显示背景		
            this.GridBackColor.Color = _RealTimeData.GridBackColor; //背景颜色		

            this.ISShowCursor.IsChecked = _RealTimeData.ISShowCursor; //显示游标		
            this.CursorColor.Color = _RealTimeData.CursorColor; //游标颜色		

            this.ISShowTime.IsChecked = _RealTimeData.ISShowTime; //显示时间		
            this.TimeColor.Color = _RealTimeData.TimeColor; //时间颜色

            this.UsePerZB.IsChecked = _RealTimeData.UsePerZB;//采用百分比坐标
            this.NoUseDataMove.IsChecked = _RealTimeData.NoUseDataMove;//无效数据移出
            this.DoubleClickShowSet.IsChecked = _RealTimeData.DoubleClickShowSet;//双击显示设置框
            this.RightShowYZB.IsChecked = _RealTimeData.RightShowYZB;           //右显示Y轴坐标

            this.MultiXZShow.IsChecked = _RealTimeData.MultiXZShow; //多X轴显示
            this.MultiYZShow.IsChecked = _RealTimeData.MultiYZShow; //多Y轴显示
            this.IsShowLegend.IsChecked = _RealTimeData.IsShowLegend;   //显示图例
            //_RealTimeData.ShowLegend;
            //this.InfoLWidth.Text = _RealTimeData.InfoLWidth.ToString(); //信息栏宽度

            //放缩设置
            //this.MouseDrawEnlare.IsChecked = _RealTimeData.MouseDrawEnlare;// 鼠标拖动放大		
            //this.XZEnlare.IsChecked = _RealTimeData.XZEnlare;// X轴放大		
            //this.YZEnlare.IsChecked = _RealTimeData.YZEnlare;// Y轴放大		
            this.MouseDrawMove.IsChecked = _RealTimeData.MouseDrawMove;// 鼠标拖动移动		
            this.XZMove.IsChecked = _RealTimeData.XZMove;// X轴移动		
            this.YZMove.IsChecked = _RealTimeData.YZMove;// Y轴移动

            //线信息
            foreach (RealTimeLineOR obj in _RealTimeData.ListRealTimeLine)
            {
                ListEletement.Add(obj.LineInfo);
            }
        }

        /// <summary>
        /// 保存设置的数据
        /// </summary>
        private void SaveDate()
        {
            //X轴
            _RealTimeData.XISSGShow = this.XISSGShow.IsChecked.Value;     //是否X轴栅格显示
            _RealTimeData.XMainNumber = this.XMainNumber.SelectedIndex;   //X主分度数
            _RealTimeData.XMainColor = this.XMainColor.Color; //X主分度颜色
            _RealTimeData.XPriNumber = this.XPriNumber.SelectedIndex;    //X次分度数
            _RealTimeData.XPriColor = this.XPriColor.Color; //X度次分颜色

            //Y轴
            _RealTimeData.YISSGShow = this.YISSGShow.IsChecked.Value;     //是否Y轴栅格显示
            _RealTimeData.YMainNumber = this.YMainNumber.SelectedIndex;   //Y主分度数
            _RealTimeData.YMainColor = this.YMainColor.Color; //Y主分度颜色
            _RealTimeData.YPriNumber = this.YPriNumber.SelectedIndex;    //Y次分度数
            _RealTimeData.YPriColor = this.YPriColor.Color; //Y度次分颜色

            //其它颜色
            _RealTimeData.ISShowBorder = this.ISShowBorder.IsChecked.Value; //显示边框
            _RealTimeData.BorderColor = this.BorderColor.Color;      //边框

            _RealTimeData.ISShowGridBack = this.ISShowGridBack.IsChecked.Value; //显示背景		
            _RealTimeData.GridBackColor = this.GridBackColor.Color; //背景颜色		

            _RealTimeData.ISShowCursor = this.ISShowCursor.IsChecked.Value; //显示游标		
            _RealTimeData.CursorColor = this.CursorColor.Color; //游标颜色		

            _RealTimeData.ISShowTime = this.ISShowTime.IsChecked.Value; //显示时间		
            _RealTimeData.TimeColor = this.TimeColor.Color; //时间颜色


            _RealTimeData.UsePerZB = this.UsePerZB.IsChecked.Value;//采用百分比坐标
            _RealTimeData.NoUseDataMove = this.NoUseDataMove.IsChecked.Value;//无效数据移出
            _RealTimeData.DoubleClickShowSet = this.DoubleClickShowSet.IsChecked.Value;//双击显示设置框
            _RealTimeData.RightShowYZB = this.RightShowYZB.IsChecked.Value;           //右显示Y轴坐标

            _RealTimeData.MultiXZShow = this.MultiXZShow.IsChecked.Value; //多X轴显示
            _RealTimeData.MultiYZShow = this.MultiYZShow.IsChecked.Value; //多Y轴显示
            _RealTimeData.IsShowLegend = this.IsShowLegend.IsChecked.Value;   //显示图例
            //_RealTimeData.ShowLegend;
            //_RealTimeData.InfoLWidth = int.Parse(this.InfoLWidth.Text); //信息栏宽度

            //放缩设置
            //_RealTimeData.MouseDrawEnlare = this.MouseDrawEnlare.IsChecked.Value;// 鼠标拖动放大		
            //_RealTimeData.XZEnlare = this.XZEnlare.IsChecked.Value;// X轴放大		
            //_RealTimeData.YZEnlare = this.YZEnlare.IsChecked.Value;// Y轴放大		
            _RealTimeData.MouseDrawMove = this.MouseDrawMove.IsChecked.Value;// 鼠标拖动移动		
            _RealTimeData.XZMove = this.XZMove.IsChecked.Value;// X轴移动		
            _RealTimeData.YZMove = this.YZMove.IsChecked.Value;// Y轴移动
        }

        List<t_Element_RealTimeLine> _lines;
        private void AdminLine()
        {
            ElementRealTimeLineDA da = new ElementRealTimeLineDA();
            //移出
            if (DeleteRealTime.Count > 0)
            {
                if (null == _lines)
                {
                    _lines = da.selectAllDate();
                }
                foreach (t_Element_RealTimeLine obj in DeleteRealTime)
                {
                    //_RealTimeData.
                    RealTimeLineOR _line = GetRealLine(obj);
                    if (_line != null)
                    {
                        _RealTimeData.ListRealTimeLine.Remove(_line);
                        _RealTimeData._CanvasLine.Children.Remove(_line.PolyLine);
                        if (_RealTimeData.ElementState == MonitorSystemGlobal.ElementSate.Save)
                        {
                            var v = _lines.Where(a => a.ID == obj.ID).FirstOrDefault();
                            if (null != v)
                            {
                                _lines.Remove(v);
                                da.Delete(v.ID);
                            }
                        }
                    }
                }
            }
            //修改、添加
            foreach (t_Element_RealTimeLine obj in ListEletement)
            {
                RealTimeLineOR _line = GetRealLine(obj);
                if (_line != null)
                {
                    _line.LineInfo = obj;
                }
                else
                {
                    RealTimeLineOR _data = new RealTimeLineOR(obj);
                    _RealTimeData.ListRealTimeLine.Add(_data);
                    if (_RealTimeData.ElementState == MonitorSystemGlobal.ElementSate.Save)
                    {
                        obj.ScreenID = _RealTimeData.ScreenElement.ScreenID;
                        obj.ElementID = _RealTimeData.ScreenElement.ElementID;
                        da.Insert(obj);
                    }
                }
            }
        }

        private RealTimeLineOR GetRealLine(t_Element_RealTimeLine obj)
        {
            foreach (RealTimeLineOR line in _RealTimeData.ListRealTimeLine)
            {
                if (line.LineInfo.ID == obj.ID)
                    return line;
            }
            return null;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            AdminLine();

            if (null == _lines)
            {
                _lines = new ElementRealTimeLineDA().selectAllDate();
            }

            if (_lines.Count > 1)
            {
                t_Element_RealTimeLine obj = _lines.First();
                foreach (t_Element_RealTimeLine t in _lines)
                {
                    //RealTimeLineOR _line = GetRealLine(obj);
                   
                        if (!_RealTimeData.MultiXZShow)
                        {
                            t.TimeLen = obj.TimeLen;
                            t.TimeLenType = obj.TimeLenType;

                            t.LineCYZQLent = obj.LineCYZQLent;
                            t.LineCYZQType = obj.LineCYZQType;
                        }

                        if (!_RealTimeData.MultiYZShow)
                        {
                            t.MaxValue = obj.MaxValue;
                            t.MinValue = obj.MinValue;
                        }
                }
            }

            SaveDate();
            
            _RealTimeData._CanvasPoint.Children.Clear();
            _RealTimeData.PaintBasicInfo();

            this.DialogResult = true;
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #region 鼠标移动处理
        private void MouseDrawEnlare_Checked(object sender, RoutedEventArgs e)
        {
            //XZEnlare.IsChecked = true;
            //YZEnlare.IsChecked = true;
            //XZEnlare.IsEnabled = true;
            //YZEnlare.IsEnabled = true;

            MouseDrawMove.IsChecked = false;
            XZMove.IsChecked = false;
            YZMove.IsChecked = false;
            XZMove.IsEnabled = false;
            YZMove.IsEnabled = false;
        }

        private void MouseDrawMove_Checked(object sender, RoutedEventArgs e)
        {
            XZMove.IsChecked = true;
            YZMove.IsChecked = true;
            XZMove.IsEnabled = true;
            YZMove.IsEnabled = true;

            //MouseDrawEnlare.IsChecked = false;
            //XZEnlare.IsChecked = false;
            //YZEnlare.IsChecked = false;
            //XZEnlare.IsEnabled = false;
            //YZEnlare.IsEnabled = false;
        }
        #endregion

        #region 曲线处理
      

       

        t_Element_RealTimeLine _RealTime = new t_Element_RealTimeLine();
        List<t_Element_RealTimeLine> DeleteRealTime = new List<t_Element_RealTimeLine>();
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            _RealTime = new t_Element_RealTimeLine();
            _RealTime.ID = "N" + Guid.NewGuid().ToString("N");
            linePro.SaveRealTimeLineOR(_RealTime);
            //SaveRealTimeLineOR();
            //名称
            foreach (t_Element_RealTimeLine obj in ListEletement)
            {
                if (obj.LineName == _RealTime.LineName)
                {
                    MessageBox.Show(string.Format("名称：{0}，已经存在！", _RealTime.LineName));
                    return;
                }
            }
            ListEletement.Add(_RealTime);
        }

        private void btnAlert_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataList.SelectedItem == null)
            {
                MessageBox.Show("请选择移出项！");
                return;
            }
          _RealTime=  linePro.SaveRealTimeLineOR();
            
            //SaveRealTimeLineOR();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataList.SelectedItem == null)
            {
                MessageBox.Show("请选择移出项！");
                return;
            }
            var v = dgDataList.SelectedItem as t_Element_RealTimeLine;
            ListEletement.Remove(v);
            DeleteRealTime.Add(v);
        }
        #endregion

        private void dgDataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = ((DataGrid)sender).SelectedItem as t_Element_RealTimeLine;
            linePro.SetRealTimeLineOR(v);
        }

               


    }
}

