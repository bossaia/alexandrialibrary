using System;
using System.Collections.Generic;
using Alexandria.Plugins;

namespace Alexandria.Playlist
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
		private PlaylistFormat defaultPlaylistFormat;
		#endregion
	
		#region IPluginSettings Members
		[PluginSetting(PluginSettingType.Boolean, "Indicates whether or not the Alexandria Playlist plugin is enabled")]
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}
		#endregion
		
		#region Public Properties
		[PluginSetting(PluginSettingType.Enumeration, "The default format to save new playlists in")]
		public PlaylistFormat DefaultPlaylistFormat
		{
			get { return defaultPlaylistFormat; }
			set { defaultPlaylistFormat = value; }
		}
		#endregion
	}
}
