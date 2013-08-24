using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFMonitor.DAL.Sys;
using WPFMonitor.Model.Sys;


namespace WPFMonitor.Core
{
    public class LoginViewModel
    {
        private static readonly LoginViewModel _instance = new LoginViewModel();
        public static LoginViewModel Instance { get { return _instance; } }

        public bool Login(string userid, string password,out string errorMsg)
        {
            try
            {
                UserOR mUser = new UserDA().Login(userid, password);
                if (mUser != null)
                {
                    errorMsg = "登录成功！";
                    GlobalData.UserName = mUser.Username;
                    return true;
                }
                else
                {
                    errorMsg = "用户名或密码错误！";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return false;
        }
    }
}
