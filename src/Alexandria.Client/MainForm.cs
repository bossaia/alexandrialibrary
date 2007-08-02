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
		private SimpleAlbumFactory albumFactory = new SimpleAlbumFactory();
		
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
		private bool isSeeking;
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
			InitializeToolbox();
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
			controller = new QueueController(this.QueueListView, broker, repository);
		
			Guid userId = new Guid("FC26A3CC-91DC-4d8b-BC54-F28DAE5BD9D6");
			IUser user = broker.LookupRecord<IUser>(userId);
			if (user != null)
			{
				//TODO: allow the default catalog to be user-defined
				if (user.Catalogs != null && user.Catalogs.Count > 0)
				{
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
		
		#region InitializeToolbox
		private void InitializeToolbox()
		{
			foreach(DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType == System.IO.DriveType.Fixed)
				{
					ListViewItem item = new ListViewItem(drive.Name, 0);
					item.ToolTipText = string.Format("{0}/{1}", drive.TotalFreeSpace / 1024, drive.TotalSize / 1024);
					ToolBoxListView.Items.Add(item);
				}
				else if (drive.DriveType == System.IO.DriveType.CDRom)
				{
					int imageIndex = 2;
					string toolTipText = "Not Ready";
					if (drive.IsReady)
					{
						imageIndex = 1;
						toolTipText = drive.VolumeLabel;
					}
					
					ListViewItem item = new ListViewItem(drive.Name, imageIndex);
					item.ToolTipText = toolTipText;
					item.Tag = new CompactDiscTrackSource(albumFactory, new Uri(drive.Name));
					ToolBoxListView.Items.Add(item);
				}
			}
		}
		#endregion
		
		#region GetVolume
		private float GetVolume()
		{
			return Convert.ToSingle(VolumeTrackBar.Value * .1);
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
			DialogResult result = FileOpenDialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				if (controller != null)
				{
					controller.OpenFile(FileOpenDialog.FileName);
				}
			}
		}

		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
			if (controller != null && controller.AudioStream != null)
			{
				controller.Play();
				if (controller.AudioStream.PlaybackState == PlaybackState.Playing)
					PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_pause_blue;
				else PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
			}
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				controller.Stop();
				PlaybackTrackBar.Value = 0;
				PlaybackTrackBar.Enabled = false;
				PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
			}
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				controller.Mute();
				if (controller.IsMuted)
				{
					VolumeTrackBar.Enabled = false;
					MuteButton.BackgroundImage = Alexandria.Client.Properties.Resources.sound;
				}
				else
				{
					VolumeTrackBar.Enabled = true;
					MuteButton.BackgroundImage = Alexandria.Client.Properties.Resources.sound_mute;
				}
			}
		}

		private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
		{
			if (controller != null)
				controller.Volume = GetVolume();
		}

		private void PreviousButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				bool isPlaying = (controller.AudioStream != null && controller.AudioStream.PlaybackState == PlaybackState.Playing);
				controller.Previous();
				if (isPlaying)
					PlayPauseButton_Click(sender, EventArgs.Empty);
			}
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				bool isPlaying = (controller.AudioStream != null && controller.AudioStream.PlaybackState == PlaybackState.Playing);
				controller.Next();
				if (isPlaying)
					PlayPauseButton_Click(sender, EventArgs.Empty);
			}
		}

		private void QueueListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (controller != null && QueueListView.SelectedItems.Count > 0)
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

		private void PlaybackTimer_Tick(object sender, EventArgs e)
		{
			if (!isSeeking)
			{
				if (controller != null && controller.AudioStream != null)
				{
					int value = Convert.ToInt32(controller.AudioStream.Elapsed.TotalSeconds);
					if (value <= PlaybackTrackBar.Maximum)
						PlaybackTrackBar.Value = value;
					else PlaybackTrackBar.Value = PlaybackTrackBar.Maximum;
					
					controller.UpdateStatus();
				}
				else
				{
					PlaybackTrackBar.Enabled = false;
					VolumeTrackBar.Enabled = false;
				}
			}
		}
		
		private void OnSelectedTrackStart(object sender, EventArgs e)
		{
			if (controller != null && controller.AudioStream != null)
			{
				if (controller.SelectedTrack != null)
					NowPlayingLabel.Text = string.Format("{0} - {1}", controller.SelectedTrack.Artist, controller.SelectedTrack.Name);
				
				controller.Volume = GetVolume();
				VolumeTrackBar.Enabled = true;

				PlaybackTrackBar.Enabled = true;
				PlaybackTrackBar.Minimum = 0;
				PlaybackTrackBar.Maximum = Convert.ToInt32(controller.AudioStream.Duration.TotalSeconds);
				PlaybackTrackBar.Value = 0;
			}
		}
		
		private void OnSelectedTrackEnd(object sender, EventArgs e)
		{
			if (controller != null)
			{
				controller.Next();
				controller.Play();
			}
		}

		private void PlaybackTrackBar_MouseDown(object sender, MouseEventArgs e)
		{
			isSeeking = true;
		}

		private void PlaybackTrackBar_MouseUp(object sender, MouseEventArgs e)
		{
			isSeeking = false;
			if (controller.AudioStream != null)
			{
				if (controller.AudioStream.CanSetElapsed)
				{
					controller.AudioStream.Elapsed = new TimeSpan(0, 0, PlaybackTrackBar.Value);
				}
			}
		}

		private void ToolBoxListView_MouseDown(object sender, MouseEventArgs e)
		{
			//if (ToolBoxListView.SelectedItems != null && ToolBoxListView.SelectedItems.Count > 0)
			//{
				//if (ToolBoxListView.SelectedItems[0].Tag != null && ToolBoxListView.SelectedItems[0].Tag is ITrackSource)
				//{
					//ToolBoxListView.DoDragDrop(ToolBoxListView.SelectedItems[0].Tag, DragDropEffects.Copy);
				//}
			//}
			//else
			//{
				//select the item based on e.X, e.Y then use the above logic
				ListViewItem item = ToolBoxListView.GetItemAt(e.X, e.Y);
				if (item != null && item.Tag != null && item.Tag is ITrackSource)
				{
					item.Selected = true;
					ToolBoxListView.DoDragDrop(item.Tag, DragDropEffects.Copy);
				}
			//}
		}

		private void ToolBoxListView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			object x = e.Item;
		}

		private void ToolBoxListView_DragLeave(object sender, EventArgs e)
		{
		}

		private void QueueListView_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.None;
			
			if (e.Data != null)
			{
				object source = e.Data.GetData(typeof(CompactDiscTrackSource));
				if (source != null)
					e.Effect = DragDropEffects.Copy;
			}
		}

		private void QueueListView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data != null)
			{
				object data = e.Data.GetData(typeof(CompactDiscTrackSource));
				if (data != null)
				{
					CompactDiscTrackSource trackSource = (CompactDiscTrackSource)data;
					IList<IAudioTrack> tracks = trackSource.GetAudioTracks();
					controller.LoadTracks(tracks);
				}
			}
		}
		#endregion
		
		#region Protected Overrides
		
		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{			
			base.OnLoad(e);
			
			PlaybackTimer.Start();
			
			InitializePlugins();
			InitializeInterface();
			
			LoadDefaultUser();
			
			controller.OnTrackStart += new EventHandler<EventArgs>(OnSelectedTrackStart);
			controller.OnTrackEnd += new EventHandler<EventArgs>(OnSelectedTrackEnd);
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