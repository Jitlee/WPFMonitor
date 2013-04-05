using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Sys;
using WPFMonitor.View.Sys;

namespace WPFMonitor.Core.ViewModel
{
    public class StationListViewModel : ListBaseViewModel
    {
        ObservableCollection<StationOR> _StationORList = null;

        public ObservableCollection<StationOR> StationORList
        {
            get { return _StationORList; }
        }

        
        public StationListViewModel()
        {
            Init();
        }

        protected void Init()
        {
            StationDA _staDA = new StationDA();
            _StationORList = _staDA.selectAllStation();
        }

        public override void Insert()
        {
            StationEdit _sta = new StationEdit(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }

      

        public override void Update(object par)
        {
            if (par is StationOR)
            {
                StationEdit _sta = new StationEdit(this,par as StationOR);
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
                    
                    new StationDA().Delete(list);
                    List<StationOR> listDelete = new List<StationOR>();
                    if (_StationORList != null)
                    {

                        foreach (StationOR obj in list)
                        {
                            listDelete.Add(obj);                            
                        }
                        foreach (StationOR obj in listDelete)
                        {
                            _StationORList.Remove(obj);
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
