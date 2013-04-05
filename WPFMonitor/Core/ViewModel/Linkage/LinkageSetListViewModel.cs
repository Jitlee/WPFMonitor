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
    public class LinkageSetListViewModel : ListBaseViewModel
    {
        ObservableCollection<LinkageSetOR> _LinkageSetORList = null;

        public ObservableCollection<LinkageSetOR> LinkageSetORList
        {
            get { return _LinkageSetORList; }
        }


        public LinkageSetListViewModel()
        {
            Init();
        }

        public void Init()
        {
            if (_LinkageSetORList != null)
            {
                _LinkageSetORList.Clear();
            }
            else
            {
                _LinkageSetORList = new ObservableCollection<LinkageSetOR>();
            }

            LinkageSetDA _DA = new LinkageSetDA();            
            var v=_DA.selectAllDate();
            foreach (var vobj in v)
            {
                _LinkageSetORList.Add(vobj);
            }
        }

        public override void Insert()
        {
            LinkageSetEditView _sta = new LinkageSetEditView(this);
            _sta.Owner = Global._MainWindow;
            _sta.ShowDialog();
        }



        public override void Update(object par)
        {
            if (par is LinkageSetOR)
            {
                LinkageSetEditView _sta = new LinkageSetEditView(this, par as LinkageSetOR);
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

                    new LinkageSetDA().Delete(list);
                    List<LinkageSetOR> listDelete = new List<LinkageSetOR>();
                    if (_LinkageSetORList != null)
                    {

                        foreach (LinkageSetOR obj in list)
                        {
                            listDelete.Add(obj);
                        }
                        foreach (LinkageSetOR obj in listDelete)
                        {
                            _LinkageSetORList.Remove(obj);
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

