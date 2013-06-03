using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPFMonitor.Model;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;

namespace MonitorSystem.MonitorSystemGlobal
{
    public abstract partial class MonitorControl : UserControl
    {
        public static Type GetType(string name)
        {
            return Type.GetType(name);
        }

        public static Action<string[], object> UpdatePropertyGrid { get; set; }

        public static void OnUpdatePropertyGrid(string[] properties, object propertyObject)
        {
            UpdatePropertyGrid(properties, propertyObject);
        }

        public static Func<Canvas> GetMainCanvas { get; set; }

        internal static Canvas OnGetMainCanvas()
        {
            return GetMainCanvas();
        }

        public static Func<t_Control, t_Element> InitElement { get; set; }

        internal static t_Element OnInitControl(t_Control control)
        {
            return InitElement(control);
        }

        public static Func<Canvas, t_Element, ElementSate, List<t_ElementProperty>, MonitorControl> LoadElement { get; set; }

        public MonitorControl OnLoadElement(Canvas canvas, t_Element element, ElementSate state, List<t_ElementProperty> properties)
        {
            if (null != LoadElement)
            {
                return LoadElement(canvas, element, state, properties);
            }
            return null;
        }

        public static Action<t_Screen> LoadScreen { get; set; }

        protected void OnLoadScreen(object sender, t_Screen screen)
        {
            if (null != LoadScreen)
            {
                LoadScreen(screen);
            }
        }

        public static Func<List<t_Screen>> GetScreenList { get; set; }
        public static List<t_Screen> ScreenList
        {
            get
            {
                return GetScreenList();
            }
        }

        /// <summary>
        /// 是否为ToolTip
        /// </summary>
        public bool IsToolTip { get; protected set; }

        public MonitorControl ParentControl { get; set; }

        private bool _allowToolTip = true;
        public bool AllowToolTip
        {
            get { return _allowToolTip; }
            set { _allowToolTip = value; }
        }
        public int ID { get; set; }
        public bool IsDesignMode { get { return null != AdornerLayer; } }
        public Adorner AdornerLayer { get; protected set; }
        public abstract void DesignMode();
        public abstract void UnDesignMode();
        public abstract FrameworkElement GetRootControl();
        public virtual void SetChildScreen(ObservableCollection<ScreenAddShowName> litobj)
        {
            throw new NotImplementedException();
        }

        public virtual ObservableCollection<ScreenAddShowName> GetChildScreenObj()
        {
            throw new NotImplementedException();
        }

        public abstract event EventHandler Selected;

        public abstract event EventHandler Unselected;

        private t_Element _ScreenElement;
        public t_Element ScreenElement
        {
            get
            {

                //SetFont();
                return _ScreenElement;
            }
            set
            {
                _ScreenElement = value;
                //GetFont(value.Font);
            }
        }
        #region 字体处理
        private void SetFont()
        {
            //[Font: Name=宋体, Size=15.75, Units=3, GdiCharSet=134, GdiVerticalFont=False]
            string s = string.Format("[Font: Name={0}, Size={1}, Units=3, GdiCharSet=134, GdiVerticalFont=False]",
                                   Common1.GetFontCN(this.FontFamily.Source),
                                  this.FontSize.ToString());
            if (_ScreenElement != null)
                _ScreenElement.Font = s;
        }

        private void GetFont(string strFont)
        {
            if (strFont == null)
            {
                this.FontSize = 12f;
                //this.FontFamily = new FontFamily("Simsun");
            }
            else
            {
                // 设置字体
                string Name = "STSong";

                double fontSize = 12f;

                int idx = strFont.IndexOf("Font:");
                if (idx != -1)
                {
                    strFont = strFont.Substring(idx + 5);
                    strFont = strFont.Remove(strFont.Length - 1);
                    char[] slip = new char[] { ',' };
                    string[] arrStr = strFont.Split(slip);
                    foreach (string str in arrStr)
                    {
                        char[] slipKey = new char[] { '=' };
                        string[] keyVal = str.Split(slipKey);
                        int LEN = keyVal[0].Length;
                        string tmp = "Name";
                        int LEN2 = tmp.Length;
                        if (keyVal[0].Equals(" Name", StringComparison.OrdinalIgnoreCase))
                            Name = keyVal[1];
                        if (keyVal[0].Equals(" Size", StringComparison.OrdinalIgnoreCase))
                            fontSize = (double)(Convert.ToDouble(keyVal[1]));
                    }
                }
                this.FontSize = fontSize;
                this.FontFamily = new FontFamily(Common1.GetFontEn(Name));
            }
        }
        #endregion
        /// <summary>
        /// 控件自定义属性列表,控件值
        /// </summary>
        public List<t_ElementProperty> ListElementProp { get; set; }
        /// <summary>
        /// 设置控件自定义属性值
        /// </summary>
        public abstract void SetPropertyValue();

        public abstract void SetCommonPropertyValue();

        /// <summary>
        /// 控件状态，新添加的，或以保存的
        /// </summary>
        public ElementSate ElementState;

