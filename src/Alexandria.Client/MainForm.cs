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
		#region Private Constants and ReadOnly Fields
		private readonly string tempPath = string.Format("{0}Alexandria\\", System.IO.Path.GetTempPath());
		#endregion
	
		#region Private Fields
		private Fmod.LocalSound audio;
		#endregion
	
		#region Constructors
		public MainForm()
		{
			try
			{
				InitializeComponent();				
				
				this.PlayPauseButton.Click += new EventHandler(PlayPauseButton_Click);
				this.StopButton.Click += new EventHandler(StopButton_Click);
			}
			catch (AlexandriaException ex)
			{

				MessageBox.Show(ex.Message, Resources.ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			}
		}
		#endregion

		#region OnLoad
		protected override void OnLoad(EventArgs e)
		{
			//TestLastFM();
			//TestMp3Tunes();
			
			base.OnLoad(e);
		}
		#endregion
		
		#region Private Methods
		private void TestMp3Tunes()
		{
			try
			{
				this.QueueListView.Columns.Add("#");
				this.QueueListView.Columns.Add("Name");
				this.QueueListView.Columns.Add("Artist");
				this.QueueListView.Columns.Add("Album");
				this.QueueListView.Columns.Add("Length");
				this.QueueListView.Columns.Add("Date");
				this.QueueListView.Columns.Add("Location");
				this.QueueListView.Columns.Add("TrackID");
				this.QueueListView.Columns.Add("Format");
				
				LoadStatus.Text = "Searching for tracks...";				
				Mp3Tunes.MusicLocker musicLocker = new Alexandria.Mp3Tunes.MusicLocker();
				musicLocker.Login("dan.poage@gmail.com", "automatic");								
				IList<IAudioTrack> tracks = musicLocker.GetTracks(false);
				if (tracks != null)
				{
					LoadStatus.Text = string.Format("Found {0,5} tracks...", tracks.Count);
					int loaded = 0;
					foreach(IAudioTrack track in tracks)
					{
						string[] data = new string[9];
						data[0] = track.TrackNumber.ToString();
						data[1] = track.Name;
						data[2] = track.Artist;
						data[3] = track.Album;
						data[4] = string.Format("{0}:{1:00}", track.Duration.Minutes, track.Duration.Seconds);
						data[5] = string.Format("{0:d}", track.ReleaseDate);
						data[6] = track.Location.Path;
						data[7] = track.Id.Value;											
						data[8] = track.Format;
					
						ListViewItem item = new ListViewItem(data);
						this.QueueListView.Items.Add(item);

						loaded++;
						LoadStatus.Text = string.Format("Loaded {0,5} of {1} tracks...", loaded, tracks.Count);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "MP3tunes error");
			}
		}
		
		private void TestLastFM()
		{
			try
			{
				IAudioscrobblerTrack track = new AudioscrobblerTrack();
				track.AlbumName = "Undertow";
				track.ArtistName = "Tool";
				track.TrackName = "Sober";
				track.MusicBrainzID = "0dfaa81e-9326-4eff-9604-c20d1c613227";
				track.TrackPlayed = DateTime.Now - new TimeSpan(0, 2, 4);
				track.TrackLength = new TimeSpan(0, 5, 6).Milliseconds;
				AudioscrobblerRequest request = new AudioscrobblerRequest();
				request.Username = "uberweasel";
				request.Password = "automatic";
				//request.SubmitTrack(track);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "LastFM error");
			}
		}
		
		private void TestFMOD()
		{
			audio = new Fmod.LocalSound(@"C:\Dev\Testing\Dael.OGG");
			audio.Load();
			audio.Play();		
		}
		
		private void TestMusicDNS()
		{
			Location location = new Location(@"D:\working\Tests\AudioTest\01 Bill Hicks - Intro.wav"); //08 Only.wav");
			MusicDns.MetadataFactory factory = new Alexandria.MusicDns.MetadataFactory();
			IAudioTrack track = factory.CreateAudioTrack(location);
			MessageBox.Show(string.Format("File: {0}\nPuid: {1}", track.Location.Path, track.Id.Value), "MusicDNS Test");
		}
		#endregion
		
		#region Private Event Methods
		private void PlayPauseButton_Click(object sender, EventArgs e)
		{	
			try
			{
				//TestFMOD();
				TestMusicDNS();
 			}
 			catch (Exception ex)
 			{
 				string a = ex.Message;
 			} 			
		}

		void StopButton_Click(object sender, EventArgs e)
		{
			if (audio != null)
			{
				audio.Stop();
				if (audio is IDisposable)
				{
					IDisposable disposable = audio as IDisposable;
					disposable.Dispose();
					audio = null;
				}
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			TestMp3Tunes();
		}
		#endregion

		private void DownloadButton_Click(object sender, EventArgs e)
		{		
			if (QueueListView.SelectedItems.Count > 0)
			{
				System.Net.WebClient client = new System.Net.WebClient();
				ListViewItem selectedItem = QueueListView.SelectedItems[0];
				Uri uri = new Uri(selectedItem.SubItems[6].Text);

				if (!System.IO.Directory.Exists(tempPath))
					System.IO.Directory.CreateDirectory(tempPath);
				
				string fileName = string.Format("{0}{1:##} {2}.{3}", tempPath, selectedItem.SubItems[0].Text, selectedItem.SubItems[1].Text, selectedItem.SubItems[8].Text);
				client.DownloadFileAsync(uri, fileName);
			}
		}
	}
}