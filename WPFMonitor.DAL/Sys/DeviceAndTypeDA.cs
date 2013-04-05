using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using WPFMonitor.Model.Sys;

namespace WPFMonitor.DAL.Sys
{
    public class DeviceAndTypeDA : DALBase
    {
        public ObservableCollection<DeviceTypeOR> GetAllDeviceType()
        {
            string sql = "select * from t_DeviceType";
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQueryDataSet(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<DeviceTypeOR> _List = new ObservableCollection<DeviceTypeOR>();
            foreach (DataRow dr in dt.Rows)
            {
                DeviceTypeOR obj = new DeviceTypeOR(dr);
                _List.Add(obj);
            }
            return _List;
        }

        /// <summary>
        /// 根据站点、设备类型查询设备
        /// </summary>
        /// <param name="nStationID"></param>
        /// <param name="mDeviceTypeID"></param>
        /// <returns></returns>
        public ObservableCollection<DeviceOR> GetAllGenerdDevice(string nStationID, string mDeviceTypeID)
        {
            string sql = string.Format(@"select d.*,dt.DeviceTypeID from t_Device d
inner join  t_DeviceType dt on d.DeviceTypeID=dt.DeviceTypeID and d.stationid={0} and dt.DeviceTypeID={1} "
                , nStationID, mDeviceTypeID);
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

        public ObservableCollection<ChannelOR> SelectChannels(string DeviceID)
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
