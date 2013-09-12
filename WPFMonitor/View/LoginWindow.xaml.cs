using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using WPFMonitor.Core;
using System.Threading;
using System.Windows.Threading;

namespace WPFMonitor.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.Focus();
        }

        string userName = string.Empty;
        string password = string.Empty;
        string mErrorMsg = string.Empty;
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            tbWait.IsBusy = true;
            userName = UserNameTextBox.Text.Trim();
            password = PasswordBox.Password;
            MessageTextBlock.Text = string.Empty;
            MessageTextBlock.Foreground = Brushes.Red;
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageTextBlock.Text = "* 请输入用户名";
                UserNameTextBox.Focus();
                tbWait.IsBusy = false;
                return;
            }
            else if (password.Length == 0)
            {
                PasswordBox.Focus();
                tbWait.IsBusy = false;
                return;
            }
            
            Thread th = new Thread(LoginFun);
            th.IsBackground = true;
            th.Start();
        }

        private void LoginFun()
        {
            //Thread.Sleep(1000);
            bool LoginInfo=LoginViewModel.Instance.Login(userName, password, out mErrorMsg);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate()
            {
                if (LoginInfo)
                {
                    MessageTextBlock.Text = "登录成功，正在加载数据...";
                    MessageTextBlock.Foreground = Brushes.Blue;
                    this.DialogResult = true;
                    Close();
                }
                else
                {
                    MessageTextBlock.Text = mErrorMsg;
                    MessageTextBlock.Foreground = Brushes.Red;
                }
                tbWait.IsBusy = false;
            });
        }
    
    }
}
