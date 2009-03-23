using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public static class ResourceTypes
	{
		public static readonly Uri Playlist = new Uri("http://www.alxlib.com/schema/1/playlist");
		public static readonly Uri Artist = new Uri("http://www.alxlib.com/schema/1/artist");
		public static readonly Uri Link = new Uri("http://www.alxlib.com/schema/1/link");
		public static readonly Uri Tag = new Uri("http://www.alxlib.com/schema/1/tag");
		public static readonly Uri Event = new Uri("http://www.alxlib.com/schema/1/event");
		public static readonly Uri User = new Uri("http://www.alxlib.com/schema/1/user");
		public static readonly Uri Profile = new Uri("http://www.alxlib.com/schema/1/profile");
		public static readonly Uri Media = new Uri("http://www.alxlib.com/schema/1/media");
		public static readonly Uri Audio = new Uri("http://www.alxlib.com/schema/1/media/audio");
		public static readonly Uri Video = new Uri("http://www.alxlib.com/schema/1/media/video");
		public static readonly Uri Image = new Uri("http://www.alxlib.com/schema/1/media/image");
		public static readonly Uri Text = new Uri("http://www.alxlib.com/schema/1/media/text");
	}
}
