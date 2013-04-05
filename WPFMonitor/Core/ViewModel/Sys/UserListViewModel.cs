using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.Model.Sys;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Sys;


namespace WPFMonitor.Core.ViewModel.Sys
{
    public class UserListViewModel : ListBaseViewModel
    {
        ObservableCollection<UserOR> _UserORList = null;

        public ObservableCollection<UserOR> UserORList
        {
            get { return _UserORList; }
        }


        public UserListViewModel()
        {
            Init();
        }

        public void Init()
        {
            UserDA _staDA = new UserDA();
            if (null != _UserORList)
                _UserORList.Clear(); 

            var vList = _staDA.selectAllDate();
            foreach (UserOR obj in vList)
            {
                _UserORList.Add(obj);
            }
        }

        public override void Insert()
        {
            UserEditView _sta = new UserEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is UserOR)
            {
                UserEditView _sta = new UserEditView(this, par as UserOR);
                _sta.Owner = Global._MainWindow;
                _sta.ShowDialog();
            }
        }

        public override void Delete(object parameter)
        {
            try
            {
                var list = parameter as IList;
                if (list.Count == 0)
                {
                    ShowMsgError("请选择删除项！");
                    return;
                }
                if (MessageBox.Show(string.Format("是否确定删除选定的{0}项?", list.Count)
                    , "询问", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    new UserDA().Delete(list);
                    List<UserOR> listDelete = new List<UserOR>();
                    if (_UserORList != null)
                    {

                        foreach (UserOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (UserOR obj in listDelete)
                        {
                            _UserORList.Remove(obj);
                        }
                        listDelete.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsgError(ex.Message);
            }
        }
    }
}

