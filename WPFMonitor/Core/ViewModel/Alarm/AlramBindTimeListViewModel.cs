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
    public class AlramBindTimeListViewModel : ListBaseViewModel
    {
        ObservableCollection<AlramBindTimeOR> _AlramBindTimeORList = null;

        public ObservableCollection<AlramBindTimeOR> AlramBindTimeORList
        {
            get { return _AlramBindTimeORList; }
        }


        public AlramBindTimeListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_AlramBindTimeORList != null)
            {
                _AlramBindTimeORList.Clear();
            }
            else
            {
                _AlramBindTimeORList = new ObservableCollection<AlramBindTimeOR>();
            }
            AlramBindTimeDA _staDA = new AlramBindTimeDA();
            
            var v=_staDA.selectAllDate();
            foreach (var vobj in v)
            {
                _AlramBindTimeORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            AlramBindTimeEditView _sta = new AlramBindTimeEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is AlramBindTimeOR)
            {
                AlramBindTimeEditView _sta = new AlramBindTimeEditView(this, par as AlramBindTimeOR);
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

                    new AlramBindTimeDA().Delete(list);
                    List<AlramBindTimeOR> listDelete = new List<AlramBindTimeOR>();
                    if (_AlramBindTimeORList != null)
                    {

                        foreach (AlramBindTimeOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (AlramBindTimeOR obj in listDelete)
                        {
                            _AlramBindTimeORList.Remove(obj);
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

