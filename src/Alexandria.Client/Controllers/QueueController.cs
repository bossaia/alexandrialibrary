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
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Alexandria;
using Alexandria.Client.Views;
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
			bindingList = new BindingListView<IMediaItem>();
			bindingList.AllowRemove = true;
			
			bindingSource = new BindingSource();
			bindingSource.DataSource = bindingList;
		}
		#endregion

		#region Private Constant Fields
		private const string COL_ID = "Id";
		private const string COL_STATUS = "Status";
		private const string COL_TYPE = "Type";
		private const string COL_SOURCE = "Source";
		private const string COL_NUMBER = "Number";
		private const string COL_TITLE = "Title";
		private const string COL_ARTIST = "Artist";
		private const string COL_ALBUM = "Album";
		private const string COL_DURATION = "Duration";
		private const string COL_DATE = "Date";
		private const string COL_FORMAT = "Format";
		private const string COL_PATH = "Path";
		
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
		private BindingListView<IMediaItem> bindingList; 
		private IList<IAudioTrack> tracks;

		MusicLocker locker = new MusicLocker();
		PlaylistFactory playlistFactory = new PlaylistFactory();
		TagLibEngine tagLibEngine = new TagLibEngine();
		
		private EventHandler<QueueEventArgs> selectedTrackChanged;

		private SimpleAlbumFactory albumFactory = new SimpleAlbumFactory();

		private BindingSource bindingSource;
		private AdvancedDataGridView grid;
		private ListView sortListView;
		private ImageList smallImageList;
		
		private DataGridViewRow selectedRow;
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		
		private PlaybackController playbackController;
		#endregion

		#region Private Properties
		private DataGridViewRow SelectedRow
		{
			get { return selectedRow;  }
			set {
				if (selectedRow != null && selectedRow != value)
				{
					//bindingList[selectedRow.Index].Status = string.Empty;
					//bindingList.ResetBindings();
					//selectedRow.Cells[COL_STATUS].Value = string.Empty;
					grid.Rows[selectedRow.Index].Cells[COL_STATUS].Value = string.Empty;
				}
				 
				selectedRow = value;
			}
		}
		#endregion

		#region Private Methods
		//private void TestSort()
		//{
		//    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(IMediaItem));
		//    ListSortDescription[] sortArray = new ListSortDescription[2];
		//    sortArray[0] = new ListSortDescription(properties.Find("Format", true), ListSortDirection.Ascending);
		//    sortArray[1] = new ListSortDescription(properties.Find("Title", true), ListSortDirection.Ascending);
		//    ListSortDescriptionCollection sorts = new ListSortDescriptionCollection(sortArray);
		//    ((IBindingListView)bindingList).ApplySort(sorts);
		//}
		
		//private void InitDataTable(DataTable queueTable)
		//{
		//    queueTable.Columns.Add(new DataColumn(COL_ID, typeof(Guid)));
		//    queueTable.Columns.Add(new DataColumn(COL_TYPE, typeof(string)));
		//    queueTable.Columns.Add(new DataColumn(COL_SOURCE, typeof(string)));
		//    queueTable.Columns.Add(new DataColumn(COL_NUMBER, typeof(int)));
		//    queueTable.Columns.Add(new DataColumn(COL_TITLE, typeof(string)));
		//    queueTable.Columns.Add(new DataColumn(COL_ARTIST, typeof(string)));
		//    queueTable.Columns.Add(new DataColumn(COL_ALBUM, typeof(string)));
		//    queueTable.Columns.Add(new DataColumn(COL_DURATION, typeof(TimeSpan)));
		//    queueTable.Columns.Add(new DataColumn(COL_DATE, typeof(DateTime)));
		//    queueTable.Columns.Add(new DataColumn(COL_FORMAT, typeof(string)));
		//    queueTable.Columns.Add(new DataColumn(COL_PATH, typeof(Uri)));
		//}
		
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
			string source = GetItemString(row.Cells[COL_SOURCE]);
			int number = GetItemInt(row.Cells[COL_NUMBER]);
			string title = GetItemString(row.Cells[COL_TITLE]);
			string artist = GetItemString(row.Cells[COL_ARTIST]);
			string album = GetItemString(row.Cells[COL_ALBUM]);
			TimeSpan duration = GetItemTimeSpan(row.Cells[COL_DURATION]);
			DateTime date = GetItemDateTime(row.Cells[COL_DATE]);
			string format =  GetItemString(row.Cells[COL_FORMAT]);
			Uri path = GetItemUri(row.Cells[COL_PATH]);
			
			IAudioTrack track = new Alexandria.Metadata.BaseAudioTrack(id, path, title, album, artist, duration, date, number, format);
			return track;
		}
		
		private void LoadTrackFromPath(string path, string source)
		{
			LoadTrackFromPath(new Uri(path), source);
		}

		private void LoadTrackFromPath(Uri path, string source)
		{
			if (path != null)
			{
				if (System.IO.File.Exists(path.LocalPath))
				{
					IAudioTrack track = tagLibEngine.GetAudioTrack(path);
					if (track != null)
						LoadTrack(track, source);
				}
			}
		}
		
		private bool ValuesAreEquivalent(object value1, object value2)
		{
			if (value1 != null && value2 != null)
			{
				return (value1.ToString() == value2.ToString());
			}
			else return (value1 == null && value2 == null);
		}
		
		private bool RowsAreEquivalent(DataGridViewRow row1, DataGridViewRow row2)
		{
			if (row1 != null && row2 != null)
			{
				for(int i=0; i<row1.Cells.Count; i++)
					if (!ValuesAreEquivalent(row1.Cells[i].Value, row2.Cells[i].Value))
						return false;

				return true;
			}
			else return false;
		}
		
		private void SwitchRows(int index1, int index2)
		{
			if (index1 >= 0 && index1 < grid.Rows.Count && index2 >= 0 && index2 < grid.Rows.Count && index1 != index2)
			{	
				for(int i=0; i<grid.Rows[index1].Cells.Count; i++)
				{
					object value1 = grid.Rows[index1].Cells[i].Value;
					object value2 = grid.Rows[index2].Cells[i].Value;
					
					grid.Rows[index1].Cells[i].Value = value2;
					grid.Rows[index2].Cells[i].Value = value1;
				}
				
				object tag1 = grid.Rows[index1].Tag;
				object tag2 = grid.Rows[index2].Tag;
				
				grid.Rows[index1].Tag = tag2;
				grid.Rows[index2].Tag = tag1;
				
				bindingList.ResetBindings();
			}
		}
		
		private void OnRowDragDrop(object sender, AdvancedDataGridRowDragDropEventArgs e)
		{		
			bindingList.RemoveAt(e.SourceIndex);
			bindingList.Insert(e.TargetIndex, e.MediaItem);
			grid.Rows[e.TargetIndex].Selected = true;
		}
		
		private void OnColumnDragDrop(object sender, AdvancedDataGridViewColumnDragDropEventArgs e)
		{
		
		}
		#endregion

		#region Private Event Methods
		void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.Value != null)
			{
				//queueTable.Columns[e.ColumnIndex].ColumnName == COL_DURATION)
				if (grid.Columns[e.ColumnIndex].Name == COL_DURATION)
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
				//if (queueTable.Columns[e.ColumnIndex].ColumnName == COL_TYPE)
				if (grid.Columns[e.ColumnIndex].Name == COL_TYPE)
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
			else e.Value = string.Empty;
		}
		#endregion

		#region Public Properties
		public AdvancedDataGridView Grid
		{
			get { return grid; }
			set {
				grid = value;
				if (grid != null)
				{
					grid.AutoGenerateColumns = false;
					grid.DataSource = bindingSource;
					grid.CellFormatting += new DataGridViewCellFormattingEventHandler(grid_CellFormatting);
					grid.RowDragDrop += new EventHandler<AdvancedDataGridRowDragDropEventArgs>(OnRowDragDrop);
					grid.ColumnDragDrop += new EventHandler<AdvancedDataGridViewColumnDragDropEventArgs>(OnColumnDragDrop);
				}
			}
		}
		
		public ListView SortListView
		{
			get { return sortListView; }
			set { sortListView = value; }
		}
		
		public PlaybackController PlaybackController
		{
			get { return playbackController; }
			set { playbackController = value; }
		}
		
		public IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}

		public IAudioTrack SelectedTrack
		{
			get { return selectedTrack; }
			set
			{
				selectedTrack = value;
			
				if (SelectedTrackChanged != null)
					SelectedTrackChanged(this, new QueueEventArgs());
			}
		}
				
		public ImageList SmallImageList
		{
			get { return smallImageList; }
			set { smallImageList = value; }
		}
		
		public EventHandler<QueueEventArgs> SelectedTrackChanged
		{
			get { return selectedTrackChanged; }
			set { selectedTrackChanged = value; }
		}
		#endregion

		#region Public Methods
		public void LoadTracks()
		{
			IList<IAudioTrack> tracks = GetMp3TunesTracks(false);
			LoadTracks(tracks, "MP3tunes");
		}

		public void LoadTracks(IList<IAudioTrack> tracks, string source)
		{
			if (tracks != null)
			{
				foreach (IAudioTrack track in tracks)
				{
					LoadTrack(track, source);
				}
			}
		}
		
		public void LoadSelectedRow()
		{
			IAudioStream audioStream = null;
			
			//if (grid.Rows.Count > 0 && grid.SelectedRows.Count > 0)
			if (grid.Rows.Count > 0)
			{
				if (SelectedRow == null)
				{
					if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
						SelectedRow = grid.SelectedRows[0];
					else SelectedRow = grid.Rows[0];
				}
				
				//# Name Artist Album Length Date Location Format
									
				if (SelectedRow.Cells.Count > 0)
				{
					//TODO: move all of this logic into AudioPlayer
					SelectedTrack = GetSelectedAudioTrack(SelectedRow);
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
							SelectedRow.Cells[COL_DURATION].Value = audioStream.Duration;
						}
					}
					
					playbackController.LoadAudioStream(audioStream);
					SelectedRow.Cells[COL_STATUS].Value = "Loaded";
				}
				else throw new ApplicationException("Could not load selected track: Id was undefined");
			}
		}
		
		private string GetSafeString(string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			else return value;
		}
		
		public void LoadTrack(IAudioTrack track, string source)
		{
			/*
			object[] data = new object[11];
			data[0] = track.Id;
			data[1] = TYPE_AUDIO;
			data[2] = source;
			data[3] = track.TrackNumber;
			data[4] = GetSafeString(track.Name);
			data[5] = GetSafeString(track.Artist);
			data[6] = GetSafeString(track.Album);
			data[7] = track.Duration;
			data[8] = track.ReleaseDate;
			data[9] = track.Format.ToLowerInvariant();
			data[10] = track.Path;
			*/

			MediaItem item = new MediaItem(track.Id, source, TYPE_AUDIO, track.TrackNumber, GetSafeString(track.Name), GetSafeString(track.Artist), GetSafeString(track.Album), track.Duration, track.ReleaseDate, track.Format, track.Path);
			if (item.Id == default(Guid)) item.Id = Guid.NewGuid();
			bindingList.Add(item);
			//queueTable.Rows.Add(data);
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

		public void OpenDirectory(string path)
		{
			if (Directory.Exists(path))
			{
				DirectoryInfo dir = new DirectoryInfo(path);
				foreach(FileInfo file in dir.GetFiles())
				{
					OpenFile(file.FullName);
				}
				foreach(DirectoryInfo subDirectory in dir.GetDirectories())
				{
					OpenDirectory(subDirectory.FullName);
				}
			}
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
						LoadTrackFromPath(item.Path, "Playlist");
						
					//TestSort();
				}
				else if (IsFormat(path, "ogg,flac,mp3,wma,aac"))
				{
					LoadTrackFromPath(path, "File");
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
					LoadTracks(tracks, "CD");
				}
			}
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

				if (SelectedRow != null && grid.Rows.Count > 1)
				{
					SelectedRow = (SelectedRow.Index > 0) ? grid.Rows[SelectedRow.Index-1] : grid.Rows[grid.Rows.Count-1];
				}
				
				LoadSelectedRow();
				
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

				if (SelectedRow != null && grid.Rows.Count > 1)
				{
					SelectedRow = (SelectedRow.Index < grid.Rows.Count-1) ? grid.Rows[SelectedRow.Index+1] : grid.Rows[0];
				}

				LoadSelectedRow();

				if (isPlaying)
					playbackController.AudioPlayer.Play();
			}
		}
		
		public void Clear()
		{
			bindingList.Clear();
			bindingList.ResetBindings();
		}
		
		public void ClearSelectedRows()
		{
			//if (grid.SelectedRows.Count > 0)
			//{
				//foreach(DataGridViewRow row in grid.SelectedRows)
				//{
					//bindingList.RemoveAt(row.Index);
				//}
				//bindingList.ResetBindings();
			//}
		
			if (grid.Rows != null && grid.Rows.Count > 0)
			{
				IList<Guid> idList = new List<Guid>();
				foreach(DataGridViewRow row in grid.SelectedRows)
				{
					//bool selected = Convert.ToBoolean(row.Cells[COL_SELECTED].Value);
					//if (selected)
					//{
						idList.Add((Guid)row.Cells[COL_ID].Value);
					//}
				}
				
				if (idList.Count > 0)
				{
					int count = bindingList.Count;
					for(int i=0;idList.Count>0;i++)
					{
						if (bindingList.Count > 0)
						{
							if (i > bindingList.Count - 1) i = 0;
							
							foreach(Guid id in idList)
							{
								if (id == bindingList[i].Id)
								{
									bindingList.RemoveAt(i);
									i = 0;
									idList.Remove(id);
									break;
								}
							}
						}
						else break;
					}
					bindingList.ResetBindings();
				}
			}
		}
		
		public void ClearRow(int index)
		{
			if (index >= 0 && index < bindingList.Count)
			{
				bindingList.RemoveAt(index);
				bindingList.ResetBindings();
			}
		}
		
		//public void DeleteSelectedRow()
		//{
		//    if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
		//    {
		//        int currentIndex = grid.SelectedRows[0].Index;
				
		//        //IMediaItem selectedItem = grid.SelectedRows[0].DataBoundItem as IMediaItem;
				
		//        if (RowsAreEquivalent(grid.SelectedRows[0], selectedRow))
		//        {
		//            if (grid.Rows.Count > 1)
		//            {
		//                Next();
		//            }
		//            else
		//            {
		//                playbackController.AudioPlayer.Stop();
		//            }
		//        }

		//        bindingList.RemoveAt(currentIndex);
		//        bindingList.ResetBindings();
		//    }
		//}
		
		public void Sort(IList<string> columns)
		{
			if (columns != null && columns.Count > 0)
			{
				ListSortDescription[] sortArray = new ListSortDescription[columns.Count];
				
				for(int i=0; i<columns.Count; i++)
				{
					PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(IMediaItem))[columns[i]];
					sortArray[i] = new ListSortDescription(property, ListSortDirection.Ascending);
				}
				
				ListSortDescriptionCollection sorts = new ListSortDescriptionCollection(sortArray);
				((IBindingListView)bindingList).ApplySort(sorts);
			}
		}
		
		public void RemoveSort()
		{
			((IBindingListView)bindingList).RemoveSort();
		}
		
		//public void MoveSelectedRowUp()
		//{
		//    if (grid.SelectedRows != null && grid.SelectedRows.Count > 0 && grid.SelectedRows[0].Index > 0)
		//    {
		//        int selectedIndex = grid.SelectedRows[0].Index;
		//        SwitchRows(selectedIndex, selectedIndex-1);
		//        grid.SelectedRows[0].Selected = false;
		//        grid.Rows[selectedIndex-1].Selected = true;
		//    }
		//}
		
		//public void MoveSelectedRowDown()
		//{
		//    if (grid.SelectedRows != null && grid.SelectedRows.Count > 0 && grid.SelectedRows[0].Index < grid.Rows.Count-1)
		//    {
		//        int selectedIndex = grid.SelectedRows[0].Index;
		//        SwitchRows(selectedIndex, selectedIndex+1);
		//        grid.SelectedRows[0].Selected = false;
		//        grid.Rows[selectedIndex+1].Selected = true;
		//    }
		//}
		#endregion
	}
}
