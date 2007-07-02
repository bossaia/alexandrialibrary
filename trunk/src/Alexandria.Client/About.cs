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
				Bitmap bitmap = (Bitmap)Bitmap.FromFile(plugin.ImagePath.LocalPath);
				
				//Create an icon from the bitmap and save it
				Icon icon = Icon.FromHandle(bitmap.GetHicon());
				string iconPath = plugin.ImagePath.LocalPath.Replace(".dll.bmp", ".ico");
				System.IO.FileStream outputStream = new System.IO.FileStream(iconPath, System.IO.FileMode.Create);
				icon.Save(outputStream);
				
				ImageList.Images.Add(bitmap);
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