        public void SetAttrByName(string name, object value)
        {
            if (null == ScreenElement
                || !ScreenElement.ControlID.HasValue)
            {
                return;
            }
            if (ListElementProp == null)
            {
                ListElementProp = new ElementPropertyDA().SelectBy(ScreenElement.ElementID);
                if (ListElementProp.Count == 0)
                {
                    var elementProperties = new ControlPropertyDA().SelectByControlId(ScreenElement.ControlID.Value);
                    foreach (t_ControlProperty elementProperty in elementProperties)
                    {
                        t_ElementProperty tt = new t_ElementProperty();
                        tt.Caption = elementProperty.Caption;
                        tt.ElementID = ScreenElement.ElementID;
                        tt.PropertyNo = elementProperty.PropertyNo;
                        tt.PropertyValue = elementProperty.DefaultValue;
                        tt.PropertyName = elementProperty.PropertyName;
                        ListElementProp.Add(tt);
                    }
                }
            }

            var property = ListElementProp.FirstOrDefault(p => string.Equals(p.PropertyName, name, StringComparison.CurrentCultureIgnoreCase));
            if (null != property)
            {
                property.PropertyValue = null == value ? string.Empty : value.ToString();
            }
            //else if (null != ScreenElement && null != value)
            //{
            //    var max = ListElementProp.Max(t => t.PropertyNo);
            //    ListElementProp.Add(new t_ElementProperty()
            //    {
            //        ElementID = ScreenElement.ElementID,
            //        PropertyName = name,
            //        PropertyValue = value.ToString(),
            //        PropertyNo = max
            //    });
            //}
            //foreach (t_ElementProperty ep in ListElementProp)
            //{
            //    if (ep.PropertyName.Trim().ToUpper() == name.Trim().ToUpper())
            //    {
            //        ep.PropertyValue = value.ToString();
            //        //break;
            //    }
            //}
        }

        private string[] m_BrowsableProperties = new string[] { "Left", "Top", "Width", "Height", "FontFamily", "FontSize", "Translate", "Foreground" };

        public abstract string[] BrowsableProperties { set; get; }

        public double Translate
        {
            get { return (double)GetValue(OpacityProperty) * 100d; }
            set { SetValue(OpacityProperty, value / 100d); this.ScreenElement.Transparent = (int)(value / 100d); }
        }


        [DefaultValue(""), Description("距左边位置")]
        public double Left
        {
            get { return (double)GetValue(Canvas.LeftProperty); }
            set { SetValue(Canvas.LeftProperty, value); AdornerLayer.SetValue(Canvas.LeftProperty, value); this.ScreenElement.ScreenX = (int)value; }
        }
        [DefaultValue(""), Description("距上面位置高度")]
        public double Top
        {
            get { return (double)GetValue(Canvas.TopProperty); }
            set { SetValue(Canvas.TopProperty, value); AdornerLayer.SetValue(Canvas.TopProperty, value); this.ScreenElement.ScreenY = (int)value; }
        }

        public MonitorControl()
        {
            SetValue(ForegroundProperty, new SolidColorBrush(Colors.Black));
        }

        #region 设置值
        protected float m_fValue;//通道值
        protected float[] m_fValueArray;
        //设置元素的值
        public virtual void SetChannelValue(float fValue)
        {
            m_fValue = fValue;
        }

        //设置元素的值(数组)
        public virtual void SetChannelValue(float[] fValueArray)
        {
            m_fValueArray = fValueArray;
        }

        //设置元素的值(数组)
        public virtual void SetChannelValue(float fPosition, float fValueArray)
        {
            //m_fValueArray = fValueArray;
        }
        #endregion

        public bool IsToolTipLoaded { get; set; }
        public ToolTipControl ToolTipControl { get; set; }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (!IsToolTip && !IsDesignMode)
            {
                if (!IsToolTipLoaded)
                {
                    if (null == ToolTipControl && null != _ScreenElement)
                    {
                        IsToolTipLoaded = true;
                        var parentID = _ScreenElement.ElementID;
                        //LoadScreen._DataContext.Load<t_Element>(LoadScreen._DataContext.GetT_ElementsByScreenIDQuery(screenID), LoadToolTipCallback, null);
                        var toolTipControlElement = new ElementDA().GetBy(-9999, parentID, "ToolTip");
                        if (null != toolTipControlElement
                            && Parent is Canvas)
                        {
                            var parent = Parent as Canvas;
                            ToolTipControl = new ToolTipControl(this);
                            ToolTipControl.Width = toolTipControlElement.Width.HasValue ? toolTipControlElement.Width.Value : 300d;
                            ToolTipControl.Height = toolTipControlElement.Height.HasValue ? toolTipControlElement.Height.Value : 200d;
                            ToolTipControl.Transparent = 100;
                            ToolTipControl.SetValue(Canvas.ZIndexProperty, 10000);
                            ToolTipControl.ScreenElement = toolTipControlElement;
                            ToolTipControl.ListElementProp = ElementPropertyDA.SelectByElementID(toolTipControlElement.ElementID);
                            ToolTipControl.ElementState = ElementSate.Save;
                            ToolTipControl.SetPropertyValue();
                            ToolTipControl.SetCommonPropertyValue();
                            parent.Children.Add(ToolTipControl);
                            SetToolTipPosition();

                            var childElements = new ElementDA().SelectBy(-9999, parentID, "ToolTip");
                            foreach (var childElement in childElements)
                            {
                                var poperties = ElementPropertyDA.SelectByElementID(childElement.ElementID);
                                OnLoadElement(ToolTipControl.ToolTipCanvas, childElement, ElementSate.Save, poperties);
                            }
                        }
                    }
                }
                else
                {
                    SetToolTipPosition();
                }
            }
            base.OnMouseEnter(e);
        }

