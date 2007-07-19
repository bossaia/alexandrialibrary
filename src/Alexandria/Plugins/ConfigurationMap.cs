using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Alexandria.Plugins
{
	public class ConfigurationMap
	{
		public ConfigurationMap(Configuration file, IPluginSettings settings)
		{
			this.file = file;
			this.settings = settings;
			
			if (file != null && settings != null)
			{
				foreach(KeyValueConfigurationElement element in file.AppSettings.Settings)
				{
					SetPluginSetting(element.Key, element.Value);
				}
			}
		}
	
		private Configuration file;
		private IPluginSettings settings;
		
		private object SetPluginSetting(string key, string value)
		{
			foreach(PropertyInfo property in 
		}
		
		public Configuration File
		{
			get { return file; }			
		}
		
		public IPluginSettings Settings
		{
			get { return settings; }
		}
	}
}
