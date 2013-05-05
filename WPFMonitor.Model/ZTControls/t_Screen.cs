using System;
using System.Data;
using System.Collections.ObjectModel;

namespace WPFMonitor.Model.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class t_Screen : ORBase
    {

        private int _ScreenID;
        /// <summary>
        /// 
        /// </summary>
        public int ScreenID
        {
            get { return _ScreenID; }
            set
            {
                _ScreenID = value;
                NotifyPropertyChanged("ScreenID");
            }
        }

        private string _ScreenName;
        /// <summary>
        /// 
        /// </summary>
        public string ScreenName
        {
            get { return _ScreenName; }
            set
            {
                _ScreenName = value;
                NotifyPropertyChanged("ScreenName");
            }
        }

        private string _ImageURL;
        /// <summary>
        /// 
        /// </summary>
        public string ImageURL
        {
            get { return _ImageURL; }
            set
            {
                _ImageURL = value;
                NotifyPropertyChanged("ImageURL");
            }
        }

        private int _ParentScreenID;
        /// <summary>
        /// 
        /// </summary>
        public int ParentScreenID
        {
            get { return _ParentScreenID; }
            set
            {
                _ParentScreenID = value;
                NotifyPropertyChanged("ParentScreenID");
            }
        }

        private int _StationID;
        /// <summary>
        /// 
        /// </summary>
        public int StationID
        {
            get { return _StationID; }
            set
            {
                _StationID = value;
                NotifyPropertyChanged("StationID");
            }
        }

        private int _Flag;
        /// <summary>
        /// 
        /// </summary>
        public int Flag
        {
            get { return _Flag; }
            set
            {
                _Flag = value;
                NotifyPropertyChanged("Flag");
            }
        }

        private int _Width;
        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
                NotifyPropertyChanged("Width");
            }
        }

        private int _Height;
        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                NotifyPropertyChanged("Height");
            }
        }

        private string _BackColor;
        /// <summary>
        /// 
        /// </summary>
        public string BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                NotifyPropertyChanged("BackColor");
            }
        }

        private bool _AutoSize;
        /// <summary>
        /// 
        /// </summary>
        public bool AutoSize
        {
            get { return _AutoSize; }
            set
            {
                _AutoSize = value;
                NotifyPropertyChanged("AutoSize");
            }
        }

        public ObservableCollection<t_Screen> Children { get; private set; }

        /// <summary>
        /// Screen构造函数
        /// </summary>
        public t_Screen()
        {
            Children = new ObservableCollection<t_Screen>();
        }

        ///// <summary>
        ///// Screen构造函数
        ///// </summary>
        //public t_Screen(DataRow row)
        //{
        //    // 
        //    _ScreenID = Convert.ToInt32(row["ScreenID"]);
        //    // 
        //    _ScreenName = row["ScreenName"].ToString().Trim();
        //    // 
        //    _ImageURL = row["ImageURL"].ToString().Trim();
        //    // 
        //    _ParentScreenID = Convert.ToInt32(row["ParentScreenID"]);
        //    // 
        //    _StationID = Convert.ToInt32(row["StationID"]);
        //    // 
        //    _Flag = Convert.ToInt32(row["Flag"]);
        //    // 
        //    _Width = Convert.ToInt32(row["Width"]);
        //    // 
        //    _Height = Convert.ToInt32(row["Height"]);
        //    // 
        //    _BackColor = row["BackColor"].ToString().Trim();
        //    // 
        //    _AutoSize = Convert.ToBoolean(row["AutoSize"]);
        //}

        public void Clone(t_Screen obj)
        {
            //
            ScreenID = obj.ScreenID;
            //
            ScreenName = obj.ScreenName;
            //
            ImageURL = obj.ImageURL;
            //
            ParentScreenID = obj.ParentScreenID;
            //
            StationID = obj.StationID;
            //
            Flag = obj.Flag;
            //
            Width = obj.Width;
            //
            Height = obj.Height;
            //
            BackColor = obj.BackColor;
            //
            AutoSize = obj.AutoSize;

        }
    }
}