using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DBUtility;
using WPFMonitor.Model.Sys;


namespace WPFMonitor.DAL.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_User";
if (!string.IsNullOrEmpty(where)){
sql = string.Format(" {0} where {1}", sql, where);
}
 DataTable dt = null;
int returnC = 0;try
 {
     dt = db.ExecuteQuery(sql,pageCrrent, pageSize,  out returnC);
  }
  catch (Exception ex)
  {
    throw ex;
  }
 pageCount = returnC;
  return dt;
}

public UserOR selectARowDate(string m_id)
{string sql = string.Format("select * from t_User where  Userid='{0}'",m_id);
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
UserOR m_User=new UserOR(dr); 
 return m_User;

}


        public ObservableCollection<UserOR> selectAllDate()
        {
            string sql = "select * from t_User";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<UserOR> _List = new ObservableCollection<UserOR>();
            foreach (DataRow dr in dt.Rows)
            {
                UserOR obj = new UserOR(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_User
		/// </summary>
		public virtual bool Insert(UserOR user)
		{
			string sql = "insert into t_User (UserPSW, UserType, UserName) values ( @UserPSW, @UserType, @UserName)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				//new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Default, user.Userid),
				new SqlParameter("@UserPSW", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserPSW", DataRowVersion.Default, user.Userpsw),
				new SqlParameter("@UserType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "UserType", DataRowVersion.Default, user.Usertype),
				new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserName", DataRowVersion.Default, user.Username)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_User
		/// </summary>
		public virtual bool Update(UserOR user)
		{
			string sql = "update t_User set  UserPSW = @UserPSW,  UserType = @UserType,  UserName = @UserName where  UserID = @UserID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Default, user.Userid),
				new SqlParameter("@UserPSW", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserPSW", DataRowVersion.Default, user.Userpsw),
				new SqlParameter("@UserType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "UserType", DataRowVersion.Default, user.Usertype),
				new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "UserName", DataRowVersion.Default, user.Username)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_User
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (UserOR obj in mlist)
            {
                string sql = string.Format("delete from t_User where  UserID = '{0}'", obj.Userid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

