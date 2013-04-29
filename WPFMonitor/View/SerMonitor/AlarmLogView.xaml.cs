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
using WPFMonitor.Model;
using WPFMonitor.Model.SerMonitor;
using WPFMonitor.DAL.SerMonitor;
using System.ComponentModel;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;

namespace WPFMonitor.View.SerMonitor
{
    /// <summary>
    /// AlarmLogView.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmLogView : DockableContent
    {
        public AlarmLogView()
        {
            InitializeComponent();            
            Init();
            this.DataContext = this;
        }
       
        public void Init()
        {
            StartDate = DateTime.Now.AddDays(-7); // Convert.ToDateTime("2011-10-01");
            StartTime = "00:00:00";
            EndDate = DateTime.Now; // Convert.ToDateTime("2011-10-15");
            EndTime = "23:59:59";

            AlarmLogS = new ObservableCollection<AlarmLogOR>();

            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();
            StationOR tempOR = new StationOR();
            tempOR.Stationid = -1;
            tempOR.Stationname = "所有站点";
            StationORList.Insert(0,tempOR);
            SelectStationOR = tempOR;

            EventLists = new ObservableCollection<StatusOR>() {
            new StatusOR(){ ID=1, Name="1"},
            new StatusOR(){ ID=2, Name="2"},
            new StatusOR(){ ID=3, Name="3"},
            new StatusOR(){ ID=4, Name="4"},
            new StatusOR(){ ID=5, Name="5"},
            new StatusOR(){ ID=6, Name="6"}
            };
            StatusOR temp = new StatusOR() { ID = -1, Name = "所有事件级别" };
            EventLists.Insert(0, temp);
            SelectEventOR = temp;

            //所有事件
            EventsNames = new ObservableCollection<string>();
            EventsNames = _dev.GetAllEventsName();
            EventsNames.Insert(0,"所有事件名称");
            SelectEventName = "所有事件名称";


            DeviceORList = new ObservableCollection<DeviceOR>();
            //DeviceOR objTemp = new DeviceOR() { Deviceid = -1, Devicename = "所有设备" };
            DeviceOR objTemp = new DeviceOR() { Deviceid = -1, Devicename = "所有设备" };
            DeviceORList.Insert(0, objTemp);
            SelectDeviceOR = objTemp; 
        }

        #region ...
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void ShowMsgError(string msg)
        {
            MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion


        DeviceDA _dev = new DeviceDA();
        #region 属性
        public ObservableCollection<AlarmLogOR> AlarmLogS { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ObservableCollection<StatusOR> EventLists { get; set; }
        public StatusOR SelectEventOR { get; set; }

        public ObservableCollection<StationOR> StationORList { get; set; }
        public StationOR SelectStationOR { get; set; }

        
        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("start cbStation_SelectionChanged");
            
            if (SelectStationOR.Stationid == -1)
                return;
            
           //SelectDeviceOR = null;

            if (DeviceORList != null)
                DeviceORList.Clear();
            else
                DeviceORList = new ObservableCollection<DeviceOR>();

            var v = _dev.selectDevices(SelectStationOR.Stationid.ToString());

            foreach (DeviceOR obj in v)
            {
                DeviceORList.Add(obj);
            }

            DeviceOR objTemp = new DeviceOR() { Deviceid = -1, Devicename = "所有设备" };
            DeviceORList.Insert(0, objTemp);
            SelectDeviceOR = objTemp;

            Console.WriteLine("End cbStation_SelectionChanged");
        }

        //设备
        public ObservableCollection<DeviceOR> DeviceORList { set; get; }
        public DeviceOR SelectDeviceOR { get; set; }

        //事件名称
        public ObservableCollection<string> EventsNames { get; set; }
        public string SelectEventName { get; set; }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetSelectValue();
        }
        protected void GetSelectValue()
        {
            string startDate = string.Format("{0} {1}", StartDate.ToString("yyyy-MM-dd"), StartTime);
            DateTime dtStart;
            if (!DateTime.TryParse(startDate, out dtStart))
            {
                ShowMsgError("开始日期或时间输入错误！");
                return;
            }

            string strEnd = string.Format("{0} {1}", EndDate.ToString("yyyy-MM-dd"), EndTime);
            DateTime dtEnd;
            if (!DateTime.TryParse(startDate, out dtEnd))
            {
                ShowMsgError("结束日期或时间输入错误！");
                return ;
            }

            string sql = "";
            sql = string.Format(@"select alrm.*,dev.DeviceName,sta.StationName  from t_AlarmLog 
alrm,t_Device  dev,t_Station sta
where alrm.DeviceID=dev.DeviceID and alrm.stationid= sta.StationID and  HappenTime > '{0}' and HappenTime< '{1}'", startDate, strEnd);


            if (SelectStationOR.Stationid != -1)
            {
                int stationid = SelectStationOR.Stationid;
                sql += " and alrm.StationID='" + stationid + "'";
            }
            if (SelectDeviceOR.Deviceid != -1)
            {
                int deviceID = SelectDeviceOR.Deviceid;
                sql += " and alrm.DeviceID=" + deviceID + "";
            }

            if (SelectEventOR.ID != -1)
            {
                sql += " and AlarmLevel=" + SelectEventOR.ID + "";
            }

            if (SelectEventName != "所有事件名称")
            {
                sql += " and EventsName='" + SelectEventOR.Name + "'";
            }
            
           
            AlertLogReportDA m_AlarmLogDao = new AlertLogReportDA();
            
            var v= m_AlarmLogDao.GetAlarmLog(sql); ;
            foreach (var vobj in v)
            {
                AlarmLogS.Add(vobj);
            }

        }


    }
}
