using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Alexandria.AlbumArtDownloader
{
	public class Task
	{		
		#region Constructors
		public Task(ArtDownloader a, string artist, string album, string filesave, bool showExisting, bool showFolder, bool bDebug)
		{
			e_abort = new ManualResetEvent(false);
			_Artist = artist;
			_Album = album;
			_showExisting = showExisting;
			_showFolder = showFolder;
			status = State.Waiting;
			artdownloader = a;
			savestr = filesave;
			isdebug = bDebug;
			_Done = false;
			scripts = new List<Script>();
			results = new List<AlbumArtDownloader.ArtDownloader.ThumbRes>();

		}

		/*
		public Task(ArtDownloader a, string artist, string album, string filesave, bool showExisting, bool showFolder, ListViewItem item)
		{
			e_abort = new ManualResetEvent(false);
			_Artist = artist;
			artdownloader = a;
			_Album = album;
			_showExisting = showExisting;
			_showFolder = showFolder;
			listviewitem = item;
			status = TaskState.Waiting;
			savestr = filesave;
			_Done = false;
			scripts = new List<Script>();
			results = new List<AlbumArtDownloader.ArtDownloader.ThumbRes>();
		}
		*/
		#endregion
		
		#region Private Fields
		private bool isdebug;
		private ArtDownloader artdownloader;
		private string savestr;
		private TaskState status;
		private string _Artist;
		private string _Album;
		private bool _showExisting;
		private bool _showFolder;
		private List<Script> scripts;
		private int _ScriptsToGo;
		private bool _Done;
		private bool _Running;
		//public ListViewItem listviewitem;
		public List<ThumbRes> results;
		ManualResetEvent e_abort;
		#endregion
		
		#region Public Properties
		public bool IsDebug
		{
			get { return isdebug; }
		}

		public int ScriptsToGo
		{
			get
			{
				lock (this)
					return _ScriptsToGo;
			}
			set
			{
				lock (this)
				{
					_ScriptsToGo = value;
					if (_ScriptsToGo == 0)
					{
						//artdownloader.mainForm.CoolBeginInvoke(artdownloader.mainForm.taskdone, this);
					}
				}
			}
		}

		public string Status
		{
			get
			{
				lock (this)
				{
					switch (status)
					{
						case TaskState.Waiting:
							return "Waiting";
						case TaskState.Searching:
							return "Searching";
						case TaskState.Done:
							return "Done";
						default:
							return "Unknown";
					}
				}
			}
		}
		
		public string Artist
		{
			get
			{
				return _Artist;
			}
		}
		
		public string FileSave
		{
			get
			{
				lock (this)
				{
					return savestr;
				}
			}
		}
		
		public string Album
		{
			get
			{
				return _Album;
			}
		}
		
		public bool ShowExisting
		{
			get
			{
				return _showExisting;
			}
		}
		
		public bool ShowFolder
		{
			get
			{
				return _showFolder;
			}
		}
		
		public bool Done
		{
			get
			{
				lock (this)
				{
					return _Done;
				}
			}
		}
		
		public bool Running
		{
			get
			{
				lock (this)
				{
					return _Running;
				}
			}
			set
			{
				lock (this)
				{
					_Running = value;
				}
			}
		}
		
		public IList<Script> Scripts
		{
			get { return scripts; }
		}
		#endregion
		
		#region Public Methods
		public void Abort()
		{
			e_abort.Set();
		}

		public bool ShouldAbort()
		{
			return e_abort.WaitOne(0, false);
		}
		
		public void AddResult(ThumbRes result)
		{
			if (result.Name.ToLowerInvariant() == (Album.ToLowerInvariant())) result.Exact = true;
			lock (results)
			{
				results.Add(result);

			}
			lock (artdownloader)
			{
				if (artdownloader.SelectedTask == this)
					artdownloader.SendThumb(result);
			}
		}
		#endregion				
	}
}
