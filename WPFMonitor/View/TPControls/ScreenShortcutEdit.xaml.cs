using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using WPFMonitor.Core.TPControls;
using WPFMonitor.DAL.ZTControls;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ScreenShortcutEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenShortcutEdit : Window
    {
        private readonly t_ScreenShortcut _shortcut = new t_ScreenShortcut();
        private readonly bool _isNew = true;

        public t_ScreenShortcut Shortcut { get { return _shortcut; } }

        public ScreenShortcutEdit()
        {
            InitializeComponent();
            this.Title = "添加设备信息";
            this.DataContext = _shortcut;
            this.ScreenCombobox.ItemsSource = ScreenTreeVM.Instance.AllScreens;
        }

        public ScreenShortcutEdit(t_ScreenShortcut shortcut)
            : this()
        {
            this.Title = "编辑设备信息";
            _shortcut = shortcut;
            this.DataContext = _shortcut;
            this.ScreenCombobox.SelectedItem = ScreenTreeVM.Instance.AllScreens.FirstOrDefault(s => s.ScreenID == _shortcut.ScreenId);
            _isNew = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (_shortcut.ScreenId <= 0)
            {
                MessageBox.Show(this, "请选择场景");
                return;
            }
            try
            {
                ScreenShortcutDA da = new ScreenShortcutDA();
                if (_isNew)
                {
                    da.Insert(_shortcut);
                }
                else
                {
                    da.Update(_shortcut);
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存数据发生异常，请与管理员联系！", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BrowserButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择关联场景图标";
            openFileDialog.Filter = "图片文件(*.jpg;*.bmp;*.png)|*.jpg;*.bmp;*.png";
            if (openFileDialog.ShowDialog(this) == true)
            {
                string fileName = openFileDialog.FileName;
                _shortcut.ImageBuffer = File.ReadAllBytes(fileName);
            }
        }

        private void ScreenCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScreenCombobox.SelectedIndex > -1)
            {
                _shortcut.ScreenId = Converter.ToInt32((ScreenCombobox.SelectedItem as t_Screen).ScreenID);
                _shortcut.ScreenName = (ScreenCombobox.SelectedItem as t_Screen).ScreenName;
            }
            else
            {
                _shortcut.ScreenId = 0;
            }
        }
    }
}
