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
    public partial class InputObj : UserControl
    {
        public InputObj()
        {
            InitializeComponent();
            this.MouseEnter +=new MouseEventHandler(boder_MouseEnter);
            this.MouseLeave += new MouseEventHandler(boder_MouseLeave);
            this.MouseLeftButtonDown+=new MouseButtonEventHandler(InputObj_MouseLeftButtonDown);
            tbShowInfo.Text = "0.0";
        }
        protected void boder_MouseEnter(object sender, MouseEventArgs e)
        {
            bdContent.BorderBrush = new SolidColorBrush(Colors.Red);
            bdContent.BorderThickness = new Thickness(1.5);
        }
        protected void boder_MouseLeave(object sender, MouseEventArgs e)
        {
            SetBorderHide();
        }

        public void SetBorderHide()
        {
            bdContent.BorderBrush = new SolidColorBrush();
        }
        private string _DataType = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string DataType
        {
            get { return _DataType; }
            set { _DataType = value; }
        }

        
        public object DataValue
        {
            get { return tbShowInfo.Text; }
            set {
                tbShowInfo.Text = value.ToString(); 
            }
        }

        private double _MaxValue =double.MinValue;
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }
       

        protected void InputObj_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            InputDilog obj = new InputDilog();
            if (_DataType == "int")
            {
                
                int val = 0;
                if (!int.TryParse(tbShowInfo.Text, out val))
                {
                    MessageBox.Show("请输入正确的数值！");
                    return;
                }
                obj.DataValue = val;
            }
            else if (_DataType == "Double")
            {
                double val = 0;
                if (!double.TryParse(tbShowInfo.Text, out val))
                {
                    MessageBox.Show("请输入正确的数值！");
                    return;
                }
                obj.DataValue = val;
            }
            else
            {
                obj.DataValue = tbShowInfo.Text;
            }

            obj.MaxValue = _MaxValue;
            obj.DataType = _DataType;
            obj.InputValueObj = this;

            obj.Show();
        }
    }
}
