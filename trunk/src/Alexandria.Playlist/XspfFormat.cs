using System;
using System.Collections.Generic;
using System.Net.Mime;
using Alexandria.Media;

namespace Alexandria.Playlist
{
	public class XspfFormat : BaseMediaFormat
	{
		#region Constructors
		public XspfFormat() : base("XSPF Playlist Format", "")
		{
			this.ContentTypes.Add(new ContentType("application/xspf+xml"));
			this.FileExtensions.Add("xspf");
		}
		#endregion		
	}
}
