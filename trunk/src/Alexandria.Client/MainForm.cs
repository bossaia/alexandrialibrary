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
using Alexandria.Playlist;
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
		private IPersistenceBroker broker;
		private IPersistenceMechanism mechanism;
		private QueueController controller;
		//private string dbDir;
		//private string dbFile;
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
		
		private string dbDir = System.IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Alexandria" + System.IO.Path.DirectorySeparatorChar);
		//@"C:\Documents and Settings\All Users\Application Data\Alexandria\";
		private const string dbFile = "Alexandria.db";
		
		#endregion
		
		#region Private Methods
		
		#region InitializePlugins
		private void InitializePlugins()
		{
			IList<FileInfo> files = new List<FileInfo>();
			foreach(string fileName in ConfigurationManager.AppSettings.AllKeys)
			{
				if (!string.IsNullOrEmpty(fileName))
				{
					FileInfo file = new FileInfo(fileName);
					files.Add(file);
				}
			}			
			repository = new PluginRepository(files);
			InitializePersistence();
		}
		#endregion
		
		#region InitializePersistence
		private void InitializePersistence()
		{
			dbPath = dbDir + dbFile;

			if (!Directory.Exists(dbDir))
				Directory.CreateDirectory(dbDir);
			
			mechanism = new SQLitePersistenceMechanism(dbPath);
			broker = new PersistenceBroker(repository, mechanism);
			broker.InitializeRecordMaps();
		}
		#endregion
		
		#region InitializeInterface
		private void InitializeInterface()
		{
			InitializeNotifyIcon();
			InitializePluginMenu();
		}
		#endregion
		
		#region InitializeNotifyIcon
		private void InitializeNotifyIcon()
		{
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
		
		#region LoadDefaultUser
		private void LoadDefaultUser()
		{
			Guid userId = new Guid("FC26A3CC-91DC-4d8b-BC54-F28DAE5BD9D6");
			IUser user = broker.LookupRecord<IUser>(userId);
			if (user != null)
			{
				//TODO: allow the default catalog to be user-defined
				if (user.Catalogs != null && user.Catalogs.Count > 0)
				{
					controller = new QueueController(this.QueueListView, broker);
					controller.LoadTracks(user.Catalogs[0].Tracks);
				}
			}
		}
		#endregion
		
		#region TestDB
		private void TestDB()
		{
			//Guid trackId = new Guid("5DD91B74-AC6F-4161-93BC-E0C9F2C4B557");
			//DateTime releaseDate = new DateTime(1993, 1, 1);
			//Mp3Tunes.MusicLocker locker = new MusicLocker();
			//IAudioTrack track = locker.GetTrack(trackId, "http://mp3tunes.com/locker/1412414124/Sober.ogg", "Sober", "Undertow", "Tool", 506000, releaseDate.ToFileTime(), 3, "ogg");
			  //Mp3Tunes.TrackAdditionalInfo additional = new TrackAdditionalInfo(Guid.NewGuid, @"M:\audio\ogg\Tool\Undertow\03 Sober.ogg");
			//track.PersistenceBroker = broker;
			//track.MetadataIdentifiers.Add(MusicBrainz.MusicBrainzIdFactory.CreateMusicBrainzId(track, Guid.NewGuid()));
			//track.MetadataIdentifiers.Add(Mp3Tunes.TrackIdFactory.CreateTrackId(track, "mp3tunes_id:2117098401"));
			//track.Save();
			//IAudioTrack track = broker.LookupRecord<IAudioTrack>(trackId);
			//string x = track.Name;

			//controller = new QueueController(this.QueueListView);
			//controller.LoadTracks();
			//if (controller.Tracks.Count > 0)
			//{
				//int x = controller.Tracks.Count;

				//Guid userId = new Guid("FC26A3CC-91DC-4d8b-BC54-F28DAE5BD9D6");
				//IUser user = broker.LookupRecord<IUser>(userId);				
				//foreach(IAudioTrack track in controller.Tracks)
				//{
					//user.Catalogs[0].Tracks.Add(track);
					//user.Save();
				//}
				//user.Save();
			//}
			
			//string x = user.Name;
			
			//Guid catalogId = new Guid("F1FE3C1E-2C3F-4b8d-AF08-6282A4313B27");
			//IUser user = new BaseUser(userId, "Dan", "secret");
			//user.Catalogs.Add(new BaseCatalog(catalogId, "Dan's Music"));
			//user.Catalogs[0].Tracks.Add(track);
			//user.PersistenceBroker = broker;
			//user.Save();
		}
		#endregion

		#region GetPluginInfo
		private IList<PluginInfo> GetPluginInfo()
		{
			IList<PluginInfo> plugins = new List<PluginInfo>();
			foreach (KeyValuePair<Assembly, bool> pair in repository.Assemblies)
			{
				Assembly assembly = pair.Key;
				bool enabled = pair.Value;
				
				string title = "Unknown Plugin";
				string description = "This plugin could not be identified";
				Version version = new Version(1, 0, 0, 0);
				FileInfo assemblyFile = new FileInfo(assembly.Location);
				string imageFileName = assemblyFile.Name.Replace(".dll", string.Empty) + "." + assemblyFile.Name.Replace(".dll", ".bmp");
				Bitmap bitmap = null;

				try
				{
					bitmap = new Bitmap(assembly.GetManifestResourceStream(imageFileName));
				}
				catch
				{
					MessageBox.Show("There was an error loading the icon for the library file: " + assembly.Location, "ERROR");
				}

				foreach (Attribute attribute in assembly.GetCustomAttributes(false))
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

				ConfigurationMap configMap = null;
				if (repository.ConfigurationMaps.ContainsKey(assembly))
					configMap = repository.ConfigurationMaps[assembly];
					
				PluginInfo info = new PluginInfo(assembly, configMap, enabled, title, description, version, bitmap);

				plugins.Add(info);
			}
			return plugins;
		}
		#endregion
		
		#region InitializePluginMenu
		private void InitializePluginMenu()
		{
			pluginsToolStripMenuItem.DropDown.Items.Clear();

			IList<PluginInfo> plugins = GetPluginInfo();
			foreach (PluginInfo plugin in plugins)
			{
				ToolStripMenuItem item = new ToolStripMenuItem(plugin.Title, (Image)plugin.Bitmap, new EventHandler(pluginConfigItem_Click));
				//ToolStripButton item = new ToolStripButton(plugin.Title, (Image)plugin.Bitmap, new EventHandler(pluginConfigItem_Click));
				item.ToolTipText = plugin.Description;
				item.Tag = plugin;
				pluginsToolStripMenuItem.DropDown.Items.Add(item);				
			}
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
			//MessageBox.Show("call controller.Play() here", "Play");
			if (controller != null)
				controller.Play();
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			//MessageBox.Show("call controller.Stop() here", "Stop");
			if (controller != null)
				controller.Stop();
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			//MessageBox.Show("call controller.Mute() here", "Mute");
			if (controller != null)
			{
				controller.Mute();
				VolumeTrackBar.Enabled = !controller.IsMuted;
			}
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
			
			IList<PluginInfo> plugins = GetPluginInfo();
						
			About about = new About(appVersion, license, plugins);
			about.ShowDialog(this);
		}
		
		private void pluginConfigItem_Click(object sender, EventArgs e)
		{
			PluginConfiguration config = new PluginConfiguration();
			ToolStripItem item = (ToolStripItem)sender;
			config.PluginInfo = (PluginInfo)item.Tag;
			config.ShowDialog();
		}
		#endregion
		
		#region Protected Overrides
		
		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{			
			base.OnLoad(e);
			
			InitializePlugins();
			InitializeInterface();
			
			LoadDefaultUser();
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