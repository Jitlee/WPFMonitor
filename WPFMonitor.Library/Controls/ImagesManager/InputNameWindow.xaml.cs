using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFMonitor.Library.Controls.ImagesManager
{
    public partial class InputNameWindow : Window
    {
        private string _exstion = string.Empty;
        public InputNameWindow()
        {
            InitializeComponent();
            this.ValueTextBox.Focus();
        }

        public string SubTitle
        {
            set { SubTitleRun.Text = value; }
        }

        public string Value
        {
            get { return ValueTextBox.Text + _exstion; }
            set {
                var index = value.LastIndexOf('.');
                if (index < 0)
                {
                    _exstion = string.Empty;
                    ValueTextBox.Text = value;
                    ValueTextBox.Select(0, value.Length);
                }
                else
                {
                    _exstion = value.Substring(index);
                    ValueTextBox.Text = value.Remove(index);
                    ValueTextBox.Select(0, index);
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (null != ValueTextBox)
            {
                this.OKButton.IsEnabled = !string.IsNullOrWhiteSpace(ValueTextBox.Text) && !Regex.IsMatch(ValueTextBox.Text.Trim(), "[\\\\/:*?\"<>|]");
            }
        }

        private void ValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && OKButton.IsEnabled)
            {
                this.DialogResult = true;
            }
        }
    }
}

