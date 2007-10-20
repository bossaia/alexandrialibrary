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
using System.Data;
using System.Net;
using System.Windows.Forms;

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

namespace Alexandria.Client.Controllers
{
	public class QueueController
	{
		#region Constructors
		public QueueController()
		{
			queueTable = new DataTable("Queue");
			queueTable.Columns.Add(new DataColumn(COL_ID, typeof(Guid)));
			queueTable.Columns.Add(new DataColumn(COL_TYPE, typeof(string)));
			queueTable.Columns.Add(new DataColumn(COL_NUMBER, typeof(int)));
			queueTable.Columns.Add(new DataColumn(COL_NAME, typeof(string)));
			queueTable.Columns.Add(new DataColumn(COL_CREATOR, typeof(string)));
			queueTable.Columns.Add(new DataColumn(COL_SOURCE, typeof(string)));
			queueTable.Columns.Add(new DataColumn(COL_DURATION, typeof(TimeSpan)));
			queueTable.Columns.Add(new DataColumn(COL_DATE, typeof(DateTime)));
			queueTable.Columns.Add(new DataColumn(COL_FORMAT, typeof(string)));
			queueTable.Columns.Add(new DataColumn(COL_PATH, typeof(Uri)));
			
			bindingSource = new BindingSource();
			bindingSource.DataSource = queueTable;
		}
		#endregion

		#region Private Constant Fields
		private const string COL_ID = "IdColumn";
		private const string COL_TYPE = "TypeColumn";
		private const string COL_NUMBER = "NumberColumn";
		private const string COL_NAME = "NameColumn";
		private const string COL_CREATOR = "CreatorColumn";
		private const string COL_SOURCE = "SourceColumn";
		private const string COL_DURATION = "DurationColumn";
		private const string COL_DATE = "DateColumn";
		private const string COL_FORMAT = "FormatColumn";
		private const string COL_PATH = "PathColumn";
		
		private const string TYPE_AUDIO = "Audio";
		private const int INDEX_AUDIO = 0;
		private const string TYPE_IMAGE = "Image";
		private const int INDEX_IMAGE = 1;
		private const string TYPE_BOOK = "Book";
		private const int INDEX_BOOK = 2;
		private const string TYPE_MOVIE = "Movie";
		private const int INDEX_MOVIE = 3;
		private const string TYPE_TELEVISION = "TV";
		private const int INDEX_TELEVISION = 4;
		#endregion

		#region Private Fields
		private IAudioTrack selectedTrack;
		//private IAudioTrack submittedTrack;
		//private IAudioStream audioStream;
		private IList<IAudioTrack> tracks;

		MusicLocker locker = new MusicLocker();
		PlaylistFactory playlistFactory = new PlaylistFactory();
		TagLibEngine tagLibEngine = new TagLibEngine();

		private EventHandler<EventArgs> trackStart;
		private EventHandler<EventArgs> trackEnd;

		//private bool isPlaying;
		
		private EventHandler<QueueEventArgs> selectedTrackChanged;

		//private IPluginRepository repository;
		//private IPersistenceBroker broker;
		//private IPersistenceMechanism mechanism;
		private SimpleAlbumFactory albumFactory = new SimpleAlbumFactory();
		//private Alexandria.
		//private ListView queueListView;
		private DataTable queueTable;
		private BindingSource bindingSource;
		private DataGridView grid;
		private ImageList smallImageList;
		
		//private ListViewItem selectedItem;
		private DataGridViewRow selectedRow;
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		
		private PlaybackController playbackController;
		#endregion

		#region Private Methods
		private Guid GetItemGuid(DataGridViewCell cell)
		{
			return (cell.Value != null) ? (Guid)cell.Value : Guid.NewGuid();
		}
		
		private string GetItemString(DataGridViewCell cell)
		{
			return (cell.Value != null) ? cell.Value.ToString() : string.Empty; 
		}
		
		private int GetItemInt(DataGridViewCell cell)
		{
			return (cell.Value != null) ? (int)cell.Value : 0;
		}
		
		private TimeSpan GetItemTimeSpan(DataGridViewCell cell)
		{
			return (cell.Value != null) ? (TimeSpan)cell.Value : TimeSpan.Zero;
		}
		
		private DateTime GetItemDateTime(DataGridViewCell cell)
		{
			return (cell.Value != null) ? (DateTime)cell.Value : DateTime.MinValue;
		}
		
		private Uri GetItemUri(DataGridViewCell cell)
		{
			return (cell.Value != null) ? (Uri)cell.Value : null;
		}
		
