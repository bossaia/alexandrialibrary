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
using Alexandria.Media;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.Catalog
{	
	[Class("Catalog", LoadType.Constructor, "Id")]
	public class BaseCatalog : ICatalog, IPersistent
	{
		#region Constructors
		[Constructor("Catalog", "A7612A3C-1A83-4b66-80AD-AB001CA67EA3")]
		public BaseCatalog(Guid id, IUser user)
		{
			this.id = id;
			this.user = user;
		}
		#endregion
	
		#region Private Fields
		private Guid id;
		private IUser user;
		private List<ICatalogAlbum> albums = new List<ICatalogAlbum>();
		private List<ICatalogArtist> artists = new List<ICatalogArtist>();
		private List<ICatalogAudioTrack> tracks = new List<ICatalogAudioTrack>();
		#endregion

		#region ICatalog Members
		public IUser User
		{
			get { return user; }
		}

		public IList<ICatalogAlbum> Albums
		{
			get { return albums; }
		}

		public IList<ICatalogArtist> Artists
		{
			get { return artists; }
		}

		public IList<ICatalogAudioTrack> Tracks
		{
			get { return tracks; }
		}
		#endregion

		#region IPersistent Members

		public Guid Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IDataStore DataStore
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public void Save()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Delete()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
