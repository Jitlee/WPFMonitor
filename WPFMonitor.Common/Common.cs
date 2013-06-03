using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace WPFMonitor
{
    public class Common
    {
        public readonly static string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string GetAppPath(string path)
        {
            if (!Directory.Exists(AppPath))
            {
                Directory.CreateDirectory(AppPath);
            }
            return Path.Combine(AppPath, path);
        }

        public static string GetAppPath(string path1, string path2)
        {
            if (!Directory.Exists(AppPath))
            {
                Directory.CreateDirectory(AppPath);
            }
            return Path.Combine(Path.Combine(AppPath, path1), path2);
        }

        public readonly static string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EasyCodeword");

        public readonly static string TempFile = GetAppPath("tmp");

        public static string GetAppDataPath(string path)
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
            return Path.Combine(AppDataPath, path);
        }

        public static string GetAppDataPath(string path1, string path2)
        {
            if (!Directory.Exists(AppDataPath))
            {
                Directory.CreateDirectory(AppDataPath);
            }
            return Path.Combine(Path.Combine(AppDataPath, path1), path2);
        }


        /// <summary>
        /// 判断当前是否具有管理员权限
        /// </summary>
        public static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
