using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace EasyCodeword.Utilities
{
    /// <summary>
    /// 常用对象转换帮助类
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// 将字符串对象转换成 Boolean
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static bool ToBoolean(object o, bool defaultValue = default(bool))
        {
            if (null != o)
            {
                bool result;
                if (bool.TryParse(o.ToString(), out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        ///// <summary>
        ///// 将字符串对象转换成 Brush
        ///// </summary>
        ///// <param name="o">要转换的字符串对象</param>
        ///// <param name="defaultValue">默认值</param>
        ///// <returns>返回转化好的值</returns>
        //public static Brush ToBrush(object o, Brush defaultValue = default(Brush))
        //{
        //    if (null != o)
        //    {
        //        var result = (Brush)new BrushConverter().ConvertFromString(o.ToString());
        //        if (null != result)
        //        {
        //            return result;
        //        }
        //    }
        //    return defaultValue;
        //}

        ///// <summary>
        ///// 将字符串对象转换成 Color
        ///// </summary>
        ///// <param name="o">要转换的字符串对象</param>
        ///// <param name="defaultValue">默认值</param>
        ///// <returns>返回转化好的值</returns>
        //public static Color ToColor(object o, Color defaultValue = default(Color))
        //{
        //    if (null != o)
        //    {
        //        var result = (Color)ColorConverter.ConvertFromString(o.ToString());
        //        if (null != result)
        //        {
        //            return result;
        //        }
        //    }
        //    return defaultValue;
        //}

        /// <summary>
        /// 将字符串对象转换成 Int32
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static int ToInt(object o, int defaultValue = default(int))
        {
            if (null != o)
            {
                int result;
                if (int.TryParse(o.ToString(), out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串对象转换成 Char
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static char ToChar(object o, char defaultValue = default(char))
        {
            if (null != o)
            {
                char result;
                if (char.TryParse(o.ToString(), out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串对象转换成 double
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static double ToDouble(object o, double defaultValue = default(double))
        {
            if (null != o)
            {
                double result;
                if (double.TryParse(o.ToString(), out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串对象转换成 decimal
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static decimal ToDecimal(object o, decimal defaultValue = default(decimal))
        {
            if (null != o)
            {
                decimal result;
                if (decimal.TryParse(o.ToString(), out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串对象转换成 DataTime
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static DateTime ToDateTime(object o, DateTime defaultValue = default(DateTime))
        {
            if (null != o)
            {
                DateTime result;
                if (DateTime.TryParse(o.ToString(), out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串对象转换成枚举对象
        /// </summary>
        /// <param name="o">要转换的字符串对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回转化好的值</returns>
        public static TResult ToEnum<TResult>(object o, TResult defaultValue = default(TResult)) where TResult : struct
        {
            if (null != o)
            {
                Type type = typeof(TResult);
                if (!type.IsEnum)
                {
                    throw new NotSupportedException("TResult must be an Enum.");
                }

                TResult result;
                if (Enum.TryParse(o.ToString(), out result))
                {
                    return (TResult)System.Convert.ChangeType(result, type);
                }
            }
            return defaultValue;
        }
    }
}
