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
    public class MainteListViewModel : ListBaseViewModel
    {
        ObservableCollection<MainteOR> _MainteORList = null;

        public ObservableCollection<MainteOR> MainteORList
        {
            get { return _MainteORList; }
        }


        public MainteListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_MainteORList != null)
            {
                _MainteORList.Clear();
            }
            else
            {
                _MainteORList = new ObservableCollection<MainteOR>();
            }

            MainteDA _DA = new MainteDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _MainteORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            MainteEditView _sta = new MainteEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is MainteOR)
            {
                MainteEditView _sta = new MainteEditView(this, par as MainteOR);
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

                    new MainteDA().Delete(list);
                    List<MainteOR> listDelete = new List<MainteOR>();
                    if (_MainteORList != null)
                    {

                        foreach (MainteOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (MainteOR obj in listDelete)
                        {
                            _MainteORList.Remove(obj);
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