        private void SetToolTipPosition()
        {
            if (null != ToolTipControl)
            {
                ToolTipControl.Visibility = Visibility.Visible;
                ToolTipControl.SetPosition();
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (!IsToolTip && !IsDesignMode
                && null != ToolTipControl)
            {
                ToolTipControl.Visibility = Visibility.Collapsed;
            }
            base.OnMouseLeave(e);
        }

        public bool Contains(Rect rect)
        {
            var margin = (Thickness)GetValue(MarginProperty);
            var x = Canvas.GetLeft(this) + margin.Left;
            var y = Canvas.GetTop(this) + margin.Top;
            var w = this.Width - margin.Left - margin.Right;
            var h = this.Height - margin.Top - margin.Bottom;
            return rect.Contains(new Point(x, y)) ||
                rect.Contains(new Point(x + w, y)) ||
                rect.Contains(new Point(x + w, y + h)) ||
                rect.Contains(new Point(x, y + h));
        }
    }

    public enum ElementSate
    {
        New, Save
    }

    public static class StringExtent
    {
        public static string Clone(this string src)
        {
            return string.Copy(src);
        }
    }

    /// <summary>
    /// 屏幕元素、包括Element对象和ListProperty
    /// </summary>
    public class ScreenElementObj
    {
        t_Element m_Element;
        /// <summary>
        /// 元素信息
        /// </summary>
        public t_Element Element
        {
            get { return m_Element; }
            set { m_Element = value; }
        }

        public MonitorControl ParentControl { get; set; }

        List<t_ElementProperty> m_ListElementProperty;
        /// <summary>
        /// 元素属性列表
        /// </summary>
        public List<t_ElementProperty> ListElementProperty
        {
            get { return m_ListElementProperty; }
            set { m_ListElementProperty = value; }
        }


        /// <summary>
        /// 复制对象
        /// </summary>
        /// <param name="obj"></param>
        public void ElementClone(MonitorControl obj, int mWidth, int mHeight)
        {
            this.ParentControl = obj.ParentControl;
             m_Element = new t_Element();
           t_Element m_Older= obj.ScreenElement;
            //ElementID
           m_Element.ElementName = m_Older.ElementName;
           m_Element.ControlID = m_Older.ControlID;
           //m_Element.ScreenX = m_Older.ScreenX;
           m_Element.ScreenX = Convert.ToInt32(obj.GetValue(Canvas.LeftProperty));
           m_Element.ScreenY = Convert.ToInt32(obj.GetValue(Canvas.TopProperty));
          // m_Element.ScreenY = m_Older.ScreenY;
           m_Element.ScreenY += Convert.ToInt16(obj.Height);
           m_Element.TxtInfo = m_Older.TxtInfo;
           m_Element.Width = mWidth;
           m_Element.Height = mHeight;
           m_Element.ImageURL = m_Older.ImageURL;
           m_Element.ForeColor = m_Older.ForeColor;
           m_Element.Font = m_Older.Font;
           m_Element.ChildScreenID = m_Older.ChildScreenID;
           m_Element.DeviceID = m_Older.DeviceID;
           m_Element.ChannelNo = m_Older.ChannelNo;
           m_Element.ScreenID = m_Older.ScreenID;
           m_Element.BackColor = m_Older.BackColor;
           m_Element.Transparent = m_Older.Transparent;
           m_Element.oldX = m_Older.oldX;
           m_Element.oldY = m_Older.oldY;
           m_Element.Method = m_Older.Method;
           m_Element.MinFloat = m_Older.MinFloat;
           m_Element.MaxFloat = m_Older.MaxFloat;
           m_Element.SerialNum = m_Older.SerialNum;
           m_Element.TotalLength = m_Older.TotalLength;
           m_Element.LevelNo = m_Older.LevelNo;
           m_Element.ComputeStr = m_Older.ComputeStr;
           m_ListElementProperty = new List<t_ElementProperty>();
            foreach(t_ElementProperty elePro in obj.ListElementProp)
            {
                t_ElementProperty m_elePro = new t_ElementProperty();
                m_elePro.PropertyNo = elePro.PropertyNo;
                m_elePro.PropertyValue = elePro.PropertyValue;
                m_elePro.Caption = elePro.Caption;
                m_elePro.PropertyName = elePro.PropertyName;
                m_ListElementProperty.Add(m_elePro);
            }
        }
    }

    //#region 监组组态的共公数据结构
    //public class ControlPropertyObj
    //{
    //    public int PropertyNo;
    //    public string PropertyName;
    //    public string DefaultValue;
    //    public string Caption;
    //    public int ControlID;
    //}

    //public class UserObj
    //{
    //    public int UserID;
    //    public string UserPSW;
    //    public string UserName;
    //    public int UserType;
    //}

    //public class ControlObj
    //{
    //    public int ControlID;
    //    public string ControlName;
    //    public int ControlType;
    //    public string ImageURL;
    //    public string ControlTypeName;
    //    public string ControlCaption;
    //    public IList<ControlPropertyObj> m_listProperty = new List<ControlPropertyObj>();//控件的所有属性
    //}

