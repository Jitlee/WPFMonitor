using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WPFMonitor.Core
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
            //    try
            //    {
            //        string msg = client.getLogin(userid, password, GlobalData.WindowNo);
            //        if (msg != "0")
            //        {
            //            errorMsg = msg;
            //            return false;
            //        }
            //    }
            //    catch (EndpointNotFoundException exEnd)
            //    {
            //        errorMsg = "配置Web服务不存在！";
            //        return false;
            //    }
            //    catch (Exception ex)
            //    {
            //        errorMsg = "登录失败！";
            //        return false;
            //    }
            //    EmployeeOR obj = client.GetEmployeeInfo(userid);
            //    GlobalData.UserID = userid.Trim();
            //    GlobalData.UserName = obj.Name;
            //}
            errorMsg = "";
            return true;
        }
    }
}
