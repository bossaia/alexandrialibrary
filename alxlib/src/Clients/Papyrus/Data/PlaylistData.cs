using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Papyrus.Data
{
	public class PlaylistData
	{
		public PlaylistData()
		{
		}

		#region Private Fields

		private IList<NamespaceData> namespaces = new List<NamespaceData>();
		private IList<AttributionData> attribution = new List<AttributionData>();
		private IList<LinkData> link = new List<LinkData>();
		private IList<MetaData> meta = new List<MetaData>();
		private IList<ExtensionData> extension = new List<ExtensionData>();
		private IList<TrackData> trackList = new List<TrackData>();

		#endregion

		public string Version { get; set; }
		public string Title { get; set; }
		public string Creator { get; set; }
		public string Annotation { get; set; }
		public Uri Info { get; set; }
		public Uri Location { get; set; }
		public Uri Identifier { get; set; }
		public Uri Image { get; set; }
		public DateTime Date { get; set; }
		public Uri License { get; set; }
		public IList<NamespaceData> Namespaces { get { return namespaces; } }
		public IList<AttributionData> Attribution { get { return attribution; } }
		public IList<LinkData> Link { get { return link; } }
		public IList<MetaData> Meta { get { return meta; } }
		public IList<ExtensionData> Extension { get { return extension; } }
		public IList<TrackData> TrackList { get { return trackList; } }
	}
}
