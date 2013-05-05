using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using System.Collections;
using System.Collections.ObjectModel;
using WPFMonitor.Model.AlertAdmin;


namespace WPFMonitor.DAL.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmPolicyManagementDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_AlarmPolicyManagement";
            if (!string.IsNullOrEmpty(where))
            {
                sql = string.Format(" {0} where {1}", sql, where);
            }
            DataTable dt = null;
            int returnC = 0; try
            {
                dt = db.ExecuteQuery(sql, pageCrrent, pageSize, out returnC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            pageCount = returnC;
            return dt;
        }

        public AlarmPolicyManagementOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_AlarmPolicyManagement where  Alarmpolicymanagementid='{0}'", m_id);
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQueryDataSet(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (dt == null)
                return null;
            if (dt.Rows.Count == 0)
                return null;
            DataRow dr = dt.Rows[0];
            AlarmPolicyManagementOR m_Alar = new AlarmPolicyManagementOR(dr);
            return m_Alar;

        }
        public AlarmPolicyManagementOR selectARowDate(int StationID, int DeviceTypeID, int DeviceID, int DeviceChannelID)
        {
            string sql = string.Format(@"select * from t_AlarmPolicyManagement where  
StationID={0} and DeviceTypeID={1} and DeviceID={2} and DeviceChannelID={3}",
        StationID, DeviceTypeID, DeviceID, DeviceChannelID);
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQueryDataSet(sql).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (dt == null)
                return null;
            if (dt.Rows.Count == 0)
                return null;
            DataRow dr = dt.Rows[0];
            AlarmPolicyManagementOR m_Alar = new AlarmPolicyManagementOR(dr);
            return m_Alar;

        }

        public ObservableCollection<AlarmPolicyManagementOR> selectAllDate()
        {
            string sql = "select * from t_AlarmPolicyManagement";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<AlarmPolicyManagementOR> _List = new ObservableCollection<AlarmPolicyManagementOR>();
            foreach (DataRow dr in dt.Rows)
            {
                AlarmPolicyManagementOR obj = new AlarmPolicyManagementOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_AlarmPolicyManagement
        /// </summary>
        public virtual bool Insert(AlarmPolicyManagementSaveOR objPolicy)
        {
            string sql = string.Format(@"insert into t_AlarmPolicyManagement (StationID,DeviceTypeID,DeviceID,DeviceChannelID,
ValueType,MaxValue,MinValue,SwitchValue,MaxTiggerType,MinTiggerType,AlarmTimes , AlarmfilterTimes,EventID,IsEnable,AlarmAudioFile,
DisAlarmAudioFile,SmsMsg,LightID,ReleaseLightID)
values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},'{12}',{13},'{14}','{15}','{16}',{17},{18})", 
            objPolicy.Stationid, objPolicy.Devicetypeid, objPolicy.Deviceid, objPolicy.Devicechannelid, objPolicy.Valuetype,
           objPolicy.Maxvalue, objPolicy.Minvalue, objPolicy.Switchvalue,
           objPolicy.Mintiggertype, objPolicy.Mintiggertype,
           objPolicy.Alarmtimes, objPolicy.Alarmfiltertimes, objPolicy.Eventid, objPolicy.Isenable, objPolicy.Alarmaudiofile, objPolicy.Disalarmaudiofile,
           objPolicy.Smsmsg, objPolicy.Lightid, objPolicy.Releaselightid);

            return db.ExecuteNoQuery(sql) > 0;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_AlarmPolicyManagement
        /// </summary>
        public virtual bool Update(AlarmPolicyManagementSaveOR objPolicy)
        {

            string sql = string.Format(@"update dbo.t_AlarmPolicyManagement set ValueType={0},MaxValue={1},MinValue={2},SwitchValue={3},MaxTiggerType={4},MinTiggerType={5}, 
            AlarmTimes={6} , AlarmfilterTimes={7},EventID='{8}',IsEnable={9},AlarmAudioFile='{10}',DisAlarmAudioFile='{11}',SmsMsg='{12}',LightID={13},ReleaseLightID={14}
            where   StationID ={15} and DeviceTypeID = {16} and DeviceID ={17} and DeviceChannelID = {18}",
           objPolicy.Valuetype, objPolicy.Maxvalue, objPolicy.Minvalue, objPolicy.Switchvalue, objPolicy.Maxtiggertype, objPolicy.Mintiggertype, objPolicy.Alarmtimes, objPolicy.Alarmfiltertimes, objPolicy.Eventid, objPolicy.Isenable,
           objPolicy.Alarmaudiofile, objPolicy.Disalarmaudiofile, objPolicy.Smsmsg, objPolicy.Lightid, objPolicy.Releaselightid, objPolicy.Stationid, objPolicy.Devicetypeid, objPolicy.Deviceid, objPolicy.Devicechannelid);
            return db.ExecuteNoQuery(sql) > 0;
        }
        #endregion

       
        #region DELETE
        /// <summary>
        /// 删除t_AlarmPolicyManagement
        /// </summary>
        public void Delete(int StationID, int DeviceTypeID,int DeviceID, int DeviceChannelID)
        {

            string sql = string.Format(@"delete t_AlarmPolicyManagement where  
StationID={0} and DeviceTypeID={1} and DeviceID={2} and DeviceChannelID={3}",
 StationID, DeviceTypeID, DeviceID, DeviceChannelID);
            db.ExecuteNoQuery(sql);
        }
        #endregion
        
    }
}

