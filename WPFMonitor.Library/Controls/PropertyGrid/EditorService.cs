using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MonitorSystem.Controls
{
	public static class EditorService
	{
		public static ValueEditorBase GetEditor(PropertyItem propertyItem, PropertyGridLabel label)
		{
			if (propertyItem == null) throw new ArgumentNullException("propertyItem");

			EditorAttribute attribute = propertyItem.GetAttribute<EditorAttribute>();
			if (attribute != null)
			{
				Type editorType = Type.GetType(attribute.EditorTypeName, false);
				if (editorType != null)
					return Activator.CreateInstance(editorType) as ValueEditorBase;
			}

			Type propertyType = propertyItem.PropertyType;

			ValueEditorBase editor = GetEditor(propertyType, label, propertyItem);

			while (editor == null && propertyType.BaseType != null)
			{
				propertyType = propertyType.BaseType;
				editor = GetEditor(propertyType, label, propertyItem);
			}

			return editor;
		}
		public static ValueEditorBase GetEditor(Type propertyType, PropertyGridLabel label, PropertyItem property)
		{
            if (property.GetAttribute<ImageAttribute>() != null)
            {
                return new ImageVaueEditor(label, property);
            }

			if (typeof(Boolean).IsAssignableFrom(propertyType))
				return new BooleanValueEditor(label, property);

			if (typeof(Enum).IsAssignableFrom(propertyType))
				return new EnumValueEditor(label, property);

			if (typeof(DateTime).IsAssignableFrom(propertyType))
				return new DateTimeValueEditor(label, property);

			if (typeof(String).IsAssignableFrom(propertyType))
				return new StringValueEditor(label, property);

            if (typeof(Brush).IsAssignableFrom(propertyType))
                return new ColorValueEditor(label, property);

            if (typeof(Color).IsAssignableFrom(propertyType))
                return new ColorValueEditor(label, property);

            if (typeof(FontFamily).IsAssignableFrom(propertyType))
                return new FontFamilyValueEditor(label, property);

            if (typeof(ValueType).IsAssignableFrom(propertyType))
                return new StringValueEditor(label, property);
            
			//if (typeof(Object).IsAssignableFrom(propertyType))
			//    return new PropertyGrid(label, property);

			return new StringValueEditor(label, property);
		}

	}
}
