using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.AlertAdmin;


namespace WPFMonitor.DAL.AlertAdmin
{
    /// <summary>
    /// 
    /// </summary>
    public class MainteDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_Mainte";
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

        public MainteOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_Mainte where  Id='{0}'", m_id);
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
            MainteOR m_Main = new MainteOR(dr);
            return m_Main;

        }


        public ObservableCollection<MainteOR> selectAllDate()
        {
            string sql = @"select m.*,t.StationName,d.DeviceName from t_Mainte m
inner join t_station t on  t.stationid= m.stationid
inner join t_Device d on m.DeviceID=d.DeviceID order by id";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<MainteOR> _List = new ObservableCollection<MainteOR>();
            foreach (DataRow dr in dt.Rows)
            {
                MainteOR obj = new MainteOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_Mainte
        /// </summary>
        public virtual bool Insert(MainteOR mainte)
        {
            string sql = @"insert into t_Mainte (DeviceID, StationID, ValueType, MainteTime, MainteName, Duty, ContentMainte) 
values (@DeviceID, @StationID, @ValueType, @MainteTime, @MainteName, @Duty, @ContentMainte)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, mainte.Id),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, mainte.Deviceid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, mainte.Stationid),
				new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, mainte.Valuetype),
				new SqlParameter("@MainteTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "MainteTime", DataRowVersion.Default, mainte.Maintetime),
				new SqlParameter("@MainteName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "MainteName", DataRowVersion.Default, mainte.Maintename),
				new SqlParameter("@Duty", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Duty", DataRowVersion.Default, mainte.Duty),
				new SqlParameter("@ContentMainte", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ContentMainte", DataRowVersion.Default, mainte.Contentmainte)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_Mainte
        /// </summary>
        public virtual bool Update(MainteOR mainte)
        {
            string sql = "update t_Mainte set  DeviceID = @DeviceID,  StationID = @StationID,  ValueType = @ValueType,  MainteTime = @MainteTime,  MainteName = @MainteName,  Duty = @Duty,  ContentMainte = @ContentMainte where  ID = @ID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, mainte.Id),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, mainte.Deviceid),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, mainte.Stationid),
				new SqlParameter("@ValueType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueType", DataRowVersion.Default, mainte.Valuetype),
				new SqlParameter("@MainteTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "MainteTime", DataRowVersion.Default, mainte.Maintetime),
				new SqlParameter("@MainteName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "MainteName", DataRowVersion.Default, mainte.Maintename),
				new SqlParameter("@Duty", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Duty", DataRowVersion.Default, mainte.Duty),
				new SqlParameter("@ContentMainte", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ContentMainte", DataRowVersion.Default, mainte.Contentmainte)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_Mainte
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (MainteOR obj in mlist)
            {
                string sql = string.Format(" delete from t_Mainte where  Id = '{0}'", obj.Id);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

