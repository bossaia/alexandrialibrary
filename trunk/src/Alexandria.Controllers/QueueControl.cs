#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

#region Using
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;
using Alexandria.Metadata;
using Alexandria.Persistence;
using Alexandria.Plugins;

using Alexandria.AlbumArtDownloader;
using Alexandria.Amazon;
using Alexandria.AsciiGenerator;
using Alexandria.CompactDiscTools;
using Alexandria.Db4o;
using Alexandria.Fmod;
using Alexandria.FreeDB;
using Alexandria.Google;
using Alexandria.Imdb;
using Alexandria.LastFM;
using Alexandria.MediaInfo;
using Alexandria.Mp3Tunes;
using Alexandria.MusicBrainz;
using Alexandria.MusicDns;
using Alexandria.Playlist;
using Alexandria.SQLite;
using Alexandria.TagLib;
#endregion

namespace Alexandria.Controllers
{
	public class QueueController
	{
		#region Constructors
		public QueueController()
		{
		}
		#endregion

		#region Private Fields
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
		
		private EventHandler<QueueEventArgs> onSelectedTrackChanged;

		//private IPluginRepository repository;
		//private IPersistenceBroker broker;
		//private IPersistenceMechanism mechanism;
		private SimpleAlbumFactory albumFactory = new SimpleAlbumFactory();
		//private Alexandria.
		#endregion

		#region Private Methods
		private void LoadTrackFromPath(string path)
		{
			LoadTrackFromPath(new Uri(path));
		}

		private void LoadTrackFromPath(Uri path)
		{
			if (path != null)
			{
				//try
				//{
					IAudioTrack track = tagLibEngine.GetAudioTrack(path);
					if (track != null)
						LoadTrack(track);
				//}
				//catch (System.IO.FileNotFoundException)
				//{
					//MessageBox.Show(string.Format("The file does not exist: {0}", path.LocalPath), "Error Loading Track");
				//}
			}
			//else MessageBox.Show("The file path is not defined", "Error Loading Track");
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

		public IAudioTrack SelectedTrack
		{
			get { return selectedTrack; }
			set { selectedTrack = value; }
		}
		
		public EventHandler<QueueEventArgs> OnSelectedTrackChanged
		{
			get { return onSelectedTrackChanged; }
			set { onSelectedTrackChanged = value; }
		}
		#endregion

		#region Public Methods
		public void LoadTracks()
		{
			IList<IAudioTrack> tracks = GetMp3TunesTracks(false);
			LoadTracks(tracks);
		}

		public void LoadTracks(IList<IAudioTrack> tracks)
		{
			//QueueListView.Items.Clear();
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
			/*
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
			*/
		}
		
		public void LoadTrack(IAudioTrack track)
		{
			/*
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
			*/
		}
		
		public string CleanupFileName(string fileName)
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

		public string GetDateString(DateTime date)
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

		public IList<IAudioTrack> GetMp3TunesTracks(bool ignoreCache)
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
				throw new AlexandriaException("There was an error loading tracks from your MP3tunes locker", ex);
				//MessageBox.Show(ex.Message, "Error loading MP3tunes tracks");
				//return null;
			}
		}

		public void SubmitTrackToLastFM(IAudioTrack track)
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
				//MessageBox.Show(ex.Message, "LastFM error");
				throw new AlexandriaException("There was an error submitting this track to Last.fm", ex);
			}
		}

		public IMetadataIdentifier LookupPuid(Uri path)
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

		public bool IsFormat(string path, string format)
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

		public string GetDurationString(TimeSpan duration)
		{
			return string.Format("{0}:{1:00}", Convert.ToInt32(Math.Truncate(duration.TotalMinutes)), Convert.ToInt32(Math.Truncate(duration.TotalSeconds % 60)));
		}

		public void OpenFile(string path)
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

			//TODO: implement this logic
			//SelectedTrack = ChangeSelectedTrack(-1); //NOTE: use this to support "shuffle"
			//if (OnSelectedTrackChanged != null)
				//OnSelectedTrackChanged(this, new QueueEventArgs());

			/*
			if (QueueListView.SelectedItems[0] != null)
			{
				int previousIndex = QueueListView.Items.Count - 1;
				if (QueueListView.SelectedIndices[0] > 0)
					previousIndex = QueueListView.SelectedIndices[0] - 1;

				QueueListView.SelectedItems[0].Selected = false;
				QueueListView.Items[previousIndex].Selected = true;
			}
			*/
		}

		public void Next()
		{
			if (isPlaying)
				Stop();

			//TODO: implement this logic
			//SelectedTrack = ChangeSelectedTrack(1);
			//if (OnSelectedTrackChanged != null)
			//OnSelectedTrackChanged(this, new QueueEventArgs());

			/*
			if (QueueListView.SelectedItems[0] != null)
			{
				int nextIndex = 0;
				if (QueueListView.SelectedIndices[0] < QueueListView.Items.Count - 1)
					nextIndex = QueueListView.SelectedIndices[0] + 1;

				QueueListView.SelectedItems[0].Selected = false;
				QueueListView.Items[nextIndex].Selected = true;
			}
			*/
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
