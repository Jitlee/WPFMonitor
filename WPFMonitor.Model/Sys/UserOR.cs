using System;
using System.Data;

namespace WPFMonitor.Model.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class UserOR : ORBase
    {

        private int _Userid;
        /// <summary>
        /// 
        /// </summary>
        public int Userid
        {
            get { return _Userid; }
            set
            {
                _Userid = value;
                NotifyPropertyChanged("Userid");
            }
        }

        private string _Userpsw;
        /// <summary>
        /// 密码
        /// </summary>
        public string Userpsw
        {
            get { return _Userpsw; }
            set
            {
                _Userpsw = value;
                NotifyPropertyChanged("Userpsw");
            }
        }

        private int _Usertype;
        /// <summary>
        /// 用户类型
        /// </summary>
        public int Usertype
        {
            get { return _Usertype; }
            set
            {
                _Usertype = value;
                NotifyPropertyChanged("Usertype");
            }
        }
        private string _UserTypeShow;
        public string UserTypeShow
        {
            set
            {
                if (value == "管理员")
                    _Usertype = 1;
                else
                    _Usertype = 0;
                _UserTypeShow = value;
                NotifyPropertyChanged("UserTypeShow");
            }
            get
            {
                if (_Usertype == 1)
                    _UserTypeShow= "管理员";
                else
                    _UserTypeShow= "操作员";
                return _UserTypeShow;
            }
        }

        private string _Username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                NotifyPropertyChanged("Username");
            }
        }

        /// <summary>
        /// User构造函数
        /// </summary>
        public UserOR()
        {

        }

        /// <summary>
        /// User构造函数
        /// </summary>
        public UserOR(DataRow row)
        {
            // 
            _Userid = Convert.ToInt32(row["UserID"]);
            // 密码
            _Userpsw = row["UserPSW"].ToString().Trim();
            // 用户类型
            _Usertype = Convert.ToInt32(row["UserType"]);
            // 用户名
            _Username = row["UserName"].ToString().Trim();
        }

        public void Clone(UserOR obj)
        {
            //
            Userid = obj.Userid;
            //密码
            Userpsw = obj.Userpsw;
            //用户类型
            Usertype = obj.Usertype;
            UserTypeShow = obj.UserTypeShow;
            //用户名
            Username = obj.Username;
        }

    }
}

