using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace WPFMonitor.Library.Controls
{
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_Mask", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "Part_Background", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Root", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Canvas", Type = typeof(Canvas))]
    [TemplatePart(Name = "PART_ItemsPresenter", Type = typeof(ItemsPresenter))]
    public class MenuScript : ItemsControl
    {
        public static DependencyProperty IsOpenedProperty =
               DependencyProperty.Register("IsOpened", typeof(bool), typeof(MenuScript), new PropertyMetadata(default(bool)));

        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }

        public event EventHandler Opened;
        private Popup _popup;
        private FrameworkElement _mask;
        private FrameworkElement _background;
        private Grid _root;
        private Canvas _canvas;
        private ItemsPresenter _itemsPresenter;
        public MenuScript()
        {
            base.DefaultStyleKey = typeof(MenuScript);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _popup = GetTemplateChild("PART_Popup") as Popup;
                _mask = GetTemplateChild("PART_Mask") as FrameworkElement;
                _background = GetTemplateChild("Part_Background") as FrameworkElement;
                _root = GetTemplateChild("PART_Root") as Grid;
                _canvas = GetTemplateChild("PART_Canvas") as Canvas;
                _itemsPresenter = GetTemplateChild("PART_ItemsPresenter") as ItemsPresenter;

                _popup.Opened += Popup_Opened;
                _popup.Closed += Popup_Closed;
                _mask.MouseLeftButtonDown += Mask_MouseButtonDown;
                _mask.MouseRightButtonDown += Mask_MouseButtonDown;
            }
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            SetMaskLayout();
        }

        private void SetMaskLayout()
        {
            //_background.Width = _itemsPresenter.ActualWidth;
            //_background.Height = _itemsPresenter.ActualHeight;
            //_itemsPresenter.Width = _background.ActualWidth;
            //_itemsPresenter.Height = _background.ActualHeight;
            //_root.Children.Remove(_itemsPresenter);
            //_canvas.Children.Add(_itemsPresenter);
            var win = Window.GetWindow(this);
            _mask.Width = win.ActualWidth;
            _mask.Height = win.ActualHeight;
            //var translatePoint = _mask.TransformToVisual(Application.Current.RootVisual);
            //var point = translatePoint.Transform(new Point(0d, 0d));
            //_mask.SetValue(Canvas.LeftProperty, -point.X);
            //_mask.SetValue(Canvas.TopProperty, -point.Y);
            if (null != Opened)
            {
                Opened(this, EventArgs.Empty);
            }

        }

        private void Popup_Closed(object sender, EventArgs e)
        {
            foreach (MenuScriptItem item in Items)
            {
                item.ClickMode = ClickMode.Release;
            }

            //_canvas.Children.Remove(_itemsPresenter);
            //_root.Children.Add(_itemsPresenter);

            //_itemsPresenter.ClearValue(WidthProperty);
            //_itemsPresenter.ClearValue(HeightProperty);

            //_mask.SetValue(Canvas.LeftProperty, 0d);
            //_mask.SetValue(Canvas.TopProperty, 0d);

            CloseAllItems();
        }

        public void CloseAllItems()
        {
            CloseAllItems(Items);
        }

        private void CloseAllItems(ItemCollection items)
        {
            foreach(MenuScriptItem item in items)
            {
                CloseAllItems(item.Items);
                if (item.IsOpened)
                {
                    item.IsOpened = false;
                }
            }
        }

        private void Mask_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsOpened = false;
        }
    }
}
