#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Alexandria.Plugins
{
	public class ConfigurationMap
	{
		#region Constructors
		public ConfigurationMap(Configuration file, IPluginSettings settings) : this(file, settings, true)
		{
		}
		
		public ConfigurationMap(Configuration file, IPluginSettings settings, bool autoLoad)
		{
			this.file = file;
			this.settings = settings;
			if (autoLoad)
				Load();
		}
		#endregion
	
		#region Private Fields
		private Configuration file;
		private IPluginSettings settings;
		#endregion
		
		#region Private Methods
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
					else if (propertyType == typeof(ushort))
						return Convert.ToUInt16(value);
					else if (propertyType == typeof(int))
						return Convert.ToInt32(value);
					else if (propertyType == typeof(uint))
						return Convert.ToUInt32(value);
					else if (propertyType == typeof(long))
						return Convert.ToInt64(value);
					else if (propertyType == typeof(ulong))
						return Convert.ToUInt64(value);
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
		#endregion
		
		#region Public Properties
		public Configuration File
		{
			get { return file; }			
		}
		
		public IPluginSettings Settings
		{
			get { return settings; }
			set { settings = value; }
		}
		#endregion
		
		#region Public Methods
		public void Load()
		{
			if (file != null && settings != null)
			{
				foreach (KeyValueConfigurationElement element in file.AppSettings.Settings)
				{
					SetPluginSetting(element.Key, element.Value);
				}
			}
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
		#endregion
	}
}
