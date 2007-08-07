using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Alexandria.Plugins;

namespace Alexandria.Control
{
	public class PluginControl
	{
		#region Constructors
		public PluginControl()
		{
		}
		#endregion

		#region Private Fields
		private IPluginRepository repository;
		private PluginInfo pluginInfo;
		private ConfigurationMap configurationMap;
		#endregion

		#region Private Methods
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
			
			foreach (KeyValuePair<Assembly, bool> pair in repository.Assemblies)
			{
				Assembly assembly = pair.Key;
				bool enabled = pair.Value;
				
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
				catch
				{
					MessageBox.Show("There was an error loading the icon for the library file: " + assembly.Location, "ERROR");
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
				if (repository.ConfigurationMaps.ContainsKey(assembly))
					configMap = repository.ConfigurationMaps[assembly];
					
				PluginInfo info = new PluginInfo(assembly, configMap, enabled, title, description, version, bitmap);

				plugins.Add(info);
			}
			
			return plugins;
		}
		#endregion
	}
}
