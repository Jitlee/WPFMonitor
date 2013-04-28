using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WPFMonitor.DAL.Sys;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using System.Windows.Controls;

namespace WPFMonitor.Core.ViewModel.SerMonitor
{
    public class AlarmLogViewModel : EditBaseViewModel
    {

       public AlarmLogViewModel()
       {
           Init();
       }

       public void Init()
       {
           StationDA _staDA = new StationDA();
           StationORList = _staDA.selectAllStation();

           if (StationORList != null && StationORList.Count > 0)
               SelectStationOR = StationORList.First();
       }

       #region 站点、设备、通首管理
       DeviceDA _DeviceDA = new DeviceDA();
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
           SelectDeviceOR = null;

           if (DeviceORList != null)
               DeviceORList.Clear();
           var v = _DeviceDA.selectDevices(SelectStationOR.Stationid.ToString());

           foreach (DeviceOR obj in v)
           {
               DeviceORList.Add(obj);
           }
       }

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

           var v = _DeviceDA.selectChannels(SelectDeviceOR.Deviceid);
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
       
       #endregion

       #region 修改默认内容,事件
       private ICommand _ShearchCommand;
        public ICommand ShearchCommand
        {
            get
            {
                if (null == this._ShearchCommand)
                {
                    this._ShearchCommand = new DelegateCommand(this.ShearchCommand_Click);
                }
                return this._ShearchCommand;
            }
        }
        protected void ShearchCommand_Click()
        {
            
        }
        #endregion

        public override void OK()
        {

        }
    }
}
