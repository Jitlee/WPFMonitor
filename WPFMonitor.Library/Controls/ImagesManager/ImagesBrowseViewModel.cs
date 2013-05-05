using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFMonitor.Library.MonitorSystemGlobal;
using WPFMonitor.Model;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public class ImagesBrowseViewModel : EntiyObject
    {
        private readonly DelegateCommand _okCommand;

        public ICommand OKCommand
        {
            get { return _okCommand; }
        }

        private readonly DelegateCommand _uploadCommand;

        public ICommand UploadCommand
        {
            get { return _uploadCommand; }
        }

        private readonly DelegateCommand _createDirectoryCommand;

        public ICommand CreateDirectoryCommand
        {
            get { return _createDirectoryCommand; }
        }

        private readonly DelegateCommand _backCommand;

        public ICommand BackCommand
        {
            get { return _backCommand; }
        }

        private readonly DelegateCommand _deleteCommand;

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        private readonly DelegateCommand _refreshCommand;

        public ICommand RefreshCommand
        {
            get { return _refreshCommand; }
        }

        private readonly DelegateCommand<FileModel> _openCommand;

        public ICommand OpenCommand
        {
            get { return _openCommand; }
        }

        private readonly DelegateCommand _renameCommand;

        public ICommand RenameCommand
        {
            get { return _renameCommand; }
        }

        private ObservableCollection<FileModel> _items;

        private PagedCollectionView _view;

        public PagedCollectionView View
        {
            get { return _view; }
            private set { _view = value; RaisePropertyChanged("View"); }
        }

        private string _keywords;

        public string Keywords
        {
            get { return _keywords; }
            private set { _keywords = value.Trim(); Refresh(); _createDirectoryCommand.RaiseCanExecuteChanged(); RaisePropertyChanged("Keywords"); }
        }

        private ObservableCollection<FileModel> _selectedItems = new ObservableCollection<FileModel>();

        public ObservableCollection<FileModel> SelectedItems
        {
            get { return _selectedItems; }
            private set { _selectedItems = value; RaisePropertyChanged("SelectedItems"); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { _isBusy = value; RaisePropertyChanged("IsBusy"); }
        }

        private string _busyTitle;
        public string BusyTitle
        {
            get { return _busyTitle; }
            private set { _busyTitle = value;  RaisePropertyChanged("BusyTitle"); }
        }

        private FileOption _fileOption = FileOption.All;



        private readonly FileContext _context = new FileContext();

        private string _path = string.Empty;

        private Action<FileModel> _callback;

        //private ChildWindow _window;

        private string _rootPath;

        public ImagesBrowseViewModel( Action<FileModel> callback, string path = "", bool onlyImage = false)
        {
            //_window = window;
            _callback = callback;
            _rootPath = _path = path;

            if (onlyImage) // 将不能创建和 显示 删除 文件夹
            {
                _fileOption = FileOption.File;
            }
            _okCommand = new DelegateCommand(OK, CanOK);
            _uploadCommand = new DelegateCommand(Upload);
            _createDirectoryCommand = new DelegateCommand(CreateDirectory, CanCreateDirectory);
            _backCommand = new DelegateCommand(Back, CanBack);
            _deleteCommand = new DelegateCommand(Delete, CanDelete);
            _refreshCommand = new DelegateCommand(Refresh, CanRefresh);
            _openCommand = new DelegateCommand<FileModel>(Open);
            _renameCommand = new DelegateCommand(Rename, CanRename);
            _selectedItems.CollectionChanged += SelectedItems_CollectionChanged;
            Refresh();
        }

        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _okCommand.RaiseCanExecuteChanged();
            _deleteCommand.RaiseCanExecuteChanged();
            _renameCommand.RaiseCanExecuteChanged();
        }

        private void Upload()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEP 图像(*.jpg);PNG 图像(*.png)|*.jpg;*.png|JPEP 图像(*.jpg)|*.jpg|PNG 图像(*.png)|*.png";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                var uploadVM = new ImagesUploadViewModel(_context, _path, openFileDialog.Files, Refresh);
            }
        }

        private void CreateDirectory()
        {
            throw new Exception();
            //var window = new InputNameWindow();
            //window.Title = "输入框";
            //window.SubTitle = "文件夹名称";
            //window.Closed += (o, e) => {
            //    if (window.DialogResult == true)
            //    {
            //        _context.CreateDirectory(_path, window.Value, CreateDirectoryCallback, window.Value);
            //    }
            //};
            //window.Show();
        }

        //private void CreateDirectoryCallback(InvokeOperation<int> result)
        //{
        //    IsBusy = false;
        //    if (result.HasError || (result.Value != 0  && result.Value != -2 ))
        //    {
        //        MessageBox.Show("创建文件夹失败.", "提示信息", MessageBoxButton.OK);
        //    }
        //    else if(result.Value == -2)
        //    {
        //        MessageBox.Show("创建文件夹失败, 文件夹已经存在.", "提示信息", MessageBoxButton.OK);
        //    }
        //    else
        //    {
        //        Refresh();
        //    }
        //}

        private bool CanCreateDirectory()
        {
            return string.IsNullOrWhiteSpace(_keywords) && _fileOption != FileOption.File;
        }

        private void Back()
        {
            var index = _path.LastIndexOf("\\");
            if (index > -1)
            {
                _path = _path.Remove(index).Trim('\\');
            }
            _backCommand.RaiseCanExecuteChanged();
            Refresh();
        }

        private bool CanBack()
        {
            return string.Compare(_rootPath, _path, StringComparison.OrdinalIgnoreCase) != 0 && _fileOption != FileOption.File;
        }

        private int _count = 0;

        private void Delete()
        {
            BusyTitle = "正在删除文件，请稍后...";
            IsBusy = true;

            throw new Exception();
            //_selectedItems.ToList().ForEach(
            //    file => 
            //    {
            //        if (file.IsDirectory) {
            //            _context.DeleteDirectory(file.DirectoryName, file.Name, DeleteCallback, file);
            //        }
            //        else
            //        {
            //            _context.DeleteFile(file.DirectoryName, file.Name, DeleteCallback, file);
            //        }
            //    });
        }

        private void DeleteCallback(InvokeOperation<int> result)
        {
            var file = result.UserState as FileModel;
            if (result.HasError || result.Value != 0)
            {
                if (file.IsDirectory)
                {
                    MessageBox.Show(string.Format("删除文件失夹败:{0}", file.Name), "提示信息", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(string.Format("删除文件失夹:{0}", file.Name), "提示信息", MessageBoxButton.OK);
                }
            }
            else
            {
                _items.Remove(file);
            }
            _count--;
            if (_count <= 0)
            {
                IsBusy = false;
            }
        }

        private bool CanDelete()
        {
            return _selectedItems.Count > 0;
        }

        private void Refresh()
        {
            if (!_context.IsLoading)
            {
                BusyTitle = "正在加载数据，请稍后...";
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(Keywords))
                {
                    _context.Load<FileModel>(_context.GetImagesQuery(_path, _fileOption), GetImagesQueryCallback, Keywords);
                    _refreshCommand.RaiseCanExecuteChanged();
                }
                else
                {
                    _context.Load<FileModel>(_context.SearchFilesQuery(_keywords, _fileOption), GetImagesQueryCallback, Keywords);
                    _refreshCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanRefresh()
        {
            return !_context.IsLoading && !_context.IsSubmitting;
        }

        private void GetImagesQueryCallback(LoadOperation<FileModel> result)
        {
            if (!result.HasError)
            {
                if ((_keywords == null && null == result.UserState) || _keywords.Equals(result.UserState))
                {
                    _items = new ObservableCollection<FileModel>(result.Entities);

                    View = new PagedCollectionView(_items);
                }
                else
                {
                    Refresh();
                    return;
                }

            }
            _refreshCommand.RaiseCanExecuteChanged();
            IsBusy = false;
        }

        private void Open(FileModel file)
        {
            if (file.IsDirectory)
            {
                _path += "\\" + file.Name.Trim('\\');
                _backCommand.RaiseCanExecuteChanged();
                Refresh();
            }
            else
            {
                //System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(file.Url, UriKind.RelativeOrAbsolute), "_blank");
                if (null != _callback)
                {
                    _callback(file);
                    //_window.DialogResult = true;
                    //_window.Close();
                }
            }
        }

        private void Rename()
        {
            var file = _selectedItems.First();
            var window = new InputNameWindow();
            window.Title = "输入框";
            window.SubTitle = file.IsDirectory ? "文件夹名称" : "文件名称";
            window.Value = file.Name;
            window.Closed += (o, e) =>
            {
                if (window.DialogResult == true)
                {
                    if (file.IsDirectory)
                    {
                        _context.RenameDirectory(file.DirectoryName, file.Name, window.Value, RenameCallback, file);
                    }
                    else
                    {
                        _context.RenameFile(file.DirectoryName, file.Name, window.Value, RenameCallback, file);
                    }
                }
            };
            window.Show();
        }

        private void RenameCallback(InvokeOperation<int> result)
        {
            var file = result.UserState as FileModel;
            if (result.HasError || (result.Value != 0 && result.Value != -3))
            {
                MessageBox.Show("重命名失败.", "提示信息", MessageBoxButton.OK);
            }
            else if (result.Value == -3)
            {
                if (file.IsDirectory)
                {
                    MessageBox.Show("重命名失败, 文件夹已经存在.", "提示信息", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("重命名失败, 文件已经存在.", "提示信息", MessageBoxButton.OK);
                }
            }
            else
            {
                Refresh();
            }
        }

        private bool CanRename()
        {
            return _selectedItems.Count == 1;
        }

        private void OK()
        {
            if (null != _callback)
            {
                _callback(_selectedItems.First());
            }
        }

        private bool CanOK()
        {
            return _selectedItems.Count == 1 && !_selectedItems[0].IsDirectory;
        }
    }
}
