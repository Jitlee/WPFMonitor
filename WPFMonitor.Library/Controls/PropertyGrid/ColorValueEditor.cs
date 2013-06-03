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
    public class ColorValueEditor : ValueEditorBase
    {
        object currentValue;
        ColorPicker _colorPicker = new ColorPicker();

        public ColorValueEditor(PropertyGridLabel label, PropertyItem property)
            : base(label, property)
        {
        
            property.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(property_PropertyChanged);
            property.ValueError += new EventHandler<ExceptionEventArgs>(property_ValueError);

            this.Content = _colorPicker;
            _colorPicker.ColorChanged += colorPicker_ColorChanged;

            if (property.Value is SolidColorBrush)
            {
                currentValue = _colorPicker.Color = (property.Value as SolidColorBrush).Color;
            }
            if (property.Value is Color)
            {
                currentValue =_colorPicker.Color = (Color)property.Value;
            }
        }

        void property_ValueError(object sender, ExceptionEventArgs e)
        {
            MessageBox.Show(e.EventException.Message);
        }
        void property_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {

                if (this.Property.Value is SolidColorBrush)
                {
                    currentValue = _colorPicker.Color = (this.Property.Value as SolidColorBrush).Color;
                }
                if (this.Property.Value is Color)
                {
                    currentValue = _colorPicker.Color = (Color)this.Property.Value;
                }
            }
        }

        private void colorPicker_ColorChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_colorPicker != null)
            {
                if (this.Property.Value is SolidColorBrush)
                {
                    this.Property.Value = new SolidColorBrush(_colorPicker.Color);
                }
                if (this.Property.Value is Color)
                {
                    this.Property.Value = _colorPicker.Color;
                }
                currentValue = _colorPicker.Color;
            }
        }
    }
}
