using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Alexandria.Control;

namespace Alexandria.Client
{
	public partial class About : Form
	{
		#region Constructors
		//public About(string version, string license, IList<PluginInfo> plugins)
		public About()
		{
			InitializeComponent();
			
			//this.VersionTextBox.Text = version;
			this.LicenseTextBox.Text = Alexandria.Client.Properties.Resources.MIT_License;
			//int i = 0;
			//foreach(PluginInfo plugin in plugins)
			//{
				//if (plugin.Bitmap != null)
					//ImageList.Images.Add(plugin.Bitmap);
				
				//ListViewItem item = new ListViewItem(new string[]{plugin.Title, plugin.Version.ToString()} , i);
				//item.ToolTipText = plugin.Description;
				//PluginListView.Items.Add(item);
				//i++;
			//}
		}
		#endregion
		
		#region Private Fields
		private PluginControl pluginController;
		#endregion

		#region Public Methods
		[CLSCompliant(false)]
		public void Initialize(PluginControl pluginController)
		{
			this.pluginController = pluginController;
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