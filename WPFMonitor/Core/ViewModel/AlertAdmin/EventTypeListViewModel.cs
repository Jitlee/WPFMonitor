using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.Model.AlertAdmin;
using WPFMonitor.DAL.AlertAdmin;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.AlertAdmin;
using WPFMonitor.Model;


namespace WPFMonitor.Core.ViewModel.AlertAdmin
{
    public class EventTypeListViewModel : ListBaseViewModel
    {
        ObservableCollection<EventTypeOR> _EventTypeORList = null;

        public ObservableCollection<EventTypeOR> EventTypeORList
        {
            get { return _EventTypeORList; }
        }


        public EventTypeListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_EventTypeORList != null)
            {
                _EventTypeORList.Clear();
            }
            else
            {
                _EventTypeORList = new ObservableCollection<EventTypeOR>();
            }

            EventTypeDA _DA = new EventTypeDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _EventTypeORList.Add(vobj);
            }
           

        }

        public override void Insert()
        {
            EventTypeEditView _sta = new EventTypeEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }

       

        public override void Update(object par)
        {
            if (par is EventTypeOR)
            {
                EventTypeEditView _sta = new EventTypeEditView(this, par as EventTypeOR);
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

                    new EventTypeDA().Delete(list);
                    List<EventTypeOR> listDelete = new List<EventTypeOR>();
                    if (_EventTypeORList != null)
                    {

                        foreach (EventTypeOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (EventTypeOR obj in listDelete)
                        {
                            _EventTypeORList.Remove(obj);
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

