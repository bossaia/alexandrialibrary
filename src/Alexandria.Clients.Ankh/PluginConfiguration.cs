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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Alexandria.Client.Controllers;

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
		private PluginController pluginController = new PluginController();
		#endregion
		
		#region Private Methods
		public void RefreshData()
		{
			Text =  pluginController.PluginInfo.Title;
			Version.Text = pluginController.PluginInfo.Version.ToString();
			if (pluginController.PluginInfo.Bitmap != null)
				this.Icon = Icon.FromHandle(pluginController.PluginInfo.Bitmap.GetHicon());
			PluginDescription.Text = pluginController.PluginInfo.Description;

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
			pluginController.Save();
			Close();
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}