#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Alexandria;
using Alexandria.Client.Properties;
using Alexandria.LastFM;
using Alexandria.Media.IO;
using Alexandria.Metadata;

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

				InitializeConfig();
				
				controller = new QueueController(this.QueueListView);
				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
				this.QueueListView.SelectedIndexChanged += new EventHandler(QueueListView_SelectedIndexChanged);
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion
		
		#region Private Fields
		private QueueController controller;
		private string dbDir;
		private string dbFile;
		private string dbPath;
		#endregion
		
		#region Private Methods
		
		#region InitializeConfig
		private void InitializeConfig()
		{
			dbDir = ConfigurationManager.AppSettings["DatabaseDirectory"].ToString();
			dbFile = ConfigurationManager.AppSettings["DatabaseFile"].ToString();
			dbPath = dbDir + dbFile;
			
			if (!Directory.Exists(dbDir))
				Directory.CreateDirectory(dbDir);
		}
		#endregion
		
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
		
		#region Protected Overrides
		
		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{			
			base.OnLoad(e);

			ListViewItem item = new ListViewItem(new string[] { "3", "Smoke & Mirrors", "Deadringer", "RJD2", "4:26", "2002/1/1", @"D:\working\Tests\AudioTest\03 Smoke & Mirrors.OGG", "ogg" });
			QueueListView.Items.Add(item);
		}
		#endregion
		
		#endregion
		
		#region Internal Properties
		internal string DatabaseDirectory
		{
			get { return dbDir; }
		}
		
		internal string DatabaseFile
		{
			get { return dbFile; }
		}
		
		internal string DatabasePath
		{
			get { return dbPath; }
		}
		#endregion
	}
}