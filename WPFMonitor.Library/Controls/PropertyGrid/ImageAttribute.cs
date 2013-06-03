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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ImageAttribute : Attribute
	{
        /// <summary>
        /// 图片存放相对路径 不包含网站的更目录， 如 : Upload\\ImageMap
        /// </summary>
        public string Path = string.Empty;

        /// <summary>
        /// 是否只包含图片
        /// </summary>
        public bool OnlyImage = false;

		// Methods
        public ImageAttribute(string path = "")
		{
            Path = path.Trim('\\');
		}

		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
            var attribute = obj as ImageAttribute;
            return ((attribute != null) && (attribute.Path == this.Path));
		}

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}
