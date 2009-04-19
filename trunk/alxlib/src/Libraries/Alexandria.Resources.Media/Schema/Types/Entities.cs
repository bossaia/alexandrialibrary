using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media.Schema.Types
{
	public static class Entities
	{
		public static readonly Uri Album = new Uri(SchemaConstants.SchemaEntitiesAlbum);
		public static readonly Uri Artist = new Uri(SchemaConstants.SchemaEntitiesArtist);
		public static readonly Uri Catalog = new Uri(SchemaConstants.SchemaEntitiesCatalog);
		public static readonly Uri Playlist = new Uri(SchemaConstants.SchemaEntitiesPlaylist);
		public static readonly Uri Stream = new Uri(SchemaConstants.SchemaEntitiesStream);
		public static readonly Uri Track = new Uri(SchemaConstants.SchemaEntitiesTrack);
	}
}
