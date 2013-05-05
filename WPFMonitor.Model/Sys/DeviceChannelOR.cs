using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;

namespace WPFMonitor.Model.Sys
{
    public class DeviceChannelOR:DeviceOR
    {
        public DeviceChannelOR(DataRow dr):base(dr)
        {

        }
        /// <summary>
        /// 设备下的通道列表
        /// </summary>
        public ObservableCollection<ChannelOR> ChannelORList
        {
            set;
            get;
            
        }
    }
}
