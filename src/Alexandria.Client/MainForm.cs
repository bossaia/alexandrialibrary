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
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Alexandria;
using Alexandria.Client.Properties;
using Alexandria.LastFM;
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
		#endregion
		
		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{			
			base.OnLoad(e);

			string dbDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Alexandria\\";
			if (!System.IO.Directory.Exists(dbDir))
				System.IO.Directory.CreateDirectory(dbDir);
			string dbPath = dbDir + "Alexandria.db";
			
			try
			{
				// MusicBrainzID childID = A7A30461-E12D-40d9-B258-43387D0F32B2
				// MusicBrainzID = 0dfaa81e-9326-4eff-9604-c20d1c613227
				// SampleID childID = FF02A9E8-B597-421f-8E6F-642F0CBD585C
				//ParentID = 4E03C5A9-D50B-4561-B43F-D19D419C78B7
				SQLite.SQLiteDataProvider provider = new Alexandria.SQLite.SQLiteDataProvider(dbPath);
				BaseAudioTrack track = provider.Lookup<BaseAudioTrack>(new Guid("3cf31aae-9dc1-4311-8423-fb533e8f948b"));
				Guid newId = new Guid("54038E25-EA9C-4dcb-A9E1-7D4456ECDCE9");
				track.MetadataIdentifiers.Add(new MetadataIdentifier(newId, track.Id, "ab2490-cc-22-1", "type 5", new Version(0, 3, 9, 0)));
				track.Save();
				//provider.Save(track);
				//provider.Initialize(typeof(BaseAudioTrack));
				MessageBox.Show("Test succeeded", "SQLite database initialized");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "SQLite Error");
			}
			//BaseAudioTrack track = provider.Lookup<BaseAudioTrack>(Guid.NewGuid());
			//MessageBox.Show(provider.Test(), "SQLite Test");
			//controller.LoadTracks();
		}
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