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
		private PluginController pluginController = new PluginController();
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