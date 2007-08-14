using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.Playlist
{
	public class Plugin : IPluginSettings
	{
		#region Constructors
		public Plugin()
		{
		}
		#endregion
	
		#region Private Fields
		private bool enabled;
		private ConfigurationMap configurationMap;
		private PlaylistFormat defaultPlaylistFormat;
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
		[PluginSetting("The default format to save new playlists in")]
		public PlaylistFormat DefaultPlaylistFormat
		{
			get { return defaultPlaylistFormat; }
			set { defaultPlaylistFormat = value; }
		}
		#endregion
	}
}
