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
		public About(string version, string license, IList<IPlugin> plugins)
		{
			InitializeComponent();
			
			this.VersionTextBox.Text = version;
			this.LicenseTextBox.Text = license;
			foreach(IPlugin plugin in plugins)
			{
				ListViewItem item = new ListViewItem(new string[]{plugin.Name, plugin.Version.ToString()});
				//item.i
				//item.ToolTipText = plugin.Description;
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