using System;
using System.Data;
using WPFMonitor.Model;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_Element_RealTimeLine: ORBase
    {
       
		private string _ID;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			get { return _ID; }
			set { _ID = value;
NotifyPropertyChanged("ID");}
		}

		private int _ScreenID;
		/// <summary>
		/// 场景ID
		/// </summary>
		public int ScreenID
		{
			get { return _ScreenID; }
			set { _ScreenID = value;
NotifyPropertyChanged("ScreenID");}
		}

		private int _ElementID;
		/// <summary>
		/// 无素ID
		/// </summary>
		public int ElementID
		{
			get { return _ElementID; }
			set { _ElementID = value;
NotifyPropertyChanged("ElementID");}
		}

		private int _LineType;
		/// <summary>
		/// 线类型
		/// </summary>
		public int LineType
		{
			get { return _LineType; }
			set { _LineType = value;
NotifyPropertyChanged("LineType");}
		}

		private string _LineName;
		/// <summary>
		/// 线名称
		/// </summary>
		public string LineName
		{
			get { return _LineName; }
			set { _LineName = value;
NotifyPropertyChanged("LineName");}
		}

		private int _LineCZ;
		/// <summary>
		/// 取值()
		/// </summary>
		public int LineCZ
		{
			get { return _LineCZ; }
			set { _LineCZ = value;
NotifyPropertyChanged("LineCZ");}
		}

		private int _LineShowType;
		/// <summary>
		/// 类型(直线、阶梯线)
		/// </summary>
		public int LineShowType
		{
			get { return _LineShowType; }
			set { _LineShowType = value;
NotifyPropertyChanged("LineShowType");}
		}

		private int _LineStyle;
		/// <summary>
		/// 样式
		/// </summary>
		public int LineStyle
		{
			get { return _LineStyle; }
			set { _LineStyle = value;
NotifyPropertyChanged("LineStyle");}
		}

		private int _LinepointBJ;
		/// <summary>
		/// 标记,不画点
		/// </summary>
		public int LinePointBJ
		{
			get { return _LinepointBJ; }
			set { _LinepointBJ = value;
NotifyPropertyChanged("LinePointBJ");}
		}

		private string _LineColor;
		/// <summary>
		/// 线颜色
		/// </summary>
		public string LineColor
		{
			get { return _LineColor; }
			set { _LineColor = value;
NotifyPropertyChanged("LineColor");}
		}

		private string _MinValue;
		/// <summary>
		/// 最小值
		/// </summary>
		public string MinValue
		{
			get { return _MinValue; }
			set { _MinValue = value;
NotifyPropertyChanged("MinValue");}
		}

		private string _MaxValue;
		/// <summary>
		/// 最大值
		/// </summary>
		public string MaxValue
		{
			get { return _MaxValue; }
			set { _MaxValue = value;
NotifyPropertyChanged("MaxValue");}
		}

		private int _ValueDecimal;
		/// <summary>
		/// 小数位长度
		/// </summary>
		public int ValueDecimal
		{
			get { return _ValueDecimal; }
			set { _ValueDecimal = value;
NotifyPropertyChanged("ValueDecimal");}
		}

		private string _ShowFormat;
		/// <summary>
		/// 显示格式
		/// </summary>
		public string ShowFormat
		{
			get { return _ShowFormat; }
			set { _ShowFormat = value;
NotifyPropertyChanged("ShowFormat");}
		}

		private int _TimeLen;
		/// <summary>
		/// 时间长度
		/// </summary>
		public int TimeLen
		{
			get { return _TimeLen; }
			set { _TimeLen = value;
NotifyPropertyChanged("TimeLen");}
		}

		private string _TimeLenType;
		/// <summary>
		/// 时间长度类型
		/// </summary>
		public string TimeLenType
		{
			get { return _TimeLenType; }
			set { _TimeLenType = value;
NotifyPropertyChanged("TimeLenType");}
		}

		private string _LineCAZQLent;
		/// <summary>
		/// 时间采样周期
		/// </summary>
		public string LineCYZQLent
		{
			get { return _LineCAZQLent; }
			set { _LineCAZQLent = value;
NotifyPropertyChanged("LineCYZQLent");}
		}

		private string _LineCYZQType;
		/// <summary>
		/// 采样周期类型
		/// </summary>
		public string LineCYZQType
		{
			get { return _LineCYZQType; }
			set { _LineCYZQType = value;
NotifyPropertyChanged("LineCYZQType");}
		}

		private int _DeviceID;
		/// <summary>
		/// 取值设备ID
		/// </summary>
		public int DeviceID
		{
			get { return _DeviceID; }
			set { _DeviceID = value;
NotifyPropertyChanged("DeviceID");}
		}

		private int _ChannelNo;
		/// <summary>
		/// 取值设备通道
		/// </summary>
		public int ChannelNo
		{
			get { return _ChannelNo; }
			set { _ChannelNo = value;
NotifyPropertyChanged("ChannelNo");}
		}

		private string _ComputeStr;
		/// <summary>
		/// 取值表达试
		/// </summary>
		public string ComputeStr
		{
			get { return _ComputeStr; }
			set { _ComputeStr = value;
NotifyPropertyChanged("ComputeStr");}
		}

		private DateTime _StartTime;
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartTime
		{
			get { return _StartTime; }
			set { _StartTime = value;
NotifyPropertyChanged("StartTime");}
		}

		/// <summary>
		/// ElementRealTimeLine构造函数
		/// </summary>
		public t_Element_RealTimeLine()
		{

		}

		/// <summary>
		/// ElementRealTimeLine构造函数
		/// </summary>
		public t_Element_RealTimeLine(DataRow row)
		{
			// 
			_ID = row["ID"].ToString().Trim();
			// 场景ID
			_ScreenID = Converter.ToInt32(row["ScreenID"]);
			// 无素ID
			_ElementID = Converter.ToInt32(row["ElementID"]);
			// 线类型
			_LineType = Converter.ToInt32(row["LineType"]);
			// 线名称
			_LineName = row["LineName"].ToString().Trim();
			// 取值()
			_LineCZ = Converter.ToInt32(row["LineCZ"]);
			// 类型(直线、阶梯线)
			_LineShowType = Converter.ToInt32(row["LineShowType"]);
			// 样式
			_LineStyle = Converter.ToInt32(row["LineStyle"]);
			// 标记,不画点
			_LinepointBJ = Converter.ToInt32(row["LinePointBJ"]);
			// 线颜色
			_LineColor = row["LineColor"].ToString().Trim();
			// 最小值
			_MinValue = row["MinValue"].ToString().Trim();
			// 最大值
			_MaxValue = row["MaxValue"].ToString().Trim();
			// 小数位长度
			_ValueDecimal = Converter.ToInt32(row["ValueDecimal"]);
			// 显示格式
			_ShowFormat = row["ShowFormat"].ToString().Trim();
			// 时间长度
			_TimeLen = Converter.ToInt32(row["TimeLen"]);
			// 时间长度类型
			_TimeLenType = row["TimeLenType"].ToString().Trim();
			// 时间采样周期
			_LineCAZQLent = row["LineCYZQLent"].ToString().Trim();
			// 采样周期类型
			_LineCYZQType = row["LineCYZQType"].ToString().Trim();
			// 取值设备ID
			_DeviceID = Converter.ToInt32(row["DeviceID"]);
			// 取值设备通道
			_ChannelNo = Converter.ToInt32(row["ChannelNo"]);
			// 取值表达试
			_ComputeStr = row["ComputeStr"].ToString().Trim();
			// 开始时间
			_StartTime = Converter.ToDateTime(row["StartTime"]);
		}

	public void Clone(t_Element_RealTimeLine obj){
//
ID = obj.ID;
//场景ID
ScreenID = obj.ScreenID;
//无素ID
ElementID = obj.ElementID;
//线类型
LineType = obj.LineType;
//线名称
LineName = obj.LineName;
//取值()
LineCZ = obj.LineCZ;
//类型(直线、阶梯线)
LineShowType = obj.LineShowType;
//样式
LineStyle = obj.LineStyle;
//标记,不画点
LinePointBJ = obj.LinePointBJ;
//线颜色
LineColor = obj.LineColor;
//最小值
MinValue = obj.MinValue;
//最大值
MaxValue = obj.MaxValue;
//小数位长度
ValueDecimal = obj.ValueDecimal;
//显示格式
ShowFormat = obj.ShowFormat;
//时间长度
TimeLen = obj.TimeLen;
//时间长度类型
TimeLenType = obj.TimeLenType;
//时间采样周期
LineCYZQLent = obj.LineCYZQLent;
//采样周期类型
LineCYZQType = obj.LineCYZQType;
//取值设备ID
DeviceID = obj.DeviceID;
//取值设备通道
ChannelNo = obj.ChannelNo;
//取值表达试
ComputeStr = obj.ComputeStr;
//开始时间
StartTime = obj.StartTime;

}

    }
}

