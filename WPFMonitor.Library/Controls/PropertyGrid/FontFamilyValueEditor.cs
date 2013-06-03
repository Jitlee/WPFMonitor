

namespace MonitorSystem.Controls
{
    using System.Windows.Controls;
    using System.Windows;
    using System.Windows.Media;
    using System;
using System.Collections.Generic;

    public class FontFamilyValueEditor : ValueEditorBase
    {
        readonly ComboBox _combox = new ComboBox();
        public FontFamilyValueEditor(PropertyGridLabel label, PropertyItem property)
            : base(label, property)
        {
            property.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(property_PropertyChanged);
            property.ValueError += new EventHandler<ExceptionEventArgs>(property_ValueError);

            this.Content = _combox;
            _combox.ItemsSource = Fonts;
            _combox.SelectedValuePath = "Value";
            _combox.DisplayMemberPath = "DisplayName";

            _combox.SelectionChanged += ComboBox_SelectionChanged;
            var fontFamily = property.Value as FontFamily;
            _combox.SelectedItem = new Font(fontFamily.Source, fontFamily);
        }

        public IEnumerable<Font> Fonts
        {
            get
            {
                yield return new Font("宋体", new FontFamily("STSong"));              
                yield return new Font("隶书", new FontFamily("LiSu"));
                yield return new Font("幼圆", new FontFamily("YouYuan"));
                yield return new Font("舒体", new FontFamily("FZShuTi"));
                yield return new Font("姚体", new FontFamily("FZYaoti"));               
                yield return new Font("仿宋", new FontFamily("STFangsong"));
                yield return new Font("彩云", new FontFamily("STCaiyun"));              
                yield return new Font("行楷", new FontFamily("STXingkai"));
                yield return new Font("新魏", new FontFamily("STXinwei"));
                yield return new Font("细黑", new FontFamily("STXihei"));
            }
        }

        public class Font
        {
            public string DisplayName { get; set; }
            public FontFamily Value { get; set; }
            public Font(string displayName, FontFamily value)
            {
                DisplayName = displayName;
                Value = value;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is Font)
                {
                    return Value.Source == (obj as Font).Value.Source;
                }
                return base.Equals(obj);
            }
        }
        //public override void InitializeCombo()
        //{
        //    this.LoadItems(new object[] { "宋体", "KaiTi", "黑体", "仿宋", "微软正黑体", "细明体", "微软雅黑" });
        //}

        void property_ValueError(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.EventException.Message);
        }

        void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                var fontFamily = this.Property.Value as FontFamily;
                _combox.SelectedItem = new Font(fontFamily.Source, fontFamily);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_combox != null)
            {
                this.Property.Value = _combox.SelectedValue;
            }
        }
    }
}
