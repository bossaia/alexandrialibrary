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
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Alexandria;
using Alexandria.Catalog;
using Alexandria.Media.IO;
using Alexandria.Metadata;
using Alexandria.Persistence;
using Alexandria.Plugins;

using Alexandria.Client.Properties;
using Alexandria.Fmod;
using Alexandria.FreeDB;
using Alexandria.Imdb;
using Alexandria.LastFM;
using Alexandria.MediaInfo;
using Alexandria.Mp3Tunes;
using Alexandria.MusicBrainz;
using Alexandria.MusicDns;
using Alexandria.Playlists;
using Alexandria.SQLite;
using Alexandria.TagLib;

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

				this.Resize += new EventHandler(MainForm_Resize);
				this.OpenToolStripMenuItem.Click += new EventHandler(OpenToolStripMenuItem_Click);				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
				this.NextButton.Click += new EventHandler(NextButton_Click);
				this.PreviousButton.Click += new EventHandler(PreviousButton_Click);
				this.MuteButton.Click += new EventHandler(MuteButton_Click);
				this.QueueListView.SelectedIndexChanged += new EventHandler(QueueListView_SelectedIndexChanged);
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion
		
		#region Private Fields
		private IPluginRepository repository;
		private QueueController controller;
		private string dbDir;
		private string dbFile;
		private string dbPath;
		
		private NotifyIcon notifyIcon = new NotifyIcon();
		private ContextMenu notifyMenu = new ContextMenu();
		private MenuItem notifyOpenItem;
		private MenuItem notifyPlayItem;
		private MenuItem notifyStopItem;
		private MenuItem notifyMuteItem;
		private MenuItem notifyPrevItem;
		private MenuItem notifyNextItem;
		private MenuItem notifyShowItem;				
		private MenuItem notifyExitItem;
		private FormWindowState oldWindowState = FormWindowState.Normal;
		#endregion
		
		#region Private Constant Fields
		
		#region license
		private const string license = @"Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the ""Software""), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.";
		#endregion
		
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
		
		#region ShowHideForm
		private void ShowHideForm()
		{
			if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
			{
				oldWindowState = WindowState;
				WindowState = FormWindowState.Minimized;
			}
			else
			{
				Show();
				WindowState = oldWindowState;
			}
		}
		#endregion
		
		#region TestDB
		private void TestDB()
		{
			
		}
		#endregion
		
		#endregion

		#region Private Event Methods
		void MainForm_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Minimized)
				Hide();
		}
		
		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("call controller.Open() here", "Open");
		}

		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("call controller.Play() here", "Play");
			//controller.Play();
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("call controller.Stop() here", "Stop");
			//controller.Stop();
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("call controller.Mute() here", "Mute");
		}

		private void PreviousButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("call controller.SelectPrevious() here", "Prev");
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("call controller.SelectNext() here", "Next");
		}

		private void QueueListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (QueueListView.SelectedItems.Count > 0)
			{
				controller.SelectTrack();
			}
		}

		private void notifyIcon_Click(object sender, EventArgs e)
		{
			MouseEventArgs m = e as MouseEventArgs;
			if (m != null)
			{
				if (m.Button == MouseButtons.Left)
					ShowHideForm();
				else if (m.Button == MouseButtons.Right)
				{
				}				
				else if (m.Button == MouseButtons.Middle)
				{
				}
			}
		}

		private void notifyOpenItem_Click(object sender, EventArgs e)
		{
			OpenToolStripMenuItem_Click(sender, e);
		}

		private void notifyPlayItem_Click(object sender, EventArgs e)
		{
			PlayPauseButton_Click(sender, e);
		}

		private void notifyStopItem_Click(object sender, EventArgs e)
		{
			StopButton_Click(sender, e);
		}

		private void notifyNextItem_Click(object sender, EventArgs e)
		{
			NextButton_Click(sender, e);
		}

		private void notifyPrevItem_Click(object sender, EventArgs e)
		{
			PreviousButton_Click(sender, e);
		}

		private void notifyMuteItem_Click(object sender, EventArgs e)
		{
			notifyMuteItem.Checked = !notifyMuteItem.Checked;
			MuteButton_Click(sender, e);
		}

		private void notifyShowItem_Click(object sender, EventArgs e)
		{
			ShowHideForm();
		}

		private void notifyExitItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			
			IList<PluginInfo> plugins = new List<PluginInfo>();
			foreach(Assembly assembly in repository.Assemblies)
			{
				string title = "Unknown Plugin";
				string description = "This plugin could not be identified";
				Version version = new Version(1, 0, 0, 0);
				string imageFileName = string.Format("{0}.bmp", assembly.Location);
				if (!System.IO.File.Exists(imageFileName))
					imageFileName = Path.Combine(Environment.CurrentDirectory, "Alexandria.dll.bmp");
				Uri imagePath = new Uri(imageFileName);
				
				foreach(Attribute attribute in assembly.GetCustomAttributes(false))
				{
					if (attribute is AssemblyTitleAttribute)
					{
						AssemblyTitleAttribute titleAttribute = attribute as AssemblyTitleAttribute;
						title = titleAttribute.Title;
					}
					else if (attribute is AssemblyDescriptionAttribute)
					{
						AssemblyDescriptionAttribute descriptionAttribute = attribute as AssemblyDescriptionAttribute;
						description = descriptionAttribute.Description;
					}
					else if (attribute is AssemblyVersionAttribute)
					{
						AssemblyVersionAttribute versionAttribute = attribute as AssemblyVersionAttribute;
						version = new Version(versionAttribute.Version);
					}
				}
				
				plugins.Add(new PluginInfo(title, description, version, imagePath));
			}
						
			About about = new About(appVersion, license, plugins);
			about.ShowDialog(this);
		}
		#endregion
		
		#region Protected Overrides
		
		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{			
			base.OnLoad(e);

			repository = new PluginRepository("Alexandria*.dll");
			
			SQLiteDataProvider mechanism = new SQLiteDataProvider(dbPath);
			PersistenceBroker broker = new PersistenceBroker(repository, mechanism);
			broker.Test();
			
			//TestDB();
			//ListViewItem item = new ListViewItem(new string[] { "3", "Smoke & Mirrors", "Deadringer", "RJD2", "4:26", "2002/1/1", @"D:\working\Tests\AudioTest\03 Smoke & Mirrors.OGG", "ogg" });
			//QueueListView.Items.Add(item);
			
			Icon icon = new Icon(@"..\..\App.ico");
			notifyIcon.Icon = icon;
			notifyIcon.Text = "Alexandria";
			notifyIcon.ContextMenu = notifyMenu;
			notifyIcon.Visible = true;
			notifyIcon.Click += new EventHandler(notifyIcon_Click);

			notifyOpenItem = new MenuItem("Open Media", new EventHandler(notifyOpenItem_Click));
			notifyPlayItem = new MenuItem("Play/Pause", new EventHandler(notifyPlayItem_Click), Shortcut.CtrlP);
			notifyStopItem = new MenuItem("Stop", new EventHandler(notifyStopItem_Click), Shortcut.CtrlS);		
			notifyPrevItem = new MenuItem("Prev", new EventHandler(notifyPrevItem_Click), Shortcut.CtrlL);
			notifyNextItem = new MenuItem("Next", new EventHandler(notifyNextItem_Click), Shortcut.CtrlN);
			notifyMuteItem = new MenuItem("Mute", new EventHandler(notifyMuteItem_Click), Shortcut.CtrlM);
			notifyShowItem = new MenuItem("Show/Hide", new EventHandler(notifyShowItem_Click));
			notifyExitItem = new MenuItem("Exit", new EventHandler(notifyExitItem_Click), Shortcut.AltF4);
			
			notifyMenu.MenuItems.Add(notifyOpenItem);
			notifyMenu.MenuItems.Add("-");
			notifyMenu.MenuItems.Add(notifyPlayItem);			
			notifyMenu.MenuItems.Add(notifyStopItem);
			notifyMenu.MenuItems.Add(notifyMuteItem);
			notifyMenu.MenuItems.Add("-");
			notifyMenu.MenuItems.Add(notifyPrevItem);
			notifyMenu.MenuItems.Add(notifyNextItem);
			notifyMenu.MenuItems.Add("-");
			notifyMenu.MenuItems.Add(notifyShowItem);
			notifyMenu.MenuItems.Add(notifyExitItem);
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