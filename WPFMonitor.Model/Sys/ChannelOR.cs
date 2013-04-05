﻿using System;
using System.Data;

namespace WPFMonitor.Model.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelOR : ORBase
    {

        private int _Deviceid;
        /// <summary>
        /// 
        /// </summary>
        public int Deviceid
        {
            get { return _Deviceid; }
            set
            {
                _Deviceid = value;
                NotifyPropertyChanged("Deviceid");
            }
        }

        private int _Channelno;
        /// <summary>
        /// 
        /// </summary>
        public int Channelno
        {
            get { return _Channelno; }
            set
            {
                _Channelno = value;
                NotifyPropertyChanged("Channelno");
            }
        }

        private string _Channelname;
        /// <summary>
        /// 
        /// </summary>
        public string Channelname
        {
            get { return _Channelname; }
            set
            {
                _Channelname = value;
                NotifyPropertyChanged("Channelname");
            }
        }

        private string _Value0Name;
        /// <summary>
        /// 
        /// </summary>
        public string Value0Name
        {
            get { return _Value0Name; }
            set
            {
                _Value0Name = value;
                NotifyPropertyChanged("Value0Name");
            }
        }

        private string _Value1Name;
        /// <summary>
        /// 
        /// </summary>
        public string Value1Name
        {
            get { return _Value1Name; }
            set
            {
                _Value1Name = value;
                NotifyPropertyChanged("Value1Name");
            }
        }

        private float _Currentvalue;
        /// <summary>
        /// 
        /// </summary>
        public float Currentvalue
        {
            get { return _Currentvalue; }
            set
            {
                _Currentvalue = value;
                NotifyPropertyChanged("Currentvalue");
            }
        }

        private string _Channelparam;
        /// <summary>
        /// 
        /// </summary>
        public string Channelparam
        {
            get { return _Channelparam; }
            set
            {
                _Channelparam = value;
                NotifyPropertyChanged("Channelparam");
            }
        }

        /// <summary>
        /// Channel构造函数
        /// </summary>
        public ChannelOR()
        {

        }

        /// <summary>
        /// Channel构造函数
        /// </summary>
        public ChannelOR(DataRow row)
        {
            // 
            _Deviceid = Convert.ToInt32(row["DeviceID"]);
            // 
            _Channelno = Convert.ToInt32(row["ChannelNo"]);
            // 
            _Channelname = row["ChannelName"].ToString().Trim();
            // 
            _Value0Name = row["Value0_Name"].ToString().Trim();
            // 
            _Value1Name = row["Value1_Name"].ToString().Trim();
            // 
            if (DBNull.Value != row["CurrentValue"])
                _Currentvalue = float.Parse(row["CurrentValue"].ToString());
            // 
            _Channelparam = row["ChannelParam"].ToString().Trim();
        }

        public void Clone(ChannelOR obj)
        {
            //
            Deviceid = obj.Deviceid;
            //
            Channelno = obj.Channelno;
            //
            Channelname = obj.Channelname;
            //
            Value0Name = obj.Value0Name;
            //
            Value1Name = obj.Value1Name;
            //
            Currentvalue = obj.Currentvalue;
            //
            Channelparam = obj.Channelparam;

        }

    }
}

