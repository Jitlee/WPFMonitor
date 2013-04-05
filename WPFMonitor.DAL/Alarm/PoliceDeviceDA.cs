using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using WPFMonitor.Model.Alarm;
using WPFMonitor.Model.Sys;

namespace WPFMonitor.DAL.Alarm
{
    public class PoliceDeviceDA : DALBase
    {
        /// <summary>
        /// 根据机房ID查询已配置的策略
        /// </summary>
        /// <param name="strStationID"></param>
        /// <returns></returns>
        public ObservableCollection<DeviceOR> selectDevices(string strStationID)
        {
            string sql = string.Format(@"select distinct d.* from t_Device d 
inner join  t_Channel A  on  a.deviceid=d.deviceid
inner join t_AlarmPolicyManagement B on A.DeviceID=B.DeviceID AND DeviceChannelID=ChannelNo 
where d.stationid={0} order by d.deviceid", strStationID);
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQueryDataSet(sql).Tables[0];
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

        /// <summary>
        /// 根据机房、设备查询已配置了的策略
        /// </summary>
        /// <param name="strStationID"></param>
        /// <param name="strDeviceID"></param>
        /// <returns></returns>
        public ObservableCollection<PolicyChannelOR> selectChannels(string strStationID, string strDeviceID)
        {
            string sql = string.Format(@"select distinct d.deviceid,d.devicename,ChannelNo,a.ChannelName
,AlarmPolicyManagementID from t_Device d inner join  t_Channel A  on  a.deviceid=d.deviceid
inner join t_AlarmPolicyManagement B on A.DeviceID=B.DeviceID AND DeviceChannelID=ChannelNo 
where d.stationid={0} and d.deviceid={1} order by d.deviceid", strStationID, strDeviceID);
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQueryDataSet(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<PolicyChannelOR> _List = new ObservableCollection<PolicyChannelOR>();
            foreach (DataRow dr in dt.Rows)
            {
                PolicyChannelOR obj = new PolicyChannelOR(dr);
                _List.Add(obj);
            }
            return _List;
        }
    }
}
