using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WPFMonitor.Core.ViewModel
{
    public class LoginViewModel
    {
        private static readonly LoginViewModel _instance = new LoginViewModel();
        public static LoginViewModel Instance { get { return _instance; } }

        public bool Login(string userid, string password,out string errorMsg)
        {
            //errorMsg = string.Empty;
            //using (var client = new QueueClientSoapClient())
            //{
            //    string msg = client.getLogin(userid, password, GlobalData.WindowNo);
            //    if (msg != "0")
            //    {
            //        errorMsg = msg;
            //        return false;
            //    }
            //    GlobalData.UserID = userid.Trim();
            //}
            errorMsg = "";
            return true;
        }
    }
}
