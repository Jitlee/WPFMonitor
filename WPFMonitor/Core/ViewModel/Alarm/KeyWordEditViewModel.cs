using System;
using System.Linq;
using WPFMonitor.Model.Alarm;
using WPFMonitor.DAL.Alarm;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Alarm;
using System.Collections.ObjectModel;
using System.Text;


namespace WPFMonitor.Core.ViewModel.Alarm
{
    public class KeyWordEditViewModel : EditBaseViewModel
    {
        KeyWordListViewModel _KeyWordListVM;
        KeyWordOR _SourceObj;
        KeyWordEditView _Window;
        public KeyWordEditViewModel(KeyWordListViewModel _vm, KeyWordEditView _mw)
        {
            _KeyWordListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            KeyWordObj = new KeyWordOR();

            Init();
        }

        public KeyWordEditViewModel(KeyWordListViewModel _vm, KeyWordEditView _mw, KeyWordOR _KeyWordObj)
        {
            _KeyWordListVM = _vm;
            _Window = _mw;
            _SourceObj = _KeyWordObj;

            OperationType = OpType.Alert;
            KeyWordObj = new KeyWordOR();
            KeyWordObj.Clone(_KeyWordObj);

            Init();
        }

        public void Init()
        {
            KeyWorkList = new ObservableCollection<string>() { "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}", "{7}",
                "{8}", "{9}","{10}","{11}","{12}","{13}","{14}"};

            DataItemList = new ObservableCollection<DataItem>()
            {
               new DataItem(){ ColumnText="设备地址", ColumnValue="SubAddr"},
new DataItem(){ ColumnText="设备编号", ColumnValue="DeviceID"},
new DataItem(){ ColumnText="设备名称", ColumnValue="DeviceName"},
new DataItem(){ ColumnText="设备类型编号", ColumnValue="DeviceTypeID"},
new DataItem(){ ColumnText="设备类型名称", ColumnValue="TypeName"},
new DataItem(){ ColumnText="机房编号", ColumnValue="StationID"},
new DataItem(){ ColumnText="机房名称", ColumnValue="StationName"},
new DataItem(){ ColumnText="测点编号", ColumnValue="ChannelNO"},
new DataItem(){ ColumnText="测点名称", ColumnValue="ChannelName"},
new DataItem(){ ColumnText="测点值", ColumnValue="ChannelValue"},
new DataItem(){ ColumnText="报警策略名称", ColumnValue="PolicyName"},
new DataItem(){ ColumnText="发生时间", ColumnValue="HappenTime"},
new DataItem(){ ColumnText="自定义", ColumnValue=""}
            };

             if (this.OperationType == OpType.Alert)
            {
                SelectKeyWord = KeyWordObj.Keyword;


                if (KeyWordObj.Iscustomize == 1)
                {
                    _Window.cbIsCustomize.IsChecked = true;
                    CustomizeReplace = KeyWordObj.Replace;
                }
                else
                {
                    _Window.cbIsCustomize.IsChecked = false;                   
                }
                string strReplace = KeyWordObj.Iscustomize == 1 ? "" : KeyWordObj.Replace;
                var v = from f in DataItemList where f.ColumnValue == strReplace select f;
                if (v.Count() > 0)
                    SelectDataItem = v.First();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public KeyWordOR KeyWordObj { get; set; }
        //关键字
        public ObservableCollection<string> KeyWorkList { get; set; }
        public string SelectKeyWord { get; set; }
        //关键字名称
        public ObservableCollection<DataItem> DataItemList { get; set; }
        public DataItem SelectDataItem { get; set; }

        public string CustomizeReplace { get; set; }

        public KeyWordEditViewModel(KeyWordOR _Sta)
        {
            OperationType = OpType.Alert;
            KeyWordObj = _Sta;
        }

        public bool SetValue()
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(SelectKeyWord))
            {
                sb.AppendLine("没有选择关键字；");
            }
            else
            {
                KeyWordObj.Keyword = SelectKeyWord;
            }
           
            
            if (_Window.cbIsCustomize.IsChecked.Value)
            {
                KeyWordObj.Keywordname = "自定义";
                KeyWordObj.Iscustomize = 1;
                KeyWordObj.Replace = CustomizeReplace;
            }
            else
            {
                if (SelectDataItem == null)
                {
                    sb.AppendLine("请选择关键字；");
                }
                else
                {
                    KeyWordObj.Keywordname = SelectDataItem.ColumnText;
                    KeyWordObj.Replace = SelectDataItem.ColumnValue;
                }
                KeyWordObj.Iscustomize = 0;
            }


            string errorMsg = string.Empty;
            errorMsg = sb.ToString();
            if (!string.IsNullOrEmpty(errorMsg))
            {
                ShowMsgError(errorMsg);
                return false;
            }
            return true;
        }

        public override void OK()
        {
            string errMsg = "";
            if (GetErrors(_Window.gridContent, out errMsg))
            {
                ShowMsgError(errMsg);
                return;
            }
            //
            if (!SetValue())
                return;

            try
            {
                if (OperationType == OpType.Alert)
                {
                    new KeyWordDA().Update(KeyWordObj);
                    _SourceObj.Clone(KeyWordObj);
                }
                else
                {
                    new KeyWordDA().Insert(KeyWordObj);
                    //_KeyWordListVM.KeyWordORList.Add(KeyWordObj);
                    _KeyWordListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }

        }
    }

    public class DataItem
    {
        public string ColumnText { get; set; }
        public string ColumnValue { get; set; }
    }
}

