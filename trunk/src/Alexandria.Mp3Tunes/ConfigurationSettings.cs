using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.Mp3Tunes
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
		private LockerSynch lockerSynch = LockerSynch.None;
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
		[PluginSetting("The user name for acessing your MP3tunes Music Locker")]
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		[PluginSetting("The password for accessing your MP3tunes Music Locker")]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		
		[PluginSetting("Indicates how Alexandria should synchronize your local metadata with your MP3tunes Music Locker")]
		public LockerSynch LockerSynch
		{
			get { return lockerSynch; }
			set { lockerSynch = value; }
		}
		#endregion
	}
}
