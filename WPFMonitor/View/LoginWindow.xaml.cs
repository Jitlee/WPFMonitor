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

            try
            {
               
                //GlobalData.WindowNo = windowNo;
            }
            catch (Exception cex)
            {
                MessageBox.Show(cex.Message);
            }
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.Focus();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            var userName = UserNameTextBox.Text.Trim();
            var password = PasswordBox.Password;
            MessageTextBlock.Text = string.Empty;
            MessageTextBlock.Foreground = Brushes.Red;
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageTextBlock.Text = "* 请输入用户名";
                UserNameTextBox.Focus();
                return;
            }
            else if (password.Length == 0)
            {
                PasswordBox.Focus();
                return;
            }

            string mErrorMsg = string.Empty;
            if (LoginViewModel.Instance.Login(userName, password, out mErrorMsg))
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
        }
    
    }
}
