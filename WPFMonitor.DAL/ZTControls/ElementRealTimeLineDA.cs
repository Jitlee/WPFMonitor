using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using DBUtility;
using WPFMonitor.Model.ZTControls;
using System.Linq;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ElementRealTimeLineDA : DALBase
    {
        
		#region 查询

        public List<t_Element_RealTimeLine> SelectBy(int elementId)
        {
            return db.ExecuteQuery("select * from t_Element_RealTimeLine where ElementID = @ElementID",
                new[]
                {
                    new SqlParameter("@ElementID", elementId),
                })
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_Element_RealTimeLine(r))
                .ToList();
        }

public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
{
string sql = "select * from t_Element_RealTimeLine";
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

public t_Element_RealTimeLine selectARowDate(string m_id)
{string sql = string.Format("select * from t_Element_RealTimeLine where  Id='{0}'",m_id);
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
t_Element_RealTimeLine m_Elem=new t_Element_RealTimeLine(dr); 
 return m_Elem;

}


        public List<t_Element_RealTimeLine> selectAllDate()
        {
            string sql = "select * from t_Element_RealTimeLine";
           
            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<t_Element_RealTimeLine> _List = new List<t_Element_RealTimeLine>();
            foreach (DataRow dr in dt.Rows)
            {
                t_Element_RealTimeLine obj = new t_Element_RealTimeLine(dr);
                _List.Add(obj);
            }
            return _List;
        }


		#endregion

		#region 插入
		/// <summary>
		/// 插入t_Element_RealTimeLine
		/// </summary>
		public virtual bool Insert(t_Element_RealTimeLine elementRealTimeLine)
		{
			string sql = "insert into t_Element_RealTimeLine (ID, ScreenID, ElementID, LineType, LineName, LineCZ, LineShowType, LineStyle, LinePointBJ, LineColor, MinValue, MaxValue, ValueDecimal, ShowFormat, TimeLen, TimeLenType, LineCYZQLent, LineCYZQType, DeviceID, ChannelNo, ComputeStr, StartTime) values (@ID, @ScreenID, @ElementID, @LineType, @LineName, @LineCZ, @LineShowType, @LineStyle, @LinePointBJ, @LineColor, @MinValue, @MaxValue, @ValueDecimal, @ShowFormat, @TimeLen, @TimeLenType, @LineCYZQLent, @LineCYZQType, @DeviceID, @ChannelNo, @ComputeStr, @StartTime)";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Char, 36, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, elementRealTimeLine.ID),
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, elementRealTimeLine.ScreenID),
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementRealTimeLine.ElementID),
				new SqlParameter("@LineType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineType", DataRowVersion.Default, elementRealTimeLine.LineType),
				new SqlParameter("@LineName", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "LineName", DataRowVersion.Default, elementRealTimeLine.LineName),
				new SqlParameter("@LineCZ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineCZ", DataRowVersion.Default, elementRealTimeLine.LineCZ),
				new SqlParameter("@LineShowType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineShowType", DataRowVersion.Default, elementRealTimeLine.LineShowType),
				new SqlParameter("@LineStyle", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineStyle", DataRowVersion.Default, elementRealTimeLine.LineStyle),
				new SqlParameter("@LinePointBJ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinePointBJ", DataRowVersion.Default, elementRealTimeLine.LinePointBJ),
				new SqlParameter("@LineColor", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "LineColor", DataRowVersion.Default, elementRealTimeLine.LineColor),
				new SqlParameter("@MinValue", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "MinValue", DataRowVersion.Default, elementRealTimeLine.MinValue),
				new SqlParameter("@MaxValue", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "MaxValue", DataRowVersion.Default, elementRealTimeLine.MaxValue),
				new SqlParameter("@ValueDecimal", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueDecimal", DataRowVersion.Default, elementRealTimeLine.ValueDecimal),
				new SqlParameter("@ShowFormat", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "ShowFormat", DataRowVersion.Default, elementRealTimeLine.ShowFormat),
				new SqlParameter("@TimeLen", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "TimeLen", DataRowVersion.Default, elementRealTimeLine.TimeLen),
				new SqlParameter("@TimeLenType", SqlDbType.VarChar, 2, ParameterDirection.Input, false, 0, 0, "TimeLenType", DataRowVersion.Default, elementRealTimeLine.TimeLenType),
				new SqlParameter("@LineCYZQLent", SqlDbType.VarChar, 2, ParameterDirection.Input, false, 0, 0, "LineCYZQLent", DataRowVersion.Default, elementRealTimeLine.LineCYZQLent),
				new SqlParameter("@LineCYZQType", SqlDbType.VarChar, 2, ParameterDirection.Input, false, 0, 0, "LineCYZQType", DataRowVersion.Default, elementRealTimeLine.LineCYZQType),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, elementRealTimeLine.DeviceID),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, elementRealTimeLine.ChannelNo),
				new SqlParameter("@ComputeStr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ComputeStr", DataRowVersion.Default, elementRealTimeLine.ComputeStr),
				new SqlParameter("@StartTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "StartTime", DataRowVersion.Default, elementRealTimeLine.StartTime)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region 修改
		/// <summary>
		/// 更新t_Element_RealTimeLine
		/// </summary>
		public virtual bool Update(t_Element_RealTimeLine elementRealTimeLine)
		{
			string sql = "update t_Element_RealTimeLine set  ScreenID = @ScreenID,  ElementID = @ElementID,  LineType = @LineType,  LineName = @LineName,  LineCZ = @LineCZ,  LineShowType = @LineShowType,  LineStyle = @LineStyle,  LinePointBJ = @LinePointBJ,  LineColor = @LineColor,  MinValue = @MinValue,  MaxValue = @MaxValue,  ValueDecimal = @ValueDecimal,  ShowFormat = @ShowFormat,  TimeLen = @TimeLen,  TimeLenType = @TimeLenType,  LineCYZQLent = @LineCYZQLent,  LineCYZQType = @LineCYZQType,  DeviceID = @DeviceID,  ChannelNo = @ChannelNo,  ComputeStr = @ComputeStr,  StartTime = @StartTime where  ID = @ID";
			SqlParameter [] parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", SqlDbType.Char, 36, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Default, elementRealTimeLine.ID),
				new SqlParameter("@ScreenID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ScreenID", DataRowVersion.Default, elementRealTimeLine.ScreenID),
				new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementRealTimeLine.ElementID),
				new SqlParameter("@LineType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineType", DataRowVersion.Default, elementRealTimeLine.LineType),
				new SqlParameter("@LineName", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "LineName", DataRowVersion.Default, elementRealTimeLine.LineName),
				new SqlParameter("@LineCZ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineCZ", DataRowVersion.Default, elementRealTimeLine.LineCZ),
				new SqlParameter("@LineShowType", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineShowType", DataRowVersion.Default, elementRealTimeLine.LineShowType),
				new SqlParameter("@LineStyle", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LineStyle", DataRowVersion.Default, elementRealTimeLine.LineStyle),
				new SqlParameter("@LinePointBJ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "LinePointBJ", DataRowVersion.Default, elementRealTimeLine.LinePointBJ),
				new SqlParameter("@LineColor", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "LineColor", DataRowVersion.Default, elementRealTimeLine.LineColor),
				new SqlParameter("@MinValue", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "MinValue", DataRowVersion.Default, elementRealTimeLine.MinValue),
				new SqlParameter("@MaxValue", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "MaxValue", DataRowVersion.Default, elementRealTimeLine.MaxValue),
				new SqlParameter("@ValueDecimal", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ValueDecimal", DataRowVersion.Default, elementRealTimeLine.ValueDecimal),
				new SqlParameter("@ShowFormat", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "ShowFormat", DataRowVersion.Default, elementRealTimeLine.ShowFormat),
				new SqlParameter("@TimeLen", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "TimeLen", DataRowVersion.Default, elementRealTimeLine.TimeLen),
				new SqlParameter("@TimeLenType", SqlDbType.VarChar, 2, ParameterDirection.Input, false, 0, 0, "TimeLenType", DataRowVersion.Default, elementRealTimeLine.TimeLenType),
				new SqlParameter("@LineCYZQLent", SqlDbType.VarChar, 2, ParameterDirection.Input, false, 0, 0, "LineCYZQLent", DataRowVersion.Default, elementRealTimeLine.LineCYZQLent),
				new SqlParameter("@LineCYZQType", SqlDbType.VarChar, 2, ParameterDirection.Input, false, 0, 0, "LineCYZQType", DataRowVersion.Default, elementRealTimeLine.LineCYZQType),
				new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "DeviceID", DataRowVersion.Default, elementRealTimeLine.DeviceID),
				new SqlParameter("@ChannelNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChannelNo", DataRowVersion.Default, elementRealTimeLine.ChannelNo),
				new SqlParameter("@ComputeStr", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ComputeStr", DataRowVersion.Default, elementRealTimeLine.ComputeStr),
				new SqlParameter("@StartTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "StartTime", DataRowVersion.Default, elementRealTimeLine.StartTime)
			};
			return db.ExecuteNoQuery(sql, parameters) > -1;
		}
		#endregion

		#region DELETE
		/// <summary>
		/// 删除t_Element_RealTimeLine
		/// </summary>
		public virtual bool Delete(IList mlist){
            if (mlist == null)
                return false;
            List<CommandList> listcmd = new List<CommandList>();
            foreach (t_Element_RealTimeLine obj in mlist)
            {
                string sql = string.Format(" delete from t_Element_RealTimeLine where  Id = '{0}'", obj.ID);
                listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
            }
            return db.ExecuteNoQueryTranPro(listcmd);
}

        public virtual bool Delete(string id)
        {
            string sql = string.Format(" delete from t_Element_RealTimeLine where  Id = '{0}'", id);
            return db.ExecuteNoQuery(sql) > 0;
        }
		#endregion
    }
}

