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
using System.Threading.Tasks;

namespace MonitorSystem.Other
{
    public partial class LineProperty : UserControl
    {

        public LineProperty()
        {
            InitializeComponent();

            ShowYear.Value = DateTime.Now.Year;
            ShowMonth.Value = DateTime.Now.Month;
            ShowDay.Value = DateTime.Now.Day;

            ShowHH.Value = DateTime.Now.Hour;
            ShowMi.Value = DateTime.Now.Minute;
            ShowSS.Value = DateTime.Now.Millisecond;

            Devices = new DeviceDA().selectAllDate();
            cbDeviceID.DisplayMemberPath = "DeviceName";

            this.DataContext = this;
            SetDefult();
        }

        #region 处理设备通道信息
        private ObservableCollection<t_Device> _devices;
        public ObservableCollection<t_Device> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
            }
        }

        private ObservableCollection<t_Channel> _channels;
        public ObservableCollection<t_Channel> Channels
        {
            get
            {
                return _channels;
            }
            set
            {
                _channels = value;
            }
        }

        private int _DeviceID = 0;
        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get { return _DeviceID; }
            set { _DeviceID = value; }
        }

        private int _ChanncelID = 0;
        /// <summary>
        /// 通道ID
        /// </summary>
        public int ChanncelID
        {
            get { return _ChanncelID; }
            set { _ChanncelID = value; }
        }

        private t_Device _selectedDevices;

        public t_Device SelectedDevices
        {
            get
            {
                return _selectedDevices;
            }
            set
            {
                _selectedDevices = value;
                RaisePropertyChanged("SelectedDevices");
            }
        }
        private void RaisePropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetDefult()
        {
            this.LineName.Text = "";
            //LineType.SelectedIndex = 0;
            LineCZ.SelectedIndex = 0;
            LineShowType.SelectedIndex = 0;
            LineStyle.SelectedIndex = 0;

            LinePointBJ.SelectedIndex = 0;

            LineColor.Color = Colors.Blue;
            MinValue.Text = "0";
            MaxValue.Text = "100";
            ValueDecimal.Value = 0;

            FormartYear.IsChecked = false;
            FormartMonth.IsChecked = false;
            FormartDay.IsChecked = false;

            FormartHH.IsChecked = true;
            FormartMi.IsChecked = true;
            FormartSS.IsChecked = true;

            TimeLen.Text = "100";
            TimeLenType.SelectedIndex = 0;

            LineCYZQLent.Text = "1";
            LineCYZQType.SelectedIndex = 0;
            this.ComputeStr.Text = "";

            ShowYear.Value = DateTime.Now.Year;
            ShowMonth.Value = DateTime.Now.Month;
            ShowDay.Value = DateTime.Now.Day;

            ShowHH.Value = DateTime.Now.Hour;
            ShowMi.Value = DateTime.Now.Minute;
            ShowSS.Value = DateTime.Now.Millisecond;
        }

        private void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDeviceID.SelectedIndex < 0)
                return;
            t_Device d = (t_Device)cbDeviceID.Items[cbDeviceID.SelectedIndex];
            LoadChanncel(d.DeviceID);
        }
        private void LoadChanncel(int deviceid)
        {
            
                try
                {
                    var channels = new ChannelDA().selectBy(deviceid);                   
                    //LoadChanncelCommplete(channels);
                    cbChanncel.ItemsSource = channels;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LoadChanncel出错了" + ex.Message);
                }
           
        }

        //private void LoadChanncelCommplete(List<t_Channel> result)
        //{
        //    var v = result;
        //    if (v.Count() > 0)
        //    {                
        //        Channels = new ObservableCollection<t_Channel>(v);
        //        var vc = v.Where(a => a.ChannelNo == _ChanncelID);
        //        if (vc.Count() > 0)
        //        {
                    
        //            // cbChanncel.DisplayMemberPath = "ChannelName";
        //        }
        //    }
        //}


        //private void LoadChanncelCommplete(int deviceid, int ChanncelID)
        //{
        //    try
        //    {
        //        ObservableCollection<t_Channel> result = new ChannelDA().selectAllDate();
        //        if (result.Count > 0)
        //        {
        //            Channels = result;
        //            cbChanncel.ItemsSource = Channels;
        //            var vc = result.Where(a => a.ChannelNo == Convert.ToInt32(ChanncelID));
        //            if (vc.Count() > 0)
        //            {
        //                cbChanncel.SelectedItem = vc.First();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "出错啦！", MessageBoxButton.OK);
        //        return;
        //    }
        //}

        //private void LoadChanncelCommplete(int deviceid)
        //{
        //    try
        //    {
        //        ObservableCollection<t_Channel> result = new ChannelDA().selectAllDate();
        //        if (result.Count > 0)
        //        {
        //            Channels = result;
        //            cbChanncel.ItemsSource = Channels;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "出错啦！", MessageBoxButton.OK);
        //        return;
        //    }
        //}
        #endregion

       
        t_Element_RealTimeLine _RealTime = new t_Element_RealTimeLine();
        public void SetRealTimeLineOR(t_Element_RealTimeLine obj)
        {
            _RealTime = obj;

            if (obj == null)
                return;
            this.LineName.Text = obj.LineName;
            //LineType.SelectedIndex = obj.LineType;
            LineCZ.SelectedIndex = obj.LineCZ;
            LineShowType.SelectedIndex = obj.LineShowType;
            LineStyle.SelectedIndex = obj.LineStyle;

            LinePointBJ.SelectedIndex = obj.LinePointBJ;

            LineColor.Color = Common1.StringToColor(obj.LineColor);
            MinValue.Text = obj.MinValue;
            MaxValue.Text = obj.MaxValue;
            ValueDecimal.Value = obj.ValueDecimal;


            FormartYear.IsChecked = false;
            FormartMonth.IsChecked = false;
            FormartDay.IsChecked = false;
            FormartHH.IsChecked = false;
            FormartMi.IsChecked = false;
            FormartSS.IsChecked = false;


            if (obj.ShowFormat.IndexOf("yyyy") == 0)//年度
            {
                FormartYear.IsChecked = true;
            }
            if (obj.ShowFormat.IndexOf("MM") >= 0)//月
            {
                FormartMonth.IsChecked = true;
            }
            if (obj.ShowFormat.IndexOf("dd") >= 0)//日
            {
                FormartDay.IsChecked = true;
            }

            if (obj.ShowFormat.IndexOf("HH") >= 0)//日
            {
                FormartHH.IsChecked = true;
            }

            if (obj.ShowFormat.IndexOf("mm") >= 0)//分
            {
                FormartMi.IsChecked = true;
            }

            if (obj.ShowFormat.IndexOf("ss") >= 0)//秒
            {
                FormartSS.IsChecked = true;
            }

            TimeLen.Text = obj.TimeLen.ToString();
            TimeLenType.SelectedIndex = GetIndex(obj.TimeLenType);

            LineCYZQLent.Text = obj.LineCYZQLent;
            LineCYZQType.SelectedIndex = GetIndex(obj.LineCYZQType);


            if (obj.ComputeStr == null)
                obj.ComputeStr = "";
            this.ComputeStr.Text = obj.ComputeStr;

                var v = _devices.FirstOrDefault(a => a.DeviceID == obj.DeviceID);
                cbDeviceID.SelectedValue = v;

                //if (obj.ChannelNo.HasValue)
                //{

                if (null != v)
                {
                    LoadChanncel(v.DeviceID);
                }
                //}
                //else
                //{
                //    LoadChanncel(v.DeviceID);
                //}


            //obj.ComputeStr = this.ComputeStr.Text;
            ShowYear.Value = DateTime.Now.Year;
            ShowMonth.Value = DateTime.Now.Month;
            ShowDay.Value = DateTime.Now.Day;

            ShowHH.Value = DateTime.Now.Hour;
            ShowMi.Value = DateTime.Now.Minute;
            ShowSS.Value = DateTime.Now.Millisecond;
        }

        

        #region 时间范围处理
        private int GetIndex(string str)
        {
            if (str == "ss")
            {
                return 0;
            }
            else if (str == "mm")
            {
                return 1;
            }
            else if (str == "")
            {
                return 2;
            }

            return 3;
        }

        private string GetStr(int index)
        {
            if (index == 0)
            {
                return "ss";
            }
            else if (index == 1)
            {
                return "mm";
            }
            else if (index == 2)
            {
                return "hh";
            }
            return "dd";
        }
        #endregion

        public t_Element_RealTimeLine SaveRealTimeLineOR(t_Element_RealTimeLine obj)
        {
            _RealTime = obj;
           return SaveRealTimeLineOR();
        }
        public t_Element_RealTimeLine SaveRealTimeLineOR()
        {

            _RealTime.LineName = this.LineName.Text;

            _RealTime.LineType = this.LineStyle.SelectedIndex;
            _RealTime.LineCZ = LineCZ.SelectedIndex;
            _RealTime.LineShowType = LineShowType.SelectedIndex;
            _RealTime.LineStyle = LineStyle.SelectedIndex;
            _RealTime.LinePointBJ = LinePointBJ.SelectedIndex;

            _RealTime.LineColor = LineColor.Color.ToString();
            _RealTime.MinValue = MinValue.Text;
            _RealTime.MaxValue = MaxValue.Text;
            _RealTime.ValueDecimal = int.Parse(ValueDecimal.Value.ToString());

            string strShowFormat = string.Empty;
            if (this.FormartYear.IsChecked.Value)//年度
            {
                strShowFormat = "yyyy";
            }
            if (this.FormartMonth.IsChecked.Value)//月
            {
                strShowFormat = string.Format("{0}{1}MM", strShowFormat, string.IsNullOrEmpty(strShowFormat) ? "" : "-");
            }
            if (this.FormartDay.IsChecked.Value)//日
            {
                strShowFormat = string.Format("{0}{1}dd", strShowFormat, string.IsNullOrEmpty(strShowFormat) ? "" : "-");
            }

            if (this.FormartHH.IsChecked.Value)//时
            {
                strShowFormat = string.Format("{0}{1}HH", strShowFormat, string.IsNullOrEmpty(strShowFormat) ? "" : " ");
            }

            if (this.FormartMi.IsChecked.Value)//分
            {
                strShowFormat = string.Format("{0}{1}mm", strShowFormat, string.IsNullOrEmpty(strShowFormat) ? "" : ":");
            }

            if (this.FormartSS.IsChecked.Value)//秒
            {
                strShowFormat = string.Format("{0}{1}ss", strShowFormat, string.IsNullOrEmpty(strShowFormat) ? "" : ":");
            }
            _RealTime.ShowFormat = strShowFormat;

            _RealTime.TimeLen = int.Parse(TimeLen.Text);
            //ComboBoxItem cbi = (ComboBoxItem)TimeLenType.SelectedItem;
            _RealTime.TimeLenType = GetStr(TimeLenType.SelectedIndex);

            _RealTime.LineCYZQLent = LineCYZQLent.Text;
            //cbi = (ComboBoxItem)LineCYZQType.SelectedItem;
            _RealTime.LineCYZQType = GetStr(LineCYZQType.SelectedIndex);

            if (cbDeviceID.SelectedIndex >= 0)
            {
                t_Device d = (t_Device)cbDeviceID.Items[cbDeviceID.SelectedIndex];
                _RealTime.DeviceID = d.DeviceID;
            }

            if (cbChanncel.SelectedIndex >= 0)
            {
                t_Channel tc = (t_Channel)cbChanncel.Items[cbChanncel.SelectedIndex];
                _RealTime.ChannelNo = tc.ChannelNo;
            }

            _RealTime.ComputeStr = this.ComputeStr.Text;

            string strTime = string.Format("{0}:{1}:{2} {3}:{4}:{5}", this.ShowYear.Value, this.ShowMonth.Value, this.ShowDay.Value,
                this.ShowHH.Value, this.ShowMi.Value, this.ShowSS.Value);

            return _RealTime;

        }

        public void HideInfo()
        {
            tbItemInfo.Visibility = Visibility.Collapsed;
            GridItemInfo.Visibility = Visibility.Collapsed;

            tbTime.IsSelected = true;
        }
    }
}
