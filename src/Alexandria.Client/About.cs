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
		public About(string version, string license, IList<string> plugins)
		{
			InitializeComponent();
			
			this.VersionTextBox.Text = version;
			this.LicenseTextBox.Text = license;
			foreach(string plugin in plugins)
			{
				PluginListbox.Items.Add(plugin);
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