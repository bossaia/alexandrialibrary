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
using System.Text;
using Alexandria;
using Alexandria.Media;

namespace Alexandria.Playlists
{
	public class BasePlaylist : IPlaylist
	{
		#region Private Fields
		private string path;
		private string name;
		private string version;
		//private List<IMedia> resources;
		
		private Guid id = Guid.NewGuid();
		#endregion				
		
		#region Constructors
		protected BasePlaylist(string path)
		{
			this.path = path;
			//files = new List<MediaFile>();
			
			//Load();
		}
		
		protected BasePlaylist(string path, string name, string version) : this(path)
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
