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
using System.ComponentModel;

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

            StartDate = DateTime.Now.AddDays(-7);// Convert.ToDateTime("2011-01-01");
            EndDate = DateTime.Now;// Convert.ToDateTime("2012-10-15");

            Init();
            this.DataContext = this;
        }
        #region 初使化
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
            SelectStationOR = StationORList.First();

            ReportTypeList = new ObservableCollection<ReportType>();
            //                                                通道  设备
            ReportType mRep = new ReportType(0, "设备历史报表",false,true);
            ReportTypeList.Add(mRep);
            SelectReportType = mRep;

            mRep = new ReportType(1, "测点历史报表", true, true);
            ReportTypeList.Add(mRep);

            mRep = new ReportType(2, "报警历史报表", false, false);
            ReportTypeList.Add(mRep);

            mRep = new ReportType(3, "数据月报表", false, true);
            ReportTypeList.Add(mRep);

            mRep = new ReportType(4, "数据年报表", false, true);
            ReportTypeList.Add(mRep);
        }
        public ObservableCollection<ReportType> ReportTypeList { get; set; }
        public ReportType SelectReportType { get; set; }
        public bool ChanncelEnable { get; set; }
        public bool DeviceEnable { get; set; }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NotifyPropertyChanged("ReportTypeList");
            NotifyPropertyChanged("SelectReportType");


            ChanncelEnable=SelectReportType.ChanncelEnable;
            DeviceEnable = SelectReportType.DeviceEnable;
            cbDevice.IsEnabled = DeviceEnable;
            cbChanncel.IsEnabled = ChanncelEnable;
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
        #endregion
        public  ObservableCollection<object> ListDevice;

        public Dictionary<string, string> Dir { get; set; }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListDevice == null)
                ListDevice = new ObservableCollection<object>();
            else
                ListDevice.Clear();

            if (Dir == null)
                Dir = new Dictionary<string, string>();
            else
                Dir.Clear();

            DataTable thisdt = GetAllDataTable();
            if (thisdt == null)
                return;

            

            bool isCN = false;
            if (SelectReportType != null)
            { 

                if (SelectReportType.index == 0)
                {
                    Dir.Add("DeviceName", "设备");
                    Dir.Add("ChannelName", "测点");
                    Dir.Add("MonitorValue", "观测值");
                    Dir.Add("MonitorTime", "观测时间");

                    foreach (DataRow dr in thisdt.Rows)
                    {
                        ListDevice.Add(new DeviceHistoryOR(dr));
                    }
                }
                else if (SelectReportType.index == 3 || SelectReportType.index == 4)
                {
                    Dir.Add("deviceno", "设备ID");

                    Dir.Add("devicename", "设备名");
                    Dir.Add("channelno", "测点ID");

                    Dir.Add("channelname", "测点名");
                    Dir.Add("maxVal", "最大值");
                    Dir.Add("minVal", "最小值");
                    Dir.Add("avgVal", "平均值");
                    Dir.Add("data", "日期");
                    //isCN = true;
                    foreach (DataRow dr in thisdt.Rows)
                    {
                        ListDevice.Add(new DeviceHistoryOR2(dr));
                    }
                }
                else
                {
                    //isCN = true;

                    Dir.Add("Content", "报警内容");
                    Dir.Add("HappenTime", "发生时间");
                    Dir.Add("DeviceName", "设备");
                    Dir.Add("RelieveTime", "解除时间");
                    Dir.Add("AlarmLevel", "报警级别");
                    Dir.Add("OperateUserID", "操作员ID");
                    Dir.Add("PolicyID", "策略ID");
                    Dir.Add("StationID", "机房名 ");
                    foreach (DataRow dr in thisdt.Rows)
                    {
                        ListDevice.Add(new DeviceHistoryOR1(dr));
                    }
                }
            }

            
            dg.Columns.Clear();
            foreach (KeyValuePair<string, string> d in Dir)
            {
                DataGridTextColumn dgc = new DataGridTextColumn();
                dgc.Header = d.Value;
                if (!isCN)
                    dgc.Binding = new Binding(d.Key);
                else
                    dgc.Binding = new Binding(d.Value);

                dgc.MinWidth = 100;
                dg.Columns.Add(dgc);
            }

            
            dg.ItemsSource = ListDevice;
           
            //int index=0;
            //foreach (KeyValuePair<string, string> d in Dir)
            //{
                
            //    dg.Columns[index].Header = d.Value;
            //    index++;
            //}
        }

        #region Common
        public void AlertNormal(string msg)
        {
            MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

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
                    DataTable Current = m_reportDao.GetDeviceHistoryReport(m_obj.DeviceName, m_obj.DeviceID,
                        _Info.Begin, _Info.End, _Info.StationID);
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
                m = 0;
            }
            
            return thisdt;
        }

        #region 实体
        public class DeviceHistoryOR
        {
            public string DeviceName { get; set; }
            public string ChannelName { get; set; }
            public string MonitorValue { get; set; }
            public string MonitorTime { get; set; }

            public DeviceHistoryOR(DataRow dr)
            {
                DeviceName = dr["DeviceName"].ToString();
                ChannelName = dr["ChannelName"].ToString();
                MonitorValue = dr["MonitorValue"].ToString();
                MonitorTime = dr["MonitorTime"].ToString();
            }
        }

        public class DeviceHistoryOR1
        {
            public string Content { get; set; }
            public string HappenTime { get; set; }
            public string DeviceName { get; set; }
            public string RelieveTime { get; set; }
            public string AlarmLevel { get; set; }
            public string OperateUserID { get; set; }
            public string PolicyID { get; set; }
            public string StationID { get; set; }
            public DeviceHistoryOR1(DataRow dr)
            {
                Content = dr["Content"].ToString();
                HappenTime = dr["HappenTime"].ToString();
                DeviceName = dr["DeviceName"].ToString();
                RelieveTime = dr["RelieveTime"].ToString();
                AlarmLevel = dr["AlarmLevel"].ToString();
                OperateUserID = dr["OperateUserID"].ToString();
                PolicyID = dr["PolicyID"].ToString();
                StationID = dr["StationID"].ToString();
            }
        }
        public class DeviceHistoryOR2
        {
            public string deviceno { get; set; }//", "设备ID");

            public string devicename { get; set; }//", "设备名");
            public string channelno { get; set; }//", "测点ID");

            public string channelname { get; set; }//", "测点名");
            public string maxVal { get; set; }//", "最大值");
            public string minVal { get; set; }//", "最小值");
            public string avgVal { get; set; }//", "平均值");
            public string data { get; set; }//", "日期");

            public DeviceHistoryOR2(DataRow dr)
            {
                deviceno = dr["设备ID"].ToString();

                devicename = dr["设备名"].ToString();
                channelno = dr["测点ID"].ToString();

                channelname = dr["测点名"].ToString();
                maxVal = dr["最大值"].ToString();
                minVal = dr["最小值"].ToString();
                avgVal = dr["平均值"].ToString();
                data = dr["日期"].ToString();
            }
        }
        #endregion

        private DataTable SerachMonthThread(object myInfo)
        {

            Info _Info = myInfo as Info;
            DataTable thisdt = new DataTable();
            if (_Info.Type == 0)
            {
                thisdt = m_reportDao.GetDataMonthReport(_Info.StationID, _Info.DeviceName, _Info.DeviceID, _Info.Begin, _Info.End);
            }
            else if (_Info.Type == 1)
            {
                int m = 0;

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
        }

        
    }

    public class ItemAlter
    {
        public string DeviceID { get; set; }
        public string DeviceName { get; set; }

    }

    public class ReportType
    {

        public int index { get; set; }
        public string Name { get; set; }

        public bool ChanncelEnable { get; set; }
        public bool DeviceEnable { get; set; }

        public ReportType(int _index, string _Name, bool _ChanncelEnable, bool _DeviceEnable)
        {
            index = _index;
            Name = _Name;
            ChanncelEnable = _ChanncelEnable;
            DeviceEnable = _DeviceEnable;
        }

        public ReportType()
        {
        }
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
