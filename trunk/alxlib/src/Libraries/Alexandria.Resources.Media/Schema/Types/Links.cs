using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media.Schema.Types
{
	public static class Links
	{
		public static readonly Uri CatalogPlaylist = new Uri(SchemaConstants.SchemaLinksCatalog_Playlist);
		public static readonly Uri PlaylistTrack = new Uri(SchemaConstants.SchemaLinksPlaylist_Track);

		public static readonly LinkType CatalogPlaylistType = new LinkType(CatalogPlaylist);
		public static readonly LinkType PlaylistTrackType = new LinkType(PlaylistTrack);
	}
}
