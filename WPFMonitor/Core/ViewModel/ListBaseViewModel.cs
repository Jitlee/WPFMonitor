using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace WPFMonitor.Core.ViewModel
{

    public abstract class ListBaseViewModel
    {
        #region 变量

        public void ShowMsgError(string msg)
        {
            MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private ICommand _QueryCommand;

        private ICommand _InsertCommand;

        private ICommand _UpdateCommand;

        private ICommand _DeleteCommand;

        private ICommand _GroupCommand;

        #endregion

        #region 属性

        public ICommand QueryCommand
        {
            get
            {
                if (null == this._QueryCommand)
                {
                   // this._QueryCommand = new DelegateCommand(this.Query);
                }
                return this._QueryCommand;
            }
        }

        public ICommand InsertCommand
        {
            get
            {
                if (null == this._InsertCommand)
                {
                    this._InsertCommand = new DelegateCommand(this.Insert);
                }
                return this._InsertCommand;
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (null == this._UpdateCommand)
                {
                    _UpdateCommand = new DelegateCommand<object>(this.Update);
                    //this._UpdateCommand = new DelegateCommand(this.Update);
                }
                return this._UpdateCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (null == this._DeleteCommand)
                {
                    this._DeleteCommand = new DelegateCommand<object>(this.Delete);
                }
                return this._DeleteCommand;
            }
        }

        public ICommand GroupCommand
        {
            get
            {
                if (null == _GroupCommand)
                {
                   // _GroupCommand = new DelegateCommand(this.Group);

                    
                }
                return _GroupCommand;
            }
        }

        #endregion

        #region 方法

        //public abstract void Query(object parameter);

        public abstract void Insert();

        public abstract void Update(object parameter);

        public abstract void Delete(object parameter);

        //public abstract void Group(object parameter);

        #endregion
    }
}
