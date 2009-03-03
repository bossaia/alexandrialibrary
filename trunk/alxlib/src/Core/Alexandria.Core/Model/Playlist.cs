using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Alexandria.Core.Model
{
	public class Playlist
	{
		#region Constructors
		public Playlist()
		{
		}
		#endregion

		#region Private Fields

		private IList<Namespace> namespaces = new List<Namespace>();
		private IList<Attribution> attribution = new List<Attribution>();
		private IList<Link> link = new List<Link>();
		private IList<Meta> meta = new List<Meta>();
		private IList<Extension> extension = new List<Extension>();
		private IList<Track> trackList = new List<Track>();

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
		public IList<Namespace> Namespaces { get { return namespaces; } }
		public IList<Attribution> Attribution { get { return attribution; } }
		public IList<Link> Link { get { return link; } }
		public IList<Meta> Meta { get { return meta; } }
		public IList<Extension> Extension { get { return extension; } }
		public IList<Track> TrackList { get { return trackList; } }
	}
}
