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
    public class SchedulingListViewModel : ListBaseViewModel
    {
        ObservableCollection<SchedulingOR> _SchedulingORList = null;

        public ObservableCollection<SchedulingOR> SchedulingORList
        {
            get { return _SchedulingORList; }
        }


        public SchedulingListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_SchedulingORList != null)
            {
                _SchedulingORList.Clear();
            }
            else
            {
                _SchedulingORList = new ObservableCollection<SchedulingOR>();
            }

            SchedulingDA _DA = new SchedulingDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _SchedulingORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            SchedulingEditView _sta = new SchedulingEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is SchedulingOR)
            {
                SchedulingEditView _sta = new SchedulingEditView(this, par as SchedulingOR);
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

                    new SchedulingDA().Delete(list);
                    List<SchedulingOR> listDelete = new List<SchedulingOR>();
                    if (_SchedulingORList != null)
                    {

                        foreach (SchedulingOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (SchedulingOR obj in listDelete)
                        {
                            _SchedulingORList.Remove(obj);
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