    //public class ElementPropertyObj
    //{
    //    public int PropertyNo;
    //    public string PropertyValue;
    //    public string PropertyName;
    //    public string Caption;
    //    public int m_State;//0,没修改过,1修改过
    //    public int ElementID;
    //}
    ////public class ElementObj
    ////{
    ////    public int ElementID;
    ////    public int ScreenID;
    ////    public int Width;
    ////    public int Height;
    ////    public int ScreenX;
    ////    public int ScreenY;
    ////    // 2009-7-27
    ////    public string ChildScreenID;
    ////    public string TxtInfo;
    ////    public string ImageURL;
    ////    public string ForeColor;
    ////    public string BackColor;
    ////    // 2008-11-30:
    ////    public int Transparent;
    ////    // 2009-4-20
    ////    public int oldX;
    ////    public int oldY;

    ////    //2011-3-7 xxy增加
    ////    public int Method;
    ////    public float MinFloat;
    ////    public float MaxFloat;
    ////    public int SerialNum;
    ////    public float TotalLength;
    ////    //2011-5-30: zeng   
    ////    public int LevelNo;
    ////    // 2011-12-7:
    ////    public string ComputeStr;

    ////    public string Font;
    ////    public string ElementName;
    ////    public IList<ElementPropertyObj> m_listProperty = new List<ElementPropertyObj>();
    ////    public int DeviceID;
    ////    public int ChannelNo;
    ////    //父类Control的属性,都入到这
    ////    public int ControlID;
    ////    public string ControlName;
    ////    public string ControlCaption;
    ////    public int ControlType;
    ////    public MonitorControl m_MonitorControl;//界面显示，对应的控件实体

    ////    public int m_State;//0,没修改,1：修改,2:添加,3,删除

    ////    public static int STATE_NONE = 0;
    ////    public static int STATE_MODIFY = 1;
    ////    public static int STATE_ADD = 2;
    ////    public static int STATE_DELETE = 3;

    ////    public static int CONTROLTYPE_TP = 1;//拓朴控件
    ////    public static int CONTROLTYPE_MONITOR = 2;//组态控件
    ////    public static int CONTROLTYPE_COMMON = 3;//组态控件


    ////    //public int m_IsShowContextMenu = false;

    ////    //深度复制
    ////    public static ElementObj ElementObjClone(ElementObj oldElement)
    ////    {

    ////        ElementObj newElement = new ElementObj();
    ////        //newElement.ElementID = oldElement.ElementID;
    ////        newElement.ScreenID = oldElement.ScreenID;
    ////        newElement.Width = oldElement.Width;
    ////        newElement.Height = oldElement.Height;
    ////        newElement.ScreenX = oldElement.ScreenX;
    ////        newElement.ScreenY = oldElement.ScreenY;
    ////        newElement.ChildScreenID = oldElement.ChildScreenID;
    ////        newElement.TxtInfo = oldElement.TxtInfo.Clone().ToString();
    ////        newElement.ImageURL = oldElement.ImageURL.Clone().ToString();
    ////        newElement.Font = oldElement.Font.Clone().ToString();
    ////        newElement.ForeColor = oldElement.ForeColor.Clone().ToString();
    ////        newElement.BackColor = oldElement.BackColor.Clone().ToString();

    ////        // 2008-11-30:
    ////        newElement.Transparent = oldElement.Transparent;

    ////        newElement.ElementName = oldElement.ElementName;
    ////        newElement.DeviceID = oldElement.DeviceID;
    ////        newElement.ChannelNo = oldElement.ChannelNo;
    ////        //父类Control的属性,都入到这
    ////        newElement.ControlID = oldElement.ControlID;
    ////        newElement.ControlName = oldElement.ControlName.Clone().ToString();
    ////        newElement.ControlType = oldElement.ControlType;



    ////        foreach (ElementPropertyObj pObj in oldElement.m_listProperty)
    ////        {
    ////            ElementPropertyObj newProperty = new ElementPropertyObj();
    ////            newProperty.m_State = ElementObj.STATE_ADD;
    ////            newProperty.PropertyName = pObj.PropertyName.Clone().ToString();
    ////            newProperty.PropertyNo = pObj.PropertyNo;

    ////            newProperty.Caption = pObj.Caption;
    ////            newProperty.PropertyValue = pObj.PropertyValue.Clone().ToString();
    ////            newProperty.ElementID = newElement.ElementID;
    ////            newElement.m_listProperty.Add(newProperty);

    ////        }
    ////        return newElement;
    ////    }


    ////    //从控件生成元素

    ////    public static ElementObj CreateByControl(ControlObj myControlObj)
    ////    {

    ////        ElementObj newElement = new ElementObj();
    ////        newElement.ElementID = 0;
    ////        newElement.ScreenID = 0;
    ////        newElement.Width = 20;
    ////        newElement.Height = 20;
    ////        newElement.ScreenX = 0;
    ////        newElement.ScreenY = 0;
    ////        // 2009-7-27
    ////        newElement.ChildScreenID = "0";
    ////        newElement.TxtInfo = "";
    ////        newElement.ForeColor = "RGB(0,0,0)";
    ////        newElement.BackColor = "RGB(0,0,0)";
    ////        // 2008-11-30:
    ////        newElement.Transparent = 0;

