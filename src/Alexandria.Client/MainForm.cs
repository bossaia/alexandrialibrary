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
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Alexandria;
using Alexandria.Catalog;
using Alexandria.Media;
using Alexandria.Media.IO;
using Alexandria.Metadata;
using Alexandria.Persistence;
using Alexandria.Plugins;

using Alexandria.Client.Controllers;
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
		private QueueController controller = new QueueController();
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
		
		//QueueController fields
		private ListViewItem selectedItem;
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
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
			Guid userId = new Guid("FC26A3CC-91DC-4d8b-BC54-F28DAE5BD9D6");
			IUser user = broker.LookupRecord<IUser>(userId);
			if (user != null)
			{
				//TODO: allow the default catalog to be user-defined
				if (user.Catalogs != null && user.Catalogs.Count > 0)
				{
					LoadTracks(user.Catalogs[0].Tracks);
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
			//LoadTracks();
			//if (Tracks.Count > 0)
			//{
				//int x = Tracks.Count;

				//Guid userId = new Guid("FC26A3CC-91DC-4d8b-BC54-F28DAE5BD9D6");
				//IUser user = broker.LookupRecord<IUser>(userId);				
				//foreach(IAudioTrack track in Tracks)
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
			ToolBoxListView.Items.Clear();
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

		#region QueueController Methods

		#region LoadTrack
		private void LoadTrack(IAudioTrack track)
		{
			string[] data = new string[8];
			data[0] = track.TrackNumber.ToString();
			data[1] = track.Name;
			data[2] = track.Artist;
			data[3] = track.Album;
			data[4] = GetDurationString(track.Duration);
			data[5] = GetDateString(track.ReleaseDate);
			data[6] = track.Path.LocalPath;
			data[7] = track.Format.ToLowerInvariant();

			ListViewItem item = new ListViewItem(data);
			item.Tag = track;
			//if (track.MetadataIdentifiers != null && track.MetadataIdentifiers.Count > 0)
			//item.Tag = track.MetadataIdentifiers[0];

			QueueListView.Items.Add(item);
		}
		#endregion

		#region LoadTracks
		private void LoadTracks()
		{
			IList<IAudioTrack> tracks = GetMp3TunesTracks(false);
			LoadTracks(tracks);
		}

		private void LoadTracks(IList<IAudioTrack> tracks)
		{
			QueueListView.Items.Clear();
			if (tracks != null)
			{
				foreach (IAudioTrack track in tracks)
				{
					LoadTrack(track);
				}
			}
		}
		#endregion

		#region SelectTrack
		public void SelectTrack()
		{
			if (QueueListView.SelectedItems.Count > 0)
			{
				//# Name Artist Album Length Date Location Format
				if (QueueListView.SelectedItems[0] != selectedItem)
				{
					selectedItem = QueueListView.SelectedItems[0];
					if (selectedItem.Tag != null)
					{
						selectedTrack = (IAudioTrack)selectedItem.Tag;
						if (selectedTrack.Format == "cdda")
						{
							string discPath = selectedTrack.Path.LocalPath.Substring(0, 2);
							audioStream = new Fmod.CompactDiscSound(discPath);
							audioStream.StreamIndex = selectedTrack.TrackNumber;
						}
						else
						{
							if (selectedTrack.Path.IsFile)
							{
								audioStream = new Fmod.LocalSound(selectedTrack.Path.LocalPath);
								audioStream.StreamIndex = 0;
							}
							else
							{
								string fileName = string.Format("{0}{1:00,2} {2} - {3} - {4}.{5}", tempPath, selectedTrack.TrackNumber, selectedTrack.Name, selectedTrack.Artist, selectedTrack.Album, selectedTrack.Format);
								fileName = CleanupFileName(fileName);
								if (!System.IO.File.Exists(fileName))
								{
									if (!System.IO.Directory.Exists(tempPath))
										System.IO.Directory.CreateDirectory(tempPath);

									WebClient client = new WebClient();
									Uri address = locker.GetLockerPath(selectedTrack.Path.ToString());
									try
									{
										client.DownloadFile(address, fileName);
									}
									catch (WebException ex)
									{
										throw new ApplicationException("There was an error downloading track : " + selectedTrack.Name, ex);
									}
								}

								audioStream = new Fmod.LocalSound(fileName);
								audioStream.StreamIndex = 0;
							}

							if (audioStream != null && audioStream.Duration != selectedTrack.Duration && audioStream.Duration != TimeSpan.Zero)
							{
								selectedItem.SubItems[4].Text = GetDurationString(audioStream.Duration);
							}
						}
					}
					else throw new ApplicationException("Could not load selected track: Id was undefined");
				}
			}
		}
		#endregion

		#region LoadTrackFromPath
		private void LoadTrackFromPath(string path)
		{
			LoadTrackFromPath(new Uri(path));
		}

		private void LoadTrackFromPath(Uri path)
		{
			if (path != null)
			{
				try
				{
					IAudioTrack track = tagLibEngine.GetAudioTrack(path);
					if (track != null)
						LoadTrack(track);
				}
				catch (System.IO.FileNotFoundException)
				{
					MessageBox.Show(string.Format("The file does not exist: {0}", path.LocalPath), "Error Loading Track");
				}
			}
			else MessageBox.Show("The file path is not defined", "Error Loading Track");
		}
		#endregion

		#region Private Fields
		//private IPluginRepository repository;
		//private IPersistenceBroker broker;
		private IAudioTrack selectedTrack;
		private IAudioTrack submittedTrack;
		private IAudioStream audioStream;
		private IList<IAudioTrack> tracks;

		MusicLocker locker = new MusicLocker();
		PlaylistFactory playlistFactory = new PlaylistFactory();
		TagLibEngine tagLibEngine = new TagLibEngine();

		private EventHandler<EventArgs> onTrackStart;
		private EventHandler<EventArgs> onTrackEnd;

		private bool isPlaying;
		#endregion

		#region Private Properties
		private IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}

		private IAudioStream AudioStream
		{
			get { return audioStream; }
		}

		private bool IsMuted
		{
			get
			{
				if (audioStream != null)
				{
					return audioStream.IsMuted;
				}
				else return false;
			}
		}

		private EventHandler<EventArgs> OnTrackStart
		{
			get { return onTrackStart; }
			set { onTrackStart = value; }
		}

		private EventHandler<EventArgs> OnTrackEnd
		{
			get { return onTrackEnd; }
			set { onTrackEnd = value; }
		}

		private float Volume
		{
			get
			{
				if (audioStream != null)
					return audioStream.Volume;
				else return -1;
			}
			set
			{
				if (audioStream != null)
					audioStream.Volume = value;
			}
		}

		private IAudioTrack SelectedTrack
		{
			get { return selectedTrack; }
			set { selectedTrack = value; }
		}
		#endregion

		#region Private Methods
		private string CleanupFileName(string fileName)
		{
			const char safeChar = '_';

			if (fileName.Length > 2)
			{
				string filePostfix = fileName.Substring(2, fileName.Length - 2).Replace(':', '_');
				fileName = fileName.Substring(0, 2) + filePostfix;
			}

			fileName = fileName.Replace('/', safeChar);
			fileName = fileName.Replace('?', safeChar);
			fileName = fileName.Replace('*', safeChar);

			return fileName;
		}

		private string GetDateString(DateTime date)
		{
			if (date == DateTime.MinValue)
				return string.Empty;
			else if (date.Year == 1600)
				return string.Empty;
			else if (date.Year == 1900)
				return string.Empty;
			else if (date.Month == 1 && date.Day == 1)
				return date.Year.ToString();
			else return string.Format("{0:d}", date);
		}

		private IList<IAudioTrack> GetMp3TunesTracks(bool ignoreCache)
		{
			try
			{
				Mp3Tunes.MusicLocker musicLocker = new Alexandria.Mp3Tunes.MusicLocker();
				musicLocker.Login("dan.poage@gmail.com", "automatic");
				tracks = musicLocker.GetTracks(ignoreCache);
				return tracks;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error loading MP3tunes tracks");
				return null;
			}
		}

		private void SubmitTrackToLastFM(IAudioTrack track)
		{
			try
			{
				LastFM.AudioscrobblerRequest request = new Alexandria.LastFM.AudioscrobblerRequest();
				request.Username = "uberweasel";
				request.Password = "automatic";
				request.SubmitTrack(track);

				/*
				LastFM.IAudioscrobblerTrack lastFMtrack = new LastFM.AudioscrobblerTrack();
				track.AlbumName = track.Album; "Undertow"
				track.ArtistName = track.Artist; "Tool"
				track.TrackName = track.Name; "Sober"
				track.TrackNumber = 3; 
				AudioTrackId = "441a8b6f-d6df-4e6e-bd9c-547a1616ac48" 
				MetadataId   = "90748683-cb71-4e3d-98aa-57a964b60eB0"
				track.MusicBrainzID =  "0dfaa81e-9326-4eff-9604-c20d1c613227";
				track.TrackPlayed = DateTime.Now - new TimeSpan(0, 2, 4);
				track.TrackLength = new TimeSpan(0, 5, 6).TotalMilliseconds;
				LastFM.AudioscrobblerRequest request = new LastFM.AudioscrobblerRequest();
				request.Username = "uberweasel";
				request.Password = "automatic";
				//request.SubmitTrack(track);
				*/
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "LastFM error");
			}
		}

		private IMetadataIdentifier LookupPuid(Uri path)
		{
			MusicDns.MetadataFactory factory = new Alexandria.MusicDns.MetadataFactory();
			IAudioTrack track = factory.CreateAudioTrack(path);
			foreach (IMetadataIdentifier metadataId in track.MetadataIdentifiers)
			{
				if (metadataId.Type.Contains("MusicDnsId"))
					return metadataId;
			}
			return null;
		}

		private bool IsFormat(string path, string format)
		{
			if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(format))
			{
				if (format.Contains(","))
				{
					string[] formats = format.Split(',');
					foreach (string subFormat in formats)
						if (path.EndsWith(subFormat, StringComparison.InvariantCultureIgnoreCase)) return true;
					return false;
				}
				else
					return path.EndsWith(format, StringComparison.InvariantCultureIgnoreCase);
			}
			return false;
		}

		private string GetDurationString(TimeSpan duration)
		{
			return string.Format("{0}:{1:00}", Convert.ToInt32(Math.Truncate(duration.TotalMinutes)), Convert.ToInt32(Math.Truncate(duration.TotalSeconds % 60)));
		}

		private void OpenFile(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				if (IsFormat(path, "xspf,m3u"))
				{
					IPlaylist playlist = playlistFactory.CreatePlaylist(new Uri(path));
					playlist.Load();
					foreach (IPlaylistItem item in playlist.Items)
						LoadTrackFromPath(item.Path);
				}
				else if (IsFormat(path, "ogg,flac,mp3,wma,aac"))
				{
					LoadTrackFromPath(path);
				}
			}
		}

		private void Play()
		{
			if (audioStream != null)
			{
				if (audioStream.PlaybackState != PlaybackState.Playing)
				{
					if (audioStream.PlaybackState == PlaybackState.Paused)
					{
						isPlaying = true;
						audioStream.Resume();
					}
					else
					{
						if (audioStream.PlaybackState == PlaybackState.Stopped)
						{
							if (OnTrackStart != null)
								OnTrackStart(audioStream, EventArgs.Empty);
						}

						if (submittedTrack != null && selectedTrack != null)
						{
							if (submittedTrack.Album != selectedTrack.Album && submittedTrack.Artist != selectedTrack.Artist && submittedTrack.Name != selectedTrack.Name)
							{
								SubmitTrackToLastFM(selectedTrack);
								submittedTrack = selectedTrack;
							}
						}

						isPlaying = true;
						audioStream.Play();
					}
				}
				else
				{
					isPlaying = false;
					audioStream.Pause();
				}
			}
			else
			{
				SelectTrack();
				if (audioStream != null)
					Play();
			}
		}

		private void Stop()
		{
			if (audioStream != null)
			{
				isPlaying = false;
				audioStream.Stop();
				if (audioStream is IDisposable)
				{
					IDisposable disposable = audioStream as IDisposable;
					disposable.Dispose();
					audioStream = null;
				}
			}
		}

		private void Previous()
		{
			if (isPlaying)
				Stop();

			if (QueueListView.SelectedItems[0] != null)
			{
				int previousIndex = QueueListView.Items.Count - 1;
				if (QueueListView.SelectedIndices[0] > 0)
					previousIndex = QueueListView.SelectedIndices[0] - 1;

				QueueListView.SelectedItems[0].Selected = false;
				QueueListView.Items[previousIndex].Selected = true;
			}
		}

		private void Next()
		{
			if (isPlaying)
				Stop();

			if (QueueListView.SelectedItems[0] != null)
			{
				int nextIndex = 0;
				if (QueueListView.SelectedIndices[0] < QueueListView.Items.Count - 1)
					nextIndex = QueueListView.SelectedIndices[0] + 1;

				QueueListView.SelectedItems[0].Selected = false;
				QueueListView.Items[nextIndex].Selected = true;
			}
		}

		private void UpdateStatus()
		{
			if (audioStream != null && isPlaying)
			{
				if (audioStream.Elapsed >= audioStream.Duration)
				{
					Stop();
					if (OnTrackEnd != null)
						OnTrackEnd(audioStream, EventArgs.Empty);
				}
			}
		}

		private void Mute()
		{
			if (audioStream != null)
			{
				audioStream.IsMuted = !audioStream.IsMuted;
			}
		}	
		#endregion
		
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
					OpenFile(FileOpenDialog.FileName);
				}
			}
		}

		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
			if (controller != null && AudioStream != null)
			{
				Play();
				if (AudioStream.PlaybackState == PlaybackState.Playing)
					PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_pause_blue;
				else PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
			}
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				Stop();
				PlaybackTrackBar.Value = 0;
				PlaybackTrackBar.Enabled = false;
				PlayPauseButton.BackgroundImage = Alexandria.Client.Properties.Resources.control_play_blue;
			}
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				Mute();
				if (IsMuted)
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
				Volume = GetVolume();
		}

		private void PreviousButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				bool isPlaying = (AudioStream != null && AudioStream.PlaybackState == PlaybackState.Playing);
				Previous();
				if (isPlaying)
					PlayPauseButton_Click(sender, EventArgs.Empty);
			}
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			if (controller != null)
			{
				bool isPlaying = (AudioStream != null && AudioStream.PlaybackState == PlaybackState.Playing);
				Next();
				if (isPlaying)
					PlayPauseButton_Click(sender, EventArgs.Empty);
			}
		}

		private void QueueListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (controller != null && QueueListView.SelectedItems.Count > 0)
			{
				SelectTrack();
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
				if (controller != null && AudioStream != null)
				{
					int value = Convert.ToInt32(AudioStream.Elapsed.TotalSeconds);
					if (value <= PlaybackTrackBar.Maximum)
						PlaybackTrackBar.Value = value;
					else PlaybackTrackBar.Value = PlaybackTrackBar.Maximum;
					
					UpdateStatus();
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
			if (controller != null && AudioStream != null)
			{
				if (SelectedTrack != null)
					NowPlayingLabel.Text = string.Format("{0} - {1}", SelectedTrack.Artist, SelectedTrack.Name);
				
				Volume = GetVolume();
				VolumeTrackBar.Enabled = true;

				PlaybackTrackBar.Enabled = true;
				PlaybackTrackBar.Minimum = 0;
				PlaybackTrackBar.Maximum = Convert.ToInt32(AudioStream.Duration.TotalSeconds);
				PlaybackTrackBar.Value = 0;
			}
		}
		
		private void OnSelectedTrackEnd(object sender, EventArgs e)
		{
			if (controller != null)
			{
				Next();
				Play();
			}
		}

		private void PlaybackTrackBar_MouseDown(object sender, MouseEventArgs e)
		{
			isSeeking = true;
		}

		private void PlaybackTrackBar_MouseUp(object sender, MouseEventArgs e)
		{
			isSeeking = false;
			if (AudioStream != null)
			{
				if (AudioStream.CanSetElapsed)
				{
					AudioStream.Elapsed = new TimeSpan(0, 0, PlaybackTrackBar.Value);
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
					LoadTracks(tracks);
				}
			}
		}

		private void ToolBoxContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem.Text == "Refresh")
			{
				InitializeToolbox();
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
			
			OnTrackStart += new EventHandler<EventArgs>(OnSelectedTrackStart);
			OnTrackEnd += new EventHandler<EventArgs>(OnSelectedTrackEnd);
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