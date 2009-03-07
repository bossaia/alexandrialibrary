using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Data
{
	public class TrackData
	{
		public TrackData()
		{
		}

		#region Private Members

		private IList<LinkData> link = new List<LinkData>();
		private IList<MetaData> meta = new List<MetaData>();
		private IList<ExtensionData> extension = new List<ExtensionData>();

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
		public IList<LinkData> Link { get { return link; } }
		public IList<MetaData> Meta { get { return meta; } }
		public IList<ExtensionData> Extension { get { return extension; } }
	}
}
