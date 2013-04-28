using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DockingLibrary;
using WPFMonitor.Core.ViewModel.SerMonitor;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using System.Data;
using WPFMonitor.DAL.SerMonitor;

namespace WPFMonitor.View.SerMonitor
{
    /// <summary>
    /// HistoryValueView.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryValueView : DockableContent
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public HistoryValueView()
        {
            InitializeComponent();
            this.DataContext = new HistoryValueViewModel();

            StartDate = Convert.ToDateTime("2011-10-01");
            EndDate = Convert.ToDateTime("2011-10-15");

            Init();
            this.DataContext = this;
        }
        private void Init()
        {
            DeviceORList = new ObservableCollection<DeviceOR>();

            StationDA _staDA = new StationDA();
            var v= _staDA.selectAllStation();
            StationORList=new ObservableCollection<StationOR>();
            foreach (StationOR obj in v)
            {
                StationORList.Add(obj);
            }
        }

        public ObservableCollection<StationOR> StationORList { get; set; }
        public StationOR SelectStationOR { get; set; }

        //设备
        public ObservableCollection<DeviceOR> DeviceORList { set; get; }
        public DeviceOR SelectDeviceOR { get; set; }

        //通首
        ObservableCollection<ChannelOR> _ChannelORList = new ObservableCollection<ChannelOR>();
        public ObservableCollection<ChannelOR> ChannelORList
        {
            set { _ChannelORList = value; }
            get { return _ChannelORList; }
        }
        public ChannelOR SelectChannelOR { get; set; }

