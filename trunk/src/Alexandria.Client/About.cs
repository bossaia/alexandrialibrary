using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Alexandria.Control;

namespace Alexandria.Client
{
	public partial class About : Form
	{
		#region Constructors
		public About()
		{
			InitializeComponent();
		}
		#endregion
		
		#region Private Fields
		private PluginControl pluginControl;
		#endregion

		#region Public Methods
		[CLSCompliant(false)]
		public void Initialize(PluginControl pluginControl)
		{
			this.pluginControl = pluginControl;

			string license = Alexandria.Client.Properties.Resources.MIT_License;
			license = license.Replace("\\n", "\r\n");

			this.VersionTextBox.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.LicenseTextBox.Text = license;
			int i = 0;
			foreach(PluginInfo plugin in pluginControl.GetPluginInfo())
			{
				if (plugin.Bitmap != null)
				ImageList.Images.Add(plugin.Bitmap);

				ListViewItem item = new ListViewItem(new string[]{plugin.Title, plugin.Version.ToString()} , i);
				item.ToolTipText = plugin.Description;
				PluginListView.Items.Add(item);
				i++;
			}
		}
		#endregion

		#region Private Event Methods
		private void OKButton_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}