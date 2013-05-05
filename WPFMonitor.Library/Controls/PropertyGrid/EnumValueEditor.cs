
namespace WPFMonitor.Library.Controls
{
	#region Using Directives
	using System.Windows.Controls;
	using WPFMonitor.Library.Controls.Converters;

	#endregion

	#region EnumValueEditor
	/// <summary>
	/// An editor for a Boolean Type
	/// </summary>
	public class EnumValueEditor : ComboBoxEditorBase
	{
		public EnumValueEditor(PropertyGridLabel label, PropertyItem property)
			: base(label, property)
		{
		}
		public override void InitializeCombo()
		{
			this.LoadItems(EnumHelper.GetValues(Property.PropertyType));
		}
	}
	#endregion
}
