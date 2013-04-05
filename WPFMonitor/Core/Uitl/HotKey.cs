using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace WPFMonitor.Core.Uitl
{
  
   public class HotKey
    {
        #region Member

        int KeyId;         //热键编号
        IntPtr Handle;     //窗体句柄
        Window Window;     //热键所在窗体
        uint ControlKey;   //热键控制键
        uint Key;          //热键主键

        public delegate void OnHotKeyEventHandler();     //热键事件委托
        public event OnHotKeyEventHandler OnHotKey = null;   //热键事件

        static Hashtable KeyPair = new Hashtable();         //热键哈希表
        private const int WM_HOTKEY = 0x0312;       // 热键消息编号

        public enum KeyFlags    //控制键编码        
        {
            MOD_ALT = 0x1,
            MOD_CONTROL = 0x2,
            MOD_SHIFT = 0x4,
            MOD_WIN = 0x8
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="win">注册窗体</param>
        /// <param name="control">控制键</param>
        /// <param name="key">主键</param>
        public HotKey(Window win, HotKey.KeyFlags control, Keys key)
        {
         

            Handle = new WindowInteropHelper(win).Handle;
            Window = win;
            ControlKey = (uint)control;
            Key = (uint)key;
            KeyId = (int)ControlKey + (int)Key * 10;

            if (HotKey.KeyPair.ContainsKey(KeyId))
            {
                throw new Exception("热键已经被注册!");
            }

            //注册热键
            if (false == HotKey.RegisterHotKey(Handle, KeyId, ControlKey, Key))
            {
                throw new Exception("热键注册失败!");
            }

            //消息挂钩只能连接一次!!
            if (HotKey.KeyPair.Count == 0)
            {
                if (false == InstallHotKeyHook(this))
                {
                    throw new Exception("消息挂钩连接失败!");
                }
            }
            if (HotKey.KeyPair.Count > 0)
                HotKey.KeyPair.Clear();
            HotKey.KeyPair.Clear();
            //添加这个热键索引
            HotKey.KeyPair.Add(KeyId, this);
        }

        public void UnHotKey()
        {
            HotKey.UnregisterHotKey(Handle, KeyId);
        }

        //析构函数,解除热键
        ~HotKey()
        {
            HotKey.UnregisterHotKey(Handle, KeyId);
        }

        #region core

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        //安装热键处理挂钩
        static private bool InstallHotKeyHook(HotKey hk)
        {
            if (hk.Window == null || hk.Handle == IntPtr.Zero)
            {
                return false;
            }

            //获得消息源
            System.Windows.Interop.HwndSource source = System.Windows.Interop.HwndSource.FromHwnd(hk.Handle);
            if (source == null)
            {
                return false;
            }

            //挂接事件            
            source.AddHook(HotKey.HotKeyHook);            
            return true;
        }

        /// <summary>
        /// 热键处理过程
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        static private IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                HotKey hk = (HotKey)HotKey.KeyPair[(int)wParam];
                if (hk.OnHotKey != null)
                {
                    hk.OnHotKey();
                }
            }
            return IntPtr.Zero;
        }

        #endregion
    }

   public class HotKeyCollection
   {
       Dictionary<string, Keys> KeysCollection = new Dictionary<string, Keys>();

       public Keys GetKeys(string mkey)
       {
           return KeysCollection[mkey];
       }

       public HotKeyCollection()
       {
           KeysCollection.Add("A", Keys.A);
           KeysCollection.Add("B", Keys.B);
           KeysCollection.Add("C", Keys.C);
           KeysCollection.Add("D", Keys.D);
           KeysCollection.Add("E", Keys.E);
           KeysCollection.Add("F", Keys.F);
           KeysCollection.Add("G", Keys.G);
           KeysCollection.Add("H", Keys.H);
           KeysCollection.Add("I", Keys.I);
           KeysCollection.Add("J", Keys.J);
           KeysCollection.Add("K", Keys.K);
           KeysCollection.Add("L", Keys.L);
           KeysCollection.Add("M", Keys.M);
           KeysCollection.Add("N", Keys.N);
           KeysCollection.Add("O", Keys.O);
           KeysCollection.Add("P", Keys.P);
           KeysCollection.Add("Q", Keys.Q);
           KeysCollection.Add("R", Keys.R);
           KeysCollection.Add("S", Keys.S);
           KeysCollection.Add("T", Keys.T);
           KeysCollection.Add("U", Keys.U);
           KeysCollection.Add("V", Keys.V);
           KeysCollection.Add("W", Keys.W);
           KeysCollection.Add("X", Keys.X);
           KeysCollection.Add("Y", Keys.Y);
           KeysCollection.Add("Z", Keys.Z);

           KeysCollection.Add("0", Keys.D0);
           KeysCollection.Add("1", Keys.D1);
           KeysCollection.Add("2", Keys.D2);
           KeysCollection.Add("3", Keys.D3);
           KeysCollection.Add("4", Keys.D4);
           KeysCollection.Add("5", Keys.D5);
           KeysCollection.Add("6", Keys.D6);
           KeysCollection.Add("7", Keys.D7);
           KeysCollection.Add("8", Keys.D8);
           KeysCollection.Add("9", Keys.D9);

           KeysCollection.Add("F1", Keys.F1);
           KeysCollection.Add("F2", Keys.F2);
           KeysCollection.Add("F3", Keys.F3);
           KeysCollection.Add("F4", Keys.F4);
           KeysCollection.Add("F5", Keys.F5);
           KeysCollection.Add("F6", Keys.F6);
           KeysCollection.Add("F7", Keys.F7);
           KeysCollection.Add("F8", Keys.F8);
           KeysCollection.Add("F9", Keys.F9);
           KeysCollection.Add("F10", Keys.F10);
           KeysCollection.Add("F11", Keys.F11);
           KeysCollection.Add("F12", Keys.F12);
       }

   }
}
