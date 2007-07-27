using System;
using System.Collections.Generic;
using System.Net.Mime;
using Alexandria.Media;

namespace Alexandria.Playlist
{
	public class M3uFormat : BaseMediaFormat
	{
		#region Constructors
		public M3uFormat() : base("MP3 Playlist Format", "")
		{
			this.ContentTypes.Add(new ContentType("application/m3u"));
			this.ContentTypes.Add(new ContentType("audio/x-mpegurl"));
			this.ContentTypes.Add(new ContentType("audio/mpeg-url"));
			this.ContentTypes.Add(new ContentType("application/x-winamp-playlist"));
			this.ContentTypes.Add(new ContentType("audio/scpls"));
			this.ContentTypes.Add(new ContentType("audio/x-scpls"));
			this.FileExtensions.Add("m3u");
		}
		#endregion
	}
}
