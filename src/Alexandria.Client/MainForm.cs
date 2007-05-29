using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Alexandria;
using Alexandria.Client.Properties;
using Alexandria.LastFM;

namespace Alexandria.Client
{
	public partial class MainForm : Form
	{
		#region Constructors
		public MainForm()
		{
			try
			{
				InitializeComponent();				
				
				controller = new QueueController(this.QueueListView);
				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
				this.QueueListView.SelectedIndexChanged += new EventHandler(QueueListView_SelectedIndexChanged);
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion
		
		#region Private Fields
		private QueueController controller;
		#endregion
		
		#region Private Event Methods
		private void PlayPauseButton_Click(object sender, EventArgs e)
		{	
			controller.Play();
		}

		void StopButton_Click(object sender, EventArgs e)
		{
			controller.Stop();
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
		}
		
		private void DownloadButton_Click(object sender, EventArgs e)
		{
		}

		void QueueListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (QueueListView.SelectedItems.Count > 0)
			{
				controller.SelectTrack();
			}
		}
		#endregion
	}
}