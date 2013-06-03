using System;

namespace MonitorSystem.Controls
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class DisplayNameAttribute : Attribute
	{
		public string DisplayName { get; private set; }

		public DisplayNameAttribute(string displayName)
		{
			if (string.IsNullOrEmpty(displayName)) throw new ArgumentNullException("displayName");
			DisplayName = displayName;
		}
	}
}
