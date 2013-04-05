using System;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Sys;
using WPFMonitor.View.Sys;

namespace WPFMonitor.Core.ViewModel
{
    public class StationEditViewModel : EditBaseViewModel
    {
        StationListViewModel _StationListVM;
        StationOR _SourceObj;
        StationEdit _Window;
        public StationEditViewModel(StationListViewModel _vm,StationEdit _mw)
        {
            _StationListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            StationObj = new StationOR();
            UpdatetxtSource(_Window.gridContent);
        }

        public StationEditViewModel(StationListViewModel _vm, StationEdit _mw, StationOR _StationObj)
        {
            _StationListVM = _vm;
            _Window = _mw;
            _SourceObj = _StationObj;

            OperationType = OpType.Alert;
            StationObj = new StationOR();
            StationObj.Clone(_StationObj);
        }

        /// <summary>
        /// 当前站点
        /// </summary>
        public StationOR StationObj { get; set; }
                 

        public StationEditViewModel(StationOR _Sta)
        {
            OperationType = OpType.Alert;
            StationObj = _Sta;
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
            try
            {
                if (OperationType == OpType.Alert)
                {
                    new StationDA().Update(StationObj);
                    _SourceObj.Clone(StationObj);
                }
                else
                {
                    new StationDA().Insert(StationObj);
                    _StationListVM.StationORList.Add(StationObj);
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }
            
        }
    }
}