    ////        newElement.ImageURL = myControlObj.ImageURL;
    ////        newElement.Font = "宋体";
    ////        newElement.ElementName = myControlObj.ControlName;
    ////        newElement.DeviceID = -1;
    ////        newElement.ChannelNo = -1;
    ////        //父类Control的属性,都入到这
    ////        newElement.ControlID = myControlObj.ControlID;
    ////        newElement.ControlName = myControlObj.ControlName;
    ////        newElement.ControlType = myControlObj.ControlType;


    ////        foreach (ControlPropertyObj pObj in myControlObj.m_listProperty)
    ////        {
    ////            ElementPropertyObj newProperty = new ElementPropertyObj();
    ////            newProperty.m_State = ElementObj.STATE_ADD;
    ////            newProperty.PropertyName = pObj.PropertyName;
    ////            newProperty.PropertyNo = pObj.PropertyNo;
    ////            newProperty.ElementID = newElement.ElementID;
    ////            newProperty.PropertyValue = pObj.DefaultValue;
    ////            newProperty.Caption = pObj.Caption;
    ////            newElement.m_listProperty.Add(newProperty);
    ////        }
    ////        return newElement;
    ////    }
    ////    //设置状态
    ////    public void SetState(int newState)
    ////    {
    ////        if (this.m_State == ElementObj.STATE_DELETE) return;
    ////        if (this.m_State == ElementObj.STATE_ADD && newState == STATE_MODIFY)
    ////            return;

    ////        this.m_State = newState;

    ////        //if (newState == 2 && this.m_State == 1) return;//添加时不要转换到修改
    ////        //this.m_State = newState;

    ////    }
    ////}

    /////// <summary>
    /////// 2009-10-13
    /////// </summary>
    ////public class StationObj
    ////{
    ////    public int StationID;
    ////    public string StationName;
    ////    public string IP;
    ////    public int Port;
    ////}

    ////public class ScreenObj
    ////{
    ////    private int m_ScreenID;

    ////    public int ScreenID
    ////    {
    ////        get { return m_ScreenID; }
    ////        set { m_ScreenID = value; }
    ////    }
    ////    public int ParentScreenID;
    ////    private string m_ScreenName;

    ////    public string ScreenName
    ////    {
    ////        get { return m_ScreenName; }
    ////        set { m_ScreenName = value; }
    ////    }
    ////    public string ImageURL;
    ////    public int StationID;
    ////    public IList<ElementObj> m_listElement = new List<ElementObj>();//本场景的所有元素
    ////}

    //#endregion
    //#region 别一些共公数据(配置程序时)
    //public class AlarmLogObj
    //{
    //    public int StationID;
    //    public string EventsName;
    //    public int AlarmLogID;
    //    public string DeviceName;
    //    public string Content;
    //    public DateTime HappenTime;
    //    public string RelieveTime;
    //    public string UserName;
    //    public int AlarmLevel;
    //    public int DeviceID;
    //    public float MonitorValue;
    //    public int OperateUserID;
    //    public string LastTime;
    //    // 2011-10-11
    //    public string AlarmType;
    //}

    //public class AlarmActionObj
    //{
    //    public int nActionID;
    //    public string strActionModule;
    //    public string strActionCommand;
    //    public string strActionParam;
    //    public string strActionName;
    //}

    //public class PolicyAction
    //{
    //    public int PolicyID;
    //    public int Id;
    //    public string AlarmMethod;
    //    public string VoiceFile;
    //    public string Target;
    //    public string AlarmTime;

    //    public int AlarmLevel;
    //}

    //public class AlarmTimeObj
    //{
    //    public int AlarmTimeID;
    //    public string AlarmTimeName;
    //    public string AlarmBeginTime;
    //    public string AlarmEndTime;
    //}


    //public class AlarmPolciyObj
    //{
    //    public int nPolicyID;
    //    public string strPolicyName;
    //    public string strPolicyContent;
    //    public int nAlarmLevel;
    //    public int nDeviceTypeID;
    //    public IList<int> PolicyDeviceMap = new List<int>();
    //    // public string strPolicyDistribute;
    //    public int nStationID;
    //    // public string strPolicyAction;
    //    // public IList<int> PolicyActionMap = new List<int>();
    //    public IList<PolicyAction> PolicyActionMap = new List<PolicyAction>();

    //    public string strPolicyFormula;
    //    public int nTimeInterval = 6;
    //}

    //public class ChannelTypeObj
    //{
    //    public int nDeviceTypeID;
    //    public int nChannelNo;
    //    public string strChannelTypeName;
    //    public string strOperateCommand;
    //    public string strOperateParam;
    //    public int nValueType;
    //    public string strValue0_Name;
    //    public string strValue1_Name;

    //}

    //public class DeviceMoubusType
    //{
    //    public int nStationID;
    //    public string nStationName;
    //    public int nDeviceID;
    //    public int nDeviceTypeID;
    //    public string nDeviceName;
    //    public int nCommunicate;
    //    public int? nChannelNo;
    //    public string nChannelName;
    //    public string nAddr;
    //    public string nSendStr;
    //    public int? nOrderType;
    //    public string nAnalyTemp;
    //    public int? nSendDelay;
    //    public string nSendType;
    //    public int? nRetryTimes;
    //    public float nMonitorValue;
    //    public string nUnit;
    //    public int LenCheck;
    //}

