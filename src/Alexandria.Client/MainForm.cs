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

				Init();
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion
		
		#region Private Fields
		private string loadedFile;
		private uint position;
		private uint length;
		private AudioPlayer audioPlayer;
		//private DataFactory dataFactory;
		private TagEngine tagEngine;
		private MetadataProvider metadataProvider;
		//private AudioPlaybackFunction playDelegate = null;
		//private AudioPlaybackFunction pauseDelegate = null;
		//private AudioPlaybackFunction stopDelegate = null;
		private bool seeking;
		//private XmlShareablePlaylist playlist;
		private int currentTrackIndex = -1;
		private bool trackBarIsInitialized;
		private bool soundLoadHasTimedOut;
		//private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(
		//("Resources", System.Reflection.Assembly.GetExecutingAssembly());
		#endregion

		#region Private Constant Fields
		private const string DIALOG_OK = "OK";
		private string RIP_DIR = System.Environment.CurrentDirectory + @"\Rip\";
		#endregion
	
		#region Private Properties
		private string Marquee
		{
			set {this.Text = Resources.ApplicationTitle + value;}
		}
		
		private uint Position
		{
			set
			{
				this.position = value;
				UpdatePosition();
			}
		}
		
		private uint Length
		{
			set
			{
				this.length = value;
				UpdatePosition();
			}
		}
		#endregion
				
		#region Private Methods
		private void Init()
		{
			bool testLastFM = true;
			if (testLastFM)
			{
				try
				{
					Alexandria.LastFM.LastFMPlugin plugin = new Alexandria.LastFM.LastFMPlugin();
					plugin.SetUserPassword("uberweasel", "automatic");
					plugin.Start();
					DateTime started = DateTime.Now.AddSeconds(-250);
					plugin.Queue.Add("Tool", "Undertow", "Sober", "0dfaa81e-9326-4eff-9604-c20d1c613227", 306, started);
					//plugin.Queue.RemoveRange(0, 1);
					//plugin.Stop();
					//MessageBox.Show("Track submitted to Last.fm", "Test Last.fm");
					//plugin.Stop();
					//Alexandria.LastFM.l
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "There was an error submitting to LastFM");
				}
			}
		
			bool testSQLite = false;
			if (testSQLite)
			{
				try
				{
					Alexandria.SQLite.SQLiteDataProvider provider = new Alexandria.SQLite.SQLiteDataProvider();
					string data = provider.Test();
					MessageBox.Show(data, "SQLite Test");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "There was an error with SQLite");
				}
			}
		
			bool testPandora = false;
			if (testPandora)
			{
				Alexandria.Pandora.PandoraClient pandoraClient = new Alexandria.Pandora.PandoraClient();
				pandoraClient.FindFiles();
				pandoraClient.DownloadFiles();
			}
		
			bool testImdb = false;
			if (testImdb)
			{
				Alexandria.Imdb.ImdbMetadataProvider imdbProvider = new Alexandria.Imdb.ImdbMetadataProvider();
				List<Alexandria.Imdb.Movie> movies = imdbProvider.SearchMovie("Mutiny on the Bounty");
				string movieList = string.Empty;
				foreach (Imdb.Movie movie in movies)
				{
					movieList += movie.lTitles[0].sTitle + " (" + movie.iYear.ToString() + ")\n";
				}
				
				MessageBox.Show(movieList, "IMDB Search Test");
			}
		
			bool testIdentifier = false;
			if (testIdentifier)
			{
				//string path = @"C:\Dev\Testing\Dael.ogg";
				//string path = @"C:\Dev\Testing\BachGavotte.mp3";
				//string path = @"C:\Dev\Testing\PurcellSongMus.mp3";
				//string path = @"C:\Dev\Testing\02 Mushaboom.mp3";
				//string path = @"C:\Dev\Testing\03 Phloam.mp3";
				//string path = @"C:\Dev\Testing\03 Bonus High Frequency Sounds.wma";
				string path = string.Empty; //NOTE: Assign a real path to identify a file
				
				MessageBox.Show("The file " + path + " has a mime type of:\n\n" + FileTools.FileIdentifier.GetContentType(path).ToString(), "Resource Identifier");
			}
			
			bool testAscii = false;
			if (testAscii)
			{
				string imageFileName = @"C:\Documents and Settings\dan\My Documents\My Pictures\T_beeker.jpg";
				string outputFileName = @"C:\test.txt";
				Image image = Image.FromFile(imageFileName);
				
				Alexandria.AsciiGenerator.AsciiConversion.TextProcessingSettings settings = new Alexandria.AsciiGenerator.AsciiConversion.TextProcessingSettings();
				settings.Size = new Size(image.Width, image.Height);
				settings.CalculateCharacterSize = true;
				settings.Font = Alexandria.AsciiGenerator.Variables.DefaultFont;
				//settings.ValidCharacters = (string)Alexandria.AsciiGenerator.Variables.DefaultRamps[0];
				
				//StringBuilder builder = new StringBuilder();
				System.IO.StreamWriter writer = new System.IO.StreamWriter(outputFileName);
				string[] text = Alexandria.AsciiGenerator.AsciiConversion.AsciiConverter.Convert(image, settings);
				foreach(string line in text)
				{
					writer.WriteLine(line);
					//builder.AppendLine(line);
				}
				
				MessageBox.Show("Image file " + imageFileName + " converted to text file: " + outputFileName, "Image to ASCII Test");
				//int x = text.Length;
			}
		
			bool testAmazon = false;
			if (testAmazon)
			{
				try
				{
					Amazon.WebServiceClient client = new Amazon.WebServiceClient();
					string results = client.BasicSearch(Alexandria.Amazon.SearchMode.Books, "Sharks");
					MessageBox.Show(results, "Amazon Test");
				}
				catch (Exception ex)
				{
					string x = ex.Message;
				}
			}
			
			bool testTags = false;
			
			if (testTags)
			{
				try
				{
					tagEngine = PluginManager.TagEngine;
					if (tagEngine != null)
					{
						//IAudioTag tag = tagEngine.GetAudioTag(new LocalMediaFile("C:\test.ogg"));
					}
				}
				catch (AlexandriaException ex)
				{
					MessageBox.Show(Resources.ErrorMessage_TagEngine + ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				}
			}
		
			bool testDb = false;
			
			if (testDb)
			{
				try
				{
					/*
					dataFactory = PluginManager.DataFactory;
					
					User user = dataFactory.GetUser("Administrator");
					if (user == null)
					{
						user = new User();
						user.Name = "Administrator";
						user.Password = "notsecret";
							
						dataFactory.AddUser(user);
					}
					else
					{
						//string name = user.Password;
					}
					*/
				}
				catch (AlexandriaException ex)
				{
					MessageBox.Show(Resources.ErrorMessage_SecurityCreateAdminUser + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				}
			}

			bool testMeta = false;
			if (testMeta)
			{
				try
				{
					metadataProvider = PluginManager.MetadataProvider;
					
					//OpticalDrive cdDrive = null;
					
					//foreach(Drive drive in Drive.Drives)
					//{
						//string x = drive.Name;
					//}
						//OpticalDrive cdDrive = drive as OpticalDrive;
						//if (cdDrive != null)
						//{
						OpticalDrive cdDrive = new OpticalDrive(@"E:\");
						IAlbumResource album = metadataProvider.GetAlbum(cdDrive);
						//MessageBox.Show(album.MusicBrainzId + " : " + album.Name, "MusicBrainz Test", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
						//}
					//}
				}
				catch (AlexandriaException ex)
				{
					MessageBox.Show("Error with metadata lookup:\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				}
			}

			try
			{
				audioPlayer = PluginManager.AudioPlayer;
				tagEngine = PluginManager.TagEngine;
			
				// wire the events to our test methods
				audioPlayer.OnPlay += new EventHandler<PlaybackEventArgs>(OnPlay);
				audioPlayer.OnPause += new EventHandler<PlaybackEventArgs>(OnPause);
				audioPlayer.OnStop += new EventHandler<PlaybackEventArgs>(OnStop);
				audioPlayer.OnPlaybackTimerTick += new EventHandler<System.Timers.ElapsedEventArgs>(OnAudioPlayerTimerTick);
				audioPlayer.OnSoundLoadTimeout += new EventHandler<EventArgs>(OnSoundLoadTimeout);
				audioPlayer.OnPlaybackStatusChange += new EventHandler<PlaybackEventArgs>(OnPlaybackStatusChange);
				audioPlayer.OnStreamingStatusChange += new EventHandler<PlaybackEventArgs>(OnStreamingStatusChange);
				audioPlayer.OnRippingStatusChange += new EventHandler<PlaybackEventArgs>(OnRippingStatusChange);
				
				//playDelegate = new AudioPlaybackFunction(audioPlayer.Play);
				//pauseDelegate = new AudioPlaybackFunction(audioPlayer.Pause);
				//stopDelegate = new AudioPlaybackFunction(audioPlayer.Stop);
			
				this.PlaybackStatusLabel.Text = audioPlayer.CurrentStatus;

				//this.PlaybackTimer.Enabled = true;
				this.PlaybackTimer.Start();
			
				// Initialize the Manifest
				MediaFileFormat.InitializeManifest();								
			}
			catch (AlexandriaException ex)
			{
				MessageBox.Show("Could not initialize playback engine\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}

		private void UpdatePosition()
		{
			this.PositionStatus.Text = this.position.ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "/" + this.length.ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + " ms";
		}

		private void OpenFile(string path)
		{
			if (!String.IsNullOrEmpty(path))
			{
				// Load a playlist
				if (path.EndsWith(".xspf", true, System.Globalization.CultureInfo.CurrentCulture))
				{
					LoadPlaylist(path);
				}
                else if (path.EndsWith(".m3u", true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    LoadM3uPlaylist(path);
                }
                else // Load an individual file
                {
                    LoadMediaFile(MediaFile.Load(path));
                }
			}
		}
		
		private void LoadPlaylist(string path)
		{
			try
			{
				/*
				this.QueueListView.Items.Clear();
		
				//TODO: add the code to dynamically load the appropriate playlist format
				//playlist = new XmlShareablePlaylist(path);
				playlist.Load();
				
				this.QueueGroupBox.Text = playlist.Name;
				
				//TODO: change the design of MediaQueue so that
				//a delegate does not have to be passed to the constructor
				MediaController controller = new MediaController(audioPlayer.GetAudioInfoHandler);
				controller.Load(playlist, tagEngine);
				
				int count = 0;
				foreach(MediaItem mediaItem in controller.Items)
				{
					count++;
					
					string[] itemFields = new string[5];
					itemFields[0] = count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
					itemFields[1] = mediaItem.Info.Title;
					itemFields[2] = mediaItem.Info.Artist;
					itemFields[3] = mediaItem.Info.FormattedLength;
					itemFields[4] = mediaItem.File.Path;
				
					ListViewItem viewItem = new ListViewItem(itemFields);
					this.QueueListView.Items.Add(viewItem);
				}

				if (this.QueueListView.Items.Count > 0)
				{
					this.QueueListView.Items[0].Selected = true;
				}
				
				this.QueueListView.Refresh();
				*/
			}
			catch (AlexandriaException ex)
			{
				MessageBox.Show("There was an error loading this playlist: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}

        private void LoadM3uPlaylist(string path)
        {
            try
            {
				/*
                this.QueueListView.Items.Clear();

                M3uPlaylist m3uPlaylist = new M3uPlaylist(path);
                m3uPlaylist.Load();
                this.QueueGroupBox.Text = path;

                //TODO: change the design of MediaQueue so that
                //a delegate does not have to be passed to the constructor
                MediaController controller = new MediaController(audioPlayer.GetAudioInfoHandler);
                controller.Load(m3uPlaylist, tagEngine);

                int count = 0;
                foreach (MediaItem mediaItem in controller.Items)
                {
                    count++;

                    string[] itemFields = new string[5];
                    itemFields[0] = count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    itemFields[1] = mediaItem.Info.Title;
                    itemFields[2] = mediaItem.Info.Artist;
                    itemFields[3] = mediaItem.Info.FormattedLength;
                    itemFields[4] = mediaItem.File.Path;

                    ListViewItem viewItem = new ListViewItem(itemFields);
                    this.QueueListView.Items.Add(viewItem);
                }

                if (this.QueueListView.Items.Count > 0)
                {
                    this.QueueListView.Items[0].Selected = true;
                }

                this.QueueListView.Refresh();
                */
            }
            catch (AlexandriaException ex)
            {
				MessageBox.Show("There was an error loading this playlist: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
		
		private void LoadMediaFile(MediaFile mediaFile)
		{
			audioPlayer.SetCurrentMediaFile(mediaFile);
			this.Marquee = audioPlayer.CurrentSound.Name;
			this.loadedFile = mediaFile.Path;
			this.Position = 0;
			
			//NOTE: remote files have an unknown length
			if (mediaFile.IsLocal)
			{
				uint ms = audioPlayer.CurrentSound.Milliseconds;
				this.Length = ms;
				this.PlaybackTrackBar.SetRange(0, Convert.ToInt32(ms));
				trackBarIsInitialized = true;
			}
		}
		
		private void SetVolume()
		{
			float volume = (float)Convert.ToDouble(.10 * this.VolumeTrackBar.Value);
			this.audioPlayer.Volume = volume;
		}
		
		private string SuspendPlayback()
		{
			string status = audioPlayer.CurrentStatus;
			if (audioPlayer.CurrentStatus == PlaybackStatus.Playing.Name || audioPlayer.CurrentStatus == PlaybackStatus.Paused.Name)
			{
				StopButton_Click(this, EventArgs.Empty);
			}
			return status;
		}
		
		private void ResumePlayback(string status)
		{
			if (status == "Playing")
			{
				PlayPauseButton_Click(this, EventArgs.Empty);
			}
		}
		
		private bool MoveTrack(int move)
		{
			int newTrackIndex = currentTrackIndex + move;
			if (newTrackIndex > -1 && newTrackIndex < this.QueueListView.Items.Count)
			{
				return SelectTrack(newTrackIndex);
			}
			else return false;
		}
		
		private bool SelectTrack(int trackIndex)
		{
			bool trackSelected = false;
			
			try
			{
				currentTrackIndex = trackIndex;
				this.QueueListView.SelectedItems[0].Selected = false;
				ListViewItem item = this.QueueListView.Items[currentTrackIndex];
				item.Selected = true;
				//string path = item.SubItems[4].Text;
				//LoadMediaFile(playlist.Files[currentTrackIndex]);
				trackSelected = true;
			}
			catch (ApplicationException)
			{
				trackSelected = false;
			}
			
			return trackSelected;
		}
		
		private bool PreviousTrack()
		{
			if (this.QueueListView.Items.Count > 0 && currentTrackIndex > -1)
			{
				if (currentTrackIndex > 0)
				{
					return MoveTrack(-1);
				}
				else return SelectTrack(this.QueueListView.Items.Count-1);
			}
			
			return false;
		}
		
		private bool NextTrack()
		{
			if (this.QueueListView.Items.Count > 0 && currentTrackIndex > -1)
			{
				if (currentTrackIndex+1 < (this.QueueListView.Items.Count))
				{
					return MoveTrack(1);
				}
				else return SelectTrack(0);
			}
			
			return false;
		}

		private void InitializeTrackBar()
		{
			this.PlaybackTrackBar.SetRange(0, Convert.ToInt32(this.audioPlayer.CurrentSound.MediaFile.Length));
			trackBarIsInitialized = true;
		}

		private void TrackLoadFailed()
		{
			// Stop the playback
			this.StopButton_Click(this, System.EventArgs.Empty);
			
			//Reset the timeout flag
			soundLoadHasTimedOut = false;
		
			// Try to advance to the next track and resume playback there
			if (NextTrack())
			{
				this.PlayPauseButton_Click(this, System.EventArgs.Empty);
			}
		}
		#endregion
		
		#region Private Event Methods
		private void OnPlay(object sender, PlaybackEventArgs e)
		{
			//string x = "OnPlay";
		}
		
		private void OnPause(object sender, PlaybackEventArgs e)
		{
		}
		
		private void OnStop(object sender, PlaybackEventArgs e)
		{
			if (audioPlayer.CurrentSound != null && audioPlayer.CurrentSound.MediaFile != null)
			{
				if (!audioPlayer.CurrentSound.MediaFile.IsLocal)
				{
					//string x = "?";
				}
			}
		}
		
		private void OnTrackEnd(object sender, PlaybackEventArgs e)
		{
			this.StopButton_Click(this, EventArgs.Empty);
		
			if (NextTrack())
			{
				PlayPauseButton_Click(this, e);
			}
		}

		private void OnAudioPlayerTimerTick(object sender, System.Timers.ElapsedEventArgs e)
		{
		}
		
		private void OnSoundLoadTimeout(object sender, System.EventArgs e)
		{
			if (!soundLoadHasTimedOut)
			{
				soundLoadHasTimedOut = true;
			
				SimpleEvent timeoutDelegate = new SimpleEvent(TrackLoadFailed);
				
				this.Invoke(timeoutDelegate);
			}
		}

		private void PlaybackTimer_Tick(object sender, EventArgs e)
		{
			if (this.audioPlayer != null && this.audioPlayer.CurrentSound != null)
			{
				//NOTE: audioPlayer.Position does not seem to be accurate for remote streams
				if (!seeking && audioPlayer.CurrentSound.MediaFile.IsLocal)
				{
					int position = Convert.ToInt32(audioPlayer.Position);
					if (position >= this.PlaybackTrackBar.Minimum && position <= this.PlaybackTrackBar.Maximum)
					{
						this.PlaybackTrackBar.Value = position;
					}
				}

				if (audioPlayer.CurrentSound.MediaFile.IsLocal)
				{					
					if (this.PlaybackTrackBar.Value < this.PlaybackTrackBar.Maximum)
					{
						//int addInterval = (this.PlaybackTrackBar.Maximum - this.PlaybackTrackBar.Value) >= (int)this.PlaybackTimer.Interval ? (int)this.PlaybackTimer.Interval : this.PlaybackTrackBar.Maximum - this.PlaybackTrackBar.Value;
						//this.PlaybackTrackBar.Value += addInterval;
						this.Position = this.audioPlayer.Position;
					}
					else
					{
						PlaybackEventArgs onTrackEndArgs = new PlaybackEventArgs();
												
						OnTrackEnd(this, onTrackEndArgs);
					}
				}
				else
				{
					this.PlaybackStatusLabel.Text = this.audioPlayer.CurrentSound.OpenStateName + "  " + this.audioPlayer.CurrentSound.PercentBuffered.ToString(System.Globalization.NumberFormatInfo.InvariantInfo) + "%";
					
					if (this.audioPlayer.CurrentSound.OpenStateName == "Ready" && !trackBarIsInitialized)
					{
						InitializeTrackBar();
					}
					
					this.PlaybackTrackBar.Value = Convert.ToInt32(audioPlayer.Position);
				}
			}
		}		

		private void PlayPauseButton_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.loadedFile))
			{
				audioPlayer.Play();
				SetVolume();
				this.PlaybackStatusLabel.Text = audioPlayer.CurrentStatus;
				
				if (audioPlayer.CurrentStatus == PlaybackStatus.Playing.Name)
				{
					// Playing
					this.PlayPauseButton.Text = Resources.PlaybackAction_Pause; //PLAYBACK_STATUS_PAUSE;
				}
				else
				{
					// Paused
					this.PlayPauseButton.Text = Resources.PlaybackAction_Play; //PLAYBACK_STATUS_PLAY;
				}
			}
			else MessageBox.Show(Resources.ErrorMessage_PlaybackTrackNotSelected, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.loadedFile))
			{
				audioPlayer.Stop();
				this.PlaybackStatusLabel.Text = audioPlayer.CurrentStatus;
				this.loadedFile = null;
				this.Position = 0;
				this.Length = 0;
				this.PlayPauseButton.Text = Resources.PlaybackAction_Play; //PLAYBACK_STATUS_PLAY;
				this.PlaybackTrackBar.Value = 0;
			}
		}

		private void PlaybackTrackBar_Scroll(object sender, EventArgs e)
		{
			if (audioPlayer.CurrentStatus == "Playing" || this.audioPlayer.CurrentStatus == "Paused")
			{
				seeking = true;
			}
		}

		private void PlaybackTrackBar_MouseUp(object sender, MouseEventArgs e)
		{
			if (seeking)
			{
				seeking = false;
				this.audioPlayer.SetPosition(Convert.ToUInt32(this.PlaybackTrackBar.Value));
			}
		}

		private void QueueListView_ItemActivate(object sender, EventArgs e)
		{
			if (QueueListView.SelectedItems[0] != null)
			{
				ListViewItem item = QueueListView.SelectedItems[0];
			
				currentTrackIndex = QueueListView.SelectedIndices[0];
			
				string path = item.SubItems[4].Text;
				string length = item.SubItems[3].Text;
				if (path != null)
				{
					if (audioPlayer.CurrentStatus == "Playing" || audioPlayer.CurrentStatus == "Paused")
					{
						StopButton_Click(this, EventArgs.Empty);
					}
					
					//TODO: change this to get the MediaFile back from the playlist/queue
					MediaFile mediaFile = MediaFile.Load(path, length);
					LoadMediaFile(mediaFile);
					
					PlayPauseButton_Click(this, EventArgs.Empty);
				}
			}
		}

		private void MuteButton_Click(object sender, EventArgs e)
		{
			if (this.MuteButton.Text == Resources.PlaybackAction_Mute) // VOLUME_STATUS_MUTE
			{
				this.audioPlayer.IsMuted = true;
				this.VolumeTrackBar.Enabled = false;
				this.MuteButton.Text = Resources.PlaybackAction_Unmute; //VOLUME_STATUS_UNMUTE;
			}
			else
			{
				this.audioPlayer.IsMuted = false;
				this.VolumeTrackBar.Enabled = true;
				this.MuteButton.Text = Resources.PlaybackAction_Mute; //VOLUME_STATUS_MUTE;
			}
		}

		private void VolumeTrackBar_Scroll(object sender, EventArgs e)
		{
			SetVolume();
		}

		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult result = this.FileOpenDialog.ShowDialog();
			if (result.ToString() == DIALOG_OK)
			{
				string path = this.FileOpenDialog.FileName;
				OpenFile(path);
			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void PreviousButton_Click(object sender, EventArgs e)
		{
			string status = SuspendPlayback();
			PreviousTrack();
			ResumePlayback(status);
		}

		private void NextButton_Click(object sender, EventArgs e)
		{
			string status = SuspendPlayback();
			NextTrack();
			ResumePlayback(status);
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.QueueListView.SelectedItems.Count > 0)
				{
					string name = this.QueueListView.SelectedItems[0].SubItems[1].Text;
					string sourceFilePath = this.QueueListView.SelectedItems[0].SubItems[4].Text;
					MediaFile file = MediaFile.Load(sourceFilePath);
					string destinationFilePath = RIP_DIR + name + "." + file.Format.DefaultFileExtension.ToString().ToLower(System.Globalization.CultureInfo.InvariantCulture);
				
					audioPlayer.SaveStreamToLocalFile(sourceFilePath, destinationFilePath);
					//Sound sound = 
					//audioPlayer.CurrentSound.SaveToWavFile(@"C:\test.wav");
				}
				
			}
			catch (AlexandriaException ex)
			{
				MessageBox.Show(Resources.ErrorMessage_SaveRemoteStream + ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		
		private void OnPlaybackStatusChange(object sender, PlaybackEventArgs e)
		{
			//string x = e.PlaybackStatus;
		}
		
		private void OnStreamingStatusChange(object sender, PlaybackEventArgs e)
		{
			this.StreamingStatus.Text = e.StreamingStatus;
			if (e.StreamingStatus == "Streaming Timeout")
			{
				if (!soundLoadHasTimedOut)
				{
					OnSoundLoadTimeout(this, System.EventArgs.Empty);
				}
			}
		}
		
		private void OnRippingStatusChange(object sender, PlaybackEventArgs e)
		{
			//string x = e.RippingStatus;
		}
		#endregion
	}
}