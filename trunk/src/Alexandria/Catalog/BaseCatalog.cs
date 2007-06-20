using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Media;
using Alexandria.Metadata;

namespace Alexandria.Catalog
{
	public class BaseCatalog : ICatalog
	{
		#region Constructors
		public BaseCatalog(IUser user)
		{
			this.user = user;
		}
		#endregion
	
		#region Private Fields
		private IUser user;
		private List<IAlbum> albums = new List<IAlbum>();
		private List<IPlaylist> playlists = new List<IPlaylist>();
		#endregion

		#region ICatalog Members
		public IUser User
		{
			get { return user; }
		}

		public IList<Alexandria.Metadata.IAlbum> Albumds
		{
			get { return albums; }
		}

		public IList<Alexandria.Media.IPlaylist> Playlists
		{
			get { return playlists; }
		}
		#endregion
	}
}
