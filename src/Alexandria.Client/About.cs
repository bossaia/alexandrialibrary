using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Alexandria.Plugins;

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
			int i = 0;
			foreach(PluginInfo plugin in plugins)
			{
				Image image = Image.FromFile(plugin.ImagePath.LocalPath);
				ImageList.Images.Add(image);
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