		private IAudioTrack GetSelectedAudioTrack(DataGridViewRow row)
		{
			Guid id = GetItemGuid(row.Cells[COL_ID]);
			string type = GetItemString(row.Cells[COL_TYPE]);
			int number = GetItemInt(row.Cells[COL_NUMBER]);
			string name = GetItemString(row.Cells[COL_NAME]);
			string creator = GetItemString(row.Cells[COL_CREATOR]);
			string source = GetItemString(row.Cells[COL_SOURCE]);
			TimeSpan duration = GetItemTimeSpan(row.Cells[COL_DURATION]);
			DateTime date = GetItemDateTime(row.Cells[COL_DATE]);
			string format = GetItemString(row.Cells[COL_FORMAT]);
			Uri path = GetItemUri(row.Cells[COL_PATH]);
			
			IAudioTrack track = new Alexandria.Metadata.BaseAudioTrack(id, path, name, source, creator, duration, date, number, format); 
			return track;
		}
		
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

		#region Private Event Methods
		void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (queueTable.Columns[e.ColumnIndex].ColumnName == COL_DURATION)
			{
				TimeSpan duration = (TimeSpan)e.Value;
				if (duration.Hours > 0)
				{
					e.Value = string.Format("{0:00}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
				}
				else if (duration.Minutes > 0)
				{
					e.Value = string.Format("{0:00}:{1:00}", duration.Minutes, duration.Seconds);
				}
				else
				{
					e.Value = string.Format("0:{0:00}", duration.Seconds);
				}
			}
			if (queueTable.Columns[e.ColumnIndex].ColumnName == COL_TYPE)
			{
				switch(e.Value.ToString())
				{
					case TYPE_AUDIO:
						e.Value = smallImageList.Images[INDEX_AUDIO];
						break;
					case TYPE_BOOK:
						e.Value = smallImageList.Images[INDEX_BOOK];
						break;
					case TYPE_IMAGE:
						e.Value = smallImageList.Images[INDEX_IMAGE];
						break;
					case TYPE_MOVIE:
						e.Value = smallImageList.Images[INDEX_MOVIE];
						break;
					case TYPE_TELEVISION:
						e.Value = smallImageList.Images[INDEX_TELEVISION];
						break;
					default:
						e.Value = smallImageList.Images[INDEX_AUDIO];
						break;
				}
			}
		}
		#endregion

		#region Public Properties
		public DataGridView Grid
		{
			get { return grid; }
			set {
				grid = value;
				if (grid != null)
				{
					grid.AutoGenerateColumns = false;
					grid.DataSource = bindingSource;
					grid.CellFormatting += new DataGridViewCellFormattingEventHandler(grid_CellFormatting);
				}
			}
		}
		
		//public ListView QueueListView
		//{
			//get { return queueListView; }
			//set { queueListView = value; }
		//}
		
		public PlaybackController PlaybackController
		{
			get { return playbackController; }
			set { playbackController = value; }
		}
		
		public IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}

		//public IAudioStream AudioStream
		//{
			//get { return audioStream; }
		//}

		//public bool IsMuted
		//{
			//get
			//{
				//if (audioStream != null)
				//{
					//return audioStream.IsMuted;
				//}
				//else return false;
			//}
		//}

		public EventHandler<EventArgs> TrackStart
		{
			get { return trackStart; }
			set { trackStart = value; }
		}

		public EventHandler<EventArgs> TrackEnd
		{
			get { return trackEnd; }
			set { trackEnd = value; }
		}

		//public float Volume
		//{
			//get
			//{
				//if (audioStream != null)
					//return audioStream.Volume;
				//else return -1;
			//}
			//set
			//{
				//if (audioStream != null)
					//audioStream.Volume = value;
			//}
		//}

		public IAudioTrack SelectedTrack
		{
			get { return selectedTrack; }
			set { selectedTrack = value; }
		}
		
		public EventHandler<QueueEventArgs> SelectedTrackChanged
		{
			get { return selectedTrackChanged; }
			set { selectedTrackChanged = value; }
		}
		