        DeviceDA _dev = new DeviceDA();
        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectStationOR == null)
                return;
            if (DeviceORList != null)
                DeviceORList.Clear();

            var v = _dev.selectDevices(SelectStationOR.Stationid.ToString());

            foreach (DeviceOR obj in v)
            {
                DeviceORList.Add(obj);
            }

            DeviceOR objTemp = new DeviceOR() { Deviceid = -1, Devicename = "所有设备" };
            DeviceORList.Insert(0, objTemp);
            SelectDeviceOR = objTemp;
        }
        public void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectChannelOR = null;
            if (ChannelORList != null)
                ChannelORList.Clear();

            if (SelectDeviceOR == null)
                return;

            var v = _dev.selectChannels(SelectDeviceOR.Deviceid);
            foreach (ChannelOR obj in v)
            {
                ChannelORList.Add(obj);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable thisdt = GetAllDataTable();
           
        }
        public void AlertNormal(string msg)
        {
            MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public ReportType SelectReportType { get; set; }

        ReportDao m_reportDao = new ReportDao();
        public DataTable GetAllDataTable()
        {
            string strStartTime = string.Format("{0} 00:00:00", StartDate.ToString("yyyy-MM-dd"));
            string strEndTime = string.Format("{0} 23:59:59", EndDate.ToString("yyyy-MM-dd"));
            Info objInfo = new Info();
            objInfo.DeviceName = SelectDeviceOR.Devicename;
            objInfo.StationID =SelectStationOR.Stationid;
            objInfo.Begin = Convert.ToDateTime(strStartTime);
            objInfo.End = Convert.ToDateTime(strEndTime);

            if (SelectReportType != null)
            {
                switch (SelectReportType.index)
                {
                    case 0:
                        if (SelectDeviceOR == null)
                        {
                            AlertNormal("请选择设备");
                            return null;
                        }
                        if (SelectDeviceOR.Deviceid != -1)
                        {
                            objInfo.DeviceID = SelectDeviceOR.Deviceid;
                            //objInfo.allDevices = null;
                            objInfo.Type = 0;
                            return SerachThread(objInfo);
                        }
                        //选择的所有设备时
                        else if (SelectDeviceOR.Deviceid == -1)
                        {
                            objInfo.allDevices = new DeviceDA().GetAllDevice(objInfo.StationID);
                            objInfo.Type = 1;
                            return SerachThread(objInfo);
                        }
                        break;
                        
                    case 1:
                        if (SelectChannelOR == null)
                        {
                            AlertNormal("请选择通道");
                            return null;
                        }
                        if (SelectChannelOR.Channelno == -1)
                        {
                            AlertNormal("选择该报表类型时，不能选择\"所有设备\"");
                            return null;
                        }
                        int ChannelNo = SelectChannelOR.Channelno;
                        //


                        DataTable dt = m_reportDao.GetChannelHistorReport(SelectDeviceOR.Devicename,
                            SelectDeviceOR.Deviceid, ChannelNo, objInfo.Begin, objInfo.End, objInfo.StationID);
                        return dt;
                    case 2:
                        dt = m_reportDao.GetAlarmHistoryReport(strStartTime, strEndTime);
                        return dt;
                    case 3:
                        if (SelectDeviceOR == null)
                        {
                            AlertNormal("请选择设备");
                            return null;
                        }

                        else if (SelectDeviceOR.Deviceid != -1)
                        {
                            objInfo.DeviceID = SelectDeviceOR.Deviceid;
                            objInfo.allDevices = null;
                            objInfo.Type = 0;
                            return SerachMonthThread(objInfo);
                        }
                        //选择的所有设备时
                        else if (SelectDeviceOR.Deviceid == -1)
                        {

                            //allDevices = m_deviceDao.GetAllDevice(StationID);
                            objInfo.allDevices = new DeviceDA().GetAllDevice(objInfo.StationID);
                            objInfo.Type = 1;
                            return SerachMonthThread(objInfo);
                        }
                        break;
                    // 2009-1-6
                    case 4:

                        if (SelectDeviceOR == null)
                        {
                            AlertNormal("请选择设备");
                            return null;
                        }
                        else if (SelectDeviceOR.Deviceid != -1)
                        {

                            objInfo.DeviceID = SelectDeviceOR.Deviceid;
                            objInfo.allDevices = null;
                            objInfo.Type = 0;
                            return SerachMonthThread(objInfo);
                        }
                        //选择的所有设备时
                        else if (SelectDeviceOR.Deviceid == -1)
                        {
                            objInfo.allDevices = new DeviceDA().GetAllDevice(objInfo.StationID);
                            objInfo.Type = 1;
                            return SerachMonthThread(objInfo);
                        }
                        break;
                }
            }
            return null;
        }
        private DataTable SerachThread(Info _Info)
        {
            DataTable thisdt = new DataTable();
            if (_Info.Type == 0)
            {
                thisdt = m_reportDao.GetDeviceHistoryReport(_Info.DeviceName, _Info.DeviceID, _Info.Begin, _Info.End, _Info.StationID);


            }
            else if (_Info.Type == 1)
            {
                int m = 0;//, currentvalue = 0;

                int count = _Info.allDevices.Count;
                if (count == 0)
                    return null;
                //int b = 0;
                foreach (DeviceObj m_obj in _Info.allDevices)
                {
                    //string str = "正在查询:";
                    //str += m_obj.DeviceName.ToString() + ",时间为：" + _Info.Begin.ToShortDateString() + "到" + _Info.End.ToShortDateString();
                    //statuc(str);
                    DataTable Current = m_reportDao.GetDeviceHistoryReport(m_obj.DeviceName, m_obj.DeviceID, _Info.Begin, _Info.End, _Info.StationID);
                    if (Current == null)
                    {
                        //statuc(str + "\r\n数据为空...");
                        //System.Threading.Thread.Sleep(500);   
                        continue;
                    }
                    //else
                    //{
                    //    statuc(str + "\r\n数据查询完毕...");
                    //}
                    //休息一段时间，让用户可以看见查询信息

                    //UpdateDataGridView(Current);
                    //continue;
                    if (m == 0)
                    {
                        if (Current != null)
                        {
                            thisdt = Current;
                            //UpdateDataGridView(this.dt);
                            m++;
                        }
                    }
                    else
                    {
                        if (Current != null)
                        {
                            thisdt.Merge(Current);
                            //this.dt = Current;
                            //UpdateDataGridView(this.dt);
                            m++;
                        }
                    }
                    //dgResult.DataSource = thisdt;
                    //UpdateDataGridView(this.dt);
                    //str += m_obj.DeviceName.ToString() + ",时间为：" + _Info.Begin.ToShortDateString() + "到" + _Info.End.ToShortDateString();
                    //statuc(str);
                    //currentvalue++;
                    //string strmyvalue = ((100 / count) * currentvalue).ToString();
                    //int myvalue = int.Parse(strmyvalue);
                    //setbarvalue(myvalue);
                    //if (b == _Info.allDevices.Count)
                    //    statuc("查询完毕！！\r\n正在更新数据表...");
                    //b++;
                }
                //setbarvalue(100);
                //statuc("查询完毕！！\r\n正在更新数据表...");
                m = 0;
            }
            return thisdt;
            //dgResult.DataSource = thisdt;
            //dgResult.DataBind();
            //释放线程
            //setbarvalue(100);
            //statuc("查询完毕！！\r\n正在更新数据表...");
            //UpdateDataGridView(this.dt);
            //if (dt != null)
            //    statuc("更新完毕！！    本次数据查询共有数据行：" + this.dt.Rows.Count.ToString() + "条");
            //else
            //    statuc("更新完毕！！    本次数据查询共有数据行：0条");

            //System.Threading.Thread.CurrentThread.Abort();
        }
        private DataTable SerachMonthThread(object myInfo)
        {

            Info _Info = myInfo as Info;
            DataTable thisdt = new DataTable();
            if (_Info.Type == 0)
            {
                //string str = "正在查询:";
                //str += _Info.DeviceName.ToString() + ",时间为：" + _Info.Begin.ToShortDateString() + "到" + _Info.End.ToShortDateString();

                thisdt = m_reportDao.GetDataMonthReport(_Info.StationID, _Info.DeviceName, _Info.DeviceID, _Info.Begin, _Info.End);

            }
            else if (_Info.Type == 1)
            {
                int m = 0, currentvalue = 0;

                int count = _Info.allDevices.Count;
                if (count == 0)
                    return null;
                int b = 0;
                foreach (DeviceObj m_obj in _Info.allDevices)
                {

                    DataTable Current = m_reportDao.GetDataMonthReport(_Info.StationID, m_obj.DeviceName, m_obj.DeviceID, _Info.Begin, _Info.End);
                    if (Current == null)
                    {
                        continue;
                    }

                    if (m == 0)
                    {
                        if (Current != null)
                        {
                            thisdt = Current;
                            m++;
                        }
                    }
                    else
                    {
                        if (Current != null)
                        {
                            thisdt.Merge(Current);
                            m++;
                        }
                    }
                }
            }
            return thisdt;

            //if (dt != null)
            //    statuc("更新完毕！！    本次数据查询共有数据行：" + this.dt.Rows.Count.ToString() + "条");
            //else
            //    statuc("更新完毕！！    本次数据查询共有数据行：0条");

            //System.Threading.Thread.CurrentThread.Abort();
        }
    }

    public class ReportType
    {
        public int index { get; set; }
        public string Name { get; set; }

        public bool ChanncelEnable { get; set; }
        public bool DeviceEnable { get; set; }
    }

    public class Info
    {
        private int _StationID;
        /// <summary>
        /// 站点ID
        /// </summary>
        public int StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }


        private string _DeviceName;
        /// <summary>
        /// 设备名
        /// </summary>
        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }

        private int _DeviceID;
        /// <summary>
        /// 设备编号
        /// </summary>
        public int DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }


        private DateTime _Begin;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Begin
        {
            get { return _Begin; }
            set { _Begin = value; }
        }

        private DateTime _End;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
        }


        private int _ChannelNO;
        /// <summary>
        /// 通道号
        /// </summary>
        public int ChannelNO
        {
            get { return _ChannelNO; }
            set { _ChannelNO = value; }
        }

        /// <summary>
        /// 设备列表
        /// </summary>
        public List<DeviceObj> allDevices = null;
        /// <summary>
        /// 【0】表示单个设备  【1】表示多个设备
        /// </summary>
        public int Type = 0;
    }
}
