using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Media;

namespace Alexandria.Playlist
{
	public class MediaPlaylist : IPlaylist
	{
		#region Private Fields
		private string path;
		private string name;
		private string version;
		//private List<IMedia> resources;
		
		private Guid id = Guid.NewGuid();
		#endregion				
		
		#region Constructors
		protected MediaPlaylist(string path)
		{
			this.path = path;
			//files = new List<MediaFile>();
			
			//Load();
		}
		
		protected MediaPlaylist(string path, string name, string version) : this(path)
		{
			this.name = name;
			this.version = version;
		}
		#endregion
		
		#region Public Properties
		public string Path
		{
			get { return path; }
			protected set { path = value; }
		}
		
		public string Name
		{
			get {return name;}
			protected set {name = value;}
		}
		
		public string Version
		{
			get {return version;}
			protected set {version = value;}
		}
		#endregion
		
		#region Public Methods
		public virtual void Load()
		{
		}
		#endregion

		#region IMedia Members
		public Guid Id
		{
			get { return id; }
		}

		public ILocation Location
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IMediaFormat Format
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IMediaPlaylist Members
		public IList<IPlaylistItem> Items
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		#endregion
	}
}
