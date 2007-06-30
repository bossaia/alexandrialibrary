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
		private IAudioTrack submittedTrack;
		private IAudioStream audio;
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
			data[6] = track.Path.ToString();
			data[7] = track.Format;

			ListViewItem item = new ListViewItem(data);
			item.Tag = track.MetadataIdentifiers[0];
			this.queueListView.Items.Add(item);
		}
		
		private IList<IAudioTrack> GetMp3TunesTracks(bool ignoreCache)
		{
			try
			{				
				Mp3Tunes.MusicLocker musicLocker = new Alexandria.Mp3Tunes.MusicLocker();
				musicLocker.Login("dan.poage@gmail.com", "automatic");
				return musicLocker.GetTracks(ignoreCache);
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

		private IIdentifier LookupPuid(Uri path)
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
		#endregion
		
		#region Public Methods
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
				selectedItem = queueListView.SelectedItems[0];
				IMetadataIdentifier id = (IMetadataIdentifier)selectedItem.Tag;
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
			}
			
		}
		
		public void Play()
		{
			if (audio != null)
			{
				if (audio.PlaybackState != PlaybackState.Playing)
				{
					if (audio.PlaybackState == PlaybackState.Paused)
					{
						audio.Resume();
					}
					else
					{
						if (submittedTrack != null && selectedTrack != null)
						{
							if (submittedTrack.Album != selectedTrack.Album &&
								submittedTrack.Artist != selectedTrack.Artist &&
								submittedTrack.Name != selectedTrack.Name)
							{
								SubmitTrackToLastFM(selectedTrack);
								submittedTrack = selectedTrack;
							}
						}
						audio.Play();
					}
				}
				else audio.Pause();
			}
			else
			{
				SelectTrack();
				if (audio != null)
					Play();
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
