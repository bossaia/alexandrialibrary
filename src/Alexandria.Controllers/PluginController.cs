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
using System.Drawing;
using System.IO;
using System.Reflection;

using Alexandria.Plugins;

namespace Alexandria.Controllers
{
	public class PluginController
	{
		#region Constructors
		public PluginController()
		{
		}
		#endregion

		#region Private Fields
		private IPluginRepository repository;
		private PluginInfo pluginInfo;
		private ConfigurationMap configurationMap;
		#endregion

		#region Private Methods
		/*
		private void ReadConfigValueFromControl(System.Windows.Forms.Control control)
		{
			if (control.GetType() == typeof(CheckBox))
			{
				CheckBox checkBox = (CheckBox)control;
				PropertyInfo property = (PropertyInfo)checkBox.Tag;
				if (checkBox.Enabled)
					property.SetValue(configurationMap.Settings, checkBox.Checked, null);
			}
			else if (control.GetType() == typeof(TextBox))
			{
				TextBox textBox = (TextBox)control;
				PropertyInfo property = (PropertyInfo)textBox.Tag;
				if (textBox.Enabled)
					property.SetValue(configurationMap.Settings, textBox.Text, null);
			}
			else if (control.GetType() == typeof(MaskedTextBox))
			{
				MaskedTextBox textBox = (MaskedTextBox)control;
				PropertyInfo property = (PropertyInfo)textBox.Tag;
				object value = null;
				if (property.PropertyType == typeof(short))
					value = Convert.ToInt16(textBox.Text);
				else if (property.PropertyType == typeof(ushort))
					value = Convert.ToUInt16(textBox.Text);
				else if (property.PropertyType == typeof(int))
					value = Convert.ToInt32(textBox.Text);
				else if (property.PropertyType == typeof(uint))
					value = Convert.ToUInt32(textBox.Text);
				else if (property.PropertyType == typeof(long))
					value = Convert.ToInt64(textBox.Text);
				else if (property.PropertyType == typeof(ulong))
					value = Convert.ToUInt64(textBox.Text);
				else if (property.PropertyType == typeof(float))
					value = Convert.ToSingle(textBox.Text);
				else if (property.PropertyType == typeof(double))
					value = Convert.ToDouble(textBox.Text);
				else if (property.PropertyType == typeof(decimal))
					value = Convert.ToDecimal(textBox.Text);
				
				if (textBox.Enabled)
					property.SetValue(configurationMap.Settings, value, null);
			}
			else if (control.GetType() == typeof(ComboBox))
			{
				ComboBox comboBox = (ComboBox)control;
				PropertyInfo property = (PropertyInfo)comboBox.Tag;
				string value = (comboBox.SelectedItem != null) ? comboBox.SelectedItem.ToString() : "None";
				if (comboBox.Enabled)
					property.SetValue(configurationMap.Settings, Enum.Parse(property.PropertyType, value), null);
			}
		}
		*/
		#endregion

		#region Public Properties
		public PluginInfo PluginInfo
		{
			get { return pluginInfo; }
			set
			{
				pluginInfo = value;
				if (value != null)
				{
					configurationMap = pluginInfo.ConfigurationMap;
					//RefreshData();
				}
			}
		}
		#endregion

		#region Public Methods
		public void Initialize(IList<string> pluginPaths)
		{
			IList<FileInfo> files = new List<FileInfo>();
			foreach (string path in pluginPaths)
			{
				if (File.Exists(path))
					files.Add(new FileInfo(path));
			}
			
			repository = new PluginRepository(files);
		}
		
		public void Load()
		{
		}
		
		public void Save()
		{
		}

		public IList<PluginInfo> GetPluginInfo()
		{
			IList<PluginInfo> plugins = new List<PluginInfo>();
			
			foreach (IPlugin plugin in repository.Plugins.Values)
			{
				Assembly assembly = plugin.Assembly;
				bool enabled = plugin.Enabled;
				
				string title = "Unknown Plugin";
				string description = "This plugin could not be identified";
				Version version = new Version(1, 0, 0, 0);
				FileInfo assemblyFile = new FileInfo(assembly.Location);
				string imageFileName = assemblyFile.Name.Replace(".dll", string.Empty) + "." + assemblyFile.Name.Replace(".dll", ".bmp");
				Bitmap bitmap = null;

				try
				{
					bitmap = new Bitmap(assembly.GetManifestResourceStream(imageFileName));
				}
				catch (Exception ex)
				{
					throw new AlexandriaException("There was an error loading the icon for plugin " + assembly.Location, ex);
				}

				foreach (Attribute attribute in assembly.GetCustomAttributes(false))
				{
					if (attribute is AssemblyTitleAttribute)
					{
						AssemblyTitleAttribute titleAttribute = attribute as AssemblyTitleAttribute;
						title = titleAttribute.Title;
					}
					else if (attribute is AssemblyDescriptionAttribute)
					{
						AssemblyDescriptionAttribute descriptionAttribute = attribute as AssemblyDescriptionAttribute;
						description = descriptionAttribute.Description;
					}
					else if (attribute is AssemblyVersionAttribute)
					{
						AssemblyVersionAttribute versionAttribute = attribute as AssemblyVersionAttribute;
						version = new Version(versionAttribute.Version);
					}
				}

				ConfigurationMap configMap = null;
				//if (repository.ConfigurationMaps.ContainsKey(assembly))
					//configMap = repository.ConfigurationMaps[assembly];
					
				PluginInfo info = new PluginInfo(assembly, configMap, enabled, title, description, version, bitmap);

				plugins.Add(info);
			}
			
			return plugins;
		}
		#endregion
	}
}
