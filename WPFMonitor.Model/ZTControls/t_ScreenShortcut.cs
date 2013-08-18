using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WPFMonitor.Model.ZTControls
{
    public class t_ScreenShortcut : ORBase
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; NotifyPropertyChanged("Id"); } }

        private int _screenId;
        public int ScreenId { get { return _screenId; } set { _screenId = value; NotifyPropertyChanged("ScreenId"); } }

        private string _screenName;
        public string ScreenName { get { return _screenName; } set { _screenName = value; NotifyPropertyChanged("ScreenName"); } }

        private string _screenDescription;
        public string ScreenDescription { get { return _screenDescription; } set { _screenDescription = value; NotifyPropertyChanged("ScreenDescription"); } }

        private byte[] _imageBuffer;
        public byte[] ImageBuffer { get { return _imageBuffer; } set { _imageBuffer = value; NotifyPropertyChanged("ImageBuffer"); } }

        public t_ScreenShortcut() { }

        public t_ScreenShortcut(DataRow row)
        {
            _id = Converter.ToInt32(row["ID"]);
            _screenId = Converter.ToInt32(row["SCREENID"]);
            _screenName = row["SCREENNAME"].ToString();
            _screenDescription = row["SCREENDESCRIPTION"].ToString();
            _imageBuffer = (byte[])row["SCREENIMAGE"];
        }
    }
}
