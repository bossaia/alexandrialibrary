using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Client
{
	public partial class About : Form
	{
		#region Constructors
		public About(string version, string license, IList<PluginInfo> plugins)
		{
			InitializeComponent();
			
			this.VersionTextBox.Text = version;
			this.LicenseTextBox.Text = license;
			foreach(PluginInfo plugin in plugins)
			{
				ListViewItem item = new ListViewItem(new string[]{plugin.Name, plugin.Type, plugin.Version, plugin.Status});
				PluginListView.Items.Add(item);
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