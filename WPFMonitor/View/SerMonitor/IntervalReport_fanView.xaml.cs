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
using WPFMonitor.View.SerMonitor;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using System.ComponentModel;
using System.Data;
using WPFMonitor.DAL.SerMonitor;

namespace WPFMonitor.View.SerMonitor
{

    public class ReportData
    {
        public string Time { get; set; }

        public Double srMax { get; set; }

        public Double srMin { get; set; }

        public Double sravg { get; set; }

        public ReportData(DataRow dr)
        {
            Time = dr["Time"].ToString();
            if (!string.IsNullOrEmpty(dr["MinValue"].ToString()))
                srMin = Convert.ToDouble(dr["MinValue"]);

            if (!string.IsNullOrEmpty(dr["MaxValue"].ToString()))
                srMax = Convert.ToDouble(dr["MaxValue"]);

            if (!string.IsNullOrEmpty(dr["AvgValue"].ToString()))
                sravg = Convert.ToDouble(dr["AvgValue"]);
        }
    }

 

    /// <summary>
    /// IntervalReport_fanView.xaml 的交互逻辑
    /// </summary>
    public partial class IntervalReport_fanView : DockableContent
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IntervalReport_fanView()
        {
            InitializeComponent();
            txtStartData.SelectedDate = Convert.ToDateTime("2011-10-01");

            txtEndData.SelectedDate = Convert.ToDateTime("2011-10-15");

            Init();
            this.DataContext = this;


        }

        private void Init()
        {

            cbStation.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            cbDeviceTypeID.SelectionChanged += new SelectionChangedEventHandler(cbStation_SelectionChanged);
            cbDeviceID.SelectionChanged += new SelectionChangedEventHandler(cbDeviceID_SelectionChanged);

            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();

            DeviceAndTypeDA _DTypeDA = new DeviceAndTypeDA();
            this.DeviceTypeORList = _DTypeDA.GetAllDeviceType();

            ReportType = new ObservableCollection<string>() { "按月统计", "按日统计" };
            SelectReport = "按月统计";
        }

        #region 属性
        public ObservableCollection<string> ReportType { get; set; }
        public string SelectReport { get; set; }
        #endregion

        #region 站点、设备类型、 设备、通首管理
        DeviceAndTypeDA _DeviceDA = new DeviceAndTypeDA();

