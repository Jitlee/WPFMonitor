using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.Alarm;


namespace WPFMonitor.DAL.Alarm
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyWordDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_KeyWord";
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

        public KeyWordOR selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_KeyWord where  Keywordid='{0}'", m_id);
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
            KeyWordOR m_KeyW = new KeyWordOR(dr);
            return m_KeyW;

        }


        public ObservableCollection<KeyWordOR> selectAllDate()
        {
            string sql = "select * from t_KeyWord";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<KeyWordOR> _List = new ObservableCollection<KeyWordOR>();
            foreach (DataRow dr in dt.Rows)
            {
                KeyWordOR obj = new KeyWordOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        #region 插入
        /// <summary>
        /// 插入t_KeyWord
        /// </summary>
        public virtual bool Insert(KeyWordOR keyWord)
        {
            //string sql = "insert into t_KeyWord (KeyWordID, KeyWord, KeyWordName, Replace, IsCustomize) values (@KeyWordID, @KeyWord, @KeyWordName, @Replace, @IsCustomize)";
            string sql = "insert into t_KeyWord (KeyWord, KeyWordName, Replace, IsCustomize) values (@KeyWord, @KeyWordName, @Replace, @IsCustomize)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@KeyWordID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "KeyWordID", DataRowVersion.Default, keyWord.Keywordid),
				new SqlParameter("@KeyWord", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "KeyWord", DataRowVersion.Default, keyWord.Keyword),
				new SqlParameter("@KeyWordName", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "KeyWordName", DataRowVersion.Default, keyWord.Keywordname),
				new SqlParameter("@Replace", SqlDbType.VarChar, 4000, ParameterDirection.Input, false, 0, 0, "Replace", DataRowVersion.Default, keyWord.Replace),
				new SqlParameter("@IsCustomize", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsCustomize", DataRowVersion.Default, keyWord.Iscustomize)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_KeyWord
        /// </summary>
        public virtual bool Update(KeyWordOR keyWord)
        {
            string sql = "update t_KeyWord set  KeyWord = @KeyWord,  KeyWordName = @KeyWordName,  Replace = @Replace,  IsCustomize = @IsCustomize where  KeyWordID = @KeyWordID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@KeyWordID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "KeyWordID", DataRowVersion.Default, keyWord.Keywordid),
				new SqlParameter("@KeyWord", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "KeyWord", DataRowVersion.Default, keyWord.Keyword),
				new SqlParameter("@KeyWordName", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "KeyWordName", DataRowVersion.Default, keyWord.Keywordname),
				new SqlParameter("@Replace", SqlDbType.VarChar, 4000, ParameterDirection.Input, false, 0, 0, "Replace", DataRowVersion.Default, keyWord.Replace),
				new SqlParameter("@IsCustomize", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "IsCustomize", DataRowVersion.Default, keyWord.Iscustomize)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_KeyWord
        /// </summary>
        public virtual bool Delete(IList mlist)
        {
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (KeyWordOR obj in mlist)
            {
                string sql = string.Format("delete from t_KeyWord where  KeyWordID = '{0}'", obj.Keywordid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
        }
        #endregion
    }
}

