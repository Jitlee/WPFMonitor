using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class AlarmGroupMembersListViewModel : ListBaseViewModel
    {
        ObservableCollection<AlarmGroupMembersOR> _AlarmGroupMembersORList = null;

        public ObservableCollection<AlarmGroupMembersOR> AlarmGroupMembersORList
        {
            get { return _AlarmGroupMembersORList; }
        }


        public AlarmGroupMembersListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_AlarmGroupMembersORList != null)
            {
                _AlarmGroupMembersORList.Clear();
            }
            else
            {
                _AlarmGroupMembersORList = new ObservableCollection<AlarmGroupMembersOR>();
            }

            AlarmGroupMembersDA _DA = new AlarmGroupMembersDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _AlarmGroupMembersORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            AlarmGroupMembersEditView _sta = new AlarmGroupMembersEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is AlarmGroupMembersOR)
            {
                AlarmGroupMembersEditView _sta = new AlarmGroupMembersEditView(this, par as AlarmGroupMembersOR);
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

                    new AlarmGroupMembersDA().Delete(list);
                    List<AlarmGroupMembersOR> listDelete = new List<AlarmGroupMembersOR>();
                    if (_AlarmGroupMembersORList != null)
                    {

                        foreach (AlarmGroupMembersOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (AlarmGroupMembersOR obj in listDelete)
                        {
                            _AlarmGroupMembersORList.Remove(obj);
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

