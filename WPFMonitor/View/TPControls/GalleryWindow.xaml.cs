using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DockingLibrary;
using MonitorSystem.Gallery;
using WPFMonitor.DAL.ZTControls;
using WPFMonitor.Model.ZTControls;
using MonitorSystem.MonitorSystemGlobal;

namespace WPFMonitor.View.TPControls
{
    /// <summary>
    /// GalleryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GalleryWindow : DockableContent
    {
        public static GalleryWindow Instance { get; private set; }
        private ControlDA _da = new ControlDA();
        public GalleryWindow()
        {
            InitializeComponent();

            //var createCommand = new DelegateCommand<t_Control>(Create);
            //GalleryListBox.SetValue(MouseDoubleClick.CommandProperty, createCommand);

            Instance = this;

            LoadingBusyIndicator.IsBusy = true;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var list = new GalleryClassificationDA().selectAllDate();
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.GalleryClassificationListBox.ItemsSource = list;
                        LoadingBusyIndicator.IsBusy = false;
                    }));
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(new Action(() =>
                       {
                           MessageBox.Show("加载图库分类数据失败", ex.Message);
                           LoadingBusyIndicator.IsBusy = false;
                       }));
                }
            });
        }

        private void GalleryClassificationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GalleryClassificationListBox.SelectedIndex == -1)
            {
                this.GalleryListBox.ItemsSource = null;
                return;
            }
            LoadingBusyIndicator.IsBusy = true;
            var galleryClassification = GalleryClassificationListBox.SelectedItem as t_GalleryClassification;
            if (null != galleryClassification)
            {
                //LoadScreen._DataContext.Load(LoadScreen._DataContext.GetT_ControlByTypeQuery(galleryClassification.Id), GetT_ControlByTypeQueryCallback, null);

                LoadingBusyIndicator.IsBusy = true;
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var list = _da.SelectBy(galleryClassification.Id);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            GetT_ControlByTypeQueryCallback(list);
                            LoadingBusyIndicator.IsBusy = false;
                        }));
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(new Action(() =>
                           {
                               MessageBox.Show("加载图库分类数据失败", ex.Message);
                               LoadingBusyIndicator.IsBusy = false;
                           }));
                    }
                });
            }
        }

        private void GetT_ControlByTypeQueryCallback(List<t_Control> result)
        {
            //this.GalleryListBox.ItemsSource = result.Entities;
            try
            {
                this.GalleryListBox.Items.Clear();

                var pointer = new Pointer();
                pointer.Height = 93d;
                pointer.Width = 93d;
                this.GalleryListBox.Items.Add(new ListBoxItem() { Content = pointer, Height = 100d, Width = 100d });

                foreach (var t in result)
                {
                    var item = new ListBoxItem();
                    item.DataContext = t;
                    try
                    {
						if (!string.IsNullOrEmpty(t.ImageURL) && t.ImageURL == "MonitorSystem.Other.RealTimeT")
						{
							string url = "/WPFMonitor;component/Resources/Images/RealtimeBG.jpg";
							BitmapImage bitmap = new BitmapImage(new Uri(url, UriKind.Relative));
							ImageSource mm = bitmap;
							Image _img = new Image();

							_img.Source = mm;

							var control = _img as FrameworkElement;
							if (null != control)
							{
								control.Height = 93d;
								control.Width = 93d;
								item.Content = control;
							}
						}
						else 
						if (!string.IsNullOrEmpty(t.ImageURL))
                        {
                            var instance = Activator.CreateInstance(MonitorControl.GetType(t.ImageURL));
                            var control = instance as FrameworkElement;
                            if (null != control)
                            {
                                control.Height = 93d;
                                control.Width = 93d;
                                item.Content = control;
                            }
                            else
                            {
                                item.Content = new TextBlock() { Text = t.ControlName, TextTrimming = TextTrimming.WordEllipsis };
                            }
                        }
                        else
                        {
                            item.Content = new TextBlock() { Text = t.ControlName, TextTrimming = TextTrimming.WordEllipsis };
                        }
                    }
                    catch
                    {
                        item.Content = new TextBlock() { Text = t.ControlName, TextTrimming = TextTrimming.WordEllipsis };
                    }
                    item.Height = 100d;
                    item.Width = 100d;
                    this.GalleryListBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
            LoadingBusyIndicator.IsBusy = false;
        }

        private void Create(t_Control tControl)
        {
            if (null != tControl)
            {
                LoadScreen._instance.CreateControl(LoadScreen._instance.csScreen, tControl, 150, 150, 0, 0);
            }
        }

        private void GalleryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GalleryListBox.SelectedIndex < 1)
            {
                LoadScreen._instance.UnAddElementModel();
            }
            else
            {
                ControlWindow.Instance.ResetSelected();
                LoadScreen._instance.AddElementModel();
            }
        }

        public t_Control GetSelected()
        {
            if (null != GalleryListBox
                && GalleryListBox.SelectedItem is ListBoxItem)
            {
                return (GalleryListBox.SelectedItem as ListBoxItem).DataContext as t_Control;
            }
            return null;
        }

        public void ResetSelected()
        {
            if (GalleryListBox.Items.Count > 0)
            {
                GalleryListBox.SelectedIndex = 0;
            }
        }

        private void GalleryListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (null != element && element.DataContext is t_Control)
            {
                Create(element.DataContext as t_Control);
            }
        }
    }
}
