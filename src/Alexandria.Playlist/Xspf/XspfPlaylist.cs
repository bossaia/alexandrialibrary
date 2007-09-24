using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
		/// <param name="path">The source URI for this playlist</param>
		public XspfPlaylist(Uri path)
		{
			this.path = path;
		}
		#endregion

		#region Private Constants
		private const string PLAYLIST_PREFIX = "xspf";
		private const string PLAYLIST_NAMESPACE = "http://xspf.org/ns/0/";
		private const string PLAYLIST_VERSION = "1";
		#endregion

		#region Private Fields
		private Uri path;
		
		private string version;
		private Title title;
		private Creator creator;
		private Annotation annotation;
		private Info info;
		private Location location;
		private Identifier identifier;
		private XspfImage image;
		private Date date;
		private License license;
		private List<Attribution> attributions = new List<Attribution>();
		private List<Link> links = new List<Link>();
		private List<Metadata> metadata = new List<Metadata>();
		private List<Extension> extensions = new List<Extension>();
		private List<Track> tracks = new List<Track>();
		private XmlDocument xml;
		private XmlNamespaceManager namespaceManager;
		#endregion
		
		#region Private Methods
		private void LoadPlaylist(XmlNode playlistNode)
		{
			if (playlistNode != null)
				version = playlistNode.Attributes["version"].Value;
		}
		
		private void LoadTitle(XmlNode titleNode)
		{
			if (titleNode != null)
				title = new Title(titleNode);
		}
		
		private void LoadCreator(XmlNode creatorNode)
		{
			if (creatorNode != null)
				creator = new Creator(creatorNode);
		}
		
		private void LoadAnnotation(XmlNode annotationNode)
		{
			if (annotationNode != null)
				annotation = new Annotation(annotationNode);
		}
		
		private void LoadInfo(XmlNode infoNode)
		{
			if (infoNode != null)
				info = new Info(infoNode);
		}
		
		private void LoadLocation(XmlNode locationNode)
		{
			if (locationNode != null)
				location = new Location(locationNode);
		}
		
		private void LoadIdentifier(XmlNode identifierNode)
		{
			if (identifierNode != null)
				identifier = new Identifier(identifierNode);
		}
		
		private void LoadImage(XmlNode imageNode)
		{
			if (imageNode != null)
				image = new XspfImage(imageNode);
		}
		
		private void LoadDate(XmlNode dateNode)
		{
			if (dateNode != null)
				date = new Date(dateNode);
		}
		
		private void LoadLicense(XmlNode licenseNode)
		{
			if (licenseNode != null)
				license = new License(licenseNode);
		}
		#endregion
		
		#region Public Properties
		/// <summary>
		/// Get the path for this playlist file
		/// </summary>
		public Uri Path
		{
			get { return path; }
		}
		
		/// <summary>
		/// Get or set the version of the XSPF specification that the playlist uses
		/// </summary>
		/// <see cref="http://www.xspf.org"/>
		public string Version
		{
			get { return version; }
			set { version = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable title for the playlist
		/// </summary>
		public Title Title
		{
			get { return title; }
			set { title = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable name of the entity (author, authors, group, company, etc) that authored the playlist
		/// </summary>
		public Creator Creator
		{
			get { return creator; }
			set { creator = value; }
		}
		
		/// <summary>
		/// Get or set a human-readable comment on the playlist
		/// </summary>
		public Annotation Annotation
		{
			get { return annotation; }
			set { annotation = value; }
		}
		
		/// <summary>
		/// Get or set a URI of a web page to find out more about this playlist
		/// </summary>
		/// <remarks>Likely to be homepage of the author, and would be used to find out more about the author and to find more playlists by the author</remarks>
		public Info Info
		{
			get { return info; }
			set { info = value; }
		}
		
		/// <summary>
		/// Get or set the source URI for this playlist
		/// </summary>
		public Location Location
		{
			get { return location; }
			set { location = value; }
		}
		
		/// <summary>
		/// Get or set the canonical ID for this playlist
		/// </summary>
		/// <remarks>Likely to be a hash or other location-independent name</remarks>
		public Identifier Identifier
		{
			get { return identifier; }
			set { identifier = value; }
		}
		
		/// <summary>
		/// Get or set a URI of an image to display in the absence of a track-specific image
		/// </summary>
		public XspfImage Image
		{
			get { return image; }
			set { image = value; }
		}
		
		/// <summary>
		/// Get or set the creation date of the playlist
		/// </summary>
		/// <remarks>Do not use the last-modified date of the playlist (that should be a Meta-item)</remarks>
		public Date Date
		{
			get { return date; }
			set { date = value; }
		}
		
		/// <summary>
		/// Get or set a URI of a resource that describes the license under which this playlist was released
		/// </summary>
		public License License
		{
			get { return license; }
			set { license = value; }
		}
		
		/// <summary>
		/// Get an ordered list of URIs describing playlists that this playlist is derived from
		/// </summary>
		public IList<Attribution> Attributions
		{
			get { return attributions; }
		}
		
		/// <summary>
		/// Get a list of links to resources related to this playlist
		/// </summary>
		public IList<Link> Links
		{
			get { return links; }
		}
		
		/// <summary>
		/// Get a list of metadata for this playlist
		/// </summary>
		public IList<Metadata> Metadata
		{
			get { return metadata; }
		}
		
		/// <summary>
		/// Get a list of XML extensions for this playlist
		/// </summary>
		public IList<Extension> Extensions
		{
			get { return extensions; }
		}
		
		/// <summary>
		/// Get a list of the tracks in this playlist
		/// </summary>
		public IList<Track> Tracks
		{
			get { return tracks; }
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
			if (path.IsFile)
			{
				if (File.Exists(path.LocalPath))
				{
					try
					{
						xml = new XmlDocument();
						xml.Load(path.LocalPath);
						namespaceManager = new XmlNamespaceManager(xml.NameTable);
						namespaceManager.AddNamespace(PLAYLIST_PREFIX, PLAYLIST_NAMESPACE);
					}
					catch(XmlException ex)
					{
						throw new ApplicationException("There was an error loading this XSPF playlist: invalid XML", ex);
					}
					
					LoadPlaylist(xml.DocumentElement);
					LoadTitle(xml.DocumentElement.SelectSingleNode("xspf:title", namespaceManager));
					LoadCreator(xml.DocumentElement.SelectSingleNode("xspf:creator", namespaceManager));
					LoadAnnotation(xml.DocumentElement.SelectSingleNode("xspf:annotation", namespaceManager));
					LoadInfo(xml.DocumentElement.SelectSingleNode("xspf:info", namespaceManager));
					LoadLocation(xml.DocumentElement.SelectSingleNode("xspf:location", namespaceManager));
					LoadIdentifier(xml.DocumentElement.SelectSingleNode("xspf:identifier", namespaceManager));
					LoadImage(xml.DocumentElement.SelectSingleNode("xspf:image", namespaceManager));
					LoadDate(xml.DocumentElement.SelectSingleNode("xspf:date", namespaceManager));
					LoadLicense(xml.DocumentElement.SelectSingleNode("xspf:license", namespaceManager));
					
					XmlNode attributionNode = xml.DocumentElement.SelectSingleNode("xspf:attribution", namespaceManager);
					if (attributionNode != null)
					{
						foreach(XmlNode attributionChildNode in attributionNode.ChildNodes)
						{
							attributions.Add(new Attribution(attributionChildNode));
						}
					}

					foreach (XmlNode linkNode in xml.DocumentElement.SelectNodes("xspf:link", namespaceManager))
						links.Add(new Link(linkNode));

					foreach (XmlNode metaNode in xml.DocumentElement.SelectNodes("xspf:meta", namespaceManager))
						metadata.Add(new Metadata(metaNode));

					foreach (XmlNode extensionNode in xml.DocumentElement.SelectNodes("xspf:extension", namespaceManager))
						extensions.Add(new Extension(extensionNode));
					
					XmlNode tracklistNode = xml.DocumentElement.SelectSingleNode("xspf:trackList", namespaceManager);
					if (tracklistNode != null)
					{
						foreach(XmlNode trackNode in tracklistNode.ChildNodes)
							tracks.Add(new Track(trackNode));
					}
				}
			}
			else
			{
				//handle remote playlists here
			}
		}
		
		public void Save(Uri path)
		{
			try
			{
				XmlTextWriter writer = new XmlTextWriter(path.LocalPath, Encoding.UTF8);
				writer.WriteStartDocument();
				writer.WriteStartElement("playlist", PLAYLIST_NAMESPACE);
				writer.WriteAttributeString("version", PLAYLIST_VERSION);
				
				writer.WriteElementString("title", Title.ToString());
				writer.WriteElementString("creator", Creator.ToString());
				writer.WriteElementString("annotation", Annotation.ToString());
				writer.WriteElementString("info", Info.ToString());
				writer.WriteElementString("location", Location.ToString());
				writer.WriteElementString("identifier", Identifier.ToString());
				writer.WriteElementString("image", Image.ToString());
				writer.WriteElementString("date", Date.ToString());
				writer.WriteElementString("license", License.ToString());
				
				writer.WriteStartElement("attribution");
				foreach(Attribution attribution in Attributions)
				{
					if (attribution.Value is Location)
						writer.WriteElementString("location", attribution.Value.ToString());
					else if (attribution.Value is Identifier)
						writer.WriteElementString("identifier", attribution.Value.ToString());
				}
				writer.WriteEndElement();
				
				foreach(Link link in Links)
				{
					writer.WriteStartElement("link");
					writer.WriteAttributeString("rel", link.Rel.ToString());
					writer.WriteValue(link.Content.ToString());
					writer.WriteEndElement();
				}

				foreach(Metadata metadataItem in this.Metadata)
				{
					writer.WriteStartElement("meta");
					writer.WriteAttributeString("rel", metadataItem.Rel.ToString());
					writer.WriteValue(metadataItem.Content);
					writer.WriteEndElement();
				}
				
				foreach(Extension extension in Extensions)
				{
					if (!string.IsNullOrEmpty(extension.ContentNamespace))
						writer.WriteStartElement("extension", extension.ContentNamespace);
					else writer.WriteStartElement("extension");	
						
					writer.WriteAttributeString("application", extension.Application.ToString());
					foreach(XmlNode extensionNode in extension.Content)
					{
						writer.WriteRaw(extensionNode.InnerXml);
					}
					writer.WriteEndElement();
				}
				
				
				foreach(Track track in Tracks)
				{
					writer.WriteStartElement("track");
					
					foreach(Location trackLocation in track.Locations)
						writer.WriteElementString("location", trackLocation.ToString());
						
					foreach(Identifier trackIdentifier in track.Identifiers)
						writer.WriteElementString("identifier", trackIdentifier.ToString());
						
					writer.WriteElementString("title", track.Title.ToString());
					writer.WriteElementString("creator", track.Creator.ToString());
					writer.WriteElementString("annotation", track.Annotation.ToString());
					writer.WriteElementString("image", track.Image.ToString());
					writer.WriteElementString("album", track.Album.ToString());
					writer.WriteElementString("trackNum", track.TrackNumber.ToString());
					writer.WriteElementString("duration", track.Duration.ToString());

					foreach (Link trackLink in track.Links)
					{
						writer.WriteStartElement("link");
						writer.WriteAttributeString("rel", trackLink.Rel.ToString());
						writer.WriteValue(trackLink.Content.ToString());
						writer.WriteEndElement();
					}

					foreach (Metadata trackMetadataItem in track.Metadata)
					{
						writer.WriteStartElement("meta");
						writer.WriteAttributeString("rel", trackMetadataItem.Rel.ToString());
						writer.WriteValue(trackMetadataItem.Content);
						writer.WriteEndElement();
					}

					foreach (Extension trackExtension in track.Extensions)
					{
						if (!string.IsNullOrEmpty(trackExtension.ContentNamespace))
							writer.WriteStartElement("extension", trackExtension.ContentNamespace);
						else writer.WriteStartElement("extension");

						writer.WriteAttributeString("application", trackExtension.Application.ToString());
						foreach (XmlNode trackExtensionNode in trackExtension.Content)
						{
							writer.WriteRaw(trackExtensionNode.InnerXml);
						}
						writer.WriteEndElement();
					}
					
					writer.WriteEndElement();
				}
				
				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Close();
			}
			catch(Exception)
			{
			
			}
		}
		#endregion
	}
}
