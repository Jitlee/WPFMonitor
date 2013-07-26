using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MonitorSystem.Other
{
    public partial class InputDilog : Window
    {
        public InputDilog()
        {
            InitializeComponent();
            this.Closed += (sender, e) =>
            {
                _InputObj.SetBorderHide();
            };
        }

        object _DataValue = 0.0;
        /// <summary>
        /// 值
        /// </summary>
        public object DataValue
        {
            get { return _DataValue; }
            set {
                string str =string.Format("{0}", value.ToString());
                txtValue.Text = str; 
            }
        }

        /// <summary>
        /// 输入框，弹出对象
        /// </summary>
        InputObj _InputObj = new InputObj();

        public InputObj InputValueObj
        {
            get { return _InputObj; }
            set { _InputObj = value; }
        }
       

        private string _DataType = string.Empty;
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }

        private double _MaxValue = double.MinValue;
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }

        private void ChildWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtValue.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _InputObj.SetBorderHide();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //_ISOK = true;
            if (_DataType == "int")
            {
                int val = 0;
                if (!int.TryParse(txtValue.Text, out val))
                {
                    ShowMsg("请输入正确的数据类型！");
                    return;
                }
                if (_MaxValue != double.MinValue)
                {
                    if (val > _MaxValue)
                    {
                        ShowMsg(string.Format("超过最大值:{0}", _MaxValue));
                        return;
                    }
                }
                _DataValue = val;
            }
            else if (_DataType == "Double")
            {
                double val = 0;
                if (!double.TryParse(txtValue.Text, out val))
                {
                    ShowMsg("请输入正确的数据类型！");
                    return;
                }
                if (_MaxValue != double.MinValue)
                {
                    if (val > _MaxValue)
                    {
                        ShowMsg(string.Format("超过最大值:{0}", _MaxValue));
                        return;
                    }
                }
                _DataValue = val;
            }
            else
            {                
                _DataValue = txtValue.Text;
            }
            _InputObj.SetBorderHide();
            _InputObj.tbShowInfo.Text = _DataValue.ToString();
            this.Close();
        }

        private void ShowMsg(string msg)
        {
            MessageBox.Show(msg);
        }
    }

    
     
}

