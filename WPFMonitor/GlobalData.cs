using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;

namespace WPFMonitor
{
   public  class GlobalData
    {
       /// <summary>
       /// 配置的窗口号
       /// </summary>
       public static string WindowNo { get; set; }

       /// <summary>
       /// 登录的用户编号
       /// </summary>
       public static string UserID { get; set; }

       public static ObservableCollection<string> DateHH = new ObservableCollection<string>() 
       { "00", "01","02","03","04","05"
       ,"06", "07","08","09","10","11"
       ,"12", "13","14","15","16","17"
       ,"18", "19","20","21","22","23"};

       public static ObservableCollection<string> DateMi = new ObservableCollection<string>() 
       { "00", "01","02","03","04","05"
       ,"06", "07","08","09","10","11"
       ,"12", "13","14","15","16","17"
       ,"18", "19","20","21","22","23"
       ,"24","25","26","27","28","29"
       ,"30","31","32","33","34","35"
       ,"36","37","38","39","40","41"
       ,"42","43","44","45","46","47"
       ,"48","49","50","51","52","53"
       ,"54","55","56","57","58","59"
       };

       public static void ShowMsgError(string msg)
       {
           MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
       }

       public static string GetStartpath()
       {
           string temp = AppDomain.CurrentDomain.BaseDirectory;
           if (!temp.EndsWith("\\"))
           {
               temp += "\\";
           }
           return temp;
       }

       /// <summary>
       /// 项目背景起动路径
       /// </summary>
       /// <returns></returns>
       public static string GetAppBgPath()
       {
         return   GlobalData.GetStartpath() + "Upload\\ImageMap\\";
       }

    }
}
