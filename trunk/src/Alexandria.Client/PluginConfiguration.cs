using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Alexandria.Client
{
	public partial class PluginConfiguration : Form
	{
		public PluginConfiguration()
		{
			InitializeComponent();
		}

		private PluginInfo pluginInfo = default(PluginInfo);

		private void OKButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			Close();
		}
		
		public PluginInfo PluginInfo
		{
			get { return pluginInfo; }
			set
			{
				pluginInfo = value;
				RefreshData();
			}
		}
		
		public void RefreshData()
		{
			Text = pluginInfo.Title;
			Version.Text = pluginInfo.Version.ToString();
			if (pluginInfo.Bitmap != null)
				this.Icon = Icon.FromHandle(pluginInfo.Bitmap.GetHicon());			
			PluginDescription.Text = pluginInfo.Description;
		}
	}
}