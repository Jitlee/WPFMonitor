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

namespace MonitorSystem.Controls
{
    public class CustomCursor
    {
        private FrameworkElement element;
        private DataTemplate template;
        private Cursor originalCursor;
        System.Windows.Controls.Primitives.Popup cursorContainer;
        private static readonly DependencyProperty CustomCursorProperty =
            DependencyProperty.RegisterAttached("CustomCursor", typeof(CustomCursor), typeof(CustomCursor), null);

        public static readonly DependencyProperty CustomProperty = DependencyProperty.RegisterAttached("Custom", typeof(bool), typeof(CustomCursor), new PropertyMetadata(new PropertyChangedCallback(OnCustomPropertyChanged)));
        private static void OnCustomPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element != null)
            {
                var customCursor = element.GetValue(CustomCursorProperty) as CustomCursor;
                if(null != customCursor)
                {
                    customCursor.OnCustomChanged((bool)e.OldValue, (bool)e.NewValue);
                }
            }
        }

        public static bool GetCustom(DependencyObject obj)
        {
            return (bool)obj.GetValue(CustomProperty);
        }

        public static void SetCustom(DependencyObject obj, bool value)
        {
            obj.SetValue(CustomProperty, value);
        }

        private void OnCustomChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                if (null == cursorContainer)
                {
                    element.SetValue(CustomCursorProperty, this);
                    originalCursor = element.Cursor;
                    element.Cursor = Cursors.None;
                    element.MouseEnter += element_MouseEnter;
                    element.MouseLeave += element_MouseLeave;
                    element.MouseMove += element_MouseMove;
                    cursorContainer = new System.Windows.Controls.Primitives.Popup()
                    {
                        IsOpen = false,
                        Child = new ContentControl()
                        {
                            ContentTemplate = template,
                            IsHitTestVisible = false,
                            RenderTransform = new TranslateTransform()
                        }
                    };
                    cursorContainer.IsHitTestVisible = false;
                }
                else
                {
                    //cursorContainer.IsOpen = true;
                }
            }
            else
            {
                if (null != cursorContainer)
                {
                    cursorContainer.IsOpen = false;
                }
            }
        }

        private CustomCursor(FrameworkElement element, DataTemplate template)
        {
            this.element = element;
            this.template = template;
            element.SetValue(CustomCursorProperty, this);
            //originalCursor = element.Cursor;
            //element.Cursor = Cursors.None;
            //element.MouseEnter += element_MouseEnter;
            //element.MouseLeave += element_MouseLeave;
            //element.MouseMove += element_MouseMove;
            //element.LayoutUpdated += (o, e) => { if (element.Visibility != Visibility.Visible && cursorContainer.IsOpen) { cursorContainer.IsOpen = false; } };
            //cursorContainer = new System.Windows.Controls.Primitives.Popup()
            //{
            //    IsOpen = false,
            //    Child = new ContentControl()
            //    {
            //        ContentTemplate = template,
            //        IsHitTestVisible = false,
            //        RenderTransform = new TranslateTransform()
            //    }
            //};
            //cursorContainer.IsHitTestVisible = false;
        }

        private void element_MouseMove(object sender, MouseEventArgs e)
        {
            //cursorContainer.IsOpen = true;
            var p = e.GetPosition(null);
            var t = (cursorContainer.Child.RenderTransform as TranslateTransform);
            t.X = p.X;
            t.Y = p.Y;
        }

        private void element_MouseLeave(object sender, MouseEventArgs e)
        {
            //element.ReleaseMouseCapture();
            if (null != cursorContainer)
            {
                cursorContainer.IsOpen = false;
            }
        }

        private void element_MouseEnter(object sender, MouseEventArgs e)
        {
            //element.CaptureMouse();
            if (null != cursorContainer)
            {
                cursorContainer.IsOpen = true;
            }
        }

        private void Dispose()
        {
            element.MouseEnter -= element_MouseEnter;
            element.MouseLeave -= element_MouseLeave;
            element.MouseMove -= element_MouseMove;
            element.ClearValue(CustomCursorProperty);
            element.Cursor = originalCursor;
            cursorContainer.IsOpen = false;
            (cursorContainer.Child as ContentControl).ContentTemplate = null;
            cursorContainer.Child = null;
            cursorContainer = null;
        }

        public static DataTemplate GetCursorTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(CursorTemplateProperty);
        }

        public static void SetCursorTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(CursorTemplateProperty, value);
        }

        public static readonly DependencyProperty CursorTemplateProperty =
            DependencyProperty.RegisterAttached("CursorTemplate", typeof(DataTemplate), typeof(CustomCursor), new PropertyMetadata(OnCursorTemplatePropertyChanged));

        private static void OnCursorTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement))
                throw new ArgumentOutOfRangeException("Property can only be attached to FrameworkElements");
            var element = (d as FrameworkElement);
            if (e.OldValue is DataTemplate)
            {
                (element.GetValue(CustomCursorProperty) as CustomCursor).Dispose();
            }
            if (e.NewValue is DataTemplate)
                new CustomCursor(element, e.NewValue as DataTemplate);
        }
    }

}
