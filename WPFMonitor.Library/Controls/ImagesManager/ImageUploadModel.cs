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
using System.IO;
using System.Threading;
using WPFMonitor.DAL;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public class ImageUploadModel : EntiyObject//, IDisposable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
        }

        private long _fileSize;

        public long FileSize
        {
            get { return _fileSize; }
        }

        private long _ready;

        public long Ready
        {
            get { return _ready; }
            set { _ready = value; RaisePropertyChanged("Ready"); RaisePropertyChanged("Percentage"); }
        }

        public int Percentage
        {
            get { if (_fileSize > 0) { return (int)(((double)_ready / (double)_fileSize) * 100d); } return 0; }
        }

        public FileInfo FileInfo{get;private set;}

        public bool IsCompleted { get { return _ready == FileSize; } }
        private bool _isFailed;

        public bool IsFailed
        {
            get { return _isFailed; }
            set { _isFailed = value; RaisePropertyChanged("IsFailed"); }
        }

        private bool _isDeleting;

        public bool IsDeleting
        {
            get { return _isDeleting; }
            set { _isDeleting = value; RaisePropertyChanged("IsDeleting"); }
        }

        private bool _isDeleted;

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; RaisePropertyChanged("IsDeleted"); }
        }

        //public DomainService1 DomainService { get; private set; }

        public ImageUploadModel(FileInfo fileInfo)
        {
            FileInfo = fileInfo;
            _name = fileInfo.Name;
            _fileSize = fileInfo.Length;
            //DomainService = new DomainService1();
        }
    }
}
