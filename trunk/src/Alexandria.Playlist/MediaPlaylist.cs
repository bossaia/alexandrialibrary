using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Playlist
{
	public class MediaPlaylist : IMediaPlaylist
	{
		#region Private Fields
		private string path;
		private string name;
		private string version;
		private List<IMedia> resources;
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
		
		public IList<IMedia> Resources
		{
			get {return resources;}
		}
		#endregion
		
		#region Public Methods
		public virtual void Load()
		{
		}
		#endregion

		#region IMediaPlaylist Members

		public IList<IMediaContainer> Items
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IResource Members
		public IIdentifier Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
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
	}
}
