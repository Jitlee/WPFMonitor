using System;
using System.Windows.Input;

namespace MonitorSystem.MonitorSystemGlobal
{
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

    public class DelegateCommand<T> : DelegateCommandBase
    {
        public DelegateCommand(Action<T> execute) : 
            base(new Action<object>(o => {
                if (null != execute)
                {
                    execute((T)o);
                }
            })) { }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecuteMethod) :
            base(new Action<object>(o =>
            {
                if (null != execute)
                {
                    execute((T)o);
                }
            }), new Predicate<object>(o => {
                if (null != canExecuteMethod)
                {
                    return canExecuteMethod((T)o);
                }
                return true;
            })) { }

        public DelegateCommand(Action<T> execute, Func<bool> canExecuteMethod) :
            base(new Action<object>(o =>
            {
                if (null != execute)
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
