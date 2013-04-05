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
    public class KeyWordListViewModel : ListBaseViewModel
    {
        ObservableCollection<KeyWordOR> _KeyWordORList = null;

        public ObservableCollection<KeyWordOR> KeyWordORList
        {
            get { return _KeyWordORList; }
        }


        public KeyWordListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_KeyWordORList != null)
            {
                _KeyWordORList.Clear();
            }
            else
            {
                _KeyWordORList = new ObservableCollection<KeyWordOR>();
            }

            KeyWordDA _DA = new KeyWordDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _KeyWordORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            KeyWordEditView _sta = new KeyWordEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is KeyWordOR)
            {
                KeyWordEditView _sta = new KeyWordEditView(this, par as KeyWordOR);
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

                    new KeyWordDA().Delete(list);
                    List<KeyWordOR> listDelete = new List<KeyWordOR>();
                    if (_KeyWordORList != null)
                    {

                        foreach (KeyWordOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (KeyWordOR obj in listDelete)
                        {
                            _KeyWordORList.Remove(obj);
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

