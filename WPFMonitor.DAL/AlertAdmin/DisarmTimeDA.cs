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
    public class DisarmTimeDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_DisarmTime";
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

        public DisarmTimeOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_DisarmTime where  Disarmid='{0}'", m_id);
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
            DisarmTimeOR m_Disa = new DisarmTimeOR(dr);
            return m_Disa;

        }


        public ObservableCollection<DisarmTimeOR> selectAllDate()
        {
            string sql = "select * from t_DisarmTime";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<DisarmTimeOR> _List = new ObservableCollection<DisarmTimeOR>();
            foreach (DataRow dr in dt.Rows)
            {
                DisarmTimeOR obj = new DisarmTimeOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_DisarmTime
        /// </summary>
        public virtual bool Insert(DisarmTimeOR disarmTime)
        {
            string sql = "insert into t_DisarmTime (DisarmName, DisarmStartTime, DisarmEndTime) values(@DisarmName, @DisarmStartTime, @DisarmEndTime)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@DisarmID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DisarmID", DataRowVersion.Default, disarmTime.Disarmid),
				new SqlParameter("@DisarmName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DisarmName", DataRowVersion.Default, disarmTime.Disarmname),
				new SqlParameter("@DisarmStartTime", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DisarmStartTime", DataRowVersion.Default, disarmTime.Disarmstarttime),
				new SqlParameter("@DisarmEndTime", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DisarmEndTime", DataRowVersion.Default, disarmTime.Disarmendtime)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_DisarmTime
        /// </summary>
        public virtual bool Update(DisarmTimeOR disarmTime)
        {
            string sql = "update t_DisarmTime set  DisarmName = @DisarmName,  DisarmStartTime = @DisarmStartTime,  DisarmEndTime = @DisarmEndTime where  DisarmID = @DisarmID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@DisarmID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DisarmID", DataRowVersion.Default, disarmTime.Disarmid),
				new SqlParameter("@DisarmName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DisarmName", DataRowVersion.Default, disarmTime.Disarmname),
				new SqlParameter("@DisarmStartTime", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DisarmStartTime", DataRowVersion.Default, disarmTime.Disarmstarttime),
				new SqlParameter("@DisarmEndTime", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DisarmEndTime", DataRowVersion.Default, disarmTime.Disarmendtime)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_DisarmTime
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (DisarmTimeOR obj in mlist)
            {
                string sql = string.Format(" delete from t_DisarmTime where  Disarmid = '{0}'", obj.Disarmid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

