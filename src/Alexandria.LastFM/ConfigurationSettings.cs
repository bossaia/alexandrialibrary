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
		[PluginSetting(PluginSettingType.Boolean, "Indicates whether or not the Last.FM Submission plugin is enabled")]
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
		[PluginSetting(PluginSettingType.Text, "The user name to access your Last.FM account")]
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		[PluginSetting(PluginSettingType.PasswordText, "The password to access your Last.FM account")]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		#endregion
	}
}