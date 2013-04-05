using System;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Sys;
using System.Collections.ObjectModel;

namespace WPFMonitor.Core.ViewModel.Sys
{
    public class UserEditViewModel : EditBaseViewModel
    {
        UserListViewModel _UserListVM;
        UserOR _SourceObj;
        UserEditView _Window;

        

        public UserEditViewModel(UserListViewModel _vm, UserEditView _mw)
        {
            _UserListVM = _vm;
            _Window = _mw;
            OperationType = OpType.Add;
            UserObj = new UserOR();
            //UpdatetxtSource(_Window.gridContent);
            Init();
        }
        
        public UserEditViewModel(UserListViewModel _vm, UserEditView _mw, UserOR _UserObj)
        {
            _UserListVM = _vm;
            _Window = _mw;
            _SourceObj = _UserObj;
            Init();
            OperationType = OpType.Alert;
            UserObj = new UserOR();
            UserObj.Clone(_UserObj);
        }

        private void Init()
        {
            listUserType = new ObservableCollection<string>() { "管理员", "操作员" };
        }

        /// <summary>
        /// 当前站点
        /// </summary>
        public UserOR UserObj { get; set; }
        public ObservableCollection<string> listUserType { get; set; }

        public UserEditViewModel(UserOR _Sta)
        {
            OperationType = OpType.Alert;
            UserObj = _Sta;
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
                    new UserDA().Update(UserObj);
                    _SourceObj.Clone(UserObj);
                }
                else
                {
                    new UserDA().Insert(UserObj);
                    _UserListVM.Init();
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

