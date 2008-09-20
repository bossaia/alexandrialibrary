using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
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
			foreach(XmlNode childNode in node.ChildNodes)
			{
				try
				{
					if (childNode.Name.Equals("location", StringComparison.InvariantCultureIgnoreCase))
					{
						Location location = new Location(childNode);
						locations.Add(location);
					}
					else if (childNode.Name.Equals("identifier", StringComparison.InvariantCultureIgnoreCase))
					{
						Identifier identifier = new Identifier(childNode);
						identifiers.Add(identifier);
					}
					else if (childNode.Name.Equals("title", StringComparison.InvariantCultureIgnoreCase))
					{
						title = new Title(childNode);
					}
					else if (childNode.Name.Equals("creator", StringComparison.InvariantCultureIgnoreCase))
					{
						creator = new Creator(childNode);
					}
					else if (childNode.Name.Equals("annotation", StringComparison.InvariantCultureIgnoreCase))
					{
						annotation = new Annotation(childNode);
					}
					else if (childNode.Name.Equals("info", StringComparison.InvariantCultureIgnoreCase))
					{
						info = new Info(childNode);
					}
					else if (childNode.Name.Equals("image", StringComparison.InvariantCultureIgnoreCase))
					{
						image = new XspfImage(childNode);
					}
					else if (childNode.Name.Equals("album", StringComparison.InvariantCultureIgnoreCase))
					{
						album = new Album(childNode);
					}
					else if (childNode.Name.Equals("trackNum", StringComparison.InvariantCultureIgnoreCase))
					{
						trackNumber = new TrackNumber(childNode);
					}
					else if (childNode.Name.Equals("duration", StringComparison.InvariantCultureIgnoreCase))
					{
						duration = new Duration(childNode);
					}
					else if (childNode.Name.Equals("link", StringComparison.InvariantCultureIgnoreCase))
					{
						Link link = new Link(childNode);
						links.Add(link);
					}
					else if (childNode.Name.Equals("meta", StringComparison.InvariantCultureIgnoreCase))
					{
						Metadata metadataItem = new Metadata(childNode);
						metadata.Add(metadataItem);
					}
					else if (childNode.Name.Equals("extension", StringComparison.InvariantCultureIgnoreCase))
					{
						Extension extension = new Extension(childNode);
						extensions.Add(extension);
					}
				}
				catch
				{
				}
			}
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
		private TrackNumber trackNumber;
		private Duration duration;
		private List<Link> links = new List<Link>();
		private List<Metadata> metadata = new List<Metadata>();
		private List<Extension> extensions = new List<Extension>();
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
		public TrackNumber TrackNumber
		{
			get { return trackNumber; }
			set { trackNumber = value; }
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
		public IList<Link> Links
		{
			get { return links; }
		}

		/// <summary>
		/// Get a list of metadata for this track
		/// </summary>
		public IList<Metadata> Metadata
		{
			get { return metadata; }
		}

		/// <summary>
		/// Get a list of XML extensions for this track
		/// </summary>
		public IList<Extension> Extensions
		{
			get { return extensions; }
		}
		#endregion
		
		#region Public Methods
		public override string ToString()
		{
			return string.Format("{0:00} {1} - {2} - {3} {4}", trackNumber, Creator, Album, Title, Duration);
		}
		#endregion
	}
}
