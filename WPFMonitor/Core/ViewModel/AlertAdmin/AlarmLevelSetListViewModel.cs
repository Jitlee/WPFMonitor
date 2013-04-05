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
    public class AlarmLevelSetListViewModel : ListBaseViewModel
    {
        ObservableCollection<AlarmLevelSetOR> _AlarmLevelSetORList = null;

        public ObservableCollection<AlarmLevelSetOR> AlarmLevelSetORList
        {
            get { return _AlarmLevelSetORList; }
        }


        public AlarmLevelSetListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_AlarmLevelSetORList != null)
            {
                _AlarmLevelSetORList.Clear();
            }
            else
            {
                _AlarmLevelSetORList = new ObservableCollection<AlarmLevelSetOR>();
            }

            AlarmLevelSetDA _DA = new AlarmLevelSetDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _AlarmLevelSetORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            AlarmLevelSetEditView _sta = new AlarmLevelSetEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is AlarmLevelSetOR)
            {
                AlarmLevelSetEditView _sta = new AlarmLevelSetEditView(this, par as AlarmLevelSetOR);
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

                    new AlarmLevelSetDA().Delete(list);
                    List<AlarmLevelSetOR> listDelete = new List<AlarmLevelSetOR>();
                    if (_AlarmLevelSetORList != null)
                    {

                        foreach (AlarmLevelSetOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (AlarmLevelSetOR obj in listDelete)
                        {
                            _AlarmLevelSetORList.Remove(obj);
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

