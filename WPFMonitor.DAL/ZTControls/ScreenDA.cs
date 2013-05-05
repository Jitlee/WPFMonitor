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
    public class ScreenDA : DALBase
    {

        #region 查询
        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_Screen";
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

        public t_Screen selectARowDate(int m_id)
        {
            string sql = string.Format("select * from t_Screen where  Screenid={0}", m_id);
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
            t_Screen m_Scre = CreateScreen(dr);
            return m_Scre;
        }


        public List<t_Screen> selectAllDate()
        {
            string sql = "select * from t_Screen";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<t_Screen> _List = new List<t_Screen>();
            foreach (DataRow dr in dt.Rows)
            {
                t_Screen obj = CreateScreen(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion
        public int GetMaxID()
        {
            string sql = "select max(ScreenID) from t_Screen";
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }
        #region 插入
        /// <summary>
        /// 插入t_Screen
        /// </summary>
        public virtual bool Insert(t_Screen screen)
        {
            string sql = @"insert into t_Screen (ScreenName, ImageURL, ParentScreenID, StationID, Flag, Width, Height, BackColor, AutoSize)
values ( @ScreenName, @ImageURL, @ParentScreenID, @StationID, @Flag, @Width, @Height, @BackColor, @AutoSize)";
            SqlParameter[] parameters = new SqlParameter[]
			{
				//new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, screen.ScreenID),
				new SqlParameter("@ScreenName", SqlDbType.VarChar, 64, ParameterDirection.Input, false, 0, 0, "ScreenName", DataRowVersion.Default, screen.ScreenName),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 256, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, screen.ImageURL),
				new SqlParameter("@ParentScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ParentScreenID", DataRowVersion.Default, screen.ParentScreenID),
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, screen.StationID),
				new SqlParameter("@Flag", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Flag", DataRowVersion.Default, screen.Flag),
				new SqlParameter("@Width", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Default, screen.Width),
				new SqlParameter("@Height", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Default, screen.Height),
				new SqlParameter("@BackColor", SqlDbType.VarChar, 40, ParameterDirection.Input, false, 0, 0, "BackColor", DataRowVersion.Default, screen.BackColor),
				new SqlParameter("@AutoSize", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AutoSize", DataRowVersion.Default, screen.AutoSize)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 更新t_Screen
        /// </summary>
        public virtual bool Update(t_Screen screen)
        {
            string sql = "update t_Screen set  ScreenName = @ScreenName,  ImageURL = @ImageURL,    StationID = @StationID,  Flag = @Flag,  Width = @Width,  Height = @Height,  BackColor = @BackColor,  AutoSize = @AutoSize where  ScreenID = @ScreenID";
            SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, screen.ScreenID),
				new SqlParameter("@ScreenName", SqlDbType.VarChar, 64, ParameterDirection.Input, false, 0, 0, "ScreenName", DataRowVersion.Default, screen.ScreenName),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 256, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, screen.ImageURL),
				
				new SqlParameter("@StationID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "StationID", DataRowVersion.Default, screen.StationID),
				new SqlParameter("@Flag", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Flag", DataRowVersion.Default, screen.Flag),
				new SqlParameter("@Width", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Default, screen.Width),
				new SqlParameter("@Height", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Default, screen.Height),
				new SqlParameter("@BackColor", SqlDbType.VarChar, 40, ParameterDirection.Input, false, 0, 0, "BackColor", DataRowVersion.Default, screen.BackColor),
				new SqlParameter("@AutoSize", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "AutoSize", DataRowVersion.Default, screen.AutoSize)
			};
            return db.ExecuteNoQuery(sql, parameters) > -1;
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除t_Screen
        /// </summary>
        public virtual bool Delete(int ID)
        {
            
            string sql = string.Format(" delete from t_Screen where  Screenid = {0}",ID);                
            
            return db.ExecuteNoQuery(sql)==1;
        }
        #endregion

        private t_Screen CreateScreen(DataRow row)
        {
            return new t_Screen()
            {
                // 
                ScreenID = Converter.ToInt32(row["ScreenID"]),
                // 
                ScreenName = row["ScreenName"].ToString().Trim(),
                // 
                ImageURL = row["ImageURL"].ToString().Trim(),
                // 
                ParentScreenID = Converter.ToInt32(row["ParentScreenID"]),
                // 
                StationID = Converter.ToInt32(row["StationID"]),
                // 
                Flag = Converter.ToInt32(row["Flag"]),
                // 
                Width = Converter.ToInt32(row["Width"]),
                // 
                Height = Converter.ToInt32(row["Height"]),
                // 
                BackColor = row["BackColor"].ToString().Trim(),
                // 
                AutoSize = Converter.ToBoolean(row["AutoSize"]),
            };
        }

        public void CopyScreen(int newScreenID,int oldScreenID)
        {
            string procedureName = "P_CopyScreen";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@newScreenID", newScreenID)
                ,new SqlParameter("@oldScreenID", oldScreenID)
            };
            db.ExecuteNoQueryProc(procedureName, parameters);
        }
    }
}

