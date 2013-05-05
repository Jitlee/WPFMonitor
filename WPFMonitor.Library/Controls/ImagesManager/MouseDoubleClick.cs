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
using System.Threading;
using System.Diagnostics;
//using System.Windows.Interactivity;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public class MouseDoubleClick : DependencyObject
    {
        public static DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseDoubleClick), new PropertyMetadata(null, OnCommandPropertyChanged));

        public static void SetCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        static void OnCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement)
            {
                var element = d as FrameworkElement;
                element.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(MouseLeftButtonDown), true);
            }
        }

        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(ICommand), typeof(MouseDoubleClick), null);

        public static void SetCommandParameter(DependencyObject d, object value)
        {
            d.SetValue(CommandParameterProperty, value);
        }

        public static object GetCommandParameter(DependencyObject d)
        {
            return d.GetValue(CommandParameterProperty);
        }

        static Timer _timer;
        static void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (null == _timer)
            {
                _timer = new Timer(MouseLeftButtonDownCallback, null, 200, 200);
            }
            else
            {
                StopTimer();
                var element = sender as FrameworkElement;
                if (null != element)
                {
                    var command = (ICommand)element.GetValue(CommandProperty);
                    if (null != command)
                    {
                        var commandParameter = element.GetValue(CommandParameterProperty);
                        if (null != commandParameter)
                        {
                            if (command.CanExecute(commandParameter))
                            {
                                command.Execute(commandParameter);
                            }
                        }
                        else
                        {
                            if (e.OriginalSource is FrameworkElement)
                            {
                                var source = e.OriginalSource as FrameworkElement;
                                var dataContext = source.GetValue(FrameworkElement.DataContextProperty);
                                if (command.CanExecute(dataContext))
                                {
                                    command.Execute(dataContext);
                                }
                            }
                        }
                    }
                }
            }
        }

        static void MouseLeftButtonDownCallback(object state)
        {
            StopTimer();
        }

        private static void StopTimer()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer.Dispose();
            _timer = null;
        }
    }
}