    //// 2011-9-15::SNMP相关
    //public class Snmp_Send
    //{
    //    public int SendDelay;
    //    public int RetryTimes;
    //    public string OID;
    //    public string MethodType;
    //    public string VersionNo;
    //    public int DeviceTypID;

    //}

    //public class Snmp_Recv
    //{
    //    public int DeviceTypID;
    //    public int SendSnmpID;
    //    public int ChannnelNO;
    //    public string ChannelName;
    //    public string OID;
    //    public string ValueType;
    //    public string Unit;
    //    public string Param;
    //    public string Info;
    //    public string ComputeStr;
    //}

    //public class DeviceTypeObj
    //{

    //    public int nDeviceTypeID;
    //    public int SaveTimeInteval;//设备采集的时间间隔  
    //    public int DeviceTypeID
    //    {
    //        get { return nDeviceTypeID; }
    //        set { nDeviceTypeID = value; }
    //    }
    //    public string strTypeName;

    //    public string TypeName
    //    {
    //        get { return strTypeName; }
    //        set { strTypeName = value; }
    //    }
    //    //2011-1-4曾巍  添加ip,param
    //    public string ip;

    //    public string IP
    //    {
    //        get { return ip; }
    //        set { ip = value; }
    //    }
    //    public string param;

    //    public string Param
    //    {
    //        get { return param; }
    //        set { param = value; }
    //    }
    //    //2011-2-21曾巍  添加stationid
    //    public int stationid;

    //    public int StationID
    //    {
    //        get { return stationid; }
    //        set { stationid = value; }
    //    }

    //    public string strParseDLL;
    //    public IList<int> PolicyDevicTypeeMap = new List<int>();
    //    public IList<PolicyAction> PolicyActionMap = new List<PolicyAction>();
    //}

    //public class SerialPortObj
    //{
    //    public int nSerialPortID;

    //    public int SerialPortID
    //    {
    //        get { return nSerialPortID; }
    //        set { nSerialPortID = value; }
    //    }
    //    public string strSerialPortName;

    //    public string SerialPortName
    //    {
    //        get { return strSerialPortName; }
    //        set { strSerialPortName = value; }
    //    }
    //    public string strCommunicationParam;
    //    public int nScanInterval;
    //    public int stationID;
    //    public int StationID
    //    {
    //        get { return stationID; }
    //        set { stationID = value; }
    //    }
    //    public string stationName;
    //    public string StationName
    //    {
    //        get { return stationName; }
    //        set { stationName = value; }
    //    }
    //}

    ////对象类
    //public class DeviceObj
    //{
    //    public int nDeviceID;

    //    public int DeviceID
    //    {
    //        get { return nDeviceID; }
    //        set { nDeviceID = value; }
    //    }
    //    public string strDeviceName;

    //    public string DeviceName
    //    {
    //        get { return strDeviceName; }
    //        set { strDeviceName = value; }
    //    }
    //    public int nStationID;
    //    public int nCommunicateType;
    //    public int nCommunicateID;
    //    public string strSubAddr;
    //    public int nDeviceTypeID;
    //    public string strParseDLL;
    //    public string strTypeName;

    //    public string UserId;
    //}

    ////在combo中用get set
    //public class ChannelObj
    //{
    //    public int nDeviceID;
    //    public int nChannelNo;
    //    public string strChannelParam;
    //    public int ChannelNo
    //    {
    //        get { return nChannelNo; }
    //        set { nChannelNo = value; }
    //    }
    //    public string strChannelName;

    //    public string ChannelName
    //    {
    //        get { return strChannelName; }
    //        set { strChannelName = value; }
    //    }
    //    public string strOperateCommand;
    //    public string strOperateParam;
    //    public int nValueType;
    //    public string strValue0_Name;
    //    public string strValue1_Name;
    //    public string strChannelTypeName;
    //    public int nDeviceTypeID;
    //    public float fCurrentValue;
    //}



    //public class DevcieDetailObj
    //{
    //    public DeviceObj m_DeviceObj;
    //    public IList<ChannelObj> m_listChannelObj = new List<ChannelObj>();

    //}

