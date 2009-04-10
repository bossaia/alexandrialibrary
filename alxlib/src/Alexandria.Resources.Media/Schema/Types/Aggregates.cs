using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media.Schema.Types
{
	public static class Aggregates
	{
		public static readonly Uri Album = new Uri(SchemaConstants.AggregatesBase + "album");
		public static readonly Uri Artist = new Uri(SchemaConstants.AggregatesBase + "artist");
		public static readonly Uri Catalog = new Uri(SchemaConstants.AggregatesBase + "catalog");
		public static readonly Uri File = new Uri(SchemaConstants.AggregatesBase + "file");
		public static readonly Uri Playlist = new Uri(SchemaConstants.AggregatesBase + "playlist");
		public static readonly Uri Track = new Uri(SchemaConstants.AggregatesBase + "track");
	}
}
