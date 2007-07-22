using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Plugins
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class PluginSettingAttribute : Attribute
	{
		public PluginSettingAttribute(PluginSettingType type, string description)
		{
			this.type = type;
			this.description = description;
		}

		public PluginSettingAttribute(PluginSettingType type, string description, string textMask) : this(type, description)
		{
			this.textMask = textMask;
		}
		
		public PluginSettingAttribute(PluginSettingType type, string description, bool isReadOnly) : this(type, description)
		{
			this.isReadOnly = isReadOnly;
		}

		private PluginSettingType type = PluginSettingType.None;		
		private string description;
		private string textMask;
		private bool isReadOnly;

		public PluginSettingType Type
		{
			get { return type; }
		}

		public string Description
		{
			get { return description; }
		}
		
		public string TextMask
		{
			get { return textMask; }
			set { textMask = value; }
		}
		
		public bool IsReadOnly
		{
			get { return isReadOnly; }
			set { isReadOnly = value; }
		}
	}
}
