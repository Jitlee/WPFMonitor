﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DBUtility;
using WPFMonitor.Model.ZTControls;
using System.Data.SqlClient;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ElementEditDA : DALBase
    {

        public void UpdateScreen(t_Screen obj)
        {
            string strSQL = "update t_Screen set Width=@Width,Height=@Height,AutoSize=@AutoSize,ImageURL=@ImageURL where ScreenID=@ScreenID";
            SqlParameter[] parameters = new SqlParameter[]
                                                    {
                                                            new SqlParameter("@ScreenID", obj.ScreenID),
                                                            new SqlParameter("@Width", obj.Width),
                                                            new SqlParameter("@Height", obj.Height),
                                                            new SqlParameter("@AutoSize", obj.AutoSize),
                                                            new SqlParameter("@ImageURL", obj.ImageURL)
                                                    };
            CmdList.Add(new CommandList() { strCommandText = strSQL, Params = parameters });
        }

        public List<CommandList> CmdList { get; set; }
        public ElementEditDA()
        {
            CmdList = new List<CommandList>();
        }

        public int GetMaxElementID()
        {
            return Converter.ToInt32(db.ExecuteScalar("SELECT MAX(ElementID) FROM t_Element"));
        }

        /// <summary>
        /// 插入t_Element
        /// </summary>
        public bool InsertElement(t_Element element)
        {
            string intsertElementSQL = @"insert into t_Element (ElementID, ElementName, ControlID, ScreenX, ScreenY, TxtInfo, Width, Height, ImageURL, ForeColor, Font, ChildScreenID, DeviceID, ChannelNo, ScreenID, BackColor, Transparent, oldX, oldY, Method, MinFloat, MaxFloat, SerialNum, TotalLength, LevelNo, ComputeStr, ElementType, ParentID) values (@ElementID, @ElementName, @ControlID, @ScreenX, @ScreenY, @TxtInfo, @Width, @Height, @ImageURL, @ForeColor, @Font, @ChildScreenID, @DeviceID, @ChannelNo, @ScreenID, @BackColor, @Transparent, @oldX, @oldY, @Method, @MinFloat, @MaxFloat, @SerialNum, @TotalLength, @LevelNo, @ComputeStr, @ElementType, @ParentID)";
            SqlParameter[] parameters = new SqlParameter[]
                                                    {
                                                            new SqlParameter("@ElementID", element.ElementID),
                                                            new SqlParameter("@ElementName", element.ElementName),
                                                            new SqlParameter("@ControlID", element.ControlID),
                                                            new SqlParameter("@ScreenX", element.ScreenX),
                                                            new SqlParameter("@ScreenY", element.ScreenY),
                                                            new SqlParameter("@TxtInfo", element.TxtInfo),
                                                            new SqlParameter("@Width", element.Width),
                                                            new SqlParameter("@Height", element.Height),
                                                            new SqlParameter("@ImageURL", element.ImageURL),
                                                            new SqlParameter("@ForeColor", element.ForeColor),
                                                            new SqlParameter("@Font", element.Font),
                                                            new SqlParameter("@ChildScreenID", element.ChildScreenID),
                                                            new SqlParameter("@DeviceID", element.DeviceID),
                                                            new SqlParameter("@ChannelNo", element.ChannelNo),
                                                            new SqlParameter("@ScreenID", element.ScreenID),
                                                            new SqlParameter("@BackColor", element.BackColor),
                                                            new SqlParameter("@Transparent", element.Transparent),
                                                            new SqlParameter("@oldX", element.oldX),
                                                            new SqlParameter("@oldY", element.oldY),
                                                            new SqlParameter("@Method", element.Method),
                                                            new SqlParameter("@MinFloat", element.MinFloat),
                                                            new SqlParameter("@MaxFloat", element.MaxFloat),
                                                            new SqlParameter("@SerialNum", ConvertToDbNull(element.SerialNum)),
                                                            new SqlParameter("@TotalLength", element.TotalLength),
                                                            new SqlParameter("@LevelNo", ConvertToDbNull(element.LevelNo)),
                                                            new SqlParameter("@ComputeStr", ConvertToDbNull(element.ComputeStr)),
                                                            new SqlParameter("@ElementType", ConvertToDbNull(element.ElementType)),
                                                            new SqlParameter("@ParentID", ConvertToDbNull(element.ParentID))
                                                    };
            CmdList.Add(new CommandList() { strCommandText = intsertElementSQL, Params = parameters });
            return true;
        }

        public virtual bool InsertPropert(t_ElementProperty elementProperty)
        {

            string sql = "insert into t_ElementProperty (ElementID, PropertyNo, PropertyValue, Caption, PropertyName) values (@ElementID, @PropertyNo, @PropertyValue, @Caption, @PropertyName)";
            SqlParameter[] parameters = new SqlParameter[]
                                                        {
                                                                new SqlParameter("@ElementID", elementProperty.ElementID),
                                                                new SqlParameter("@PropertyNo", elementProperty.PropertyNo),
                                                                new SqlParameter("@PropertyValue", elementProperty.PropertyValue),
                                                                new SqlParameter("@Caption", elementProperty.Caption),
                                                                new SqlParameter("@PropertyName", elementProperty.PropertyName)
                                                        };
            CmdList.Add(new CommandList() { strCommandText = sql, Params = parameters });
            return true;
        }
        public virtual bool InsertRealTimeT(t_Element_RealTimeLine elementRealTimeLine)
        {
            string intsertRealtimeTSQL = "insert into t_Element_RealTimeLine (ID, ScreenID, ElementID, LineType, LineName, LineCZ, LineShowType, LineStyle, LinePointBJ, LineColor, MinValue, MaxValue, ValueDecimal, ShowFormat, TimeLen, TimeLenType, LineCYZQLent, LineCYZQType, DeviceID, ChannelNo, ComputeStr, StartTime) values (@ID, @ScreenID, @ElementID, @LineType, @LineName, @LineCZ, @LineShowType, @LineStyle, @LinePointBJ, @LineColor, @MinValue, @MaxValue, @ValueDecimal, @ShowFormat, @TimeLen, @TimeLenType, @LineCYZQLent, @LineCYZQType, @DeviceID, @ChannelNo, @ComputeStr, @StartTime)";
            SqlParameter[] parameters = new SqlParameter[]
                                                            {
                                                                    new SqlParameter("@ID", Guid.NewGuid().ToString("N")),
                                                                    new SqlParameter("@ScreenID",elementRealTimeLine.ScreenID),
                                                                    new SqlParameter("@ElementID", elementRealTimeLine.ElementID),
                                                                    new SqlParameter("@LineType",  elementRealTimeLine.LineType),
                                                                    new SqlParameter("@LineName",  elementRealTimeLine.LineName),
                                                                    new SqlParameter("@LineCZ", elementRealTimeLine.LineCZ),
                                                                    new SqlParameter("@LineShowType",  elementRealTimeLine.LineShowType),
                                                                    new SqlParameter("@LineStyle", elementRealTimeLine.LineStyle),
                                                                    new SqlParameter("@LinePointBJ",elementRealTimeLine.LinePointBJ),
                                                                    new SqlParameter("@LineColor", elementRealTimeLine.LineColor),
                                                                    new SqlParameter("@MinValue",  elementRealTimeLine.MinValue),
                                                                    new SqlParameter("@MaxValue",elementRealTimeLine.MaxValue),
                                                                    new SqlParameter("@ValueDecimal",elementRealTimeLine.ValueDecimal),
                                                                    new SqlParameter("@ShowFormat",elementRealTimeLine.ShowFormat),
                                                                    new SqlParameter("@TimeLen", elementRealTimeLine.TimeLen),
                                                                    new SqlParameter("@TimeLenType", elementRealTimeLine.TimeLenType),
                                                                    new SqlParameter("@LineCYZQLent", elementRealTimeLine.LineCYZQLent),
                                                                    new SqlParameter("@LineCYZQType",elementRealTimeLine.LineCYZQType),
                                                                    new SqlParameter("@DeviceID",ConvertToDbNull(elementRealTimeLine.DeviceID)),
                                                                    new SqlParameter("@ChannelNo",ConvertToDbNull(elementRealTimeLine.ChannelNo)),
                                                                    new SqlParameter("@ComputeStr", ConvertToDbNull(elementRealTimeLine.ComputeStr)),
                                                                    new SqlParameter("@StartTime", ConvertToDbNull(elementRealTimeLine.StartTime))
                                                            };
            CmdList.Add(new CommandList() { strCommandText = intsertRealtimeTSQL, Params = parameters });
            return true;
        }

        public bool UpdateElement(t_Element element)
        {
            string intsertElementSQL = @"update  t_Element 
 SET  ScreenX=@ScreenX, ScreenY=@ScreenY, TxtInfo=@TxtInfo
, Width=@Width, Height=@Height, ImageURL=@ImageURL
, ForeColor=@ForeColor, Font=@Font, ChildScreenID=@ChildScreenID
, DeviceID=@DeviceID, ChannelNo=@ChannelNo, ScreenID=@ScreenID
, BackColor=@BackColor, Transparent=@Transparent, oldX=@oldX, oldY=@oldY
, Method=@Method, MinFloat=@MinFloat, MaxFloat=@MaxFloat
, SerialNum=@SerialNum, TotalLength=@TotalLength, LevelNo=@LevelNo
, ComputeStr=@ComputeStr, ElementType=@ElementType, ParentID=@ParentID
WHERE ElementID=@ElementID
";
            SqlParameter[] parameters = new SqlParameter[]
                                                    {
                                                            new SqlParameter("@ElementID", element.ElementID),
                                                            //new SqlParameter("@ElementName", element.ElementName),
                                                            //new SqlParameter("@ControlID", element.ControlID),
                                                            new SqlParameter("@ScreenX", element.ScreenX),
                                                            new SqlParameter("@ScreenY", element.ScreenY),
                                                            new SqlParameter("@TxtInfo", element.TxtInfo),
                                                            new SqlParameter("@Width", element.Width),
                                                            new SqlParameter("@Height", element.Height),
                                                            new SqlParameter("@ImageURL", element.ImageURL),
                                                            new SqlParameter("@ForeColor", element.ForeColor),
                                                            new SqlParameter("@Font", element.Font),
                                                            new SqlParameter("@ChildScreenID", element.ChildScreenID),
                                                            new SqlParameter("@DeviceID", element.DeviceID),
                                                            new SqlParameter("@ChannelNo", element.ChannelNo),
                                                            new SqlParameter("@ScreenID", element.ScreenID),
                                                            new SqlParameter("@BackColor", element.BackColor),
                                                            new SqlParameter("@Transparent", element.Transparent),
                                                            new SqlParameter("@oldX", element.oldX),
                                                            new SqlParameter("@oldY", element.oldY),
                                                            new SqlParameter("@Method", element.Method),
                                                            new SqlParameter("@MinFloat", element.MinFloat),
                                                            new SqlParameter("@MaxFloat", element.MaxFloat),
                                                            new SqlParameter("@SerialNum", ConvertToDbNull(element.SerialNum)),
                                                            new SqlParameter("@TotalLength", element.TotalLength),
                                                            new SqlParameter("@LevelNo", ConvertToDbNull(element.LevelNo)),
                                                            new SqlParameter("@ComputeStr", ConvertToDbNull(element.ComputeStr)),
                                                            new SqlParameter("@ElementType", ConvertToDbNull(element.ElementType)),
                                                            new SqlParameter("@ParentID", ConvertToDbNull(element.ParentID))
                                                    };
            CmdList.Add(new CommandList() { strCommandText = intsertElementSQL, Params = parameters });
            return true;
        }
		public bool DeleteRealTimeLine(int ElementID)
		{
			string sqlR = string.Format("DELETE t_Element_RealTimeLine  WHERE ElementID={0}", ElementID);
			CmdList.Add(new CommandList() { strCommandText = sqlR});
			return true;
		}

        public bool DeleteElement(int ElementID)
        {
            string sqlE = string.Format("DELETE t_Element  WHERE ElementID={0}", ElementID);
			CmdList.Add(new CommandList() { strCommandText = sqlE, Type = CommandType.Text });
            string sqlP = string.Format("DELETE t_ElementProperty  WHERE ElementID={0}", ElementID);
			CmdList.Add(new CommandList() { strCommandText = sqlP, Type = CommandType.Text });
			string sqlR = string.Format("DELETE t_Element_RealTimeLine  WHERE ElementID={0}", ElementID);
            CmdList.Add(new CommandList() { strCommandText = sqlR, Type = CommandType.Text });
			string sqlChild = string.Format("DELETE t_Element WHERE ParentID={0}", ElementID);
			CmdList.Add(new CommandList() { strCommandText = sqlChild, Type = CommandType.Text });
            return true;
        }
        public virtual bool UpdatePropert(t_ElementProperty elementProperty)
        {

            string sql = @"update t_ElementProperty   SET PropertyValue=@PropertyValue  WHERE ElementID=@ElementID AND PropertyNo=@PropertyNo";
            SqlParameter[] parameters = new SqlParameter[]
                                                        {
                                                                new SqlParameter("@ElementID", elementProperty.ElementID),
                                                                new SqlParameter("@PropertyNo", elementProperty.PropertyNo),
                                                                new SqlParameter("@PropertyValue", elementProperty.PropertyValue)
                                                                                                //new SqlParameter("@Caption", elementProperty.Caption),
                                                                                                //new SqlParameter("@PropertyName", elementProperty.PropertyName)
                                                        };
            CmdList.Add(new CommandList() { strCommandText = sql, Params = parameters });
            return true;
        }
        public virtual bool UpdateRealTimeT(t_Element_RealTimeLine elementRealTimeLine)
        {
            string intsertRealtimeTSQL = @"update t_Element_RealTimeLine  SET  ScreenID= @ScreenID, ElementID=@ElementID, LineType=@LineType, LineName=@LineName,
LineCZ=@LineCZ, LineShowType=@LineShowType, LineStyle=@LineStyle, LinePointBJ=@LinePointBJ, LineColor= @LineColor,
MinValue=@MinValue, MaxValue= @MaxValue, ValueDecimal=@ValueDecimal, ShowFormat= @ShowFormat, TimeLen=@TimeLen,
TimeLenType= @TimeLenType, LineCYZQLent=@LineCYZQLent, LineCYZQType=@LineCYZQType, DeviceID=@DeviceID,
ChannelNo=@ChannelNo, ComputeStr=@ComputeStr, StartTime= @StartTime WHERE ID=@ID";
            SqlParameter[] parameters = new SqlParameter[]
                                                            {
                                                                    new SqlParameter("@ID", Guid.NewGuid().ToString("N")),
                                                                    new SqlParameter("@ScreenID",elementRealTimeLine.ScreenID),
                                                                    new SqlParameter("@ElementID", elementRealTimeLine.ElementID),
                                                                    new SqlParameter("@LineType",  elementRealTimeLine.LineType),
                                                                    new SqlParameter("@LineName",  elementRealTimeLine.LineName),
                                                                    new SqlParameter("@LineCZ", elementRealTimeLine.LineCZ),
                                                                    new SqlParameter("@LineShowType",  elementRealTimeLine.LineShowType),
                                                                    new SqlParameter("@LineStyle", elementRealTimeLine.LineStyle),
                                                                    new SqlParameter("@LinePointBJ",elementRealTimeLine.LinePointBJ),
                                                                    new SqlParameter("@LineColor", elementRealTimeLine.LineColor),
                                                                    new SqlParameter("@MinValue",  elementRealTimeLine.MinValue),
                                                                    new SqlParameter("@MaxValue",elementRealTimeLine.MaxValue),
                                                                    new SqlParameter("@ValueDecimal",elementRealTimeLine.ValueDecimal),
                                                                    new SqlParameter("@ShowFormat",elementRealTimeLine.ShowFormat),
                                                                    new SqlParameter("@TimeLen", elementRealTimeLine.TimeLen),
                                                                    new SqlParameter("@TimeLenType", elementRealTimeLine.TimeLenType),
                                                                    new SqlParameter("@LineCYZQLent", elementRealTimeLine.LineCYZQLent),
                                                                    new SqlParameter("@LineCYZQType",elementRealTimeLine.LineCYZQType),
                                                                    new SqlParameter("@DeviceID",ConvertToDbNull(elementRealTimeLine.DeviceID)),
                                                                    new SqlParameter("@ChannelNo",ConvertToDbNull(elementRealTimeLine.ChannelNo)),
                                                                    new SqlParameter("@ComputeStr", ConvertToDbNull(elementRealTimeLine.ComputeStr)),
                                                                    new SqlParameter("@StartTime", ConvertToDbNull(elementRealTimeLine.StartTime))
                                                            };
            CmdList.Add(new CommandList() { strCommandText = intsertRealtimeTSQL, Params = parameters });
            return true;
        }

        private object ConvertToDbNull(object value)
        {
            return null == value ? DBNull.Value : value;
        }

		/// <summary>
		/// 保存
		/// </summary>
		/// <returns></returns>
        public bool ExcutCmd()
        {
			if (CmdList == null)
				return false;
			if (CmdList.Count == 0)
				return false;

			string strSql="select * from t_ElementProperty where ElementID not in (Select ElementID from t_Element)";
			CmdList.Add(new CommandList() { strCommandText = strSql, Type = CommandType.Text });			 
            
			return db.ExecuteNoQueryTranPro(CmdList);
        }
    }
}