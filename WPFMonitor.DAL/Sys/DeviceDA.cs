using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using WPFMonitor.Model.Sys;

namespace WPFMonitor.DAL.Sys
{
   public class DeviceDA:DALBase
    {
       public ObservableCollection<DeviceOR> selectDevices(string strid)
       {
           string sql = string.Format("select * from t_Device where StationID={0} order by StationID,DeviceName", strid);
           DataTable dt = null;
           try
           {
               dt = db.ExecuteQuery(sql);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           ObservableCollection<DeviceOR> _List = new ObservableCollection<DeviceOR>();
           foreach (DataRow dr in dt.Rows)
           {
               DeviceOR obj = new DeviceOR(dr);
               _List.Add(obj);
           }
           return _List;
       }

       public ObservableCollection<ChannelOR> selectChannels(string DeviceID)
       {
           string sql = string.Format("select * from t_Channel where DeviceID={0} order by deviceid,ChannelName", DeviceID);

           DataTable dt = null;
           try
           {
               dt = db.ExecuteQuery(sql);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           ObservableCollection<ChannelOR> _List = new ObservableCollection<ChannelOR>();
           foreach (DataRow dr in dt.Rows)
           {
               ChannelOR obj = new ChannelOR(dr);
               _List.Add(obj);
           }
           return _List;
       }

    }
}
