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
using WPFMonitor.DAL;
using WPFMonitor.Model;


namespace WPFMonitor.Library.ZTControls
{
    public partial class SetSinglePropertyDrawLine : Window
    {
        #region 属性
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


        private int _LevelNo = 1;
        /// <summary>
        /// 控件层次ID
        /// </summary>
        public int LevelNo
        {
            get { return _LevelNo; }
            set { _LevelNo = value; }
        }

        private string _ComputeStr = "";
        /// <summary>
        /// 表达式
        /// </summary>
        public string ComputeStr
        {
            get { return _ComputeStr; }
            set { _ComputeStr = value; }
        }

        private int _Method = 0;
        /// <summary>
        /// 区间运算数值
        /// </summary>
        public int Method
        {
            get { return _Method; }
            set { _Method = value; }
        }

        private double? _MinFloat;
        /// <summary>
        /// 最小区间数值
        /// </summary>
        public double? MinFloat
        {
            get { return _MinFloat; }
            set { _MinFloat = value; }
        }

        private double? _MaxFloat;
        /// <summary>
        /// 最大区间计算值
        /// </summary>
        public double? MaxFloat
        {
            get { return _MaxFloat; }
            set { _MaxFloat = value; }
        }


        private bool _IsOK = false;
        /// <summary>
        /// 是否点击的OK按钮
        /// </summary>
        public bool IsOK
        {
            get { return _IsOK; }
        }


        #endregion
        MonitorServers _dataContext = new MonitorServers();
        CV _DataCV = new CV();
        public SetSinglePropertyDrawLine()
        {
            InitializeComponent();
            _dataContext = LoadScreen._DataContext;

            cbDeviceID.ItemsSource = _DataCV.t_Devices;
            cbDeviceID.DisplayMemberPath = "DeviceName";
        }

        public void Init()
        {
            var v = LoadScreen._DataCV.t_Devices.Where(a => a.DeviceID == _DeviceID);
            if (v.Count() > 0)
            {
                cbDeviceID.SelectedItem = v.First();
            }
            //通道
            cbLayer.SelectedIndex = _LevelNo - 1;
            txtBDS.Text = _ComputeStr;
            if (this._Method == 1)
            {
                cbFlot.IsChecked = true;
                txtMin.Text = _MinFloat.ToString();
                txtMax.Text = _MaxFloat.ToString();
            }
            else
            {
                ShowOrHide(System.Windows.Visibility.Collapsed);
            }
        }

        private void cbDeviceID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cbChanncel.Items.Clear();
            t_Device d=(t_Device)cbDeviceID.Items[cbDeviceID.SelectedIndex];
            LoadChanncel(d.DeviceID);

        }

        private void LoadChanncel(int deviceid)
        {
            _DataCV.Load(_DataCV.GetT_ChannelQuery().Where(a => a.DeviceID == deviceid),
                LoadChanncelCommplete, deviceid);
        }

        private void LoadChanncelCommplete(LoadOperation<t_Channel> result)
        {
            if (result.HasError)
            {
                MessageBox.Show(result.Error.Message, "出错啦！", MessageBoxButton.OK);
                return;
            }
            
            var v = result.Entities;
            if (v.Count() > 0)
            {
                cbChanncel.ItemsSource = v;

                var vc = v.Where(a => a.ChannelNo == _ChanncelID);
                if (vc.Count() > 0)
                {
                    cbChanncel.SelectedItem = vc.First();
                   // cbChanncel.DisplayMemberPath = "ChannelName";
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            _IsOK = true;

            if (cbDeviceID.SelectedValue == null)
            {
                MessageBox.Show("请选择设备！", "温馨提示！", MessageBoxButton.OK);
                return;
            }
            if (cbChanncel.SelectedValue == null)
            {

                MessageBox.Show("请选择通道！", "温馨提示！", MessageBoxButton.OK);
                return;
            }

            if (cbFlot.IsChecked.Value)
            {
                double mMax, mMin;
                if (!double.TryParse(txtMax.Text, out mMax) || !double.TryParse(txtMin.Text, out mMin))
                {
                    MessageBox.Show("请输入正确的区间计算值！", "温馨提示！", MessageBoxButton.OK);
                    return;
                }
                Method = 1;
                MaxFloat =mMax;
                MinFloat = mMin;
            }
            else
            {
                Method = 0;
                MaxFloat = 0;
                MinFloat = 0;
            }

            _DeviceID = ((t_Device)cbDeviceID.SelectedValue).DeviceID;
            _ChanncelID = ((t_Channel)cbChanncel.SelectedValue).ChannelNo;
            _LevelNo =int.Parse( ((ComboBoxItem)cbLayer.SelectedItem).Content.ToString());
            _ComputeStr = txtBDS.Text;

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _IsOK = false;
            this.DialogResult = false;
        }

        private void cbFlot_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbFlot_Click(object sender, RoutedEventArgs e)
        {
            if (cbFlot.IsChecked.Value)
            {
                ShowOrHide(System.Windows.Visibility.Visible);
            }
            else
            {
                ShowOrHide(System.Windows.Visibility.Collapsed);
            }
        }
        private void ShowOrHide(Visibility isShow)
        {
            float1.Visibility = isShow;
            float2.Visibility = isShow;
            float3.Visibility = isShow;
            float4.Visibility = isShow;
            txtMax.Visibility= isShow;
            txtMin.Visibility = isShow;
        }

    }
}

