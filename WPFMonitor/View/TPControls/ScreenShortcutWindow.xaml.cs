using DockingLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFMonitor.DAL.ZTControls;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ScreenShortcutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenShortcutWindow : DockableContent
    {
        private ObservableCollection<t_ScreenShortcut> _items;
        public ScreenShortcutWindow()
        {
            InitializeComponent();
            try
            {
                this.ScreenShortcutListBox.ItemsSource = _items = new ScreenShortcutDA().Select();
            }
            catch
            {

            }
        }

        private void NewDevice_Click(object sender, RoutedEventArgs e)
        {
            if(null == _items)
            {
                return;
            }

            ScreenShortcutEdit editWindow = new ScreenShortcutEdit();
            editWindow.Owner = Application.Current.MainWindow;
            if (editWindow.ShowDialog() == true)
            {
                _items.Add(editWindow.Shortcut);
            }
        }

        private void EditDevice_Click(object sender, RoutedEventArgs e)
        {
            ScreenShortcutEdit editWindow = new ScreenShortcutEdit(ScreenShortcutListBox.SelectedItem as t_ScreenShortcut);
            editWindow.Owner = Application.Current.MainWindow;
            editWindow.ShowDialog();
        }

        private void RemoveDevice_Click(object sender, RoutedEventArgs e)
        {
            new ScreenShortcutDA().Delete((ScreenShortcutListBox.SelectedItem as t_ScreenShortcut).Id);
        }

        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            NewDeviceMenuItem.IsEnabled = null != _items;
            if ((e.OriginalSource as FrameworkElement).DataContext is t_ScreenShortcut)
            {
                EditDeviceMenuItem.IsEnabled = true;
                RemoveDeviceMenuItem.IsEnabled = true;
            }
            else
            {
                EditDeviceMenuItem.IsEnabled = false;
                RemoveDeviceMenuItem.IsEnabled = false;
            }
        }
    }
}
