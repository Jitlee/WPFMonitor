using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WPFMonitor.DAL.SerMonitor
{
    public class DailyreportDA : DALBase
    {
        public DailyreportDA()
        {
            db = MoniBase;
        }

        //public IList<DeviceTypeObj> GetAllDeviceType()
        //{
        //    //string strSql = string.Format("select * from t_DeviceType where StationID={0}", StationID);
        //    string strSql = string.Format("select * from t_DeviceType");
        //    DataTable dt = db.ExecuteQuery(strSql);

        //    IList<DeviceTypeObj> m_list = new List<DeviceTypeObj>();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DeviceTypeObj m_obj = new DeviceTypeObj();
        //        m_obj.DeviceTypeID = int.Parse(dt.Rows[i]["DeviceTypeID"].ToString());
        //        m_obj.strParseDLL = dt.Rows[i]["ParseDLL"].ToString();
        //        m_obj.TypeName = dt.Rows[i]["TypeName"].ToString();
        //        m_obj.SaveTimeInteval = int.Parse(dt.Rows[i]["SaveTimeInteval"].ToString());
        //        try
        //        {
        //            m_obj.StationID = int.Parse(dt.Rows[i]["StationID"].ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        m_list.Add(m_obj);
        //    }

        //    return m_list;
        //}

        public string IsGenerdDll(int DeviceTypeID)
        {
            string strSql = string.Format("select ParseDll from t_DeviceType where DeviceTypeID={0}", DeviceTypeID);
            return db.ExecuteScalar(strSql).ToString();
        }

        public DataTable GetHistoryChannelValue(string devName, int nDeviceID, DateTime begin, DateTime end, int StationID, int ChannelNO)
        {

            // 以1970年为限，（年份－1970）×12+月份为数值，一直循环到结束时间
            int t1 = (begin.Year - 1970) * 12 + begin.Month - 1;
            int t2 = (end.Year - 1970) * 12 + end.Month - 1;
            int t = 0;
            bool bFirst = true;
            DataTable result = null;
            for (t = t1; t <= t2; t++)
            {
                int year = t / 12 + 1970;
                int month = t % 12 + 1;

                // 生成表名
                string strmonth = string.Format("{0:00}", month);// 00001234
                string targetTable = "t_" + StationID.ToString().Trim() + "_" + devName + "_" + Convert.ToString(year) + "_" + strmonth;

                //string tableName2 = targetTable;
                string tableNametemp = "\"" + targetTable + "\"";
                string tableName2 = "";
                foreach (char c in tableNametemp)
                {
                    if (c == '(' || c == ')')
                    {
                        tableName2 += new string('/', 1);
                    }
                    tableName2 += new string(c, 1);
                }

                string strSQL = "SELECT count(*) FROM dbo.sysobjects WHERE id = OBJECT_ID(N'" + tableName2 + "') AND OBJECTPROPERTY(id, N'IsUserTable') = 1";
                string s = db.ExecuteScalar(strSQL).ToString();
                if (s == "" || Convert.ToInt32(s) <= 0)
                    continue;


                // 根据表名和时间返回数据
                string time1 = begin.ToString("yyyy-MM-dd HH:mm:ss");
                string time2 = end.ToString("yyyy-MM-dd HH:mm:ss");
                string strdev = Convert.ToString(nDeviceID);

                //string strSql = string.Format("select t2.DeviceName,t3.ChannelName ,t1.DeviceID,t1.ChannelNo, t1.MonitorValue,t1.MonitorTime from {0} t1,t_Device t2, t_Channel t3 where t1.DeviceID=t2.DeviceID and t3.deviceid=t1.deviceid and t3.channelno = t1.channelno and t1.DeviceID={1} and t1.MonitorTime between '{2}' and '{3}' and t1.StationID={4} and t1.ChannelNo={5}", tableName2, nDeviceID, time1, time2, StationID,ChannelNO);
                string strSql = string.Format(@"select t1.DataID,t1.DeviceID,t1.ChannelNo,t1.MonitorValue,t1.MonitorTime,t1.StationID,t1.Info 
 ,cast(datepart(hour,MonitorTime) as varchar) +':' + cast(datepart(minute,MonitorTime) as varchar) as TimeS 
,ts.StationName,t2.DeviceName,t3.ChannelName
from {0} t1,t_Device t2, t_Channel t3,t_Station ts where t1.DeviceID=t2.DeviceID and t3.deviceid=t1.deviceid 
and t3.channelno = t1.channelno  and ts.StationID =t1.stationid
and t1.DeviceID={1} and t1.MonitorTime between '{2}' and '{3}' and t1.StationID={4} and t1.ChannelNo={5} order by MonitorTime ", tableName2, nDeviceID, time1, time2, StationID, ChannelNO);


                DataTable dt = db.ExecuteQuery(strSql);

                if (bFirst)
                    result = dt;
                else
                    result.Merge(dt);

                bFirst = false;
            }


            return result;
        }

        public DataTable GetHistoryChannelValue(string strTime, string endTim, int ChannelNO, int devid)
        {
            string sql = string.Format("select * from t_ChannelHistoryValue where MonitorTime  between '{0}' and '{1}' and ChannelNo={2} and DeviceID={3}", strTime, endTim, ChannelNO, devid);
            DataTable dt = db.ExecuteQuery(sql);
            return dt;
        }

    }
}
