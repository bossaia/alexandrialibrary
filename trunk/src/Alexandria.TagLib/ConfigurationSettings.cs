using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.TagLib
{
	public class ConfigurationSettings : IPluginSettings
	{
		#region Constructors
		public ConfigurationSettings()
		{
		}
		#endregion

		#region Private Fields
		private bool enabled;
		private ConfigurationMap configurationMap;
		#endregion

		#region IPluginSettings Members
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		public ConfigurationMap ConfigurationMap
		{
			get { return configurationMap; }
			set { configurationMap = value; }
		}

		public void Load()
		{
			if (ConfigurationMap != null)
				ConfigurationMap.Load();
		}

		public void Save()
		{
			if (ConfigurationMap != null)
				ConfigurationMap.Save();
		}
		#endregion
	}
}