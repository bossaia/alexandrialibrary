using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class Track
	{
		#region Constructors

		public Track()
		{
		}

		#endregion

		#region Private Members

		private IList<Link> link = new List<Link>();
		private IList<Meta> meta = new List<Meta>();
		private IList<Extension> extension = new List<Extension>();

		#endregion

		public Uri Location { get; set; }
		public Uri Identifier { get; set; }
		public string Title { get; set; }
		public string Creator { get; set; }
		public string Annotation { get; set; }
		public Uri Info { get; set; }
		public Uri Image { get; set; }
		public string Album { get; set; }
		public uint TrackNum { get; set; }
		public TimeSpan Duration { get; set; }
		public IList<Link> Link { get { return link; } }
		public IList<Meta> Meta { get { return meta; } }
		public IList<Extension> Extension { get { return extension; } }
	}
}
