using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MonitorSystem.MonitorSystemGlobal;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.DAL.ZTControls;
using System.Collections.ObjectModel;
using System.IO;
using WPFMonitor;
using MonitorSystem.ZTControls;

namespace MonitorSystem.Other
{
    public partial class ScreenView : UserControl
    {
        private t_Screen _screen;
        public ScreenView()
        {
            InitializeComponent();
        }

        public  void ScreenInit(t_Screen obj)
        {
            _screen = obj;
            csScreen.Children.Clear();
            //RectangleGeometry r = new RectangleGeometry();
            //Rect rect = new Rect();
            csScreen.Width = obj.Width > 100 ? obj.Width : 1920;
            csScreen.Height = obj.Height > 100 ? obj.Height : 1024;
            //r.Rect = rect;
            //this.Clip = r;

            double sfPerw = 200 / csScreen.Width;
            double sfPerh = 150 / csScreen.Height;
            if (sfPerh < sfPerw)
                sfPerw = sfPerh;
            ScaleTransform mainShowCanvasScaleTrans = new ScaleTransform();
            mainShowCanvasScaleTrans.CenterX = 0.0;
            mainShowCanvasScaleTrans.CenterY = 0.0;
            mainShowCanvasScaleTrans.ScaleX = sfPerw;
            mainShowCanvasScaleTrans.ScaleY = sfPerw;
            csScreen.RenderTransform = mainShowCanvasScaleTrans;

            SetScreenImg(obj.ImageURL);
        }
        private void SetScreenImg(string strImg, bool resize = false)
        {
            var gbUrl = Common.GetAppPath("ImageMap", strImg);
            if (File.Exists(gbUrl))
            {
                var bitmap = new BitmapImage(new Uri(gbUrl, UriKind.Absolute));

                var imgB = new ImageBrush() { Stretch = Stretch.UniformToFill };
                imgB.ImageSource = bitmap;
                csScreen.Background = imgB;
            }
            else
            {
                var imgB = new ImageBrush();
                csScreen.Background = imgB;
            }
        }
      
