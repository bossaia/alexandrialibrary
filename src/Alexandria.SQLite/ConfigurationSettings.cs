using System;
using System.Collections.Generic;
using System.IO;
using Alexandria.Plugins;

namespace Alexandria.SQLite
{
	public class ConfigurationSettings : IPluginSettings
	{
		public ConfigurationSettings()
		{
		}
		
		private bool enabled;
		private string databaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Alexandria" + Path.DirectorySeparatorChar);
		private string databaseName = "Alexandria.db";
		private DatabaseUpdate databaseUpdate = DatabaseUpdate.None;
		private DatabaseSynch databaseSynch = DatabaseSynch.None;
		
		[PluginSetting(PluginSettingType.Boolean, "Indicates whether or not the SQLite plugin is enabled")]
		public bool Enabled
		{
			get { return enabled; }
			set { enabled = value; }
		}

		[PluginSetting(PluginSettingType.DirectoryPath, "The directory where the SQLite database file is located")]
		public string DatabaseDirectory
		{
			get { return databaseDirectory; }
			set { databaseDirectory = value; }
		}

		[PluginSetting(PluginSettingType.FileName, "The name of the SQLite database file")]
		public string DatabaseName
		{
			get { return databaseName; }
			set { databaseName = value; }
		}

		[PluginSetting(PluginSettingType.Enumeration, "Indicates how the database should be updated when Alexandria is updated")]
		public DatabaseUpdate DatabaseUpdate
		{
			get { return databaseUpdate; }
			set { databaseUpdate = value; }
		}

		[PluginSetting(PluginSettingType.Enumeration, "Indicates how the database should be synchronized with other Alexandria clients")]
		public DatabaseSynch DatabaseSynch
		{
			get { return databaseSynch; }
			set { databaseSynch = value; }
		}
		
		public string DatabasePath
		{
			get { return Path.Combine(databaseDirectory, databaseName); }
		}
	}
}
