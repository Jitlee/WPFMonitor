using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using WPFMonitor.Model.ZTControls;
using WPFMonitor.View.TPControls;
using WPFMonitor.DAL.ZTControls;
using System.Collections.ObjectModel;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Core.TPControls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace WPFMonitor.Core.ViewModel.TPControls
{
    class ScreenViewModel : EditBaseViewModel
    {
        ScreenEdit _Window;
        t_Screen _ParentScreen;//所属场景
        t_Screen _ScreenObj;
        public ScreenViewModel(t_Screen _mScreen, OpType mType, ScreenEdit mWindow)//ScreenEdit _mw)
        {
            OperationType = mType;
            if (mType == OpType.Add)
            {
                _ParentScreen = _mScreen;
                ScreenObj = new t_Screen();
                _ScreenObj = new t_Screen();
            }
            else
            {
                ScreenObj = new t_Screen();
                ScreenObj.Clone(_mScreen);
                _ScreenObj = _mScreen;
            }
            _Window = mWindow;
            //UpdatetxtSource(_Window.gridContent);
            Init();

        }

        /// <summary>
        /// 当前站点
        /// </summary>
        public t_Screen ScreenObj { get; set; }

        ObservableCollection<StationOR> _StationORList = new ObservableCollection<StationOR>();
        public ObservableCollection<StationOR> StationORList
        {
            set { _StationORList = value; NotifyPropertyChanged("StationORList"); }
            get { return _StationORList; }
        }
        public StationOR SelectStationOR { get; set; }

        private void Init()
        {
            StationORList = new ObservableCollection<StationOR>();
            StationDA _staDA = new StationDA();
            var v = _staDA.selectAllStation();
            foreach (StationOR obj in v)
            {

                StationORList.Add(obj);
                if (OperationType == OpType.Alert)
                {
                    if (obj.Stationid == ScreenObj.StationID)
                        SelectStationOR = obj;
                }
            }
        }

        private ICommand _SelectImageCommand;
        public ICommand SelectImageCommand
        {
            get
            {
                if (null == this._SelectImageCommand)
                {
                    this._SelectImageCommand = new DelegateCommand(this.SelectImage_Command);
                }
                return this._SelectImageCommand;
            }
        }

        protected void SelectImage_Command()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "选择背景图片";
            open.Multiselect = false;
            if(System.IO.Directory.Exists(GlobalData.GetAppBgPath()))
            open.InitialDirectory = GlobalData.GetAppBgPath();
            else
                open.InitialDirectory = GlobalData.GetStartpath();

            open.Filter = "图片文件(*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp|所有文件|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                SetScreenImg(open.FileName);
                ScreenObj.ImageURL = open.FileName.Replace(GlobalData.GetAppBgPath(),"");
            }
        }

        public override void OK()
        {
            string errMsg = "";
            if (GetErrors(_Window.gridContent, out errMsg))
            {
                ShowMsgError(errMsg);
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(ScreenObj.ScreenName))
                sb.AppendLine("场景名称不能为空!");
            if (null == SelectStationOR)
            {
                sb.AppendLine("请选择站点!");                
            }
            if (string.IsNullOrEmpty(ScreenObj.ImageURL))
                sb.AppendLine("背景图片不能为空!");

            if (sb.ToString().Length > 0)
            {
                GlobalData.ShowMsgError(sb.ToString());
                return;
            }

            ScreenObj.StationID = SelectStationOR.Stationid;           
            
            try
            {
             
                if (OperationType == OpType.Alert)
                {
                    new ScreenDA().Update(ScreenObj);
                    _ScreenObj.Clone(ScreenObj);
                }
                else
                {
                    //参数初使化
                    if (_ParentScreen == null)
                        ScreenObj.ParentScreenID = 0;
                    else
                        ScreenObj.ParentScreenID = _ParentScreen.ScreenID;
                    ScreenObj.BackColor = "#FFFFFF";
                    ScreenObj.Flag = 0;
                    


                    ScreenDA mDA = new ScreenDA();
                    mDA.Insert(ScreenObj);
                    int InserID = mDA.GetMaxID();
                    t_Screen mobj = mDA.selectARowDate(InserID);
                    if (_ParentScreen == null)
                    {
                        ScreenTreeVM.Instance.Screens.Add(mobj);
                    }
                    else
                    {
                        _ParentScreen.Children.Add(mobj);
                    }
                    ScreenTreeVM.Instance.AllScreens.Add(mobj);
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }

        #region 图片设置

        double ImgWidth = 0.0;
        double ImgHeight = 0.0;

        public void SetScreenImg(string strImg)
        {
            
            var bitmap = new BitmapImage(new Uri(strImg, UriKind.Absolute));
          
            ImgWidth = bitmap.PixelWidth;
            ImgHeight = bitmap.PixelHeight;

            ScreenObj.Width = (int)ImgWidth;
            ScreenObj.Height = (int)ImgHeight;
        }

        

        #endregion

    }

}