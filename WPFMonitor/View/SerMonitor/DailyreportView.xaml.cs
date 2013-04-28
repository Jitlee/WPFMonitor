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
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using System.Data;
using WPFMonitor.DAL.SerMonitor;
using System.Windows.Controls.DataVisualization.Charting;

namespace WPFMonitor.View.SerMonitor
{
    /// <summary>
    /// DailyreportView.xaml 的交互逻辑
    /// </summary>
    public partial class DailyreportView : DockableContent
    {
        public DailyreportView()
        {
            InitializeComponent();
            Init();
            //ShowTreeView();
            this.DataContext = this;
        }
        public DateTime StartDate { get; set; }

        public void Init()
        {
            StationDA _staDA = new StationDA();
            StationORList = _staDA.selectAllStation();
            if (StationORList != null && StationORList.Count > 0)
            {
                SelectStationOR = StationORList.First();
            }
            StartDate = Convert.ToDateTime("2011-10-02");// DateTime.Now;
        }

        #region 树处理
        DeviceDA _DeviceDA = new DeviceDA();
        //站点
        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value; }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }

        public void cbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectStationOR != null)
                Load(SelectStationOR.Stationid.ToString());

        }

        public ObservableCollection<NodeItem> ItemList { get; set; }
        private void Load(string stationID)
        {
            if (ItemList != null)
            {
                ItemList.Clear();
            }
            else
            {
                ItemList = new ObservableCollection<NodeItem>();
            }
            var v = _DeviceDA.selectDeviceChannels(stationID);
            foreach (var vobj in v)
            {
                NodeItem mitem = new NodeItem();
                mitem.ID = vobj.Deviceid;
                mitem.Name = vobj.Devicename;
                mitem.IsSelect = false;
                mitem.DevObj = vobj;

                //加载子通道
                ObservableCollection<ChannelOR> listChan = _DeviceDA.selectChannels(vobj.Deviceid);
                mitem.ListChilds = new List<NodeItem>();
                foreach (var vc in listChan)
                {
                    NodeItem chanItem = new NodeItem();
                    chanItem.ID = vc.Channelno;
                    chanItem.Name = vc.Channelname;
                    chanItem.IsSelect = false;
                   
                    mitem.ListChilds.Add(chanItem);    
                }
                ItemList.Add(mitem);
            }
               
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectStationOR == null)
                return;
            int CurrentStationID =SelectStationOR.Stationid;
            string strDate = StartDate.ToString("yyyy-MM-dd");
            string strtime = StartDate.ToString("yyyy-MM-dd") + " 00:00:00.000";
            DateTime begin;
            if (!DateTime.TryParse(strtime, out begin))
            {
                AlertNormal("请选择正确的日期！");
                return;
            }

            string endtime = strDate + " 23:59:59.999";
            DateTime end = Convert.ToDateTime(endtime);
            //DataTable dt = new DataTable();
            List<DataTable> lst = new List<DataTable>();
            List<string> tuli = new List<string>();
            List<Color> colors = new List<Color>();
            this.cht.Series.Clear();
            if (CurrentStationID != -1)
                foreachTreeView(begin, end, ref lst, ref  tuli, ref colors, CurrentStationID);
            else
            {
                AlertNormal("站点信息错误！");
                return;
            }            
            //AlertNormal("数据查询完毕！");
        }
        public void AlertNormal(string msg)
        {
            MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #region 查询数据
        private void foreachTreeView(DateTime strTime, DateTime endTime, ref List<DataTable> lst,
            ref  List<string> tuli, ref  List<Color> colors, int StationID)
        {
            // Color[] all = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Brown, Color.Chocolate, Color.Crimson, Color.Crimson, Color.DarkMagenta, Color.FloralWhite };
            // mylis.Clear(); ;
            DailyreportDA objDailyBLLClass = new DailyreportDA();

            bool isHaveSelect = false;
            foreach (NodeItem DeviceNode in ItemList)
            {
                string DeviceName = DeviceNode.Name;

                foreach (NodeItem ChannelNode in DeviceNode.ListChilds)
                {
                    if (!ChannelNode.IsSelect)
                        continue;

                    int DeviceID = DeviceNode.ID;

                    int ChannelNO = ChannelNode.ID;
                    string ChannelName =ChannelNode.Name;
                    DataTable dt = objDailyBLLClass.GetHistoryChannelValue(DeviceName, DeviceID, strTime, endTime, StationID, ChannelNO);
                    if (dt == null)
                    {
                        //AlertNormal("获取数据失败");
                        continue;
                    }
                    isHaveSelect = true;
                    CreateLineByList(dt,string.Format("{0}-{1}",DeviceNode.Name, ChannelNode.Name));
                }
            }
            if (!isHaveSelect)
                AlertNormal("请选择通道！");
        }
        public LineSeries CreateLineByList(DataTable SourceDataTable, String LineName)
        {
            LineSeries LineCreate = new LineSeries();
            LineCreate.IsSelectionEnabled = true;
            LineCreate.IndependentValuePath = "Key";
            LineCreate.DependentValuePath = "Value";
            LineCreate.Title = LineName;
            //LineCreate.DataPointStyle = this.Resources["LineDataPointStyle"] as Style;
            List<KeyValuePair<DateTime, Double>> valueList = new List<KeyValuePair<DateTime, Double>>();
            for (int i = 0; i < SourceDataTable.Rows.Count; i++)
            {
                DataRow dr = SourceDataTable.Rows[i];
                DateTime Dt = System.Convert.ToDateTime(dr["MonitorTime"]);
                Double reusltValue = System.Convert.ToDouble(dr["MonitorValue"]);
                valueList.Add(new KeyValuePair<DateTime, Double>(Dt, reusltValue));
                LineCreate.Tag = dr;
            }
            LineCreate.ItemsSource = valueList;
            this.cht.Series.Add(LineCreate);
           // cht.Add(LineCreate, valueList);
            return LineCreate;
        }
        #endregion

      

        #region 曲线
        #region Y 轴颜色设置
        Int32 CurreLineY = 0;
        void LineY_Loaded(object sender, RoutedEventArgs e)
        {
            Line SendLine = sender as Line;
            SendLine.StrokeThickness = 2;
            switch (CurreLineY)
            {
                case 0:
                    SendLine.Stroke = new SolidColorBrush(Colors.Black);
                    break;
                case 1:
                    SendLine.Stroke = new SolidColorBrush(Colors.Blue);
                    break;
                case 2:
                    SendLine.Stroke = new SolidColorBrush(Colors.Red);
                    break;
                case 3:
                    SendLine.Stroke = new SolidColorBrush(Colors.Chocolate);
                    break;
                case 4:
                    SendLine.Stroke = new SolidColorBrush(Colors.Black);
                    break;
                case 5:
                    SendLine.Stroke = new SolidColorBrush(Colors.Chocolate);
                    break;
                case 6:
                    SendLine.Stroke = new SolidColorBrush(Colors.Red);
                    break;
                case 7:
                    SendLine.Stroke = new SolidColorBrush(Colors.Blue);
                    break;
                case 8:
                    SendLine.Stroke = new SolidColorBrush(Colors.Black);
                    break;
                default:
                    break;
            }
            CurreLineY++;
        }
        #endregion

        #region  X 轴线条状态设置
        void LineX_Loaded(object sender, RoutedEventArgs e)
        {
            Line SendLine = sender as Line;
            DoubleCollection Dc = new DoubleCollection();
            Dc.Add(2);
            Dc.Add(4);
            SendLine.StrokeDashArray = Dc;
            SendLine.Stroke = new SolidColorBrush(Colors.BlueViolet);
        }
        #endregion
        #endregion
    }

    public class NodeItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public bool IsSelect { get; set; }

        public DeviceOR DevObj { get; set; }

        public ChannelOR ChanObj { get; set; }

        public List<NodeItem> ListChilds { get; set; }
    }
}
