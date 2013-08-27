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
using DockingLibrary;
using WPFMonitor.Core.TPControls;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// ControlWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ControlWindow : DockableContent
    {
        public static ControlWindow Instance { get; private set; }
        public ControlWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ControlWindow_Loaded);
            TPButton.IsChecked = true;
            Instance = this;
        }

        void ControlWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ControlVM.Instance;
        }

        void Control_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex < 1)
            {
                LoadScreen._instance.UnAddElementModel();
            }
            else
            {
                GalleryWindow.Instance.ResetSelected();
                LoadScreen._instance.AddElementModel();
            }
        }

        public t_Control GetSelected()
        {
            ListBox listBox = null;
            if (TPButton.IsChecked == true)
            {
                listBox = tpListBox;
            }
            else if (ZTButton.IsChecked == true)
            {
                listBox = ztListBox;
            }
            else if (GGButton.IsChecked == true)
            {
                listBox = ggListBox;
            }
            if (null != listBox && listBox.SelectedIndex > 0)
            {
                return listBox.SelectedItem as t_Control;
            }
            return null;
        }

        public void ResetSelected()
        {
            if (null != tpListBox && tpListBox.Items.Count > 0)
            {
                tpListBox.SelectedIndex = 0;
            }
            if (null != ztListBox && ztListBox.Items.Count > 0)
            {
                ztListBox.SelectedIndex = 0;
            }
            if (null != ggListBox && ggListBox.Items.Count > 0)
            {
                ggListBox.SelectedIndex = 0;
            }
        }

        private void accordion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetSelected();
        }

        private void TPButton_Checked(object sender, RoutedEventArgs e)
        {
            tpListBox.Visibility = Visibility.Visible;
            TPRow.Height = new GridLength(1, GridUnitType.Star);
            ztListBox.Visibility = Visibility.Collapsed;
            ggListBox.Visibility = Visibility.Collapsed;
            ZTRow.Height = new GridLength(1, GridUnitType.Auto);
            GGRow.Height = new GridLength(1, GridUnitType.Auto);

            ZTButton.IsChecked = false;
            GGButton.IsChecked = false;
        }

        private void ZTButton_Checked(object sender, RoutedEventArgs e)
        {
            ztListBox.Visibility = Visibility.Visible;
            ZTRow.Height = new GridLength(1, GridUnitType.Star);
            tpListBox.Visibility = Visibility.Collapsed;
            ggListBox.Visibility = Visibility.Collapsed;
            TPRow.Height = new GridLength(1, GridUnitType.Auto);
            GGRow.Height = new GridLength(1, GridUnitType.Auto);

            TPButton.IsChecked = false;
            GGButton.IsChecked = false;
        }

        private void GGButton_Checked(object sender, RoutedEventArgs e)
        {
            ggListBox.Visibility = Visibility.Visible;
            GGRow.Height = new GridLength(1, GridUnitType.Star);
            ztListBox.Visibility = Visibility.Collapsed;
            tpListBox.Visibility = Visibility.Collapsed;
            ZTRow.Height = new GridLength(1, GridUnitType.Auto);
            TPRow.Height = new GridLength(1, GridUnitType.Auto);

            ZTButton.IsChecked = false;
            TPButton.IsChecked = false;
        }
    }
}
