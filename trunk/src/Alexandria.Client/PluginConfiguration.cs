using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Alexandria.Plugins;

namespace Alexandria.Client
{
	public partial class PluginConfiguration : Form
	{
		public PluginConfiguration()
		{
			InitializeComponent();
		}

		private PluginInfo pluginInfo = default(PluginInfo);
		private ConfigurationMap configurationMap = null;

		private void OKButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		public PluginInfo PluginInfo
		{
			get { return pluginInfo; }
			set
			{
				pluginInfo = value;
				if (value != null)
				{
					configurationMap = pluginInfo.ConfigurationMap;
					RefreshData();
				}
			}
		}
		
		public void RefreshData()
		{
			Text = pluginInfo.Title;
			Version.Text = pluginInfo.Version.ToString();
			if (pluginInfo.Bitmap != null)
				this.Icon = Icon.FromHandle(pluginInfo.Bitmap.GetHicon());			
			PluginDescription.Text = pluginInfo.Description;
			
			if (configurationMap != null && configurationMap.Settings != null)
			{
				SuspendLayout();
				
				foreach(PropertyInfo property in configurationMap.Settings.GetType().GetProperties())
				{
					foreach(PluginSettingAttribute attribute in property.GetCustomAttributes(typeof(PluginSettingAttribute), false))
					{
						Label label = new Label();
						label.Text = property.Name;
						label.TextAlign = ContentAlignment.MiddleLeft;
						label.AutoSize = true;
						
						object value = property.GetValue(configurationMap.Settings, null);
						
						if (attribute.Type == PluginSettingType.Boolean)
						{
							CheckBox checkBox = new CheckBox();
							checkBox.Checked = Convert.ToBoolean(value);
							checkBox.Tag = property;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(checkBox);
						}
						else if (attribute.Type == PluginSettingType.Text || attribute.Type == PluginSettingType.FileName || attribute.Type == PluginSettingType.PasswordText)
						{
							TextBox textBox = new TextBox();						
							textBox.Tag = property;
							if (attribute.Type == PluginSettingType.PasswordText)
								textBox.PasswordChar = '*';
							
							textBox.Multiline = false;								
							textBox.Size = new Size(textBox.Size.Width * 2, textBox.Size.Height);
							textBox.Text = (value != null) ? value.ToString() : string.Empty;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(textBox);
						}
						else if (attribute.Type == PluginSettingType.DirectoryPath)
						{
						}
						else if (attribute.Type == PluginSettingType.Enumeration)
						{
							ComboBox comboBox = new ComboBox();
							foreach(string name in Enum.GetNames(property.PropertyType))
							{		
								comboBox.Items.Add(name);
							}
							comboBox.SelectedItem = value.ToString();						
							//comboBox.DroppedDown = false;
							//comboBox.AllowDrop = false;
							comboBox.Tag = property;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(comboBox);
						}
						
						break;
					}
				}
				
				ResumeLayout();
			}
		}
	}
}