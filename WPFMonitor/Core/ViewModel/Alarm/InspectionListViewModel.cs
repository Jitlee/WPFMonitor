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
    public class InspectionListViewModel : ListBaseViewModel
    {
        ObservableCollection<InspectionOR> _InspectionORList = null;

        public ObservableCollection<InspectionOR> InspectionORList
        {
            get { return _InspectionORList; }
        }


        public InspectionListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_InspectionORList != null)
                _InspectionORList.Clear();
            else
                _InspectionORList = new ObservableCollection<InspectionOR>();

            InspectionDA _staDA = new InspectionDA();
            var _List = _staDA.selectAllDate();
            foreach (var obj in _List)
            {
                _InspectionORList.Add(obj);
            }
        }

        public override void Insert()
        {
            InspectionEditView _sta = new InspectionEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is InspectionOR)
            {
                InspectionEditView _sta = new InspectionEditView(this, par as InspectionOR);
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

                    new InspectionDA().Delete(list);
                    List<InspectionOR> listDelete = new List<InspectionOR>();
                    if (_InspectionORList != null)
                    {

                        foreach (InspectionOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (InspectionOR obj in listDelete)
                        {
                            _InspectionORList.Remove(obj);
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

