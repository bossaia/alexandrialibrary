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
		
		private void SetPluginSetting(string key, string value)
		{
			PropertyInfo property = settings.GetType().GetProperty(key);
			if (property != null)
			{
				foreach(PluginSettingAttribute attribute in property.GetCustomAttributes(typeof(PluginSettingAttribute), false))
				{
					object propertyValue = GetSettingValueFromConfigValue(attribute, property.PropertyType, value);
					property.SetValue(settings, propertyValue, null);
				}
			}
		}
		
		private void SetConfigSetting(string key, string value)
		{
			if (file.AppSettings.Settings[key] != null)
			{
				file.AppSettings.Settings[key].Value = value;
			}
			else
			{
				file.AppSettings.Settings.Add(key, value);
			}
		}
		
		private object GetSettingValueFromConfigValue(PluginSettingAttribute attribute, Type propertyType, string value)
		{
			//TODO: add some kind of validation and error handling for bad config values
			switch(attribute.Type)
			{
				case PluginSettingType.Text:
				case PluginSettingType.MaskedText:
				case PluginSettingType.PasswordText:
				case PluginSettingType.DirectoryPath:
				case PluginSettingType.FileName:
				case PluginSettingType.FilePath:
					return value;
				case PluginSettingType.Boolean:
					return Convert.ToBoolean(value);
				case PluginSettingType.Integer:
					if (propertyType == typeof(short))
						return Convert.ToInt16(value);
					else if (propertyType == typeof(int))
						return Convert.ToInt32(value);
					else if (propertyType == typeof(long))
						return Convert.ToInt64(value);
					else if (propertyType == typeof(decimal))
						return Convert.ToDecimal(value);
					else throw new AlexandriaException("Could not convert configuration integer value");
				case PluginSettingType.Real:
					if (propertyType == typeof(float))
						return Convert.ToSingle(value);
					else if (propertyType == typeof(double))
						return Convert.ToDouble(value);
					else if (propertyType == typeof(decimal))
						return Convert.ToDecimal(value);
					else throw new AlexandriaException("Could not convert configuration real value");
				case PluginSettingType.Enumeration:
					return Enum.Parse(propertyType, value);
				default:
					return null;
			}
		}
		
		public Configuration File
		{
			get { return file; }			
		}
		
		public IPluginSettings Settings
		{
			get { return settings; }
		}
		
		public void Save()
		{
			foreach(PropertyInfo property in settings.GetType().GetProperties())
			{
				foreach(PluginSettingAttribute attribute in property.GetCustomAttributes(typeof(PluginSettingAttribute), false))
				{
					SetConfigSetting(property.Name, property.GetValue(settings, null).ToString());
					break;
				}
			}
			file.Save();
		}
	}
}
