using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFMonitor.Model
{
    public class FileModel : ORBase
    {
        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; NotifyPropertyChanged("Uri"); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }

        private string _displayName;

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; NotifyPropertyChanged("DisplayName"); }
        }

        private long _fileSize;

        public long FileSize
        {
            get { return _fileSize; }
            set { _fileSize = value; NotifyPropertyChanged("FileSize"); }
        }
        private DateTime _modifyTime;

        public DateTime ModifyTime
        {
            get { return _modifyTime; }
            set { _modifyTime = value; NotifyPropertyChanged("ModifyTime"); }
        }
        private DateTime _creationTime;

        public DateTime CreationTime
        {
            get { return _creationTime; }
            set { _creationTime = value; NotifyPropertyChanged("CreationTime"); }
        }
        private double _height;

        public double Height
        {
            get { return _height; }
            set { _height = value; NotifyPropertyChanged("Height"); }
        }
        private double _width;

        public double Width
        {
            get { return _width; }
            set { _width = value; NotifyPropertyChanged("Width"); }
        }

        private bool _isDirectory;

        public bool IsDirectory
        {
            get { return _isDirectory; }
            set { _isDirectory = value; NotifyPropertyChanged("IsDirectory"); }
        }

        private string _directoryName;

        public string DirectoryName
        {
            get { return _directoryName; }
            set { _directoryName = value; NotifyPropertyChanged("DirectoryName"); }
        }

        private int _directoriesCount;

        public int DirectoriesCount
        {
            get { return _directoriesCount; }
            set { _directoriesCount = value; NotifyPropertyChanged("DirectoriesCount"); }
        }

        private int _filesCount;
        public int FilesCount
        {
            get { return _filesCount; }
            set { _filesCount = value; NotifyPropertyChanged("FilesCount"); }
        }
    }
}
