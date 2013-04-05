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
    public class DisarmTimeListViewModel : ListBaseViewModel
    {
        ObservableCollection<DisarmTimeOR> _DisarmTimeORList = null;

        public ObservableCollection<DisarmTimeOR> DisarmTimeORList
        {
            get { return _DisarmTimeORList; }
        }


        public DisarmTimeListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_DisarmTimeORList != null)
            {
                _DisarmTimeORList.Clear();
            }
            else
            {
                _DisarmTimeORList = new ObservableCollection<DisarmTimeOR>();
            }

            DisarmTimeDA _DA = new DisarmTimeDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _DisarmTimeORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            DisarmTimeEditView _sta = new DisarmTimeEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is DisarmTimeOR)
            {
                DisarmTimeEditView _sta = new DisarmTimeEditView(this, par as DisarmTimeOR);
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

                    new DisarmTimeDA().Delete(list);
                    List<DisarmTimeOR> listDelete = new List<DisarmTimeOR>();
                    if (_DisarmTimeORList != null)
                    {

                        foreach (DisarmTimeOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (DisarmTimeOR obj in listDelete)
                        {
                            _DisarmTimeORList.Remove(obj);
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

