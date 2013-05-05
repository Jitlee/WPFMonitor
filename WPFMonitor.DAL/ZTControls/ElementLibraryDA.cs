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
    public class ElementLibraryDA : DALBase
    {
        
		#region 查询
public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Element_Library";
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

public t_ElementLibrary selectARowDate(string m_id)
{string sql = string.Format("select * from t_Element_Library where  Elementid='{0}'",m_id);
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
t_ElementLibrary m_Elem=new t_ElementLibrary(dr); 
 return m_Elem;

}


        public ObservableCollection<t_ElementLibrary> selectAllDate()
        {
            string sql = "select * from t_Element_Library";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ObservableCollection<t_ElementLibrary> _List = new ObservableCollection<t_ElementLibrary>();
            foreach (DataRow dr in dt.Rows)
            {
                t_ElementLibrary obj = new t_ElementLibrary(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Element_Library
		/// </summary>
		public virtual bool Insert(t_ElementLibrary elementLibrary)
		{
			string sql = "insert into t_Element_Library (ElementID, ElementName, ControlID, ScreenX, ScreenY, TxtInfo, Width, Height, ImageURL, ForeColor, Font, ChildScreenID, DeviceID, ChannelNo, ScreenID, BackColor, Transparent, oldX, oldY, Method, MinFloat, MaxFloat, SerialNum, TotalLength) values (@ElementID, @ElementName, @ControlID, @ScreenX, @ScreenY, @TxtInfo, @Width, @Height, @ImageURL, @ForeColor, @Font, @ChildScreenID, @DeviceID, @ChannelNo, @ScreenID, @BackColor, @Transparent, @oldX, @oldY, @Method, @MinFloat, @MaxFloat, @SerialNum, @TotalLength)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementLibrary.Elementid),
				new SqlParameter("@ElementName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ElementName", DataRowVersion.Default, elementLibrary.Elementname),
				new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, elementLibrary.Controlid),
				new SqlParameter("@ScreenX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenX", DataRowVersion.Default, elementLibrary.Screenx),
				new SqlParameter("@ScreenY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenY", DataRowVersion.Default, elementLibrary.Screeny),
				new SqlParameter("@TxtInfo", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "TxtInfo", DataRowVersion.Default, elementLibrary.Txtinfo),
				new SqlParameter("@Width", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Default, elementLibrary.Width),
				new SqlParameter("@Height", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Default, elementLibrary.Height),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, elementLibrary.Imageurl),
				new SqlParameter("@ForeColor", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ForeColor", DataRowVersion.Default, elementLibrary.Forecolor),
				new SqlParameter("@Font", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Font", DataRowVersion.Default, elementLibrary.Font),
				new SqlParameter("@ChildScreenID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ChildScreenID", DataRowVersion.Default, elementLibrary.Childscreenid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, elementLibrary.Deviceid),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, elementLibrary.Channelno),
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, elementLibrary.Screenid),
				new SqlParameter("@BackColor", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "BackColor", DataRowVersion.Default, elementLibrary.Backcolor),
				new SqlParameter("@Transparent", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Transparent", DataRowVersion.Default, elementLibrary.Transparent),
				new SqlParameter("@oldX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldX", DataRowVersion.Default, elementLibrary.Oldx),
				new SqlParameter("@oldY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldY", DataRowVersion.Default, elementLibrary.Oldy),
				new SqlParameter("@Method", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Method", DataRowVersion.Default, elementLibrary.Method),
				new SqlParameter("@MinFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MinFloat", DataRowVersion.Default, elementLibrary.Minfloat),
				new SqlParameter("@MaxFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MaxFloat", DataRowVersion.Default, elementLibrary.Maxfloat),
				new SqlParameter("@SerialNum", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "SerialNum", DataRowVersion.Default, elementLibrary.Serialnum),
				new SqlParameter("@TotalLength", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "TotalLength", DataRowVersion.Default, elementLibrary.Totallength)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Element_Library
		/// </summary>
		public virtual bool Update(t_ElementLibrary elementLibrary)
		{
			string sql = "update t_Element_Library set  ElementName = @ElementName,  ControlID = @ControlID,  ScreenX = @ScreenX,  ScreenY = @ScreenY,  TxtInfo = @TxtInfo,  Width = @Width,  Height = @Height,  ImageURL = @ImageURL,  ForeColor = @ForeColor,  Font = @Font,  ChildScreenID = @ChildScreenID,  DeviceID = @DeviceID,  ChannelNo = @ChannelNo,  ScreenID = @ScreenID,  BackColor = @BackColor,  Transparent = @Transparent,  oldX = @oldX,  oldY = @oldY,  Method = @Method,  MinFloat = @MinFloat,  MaxFloat = @MaxFloat,  SerialNum = @SerialNum,  TotalLength = @TotalLength where  ElementID = @ElementID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementLibrary.Elementid),
				new SqlParameter("@ElementName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ElementName", DataRowVersion.Default, elementLibrary.Elementname),
				new SqlParameter("@ControlID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ControlID", DataRowVersion.Default, elementLibrary.Controlid),
				new SqlParameter("@ScreenX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenX", DataRowVersion.Default, elementLibrary.Screenx),
				new SqlParameter("@ScreenY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenY", DataRowVersion.Default, elementLibrary.Screeny),
				new SqlParameter("@TxtInfo", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "TxtInfo", DataRowVersion.Default, elementLibrary.Txtinfo),
				new SqlParameter("@Width", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Width", DataRowVersion.Default, elementLibrary.Width),
				new SqlParameter("@Height", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Height", DataRowVersion.Default, elementLibrary.Height),
				new SqlParameter("@ImageURL", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ImageURL", DataRowVersion.Default, elementLibrary.Imageurl),
				new SqlParameter("@ForeColor", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "ForeColor", DataRowVersion.Default, elementLibrary.Forecolor),
				new SqlParameter("@Font", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Font", DataRowVersion.Default, elementLibrary.Font),
				new SqlParameter("@ChildScreenID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ChildScreenID", DataRowVersion.Default, elementLibrary.Childscreenid),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, elementLibrary.Deviceid),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, elementLibrary.Channelno),
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, elementLibrary.Screenid),
				new SqlParameter("@BackColor", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "BackColor", DataRowVersion.Default, elementLibrary.Backcolor),
				new SqlParameter("@Transparent", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Transparent", DataRowVersion.Default, elementLibrary.Transparent),
				new SqlParameter("@oldX", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldX", DataRowVersion.Default, elementLibrary.Oldx),
				new SqlParameter("@oldY", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "oldY", DataRowVersion.Default, elementLibrary.Oldy),
				new SqlParameter("@Method", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Method", DataRowVersion.Default, elementLibrary.Method),
				new SqlParameter("@MinFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MinFloat", DataRowVersion.Default, elementLibrary.Minfloat),
				new SqlParameter("@MaxFloat", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "MaxFloat", DataRowVersion.Default, elementLibrary.Maxfloat),
				new SqlParameter("@SerialNum", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "SerialNum", DataRowVersion.Default, elementLibrary.Serialnum),
				new SqlParameter("@TotalLength", SqlDbType.Float, 8, ParameterDirection.Input, false, 0, 0, "TotalLength", DataRowVersion.Default, elementLibrary.Totallength)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Element_Library
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_ElementLibrary obj in mlist)
            {
                string sql = string.Format(" delete from t_Element_Library where  Elementid = '{0}'", obj.Elementid);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}
		#endregion
    }
}

