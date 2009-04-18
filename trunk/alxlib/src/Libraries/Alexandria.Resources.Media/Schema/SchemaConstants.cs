using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media.Schema
{
	public static class SchemaConstants
	{
		public const string Host = "http://www.alxlib.com/";
		
		public const string Media = Host + "media/";
		public const string MediaAlbums = Media + "albums/";
		public const string MediaArtists = Media + "artists/";
		public const string MediaCatalogs = Media + "catalogs/";
		public const string MediaDates = Media + "dates/";
		public const string MediaDurations = Media + "durations/";
		public const string MediaEvents = Media + "events/";
		public const string MediaFiles = Media + "files/";
		public const string MediaNames = Media + "names/";
		public const string MediaNumbers = Media + "numbers/";
		public const string MediaPlaylists = Media + "playlists/";
		public const string MediaTags = Media + "tags/";
		public const string MediaTracks = Media + "tracks/";
		public const string MediaUsers = Media + "users/";

		public const string Schema = Host + "schema/1/types/";
		public const string SchemaAggregates = Schema + "aggregates/";
		public const string SchemaAggregatesAlbumWithTracks = SchemaAggregates + "album_with_tracks";
		public const string SchemaAggregatesFileWithTrack = SchemaAggregates + "file_with_track";
		public const string SchemaEntities = Schema + "entities/";
		public const string SchemaEntitiesAlbum = SchemaEntities + "album";
		public const string SchemaEntitiesArtist = SchemaEntities + "artist";
		public const string SchemaEntitiesCatalog = SchemaEntities + "catalog";
		public const string SchemaEntitiesEvent = SchemaEntities + "event";
		public const string SchemaEntitiesPlaylist = SchemaEntities + "playlist";
		public const string SchemaEntitiesStream = SchemaEntities + "stream";
		public const string SchemaEntitiesTag = SchemaEntities + "tag";
		public const string SchemaEntitiesTrack = SchemaEntities + "track";
		public const string SchemaEntitiesUser = SchemaEntities + "user";
		public const string SchemaLinks = Schema + "links/";
		public const string SchemaLinksCatalog_Playlist = SchemaLinks + "catalog_playlist";
		public const string SchemaLinksPlaylist_Track = SchemaLinks + "playlist_track";
		public const string SchemaValues = Schema + "values/";
		public const string SchemaValuesDate = SchemaValues + "date";
		public const string SchemaValuesDuration = SchemaValues + "duration";
		public const string SchemaValuesName = SchemaValues + "name";
		public const string SchemaValuesNumber = SchemaValues + "number";
	}
}
