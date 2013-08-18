using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DBUtility;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ElementDA : DALBase
    {

        #region 查询

        public t_Element GetBy(int elementID)
        {
            var dt = db.ExecuteQuery("select * from t_Element where ElementID = @ElementID",
                new[]
                {
                    new SqlParameter("@ElementID", elementID),
                });
            if (dt.Rows.Count > 0)
            {
                return new t_Element(dt.Rows[0]);
            }
            return null;
        }

        public t_Element GetBy(int controlID, int parentID, string elementType)
        {
            var dt = db.ExecuteQuery("select * from t_Element where ControlID = @ControlID AND ParentID = @ParentID AND ElementType = @ElementType",
                new[]
                {
                    new SqlParameter("@ControlID", controlID),
                    new SqlParameter("@ParentID", parentID),
                    new SqlParameter("@ElementType", elementType),
                });
            if (dt.Rows.Count > 0)
            {
                return new t_Element(dt.Rows[0]);
            }
            return null;
        }

        public List<t_Element> SelectBy(int controlID, int parentId, string elementType)
        {
            return db.ExecuteQuery("select * from t_Element where ControlID <> @ControlID AND ParentID = @ParentID AND ElementType = @ElementType",
                new[]
                {
                    new SqlParameter("@ControlID", controlID),
                    new SqlParameter("@ParentID", parentId),
                    new SqlParameter("@ElementType", elementType),
                })
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_Element(r))
                .ToList();
        }

        public List<t_Element> SelectBy(int parentId, string elementType)
        {
            return db.ExecuteQuery("select * from t_Element where ParentID = @ParentID AND ElementType = @ElementType",
                new[]
                {
                    new SqlParameter("@ParentID", parentId),
                    new SqlParameter("@ElementType", elementType),
                })
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_Element(r))
                .ToList();
        }

        public List<t_Element> SelectBy(int screenId)
        {
            return db.ExecuteQuery("select * from t_Element where ScreenID = @ScreenID",
                new[]
                {
                    new SqlParameter("@ScreenID", screenId),
                })
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_Element(r))
                .ToList();
        }

        public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
        {
            string sql = "select * from t_Element";
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

        public t_Element selectARowDate(string m_id)
        {
            string sql = string.Format("select * from t_Element where  Elementid='{0}'", m_id);
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
            t_Element m_Elem = new t_Element(dr);
            return m_Elem;

        }


        public ObservableCollection<t_Element> selectAllDate()
        {
            string sql = "select * from t_Element";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<t_Element> _List = new ObservableCollection<t_Element>();
            foreach (DataRow dr in dt.Rows)
            {
                t_Element obj = new t_Element(dr);
                _List.Add(obj);
            }
            return _List;
        }


        #endregion

        //#region 插入
        ///// <summary>
        ///// 插入t_Element
        ///// </summary>
        //public virtual bool Insert(t_Element element)
        //{
        //    string sql = "insert into t_Element (ElementID, ElementName, ControlID, ScreenX, ScreenY, TxtInfo, Width, Height, ImageURL, ForeColor, Font, ChildScreenID, DeviceID, ChannelNo, ScreenID, BackColor, Transparent, oldX, oldY, Method, MinFloat, MaxFloat, SerialNum, TotalLength, LevelNo, ComputeStr, ElementType, ParentID) values (@ElementID, @ElementName, @ControlID, @ScreenX, @ScreenY, @TxtInfo, @Width, @Height, @ImageURL, @ForeColor, @Font, @ChildScreenID, @DeviceID, @ChannelNo, @ScreenID, @BackColor, @Transparent, @oldX, @oldY, @Method, @MinFloat, @MaxFloat, @SerialNum, @TotalLength, @LevelNo, @ComputeStr, @ElementType, @ParentID)";
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, element.ElementID),
        //        new SqlParameter("@ElementName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ElementName", DataRowVersion.Default, element.ElementName),
        //        new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, element.ControlID),
        //        new SqlParameter("@ScreenX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenX", DataRowVersion.Default, element.ScreenX),
        //        new SqlParameter("@ScreenY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenY", DataRowVersion.Default, element.ScreenY),
        //        new SqlParameter("@TxtInfo", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "TxtInfo", DataRowVersion.Default, element.TxtInfo),
        //        new SqlParameter("@Width", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Default, element.Width),
        //        new SqlParameter("@Height", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Default, element.Height),
        //        new SqlParameter("@ImageURL", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, element.ImageURL),
        //        new SqlParameter("@ForeColor", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ForeColor", DataRowVersion.Default, element.ForeColor),
        //        new SqlParameter("@Font", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Font", DataRowVersion.Default, element.Font),
        //        new SqlParameter("@ChildScreenID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ChildScreenID", DataRowVersion.Default, element.ChildScreenID),
        //        new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, element.DeviceID),
        //        new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, element.ChannelNo),
        //        new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, element.ScreenID),
        //        new SqlParameter("@BackColor", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "BackColor", DataRowVersion.Default, element.BackColor),
        //        new SqlParameter("@Transparent", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Transparent", DataRowVersion.Default, element.Transparent),
        //        new SqlParameter("@oldX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldX", DataRowVersion.Default, element.oldX),
        //        new SqlParameter("@oldY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldY", DataRowVersion.Default, element.oldY),
        //        new SqlParameter("@Method", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Method", DataRowVersion.Default, element.Method),
        //        new SqlParameter("@MinFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MinFloat", DataRowVersion.Default, element.MinFloat),
        //        new SqlParameter("@MaxFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MaxFloat", DataRowVersion.Default, element.MaxFloat),
        //        new SqlParameter("@SerialNum", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "SerialNum", DataRowVersion.Default, element.SerialNum),
        //        new SqlParameter("@TotalLength", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "TotalLength", DataRowVersion.Default, element.TotalLength),
        //        new SqlParameter("@LevelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LevelNo", DataRowVersion.Default, element.LevelNo),
        //        new SqlParameter("@ComputeStr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ComputeStr", DataRowVersion.Default, element.ComputeStr),
        //        new SqlParameter("@ElementType", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "ElementType", DataRowVersion.Default, element.ElementType),
        //        new SqlParameter("@ParentID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ParentID", DataRowVersion.Default, element.ParentID)
        //    };
        //    return db.ExecuteNoQuery(sql, parameters) > -1;
        //}
        //#endregion

        //#region 修改
        ///// <summary>
        ///// 更新t_Element
        ///// </summary>
        //public virtual bool Update(t_Element element)
        //{
        //    string sql = "update t_Element set  ElementName = @ElementName,  ControlID = @ControlID,  ScreenX = @ScreenX,  ScreenY = @ScreenY,  TxtInfo = @TxtInfo,  Width = @Width,  Height = @Height,  ImageURL = @ImageURL,  ForeColor = @ForeColor,  Font = @Font,  ChildScreenID = @ChildScreenID,  DeviceID = @DeviceID,  ChannelNo = @ChannelNo,  ScreenID = @ScreenID,  BackColor = @BackColor,  Transparent = @Transparent,  oldX = @oldX,  oldY = @oldY,  Method = @Method,  MinFloat = @MinFloat,  MaxFloat = @MaxFloat,  SerialNum = @SerialNum,  TotalLength = @TotalLength,  LevelNo = @LevelNo,  ComputeStr = @ComputeStr,  ElementType = @ElementType,  ParentID = @ParentID where  ElementID = @ElementID";
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, element.ElementID),
        //        new SqlParameter("@ElementName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ElementName", DataRowVersion.Default, element.ElementName),
        //        new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, element.ControlID),
        //        new SqlParameter("@ScreenX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenX", DataRowVersion.Default, element.ScreenX),
        //        new SqlParameter("@ScreenY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenY", DataRowVersion.Default, element.ScreenY),
        //        new SqlParameter("@TxtInfo", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "TxtInfo", DataRowVersion.Default, element.TxtInfo),
        //        new SqlParameter("@Width", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Default, element.Width),
        //        new SqlParameter("@Height", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Default, element.Height),
        //        new SqlParameter("@ImageURL", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, element.ImageURL),
        //        new SqlParameter("@ForeColor", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ForeColor", DataRowVersion.Default, element.ForeColor),
        //        new SqlParameter("@Font", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Font", DataRowVersion.Default, element.Font),
        //        new SqlParameter("@ChildScreenID", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ChildScreenID", DataRowVersion.Default, element.ChildScreenID),
        //        new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, element.DeviceID),
        //        new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, element.ChannelNo),
        //        new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, element.ScreenID),
        //        new SqlParameter("@BackColor", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "BackColor", DataRowVersion.Default, element.BackColor),
        //        new SqlParameter("@Transparent", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Transparent", DataRowVersion.Default, element.Transparent),
        //        new SqlParameter("@oldX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldX", DataRowVersion.Default, element.oldX),
        //        new SqlParameter("@oldY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldY", DataRowVersion.Default, element.oldY),
        //        new SqlParameter("@Method", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Method", DataRowVersion.Default, element.Method),
        //        new SqlParameter("@MinFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MinFloat", DataRowVersion.Default, element.MinFloat),
        //        new SqlParameter("@MaxFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MaxFloat", DataRowVersion.Default, element.MaxFloat),
        //        new SqlParameter("@SerialNum", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "SerialNum", DataRowVersion.Default, element.SerialNum),
        //        new SqlParameter("@TotalLength", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "TotalLength", DataRowVersion.Default, element.TotalLength),
        //        new SqlParameter("@LevelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LevelNo", DataRowVersion.Default, element.LevelNo),
        //        new SqlParameter("@ComputeStr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ComputeStr", DataRowVersion.Default, element.ComputeStr),
        //        new SqlParameter("@ElementType", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "ElementType", DataRowVersion.Default, element.ElementType),
        //        new SqlParameter("@ParentID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ParentID", DataRowVersion.Default, element.ParentID)
        //    };
        //    return db.ExecuteNoQuery(sql, parameters) > -1;
        //}
        //#endregion

        //#region DELETE
        ///// <summary>
        ///// 删除t_Element
        ///// </summary>
        //public virtual bool Delete(IList mlist)
        //{
        //    if (mlist == null)
        //        return false;
        //    List<CommandList> listcmd = new List<CommandList>();
        //    foreach (t_Element obj in mlist)
        //    {
        //        string sql = string.Format(" delete from t_Element where  Elementid = '{0}'", obj.ElementID);
        //        listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
        //    }
        //    return db.ExecuteNoQueryTranPro(listcmd);
        //}
        //public virtual bool Delete(int elementID)
        //{
        //    string sql = string.Format(" delete from t_Element where  Elementid = '{0}'", elementID);
        //    return db.ExecuteNoQuery(sql) > 0;
        //#endregion
        //}
    }
}

