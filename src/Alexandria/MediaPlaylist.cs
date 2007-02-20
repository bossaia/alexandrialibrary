using System;
using System.Collections.Generic;
using System.Text;
using AlexandriaOrg.Alexandria;

namespace AlexandriaOrg.Alexandria
{
	public class MediaPlaylist : IMediaPlaylist
	{
		#region Private Fields
		private string path;
		private string name;
		private string version;
		private List<MediaFile> files;
		#endregion				
		
		#region Constructors
		protected MediaPlaylist(string path)
		{
			this.path = path;
			files = new List<MediaFile>();
			
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
		
		public IList<MediaFile> Files
		{
			get {return files;}
		}
		#endregion
		
		#region Public Methods
		public virtual void Load()
		{
		}
		#endregion
	}
}
