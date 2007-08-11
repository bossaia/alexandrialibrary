using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.LastFM
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
		private string userName;
		private string password;
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

		#region Public Properties
		[PluginSetting("The user name to access your Last.FM account")]
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		[PluginSetting("The password to access your Last.FM account", PluginSettingType.PasswordText)]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		#endregion
	}
}