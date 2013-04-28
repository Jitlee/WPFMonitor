using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WPFMonitor.Model.Sys;
using System.Data;
using WPFMonitor.DAL;

namespace WPFMonitor.DAL.Sys
{
    public class SysParaDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_SysPara";
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

        public SysParaOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_SysPara where  Keystr='{0}'", m_id);
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
            SysParaOR m_SysP = new SysParaOR(dr);
            return m_SysP;

        }

        #endregion

        #region 插入
        /// <summary>
        /// 插入t_SysPara
        /// </summary>
        public virtual bool Insert(SysParaOR sysPara)
        {
            string sql = "insert into t_SysPara (KeyStr, ValStr) values (@KeyStr, @ValStr)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@KeyStr", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "KeyStr", DataRowVersion.Default, sysPara.Keystr),
				new SqlParameter("@ValStr", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ValStr", DataRowVersion.Default, sysPara.Valstr)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_SysPara
        /// </summary>
        public virtual bool Update(SysParaOR sysPara)
        {
            string sql = "update t_SysPara set  ValStr = @ValStr where  KeyStr = @KeyStr";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@KeyStr", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "KeyStr", DataRowVersion.Default, sysPara.Keystr),
				new SqlParameter("@ValStr", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ValStr", DataRowVersion.Default, sysPara.Valstr)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_SysPara
        /// </summary>
        public virtual bool Delete(string strKeystr)
        {
            string sql = "delete from t_SysPara where  KeyStr = @KeyStr";
            SqlParameter parameter = new SqlParameter("@KeyStr", strKeystr);
            return db.ExecuteNoQuery(sql, parameter) > -1;
        }
        #endregion
    }
}
