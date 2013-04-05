using System;
using System.Linq;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using System.Collections.ObjectModel;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class AlarmLevelSetEditViewModel : EditBaseViewModel
    {
        AlarmLevelSetListViewModel _AlarmLevelSetListVM;
        AlarmLevelSetOR _SourceObj;
        AlarmLevelSetEditView _Window;
        public AlarmLevelSetEditViewModel(AlarmLevelSetListViewModel _vm, AlarmLevelSetEditView _mw)
        {
            _AlarmLevelSetListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            AlarmLevelSetObj = new AlarmLevelSetOR();

            Init();
        }

        public AlarmLevelSetEditViewModel(AlarmLevelSetListViewModel _vm, AlarmLevelSetEditView _mw, AlarmLevelSetOR _AlarmLevelSetObj)
        {
            _AlarmLevelSetListVM = _vm;
            _Window = _mw;
            _SourceObj = _AlarmLevelSetObj;

            OperationType = OpType.Alert;
            AlarmLevelSetObj = new AlarmLevelSetOR();
            AlarmLevelSetObj.Clone(_AlarmLevelSetObj);

            Init();
        }

        private void Init()
        {
            AlarmLevel = new ObservableCollection<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            
        }

        public ObservableCollection<int> AlarmLevel { get; set; }

        public AlarmLevelSetOR AlarmLevelSetObj { get; set; }


        public AlarmLevelSetEditViewModel(AlarmLevelSetOR _Sta)
        {
            OperationType = OpType.Alert;
            AlarmLevelSetObj = _Sta;
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
                    new AlarmLevelSetDA().Update(AlarmLevelSetObj);
                    _SourceObj.Clone(AlarmLevelSetObj);
                }
                else
                {
                    new AlarmLevelSetDA().Insert(AlarmLevelSetObj);
		    //_AlarmLevelSetListVM.AlarmLevelSetORList.Add(AlarmLevelSetObj);
                    _AlarmLevelSetListVM.Init();
                }
                _Window.Close();
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("IX_t_AlarmLevelSet_Priority") > 0)
                {
                    ShowMsgError(string.Format("报警等级：{0} 已添加！", AlarmLevelSetObj.Priority));
                    return;
                }
                ShowMsgError(ex.Message);
            }

        }
    }
}

