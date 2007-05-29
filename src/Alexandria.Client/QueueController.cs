using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Alexandria;

namespace Alexandria.Client
{
	public class QueueController
	{
		#region Constructors
		public QueueController(ListView queueListView)
		{
			this.queueListView = queueListView;
		}
		#endregion
	
		#region Private Constants and ReadOnly Fields
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		#endregion

		#region Private Fields
		private ListView queueListView;
		private ListViewItem selectedItem;
		private IAudioTrack selectedTrack;
		private IAudio audio;
		//Fmod.LocalSound audio;
		#endregion

		#region Private Methods
		private void LoadTrack(IAudioTrack track)
		{
			string[] data = new string[8];
			data[0] = track.TrackNumber.ToString();
			data[1] = track.Name;
			data[2] = track.Artist;
			data[3] = track.Album;
			data[4] = string.Format("{0}:{1:00}", track.Duration.Minutes, track.Duration.Seconds);
			data[5] = string.Format("{0:d}", track.ReleaseDate);
			data[6] = track.Location.Path;
			data[7] = track.Format;

			ListViewItem item = new ListViewItem(data);
			item.Tag = track.Id;
			this.queueListView.Items.Add(item);
		}

		private void Download(ListViewItem selectedItem)
		{
			if (selectedItem != null)
			{
				System.Net.WebClient client = new System.Net.WebClient();
				Uri uri = new Uri(selectedItem.SubItems[6].Text);

				if (!System.IO.Directory.Exists(tempPath))
					System.IO.Directory.CreateDirectory(tempPath);

				string fileName = string.Format("{0}{1:##} {2}.{3}", tempPath, selectedItem.SubItems[0].Text, selectedItem.SubItems[1].Text, selectedItem.SubItems[8].Text);
				client.DownloadFileAsync(uri, fileName);
			}
		}
		
		private void TestMp3Tunes()
		{
			try
			{
				//LoadStatus.Text = "Searching for tracks...";
				Mp3Tunes.MusicLocker musicLocker = new Alexandria.Mp3Tunes.MusicLocker();
				musicLocker.Login("dan.poage@gmail.com", "automatic");
				IList<IAudioTrack> tracks = musicLocker.GetTracks(false);
				if (tracks != null)
				{
					//LoadStatus.Text = string.Format("Found {0,5} tracks...", tracks.Count);
					int loaded = 0;
					foreach (IAudioTrack track in tracks)
					{
						LoadTrack(track);
						loaded++;
						//LoadStatus.Text = string.Format("Loaded {0,5} of {1} tracks...", loaded, tracks.Count);
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
				LastFM.IAudioscrobblerTrack track = new LastFM.AudioscrobblerTrack();
				track.AlbumName = "Undertow";
				track.ArtistName = "Tool";
				track.TrackName = "Sober";
				track.MusicBrainzID = "0dfaa81e-9326-4eff-9604-c20d1c613227";
				track.TrackPlayed = DateTime.Now - new TimeSpan(0, 2, 4);
				track.TrackLength = new TimeSpan(0, 5, 6).Milliseconds;
				LastFM.AudioscrobblerRequest request = new LastFM.AudioscrobblerRequest();
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
		
		#region Public Methods
		public void LoadTracks(IList<IAudioTrack> tracks)
		{
			queueListView.Items.Clear();
			if (tracks != null)
			{
				foreach (IAudioTrack track in tracks)
				{
					LoadTrack(track);
				}
			}
		}
		
		public void SelectTrack()
		{
			if (queueListView.SelectedItems.Count > 0)
			{
				//# Name Artist Album Length Date Location Format
				selectedItem = queueListView.SelectedItems[0];
				IIdentifier id = (IIdentifier)selectedItem.Tag;
				int trackNumber = Convert.ToInt32(selectedItem.SubItems[0].Text);
				string name = selectedItem.SubItems[1].Text;
				string artist = selectedItem.SubItems[2].Text;
				string album = selectedItem.SubItems[3].Text;
				string[] durationParts = selectedItem.SubItems[4].Text.Split(':');
				int hours = 0; int minutes = 0; int seconds = 0;
				if (durationParts != null && durationParts.Length > 1)
				{
					minutes = Convert.ToInt32(durationParts[0]);
					seconds = Convert.ToInt32(durationParts[1]);
				}				
				TimeSpan duration = new TimeSpan(hours, minutes, seconds);
				DateTime releaseDate = Convert.ToDateTime(selectedItem.SubItems[5].Text);
				ILocation location = new Location(selectedItem.SubItems[6].Text);
				string format = selectedItem.SubItems[7].Text;
				selectedTrack = new BaseAudioTrack(id, location, name, album, artist, duration, releaseDate, trackNumber, format);
				
				if (selectedTrack.Location.IsLocal)
				{
					audio = new Fmod.LocalSound(selectedTrack.Location.Path);
					audio.Load();
				}
				else
				{
					string fileName = string.Format("{0}{1:00,2} {2} - {3} - {4}.{5}", tempPath, selectedTrack.TrackNumber, selectedTrack.Name, selectedTrack.Artist, selectedTrack.Album, selectedTrack.Format);
					if (!System.IO.File.Exists(fileName))
					{
						WebClient client = new WebClient();
						client.DownloadFile(selectedTrack.Location.Path, fileName);
					}
					
					audio = new Fmod.LocalSound(fileName);
					audio.Load();
				}
			}
			
		}
		
		public void Play()
		{
			if (audio != null)
			{
				if (audio.PlaybackState != PlaybackState.Playing)
					audio.Play();
				else audio.Pause();
			}
		}
		
		public void Stop()
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
		#endregion
	}
}
