using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AlexandriaOrg.Alexandria
{
	public class XmlShareablePlaylist : MediaPlaylist
	{
		#region Private Constants
		private const string ATTRIB_NAME = "name";
		private const string ATTRIB_VERSION = "version";
		private const string ATTRIB_LENGTH = "length";
		private const string NODE_PLAYLIST = "playlist";
		private const string NODE_TRACK_LIST = "tracklist";
		private const string NODE_TRACK = "track";
		private const string NODE_LOCATION = "location";
		private const string PREFIX_FILE = "file:///";
		private const string PREFIX_HTTP = "http://";
		#endregion
		
		#region Private Fields
		private XmlDocument xml;
		#endregion
	
		#region Private Methods
		private void ReadPlaylistNode(XmlNode playlistNode)
		{
			// Read attributes
			foreach (XmlAttribute playlistAttrib in playlistNode.Attributes)
			{
				if (playlistAttrib != null)
				{
					if (String.Compare(playlistAttrib.Name, ATTRIB_NAME, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
					{
						this.Name = playlistAttrib.Value;					
					}
					else if (String.Compare(playlistAttrib.Name, ATTRIB_VERSION, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
					{
						this.Version = playlistAttrib.Value;
					}
				}
			}
			
			// Read child nodes
			foreach (XmlNode childNode in playlistNode.ChildNodes)
			{
				if (childNode != null)
				{
					if (String.Compare(childNode.Name, NODE_TRACK_LIST, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
					{
						ReadTracklistNode(childNode);
					}
				}
			}
		}
		
		private void ReadTracklistNode(XmlNode tracklistNode)
		{
			string length = string.Empty;
		
			foreach(XmlNode trackNode in tracklistNode.ChildNodes)
			{
				if (trackNode != null)
				{
					if (String.Compare(trackNode.Name, NODE_TRACK, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
					{
						length = string.Empty;
						
						foreach(XmlAttribute trackAttribute in trackNode.Attributes)
						{
							if (String.Compare(trackAttribute.Name, ATTRIB_LENGTH, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
							{
								length = trackAttribute.Value;
								break;
							}
						}
						
						ReadTrackNode(trackNode, length);
					}
				}
			}
		}
		
		private void ReadTrackNode(XmlNode trackNode, string length)
		{
			foreach (XmlNode locationNode in trackNode.ChildNodes)
			{
				if (locationNode != null)
				{
					if (String.Compare(locationNode.Name, NODE_LOCATION, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
					{
						string location = locationNode.InnerText;
						MediaFile mediaFile = null;
					
						if (location != null)
						{
							/*
							if (location.StartsWith(PREFIX_FILE, true, System.Globalization.CultureInfo.CurrentCulture))
							{
								mediaFile = new LocalMediaFile(location.Substring(PREFIX_FILE.Length, location.Length-PREFIX_FILE.Length));
							}
							else if (location.StartsWith(PREFIX_HTTP, true, System.Globalization.CultureInfo.CurrentCulture))
							{
								mediaFile = new RemoteMediaFile(location.Substring(PREFIX_HTTP.Length, location.Length - PREFIX_HTTP.Length));
							}
							*/
							mediaFile = MediaFile.Load(location, length);
							this.Files.Add(mediaFile);
						}
					}
				}
			}
		}
		#endregion
	
		#region Constructors
		public XmlShareablePlaylist(string path) : base(path)
		{
		}
		#endregion
		
		#region Protected Methods
		public override void Load()
		{
			xml = new XmlDocument();
			xml.Load(this.Path);
			foreach(XmlNode node in xml.ChildNodes)
			{
				if (String.Compare(node.Name, NODE_PLAYLIST, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
				{
					ReadPlaylistNode(node);
				}
			}
		}
		#endregion
	}
}
