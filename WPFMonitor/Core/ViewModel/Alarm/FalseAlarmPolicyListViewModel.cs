using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.Model.Alarm;
using WPFMonitor.DAL.Alarm;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Alarm;


namespace WPFMonitor.Core.ViewModel.Alarm
{
    public class FalseAlarmPolicyListViewModel : ListBaseViewModel
    {
        ObservableCollection<FalseAlarmPolicyOR> _FalseAlarmPolicyORList = null;

        public ObservableCollection<FalseAlarmPolicyOR> FalseAlarmPolicyORList
        {
            get { return _FalseAlarmPolicyORList; }
        }


        public FalseAlarmPolicyListViewModel()
        {
            Init();
        }

        public void Init()
        {
            FalseAlarmPolicyDA _staDA = new FalseAlarmPolicyDA();
            if (_FalseAlarmPolicyORList != null)
            {
                _FalseAlarmPolicyORList.Clear();
            }
            else
            {
                _FalseAlarmPolicyORList = new ObservableCollection<FalseAlarmPolicyOR>();
            }

            var v = _staDA.selectAllDate();
            foreach (var vobj in v)
            {
                _FalseAlarmPolicyORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            FalseAlarmPolicyEditView _sta = new FalseAlarmPolicyEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is FalseAlarmPolicyOR)
            {
                FalseAlarmPolicyEditView _sta = new FalseAlarmPolicyEditView(this, par as FalseAlarmPolicyOR);
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

                    new FalseAlarmPolicyDA().Delete(list);
                    List<FalseAlarmPolicyOR> listDelete = new List<FalseAlarmPolicyOR>();
                    if (_FalseAlarmPolicyORList != null)
                    {

                        foreach (FalseAlarmPolicyOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (FalseAlarmPolicyOR obj in listDelete)
                        {
                            _FalseAlarmPolicyORList.Remove(obj);
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

