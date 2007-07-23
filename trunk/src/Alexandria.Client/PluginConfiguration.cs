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

		private void OKButton_Click(object sender, EventArgs e)
		{
			if (configurationMap != null)
			{
				foreach(Control control in SettingsLayoutPanel.Controls)
					ReadConfigValueFromControl(control);
			
				configurationMap.Save();
			}
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
						label.Padding = new Padding(0, 7, 0, 0);
						label.AutoSize = true;
						DescriptionToolTip.SetToolTip(label, attribute.Description);
												
						object value = property.GetValue(configurationMap.Settings, null);
						
						if (attribute.Type == PluginSettingType.Boolean)
						{
							CheckBox checkBox = new CheckBox();
							checkBox.Enabled = !attribute.IsReadOnly;
							checkBox.Checked = Convert.ToBoolean(value);
							checkBox.Tag = property;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(checkBox);
						}
						else if (attribute.Type == PluginSettingType.Text || attribute.Type == PluginSettingType.FileName || attribute.Type == PluginSettingType.PasswordText)
						{
							TextBox textBox = new TextBox();
							textBox.Enabled = !attribute.IsReadOnly;
							textBox.Tag = property;
							if (attribute.Type == PluginSettingType.PasswordText)
								textBox.PasswordChar = '*';
							
							textBox.Multiline = false;
							string valueString = (value != null) ? value.ToString() : string.Empty;
							textBox.Text = valueString;
							textBox.Width = 200;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(textBox);
						}
						else if (attribute.Type == PluginSettingType.Integer)
						{
							MaskedTextBox textBox = new MaskedTextBox();
							textBox.Enabled = !attribute.IsReadOnly;
							textBox.Tag = property;
							//textBox.Mask = "0000000";

							textBox.Multiline = false;
							
							string valueString = (value != null) ? value.ToString() : string.Empty;
							textBox.Text = valueString;
							textBox.Width = 100;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(textBox);
						}
						else if (attribute.Type == PluginSettingType.Real)
						{
							MaskedTextBox textBox = new MaskedTextBox();
							textBox.Enabled = !attribute.IsReadOnly;
							textBox.Tag = property;
							//textBox.Mask = "0000.00";

							textBox.Multiline = false;
							string valueString = (value != null) ? value.ToString() : string.Empty;
							textBox.Text = valueString;
							textBox.Width = 100;
							SettingsLayoutPanel.Controls.Add(label);
							SettingsLayoutPanel.Controls.Add(textBox);
						}
						else if (attribute.Type == PluginSettingType.Enumeration)
						{
							ComboBox comboBox = new ComboBox();
							comboBox.Enabled = !attribute.IsReadOnly;
							foreach(string name in Enum.GetNames(property.PropertyType))
								comboBox.Items.Add(name);
							
							comboBox.Width = 150;
							comboBox.SelectedItem = value.ToString();
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