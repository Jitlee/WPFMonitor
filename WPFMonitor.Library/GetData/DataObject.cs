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
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;

namespace MonitorSystem.GetData
{
    public class DataObject : INotifyPropertyChanged, IEditableObject
    {
        protected Dictionary<string, object> _backupData = new Dictionary<string, object>();

        //public enum DataStates
        //{
        //    Unchanged = 2,
        //    Added = 4,
        //    Deleted = 8,
        //    Modified = 16,
        //}
        //public DataStates State { get; set; }
        public object GetFieldValue(string fieldname)
        {
            PropertyInfo pi = this.GetType().GetProperty(fieldname);
            if (pi != null)
                return pi.GetValue(this, null);
            return null;
        }
        public void SetFieldValue(string fieldname, object value, bool initial)
        {
            this.SetFieldValue(fieldname, value);
            if (initial)
            {
                //this.State = DataStates.Unchanged;
                if (!_backupData.ContainsKey(fieldname))
                    _backupData.Add(fieldname, value);
            }

        }
        public void SetFieldValue(string fieldname, object value)
        {
            PropertyInfo pi = this.GetType().GetProperty(fieldname);
            if (pi != null)
            {
                object pValue = Convert.ChangeType(value, pi.PropertyType, null);
                if (pValue != null)
                    pi.SetValue(this, pValue, null);
            }
        }
        //public void Delete()
        //{
        //    this.State = DataStates.Deleted;
        //}
        //public void NewRow()
        //{
        //    this.State = DataStates.Added;
        //}
        protected void NotifyChange(params string[] properties)
        {
            if (PropertyChanged != null)
            {
                foreach (string p in properties)
                    PropertyChanged(this, new PropertyChangedEventArgs(p));
                //this.State = DataStates.Modified;
            }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region IEditableObject Members

        public void BeginEdit()
        {
        }

        public void CancelEdit()
        {
            foreach (string fieldName in _backupData.Keys)
                this.SetFieldValue(fieldName, _backupData[fieldName], true);
        }

        public void EndEdit()
        {

        }
        #endregion
    }
}
