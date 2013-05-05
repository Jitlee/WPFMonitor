using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFMonitor.Library.Controls
{
    [TemplatePart(Name = "PART_Button", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_Mask", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Path", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Top", Type = typeof(FrameworkElement))]
    public class MenuScriptItem : ItemsControl
    {
        public event RoutedEventHandler Click;
        public static DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(MenuScriptItem), new PropertyMetadata(null));

        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MenuScriptItem), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(MenuScriptItem), new PropertyMetadata(null));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static DependencyProperty IsOpenedProperty =
            DependencyProperty.Register("IsOpened", typeof(bool), typeof(MenuScriptItem), new PropertyMetadata(default(bool), OnIsOpenedPropertyChanged));

        public bool IsOpened
        {
            get { return (bool)GetValue(IsOpenedProperty); }
            set { SetValue(IsOpenedProperty, value); }
        }

        static void OnIsOpenedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var MenuScriptItem = d as MenuScriptItem;
            MenuScriptItem.OnIsOpendChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        private void OnIsOpendChanged(bool oldValue, bool newValue)
        {
            if (this.Items.Count > 0)
            {
                _popup.IsOpen = newValue;
            }
            this._button.IsChecked = newValue;
        }

        public static DependencyProperty ClickModeProperty =
            DependencyProperty.Register("ClickMode", typeof(ClickMode), typeof(MenuScriptItem), new PropertyMetadata(ClickMode.Hover));

        public ClickMode ClickMode
        {
            get { return (ClickMode)GetValue(ClickModeProperty); }
            set { SetValue(ClickModeProperty, value); }
        }

        public static DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(MenuScriptItem), new PropertyMetadata(null));

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private ToggleButton _button;
        private Popup _popup;
        private FrameworkElement _mask;
        private FrameworkElement _path;
        private FrameworkElement _top;
        private MenuScript __MenuScript;
        private MenuScript _MenuScript
        {
            get
            {
                if (null == __MenuScript)
                {
                    __MenuScript = FindVisualParent<MenuScript>(this);
                }
                return __MenuScript;
            }
        }

        public MenuScriptItem()
        {
            base.DefaultStyleKey = typeof(MenuScriptItem);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                _button = GetTemplateChild("PART_Button") as ToggleButton;
                _popup = GetTemplateChild("PART_Popup") as Popup;
                _mask = GetTemplateChild("PART_Mask") as FrameworkElement;
                _path = GetTemplateChild("PART_Path") as FrameworkElement;
                _top = GetTemplateChild("PART_Top") as FrameworkElement;

                _mask.MouseLeftButtonUp += Mask_MouseLeftButtonUp;

                _button.Checked += Button_Checked;
                _button.Unchecked += Button_Unchecked;
                _popup.Opened += Popup_Opened;
                _popup.Closed += Popup_Closed;
                if (null != _MenuScript)
                {
                    _button.MouseLeftButtonDown += Button_MouseLeftButtonDown;
                    ClickMode = ClickMode.Release;
                    _mask.MouseLeftButtonDown += Mask_MouseButtonDown;
                    _mask.MouseRightButtonDown += Mask_MouseButtonDown;
                    _popup.SetValue(VerticalAlignmentProperty, VerticalAlignment.Bottom);
                    _popup.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                    _popup.VerticalOffset = -1d;
                    _button.SetValue(StyleProperty, Application.Current.Resources["MenuScriptToggleButtonStyle"]);
                    _top.Visibility = Visibility.Visible;
                }
                else
                {
                    this.MouseLeave += Button_MouseLeave;
                    if (Items.Count > 0)
                    {
                        _path.Visibility = Visibility.Visible;
                    }
                    _top.Visibility = Visibility.Collapsed;
                }
                _mask.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsOpened)
            {
                CloseTopMenuScript();
            }
        }

        private void Mask_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CloseTopMenuScript();
            if (null != Command && Command.CanExecute(CommandParameter))
            {
                CloseTopMenuScript();
                Command.Execute(CommandParameter);
            }
            else if (null != Click)
            {
                CloseTopMenuScript();
                Click(this, new RoutedEventArgs() {  });
            }
        }

        private void CloseTopMenuScript()
        {
            var menuScript = FindVisualParent<MenuScript>(this);
            if (null != menuScript || null != (menuScript = FindParent<MenuScript>(this)))
            {
                menuScript.IsOpened = false;
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Items.Count == 0)
            {
                this.IsOpened = false;
            }
        }

        private void Button_Checked(object sender, RoutedEventArgs e)
        {
            if (_isFirstOpen && null != _MenuScript &&  _MenuScript.Items.IndexOf(this) != 0)
            {
                _isFirstOpen = false;
                _button.IsChecked = false;
                return;
            }

            if (null != _MenuScript)
            {
                _MenuScript.CloseAllItems();
            }
            else
            {
                (Parent as MenuScriptItem).CloseAllItems();
            }
            if (null != _MenuScript && !_MenuScript.IsOpened)
            {
                _MenuScript.Opened -= MenuScript_Opened;
                _MenuScript.Opened += MenuScript_Opened;
                _MenuScript.IsOpened = true;
            }
            else
            {
                IsOpened = true;
            }
            _mask.Visibility = Visibility.Visible;
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

        private void MenuScript_Opened(object sender, EventArgs e)
        {
            _MenuScript.Opened -= MenuScript_Opened;
            IsOpened = true;
        }

        private void Button_Unchecked(object sender, RoutedEventArgs e)
        {
            IsOpened = false;
            _mask.Visibility = Visibility.Collapsed;
        }

        private T FindVisualParent<T>(UIElement control) where T : UIElement
        {
            UIElement p = VisualTreeHelper.GetParent(control) as UIElement;
            if (p != null)
            {
                if (p is T)
                    return p as T;
                else
                    return FindVisualParent<T>(p);
            }
            return null;
        }

        private T FindParent<T>(FrameworkElement control) where T : FrameworkElement
        {
            if (control == null)
            {
                return null;
            }
            var p = control.Parent as FrameworkElement;
            if (p != null)
            {
                if (p is T)
                    return p as T;
                else
                    return FindParent<T>(p);
            }
            return null;
        }
        
        static bool _isFirstOpen;

        private void Popup_Opened(object sender, EventArgs e)
        {
            if (_button.IsChecked != true)
            {
                _button.IsChecked = true;
            }

            if (null != _MenuScript)
            {
                if (_MenuScript.Items.IndexOf(this) == 0)
                {
                    _isFirstOpen = true;
                }
                foreach (MenuScriptItem item in _MenuScript.Items)
                {
                    item.ClickMode = ClickMode.Hover;
                }

                _top.Visibility = Visibility.Visible;
            }
        }

        private void Popup_Closed(object sender, EventArgs e)
        {
            if (null != _MenuScript)
            {
                _mask.Visibility = Visibility.Collapsed;
            }
            if (_button.IsChecked != false)
            {
                _button.IsChecked = false;
            }
        }

        private void Mask_MouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            _button.IsChecked = false;
            (Parent as MenuScript).IsOpened = false;
        }
    }
}
