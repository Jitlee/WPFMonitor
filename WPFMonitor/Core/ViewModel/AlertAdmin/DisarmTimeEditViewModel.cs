using System;
using System.Linq;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class DisarmTimeEditViewModel : EditBaseViewModel
    {
        DisarmTimeListViewModel _DisarmTimeListVM;
        DisarmTimeOR _SourceObj;
        DisarmTimeEditView _Window;
        public DisarmTimeEditViewModel(DisarmTimeListViewModel _vm, DisarmTimeEditView _mw)
        {
            _DisarmTimeListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            DisarmTimeObj = new DisarmTimeOR();
            //UpdatetxtSource(_Window.gridContent);
        }

        public DisarmTimeEditViewModel(DisarmTimeListViewModel _vm, DisarmTimeEditView _mw, DisarmTimeOR _DisarmTimeObj)
        {
            _DisarmTimeListVM = _vm;
            _Window = _mw;
            _SourceObj = _DisarmTimeObj;

            OperationType = OpType.Alert;
            DisarmTimeObj = new DisarmTimeOR();
            DisarmTimeObj.Clone(_DisarmTimeObj);
        }

        /// <summary>
        /// 当前站点
        /// </summary>
        public DisarmTimeOR DisarmTimeObj { get; set; }


        public DisarmTimeEditViewModel(DisarmTimeOR _Sta)
        {
            OperationType = OpType.Alert;
            DisarmTimeObj = _Sta;
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
                    new DisarmTimeDA().Update(DisarmTimeObj);
                    _SourceObj.Clone(DisarmTimeObj);
                }
                else
                {
                    new DisarmTimeDA().Insert(DisarmTimeObj);
                    _DisarmTimeListVM.Init();
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

