using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Plugins
{
	public class PluginSettings : IPluginSettings
	{
		#region Constructors
		public PluginSettings()
		{
		}
		#endregion
			
		#region IPluginSettings Members
		[PluginSetting(PluginSettingType.Boolean, "Indicates whether or not the Alexandria Core Library is enabled", true)]
		public bool Enabled
		{
			get { return true; }
			set { }
		}
		#endregion
	}
}
