
using System;
using System.Windows.Input;

namespace WPFMonitor.Core
{
    /// <summary>
    /// Command 实体基类
    /// </summary>
    public class DelegateCommandBase : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommandBase(Action<object> execute)
        {
            _execute = execute;
        }

        public DelegateCommandBase(Action<object> execute,
                       Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// Command 实体类
    /// </summary>
    /// <typeparam name="T">Command 参数类型</typeparam>
    public class DelegateCommand<T> : DelegateCommandBase
    {
        public DelegateCommand(Action<T> execute) :
            base(new Action<object>(o =>
            {
                if (null != execute && o is T)
                {
                    execute((T)o);
                }
            })) { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecuteMethod) :
            base(new Action<object>(o =>
            {
                if (null != execute && o is T)
                {
                    execute((T)o);
                }
            }), new Predicate<object>(o =>
            {
                if (null != canExecuteMethod)
                {
                    return canExecuteMethod((T)o);
                }
                return true;
            })) { }

        public DelegateCommand(Action<T> execute, Func<bool> canExecuteMethod) :
            base(new Action<object>(o =>
            {
                if (null != execute && o is T)
                {
                    execute((T)o);
                }
            }), new Predicate<object>(o =>
            {
                if (null != canExecuteMethod)
                {
                    return canExecuteMethod();
                }
                return true;
            })) { }
    }

    /// <summary>
    /// Command 实体类
    /// </summary>
    public class DelegateCommand : DelegateCommandBase
    {
        public DelegateCommand(Action execute) :
            base(new Action<object>(o =>
            {
                if (null != execute)
                {
                    execute();
                }
            })) { }

        public DelegateCommand(Action execute, Func<bool> canExecuteMethod) :
            base(new Action<object>(o =>
            {
                if (null != execute)
                {
                    execute();
                }
            }), new Predicate<object>(o =>
            {
                if (null != canExecuteMethod)
                {
                    return canExecuteMethod();
                }
                return true;
            })) { }
    }
}