    //public class SchedulingObj
    //{
    //    public int Frequency;
    //    public string FrequencyName;
    //    public string StartTime;
    //    public string EndTime;
    //    public int StationID;
    //}
    //public class AlarmGroupsObj
    //{
    //    public int ID;
    //    public int AlarmGroupsID;
    //    public string GroupName;
    //    public int StationID;
    //}
    //public class AlarmGroupsMemberObj
    //{
    //    public int ID;
    //    public int AlarmGroupsID;
    //    public string Name;
    //    public string MobileNo;
    //    public string TelNo;
    //    public string Email;
    //    public int Scheduling;
    //    public int ProcessLevel;
    //    public int StationID;
    //}
    //public class AlarmLevelSetObj
    //{
    //    public int ID;
    //    public string LevelName;
    //    public string Priority;
    //    public string UpInterval;
    //    public int StationID;
    //}
    //public class LinkageSetObj
    //{
    //    public int ID;
    //    public int StationID;
    //    public int DeviceID;
    //    public int ChannelNo;
    //    public int ValueType;
    //    public float MaxTriggerValue;
    //    public float MinTriggerValue;
    //    public int SwitchTriggerValue;
    //    public int LinkageStationID;
    //    public int LinkageDeviveID;
    //    public int LinkageChannelNo;
    //    public float LinkageMaxValue;
    //    public float LinkagMinValue;
    //    public int LinkageSwitchValue;
    //}
    //public class LinkageTimeSetObj
    //{
    //    public int ID;
    //    public int LinkageID;
    //    public int LinkageStationID;
    //    public string TriggerTime;
    //    public int LinkageDeviceID;
    //    public int LinkageChannelNo;
    //}
    //public class AlramBindTimeObj
    //{
    //    public int ID;
    //    public int StationID;
    //    public int DeviceID;
    //    public int ChannelNo;
    //    public int IntervalTime;
    //}
    //public class MainteObj
    //{
    //    public int ID;
    //    public int DeviceID;
    //    public int StationID;
    //    public string ContentMainte;
    //    public string MainteName;
    //    public int ValueType;
    //    public string MainteTime;
    //    public string Duty;
    //}
    //public class GroupsObj
    //{
    //    public int AlarmGroupsID;
    //    public string GroupName;
    //    public int StationID;
    //}
    //public class GenChannelTypeObj
    //{
    //    public int nDecID;
    //    public int nDeviceTypeID;
    //    public int nSendStrID;
    //    public string nDecStr;
    //    public int nChannelNO;
    //    public string nChannelName;
    //    public int nValueType;
    //    public string nParam;
    //    public string nValue0Name;
    //    public string nValue1Name;
    //    public string nUnit;
    //}

    //// 2011-9-23
    //public class SnmpChannelTypeObj
    //{
    //    public int nDecID;
    //    public int nDeviceTypeID;
    //    public int SendSnmpID;
    //    public string OID;
    //    public int nChannelNO;
    //    public string nChannelName;
    //    public int nValueType;
    //    public string nParam;
    //    public string nUnit;
    //}
    //#endregion

    //#region 系统参数

    //public class MonitorSystemParam
    //{
    //    public static int StartScreenID = 0;
    //    // 2009-10-7
    //    public static bool AutoStartVideo = false;
    //    public static int VideoScreenID = 0;
    //    public static int AlarmLogShowLine = 10;
    //    public static int AlarmLogTime = 5;
    //    public static int MonitorRefreshTime = 5;
    //    public static int ServerPort = 0;
    //    public static String ServerIP = "";
    //    // 2011-11-04
    //    public static int ControlPort = 0;

    //    public static string Door_Sysid = "01";
    //    public static int Door_Com = 1;
    //    public static int HaveDoor = 0;

    //    // 最大报警层次数，报警时依次从1报到AlarmMaxLevel
    //    public static int AlarmMaxLevel = 1;

    //    public static int UserID = 1;
    //    public static string PSW = "";

    //    // 2011-2-17::增加网络调试日志
    //    public static int NetDebug = 0;

    //    public static int g_srcScreenX = 1024;
    //    public static int g_srcSscreenY = 768;


    //    public static int g_dstScreenX = 1024;
    //    public static int g_dstSscreenY = 768;

    //    // 2011-10-12
    //    public static int g_curUserType = -1;
    //    // 2011-10-19:
    //    public static int g_IsLog = 0;


    //}

    //public class MonitorSystemConfig
    //{
    //    public static string SplashScreen = "";
    //    public static string CompanyName = "";
    //    public static string SoftwareName = "";
    //    public static string SoftwareVersion = "";
    //    public static string SoftwareDescription = "";
    //    public static string SoftwareState = "";
    //    public static string connString = "";
    //    public static string dbType = "";
    //    public static string mysqlconnString = "";
    //}


    //public class SystemConfigManager
    //{
    //    public void GetSystemConfig()
    //    {
    //        // Modify by ab in 2012-04-12
    //        // TODO : 
    //        var xmlPath = new Uri(App.Current.Host.Source, "../SystemConfig.xml").ToString();

    //        #region unused code
    //        ////string xmlPath = "..\\..\\Design\\SystemConfig.xml";
    //        //string xmlPath = "\\SystemConfig.xml";
    //        //xmlPath = Environment.SystemDirectory.ToString() + xmlPath;
    //        //XmlDocument m_xml = new XmlDocument();
    //        //XmlTextReader m_reader = new XmlTextReader(xmlPath);

    //        //m_xml.Load(m_reader);
    //        ////ControlObj m_obj = new ControlObj();

    //        //// MonitorSystemConfig m_config = new MonitorSystemConfig();
    //        //XmlNode parent = m_xml.ChildNodes[1];
    //        //XmlNode splash = parent.ChildNodes[0];
    //        //MonitorSystemConfig.SplashScreen = splash.InnerText;
    //        //XmlNode software = parent.ChildNodes[1];
    //        //XmlNode company = software.ChildNodes[0];
    //        //XmlNode companyname = company.ChildNodes[0];
    //        //MonitorSystemConfig.CompanyName = companyname.InnerText;
    //        //XmlNode version = software.ChildNodes[1];
    //        //XmlNode name = software.ChildNodes[2];
    //        //XmlNode description = software.ChildNodes[3];
    //        //XmlNode state = software.ChildNodes[4];
    //        //MonitorSystemConfig.SoftwareVersion = version.InnerText;
    //        //MonitorSystemConfig.SoftwareName = name.InnerText;
    //        //MonitorSystemConfig.SoftwareDescription = description.InnerText;
    //        //MonitorSystemConfig.SoftwareState = state.InnerText;

