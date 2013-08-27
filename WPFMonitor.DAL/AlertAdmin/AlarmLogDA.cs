using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.Model.AlertAdmin;
using System.Data;
using System.Collections.ObjectModel;

namespace WPFMonitor.DAL.AlertAdmin
{
	public class AlarmLogDA : DALBase
	{
		public ObservableCollection<AlarmLogOR> SelectAllLog()
		{
//            string sql = @"SELECT TOP 50 
//	   [AlarmLogID]
//      ,t_Station.[StationID]
//      ,[Content]
//      ,[HappenTime]
//      ,[DeviceID]
//      ,[RelieveTime]
//      ,[AlarmLevel]
//      ,[OperateUserID]
//      ,[MonitorValue]
//      ,[PolicyID]
//      ,AlarmLeftTime
//      ,EventsName
//      ,AlarmType
//      ,LastTime
//      ,t_Station.StationName
//  FROM t_AlarmLog
//  LEFT JOIN  t_Station ON t_Station.StationID= t_AlarmLog.StationID  
//  WHERE RelieveTime IS NULL
//  ORDER BY HappenTime DESC";
            string sql = @"select top 50 alrm.*,dev.DeviceName,sta.StationName  from t_AlarmLog 
alrm,t_Device  dev,t_Station sta
where  alrm.DeviceID=dev.DeviceID 
and alrm.stationid= sta.StationID order by HappenTime  desc";
			DataTable dt = db.ExecuteQuery(sql);
			if (dt == null)
				return null;
			ObservableCollection<AlarmLogOR> listOR = new ObservableCollection<AlarmLogOR>();
			foreach (DataRow dr in dt.Rows)
			{
				AlarmLogOR obj = new AlarmLogOR(dr);
				listOR.Add(obj);
			}
			return listOR;
		}

		public bool Confirm(int AlarmLogID)
		{
			try
			{
				string sql = string.Format("update t_AlarmLog set RelieveTime=GETDATE() where AlarmLogID={0}", AlarmLogID);
				return	db.ExecuteNoQuery(sql)>0;
			}
			catch (Exception ex)
			{

			}
			return false;
		}
	}
}
