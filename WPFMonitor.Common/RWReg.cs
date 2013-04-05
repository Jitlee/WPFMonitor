using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace WPFMonitor
{
    public class RWReg
    {
        private static readonly string DefaultKey = "覾轃主场双腕驞伽";

        static ILogger _logger = LoggerFactory.GetLogger(typeof(RWReg).FullName);

        public static object GetValue(string subName, string keyName, object defualtValue = null, bool unDecrypt = false)
        {
            return GetValue(Registry.CurrentUser, subName, keyName, defualtValue, unDecrypt);
        }

        public static object GetValue(RegistryKey registryKey, string subName, string keyName, object defualtValue = null, bool unDecrypt = false)
        {
            using (var rootKey = registryKey)
            {
                using (var subKey = rootKey.OpenSubKey(subName))
                {
                    if (null == subKey)
                    {
                        return defualtValue;
                    }
                    if (!unDecrypt)
                    {
                        var result = subKey.GetValue(keyName, null);
                        if (null != result)
                        {
                            return AES.Decrypt(result.ToString(), DefaultKey);
                        }
                        return defualtValue;
                    }
                    else
                    {
                        return subKey.GetValue(keyName, defualtValue);
                    }
                }
            }
        }

        public static void SetValue(string subName, string keyName, object value, bool unEncrypt = false)
        {
            SetValue(Registry.CurrentUser, subName, keyName, value, unEncrypt);
        }

        public static void SetValue(RegistryKey registryKey, string subName, string keyName, object value, bool unEncrypt = false)
        {
            using (var rootKey = registryKey)
            {
                using (var subKey = rootKey.OpenSubKey(subName, true))
                {
                    if (null == subKey)
                    {
                        using (var newSubKey = rootKey.CreateSubKey(subName))
                        {
                            if (!unEncrypt)
                            {
                                if (null != value)
                                {
                                    newSubKey.SetValue(keyName, AES.Encrypt(value.ToString(), DefaultKey));
                                }
                                else
                                {
                                    newSubKey.SetValue(keyName, value);
                                }
                            }
                            else
                            {
                                subKey.SetValue(keyName, value);
                            }
                        }
                    }
                    else
                    {
                        if (!unEncrypt)
                        {
                            if (null != value)
                            {
                                subKey.SetValue(keyName, AES.Encrypt(value.ToString(), DefaultKey));
                            }
                            else
                            {
                                subKey.SetValue(keyName, value);
                            }
                        }
                        else
                        {
                            subKey.SetValue(keyName, value);
                        }
                    }
                }
            }
        }

        public static void RemoveKey(string subName, string keyName)
        {
            RemoveKey(Registry.CurrentUser, subName, keyName);
        }

        public static void RemoveKey(RegistryKey registryKey, string subName, string keyName)
        {
            using (var rootKey = registryKey)
            {
                using (var subKey = rootKey.OpenSubKey(subName, true))
                {
                    if (null != subKey)
                    {
                        try
                        {
                            var key = subKey.OpenSubKey(keyName);
                            if (null != key)
                            {
                                subKey.DeleteValue(keyName);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Debug("RemoveKey", "Remove registrykey occur a error", ex.Message);
                        }
                    }
                }
            }
        }
    }
}
