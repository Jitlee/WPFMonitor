using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.Model.Linkage;
using WPFMonitor.DAL.Linkage;
using WPFMonitor.Core.ViewModel;
using WPFMonitor.View.Linkage;


namespace WPFMonitor.Core.ViewModel.Linkage
{
    public class TimeLinkageListViewModel : ListBaseViewModel
    {
        ObservableCollection<TimeLinkageOR> _TimeLinkageORList = null;

        public ObservableCollection<TimeLinkageOR> TimeLinkageORList
        {
            get { return _TimeLinkageORList; }
        }


        public TimeLinkageListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_TimeLinkageORList != null)
            {
                _TimeLinkageORList.Clear();
            }
            else
            {
                _TimeLinkageORList = new ObservableCollection<TimeLinkageOR>();
            }

            TimeLinkageDA _DA = new TimeLinkageDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _TimeLinkageORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            TimeLinkageEditView _sta = new TimeLinkageEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is TimeLinkageOR)
            {
                TimeLinkageEditView _sta = new TimeLinkageEditView(this, par as TimeLinkageOR);
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

                    new TimeLinkageDA().Delete(list);
                    List<TimeLinkageOR> listDelete = new List<TimeLinkageOR>();
                    if (_TimeLinkageORList != null)
                    {

                        foreach (TimeLinkageOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (TimeLinkageOR obj in listDelete)
                        {
                            _TimeLinkageORList.Remove(obj);
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

