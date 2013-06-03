using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_Element: ORBase
    {
       
		private int _ElementID;
		/// <summary>
		/// 
		/// </summary>
		public int ElementID
		{
			get { return _ElementID; }
			set { _ElementID = value;
NotifyPropertyChanged("ElementID");}
		}

		private string _ElementName;
		/// <summary>
		/// 
		/// </summary>
		public string ElementName
		{
			get { return _ElementName; }
			set { _ElementName = value;
NotifyPropertyChanged("ElementName");}
		}

		private int? _ControlID;
		/// <summary>
		/// 
		/// </summary>
		public int? ControlID
		{
			get { return _ControlID; }
			set { _ControlID = value;
NotifyPropertyChanged("ControlID");}
		}

		private int _ScreenX;
		/// <summary>
		/// 
		/// </summary>
		public int ScreenX
		{
			get { return _ScreenX; }
			set { _ScreenX = value;
NotifyPropertyChanged("ScreenX");}
		}

		private int _ScreenY;
		/// <summary>
		/// 
		/// </summary>
		public int ScreenY
		{
			get { return _ScreenY; }
			set { _ScreenY = value;
NotifyPropertyChanged("ScreenY");}
		}

		private string _TxtInfo;
		/// <summary>
		/// 
		/// </summary>
		public string TxtInfo
		{
			get { return _TxtInfo; }
			set { _TxtInfo = value;
NotifyPropertyChanged("TxtInfo");}
		}

		private int? _Width;
		/// <summary>
		/// 
		/// </summary>
		public int? Width
		{
			get { return _Width; }
			set { _Width = value;
NotifyPropertyChanged("Width");}
		}

		private int? _Height;
		/// <summary>
		/// 
		/// </summary>
		public int? Height
		{
			get { return _Height; }
			set { _Height = value;
NotifyPropertyChanged("Height");}
		}

		private string _ImageURL;
		/// <summary>
		/// 
		/// </summary>
		public string ImageURL
		{
			get { return _ImageURL; }
			set { _ImageURL = value;
NotifyPropertyChanged("ImageURL");}
		}

		private string _ForeColor;
		/// <summary>
		/// 
		/// </summary>
		public string ForeColor
		{
			get { return _ForeColor; }
			set { _ForeColor = value;
NotifyPropertyChanged("ForeColor");}
		}

		private string _Font;
		/// <summary>
		/// 
		/// </summary>
		public string Font
		{
			get { return _Font; }
			set { _Font = value;
NotifyPropertyChanged("Font");}
		}

		private string _ChildScreenID;
		/// <summary>
		/// 
		/// </summary>
		public string ChildScreenID
		{
			get { return _ChildScreenID; }
			set { _ChildScreenID = value;
NotifyPropertyChanged("ChildScreenID");}
		}

		private int? _DeviceID;
		/// <summary>
		/// 
		/// </summary>
		public int? DeviceID
		{
			get { return _DeviceID; }
			set { _DeviceID = value;
NotifyPropertyChanged("DeviceID");}
		}

		private int? _ChannelNo;
		/// <summary>
		/// 
		/// </summary>
		public int? ChannelNo
		{
			get { return _ChannelNo; }
			set { _ChannelNo = value;
NotifyPropertyChanged("ChannelNo");}
		}

		private int _ScreenID;
		/// <summary>
		/// 
		/// </summary>
		public int ScreenID
		{
			get { return _ScreenID; }
			set { _ScreenID = value;
NotifyPropertyChanged("ScreenID");}
		}

		private string _BackColor;
		/// <summary>
		/// 
		/// </summary>
		public string BackColor
		{
			get { return _BackColor; }
			set { _BackColor = value;
NotifyPropertyChanged("BackColor");}
		}

		private int? _Transparent;
		/// <summary>
		/// 
		/// </summary>
		public int? Transparent
		{
			get { return _Transparent; }
			set { _Transparent = value;
NotifyPropertyChanged("Transparent");}
		}

		private int _oldX;
		/// <summary>
		/// 
		/// </summary>
		public int oldX
		{
			get { return _oldX; }
			set { _oldX = value;
NotifyPropertyChanged("oldX");}
		}

		private int _oldY;
		/// <summary>
		/// 
		/// </summary>
		public int oldY
		{
			get { return _oldY; }
			set { _oldY = value;
NotifyPropertyChanged("oldY");}
		}

		private int _Method;
		/// <summary>
		/// 
		/// </summary>
		public int Method
		{
			get { return _Method; }
			set { _Method = value;
NotifyPropertyChanged("Method");}
		}

		private double? _MinFloat;
		/// <summary>
		/// 
		/// </summary>
        public double? MinFloat
		{
			get { return _MinFloat; }
			set { _MinFloat = value;
NotifyPropertyChanged("MinFloat");}
		}

        private double? _MaxFloat;
		/// <summary>
		/// 
		/// </summary>
		public double? MaxFloat
		{
			get { return _MaxFloat; }
			set { _MaxFloat = value;
NotifyPropertyChanged("MaxFloat");}
		}

		private int _SerialNum;
		/// <summary>
		/// 
		/// </summary>
		public int SerialNum
		{
			get { return _SerialNum; }
			set { _SerialNum = value;
NotifyPropertyChanged("SerialNum");}
		}

		private float _TotalLength;
		/// <summary>
		/// 
		/// </summary>
		public float TotalLength
		{
			get { return _TotalLength; }
			set { _TotalLength = value;
NotifyPropertyChanged("TotalLength");}
		}

		private int? _LevelNo;
		/// <summary>
		/// 
		/// </summary>
		public int? LevelNo
		{
			get { return _LevelNo; }
			set { _LevelNo = value;
NotifyPropertyChanged("LevelNo");}
		}

		private string _ComputeStr;
		/// <summary>
		/// 
		/// </summary>
		public string ComputeStr
		{
			get { return _ComputeStr; }
			set { _ComputeStr = value;
NotifyPropertyChanged("ComputeStr");}
		}

		private string _ElementType;
		/// <summary>
		/// 
		/// </summary>
		public string ElementType
		{
			get { return _ElementType; }
			set { _ElementType = value;
NotifyPropertyChanged("ElementType");}
		}

		private int _ParentID;
		/// <summary>
		/// 
		/// </summary>
		public int ParentID
		{
			get { return _ParentID; }
			set { _ParentID = value;
NotifyPropertyChanged("ParentID");}
		}

		/// <summary>
		/// Element构造函数
		/// </summary>
		public t_Element()
		{

		}

		/// <summary>
		/// Element构造函数
		/// </summary>
		public t_Element(DataRow row)
		{
			// 
			_ElementID = Converter.ToInt32(row["ElementID"]);
			// 
			_ElementName = row["ElementName"].ToString().Trim();
			// 
			_ControlID = Converter.ToInt32(row["ControlID"]);
			// 
			_ScreenX = Converter.ToInt32(row["ScreenX"]);
			// 
			_ScreenY = Converter.ToInt32(row["ScreenY"]);
			// 
			_TxtInfo = row["TxtInfo"].ToString().Trim();
			// 
			_Width = Converter.ToInt32(row["Width"]);
			// 
			_Height = Converter.ToInt32(row["Height"]);
			// 
			_ImageURL = row["ImageURL"].ToString().Trim();
			// 
			_ForeColor = row["ForeColor"].ToString().Trim();
			// 
			_Font = row["Font"].ToString().Trim();
			// 
			_ChildScreenID = row["ChildScreenID"].ToString().Trim();
			// 
			_DeviceID = Converter.ToInt32(row["DeviceID"]);
			// 
			_ChannelNo = Converter.ToInt32(row["ChannelNo"]);
			// 
			_ScreenID = Converter.ToInt32(row["ScreenID"]);
			// 
			_BackColor = row["BackColor"].ToString().Trim();
			// 
			_Transparent = Converter.ToInt32(row["Transparent"]);
			// 
			_oldX = Converter.ToInt32(row["oldX"]);
			// 
			_oldY = Converter.ToInt32(row["oldY"]);
			// 
			_Method = Converter.ToInt32(row["Method"]);
			// 
            _MinFloat = Converter.ToFloat(row["MinFloat"]);
			// 
            _MaxFloat = Converter.ToFloat(row["MaxFloat"]);
			// 
			_SerialNum = Converter.ToInt32(row["SerialNum"]);
			// 
			_TotalLength = Converter.ToFloat(row["TotalLength"]);
			// 
			_LevelNo = Converter.ToInt32(row["LevelNo"]);
			// 
			_ComputeStr = row["ComputeStr"].ToString().Trim();
			// 
			_ElementType = row["ElementType"].ToString().Trim();
			// 
			_ParentID = Converter.ToInt32(row["ParentID"]);
		}

	public void Clone(t_Element obj){
//
ElementID = obj.ElementID;
//
ElementName = obj.ElementName;
//
ControlID = obj.ControlID;
//
ScreenX = obj.ScreenX;
//
ScreenY = obj.ScreenY;
//
TxtInfo = obj.TxtInfo;
//
Width = obj.Width;
//
Height = obj.Height;
//
ImageURL = obj.ImageURL;
//
ForeColor = obj.ForeColor;
//
Font = obj.Font;
//
ChildScreenID = obj.ChildScreenID;
//
DeviceID = obj.DeviceID;
//
ChannelNo = obj.ChannelNo;
//
ScreenID = obj.ScreenID;
//
BackColor = obj.BackColor;
//
Transparent = obj.Transparent;
//
oldX = obj.oldX;
//
oldY = obj.oldY;
//
Method = obj.Method;
//
MinFloat = obj.MinFloat;
//
MaxFloat = obj.MaxFloat;
//
SerialNum = obj.SerialNum;
//
TotalLength = obj.TotalLength;
//
LevelNo = obj.LevelNo;
//
ComputeStr = obj.ComputeStr;
//
ElementType = obj.ElementType;
//
ParentID = obj.ParentID;

}

    }
}