        //站点
        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value; NotifyPropertyChanged("StationORList"); }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }
        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectDeviceTypeOR == null || SelectStationOR == null)
                return;

            SelectDeviceOR = null;

            if (DeviceORList != null)
                DeviceORList.Clear();

            var v = _DeviceDA.GetAllGenerdDevice(SelectStationOR.Stationid.ToString(),
                SelectDeviceTypeOR.Devicetypeid.ToString());

            foreach (DeviceOR obj in v)
            {
                DeviceORList.Add(obj);
            }
            if (DeviceORList.Count > 0)
                SelectDeviceOR = DeviceORList.First();
        }
        //设备类型
        ObservableCollection<DeviceTypeOR> _DeviceTypeORList = new ObservableCollection<DeviceTypeOR>();
        public ObservableCollection<DeviceTypeOR> DeviceTypeORList
        {
            set
            {
                _DeviceTypeORList = value;
                NotifyPropertyChanged("DeviceTypeORList");
            }
            get { return _DeviceTypeORList; }
        }
        public DeviceTypeOR SelectDeviceTypeOR { get; set; }

        //设备
        ObservableCollection<DeviceOR> _DeviceORList = new ObservableCollection<DeviceOR>();
        public ObservableCollection<DeviceOR> DeviceORList
        {
            set
            {
                _DeviceORList = value;
                NotifyPropertyChanged("DeviceORList");
            }
            get { return _DeviceORList; }
        }
        public DeviceOR SelectDeviceOR { get; set; }

        public void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectChannelOR = null;
            if (ChannelORList != null)
                ChannelORList.Clear();

            if (SelectDeviceOR == null)
                return;

            var v = _DeviceDA.SelectChannels(SelectDeviceOR.Deviceid.ToString());
            foreach (ChannelOR obj in v)
            {
                ChannelORList.Add(obj);
            }
        }
        //通首
        ObservableCollection<ChannelOR> _ChannelORList = new ObservableCollection<ChannelOR>();
        public ObservableCollection<ChannelOR> ChannelORList
        {
            set { _ChannelORList = value; }
            get { return _ChannelORList; }
        }
        public ChannelOR SelectChannelOR { get; set; }


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

        public ObservableCollection<ReportData> ObjReportData { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectDeviceOR == null || SelectStationOR == null
                   || SelectChannelOR == null)
            {
                ShowMsgError("请选择完整的站点、设备、通道。");
                return;
            }
            DataTable dt = null;
            try
            {
                dt = Getvalue();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
                return;
            }

            if (dt == null)
                return;

            if (dt.Rows.Count == 0)
            {
                return;
            }
            if (ObjReportData == null)
                ObjReportData = new ObservableCollection<ReportData>();
            else
                ObjReportData.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ObjReportData.Add(new ReportData(dr));
            }
            srMain.ItemsSource = ObjReportData;
        }
        private DataTable Getvalue()
        {
            if (!txtStartData.SelectedDate.HasValue)
            {
                ShowMsgError("请输入开始时间！");
                return null;
            }
            string startDate = string.Format("{0} 0:00:00", txtStartData.SelectedDate.Value.ToString("yyyy-MM-dd"));
            DateTime begin;
            if (!DateTime.TryParse(startDate, out begin))
            {
                ShowMsgError("开始日期或时间输入错误！");
                return null;
            }

            if (!txtEndData.SelectedDate.HasValue)
            {
                ShowMsgError("请输入结束时间！");
                return null;
            }

            string strEnd = string.Format("{0} 23:59:59", txtEndData.SelectedDate.Value.ToString("yyyy-MM-dd"));
            DateTime end;
            if (!DateTime.TryParse(strEnd, out end))
            {
                ShowMsgError("结束日期或时间输入错误！");
                return null;
            }


            string beginTime = begin.ToString("yyyy-MM-dd HH:mm:ss");
            string endTime = end.ToString("yyyy-MM-dd HH:mm:ss");

            if (begin > end)
            {
                ShowMsgError("开始日期不能大于结束日期！");
                return null;
            }

            DataTable mytb = new DataTable();
            DataColumn Avgvalue = new DataColumn("AvgValue");
            mytb.Columns.Add(Avgvalue);
            Avgvalue = new DataColumn("MinValue");
            mytb.Columns.Add(Avgvalue);
            Avgvalue = new DataColumn("MaxValue");
            mytb.Columns.Add(Avgvalue);
            Avgvalue = new DataColumn("Time");
            mytb.Columns.Add(Avgvalue);
            //for 
            //DateTime mytime = new DateTime(StartYear, StartMonth, startDay, 0, 0, 0);
            //mytime .
            IntervalReportDA objDailyBLLClass = new IntervalReportDA();
            DataTable tb_mytime;

            string CurrentDeviceName = "";// cmb_Device.SelectedItem.Text;
            int CurrentDevicID = -1;// int.Parse(cmb_Device.SelectedItem.Value);

            int CurrentStationID = -1;// int.Parse(comstationid.SelectedItem.Value);
            int CurrentChannelNO = -1;// int.Parse(dpdChannelno.SelectedItem.Value);
            if (SelectDeviceOR != null)
            {
                CurrentDeviceName = SelectDeviceOR.Devicename;
                CurrentDevicID = SelectDeviceOR.Deviceid;
            }

            if (SelectStationOR != null)
                CurrentStationID = SelectStationOR.Stationid;

            if (SelectChannelOR != null)
                CurrentChannelNO = SelectChannelOR.Channelno;
            

            bool shujuok = objDailyBLLClass.GetAllHistoryChannelValue(CurrentDeviceName, CurrentDevicID, 
                begin, end, CurrentStationID, CurrentChannelNO);
            if (!shujuok)
            {
                ShowMsgError("获取数据失败！");
                return null;
            }
            if (SelectReport == "按日统计")//(rdiDay.Checked)
            {
                tb_mytime = objDailyBLLClass.GetInertvalDateTime(beginTime, endTime);
                if (tb_mytime == null || tb_mytime.Rows.Count < 1)
                    return null;
                
                foreach (DataRow mytime_row in tb_mytime.Rows)
                {
                    string[] date = mytime_row["date"].ToString().Split('-');
                    int y, m, d;
                    y = int.Parse(date[0]);
                    m = int.Parse(date[1]);
                    d = int.Parse(date[2]);
                    DataTable dt = objDailyBLLClass.GetDayChannelValue(y, m, d, CurrentDevicID, CurrentChannelNO, CurrentStationID);
                    //DataTable dt = objDailyBLLClass.GetDayChannelValue(y,m,d, 60, 0, 2);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow myrow = mytb.NewRow();
                        myrow["AvgValue"] = dt.Rows[0]["AvgValue"].ToString();
                        myrow["MinValue"] = dt.Rows[0]["MinValue"].ToString();
                        myrow["MaxValue"] = dt.Rows[0]["MaxValue"].ToString();
                        myrow["Time"] = mytime_row["date"].ToString();// y.ToString() + "-" + m.ToString() + "-" + d.ToString();
                        mytb.Rows.Add(myrow);
                    }
                    else if (dt == null || dt.Rows.Count < 1)
                    {
                        DataRow myrow = mytb.NewRow();
                        myrow["AvgValue"] = 0.ToString();
                        myrow["MinValue"] = 0.ToString();
                        myrow["MaxValue"] = 0.ToString();
                        myrow["Time"] = mytime_row["date"].ToString();// y.ToString() + "-" + m.ToString() + "-" + d.ToString();
                        mytb.Rows.Add(myrow);
                    }
                }
            }
            else// if (rdiMonth.Checked)
            {
                string beginTime_Month = begin.Year.ToString() + "-" + begin.Month.ToString() + "-" + "01";
                string endTime_Month = end.Year.ToString().Trim() + "-" + end.Month.ToString().Trim() + "-" + "20";
                tb_mytime = objDailyBLLClass.GetInertvalDateTimeMonth(beginTime_Month, endTime_Month);
                if (tb_mytime == null || tb_mytime.Rows.Count < 1)
                    return null;

                foreach (DataRow mytime_row in tb_mytime.Rows)
                {
                    string[] date = mytime_row["date"].ToString().Split('-');
                    int y, m, d;
                    y = int.Parse(date[0]);
                    m = int.Parse(date[1]);
                    d = int.Parse(date[2]);
                    DataTable dt = objDailyBLLClass.GetDayChannelValue(y, m, CurrentDevicID, CurrentChannelNO, CurrentStationID);
                    //DataTable dt = objDailyBLLClass.GetDayChannelValue(y,m, 60, 0, 2);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow myrow = mytb.NewRow();
                        myrow["AvgValue"] = dt.Rows[0]["AvgValue"].ToString();
                        myrow["MinValue"] = dt.Rows[0]["MinValue"].ToString();
                        myrow["MaxValue"] = dt.Rows[0]["MaxValue"].ToString();
                        myrow["Time"] = mytime_row["date"].ToString().Substring(0, 7);// y.ToString() + "-" + m.ToString() + "-" + d.ToString();
                        mytb.Rows.Add(myrow);
                    }
                    else if (dt == null || dt.Rows.Count < 1)
                    {
                        DataRow myrow = mytb.NewRow();
                        myrow["AvgValue"] = 0.ToString();
                        myrow["MinValue"] = 0.ToString();
                        myrow["MaxValue"] = 0.ToString();
                        myrow["Time"] = mytime_row["date"].ToString().Substring(0, 7);// y.ToString() + "-" + m.ToString() + "-" + d.ToString();
                        mytb.Rows.Add(myrow);
                    }
                }
            }
            objDailyBLLClass.DeleteTemp();
            return mytb;
        }
    }
}
