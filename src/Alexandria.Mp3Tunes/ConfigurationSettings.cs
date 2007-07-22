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
		private string userName;
		private string password;
		private LockerSynch lockerSynch = LockerSynch.None;
		#endregion
	
		#region IPluginSettings Members
		[PluginSetting(PluginSettingType.Boolean, "Insdicates whether or not the MP3tunes Music Locker plugin is enabled")]
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
		#endregion
		
		#region Public Properties
		[PluginSetting(PluginSettingType.Text, "The user name for acessing your MP3tunes Music Locker")]
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		[PluginSetting(PluginSettingType.PasswordText, "The password for accessing your MP3tunes Music Locker")]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		
		[PluginSetting(PluginSettingType.Enumeration, "Indicates how Alexandria should synchronize your local metadata with your MP3tunes Music Locker")]
		public LockerSynch LockerSynch
		{
			get { return lockerSynch; }
			set { lockerSynch = value; }
		}
		#endregion
	}
}
