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

		public static readonly IEntityType AlbumType = new EntityType(Album);
		public static readonly IEntityType ArtistType = new EntityType(Artist);
		public static readonly IEntityType CatalogType = new EntityType(Catalog);
		public static readonly IEntityType PlaylistType = new EntityType(Playlist);
		public static readonly IEntityType StreamType = new EntityType(Stream);
		public static readonly IEntityType TrackType = new EntityType(Track);
	}
}
