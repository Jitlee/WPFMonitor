using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using WPFMonitor.Library.MonitorSystemGlobal;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;

namespace WPFMonitor.Library
{
    public class ScreenView : Canvas
    {
        #region 变量

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        #endregion

        #region 公共方法

        public void ScreenInit(t_Screen obj)
        {
            this.Children.Clear();
            RectangleGeometry r = new RectangleGeometry();
            Rect rect = new Rect();
            if (obj.Width != null && obj.Height != null)
            {
                this.Width = this.Width = rect.Width = obj.Width;
                this.Height = this.Height = rect.Height = obj.Height;
            }
            else
            {
                this.Width = this.Width = rect.Width = 1024;
                this.Height = this.Height = rect.Height = 768;
            }
            r.Rect = rect;
            this.Clip = r;

            SetScreenImg(obj.ImageURL);
        }

        public MonitorControl ShowElement(t_Element obj, ElementSate eleStae,
            List<t_ElementProperty> listObj)
        {
            Canvas canvas = this;
            try
            {
                if (obj.ImageURL != null && obj.ImageURL.IndexOf("MonitorSystem") == 0)
                {
                    MonitorControl instance = (MonitorControl)Activator.CreateInstance(Type.GetType(obj.ImageURL));
                    SetEletemt(canvas, instance, obj, eleStae, listObj);
                    return instance;
                }
                else
                {
                    switch (obj.ElementName)
                    {
                        case "MyButton":
                            TP_Button mtpButtom = new TP_Button();
                            SetEletemt(canvas, mtpButtom, obj, eleStae, listObj);
                            return mtpButtom;
                        //break;
                        //case "MonitorLine":
                        //    MonitorLine mPubLine = new MonitorLine();
                        //    SetEletemt(canvas, mPubLine, obj, eleStae, listObj);
                        //    return mPubLine;
                        ////break;
                        //case "MonitorText":
                        //    MonitorText mPubText = new MonitorText();
                        //    mPubText.MyText = obj.TxtInfo;
                        //    SetEletemt(canvas, mPubText, obj, eleStae, listObj);
                        //    return mPubText;
                        ////break;
                        //case "ColorText":
                        //    ColorText mColorText = new ColorText();
                        //    SetEletemt(canvas, mColorText, obj, eleStae, listObj);
                        //    return mColorText;
                        ////break;
                        //case "InputTextBox":
                        //    InputTextBox mInputTextBox = new InputTextBox();
                        //    mInputTextBox.MyText = obj.TxtInfo;
                        //    SetEletemt(canvas, mInputTextBox, obj, eleStae, listObj);
                        //    return mInputTextBox;
                        ////break;
                        //case "ButtonCtrl":
                        //    ButtonCtrl mButtonCtrl = new ButtonCtrl();
                        //    mButtonCtrl.MyText = obj.TxtInfo;
                        //    SetEletemt(canvas, mButtonCtrl, obj, eleStae, listObj);
                        //    return mButtonCtrl;
                        ////break;
                        //case "MonitorCur":
                        //    MonitorCur mPubCur = new MonitorCur();
                        //    SetEletemt(canvas, mPubCur, obj, eleStae, listObj);
                        //    return mPubCur;
                        ////break;
                        //case "MonitorRectangle":
                        //    MonitorRectangle mPubRec = new MonitorRectangle();
                        //    SetEletemt(canvas, mPubRec, obj, eleStae, listObj);
                        //    return mPubRec;
                        ////break;
                        //case "MonitorGrid":
                        //    MonitorGrid mPubGrid = new MonitorGrid();
                        //    SetEletemt(canvas, mPubGrid, obj, eleStae, listObj);
                        //    return mPubGrid;
                        ////break;
                        //case "FoldLine":
                        //    MonitorFoldLine mPubFoldLine = new MonitorFoldLine();
                        //    SetEletemt(canvas, mPubFoldLine, obj, eleStae, listObj);
                        //    return mPubFoldLine;
                        ////break;
                        //case "Temprary":
                        //    Temprary mTemprary = new Temprary();
                        //    SetEletemt(canvas, mTemprary, obj, eleStae, listObj);
                        //    return mTemprary;
                        //case "DLBiaoPan":
                        //    DLBiaoPan mDLBiaoPan = new DLBiaoPan();
                        //    obj.Width = 2 * obj.Height.Value;
                        //    SetEletemt(canvas, mDLBiaoPan, obj, eleStae, listObj);
                        //    return mDLBiaoPan;
                        //case "DigitalBiaoPan":
                        //    DigitalBiaoPan mDigitalBiaoPan = new DigitalBiaoPan();
                        //    SetEletemt(canvas, mDigitalBiaoPan, obj, eleStae, listObj);
                        //    return mDigitalBiaoPan;
                        //case "Switch":
                        //    Switch mSwitch = new Switch();
                        //    SetEletemt(canvas, mSwitch, obj, eleStae, listObj);
                        //    return mSwitch;
                        //case "SignalSwitch":
                        //    SignalSwitch mSignalSwitch = new SignalSwitch();
                        //    obj.Width = obj.Height;
                        //    SetEletemt(canvas, mSignalSwitch, obj, eleStae, listObj);
                        //    return mSignalSwitch;
                        //case "DetailSwitch":
                        //    DetailSwitch mDetailSwitch = new DetailSwitch();
                        //    SetEletemt(canvas, mDetailSwitch, obj, eleStae, listObj);
                        //    return mDetailSwitch;
                        //case "RealTimeCurve":
                        //    RealTimeCurve mRealTime = new RealTimeCurve();
                        //    SetEletemt(canvas, mRealTime, obj, eleStae, listObj);
                        //    return mRealTime;
                        //case "TableCtrl":
                        //    TableCtrl mTableCtrl = new TableCtrl();
                        //    SetEletemt(canvas, mTableCtrl, obj, eleStae, listObj);
                        //    return mTableCtrl;
                        //case "zedGraphCtrl":
                        //    zedGraphCtrl mzedGraphCtrl = new zedGraphCtrl();
                        //    SetEletemt(canvas, mzedGraphCtrl, obj, eleStae, listObj);
                        //    return mzedGraphCtrl;
                        //case "zedGraphLineCtrl":
                        //    zedGraphLineCtrl mzedGraphLineCtrl = new zedGraphLineCtrl();
                        //    SetEletemt(canvas, mzedGraphLineCtrl, obj, eleStae, listObj);
                        //    return mzedGraphLineCtrl;
                        //case "zedGraphPieCtrl":
                        //    zedGraphPieCtrl mzedGraphPieCtrl = new zedGraphPieCtrl();
                        //    SetEletemt(canvas, mzedGraphPieCtrl, obj, eleStae, listObj);
                        //    return mzedGraphPieCtrl;
                        //case "MyLine"://曲线
                        //    MyLine mMyLine = new MyLine();
                        //    SetEletemt(canvas, mMyLine, obj, eleStae, listObj);
                        //    return mMyLine;
                        //case "BackgroundRect"://背景
                        //    BackgroundRect mBackgroundRect = new BackgroundRect();
                        //    SetEletemt(canvas, mBackgroundRect, obj, eleStae, listObj);
                        //    return mBackgroundRect;
                        //case "PicBox"://窗口式背景控件
                        //    PicBox mPicBox = new PicBox();
                        //    SetEletemt(canvas, mPicBox, obj, eleStae, listObj);
                        //    return mPicBox;
                        //case "DrawLine"://窗口式背景控件
                        //    DrawLine mDrawLine = new DrawLine();
                        //    SetEletemt(canvas, mDrawLine, obj, eleStae, listObj);
                        //    return mDrawLine;
                        //case "ExtProControl"://窗口式背景控件
                        //    ExtProControl mExtProControl = new ExtProControl();
                        //    SetEletemt(canvas, mExtProControl, obj, eleStae, listObj);
                        //    return mExtProControl;
                        //case "DimorphismGraphCtrl"://窗口式背景控件
                        //    DimorphismGraphCtrl mDimorphismGraphCtrl = new DimorphismGraphCtrl();
                        //    SetEletemt(canvas, mDimorphismGraphCtrl, obj, eleStae, listObj);
                        //    return mDimorphismGraphCtrl;
                        //case "BackgroundControl":
                        //    BackgroundControl backgroundControl = new BackgroundControl();
                        //    SetEletemt(canvas, backgroundControl, obj, eleStae, listObj);
                        //    var childElements = LoadScreen._DataContext.t_Elements.Where(e => e.ParentID == obj.ElementID && e.ElementType == "Background").ToList();
                        //    ShowElements(childElements, backgroundControl.BackgroundCanvas, backgroundControl);
                        //    return backgroundControl;
                        default:
                            string url = string.Format("/WPFMonitor;component/Resources/Images/TPControls/{0}", obj.ImageURL);
                            BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                            ImageSource mm = bitmap;
                            TP mtp = new TP();
                            mtp.Source = mm;
                            SetEletemt(canvas, mtp, obj, eleStae, listObj);
                            return mtp;


                        //break;
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        
        #endregion

        #region 私有方法
        
        private void SetScreenImg(string strImg, bool resize = false)
        {
            var gbUrl = WPFMonitor.Common.GetAppPath("\\Upload\\ImageMap\\", strImg);
            var bitmap = new BitmapImage(new Uri(gbUrl, UriKind.RelativeOrAbsolute));

            var imgB = new ImageBrush() { Stretch = Stretch.UniformToFill };
            imgB.ImageSource = bitmap;
            this.Background = imgB;
        }

        private void ShowElements(List<t_Element> lsitElement, Canvas canvas, MonitorControl parentContol = null)
        {
            foreach (t_Element el in lsitElement)
            {
                var list = ElementPropertyDA.SelectByElementID(el.ElementID);
                var monitorControl = ShowElement(el, ElementSate.Save, list.ToList());

                // _ScreenView.ShowElement(canvas, el, ElementSate.Save, list.ToList());
                if (null != monitorControl && null != parentContol)
                {
                    monitorControl.ParentControl = parentContol;
                    monitorControl.AllowToolTip = false;
                    monitorControl.ClearValue(Canvas.ZIndexProperty);
                    if (null != monitorControl.AdornerLayer)
                    {
                        monitorControl.AdornerLayer.AllToolTip = false;
                    }
                }
            }
        }
        
        private void SetEletemt(Canvas canvas, MonitorControl mControl, t_Element obj, ElementSate eleStae,
           List<t_ElementProperty> listObj)
        {
            mControl.Selected += (o, e) =>
            {
                //PropertyMain.Instance.ControlPropertyGrid.SelectedObject = null;
                //PropertyMain.Instance.ControlPropertyGrid.BrowsableProperties = mControl.BrowsableProperties;
                //PropertyMain.Instance.ControlPropertyGrid.SelectedObject = mControl;
                MonitorControl.OnUpdatePropertyGrid(new string[0], null);
                MonitorControl.OnUpdatePropertyGrid(mControl.BrowsableProperties, mControl);
            };
            if (eleStae == ElementSate.Save)
            {
                mControl.Name = "wpft" + obj.ElementID.ToString();
            }
            mControl.ScreenElement = obj;
            mControl.ListElementProp = listObj;
            mControl.ElementState = eleStae;

            mControl.SetPropertyValue();
            mControl.SetCommonPropertyValue();
            //添加到场景
            canvas.Children.Add(mControl);
        }
        
        #endregion
    }
}
