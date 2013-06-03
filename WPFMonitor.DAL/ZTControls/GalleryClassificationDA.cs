using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class GalleryClassificationDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_GalleryClassification";
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

public t_GalleryClassification selectARowDate(string m_id)
{string sql = string.Format("select * from t_GalleryClassification where  Id='{0}'",m_id);
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
t_GalleryClassification m_Gall=new t_GalleryClassification(dr); 
 return m_Gall;

}


        public List<t_GalleryClassification> selectAllDate()
        {
            string sql = "select * from t_GalleryClassification";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<t_GalleryClassification> _List = new List<t_GalleryClassification>();
            foreach (DataRow dr in dt.Rows)
            {
                t_GalleryClassification obj = new t_GalleryClassification(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_GalleryClassification
		/// </summary>
		public virtual bool Insert(t_GalleryClassification galleryClassification)
		{
			string sql = "insert into t_GalleryClassification (Id, Name, Description, Sort) values (@Id, @Name, @Description, @Sort)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Id", DataRowVersion.Default, galleryClassification.Id),
				new SqlParameter("@Name", SqlDbType.NVarChar, 32, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Default, galleryClassification.Name),
				new SqlParameter("@Description", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Default, galleryClassification.Description),
				new SqlParameter("@Sort", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Sort", DataRowVersion.Default, galleryClassification.Sort)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_GalleryClassification
		/// </summary>
		public virtual bool Update(t_GalleryClassification galleryClassification)
		{
			string sql = "update t_GalleryClassification set  Name = @Name,  Description = @Description,  Sort = @Sort where  Id = @Id";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Id", DataRowVersion.Default, galleryClassification.Id),
				new SqlParameter("@Name", SqlDbType.NVarChar, 32, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Default, galleryClassification.Name),
				new SqlParameter("@Description", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Default, galleryClassification.Description),
				new SqlParameter("@Sort", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Sort", DataRowVersion.Default, galleryClassification.Sort)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_GalleryClassification
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_GalleryClassification obj in mlist)
            {
                string sql = string.Format(" delete from t_GalleryClassification where  Id = '{0}'", obj.Id);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

