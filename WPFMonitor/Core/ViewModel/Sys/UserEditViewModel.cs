using System;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Sys;
using System.Collections.ObjectModel;
using System.Text;

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
        private bool SetValue()
        {
            StringBuilder sbError = new StringBuilder();

            if (string.IsNullOrEmpty(UserObj.Username))
            {
                sbError.AppendLine("名称不能为空；");
            }

            if (string.IsNullOrEmpty(UserObj.Userpsw))
            {
                sbError.AppendLine("密码不能为空；");
            }

            if (UserObj.UserTypeShow == "管理员")
                UserObj.Usertype = 1;
            else
                UserObj.Usertype = 0;
            
            string ErrorMsg = sbError.ToString();
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                ShowMsgError(ErrorMsg);
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
            if (!SetValue())
                return;
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

