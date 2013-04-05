using System;
using System.Linq;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class SchedulingEditViewModel : EditBaseViewModel
    {
        SchedulingListViewModel _SchedulingListVM;
        SchedulingOR _SourceObj;
        SchedulingEditView _Window;
        public SchedulingEditViewModel(SchedulingListViewModel _vm, SchedulingEditView _mw)
        {
            _SchedulingListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            SchedulingObj = new SchedulingOR();
            //UpdatetxtSource(_Window.gridContent);
        }

        public SchedulingEditViewModel(SchedulingListViewModel _vm, SchedulingEditView _mw, SchedulingOR _SchedulingObj)
        {
            _SchedulingListVM = _vm;
            _Window = _mw;
            _SourceObj = _SchedulingObj;

            OperationType = OpType.Alert;
            SchedulingObj = new SchedulingOR();
            SchedulingObj.Clone(_SchedulingObj);
        }

        /// <summary>
        /// 当前站点
        /// </summary>
        public SchedulingOR SchedulingObj { get; set; }


        public SchedulingEditViewModel(SchedulingOR _Sta)
        {
            OperationType = OpType.Alert;
            SchedulingObj = _Sta;
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
                    new SchedulingDA().Update(SchedulingObj);
                    _SourceObj.Clone(SchedulingObj);
                }
                else
                {
                    new SchedulingDA().Insert(SchedulingObj);
		    //_SchedulingListVM.SchedulingORList.Add(SchedulingObj);
                    _SchedulingListVM.Init();
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

