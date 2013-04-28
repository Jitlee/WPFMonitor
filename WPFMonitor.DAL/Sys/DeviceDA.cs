using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using WPFMonitor.Model.Sys;

namespace WPFMonitor.DAL.Sys
{
    public class DeviceDA : DALBase
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

       public ObservableCollection<DeviceChannelOR> selectDeviceChannels(string strid)
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
           ObservableCollection<DeviceChannelOR> _List = new ObservableCollection<DeviceChannelOR>();
           foreach (DataRow dr in dt.Rows)
           {
               DeviceChannelOR obj = new DeviceChannelOR(dr);
               _List.Add(obj);
           }
           return _List;
       }

       public ObservableCollection<ChannelOR> selectChannels(int DeviceID)
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

       public List<DeviceObj> GetAllDevice(int StationID)
       {
           string strSql = string.Format("select * from v_Device where StationID={0} and StationID is not null", StationID);
           DataTable dt = db.ExecuteQuery(strSql);

           List<DeviceObj> m_list = new List<DeviceObj>();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DeviceObj m_obj = new DeviceObj();
               m_obj.strDeviceName = dt.Rows[i]["DeviceName"].ToString();
               m_obj.nCommunicateType = int.Parse(dt.Rows[i]["CommunicateType"].ToString());
               m_obj.nCommunicateID = int.Parse(dt.Rows[i]["CommunicateID"].ToString());
               m_obj.strSubAddr = dt.Rows[i]["SubAddr"].ToString();
               m_obj.nDeviceTypeID = int.Parse(dt.Rows[i]["DeviceTypeID"].ToString());
               m_obj.nDeviceID = int.Parse(dt.Rows[i]["DeviceID"].ToString());
               m_obj.strParseDLL = dt.Rows[i]["ParseDll"].ToString();
               m_obj.strTypeName = dt.Rows[i]["TypeName"].ToString();

               m_list.Add(m_obj);

           }

           return m_list;
       }

        
       #region 报警管理
       //select 
//*,dbo.F_ISExisAlarmPolicy({0},{1},{2},Channelno) as ISHavePolice from t_Channel
//select * from t_Channel

       public ObservableCollection<DeviceAddTypeOR> GetAllGenerdDevice(string nStationID)
       {
           string sql = string.Format(@"select d.*,dt.DeviceTypeID from t_Device d
inner join  t_DeviceType dt on d.DeviceTypeID=dt.DeviceTypeID and d.stationid={0}", nStationID);
           DataTable dt = null;
           try
           {
               dt = db.ExecuteQueryDataSet(sql).Tables[0];
           }
           catch (Exception ex)
           {
               throw ex;
           }
           ObservableCollection<DeviceAddTypeOR> _List = new ObservableCollection<DeviceAddTypeOR>();
           foreach (DataRow dr in dt.Rows)
           {
               DeviceAddTypeOR obj = new DeviceAddTypeOR(dr);
               _List.Add(obj);
           }
           return _List;
       }


       public ObservableCollection<ChannelManagementOR> SelectChannelManagements(int strDeviceID, int StationID, int DeviceTypeID)
       {
           string sql = string.Format(@"select 
*,dbo.F_ISExisAlarmPolicy({0},{1},{2},Channelno) as ISHavePolice from t_Channel where DeviceID={2} order by ChannelName",
 StationID, DeviceTypeID, strDeviceID);

           DataTable dt = null;
           try
           {
               dt = db.ExecuteQuery(sql);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           ObservableCollection<ChannelManagementOR> _List = new ObservableCollection<ChannelManagementOR>();
           foreach (DataRow dr in dt.Rows)
           {
               ChannelManagementOR obj = new ChannelManagementOR(dr);
               obj.StationID = StationID;
               obj.Deviceid = strDeviceID;
               obj.DeviceTypeID = DeviceTypeID;
               _List.Add(obj);
           }
           return _List;
       }
       #endregion

       #region 获取名称

       public string GetDeviceTypeName(int strTypeID)
       {
           string sql = string.Format("select TypeName from t_DeviceType   where DeviceTypeID='{0}'", strTypeID);
           return db.ExecuteScalar(sql).ToString();
       }

       public string GetDeviceName(int strDeviceID)
       {
           string sql = string.Format("select DeviceName from t_Device   where DeviceID='{0}'", strDeviceID);
           return db.ExecuteScalar(sql).ToString();
       }
       public string GetChannelName(int strChannelID)
       {
           string sql = string.Format("select ChannelName from t_Channel   where ChannelNo='{0}'", strChannelID);
           return db.ExecuteScalar(sql).ToString();
       }
       #endregion

       /// <summary>
       /// 获取通道值类型
       /// </summary>
       /// <param name="DevID"></param>
       /// <param name="ChannelNo"></param>
       /// <returns></returns>
       public int GetChannelValueType(int DevID, int ChannelNo)
       {
           int ValueType = 0;
           string sql = string.Format("select * from dbo.t_ChannelType where DeviceTypeID=(select DeviceTypeID from dbo.t_Device where DeviceID={0} ) and ChannelNo={1}", DevID, ChannelNo);
           DataTable dt = db.ExecuteQuery(sql);
           if (dt != null && dt.Rows.Count > 0)
           {
           }
           else
           {
               sql = string.Format("select * from dbo.t_GenChannelType where DeviceTypeID=(select DeviceTypeID from dbo.t_Device where DeviceID={0} ) and ChannelNo={1}", DevID, ChannelNo);
               dt = db.ExecuteQuery(sql);
           }
           if (dt != null && dt.Rows.Count > 0)
               if (dt.Rows[0]["ValueType"].ToString() != string.Empty)
                   ValueType = int.Parse(dt.Rows[0]["ValueType"].ToString());
               else
                   ValueType = 0;
           return ValueType;
       }


       /// <summary>
       /// 所有事件名
       /// </summary>
       /// <returns></returns>
       public ObservableCollection<string> GetAllEventsName()
       {
           string sql = string.Format("select distinct EventsName from t_AlarmLog where EventsName  is not null order by EventsName ");
           DataTable dt = db.ExecuteQuery(sql);
           ObservableCollection<string> EventsNames = new ObservableCollection<string>();
           foreach (DataRow dr in dt.Rows)
           {
               EventsNames.Add(dr["EventsName"].ToString());
           }
           return EventsNames;
       }

    }
}
