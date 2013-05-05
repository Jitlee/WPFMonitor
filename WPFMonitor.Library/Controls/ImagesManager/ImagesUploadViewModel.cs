using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WPFMonitor.DAL;
using System.Threading;
using System.ComponentModel;
using WPFMonitor.Library.MonitorSystemGlobal;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public class ImagesUploadViewModel : EntiyObject
    {
        private const int BUFFER_SIZE = 2408;
        private readonly string _path;
        private readonly DelegateCommand<ImageUploadModel> _removeCommand;

        private long _totalSize;

        public long TotalSize
        {
            get { return _totalSize; }
            set { _totalSize = value; RaisePropertyChanged("TotalSize"); }
        }

        private long _totalready;

        public long TotalReady
        {
            get { return _totalready; }
            set { _totalready = value; RaisePropertyChanged("TotalReady"); RaisePropertyChanged("TotalPercentage"); }
        }

        public int TotalPercentage
        {
            get { if (_totalSize > 0) { return (int)(((double)_totalready / (double)_totalSize) * 100d); } return 0; }
        }
        private ObservableCollection<ImageUploadModel> _items;

        public ObservableCollection<ImageUploadModel> Items
        {
            get { return _items; }
        }

        public ICommand RemoveCommand
        {
            get { return _removeCommand; }
        }

        private string _result;

        public string Result
        {
            get { return _result; }
            set { _result = value; RaisePropertyChanged("Result"); }
        }

        private SolidColorBrush _resultBrush;

        public SolidColorBrush ResultBrush
        {
            get { return _resultBrush; }
            set { _resultBrush = value; RaisePropertyChanged("ResultBrush"); }
        }

        ImagesUploadWindow _window;

        private bool _cancel;

        private bool _isCompleted;

        private Action _finished;

        private int _removeCount = 0;

        private readonly FileContext _context;

        public ImagesUploadViewModel(FileContext context, string path, IEnumerable<FileInfo> files, Action finished)
        {
            _context = context;

            _path = path;

            _finished = finished;

            _items = new ObservableCollection<ImageUploadModel>(files.Select(file => new ImageUploadModel(file)));

            _totalSize = files.Sum(file => file.Length);

            _removeCommand = new DelegateCommand<ImageUploadModel>(Remove);

            _window = new ImagesUploadWindow();

            _window.DataContext = this;

            _window.Loaded += ImagesUploadWindow_Loaded;

            _window.Closed += Window_Closed;

            _window.Closing += Window_Closing;

            _window.Show();

        }

        private void ImagesUploadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var file in _items)
            {
                Upload(file);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_items.Count > 0 && !_isCompleted && MessageBox.Show("文件上传工作还未完成，是否确认要退出？", "确认", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _window.Loaded -= ImagesUploadWindow_Loaded;

            _window.Closed -= Window_Closed;

            _window.Closing -= Window_Closing;

            if (_isCompleted)
            {
                if (null != _finished)
                {
                    _finished();
                }
            }
            else
            {
                _cancel = true;

                foreach (var file in _items)
                {
                    Delete(file);
                }
            }
        }

        private void Delete(ImageUploadModel file)
        {
            _context.DeleteFile(_path, file.Name, DeleteCallback, file);
        }

        private void DeleteCallback(InvokeOperation<int> result)
        {
            var file = result.UserState as ImageUploadModel;
            if (result.HasError || result.Value != 0)
            {
                MessageBox.Show(string.Format("删除文件失败:{0}",file.Name), "提示信息", MessageBoxButton.OK);
            }


            TotalSize -= file.FileSize;
            TotalReady -= file.Ready;

            file.IsDeleted = true;
        }

        private void Upload(ImageUploadModel file)
        {
            if (!_cancel && !file.IsFailed && !file.IsCompleted)
            {
                using (var stream = file.FileInfo.OpenRead())
                {
                    stream.Position = file.Ready;
                    var len = (int)Math.Min(BUFFER_SIZE, stream.Length - stream.Position);
                    var buffer = new byte[len];
                    stream.Read(buffer, 0, len);
                    _context.UploadFile(_path, file.Name, buffer, stream.Position == 0, UploadCallback, file);
                }
            }
        }

        private void UploadCallback(InvokeOperation<int> result)
        {
            var file = result.UserState as ImageUploadModel;
            if (file.IsDeleted)
            {
                return;
            }
            if (file.IsDeleting)
            {
                Delete(file);
                return;
            }

            if (!result.HasError && result.Value == 0)
            {
                var finished = Math.Min(BUFFER_SIZE, file.FileSize - file.Ready);
                file.Ready += finished;
                TotalReady += finished;
                Upload(file);
            }
            else
            {
                file.IsFailed = true;
                TotalReady += file.FileSize - file.Ready;
            }

            var failedCount = _items.Count(f => f.IsFailed);
            if (TotalReady == TotalSize)
            {
                Result = "图片都已上传成功！";
                ResultBrush = new SolidColorBrush(Colors.Blue);
                _isCompleted = true;
            }
            else if (failedCount == _items.Count)
            {
                Result = _items.Count == 1 ? "图片上传失败" : "图片全部上传失败！";
                ResultBrush = new SolidColorBrush(Colors.Red);
                _isCompleted = true;
            }
            else if(_items.Count(f => !f.IsCompleted && !f.IsFailed) == 0)
            {
                Result = string.Format("成功上传{0}张，失败{1}张，中途取消{2}张", _items.Count - failedCount, failedCount, _removeCount);
                ResultBrush = new SolidColorBrush(Colors.Red);
                _isCompleted = true;
            }
        }

        private void Remove(ImageUploadModel imageUploadModel)
        {
            _items.Remove(imageUploadModel);
            _removeCount++;
            if (!imageUploadModel.IsFailed && !imageUploadModel.IsCompleted && imageUploadModel.Ready > 0)
            {
                imageUploadModel.IsDeleting = true;
            }
            else
            {
                Delete(imageUploadModel);
            }
        }
    }
}