		public ImageList SmallImageList
		{
			get { return smallImageList; }
			set { smallImageList = value; }
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
			IAudioStream audioStream = null;
			
			if (grid.SelectedRows.Count > 0)
			{
				//# Name Artist Album Length Date Location Format
				//if (QueueListView.SelectedItems[0] != selectedItem)
				if (grid.SelectedRows[0] != selectedRow)
				{
					//selectedItem = QueueListView.SelectedItems[0];
					selectedRow = grid.SelectedRows[0];
					//selectedItem.Tag != null)
					if (selectedRow.Cells.Count > 0)
					{
						//TODO: move all of this logic into AudioPlayer
						//selectedTrack = (IAudioTrack)selectedItem.Tag;
						selectedTrack = GetSelectedAudioTrack(selectedRow);
						if (selectedTrack.Format == "cdda")
						{
							string discPath = selectedTrack.Path.LocalPath.Substring(0, 2);
							audioStream = new Fmod.CompactDiscSound(discPath);
							audioStream.StreamIndex = selectedTrack.TrackNumber-1;
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
								//selectedItem.SubItems[4].Text = GetDurationString(audioStream.Duration);
								selectedRow.Cells[6].Value = audioStream.Duration;
							}
						}
						
						playbackController.LoadAudioStream(audioStream);
					}
					else throw new ApplicationException("Could not load selected track: Id was undefined");
				}
			}
		}
		
		public void LoadTrack(IAudioTrack track)
		{
			object[] data = new object[10];
			data[0] = track.Id;
			data[1] = TYPE_AUDIO;
			data[2] = track.TrackNumber;
			data[3] = track.Name;
			data[4] = track.Artist;
			data[5] = track.Album;
			data[6] = track.Duration; //GetDurationString(track.Duration);
			data[7] = track.ReleaseDate; //GetDateString(track.ReleaseDate);
			data[8] = track.Format.ToLowerInvariant();
			data[9] = track.Path; //track.Path.LocalPath;

			//ListViewItem item = new ListViewItem(data);
			//item.Tag = track;
			//QueueListView.Items.Add(item);
			//grid.Rows.Add(data);
			queueTable.Rows.Add(data);
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
				else return path.EndsWith(format, StringComparison.InvariantCultureIgnoreCase);
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

		public void LoadData(IDataObject data)
		{
			if (data != null)
			{
				object sourceData = data.GetData(typeof(TrackSource));
				if (sourceData != null)
				{
					TrackSource trackSource = (TrackSource)sourceData;
					IList<IAudioTrack> tracks = trackSource.GetAudioTracks();
					LoadTracks(tracks);
				}
			}
		}

		public void Play()
		{
			/*
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
			*/
		}


		public void Previous()
		{
			if (playbackController != null)
			{
				bool isPlaying = false;
				if (playbackController.AudioPlayer.CurrentAudioStream != null && playbackController.AudioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing)
					isPlaying = true;
				
				if (isPlaying)
					playbackController.AudioPlayer.Stop();

				//TODO: implement this logic
				//SelectedTrack = ChangeSelectedTrack(-1); //NOTE: use this to support "shuffle"
				//if (OnSelectedTrackChanged != null)
					//OnSelectedTrackChanged(this, new QueueEventArgs());

				//if (QueueListView.SelectedItems[0] != null)
				if (grid.SelectedRows[0] != null)
				{
					int previousIndex = grid.Rows.Count - 1;
					if (grid.SelectedRows[0].Index > 0)
						previousIndex = grid.SelectedRows[0].Index - 1;
						
					grid.SelectedRows[0].Selected = false;
					grid.Rows[previousIndex].Selected = true;
					
					//QueueListView.Items.Count - 1;
					//if (QueueListView.SelectedIndices[0] > 0)
						//previousIndex = QueueListView.SelectedIndices[0] - 1;

					//QueueListView.SelectedItems[0].Selected = false;
					//QueueListView.Items[previousIndex].Selected = true;
				}
				
				SelectTrack();
				
				if (isPlaying)
					playbackController.AudioPlayer.Play();
			}
		}

		public void Next()
		{
			if (playbackController != null)
			{
				bool isPlaying = false;
				if (playbackController.AudioPlayer.CurrentAudioStream != null && playbackController.AudioPlayer.CurrentAudioStream.PlaybackState == PlaybackState.Playing)
					isPlaying = true;
				
				if (isPlaying)
					playbackController.AudioPlayer.Stop();

				//TODO: implement this logic
				//SelectedTrack = ChangeSelectedTrack(1);
				//if (OnSelectedTrackChanged != null)
				//OnSelectedTrackChanged(this, new QueueEventArgs());

				//if (QueueListView.SelectedItems[0] != null)
				if (grid.SelectedRows[0] != null)
				{
					int nextIndex = 0;
					if (grid.SelectedRows[0].Index < grid.Rows.Count - 1)
						nextIndex = grid.SelectedRows[0].Index + 1;
						
					grid.SelectedRows[0].Selected = false;
					grid.Rows[nextIndex].Selected = true;	
					
					//if (QueueListView.SelectedIndices[0] < QueueListView.Items.Count - 1)
						//nextIndex = QueueListView.SelectedIndices[0] + 1;

					//QueueListView.SelectedItems[0].Selected = false;
					//QueueListView.Items[nextIndex].Selected = true;
				}

				SelectTrack();

				if (isPlaying)
					playbackController.AudioPlayer.Play();
			}
		}

		/*
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
		*/
		#endregion
	}
}