        public MonitorControl ShowElement(t_Element obj, ElementSate eleStae,
            List<t_ElementProperty> listObj)
        {
            Canvas canvas = csScreen;
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
                            break;
                        case "MonitorLine":
                            MonitorLine mPubLine = new MonitorLine();
                            SetEletemt(canvas, mPubLine, obj, eleStae, listObj);
                            return mPubLine;
                        //break;
                        case "MonitorText":
                            MonitorText mPubText = new MonitorText();
                            mPubText.MyText = obj.TxtInfo;
                            SetEletemt(canvas, mPubText, obj, eleStae, listObj);
                            return mPubText;
                        //break;
                        case "ColorText":
                            ColorText mColorText = new ColorText();
                            SetEletemt(canvas, mColorText, obj, eleStae, listObj);
                            return mColorText;
                        //break;
                        case "InputTextBox":
                            InputTextBox mInputTextBox = new InputTextBox();
                            mInputTextBox.MyText = obj.TxtInfo;
                            SetEletemt(canvas, mInputTextBox, obj, eleStae, listObj);
                            return mInputTextBox;
                        //break;
                        case "ButtonCtrl":
                            ButtonCtrl mButtonCtrl = new ButtonCtrl();
                            mButtonCtrl.MyText = obj.TxtInfo;
                            SetEletemt(canvas, mButtonCtrl, obj, eleStae, listObj);
                            return mButtonCtrl;
                        //break;
                        case "MonitorCur":
                            MonitorCur mPubCur = new MonitorCur();
                            SetEletemt(canvas, mPubCur, obj, eleStae, listObj);
                            return mPubCur;
                        //break;
                        case "MonitorRectangle":
                            MonitorRectangle mPubRec = new MonitorRectangle();
                            SetEletemt(canvas, mPubRec, obj, eleStae, listObj);
                            return mPubRec;
                        //break;
                        case "MonitorGrid":
                            MonitorGrid mPubGrid = new MonitorGrid();
                            SetEletemt(canvas, mPubGrid, obj, eleStae, listObj);
                            return mPubGrid;
                        //break;
                        case "FoldLine":
                            MonitorFoldLine mPubFoldLine = new MonitorFoldLine();
                            SetEletemt(canvas, mPubFoldLine, obj, eleStae, listObj);
                            return mPubFoldLine;
                        //break;
                        case "Temprary":
                            Temprary mTemprary = new Temprary();
                            SetEletemt(canvas, mTemprary, obj, eleStae, listObj);
                            return mTemprary;
                        case "DLBiaoPan":
                            DLBiaoPan mDLBiaoPan = new DLBiaoPan();
                            obj.Width = 2 * obj.Height.Value;
                            SetEletemt(canvas, mDLBiaoPan, obj, eleStae, listObj);
                            return mDLBiaoPan;
                        case "DigitalBiaoPan":
                            DigitalBiaoPan mDigitalBiaoPan = new DigitalBiaoPan();
                            SetEletemt(canvas, mDigitalBiaoPan, obj, eleStae, listObj);
                            return mDigitalBiaoPan;
                        case "Switch":
                            Switch mSwitch = new Switch();
                            SetEletemt(canvas, mSwitch, obj, eleStae, listObj);
                            return mSwitch;
                        case "SignalSwitch":
                            SignalSwitch mSignalSwitch = new SignalSwitch();
                            obj.Width = obj.Height;
                            SetEletemt(canvas, mSignalSwitch, obj, eleStae, listObj);
                            return mSignalSwitch;
                        case "DetailSwitch":
                            DetailSwitch mDetailSwitch = new DetailSwitch();
                            SetEletemt(canvas, mDetailSwitch, obj, eleStae, listObj);
                            return mDetailSwitch;
                        case "RealTimeCurve":
                            RealTimeCurve mRealTime = new RealTimeCurve();
                            SetEletemt(canvas, mRealTime, obj, eleStae, listObj);
                            return mRealTime;
                        case "TableCtrl":
                            TableCtrl mTableCtrl = new TableCtrl();
                            SetEletemt(canvas, mTableCtrl, obj, eleStae, listObj);
                            return mTableCtrl;
                        case "zedGraphCtrl":
                            zedGraphCtrl mzedGraphCtrl = new zedGraphCtrl();
                            SetEletemt(canvas, mzedGraphCtrl, obj, eleStae, listObj);
                            return mzedGraphCtrl;
                        case "zedGraphLineCtrl":
                            zedGraphLineCtrl mzedGraphLineCtrl = new zedGraphLineCtrl();
                            SetEletemt(canvas, mzedGraphLineCtrl, obj, eleStae, listObj);
                            return mzedGraphLineCtrl;
                        case "zedGraphPieCtrl":
                            zedGraphPieCtrl mzedGraphPieCtrl = new zedGraphPieCtrl();
                            SetEletemt(canvas, mzedGraphPieCtrl, obj, eleStae, listObj);
                            return mzedGraphPieCtrl;
                        case "MyLine"://曲线
                            MyLine mMyLine = new MyLine();
                            SetEletemt(canvas, mMyLine, obj, eleStae, listObj);
                            return mMyLine;
                        case "BackgroundRect"://背景
                            BackgroundRect mBackgroundRect = new BackgroundRect();
                            SetEletemt(canvas, mBackgroundRect, obj, eleStae, listObj);
                            return mBackgroundRect;
                        case "PicBox"://窗口式背景控件
                            PicBox mPicBox = new PicBox();
                            SetEletemt(canvas, mPicBox, obj, eleStae, listObj);
                            return mPicBox;
                        case "DrawLine"://窗口式背景控件
                            DrawLine mDrawLine = new DrawLine();
                            SetEletemt(canvas, mDrawLine, obj, eleStae, listObj);
                            return mDrawLine;
                        case "ExtProControl"://窗口式背景控件
                            ExtProControl mExtProControl = new ExtProControl();
                            SetEletemt(canvas, mExtProControl, obj, eleStae, listObj);
                            return mExtProControl;
                        case "DimorphismGraphCtrl"://窗口式背景控件
                            DimorphismGraphCtrl mDimorphismGraphCtrl = new DimorphismGraphCtrl();
                            SetEletemt(canvas, mDimorphismGraphCtrl, obj, eleStae, listObj);
                            return mDimorphismGraphCtrl;
                        case "BackgroundControl":
                            BackgroundControl backgroundControl = new BackgroundControl();
                            SetEletemt(canvas, backgroundControl, obj, eleStae, listObj);
                            var childElements = new ElementDA().SelectBy(obj.ElementID, "Background");
                            ShowElements(childElements, backgroundControl.BackgroundCanvas, backgroundControl);
                            return backgroundControl;
                        default:
                            string url = string.Format("/WPFMonitor;component/Resources/Images/ControlsImg/{0}", obj.ImageURL);
                            BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.Relative));
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

        private void ShowElements(List<t_Element> lsitElement, Canvas canvas, MonitorControl parentContol = null)
        {
            List<t_ElementProperty> _elementProperties = new ElementPropertyDA().selectByScreenID(_screen.ScreenID);
            foreach (t_Element el in lsitElement)
            {
                var list = _elementProperties.Where(a => a.ElementID == el.ElementID);
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

        public void SetScale(ScaleTransform st)
        {
            csScreen.RenderTransform = st;
        }

        private void SetEletemt(Canvas canvas, MonitorControl mControl, t_Element obj, ElementSate eleStae,
           List<t_ElementProperty> listObj)
        {
            mControl.Selected += (o, e) =>
            {
                MonitorControl.UpdatePropertyGrid(mControl.BrowsableProperties, null);
                MonitorControl.UpdatePropertyGrid(mControl.BrowsableProperties, mControl);
            };
            if (eleStae == ElementSate.Save)
            {
                mControl.Name ="slt"+ obj.ElementID.ToString();
            }
            mControl.ScreenElement = obj;
            mControl.ListElementProp = listObj;
            mControl.ElementState = eleStae;

            mControl.SetPropertyValue();
            mControl.SetCommonPropertyValue();
            //添加到场景
            canvas.Children.Add(mControl);
        }
    
    }
}
