using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Alexandria.Control;

namespace Alexandria.Client
{
	public partial class PluginConfiguration : Form
	{
		#region Constructors
		public PluginConfiguration()
		{
			InitializeComponent();
		}
		#endregion

		#region Private Fields
		private PluginControl pluginControl = new PluginControl();
		#endregion
		
		#region Private Methods
		public void RefreshData()
		{
			Text =  pluginControl.PluginInfo.Title;
			Version.Text = pluginControl.PluginInfo.Version.ToString();
			if (pluginControl.PluginInfo.Bitmap != null)
				this.Icon = Icon.FromHandle(pluginControl.PluginInfo.Bitmap.GetHicon());
			PluginDescription.Text = pluginControl.PluginInfo.Description;

			/*
			if (configurationMap != null && configurationMap.Settings != null)
			{
				SuspendLayout();

				foreach (PropertyInfo property in configurationMap.Settings.GetType().GetProperties())
				{
					foreach (PluginSettingAttribute attribute in property.GetCustomAttributes(typeof(PluginSettingAttribute), false))
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
							foreach (string name in Enum.GetNames(property.PropertyType))
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
			*/
		}
		#endregion
		
		#region Private Event Methods
		private void OKButton_Click(object sender, EventArgs e)
		{
			//if (configurationMap != null)
			//{
				//foreach(Control control in SettingsLayoutPanel.Controls)
					//ReadConfigValueFromControl(control);
			
				//configurationMap.Save();
			//}
			pluginControl.Save();
			Close();
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}