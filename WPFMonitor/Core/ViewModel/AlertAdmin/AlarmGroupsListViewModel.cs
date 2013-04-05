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
    public class AlarmGroupsListViewModel : ListBaseViewModel
    {
        ObservableCollection<AlarmGroupsOR> _AlarmGroupsORList = null;

        public ObservableCollection<AlarmGroupsOR> AlarmGroupsORList
        {
            get { return _AlarmGroupsORList; }
        }


        public AlarmGroupsListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_AlarmGroupsORList != null)
            {
                _AlarmGroupsORList.Clear();
            }
            else
            {
                _AlarmGroupsORList = new ObservableCollection<AlarmGroupsOR>();
            }

            AlarmGroupsDA _DA = new AlarmGroupsDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _AlarmGroupsORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            AlarmGroupsEditView _sta = new AlarmGroupsEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is AlarmGroupsOR)
            {
                AlarmGroupsEditView _sta = new AlarmGroupsEditView(this, par as AlarmGroupsOR);
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

                    new AlarmGroupsDA().Delete(list);
                    List<AlarmGroupsOR> listDelete = new List<AlarmGroupsOR>();
                    if (_AlarmGroupsORList != null)
                    {

                        foreach (AlarmGroupsOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (AlarmGroupsOR obj in listDelete)
                        {
                            _AlarmGroupsORList.Remove(obj);
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

