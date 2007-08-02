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
using System.Net;
using System.Text;
using System.Windows.Forms;
using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;
using Alexandria.Metadata;
using Alexandria.Persistence;
using Alexandria.Playlist;
using Alexandria.Plugins;
using Alexandria.TagLib;
using Alexandria.Fmod;
using Alexandria.Mp3Tunes;

namespace Alexandria.Client
{
	public class QueueController
	{
		#region Constructors
		public QueueController(ListView queueListView, IPersistenceBroker broker, IPluginRepository repository)
		{
			this.queueListView = queueListView;
			this.broker = broker;
			
			this.locker = new MusicLocker();
			//locker.Login("dan.poage@gmail.com", "automatic");
			
			Fmod.ConfigurationSettings configSettings = new Alexandria.Fmod.ConfigurationSettings();
			
			//TODO: move to Fmod.ConfigurationSettings
			this.repository = repository;
			Fmod.ConfigurationSettings fmodConfig = (Fmod.ConfigurationSettings)repository.GetConfigurationMap("Alexandria.Fmod").Settings;
			if (fmodConfig != null)
			{
				Fmod.SoundSystemFactory.DefaultSoundSystem.OutputType = fmodConfig.OutputType;
			}
		}
		#endregion
	
		#region Private Constants and ReadOnly Fields
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		#endregion

		#region Private Fields
		private IPluginRepository repository;
		private IPersistenceBroker broker;
		private ListView queueListView;
		private ListViewItem selectedItem;
		private IAudioTrack selectedTrack;
		private IAudioTrack submittedTrack;
		private IAudioStream audioStream;
		//private int audioStreamIndex;
		private IList<IAudioTrack> tracks;
		
		MusicLocker locker;
		PlaylistFactory playlistFactory = new PlaylistFactory();
		TagLibEngine tagLibEngine = new TagLibEngine();
		
		private EventHandler<EventArgs> onTrackStart;
		private EventHandler<EventArgs> onTrackEnd;
		
		private bool isPlaying;
		#endregion

		#region Private Methods
		private string CleanupFileName(string fileName)
		{
			const char safeChar = '_';
			
			if (fileName.Length > 2)
			{
				string filePostfix = fileName.Substring(2, fileName.Length-2).Replace(':', '_');
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
			foreach(IMetadataIdentifier metadataId in track.MetadataIdentifiers)
			{
				if (metadataId.Type.Contains("MusicDnsId"))
					return metadataId;
			}
			return null;
		}
		
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
		
		private bool IsFormat(string path, string format)
		{
			if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(format))
			{
				if (format.Contains(","))
				{
					string[] formats = format.Split(',');
					foreach(string subFormat in formats)
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
		#endregion
		
		#region Public Properties
		public IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}
		
		public IAudioStream AudioStream
		{
			get { return audioStream; }
		}
		
		public bool IsMuted
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
		
		public EventHandler<EventArgs> OnTrackStart
		{
			get { return onTrackStart; }
			set { onTrackStart = value; }
		}
		
		public EventHandler<EventArgs> OnTrackEnd
		{
			get { return onTrackEnd; }
			set { onTrackEnd = value; }
		}

		public float Volume
		{
			get {
				if (audioStream != null)
					return audioStream.Volume;
				else return -1;
			}
			set {
				if (audioStream != null)
					audioStream.Volume = value;
			}
		}
		
		public IAudioTrack SelectedTrack
		{
			get { return selectedTrack; }
		}
		#endregion
		
		#region Public Methods
		public void OpenFile(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				if (IsFormat(path, "xspf,m3u"))
				{
					IPlaylist playlist = playlistFactory.CreatePlaylist(new Uri(path));
					playlist.Load();
					foreach(IPlaylistItem item in playlist.Items)
						LoadTrackFromPath(item.Path);
				}
				else if (IsFormat(path, "ogg,flac,mp3,wma,aac"))
				{
					LoadTrackFromPath(path);
				}
			}
		}
		
		public void LoadTrack(IAudioTrack track)
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

			this.queueListView.Items.Add(item);
		}

		public void LoadTracks()
		{
			IList<IAudioTrack> tracks = GetMp3TunesTracks(false);
			LoadTracks(tracks);
		}		
		
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
				if (queueListView.SelectedItems[0] != selectedItem)
				{
					selectedItem = queueListView.SelectedItems[0];
					if (selectedItem.Tag != null)
					{
						selectedTrack = (IAudioTrack)selectedItem.Tag;
						if (selectedTrack.Format == "cdda")
						{
							string discPath = selectedTrack.Path.LocalPath.Substring(0,2);
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

							if (audioStream != null && audioStream.Duration != selectedTrack.Duration)
							{
								selectedItem.SubItems[4].Text = GetDurationString(audioStream.Duration);
							}
						}
					}
					else throw new ApplicationException("Could not load selected track: Id was undefined");
				}
			}
		}

		#region Old SelectTrack code
		/*
		//IMetadataIdentifier id = (IMetadataIdentifier)selectedItem.Tag;
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
		Uri path = new Uri(selectedItem.SubItems[6].Text);
		string format = selectedItem.SubItems[7].Text;
		
		selectedTrack = null;
		
		//TODO: fix this to use an IPersistenceBroker
		selectedTrack = new BaseAudioTrack(Guid.NewGuid(), path, name, album, artist, duration, releaseDate, trackNumber, format);
		selectedTrack.MetadataIdentifiers.Add(id);
		
		if (selectedTrack.Path.IsFile)
		{
			audio = new Fmod.LocalSound(selectedTrack.Path.ToString());
			//audio.Load();
		}
		else
		{
			string downloadPath = string.Format("{0}{1:00,2} {2} - {3} - {4}.{5}", tempPath, selectedTrack.TrackNumber, selectedTrack.Name, selectedTrack.Artist, selectedTrack.Album, selectedTrack.Format);
			if (!System.IO.File.Exists(downloadPath))
			{
				WebClient client = new WebClient();
				client.DownloadFile(selectedTrack.Path, downloadPath);
			}
			
			//location = new Location(path);
			audio = new Fmod.LocalSound(path.ToString());
			//audio.Load();
		}
		*/
		#endregion
		
		public void Play()
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
		
		public void Stop()
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
		
		public void Previous()
		{
			if (isPlaying)
				Stop();
				
			if (queueListView.SelectedItems[0] != null)
			{
				int previousIndex = queueListView.Items.Count-1;
				if (queueListView.SelectedIndices[0] > 0)
					previousIndex = queueListView.SelectedIndices[0] - 1;

				queueListView.SelectedItems[0].Selected = false;
				queueListView.Items[previousIndex].Selected = true;
			}
		}
		
		public void Next()
		{
			if (isPlaying)
				Stop();
			
			if (queueListView.SelectedItems[0] != null)
			{
				int nextIndex = 0;
				if (queueListView.SelectedIndices[0] < queueListView.Items.Count-1)
					nextIndex = queueListView.SelectedIndices[0] + 1;
					
				queueListView.SelectedItems[0].Selected = false;
				queueListView.Items[nextIndex].Selected = true;
			}
		}
		
		public void UpdateStatus()
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
		
		public void Mute()
		{
			if (audioStream != null)
			{
				audioStream.IsMuted = !audioStream.IsMuted;
			}
		}		
		#endregion
	}
}
