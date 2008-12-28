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
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Alexandria;
using Alexandria.IO;

using Alexandria.AsciiGenerator;
using Alexandria.CompactDiscTools;
using Alexandria.Fmod;
using Alexandria.LastFM;
using Alexandria.MusicDns;
using Alexandria.TagLib;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Mp3Tunes;
using Telesophy.Alexandria.MusicBrainz;

using Telesophy.Alexandria.Clients.Ankh.Views;
using Telesophy.Alexandria.Clients.Ankh.Views.Data;
using Telesophy.Alexandria.Extensions.CompactDisc;
using Telesophy.Alexandria.Extensions.Playlist;

using Telesophy.Babel.Persistence;
#endregion

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	public class QueueController
	{
		#region Constructors
		public QueueController()
		{
		}
		#endregion

		#region Private Fields
		private IMediaItem selectedTrack;
		private IList<IMediaItem> tracks;

		MusicLocker locker = new MusicLocker();
		PlaylistFactory playlistFactory = new PlaylistFactory();
		TagLibEngine tagLibEngine = new TagLibEngine();
		
		private EventHandler<QueueEventArgs> selectedTrackChanged;

		private SimpleAlbumFactory albumFactory = new SimpleAlbumFactory();

		private MediaItemDataGridView grid;
		private ListView sortListView;
		private ImageList smallImageList;
		
		private DataGridViewRow selectedRow;
		
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		
		private PlaybackController playbackController;
		private PersistenceController persistenceController;
		private TaskController taskController;
		
		private Dictionary<Uri, AspiDeviceInfo> deviceMaps = new Dictionary<Uri,AspiDeviceInfo>();

        private string playbackMode;

        private PictureBox nowPlayingImage;
		#endregion

		#region Private Properties
		private DataGridViewRow SelectedRow
		{
			get { return selectedRow;  }
			set {
				if (selectedRow != null && selectedRow != value && selectedRow.Index > -1)
				{
					grid.Rows[selectedRow.Index].Cells[Columns.Queue.Status.Name].Value = string.Empty;
				}
				 
				selectedRow = value;
			}
		}
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
		
		private IMediaItem GetSelectedAudioTrack(DataGridViewRow row)
		{
			if (row.Index > -1)
			{
				Guid id = GetItemGuid(row.Cells[Columns.Queue.Id.Name]);
                string type = GetItemString(row.Cells[Columns.Queue.Type.Name]);
                string source = GetItemString(row.Cells[Columns.Queue.Source.Name]);
                int number = GetItemInt(row.Cells[Columns.Queue.Number.Name]);
                string title = GetItemString(row.Cells[Columns.Queue.Title.Name]);
                string artist = GetItemString(row.Cells[Columns.Queue.Artist.Name]);
                string album = GetItemString(row.Cells[Columns.Queue.Album.Name]);
                TimeSpan duration = GetItemTimeSpan(row.Cells[Columns.Queue.Duration.Name]);
                DateTime date = GetItemDateTime(row.Cells[Columns.Queue.Date.Name]);
                string format = GetItemString(row.Cells[Columns.Queue.Format.Name]);
                Uri path = GetItemUri(row.Cells[Columns.Queue.Path.Name]);
				
				IMediaItem track = new AudioTrack(id, source, number, title, artist, album, duration, date, format, path);
				return track;
			}
			else throw new ApplicationException("The row currently selected in the queue is invalid");
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
					IMediaItem track = tagLibEngine.GetAudioTrack(path);
					if (track != null)
						LoadTrack(track, source);
				}
			}
		}

        private void DrawSortGlyphs(IDictionary<string, bool> columns)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (Columns.Queue.ColumnsByName.ContainsKey(column.Name))
                {
                    string propertyName = Columns.Queue.ColumnsByName[column.Name].PropertyName;

                    SortOrder direction = SortOrder.None;
                    if (columns.ContainsKey(propertyName))
                        direction = (columns[propertyName]) ? SortOrder.Ascending : SortOrder.Descending;

                    column.HeaderCell.SortGlyphDirection = direction;
                }
            }
        }

        private void LoadNowPlayingImage(Uri trackPath)
        {
            const string IMAGE_NAME = "Folder.jpg";

            if (trackPath != null)
            {
                FileInfo file = new FileInfo(trackPath.LocalPath);
                if (file.Directory.Exists)
                {
                    string path = Path.Combine(file.Directory.FullName, IMAGE_NAME);
                    if (System.IO.File.Exists(path))
                    {
                        NowPlayingImage.Image = System.Drawing.Image.FromFile(path);
                    }
                }
            }
        }
		#endregion

		#region Private Event Methods
		//void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		//{
		//    if (e.Value != null)
		//    {
		//        //queueTable.Columns[e.ColumnIndex].ColumnName == COL_DURATION)
		//        if (grid.Columns[e.ColumnIndex].Name == ControllerConstants.COL_DURATION)
		//        {
		//            TimeSpan duration = (TimeSpan)e.Value;
		//            if (duration.Hours > 0)
		//            {
		//                e.Value = string.Format("{0:00}:{1:00}:{2:00}", duration.Hours, duration.Minutes, duration.Seconds);
		//            }
		//            else if (duration.Minutes > 0)
		//            {
		//                e.Value = string.Format("{0:00}:{1:00}", duration.Minutes, duration.Seconds);
		//            }
		//            else
		//            {
		//                e.Value = string.Format("0:{0:00}", duration.Seconds);
		//            }
		//        }
		//        //if (queueTable.Columns[e.ColumnIndex].ColumnName == COL_TYPE)
		//        if (grid.Columns[e.ColumnIndex].Name == ControllerConstants.COL_TYPE)
		//        {
		//            switch(e.Value.ToString())
		//            {
		//                case ControllerConstants.TYPE_AUDIO:
		//                    e.Value = smallImageList.Images[ControllerConstants.INDEX_AUDIO];
		//                    break;
		//                case ControllerConstants.TYPE_BOOK:
		//                    e.Value = smallImageList.Images[ControllerConstants.INDEX_BOOK];
		//                    break;
		//                case ControllerConstants.TYPE_IMAGE:
		//                    e.Value = smallImageList.Images[ControllerConstants.INDEX_IMAGE];
		//                    break;
		//                case ControllerConstants.TYPE_MOVIE:
		//                    e.Value = smallImageList.Images[ControllerConstants.INDEX_MOVIE];
		//                    break;
		//                case ControllerConstants.TYPE_TELEVISION:
		//                    e.Value = smallImageList.Images[ControllerConstants.INDEX_TELEVISION];
		//                    break;
		//                default:
		//                    e.Value = smallImageList.Images[ControllerConstants.INDEX_AUDIO];
		//                    break;
		//            }
		//        }
		//    }
		//    else e.Value = string.Empty;
		//}
		#endregion

		#region Public Properties
		public MediaItemDataGridView Grid
		{
			get { return grid; }
			set { grid = value; }
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
		
		[CLSCompliant(false)]
		public PersistenceController PersistenceController
		{
			get { return persistenceController; }
			set { persistenceController = value; }
		}
		
		public TaskController TaskController
		{
			get { return taskController; }
			set { taskController = value; }
		}
		
		public IList<IMediaItem> Tracks
		{
			get { return tracks; }
		}

		[CLSCompliant(false)]
		public IMediaItem SelectedTrack
		{
			get { return selectedTrack; }
			set
			{
				selectedTrack = value;
			
				if (selectedTrackChanged != null)
					selectedTrackChanged(this, new QueueEventArgs());
			}
		}
				
		public ImageList SmallImageList
		{
			get { return smallImageList; }
			set { smallImageList = value; }
		}
		
		public string SelectedRowStatus
		{
			get { 
				return (SelectedRow != null && SelectedRow.Index > -1)
                    ? SelectedRow.Cells[Columns.Queue.Status.Name].Value.ToString()
					: null;
			}
			set { 
				if (SelectedRow != null && SelectedRow.Index > -1)
                    SelectedRow.Cells[Columns.Queue.Status.Name].Value = value;
			}
		}

        public string PlaybackMode
        {
            get { return playbackMode; }
            set { playbackMode = value; }
        }

        public PictureBox NowPlayingImage
        {
            get { return nowPlayingImage; }
            set { nowPlayingImage = value; }
        }
		#endregion

		#region Public Methods
		public void LoadTracks()
		{
			IList<IMediaItem> tracks = GetMp3TunesTracks(false);
			LoadTracks(tracks, "MP3tunes");
		}

		public void LoadTracks(IList<MediaItemData> data)
		{
			if (data != null)
			{
				foreach (MediaItemData item in data)
				{
					LoadTrack(item);
				}
			}
		}

		public void LoadTracks(IList<IMediaItem> tracks, string source)
		{
			if (tracks != null)
			{
				foreach (IMediaItem track in tracks)
				{
					LoadTrack(track, source);
				}
			}
		}
		
		public void LoadSelectedRow()
		{
			IAudioStream audioStream = null;
			
			if (grid.Rows.Count > 0)
			{
				if (SelectedRow == null)
				{
					if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
						SelectedRow = grid.SelectedRows[0];
					else SelectedRow = grid.Rows[0];
				}
				
				IMediaItem track = GetSelectedAudioTrack(SelectedRow);
				if (SelectedTrack == null || SelectedTrack.Id != track.Id)
				{
					SelectedTrack = track;
					
					if (SelectedTrack.Format == "cdda")
					{
						string discPath = SelectedTrack.Path.LocalPath.Substring(0, 2);
						audioStream = new CompactDiscSound(discPath);
						audioStream.StreamIndex = SelectedTrack.Number-1;
					}
					else
					{
						if (SelectedTrack.Path.IsFile)
						{
							audioStream = new LocalSound(SelectedTrack.Path.LocalPath);
							audioStream.StreamIndex = 0;

                            LoadNowPlayingImage(track.Path);
						}
						else
						{
							string fileName = string.Format("{0}{1:00,2} {2} - {3} - {4}.{5}", tempPath, SelectedTrack.Number, SelectedTrack.Title, SelectedTrack.Artist, SelectedTrack.Album, SelectedTrack.Format);
							fileName = CleanupFileName(fileName);
							if (!System.IO.File.Exists(fileName))
							{
								if (!System.IO.Directory.Exists(tempPath))
									System.IO.Directory.CreateDirectory(tempPath);

								WebClient client = new WebClient();
								Uri address = locker.GetLockerPath(SelectedTrack.Path.ToString());
								try
								{
									client.DownloadFile(address, fileName);
								}
								catch (WebException ex)
								{
									throw new ApplicationException("There was an error downloading track : " + selectedTrack.Title, ex);
								}
							}

							audioStream = new LocalSound(fileName);
							audioStream.StreamIndex = 0;
						}

						if (audioStream != null && audioStream.Duration != SelectedTrack.Duration && audioStream.Duration != TimeSpan.Zero)
						{
							SelectedRow.Cells[Columns.Queue.Duration.Name].Value = audioStream.Duration;
						}
					}
					
					playbackController.LoadAudioStream(audioStream);
					SelectedRowStatus = "Loaded";
				}
			}
			else
			{
				SelectedRow = null;
				SelectedTrack = null;
			}
		}
		
		private string GetSafeString(string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;
			else return value;
		}
		
		public void SelectRow(int rowIndex)
		{
			if (rowIndex >= 0 && rowIndex <= grid.Rows.Count-1)
			{
				playbackController.Stop();
				SelectedRow = grid.Rows[rowIndex];
				LoadSelectedRow();
				playbackController.Play();
			}
		}

		[CLSCompliant(false)]
		public void LoadTrack(IMediaItem track, string source)
		{
			//TODO: fix this so that I don't have to wrap the input track
			//AudioTrack item = new AudioTrack(track.Id, source, track.Number, GetSafeString(track.Title), GetSafeString(track.Artist), GetSafeString(track.Album), track.Duration, track.Date, track.Format.ToLower(), track.Path);
			//if (item.Id == default(Guid)) item.Id = Guid.NewGuid();
			//track.Source = source;

			MediaItemData item = new MediaItemData(track.Id, track.Type, source, track.Number, GetSafeString(track.Title), GetSafeString(track.Artist), GetSafeString(track.Album), track.Duration, track.Date, track.Format.ToLower(), track.Path);

			grid.AddItem(item);
		}
		
		public void LoadTrack(MediaItemData item)
		{
			grid.AddItem(item);
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

		[CLSCompliant(false)]
		public IMediaItem GetMediaItem(Uri path)
		{
			return tagLibEngine.GetAudioTrack(path);
		}

		public IList<IMediaItem> GetMp3TunesTracks(bool ignoreCache)
		{
			try
			{
				MusicLocker musicLocker = new MusicLocker();
				musicLocker.Login("dan.poage@gmail.com", "automatic");
				tracks = null; //musicLocker.GetTracks(ignoreCache);
				return tracks;
			}
			catch (Exception ex)
			{
				throw new ApplicationException("There was an error loading tracks from your MP3tunes locker", ex);
			}
		}

		[CLSCompliant(false)]
		public IMetadataIdentifier LookupPuid(Uri path)
		{
			MetadataFactory factory = new MetadataFactory();
			IMediaItem track = factory.CreateAudioTrack(path);
			
			//TODO: Implement a replacement for track.MetadataIdentifiers
			//foreach (IMetadataIdentifier metadataId in track.MetadataIdentifiers)
			//{
				//if (metadataId.Type.Contains("MusicDnsId"))
					//return metadataId;
			//}
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
				if (IsFormat(path, ContextHelper.GetPlaylistFormats()))
				{
					//TODO: Change this to use the new Alexandria.Extensions.Playlist project
					//IPlaylist playlist = playlistFactory.CreatePlaylist(new Uri(path));
					//playlist.Load();
					//foreach (IPlaylistItem item in playlist.Items)
						//LoadTrackFromPath(item.Path, "Playlist");
				}
				else if (IsFormat(path, ContextHelper.GetAudioFormats()))
				{
					LoadTrackFromPath(path, Values.Source.File);
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
					TrackSource trackSource = sourceData as TrackSource;
					if (trackSource != null)
					{
						IList<IMediaItem> tracks = null;
						string source = string.Empty;
						
						if (trackSource.Id != default(Guid))
						{
							IMediaSet set = persistenceController.LookupMediaSet(trackSource.Id);
							if (set != null)
							{
								tracks = set.Items;
								source = set.Source;
							}
						}
						else
						{
							tracks = trackSource.GetAudioTracks();
							source = string.Format("CD [{0} Drive]", trackSource.Path.ToString().Substring(8, 1));
						}
					
						if (tracks != null && tracks.Count > 0)
						{
							LoadTracks(tracks, source);
						}
					
						if (trackSource.DeviceInfo != null)
						{
							deviceMaps[trackSource.Path] = trackSource.DeviceInfo;
						}
					}
				}
			}
		}

        private DataGridViewRow GetRandomRow()
        {
            if (Grid != null && Grid.Rows.Count > 0)
            {
                int excludeIndex = (SelectedRow != null) ? SelectedRow.Index : -1;
                int index = 0;

                Random generator = new Random();

                do
                {
                    index = generator.Next(Grid.Rows.Count);
                }
                while (index == excludeIndex); //keep getting a random index until we get on that is not excluded

                if (index > -1 && index < Grid.Rows.Count)
                    return Grid.Rows[index];
            }

            return null;
        }

        private DataGridViewRow GetNewRow(bool forward)
        {
            switch (PlaybackMode)
            {
                case PlayModes.RepeatList:
                    if (forward)
                        return (SelectedRow.Index < grid.Rows.Count - 1) ? grid.Rows[SelectedRow.Index + 1] : grid.Rows[0];
                    else return (SelectedRow.Index > 0) ? grid.Rows[SelectedRow.Index - 1] : grid.Rows[grid.Rows.Count - 1];
                case PlayModes.RepeatTrack:
                    return SelectedRow;
                case PlayModes.Random:
                    return GetRandomRow();
                default:
                    return null;
            }
        }

		public void Previous()
		{
			if (playbackController != null)
			{
				bool isPlaying = playbackController.IsPlaying();
				
				if (isPlaying)
					playbackController.Stop();

				if (SelectedRow != null && grid.Rows.Count > 1)
				{
                    SelectedRow = GetNewRow(false);
                        //(SelectedRow.Index > 0) ? grid.Rows[SelectedRow.Index-1] : grid.Rows[grid.Rows.Count-1];
				}
				
				LoadSelectedRow();
				
				if (isPlaying)
					playbackController.Play();
			}
		}

		public void Next()
		{
			if (playbackController != null)
			{
				bool isPlaying = playbackController.IsPlaying();
				
				if (isPlaying)
					playbackController.Stop();

				if (SelectedRow != null && grid.Rows.Count > 1)
				{
                    SelectedRow = GetNewRow(true);
                        //(SelectedRow.Index < grid.Rows.Count-1) ? grid.Rows[SelectedRow.Index+1] : grid.Rows[0];
				}

				LoadSelectedRow();

				if (isPlaying)
					playbackController.Play();
			}
		}
		
		public void Clear()
		{
			grid.Clear();
		}
		
		public void ClearSelectedRows()
		{
			grid.ClearSelectedRows();
		}
		
		public void ClearRow(int index)
		{
			grid.ClearRow(index);
		}

		public void Sort(IDictionary<string, bool> columns)
		{
			if (columns != null && columns.Count > 0)
			{
				Guid selectedId = default(Guid);
				if (SelectedRow != null && SelectedRow.Index > -1)
					selectedId = (Guid)SelectedRow.Cells[Columns.Queue.Id.Name].Value;
			
				ListSortDescription[] sortArray = new ListSortDescription[columns.Count];
				
				int columnIndex = 0;
				foreach (KeyValuePair<string, bool> column in columns)
				{
					ListSortDirection direction = (column.Value) ? ListSortDirection.Ascending : ListSortDirection.Descending;
                    PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(IMediaItem))[Columns.Queue.ColumnsByPropertyName[column.Key].PropertyName];
					sortArray[columnIndex] = new ListSortDescription(property, direction);
					columnIndex++;					
				}
				
				ListSortDescriptionCollection sorts = new ListSortDescriptionCollection(sortArray);
				
				grid.Sort(sorts);

                DrawSortGlyphs(columns);
			}
		}

		public void RemoveSort()
		{
			grid.RemoveSort();
		}
		
		[CLSCompliant(false)]
		public void Filter(Query query)
		{
			grid.Clear();
			//bindingList.Clear();
			
			IList<MediaItem> items = persistenceController.ListMediaItems(query.ToString());
			
			foreach (IMediaItem item in (IEnumerable<IMediaItem>)items)
			{
				MediaItemData dataItem = new MediaItemData(item.Id, item.Type, item.Source, item.Number, item.Title, item.Artist, item.Album, item.Duration, item.Date, item.Format, item.Path);
				grid.AddItem(dataItem);
				//bindingList.Add(item);
			}
			
			//using (IEnumerator<IMediaItem> iter = items.GetEnumerator())
			//{
			//    iter.Reset();
			//    while (iter.MoveNext())			
			//        bindingList.Add(iter.Current);
			//}
		}
				
		public void SaveRow(int index)
		{
			if (persistenceController != null && index >= 0 && index < grid.Rows.Count)
			//bindingList.Count)
			{
				MediaItemData data = grid.GetItem(index);
				IMediaItem item = persistenceController.CreateMediaItem(data);
				
				//IMediaItem item = bindingList[index];
				item.Source = Values.Source.Catalog;
				persistenceController.SaveMediaItem(item);
			}
		}
		
		public void SaveAllRows()
		{
			if (persistenceController != null)
			{
				for(int i=0; i<grid.Rows.Count; i++)
					SaveRow(i);
			}
		}
		
		public void DeleteRow(int index)
		{
			if (persistenceController != null && index >= 0 && index < grid.Rows.Count)
			//bindingList.Count)
			{
				MediaItemData data = grid.GetItem(index);
				IMediaItem item = persistenceController.CreateMediaItem(data);
				
				//IMediaItem item = bindingList[index];
				persistenceController.DeleteMediaItem(item);
			}
		}
		
		public void DeleteSelectedRows()
		{
			if (persistenceController != null && grid.SelectedRows.Count > 0)
			{
				IList<IMediaItem> items = new List<IMediaItem>();
				foreach (DataGridViewRow row in grid.SelectedRows)
				{
					MediaItemData data = grid.GetItem(row.Index);
					IMediaItem item = persistenceController.CreateMediaItem(data);
					
					items.Add(item);
					
					//bindingList.RemoveAt(row.Index);
					grid.RemoveAt(row.Index);
				}
				
				persistenceController.DeleteMediaItems(items);
			}	
		}
		
		public void LoadDefaultCatalog()
		{
			//bindingList.Clear();
			grid.Clear();
			
			IList<MediaItem> items = persistenceController.ListAllMediaItems();
			
			foreach (MediaItem item in items)
			{
				MediaItemData data = new MediaItemData(item.Id, item.Type, item.Source, item.Number, item.Title, item.Artist, item.Album, item.Duration, item.Date, item.Format, item.Path);
				grid.AddItem(data);
			}
			
			//using (IEnumerator<IMediaItem> iter = items.GetEnumerator())
			//{
			//    iter.Reset();
			//    while (iter.MoveNext())
			//        bindingList.Add(iter.Current);
			//}
		}
		
		public void WireSelectedTrackChanged(EventHandler<QueueEventArgs> handler)
		{
			if (handler != null)
				selectedTrackChanged += handler;
		}
		
		public IList<IMediaItem> GetSelectedItems()
		{
			IList<IMediaItem> list = new List<IMediaItem>();
			
			if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
			{
				foreach(DataGridViewRow row in grid.SelectedRows)
				{
					MediaItemData data = grid.GetItem(row.Index);
					IMediaItem item = persistenceController.CreateMediaItem(data);
					list.Add(item);
				
					//list.Add(bindingList[row.Index]);
				}
			}
			
			return list;
		}
		#endregion
	}
}
