using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace WPFMonitor.Core.ViewModel
{
    public abstract class EditBaseViewModel
    {
       private ICommand _OKCommand;

       public OpType OperationType { get; set; }
       public ICommand OKCommand
       {
           get
           {
               if (null == this._OKCommand)
               {
                   this._OKCommand = new DelegateCommand(this.OK);
               }
               return this._OKCommand;
           }
       }

       public void ShowMsgError(string msg)
       {
           MessageBox.Show(msg, "温馨提示", MessageBoxButton.OK, MessageBoxImage.Error);
       }
        /// <summary>
        /// 获取异常
        /// </summary>
        /// <param name="_Element"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
       protected bool GetErrors(DependencyObject _Element,out string errMsg)
       {
           StringBuilder sb=new StringBuilder();
           bool IsHaveError = false;
           foreach (object child in LogicalTreeHelper.GetChildren(_Element))
           {
               TextBox element = child as TextBox;
               if (null == element) continue;
               
               if (Validation.GetHasError(element))
               {
                   IsHaveError = true;
                   sb.AppendLine(element.Text + "输入错误；");
                   foreach (ValidationError _error in Validation.GetErrors(element))
                   {
                       sb.Append("  " + _error.ErrorContent.ToString());
                       sb.AppendLine("");
                   }
               }
           }
           errMsg = sb.ToString();
           return IsHaveError;
       }

       protected void UpdatetxtSource(DependencyObject _Element)
       {
           foreach (object child in LogicalTreeHelper.GetChildren(_Element))
           {
               TextBox element = child as TextBox;
               if (null == element) continue;
               element.Focus();
           }  

       }
       public abstract void OK();

       public event PropertyChangedEventHandler PropertyChanged;

       protected void NotifyPropertyChanged(String info)
       {
           if (PropertyChanged != null)
           {
               PropertyChanged(this, new PropertyChangedEventArgs(info));
           }
       }

    }
}
