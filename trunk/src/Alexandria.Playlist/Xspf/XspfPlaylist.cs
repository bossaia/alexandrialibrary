using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	/// <summary>
	/// Represents an XML Shareable Playlist Format ("spiff") file
	/// </summary>
	public class XspfPlaylist
	{
		#region Constructors
		/// <summary>
		/// Instantiate an XspfPlaylist
		/// </summary>
		/// <param name="location">The source URI for this playlist</param>
		public XspfPlaylist(Uri location)
		{
			this.location = location;
		}
		#endregion

		#region Private Fields
		private Version version;
		private string title;
		private string creator;
		private string annotation;
		private Uri info;
		private Uri location;
		private Uri identifier;
		private Uri image;
		private DateTime date;
		private Uri license;
		private List<Attribution> attribution = new List<Attribution>();
		private List<Link> link = new List<Link>();
		private List<Metadata> meta = new List<Metadata>();
		private List<Extension> extension = new List<Extension>();
		private List<Track> trackList = new List<Track>();
		private XmlDocument xml;
		#endregion
		
		#region Private Methods
		private void LoadPlaylist(XmlNode playlistNode)
		{
			if (playlistNode != null)
			{
				int major = Convert.ToInt32(playlistNode.Attributes["version"].Value);
				version = new Version(major, 0);
			}
		}
		
		private void LoadTitle(XmlNode titleNode)
		{
			if (titleNode != null)
			{
				title = titleNode.Value;
			}
		}
		
		private void LoadCreator(XmlNode creatorNode)
		{
			if (creatorNode != null)
			{
				creator = creatorNode.Value;
			}
		}
		//annotation, info, location, identifier, image, date, license
		private void LoadAnnotation(XmlNode annotationNode)
		{
		}
		
		private void LoadInfo(XmlNode infoNode)
		{
		}
		
		private void LoadLocation(XmlNode locationNode)
		{
		
		}
		
		private void LoadIdentifier(XmlNode identifierNode)
		{
		
		}
		
		private void LoadImage(XmlNode imageNode)
		{
		}
		
		private void LoadDate(XmlNode dateNode)
		{
		}
		
		private void LoadLicense(XmlNode licenseNode)
		{
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get or set the version of the XSPF specification that the playlist uses
		/// </summary>
		/// <see cref="http://www.xspf.org"/>
		public Version Version
		{
			get { return version; }
			set { version = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable title for the playlist
		/// </summary>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable name of the entity (author, authors, group, company, etc) that authored the playlist
		/// </summary>
		public string Creator
		{
			get { return creator; }
			set { creator = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable comment on the playlist
		/// </summary>
		public string Annotation
		{
			get { return annotation; }
			set { annotation = value; }
		}
		
		/// <summary>
		/// Get or set a URI of a web page to find out more about this playlist
		/// </summary>
		/// <remarks>Likely to be homepage of the author, and would be used to find out more about the author and to find more playlists by the author</remarks>
		public Uri Info
		{
			get { return info; }
			set { info = value; }
		}
		
		/// <summary>
		/// Get or set the source URI for this playlist
		/// </summary>
		public Uri Location
		{
			get { return location; }
			set { location = value; }
		}
		
		/// <summary>
		/// Get or set the canonical ID for this playlist
		/// </summary>
		/// <remarks>Likely to be a hash or other location-independent name</remarks>
		public Uri Identifier
		{
			get { return identifier; }
			set { identifier = value; }
		}
		
		/// <summary>
		/// Get or set a URI of an image to display in the absence of a track-specific image
		/// </summary>
		public Uri Image
		{
			get { return image; }
			set { image = value; }
		}
		
		/// <summary>
		/// Get or set the creation date of the playlist
		/// </summary>
		/// <remarks>Do not use the last-modified date of the playlist (that should be a Meta-item)</remarks>
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}
		
		/// <summary>
		/// Get or set a URI of a resource that describes the license under which this playlist was released
		/// </summary>
		public Uri License
		{
			get { return license; }
			set { license = value; }
		}
		
		/// <summary>
		/// Get an ordered list of URIs describing playlists that this playlist is derived from
		/// </summary>
		public IList<Attribution> Attribution
		{
			get { return attribution; }
		}
		
		/// <summary>
		/// Get a list of links to resources related to this playlist
		/// </summary>
		public IList<Link> Link
		{
			get { return link; }
		}
		
		/// <summary>
		/// Get a list of metadata for this playlist
		/// </summary>
		public IList<Metadata> Meta
		{
			get { return meta; }
		}
		
		/// <summary>
		/// Get a list of XML extensions for this playlist
		/// </summary>
		public IList<Extension> Extension
		{
			get { return extension; }
		}
		
		/// <summary>
		/// Get the XML document that this playlist represents
		/// </summary>
		public XmlDocument Xml
		{
			get { return xml; }
		}
		#endregion
		
		#region IPlaylist Members
		public void Load()
		{
			if (location.IsFile)
			{
				xml = new XmlDocument();
				xml.Load(location.LocalPath);
				
				LoadPlaylist(xml.SelectSingleNode("/playlist"));
				LoadTitle(xml.SelectSingleNode("/playlist/title"));
				LoadCreator(xml.SelectSingleNode("/playlist/creator"));
				LoadAnnotation(xml.SelectSingleNode("/playlist/annotation"));
				LoadInfo(xml.SelectSingleNode("/playlist/info"));
				LoadLocation(xml.SelectSingleNode("/playlist/location"));
				LoadIdentifier(xml.SelectSingleNode("/playlist/identifier"));
				LoadImage(xml.SelectSingleNode("/playlist/image"));
				LoadDate(xml.SelectSingleNode("/playlist/date"));
				LoadLicense(xml.SelectSingleNode("/playlist/license"));
				
				foreach(XmlNode attributionNode in xml.SelectNodes("/playlist/attribution"))
					attribution.Add(new Attribution(attributionNode));
				
				foreach(XmlNode linkNode in xml.SelectNodes("/playlist/link"))
					link.Add(new Link(linkNode));

				foreach (XmlNode metaNode in xml.SelectNodes("/playlist/meta"))
					meta.Add(new Metadata(metaNode));

				foreach (XmlNode extensionNode in xml.SelectNodes("/playlist/extension"))
					extension.Add(new Extension(extensionNode));
				
				foreach(XmlNode trackNode in xml.SelectNodes("/playlist/tracklist/track"))
					trackList.Add(new Track(trackNode));
			}
			else
			{
				//handle remote playlists here
			}
		}
		
		public void Save()
		{
		}
		#endregion
	}
}
