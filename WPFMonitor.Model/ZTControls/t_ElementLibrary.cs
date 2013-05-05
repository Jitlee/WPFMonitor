using System;
using System.Data;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_ElementLibrary: ORBase
    {
       
		private int _Elementid;
		/// <summary>
		/// 
		/// </summary>
		public int Elementid
		{
			get { return _Elementid; }
			set { _Elementid = value;
NotifyPropertyChanged("Elementid");}
		}

		private string _Elementname;
		/// <summary>
		/// 
		/// </summary>
		public string Elementname
		{
			get { return _Elementname; }
			set { _Elementname = value;
NotifyPropertyChanged("Elementname");}
		}

		private int _Controlid;
		/// <summary>
		/// 
		/// </summary>
		public int Controlid
		{
			get { return _Controlid; }
			set { _Controlid = value;
NotifyPropertyChanged("Controlid");}
		}

		private int _Screenx;
		/// <summary>
		/// 
		/// </summary>
		public int Screenx
		{
			get { return _Screenx; }
			set { _Screenx = value;
NotifyPropertyChanged("Screenx");}
		}

		private int _Screeny;
		/// <summary>
		/// 
		/// </summary>
		public int Screeny
		{
			get { return _Screeny; }
			set { _Screeny = value;
NotifyPropertyChanged("Screeny");}
		}

		private string _Txtinfo;
		/// <summary>
		/// 
		/// </summary>
		public string Txtinfo
		{
			get { return _Txtinfo; }
			set { _Txtinfo = value;
NotifyPropertyChanged("Txtinfo");}
		}

		private int _Width;
		/// <summary>
		/// 
		/// </summary>
		public int Width
		{
			get { return _Width; }
			set { _Width = value;
NotifyPropertyChanged("Width");}
		}

		private int _Height;
		/// <summary>
		/// 
		/// </summary>
		public int Height
		{
			get { return _Height; }
			set { _Height = value;
NotifyPropertyChanged("Height");}
		}

		private string _Imageurl;
		/// <summary>
		/// 
		/// </summary>
		public string Imageurl
		{
			get { return _Imageurl; }
			set { _Imageurl = value;
NotifyPropertyChanged("Imageurl");}
		}

		private string _Forecolor;
		/// <summary>
		/// 
		/// </summary>
		public string Forecolor
		{
			get { return _Forecolor; }
			set { _Forecolor = value;
NotifyPropertyChanged("Forecolor");}
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

		private string _Childscreenid;
		/// <summary>
		/// 
		/// </summary>
		public string Childscreenid
		{
			get { return _Childscreenid; }
			set { _Childscreenid = value;
NotifyPropertyChanged("Childscreenid");}
		}

		private int _Deviceid;
		/// <summary>
		/// 
		/// </summary>
		public int Deviceid
		{
			get { return _Deviceid; }
			set { _Deviceid = value;
NotifyPropertyChanged("Deviceid");}
		}

		private int _Channelno;
		/// <summary>
		/// 
		/// </summary>
		public int Channelno
		{
			get { return _Channelno; }
			set { _Channelno = value;
NotifyPropertyChanged("Channelno");}
		}

		private int _Screenid;
		/// <summary>
		/// 
		/// </summary>
		public int Screenid
		{
			get { return _Screenid; }
			set { _Screenid = value;
NotifyPropertyChanged("Screenid");}
		}

		private string _Backcolor;
		/// <summary>
		/// 
		/// </summary>
		public string Backcolor
		{
			get { return _Backcolor; }
			set { _Backcolor = value;
NotifyPropertyChanged("Backcolor");}
		}

		private int _Transparent;
		/// <summary>
		/// 
		/// </summary>
		public int Transparent
		{
			get { return _Transparent; }
			set { _Transparent = value;
NotifyPropertyChanged("Transparent");}
		}

		private int _Oldx;
		/// <summary>
		/// 
		/// </summary>
		public int Oldx
		{
			get { return _Oldx; }
			set { _Oldx = value;
NotifyPropertyChanged("Oldx");}
		}

		private int _Oldy;
		/// <summary>
		/// 
		/// </summary>
		public int Oldy
		{
			get { return _Oldy; }
			set { _Oldy = value;
NotifyPropertyChanged("Oldy");}
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

		private float _Minfloat;
		/// <summary>
		/// 
		/// </summary>
		public float Minfloat
		{
			get { return _Minfloat; }
			set { _Minfloat = value;
NotifyPropertyChanged("Minfloat");}
		}

		private float _Maxfloat;
		/// <summary>
		/// 
		/// </summary>
		public float Maxfloat
		{
			get { return _Maxfloat; }
			set { _Maxfloat = value;
NotifyPropertyChanged("Maxfloat");}
		}

		private int _Serialnum;
		/// <summary>
		/// 
		/// </summary>
		public int Serialnum
		{
			get { return _Serialnum; }
			set { _Serialnum = value;
NotifyPropertyChanged("Serialnum");}
		}

		private float _Totallength;
		/// <summary>
		/// 
		/// </summary>
		public float Totallength
		{
			get { return _Totallength; }
			set { _Totallength = value;
NotifyPropertyChanged("Totallength");}
		}

		/// <summary>
		/// ElementLibrary构造函数
		/// </summary>
		public t_ElementLibrary()
		{

		}

		/// <summary>
		/// ElementLibrary构造函数
		/// </summary>
		public t_ElementLibrary(DataRow row)
		{
			// 
			_Elementid = Convert.ToInt32(row["ElementID"]);
			// 
			_Elementname = row["ElementName"].ToString().Trim();
			// 
			_Controlid = Convert.ToInt32(row["ControlID"]);
			// 
			_Screenx = Convert.ToInt32(row["ScreenX"]);
			// 
			_Screeny = Convert.ToInt32(row["ScreenY"]);
			// 
			_Txtinfo = row["TxtInfo"].ToString().Trim();
			// 
			_Width = Convert.ToInt32(row["Width"]);
			// 
			_Height = Convert.ToInt32(row["Height"]);
			// 
			_Imageurl = row["ImageURL"].ToString().Trim();
			// 
			_Forecolor = row["ForeColor"].ToString().Trim();
			// 
			_Font = row["Font"].ToString().Trim();
			// 
			_Childscreenid = row["ChildScreenID"].ToString().Trim();
			// 
			_Deviceid = Convert.ToInt32(row["DeviceID"]);
			// 
			_Channelno = Convert.ToInt32(row["ChannelNo"]);
			// 
			_Screenid = Convert.ToInt32(row["ScreenID"]);
			// 
			_Backcolor = row["BackColor"].ToString().Trim();
			// 
			_Transparent = Convert.ToInt32(row["Transparent"]);
			// 
			_Oldx = Convert.ToInt32(row["oldX"]);
			// 
			_Oldy = Convert.ToInt32(row["oldY"]);
			// 
			_Method = Convert.ToInt32(row["Method"]);
			// 
			_Minfloat = float.Parse(row["MinFloat"].ToString());
			// 
			_Maxfloat = float.Parse(row["MaxFloat"].ToString());
			// 
			_Serialnum = Convert.ToInt32(row["SerialNum"]);
			// 
			_Totallength = float.Parse(row["TotalLength"].ToString());
		}

	public void Clone(t_ElementLibrary obj){
//
Elementid = obj.Elementid;
//
Elementname = obj.Elementname;
//
Controlid = obj.Controlid;
//
Screenx = obj.Screenx;
//
Screeny = obj.Screeny;
//
Txtinfo = obj.Txtinfo;
//
Width = obj.Width;
//
Height = obj.Height;
//
Imageurl = obj.Imageurl;
//
Forecolor = obj.Forecolor;
//
Font = obj.Font;
//
Childscreenid = obj.Childscreenid;
//
Deviceid = obj.Deviceid;
//
Channelno = obj.Channelno;
//
Screenid = obj.Screenid;
//
Backcolor = obj.Backcolor;
//
Transparent = obj.Transparent;
//
Oldx = obj.Oldx;
//
Oldy = obj.Oldy;
//
Method = obj.Method;
//
Minfloat = obj.Minfloat;
//
Maxfloat = obj.Maxfloat;
//
Serialnum = obj.Serialnum;
//
Totallength = obj.Totallength;

}

    }
}

