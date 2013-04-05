using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WPFMonitor.Model.AlertAdmin;

namespace WPFMonitor.DAL.AlertAdmin
{
    public class AlarmPolicyManagementDA : DALBase
    {
        public AlarmPolicyManagementDA()
        {
            db = MoniBase;
        }
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
        public AlarmPolicyManagementOR selectARowDate(string strAlarmPolicyManagementID)
        {
            string sql = string.Format(@"select * from t_AlarmPolicyManagement where  
AlarmPolicyManagementID={0}", strAlarmPolicyManagementID);
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

        public AlarmPolicyManagementOR selectARowDate(string StationID, string DeviceTypeID,
            string DeviceID, string DeviceChannelID)
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

        #endregion

        #region 插入
        /// <summary>
        /// 插入t_AlarmPolicyManagement
        /// </summary>
        public virtual bool Insert(AlarmPolicyManagementOR objPolicy)
        {
            string sql = string.Format("insert into t_AlarmPolicyManagement (StationID,DeviceTypeID,DeviceID,DeviceChannelID,ValueType,MaxValue,MinValue,SwitchValue,MaxTiggerType,MinTiggerType,AlarmTimes , AlarmfilterTimes,EventID,IsEnable,AlarmAudioFile,DisAlarmAudioFile,SmsMsg,LightID,ReleaseLightID)"
           + "values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},'{12}',{13},'{14}','{15}','{16}',{17},{18})", objPolicy.Stationid, objPolicy.Devicetypeid, objPolicy.Deviceid, objPolicy.Devicechannelid, objPolicy.Valuetype,
           objPolicy.Maxvalue, objPolicy.Minvalue, objPolicy.Switchvalue,
           objPolicy.Mintiggertype, objPolicy.Mintiggertype,
           objPolicy.Alarmtimes, objPolicy.Alarmfiltertimes, objPolicy.Eventid, objPolicy.Isenable, objPolicy.Alarmaudiofile, objPolicy.Disalarmaudiofile,
           objPolicy.Smsmsg, objPolicy.Lightid, objPolicy.Releaselightid);

            return db.ExecuteNoQuery(sql) > 0;

            //string sql = "insert into t_AlarmPolicyManagement (StationID, DeviceTypeID, DeviceID, DeviceChannelID, ValueType, MaxTiggerType, MaxValue, MinTiggerType, MinValue, SwitchValue, AlarmLevel, AlarmTarget, AlarmWay, IsEnableFrequency, AlarmAudioFile, DisAlarmAudioFile, AlarmTimes, AlarmfilterTimes, SmsMsg, AlarmVerify, IsEnable, EventID, LightID, ReleaseLightID) values ( @StationID, @DeviceTypeID, @DeviceID, @DeviceChannelID, @ValueType, @MaxTiggerType, @MaxValue, @MinTiggerType, @MinValue, @SwitchValue, @AlarmLevel, @AlarmTarget, @AlarmWay, @IsEnableFrequency, @AlarmAudioFile, @DisAlarmAudioFile, @AlarmTimes, @AlarmfilterTimes, @SmsMsg, @AlarmVerify, @IsEnable, @EventID, @LightID, @ReleaseLightID)";
            //SqlParameter[] parameters = new SqlParameter[]
            //{
            //    new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alarmPolicyManagement.Stationid),
            //    new SqlParameter("@DeviceTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceTypeID", DataRowVersion.Default, alarmPolicyManagement.Devicetypeid),
            //    new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, alarmPolicyManagement.Deviceid),
            //    new SqlParameter("@DeviceChannelID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceChannelID", DataRowVersion.Default, alarmPolicyManagement.Devicechannelid),
            //    new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, alarmPolicyManagement.Valuetype),
            //    new SqlParameter("@MaxTiggerType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "MaxTiggerType", DataRowVersion.Default, alarmPolicyManagement.Maxtiggertype),
            //    new SqlParameter("@MaxValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MaxValue", DataRowVersion.Default, alarmPolicyManagement.Maxvalue),
            //    new SqlParameter("@MinTiggerType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "MinTiggerType", DataRowVersion.Default, alarmPolicyManagement.Mintiggertype),
            //    new SqlParameter("@MinValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MinValue", DataRowVersion.Default, alarmPolicyManagement.Minvalue),
            //    new SqlParameter("@SwitchValue", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "SwitchValue", DataRowVersion.Default, alarmPolicyManagement.Switchvalue),
            //    new SqlParameter("@AlarmLevel", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmLevel", DataRowVersion.Default, alarmPolicyManagement.Alarmlevel),
            //    new SqlParameter("@AlarmTarget", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmTarget", DataRowVersion.Default, alarmPolicyManagement.Alarmtarget),
            //    new SqlParameter("@AlarmWay", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmWay", DataRowVersion.Default, alarmPolicyManagement.Alarmway),
            //    new SqlParameter("@IsEnableFrequency", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsEnableFrequency", DataRowVersion.Default, alarmPolicyManagement.Isenablefrequency),
            //    new SqlParameter("@AlarmAudioFile", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "AlarmAudioFile", DataRowVersion.Default, alarmPolicyManagement.Alarmaudiofile),
            //    new SqlParameter("@DisAlarmAudioFile", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "DisAlarmAudioFile", DataRowVersion.Default, alarmPolicyManagement.Disalarmaudiofile),
            //    new SqlParameter("@AlarmTimes", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmTimes", DataRowVersion.Default, alarmPolicyManagement.Alarmtimes),
            //    new SqlParameter("@AlarmfilterTimes", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmfilterTimes", DataRowVersion.Default, alarmPolicyManagement.Alarmfiltertimes),
            //    new SqlParameter("@SmsMsg", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "SmsMsg", DataRowVersion.Default, alarmPolicyManagement.Smsmsg),
            //    new SqlParameter("@AlarmVerify", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmVerify", DataRowVersion.Default, alarmPolicyManagement.Alarmverify),
            //    new SqlParameter("@IsEnable", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsEnable", DataRowVersion.Default, alarmPolicyManagement.Isenable),
            //    new SqlParameter("@EventID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "EventID", DataRowVersion.Default, alarmPolicyManagement.Eventid),
            //    new SqlParameter("@LightID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LightID", DataRowVersion.Default, alarmPolicyManagement.Lightid),
            //    new SqlParameter("@ReleaseLightID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ReleaseLightID", DataRowVersion.Default, alarmPolicyManagement.Releaselightid)
            //};
            //return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_AlarmPolicyManagement
        /// </summary>
        public virtual bool Update(AlarmPolicyManagementOR objPolicy)
        {

            string sql = string.Format("update dbo.t_AlarmPolicyManagement set ValueType={0},MaxValue={1},MinValue={2},SwitchValue={3},MaxTiggerType={4},MinTiggerType={5},  "
           + " AlarmTimes={6} , AlarmfilterTimes={7},EventID='{8}',IsEnable={9},AlarmAudioFile='{10}',DisAlarmAudioFile='{11}',SmsMsg='{12}',LightID={13},ReleaseLightID={14} where   StationID ={15} and DeviceTypeID = {16} and DeviceID ={17} and DeviceChannelID = {18}",
           objPolicy.Valuetype, objPolicy.Maxvalue, objPolicy.Minvalue, objPolicy.Switchvalue, objPolicy.Maxtiggertype, objPolicy.Mintiggertype, objPolicy.Alarmtimes, objPolicy.Alarmfiltertimes, objPolicy.Eventid, objPolicy.Isenable,
           objPolicy.Alarmaudiofile, objPolicy.Disalarmaudiofile, objPolicy.Smsmsg, objPolicy.Lightid, objPolicy.Releaselightid, objPolicy.Stationid, objPolicy.Devicetypeid, objPolicy.Deviceid, objPolicy.Devicechannelid);
            return db.ExecuteNoQuery(sql) > 0;
            //            string sql = @"update t_AlarmPolicyManagement set  ValueType = @ValueType,  MaxTiggerType = @MaxTiggerType,  MaxValue = @MaxValue,  MinTiggerType = @MinTiggerType,  
            //MinValue = @MinValue,  SwitchValue = @SwitchValue,  AlarmLevel = @AlarmLevel,  AlarmTarget = @AlarmTarget,  AlarmWay = @AlarmWay,  
            //IsEnableFrequency = @IsEnableFrequency,  AlarmAudioFile = @AlarmAudioFile,  DisAlarmAudioFile = @DisAlarmAudioFile,  AlarmTimes = @AlarmTimes, 
            //AlarmfilterTimes = @AlarmfilterTimes,  SmsMsg = @SmsMsg,  AlarmVerify = @AlarmVerify,  IsEnable = @IsEnable,  EventID = @EventID,  LightID = @LightID, 
            //ReleaseLightID = @ReleaseLightID where  StationID = @StationID and DeviceTypeID = @DeviceTypeID and DeviceID = @DeviceID and
            //DeviceChannelID = @DeviceChannelID";
            //            SqlParameter[] parameters = new SqlParameter[]
            //            {
            //                new SqlParameter("@AlarmPolicyManagementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmPolicyManagementID", DataRowVersion.Default, alarmPolicyManagement.Alarmpolicymanagementid),
            //                new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, alarmPolicyManagement.Stationid),
            //                new SqlParameter("@DeviceTypeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceTypeID", DataRowVersion.Default, alarmPolicyManagement.Devicetypeid),
            //                new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, alarmPolicyManagement.Deviceid),
            //                new SqlParameter("@DeviceChannelID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceChannelID", DataRowVersion.Default, alarmPolicyManagement.Devicechannelid),
            //                new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, alarmPolicyManagement.Valuetype),
            //                new SqlParameter("@MaxTiggerType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "MaxTiggerType", DataRowVersion.Default, alarmPolicyManagement.Maxtiggertype),
            //                new SqlParameter("@MaxValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MaxValue", DataRowVersion.Default, alarmPolicyManagement.Maxvalue),
            //                new SqlParameter("@MinTiggerType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "MinTiggerType", DataRowVersion.Default, alarmPolicyManagement.Mintiggertype),
            //                new SqlParameter("@MinValue", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MinValue", DataRowVersion.Default, alarmPolicyManagement.Minvalue),
            //                new SqlParameter("@SwitchValue", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "SwitchValue", DataRowVersion.Default, alarmPolicyManagement.Switchvalue),
            //                new SqlParameter("@AlarmLevel", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmLevel", DataRowVersion.Default, alarmPolicyManagement.Alarmlevel),
            //                new SqlParameter("@AlarmTarget", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmTarget", DataRowVersion.Default, alarmPolicyManagement.Alarmtarget),
            //                new SqlParameter("@AlarmWay", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AlarmWay", DataRowVersion.Default, alarmPolicyManagement.Alarmway),
            //                new SqlParameter("@IsEnableFrequency", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsEnableFrequency", DataRowVersion.Default, alarmPolicyManagement.Isenablefrequency),
            //                new SqlParameter("@AlarmAudioFile", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "AlarmAudioFile", DataRowVersion.Default, alarmPolicyManagement.Alarmaudiofile),
            //                new SqlParameter("@DisAlarmAudioFile", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "DisAlarmAudioFile", DataRowVersion.Default, alarmPolicyManagement.Disalarmaudiofile),
            //                new SqlParameter("@AlarmTimes", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmTimes", DataRowVersion.Default, alarmPolicyManagement.Alarmtimes),
            //                new SqlParameter("@AlarmfilterTimes", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmfilterTimes", DataRowVersion.Default, alarmPolicyManagement.Alarmfiltertimes),
            //                new SqlParameter("@SmsMsg", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "SmsMsg", DataRowVersion.Default, alarmPolicyManagement.Smsmsg),
            //                new SqlParameter("@AlarmVerify", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AlarmVerify", DataRowVersion.Default, alarmPolicyManagement.Alarmverify),
            //                new SqlParameter("@IsEnable", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsEnable", DataRowVersion.Default, alarmPolicyManagement.Isenable),
            //                new SqlParameter("@EventID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "EventID", DataRowVersion.Default, alarmPolicyManagement.Eventid),
            //                new SqlParameter("@LightID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LightID", DataRowVersion.Default, alarmPolicyManagement.Lightid),
            //                new SqlParameter("@ReleaseLightID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ReleaseLightID", DataRowVersion.Default, alarmPolicyManagement.Releaselightid)
            //            };
            //return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_AlarmPolicyManagement
        /// </summary>
        public void Delete(string StationID, string DeviceTypeID,
           string DeviceID, string DeviceChannelID)
        {

            string sql = string.Format(@"delete t_AlarmPolicyManagement where  
StationID={0} and DeviceTypeID={1} and DeviceID={2} and DeviceChannelID={3}",
 StationID, DeviceTypeID, DeviceID, DeviceChannelID);
            db.ExecuteNoQuery(sql);
        }
        #endregion
    }
}