    //        //XmlNode conn = parent.ChildNodes[2];
    //        //// 2011-10-17::修改读取配置文件格式
    //        //if (parent.ChildNodes.Count > 5)
    //        //{
    //        //    XmlNode tmpconn = parent.ChildNodes[5];
    //        //    if (tmpconn.Name.IndexOf("connSystem") > -1)
    //        //        conn = tmpconn;
    //        //}
    //        //MonitorSystemConfig.connString = conn.InnerText;

    //        //XmlNode db = parent.ChildNodes[3];
    //        //if (db == null)
    //        //{
    //        //    MessageBox.Show("请将V4.5以上的SystemConfig.xml文件放到系统system32目录下!", "错误!");
    //        //    return;
    //        //}
    //        //else
    //        //    MonitorSystemConfig.dbType = db.InnerText;

    //        //XmlNode mysql = parent.ChildNodes[4];
    //        //MonitorSystemConfig.mysqlconnString = mysql.InnerText;
    //        ////return m_config;  
    //        #endregion
    //        // end modify
    //    }
    //}
    //#endregion

    //#region 网络通信的指令

    //public static class NetCommandType
    //{
    //    public const int NC_GET_CHANNEL = 0x20;//请求通道值
    //    public const int NC_ACK_CHANNEL = 0x21; //返回通道值
    //    public const int NC_CONTROL_CHANNEL = 0x22; //控制通道

    //    public const int NC_GET_ALL_HISTORY = 0x30; //所有通道的历史值
    //    public const int NC_ACK_HISTORY = 0x31; //返回历史值


    //    public const int NC_SET_CHANNEL = 0x40; //设置通道值
    //    public const int NC_ACK_SET_CHANNEL = 0x41;//设置回应

    //    public const int NC_ACK_ALARM = 0x50;//设置回应
    //}

    ////单个通道内容
    //public struct tagChannel
    //{
    //    public Int32 nDeviceID;
    //    public Int32 nChannelNo;
    //    public Int32 nTime;
    //    public float fValue;
    //}

    ////多个通道参数指令
    //public struct tagChannelCommand
    //{
    //    public byte cType;
    //    public Int32 nStationID;
    //    public Int32 nScreenID;
    //    public Int32 nCount;
    //    public IList<tagChannel> listCMD;

    //}


    ////ascii码指令
    //public struct tagTextCommand
    //{
    //    public byte cType;
    //    public Int32 nStationID;
    //    public string m_text;
    //}

    //public struct tagControlCMD
    //{
    //    public Int32 nDeviceID;
    //    public Int32 nChannelNo;
    //    public Int32 cmdtype;
    //    public Int32 cmdparam;
    //};

    //#endregion

    //#region 报警设置相关数据结构

    //public class TargetData
    //{
    //    public int Id;
    //    public string Name;
    //    public string MobileNo;
    //    public string TelNo;
    //    public int ParentId;
    //}

    //public class AlarmMethod
    //{
    //    public int Id;
    //    public string Name;
    //    public string Type;
    //    public int TargetId;

    //}

    //public class AlarmGroupStruct
    //{
    //    public int Id;
    //    public string Name;
    //    public string MethodId;
    //    public string MethodName;

    //}

    //#endregion

    //#region 服务器参数结构
    //public class ServerParam
    //{
    //    public string smsComPort;
    //    public string smsComPortSetting;
    //    public string stationId;
    //    public int saveInterval;
    //    public int listenPort;
    //    public string smstype;
    //    public string smsCenterNo;
    //    public int stationID;
    //    public string stationName;
    //    public string nremoteip;
    //    public string nremotereport;
    //    public int IsCheck;
    //    public int IsSmsCheck;
    //}
    //#endregion
    //#region 联动相关数据结构
    //public class RelationAction
    //{
    //    public Int32 StationId = -1;
    //    public IList<RelationActionEach> rel = new List<RelationActionEach>();
    //}

    //public class RelationActionEach
    //{
    //    public Int32 ID = -1;

    //    public Int32 SourceDeviceID = -1;  // 联动源设备
    //    public Int32 SourceChannelNo = -1; // 联动源通道
    //    public float StartValue = 0.0f; // 联动开始动作的值
    //    public float StopValue = 0.0f;  // 联动结束动作的值

    //    public float LastValue = -2; // 


    //    public string ActionDevType = "video"; // 联动目的设备类型，暂时只支持视频设备,即video
    //    public string ActionDevParam = "start"; // 联动动作参数

    //    public Int32 DestDeviceID = -1;  // 联动目的设备
    //    public Int32 DestChannelNo = -1; // 联动目的通道

    //    // 2010-11-15
    //    public DateTime RecingTime = DateTime.Now;
    //   // public System.Threading.Mutex mutex = new System.Threading.Mutex(false);
    //    public bool IsRecing = false;
    //}
    //#endregion

   
}
