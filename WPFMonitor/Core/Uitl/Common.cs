using System;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace WPFMonitor.Core.Util
{
    /// <summary>
    /// 通用帮助类
    /// </summary>
    public static class Common
    {
        public readonly static string ApplicationDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WPFMonitor");

        public readonly static string TempFile = GetAppPath("tmp");

        public static string GetAppPath(string path)
        {
            if (!Directory.Exists(ApplicationDataPath))
            {
                Directory.CreateDirectory(ApplicationDataPath);
            }
            return Path.Combine(ApplicationDataPath, path);
        }

        public static string GetAppPath(string path1, string path2)
        {
            if (!Directory.Exists(ApplicationDataPath))
            {
                Directory.CreateDirectory(ApplicationDataPath);
            }
            return Path.Combine(Path.Combine(ApplicationDataPath, path1), path2);
        }

        private static readonly bool _IsInDesignMode = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
        public static bool IsInDesignMode
        {
            get { return _IsInDesignMode; }
        }

        /// <summary>
        /// 获取文件安全路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetSafeFilePath(string path)
        {
            // 如果只有文件名, 打开文件对话框会影响当前默认目录, 所以需要试探下
            if (!path.Contains("\\"))
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }
            return path;
        }

        public static Color StringToColor(string htmlColr)
        {
            try
            {
                int baseIndex = 1;
                byte a, r, g, b; a = r = g = b = 255;
                if (htmlColr.Length == 9)
                {
                    a = Convert.ToByte(htmlColr.Substring(baseIndex, 2), 16); baseIndex += 2;
                }
                if (htmlColr.IndexOf("RGB(") == 0)
                {
                    string[] rgb = htmlColr.ToUpper().Replace("RGB(", "").Replace(")", "").Split(',');
                    if (rgb.Length == 3)//RGB(177,255,255)
                    {
                        r = Convert.ToByte(int.Parse(rgb[0]));
                        g = Convert.ToByte(int.Parse(rgb[1]));
                        b = Convert.ToByte(int.Parse(rgb[2]));
                    }
                }
                else
                {
                    r = Convert.ToByte(htmlColr.Substring(baseIndex, 2), 16);
                    g = Convert.ToByte(htmlColr.Substring(baseIndex += 2, 2), 16);
                    b = Convert.ToByte(htmlColr.Substring(baseIndex += 2, 2), 16);
                }
                return Color.FromArgb(a, r, g, b);

                //ColorTranslator.FromHtml(strColor);
            }
            catch
            {
                return Colors.White;
            }
        }

        ///// <summary>
        ///// 判断当前是否具有管理员权限
        ///// </summary>
        //public static bool IsAdmin()
        //{
        //    return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        //}

        #region  字体处理

        /// <summary>
        /// 中文转英文
        /// </summary>
        /// <returns></returns>
        public static string GetFontEn(string str)
        {
            switch (str.Trim())
            {
                case "隶书":
                    str = "LiSu";
                    break;
                case "幼圆":
                    str = "YouYuan";
                    break;
                case "舒体":
                    str = "FZShuTi";
                    break;
                case "姚体":
                    str = "FZYaoti";
                    break;
                case "楷体":
                    str = "STKaiti";
                    break;
                case "宋体":
                    str = "STSong";
                    break;
                case "中宋":
                    str = "STZhongsong";
                    break;
                case "仿宋":
                    str = "STFangsong";
                    break;
                case "彩云":
                    str = "STCaiyun";
                    break;
                case "琥珀":
                    str = "STHupo";
                    break;
                case "行楷":
                    str = "STXingkai";
                    break;
                case "新魏":
                    str = "STXinwei";
                    break;
                case "细黑":
                    str = "STXihei";
                    break;
                default:
                    str = "STSong";
                    break;
            }
            return str;
        }
        /// <summary>
        /// 英文转中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFontCN(string str)
        {
            switch (str.Trim())
            {
                case "LiSu":
                    str = "隶书";
                    break;
                case "YouYuan":
                    str = "幼圆";
                    break;
                case "FZShuTi":
                    str = "舒体";
                    break;
                case "FZYaoti":
                    str = "姚体";
                    break;
                case "STKaiti":
                    str = "楷体";
                    break;
                case "STSong":
                    str = "宋体";
                    break;
                case "STZhongsong":
                    str = "中宋";
                    break;
                case "STFangsong":
                    str = "仿宋";
                    break;
                case "STCaiyun":
                    str = "彩云";
                    break;
                case "STHupo":
                    str = "琥珀";
                    break;
                case "STXingkai":
                    str = "行楷";
                    break;
                case "STXinwei":
                    str = "新魏";
                    break;
                case "STXihei":
                    str = "细黑";
                    break;
                default:
                    str = "宋体";
                    break;
            }
            return str;
        }
        #endregion
    }
}
