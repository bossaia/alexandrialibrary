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

using Alexandria.AsciiGenerator;
using Alexandria.CompactDiscTools;
using Alexandria.Fmod;
using Alexandria.LastFM;
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
		
		private const string SOURCE_CATALOG = "Catalog";

		private const string FORMAT_PLAYLIST = "asx,m3u,pls,xspf";
		private const string FORMAT_AUDIO = "aac,flac,m4a,mp3,ogg,wav,wma";
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
		private int selectedRowSaveIndex;
		//private Guid selectedGuid;
		
		private readonly string tempPath = string.Format("{0}Alexandria{1}", System.IO.Path.GetTempPath(), System.IO.Path.DirectorySeparatorChar);
		
		private PlaybackController playbackController;
		private PersistenceController persistenceController;
		
		private DateTime importStart;
		private string importPath;
		private int scanCount;
		private int importCount;
		private int errorCount;
		private ImportStatusUpdateDelegate importStatusUpdateCallback;
		#endregion

		#region Private Properties
		private DataGridViewRow SelectedRow
		{
			get { return selectedRow;  }
			set {
				if (selectedRow != null && selectedRow != value && selectedRow.Index > -1)
				{
					grid.Rows[selectedRow.Index].Cells[COL_STATUS].Value = string.Empty;
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
		
		private IAudioTrack GetSelectedAudioTrack(DataGridViewRow row)
		{
			if (row.Index > -1)
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
					IAudioTrack track = tagLibEngine.GetAudioTrack(path);
					if (track != null)
						LoadTrack(track, source);
				}
			}
		}
		
		private void OnRowDragDropping(object sender, AdvancedDataGridRowDragDropEventArgs e)
		{
		
		}
		
		private void OnRowDragDropped(object sender, AdvancedDataGridRowDragDropEventArgs e)
		{		
			bindingList.RemoveAt(e.SourceIndex);
			bindingList.Insert(e.TargetIndex, e.MediaItem);
			grid.Rows[e.TargetIndex].Selected = true;
		}
		
		private void OnColumnDragDropping(object sender, AdvancedDataGridViewColumnDragDropEventArgs e)
		{
			selectedRowSaveIndex = SelectedRow.Index;
		}
		
		private void OnColumnDragDropped(object sender, AdvancedDataGridViewColumnDragDropEventArgs e)
		{
			SelectedRow = grid.Rows[selectedRowSaveIndex];
		}
		
		////remove the source column
		//this.Columns.RemoveAt(DragDropSourceIndex);

		////insert a new column at the target index using the source column as a template
		//this.Columns.Insert(DragDropTargetIndex, new DataGridViewColumn(DragDropColumn.CellTemplate));

		////copy the source column's header cell to the new column
		//this.Columns[DragDropTargetIndex].HeaderCell = DragDropColumn.HeaderCell;

		////select the newly-inserted column
		//this.Columns[DragDropTargetIndex].Selected = true;

		////update the position of the cuurent cell in the DGV
		//this.CurrentCell = this[DragDropTargetIndex, 0];
		//for (int i = 0; i < this.RowCount; i++)
		//{
		//    //for each cell in the new column
		//    if (DragDropColumnCellValue[i] != null)
		//    {
		//        //set the cell's value equal to that of the corresponding cell in the source column
		//        this.Rows[i].Cells[DragDropTargetIndex].Value = DragDropColumnCellValue[i];
		//    }
		//}
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
					grid.RowDragDropping += new EventHandler<AdvancedDataGridRowDragDropEventArgs>(OnRowDragDropping);
					grid.RowDragDropped += new EventHandler<AdvancedDataGridRowDragDropEventArgs>(OnRowDragDropped);
					grid.ColumnDragDropping += new EventHandler<AdvancedDataGridViewColumnDragDropEventArgs>(OnColumnDragDropping);
					grid.ColumnDragDropped += new EventHandler<AdvancedDataGridViewColumnDragDropEventArgs>(OnColumnDragDropped);
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
		
		public PersistenceController PersistenceController
		{
			get { return persistenceController; }
			set { persistenceController = value; }
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
			
				if (selectedTrackChanged != null)
					selectedTrackChanged(this, new QueueEventArgs());
			}
		}
				
		public ImageList SmallImageList
		{
			get { return smallImageList; }
			set { smallImageList = value; }
		}
		
		//public EventHandler<QueueEventArgs> SelectedTrackChanged
		//{
			//get { return selectedTrackChanged; }
			//set { selectedTrackChanged = value; }
		//}
		
		public string SelectedRowStatus
		{
			get { return (SelectedRow != null) ? SelectedRow.Cells[COL_STATUS].Value.ToString() : null; }
			set { 
				if (SelectedRow != null)
					SelectedRow.Cells[COL_STATUS].Value = value;
			}
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
			
			if (grid.Rows.Count > 0)
			{
				if (SelectedRow == null)
				{
					if (grid.SelectedRows != null && grid.SelectedRows.Count > 0)
						SelectedRow = grid.SelectedRows[0];
					else SelectedRow = grid.Rows[0];
				}
				
				IAudioTrack track = GetSelectedAudioTrack(SelectedRow);
				if (SelectedTrack == null || SelectedTrack.Id != track.Id)
				{
					SelectedTrack = track;
					
					if (SelectedTrack.Format == "cdda")
					{
						string discPath = SelectedTrack.Path.LocalPath.Substring(0, 2);
						audioStream = new Fmod.CompactDiscSound(discPath);
						audioStream.StreamIndex = SelectedTrack.TrackNumber-1;
					}
					else
					{
						if (SelectedTrack.Path.IsFile)
						{
							audioStream = new Fmod.LocalSound(SelectedTrack.Path.LocalPath);
							audioStream.StreamIndex = 0;
						}
						else
						{
							string fileName = string.Format("{0}{1:00,2} {2} - {3} - {4}.{5}", tempPath, SelectedTrack.TrackNumber, SelectedTrack.Name, SelectedTrack.Artist, SelectedTrack.Album, SelectedTrack.Format);
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
									throw new ApplicationException("There was an error downloading track : " + selectedTrack.Name, ex);
								}
							}

							audioStream = new Fmod.LocalSound(fileName);
							audioStream.StreamIndex = 0;
						}

						if (audioStream != null && audioStream.Duration != SelectedTrack.Duration && audioStream.Duration != TimeSpan.Zero)
						{
							SelectedRow.Cells[COL_DURATION].Value = audioStream.Duration;
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
		
		public void LoadTrack(IAudioTrack track, string source)
		{
			MediaItem item = new MediaItem(track.Id, source, TYPE_AUDIO, track.TrackNumber, GetSafeString(track.Name), GetSafeString(track.Artist), GetSafeString(track.Album), track.Duration, track.ReleaseDate, track.Format, track.Path);
			if (item.Id == default(Guid)) item.Id = Guid.NewGuid();
			bindingList.Add(item);
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
		
		public void BeginImportDirectory(string path, ImportStatusUpdateDelegate importStatusUpdateCallback)
		{
			this.importPath = path;
			this.scanCount = 0;
			this.importCount = 0;
			this.errorCount = 0;
			this.importStatusUpdateCallback = importStatusUpdateCallback;
			this.importStart = DateTime.Now;
			
			MethodInvoker invoker = new MethodInvoker(ImportDirectory);
			AsyncCallback callback = new AsyncCallback(EndImportDirectory);
			invoker.BeginInvoke(callback, null);
		}
		
		public void EndImportDirectory(IAsyncResult result)
		{
			if (importStatusUpdateCallback != null)
			{
				TimeSpan completedTime = DateTime.Now.Subtract(importStart);
				ImportStatusUpdateEventArgs args = new ImportStatusUpdateEventArgs(scanCount, importCount, errorCount, completedTime);
				importStatusUpdateCallback(this, args);
			}
		}
		
		private void ImportDirectory()
		{
			ImportDirectory(importPath);
		}
		
		private void ImportDirectory(string path)
		{
			if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
			{
				DirectoryInfo dir = new DirectoryInfo(path);
				foreach(FileInfo file in dir.GetFiles())
				{
					ImportFile(file.FullName);
				}
				foreach(DirectoryInfo subDirectory in dir.GetDirectories())
				{
					ImportDirectory(subDirectory.FullName);
				}
			}
		}

		public void ImportFile(string path)
		{
			if (System.IO.File.Exists(path))
			{
				if (!string.IsNullOrEmpty(path) && IsFormat(path, FORMAT_AUDIO))
				{
					try
					{
						IAudioTrack track = tagLibEngine.GetAudioTrack(new Uri(path));
						if (track != null)
						{
							IMediaItem item = new MediaItem(Guid.NewGuid(), SOURCE_CATALOG, TYPE_AUDIO, track.TrackNumber, track.Name, track.Artist, track.Album, track.Duration, track.ReleaseDate, track.Format, track.Path);
							persistenceController.SaveMediaItem(item);
							
							if (importStatusUpdateCallback != null)
							{
								scanCount++;
								importCount++;
								ImportStatusUpdateEventArgs args = new ImportStatusUpdateEventArgs(scanCount, importCount, errorCount, path);
								importStatusUpdateCallback(this, args);
							}
						}
					}
					catch(Exception ex)
					{
						if (importStatusUpdateCallback != null)
						{
							errorCount++;
							ImportStatusUpdateEventArgs args = new ImportStatusUpdateEventArgs(scanCount, importCount, errorCount, path);
							importStatusUpdateCallback(this, args);
						}
						//else
						//{
							MessageBox.Show(string.Format("The following file could not be imported: \n{0}\n\n{1}", path, ex.Message), "IMPORT ERROR");
						//}
					}
				}
				else
				{
					if (importStatusUpdateCallback != null)
					{
						scanCount++;
						ImportStatusUpdateEventArgs args = new ImportStatusUpdateEventArgs(scanCount, importCount, errorCount, path);
						importStatusUpdateCallback(this, args);
					}
				}
			}
		}

		public void OpenFile(string path)
		{
			if (!string.IsNullOrEmpty(path))
			{
				if (IsFormat(path, FORMAT_PLAYLIST))
				{
					IPlaylist playlist = playlistFactory.CreatePlaylist(new Uri(path));
					playlist.Load();
					foreach (IPlaylistItem item in playlist.Items)
						LoadTrackFromPath(item.Path, "Playlist");
						
					//TestSort();
				}
				else if (IsFormat(path, FORMAT_AUDIO))
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
				bool isPlaying = playbackController.IsPlaying();
				
				if (isPlaying)
					playbackController.Stop();

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
					playbackController.Play();
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
		
		public void Sort(IDictionary<string, bool> columns)
		{
			if (columns != null && columns.Count > 0)
			{
				Guid selectedId = default(Guid);
				if (SelectedRow != null && SelectedRow.Index > -1)
					selectedId = (Guid)SelectedRow.Cells[COL_ID].Value;
			
				ListSortDescription[] sortArray = new ListSortDescription[columns.Count];
				
				int columnIndex = 0;
				foreach (KeyValuePair<string, bool> column in columns)
				{
					ListSortDirection direction = (column.Value) ? ListSortDirection.Ascending : ListSortDirection.Descending;
					PropertyDescriptor property = TypeDescriptor.GetProperties(typeof(IMediaItem))[column.Key];
					sortArray[columnIndex] = new ListSortDescription(property, direction);
					columnIndex++;					
				}
				
				ListSortDescriptionCollection sorts = new ListSortDescriptionCollection(sortArray);
				((IBindingListView)bindingList).ApplySort(sorts);
				
				if (selectedId != default(Guid))
				{
					for(int i=0; i<bindingList.Count;i++)
					{
						if (bindingList[i].Id == selectedId)
						{
							SelectedRow = grid.Rows[i];
							break;
						}
					}
				}
				
				foreach(DataGridViewColumn column in grid.Columns)
				{
					SortOrder direction = SortOrder.None;
					if (columns.ContainsKey(column.Name))
						direction = (columns[column.Name]) ? SortOrder.Ascending : SortOrder.Descending;
						
					column.HeaderCell.SortGlyphDirection = direction;
				}
			}
		}
		
		public void RemoveSort()
		{
			((IBindingListView)bindingList).RemoveSort();
			
			foreach(DataGridViewColumn column in grid.Columns)
				column.HeaderCell.SortGlyphDirection = SortOrder.None;
		}
		
		public void SaveRow(int index)
		{
			if (persistenceController != null && index >= 0 && index < bindingList.Count)
			{
				IMediaItem item = bindingList[index];
				item.Source = SOURCE_CATALOG;
				persistenceController.SaveMediaItem(item);
			}
		}
		
		public void SaveAllRows()
		{
			if (persistenceController != null)
			{
				for(int i=0; i<bindingList.Count; i++)
					SaveRow(i);
			}
		}
		
		public void DeleteRow(int index)
		{
			if (persistenceController != null && index >= 0 && index < bindingList.Count)
			{
				IMediaItem item = bindingList[index];
				persistenceController.DeleteMediaItem(item);
			}
		}		
		
		public void LoadDefaultCatalog()
		{
			IList<IMediaItem> items = persistenceController.ListAllMediaItems();
			foreach(IMediaItem item in items)
				bindingList.Add(item);
		}
		
		public void WireSelectedTrackChanged(EventHandler<QueueEventArgs> handler)
		{
			if (handler != null)
				selectedTrackChanged += handler;
		}
		#endregion
	}
}
