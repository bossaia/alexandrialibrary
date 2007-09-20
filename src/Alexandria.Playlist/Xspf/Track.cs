using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	/// <summary>
	/// Represents a media resource in an XSPF Playlist
	/// </summary>
	public class Track
	{
		#region Constructors
		/// <summary>
		/// Instantiate a Track
		/// </summary>
		public Track()
		{
		}
		
		/// <summary>
		/// Instantiate a Track
		/// </summary>
		/// <param name="node">An XML node containing the track data</param>
		public Track(XmlNode node)
		{
		
		}
		#endregion
		
		#region Private Fields
		private List<Location> locations = new List<Location>();
		private List<Identifier> identifiers = new List<Identifier>();
		private Title title;
		private Creator creator;
		private Annotation annotation;
		private Info info;
		private XspfImage image;
		private Album album;
		private TrackNumber trackNum;
		private Duration duration;
		private List<Link> link = new List<Link>();
		private List<Metadata> meta = new List<Metadata>();
		private List<Extension> extension = new List<Extension>();
		#endregion
		
		#region Private Methods
		private void LoadLocation(XmlNode locationNode)
		{
		}
		
		private void LoadIdentifier(XmlNode identifierNode)
		{
		
		}
		
		private void LoadTitle(XmlNode titleNode)
		{
		}
		
		private void LoadCreator(XmlNode creatorNode)
		{
		}

		private void LoadAnnotation(XmlNode annotationNode)
		{
		}

		private void LoadInfo(XmlNode infoNode)
		{
		}

		private void LoadImage(XmlNode imageNode)
		{
		}

		private void LoadAlbum(XmlNode albumNode)
		{
		}

		private void LoadTrackNum(XmlNode trackNumNode)
		{
		
		}
		
		private void LoadDuration(XmlNode durationNode)
		{
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get a list of URIs of the resource to be rendered
		/// </summary>
		public IList<Location> Locations
		{
			get { return locations; }
		}
		
		/// <summary>
		/// Get a list of canonical IDs for this resource
		/// </summary>
		public IList<Identifier> Identifiers
		{
			get { return identifiers; }
		}
		
		/// <summary>
		/// Get or set a human-readable name of the track that authored the resource which defines the duration of track rendering
		/// </summary>
		public Title Title
		{
			get { return title; }
			set { title = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable name of the entity (author, authors, group, company, etc) that authored the resource which defines the duration of track rendering.
		/// </summary>
		public Creator Creator
		{
			get { return creator; }
			set { creator = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable comment on the track
		/// </summary>
		public Annotation Annotation
		{
			get { return annotation; }
			set { annotation = value; }
		}
		
		/// <summary>
		/// Get or set a URI of a place where this resource can be bought or more info can be found
		/// </summary>
		public Info Info
		{
			get { return info; }
			set { info = value; }
		}
		
		/// <summary>
		/// Get or set a URI of an image to display for the duration of the track
		/// </summary>
		public XspfImage Image
		{
			get { return image; }
			set { image = value; }
		}
		
		/// <summary>
		/// Get or set the human-readable name of the collection from which the resource which defines the duration of track rendering comes
		/// </summary>
		/// <remarks>For a song originally published as a part of a CD or LP, this would be the title of the original release</remarks>
		public Album Album
		{
			get { return album; }
			set { album = value; }
		}
		
		/// <summary>
		/// Get or set the ordinal position of this track on its album
		/// </summary>
		public TrackNumber TrackNum
		{
			get { return trackNum; }
			set { trackNum = value; }
		}
		
		/// <summary>
		/// Get or set the time to render this resource
		/// </summary>
		public Duration Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		/// <summary>
		/// Get a list of links to resources related to this track
		/// </summary>
		public IList<Link> Link
		{
			get { return link; }
		}

		/// <summary>
		/// Get a list of metadata for this track
		/// </summary>
		public IList<Metadata> Meta
		{
			get { return meta; }
		}

		/// <summary>
		/// Get a list of XML extensions for this track
		/// </summary>
		public IList<Extension> Extension
		{
			get { return extension; }
		}
		#endregion
	}
}
