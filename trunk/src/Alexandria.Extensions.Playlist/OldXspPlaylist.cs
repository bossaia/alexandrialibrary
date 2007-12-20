#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Telesophy.Alexandria.Extensions.Playlist
{
	public class OldXspPlaylist : BasePlaylist
	{
		#region Constructors
		public OldXspPlaylist(Uri path) : base(path) //, new XspfFormat())
		{
		}
		#endregion
	
		#region Private Constants
		private const string ATTRIB_NAME = "name";
		private const string ATTRIB_VERSION = "version";
		private const string ATTRIB_LENGTH = "length";
		private const string NODE_PLAYLIST = "playlist";
		private const string NODE_TRACK_LIST = "tracklist";
		private const string NODE_TRACK = "track";
		private const string NODE_LOCATION = "location";
		private const string NODE_IDENTIFIER = "identifier";
		private const string NODE_TITLE = "title";
		private const string PREFIX_FILE = "file:///";
		private const string PREFIX_HTTP = "http://";
		private const string NAMESPACE_PLAYLIST = "http://xspf.org/ns/0/";
		private const string XSPF_VERSION = "1";
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
						string version = playlistAttrib.Value;
						if (!string.IsNullOrEmpty(version))
						{
							if (version.Length == 1)
								version += ".0.0.0";
							this.Version = new Version(version);
						}
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

			foreach (XmlNode trackNode in tracklistNode.ChildNodes)
			{
				if (trackNode != null)
				{
					if (String.Compare(trackNode.Name, NODE_TRACK, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
					{
						length = string.Empty;

						foreach (XmlAttribute trackAttribute in trackNode.Attributes)
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
						//MediaFile mediaFile = null;

						if (location != null)
						{
							TimeSpan duration = TimeSpan.Zero;
							if (!string.IsNullOrEmpty(length))
							{
								duration = TimeSpan.Parse(length);
							}
							
							PlaylistItem item = new PlaylistItem(new Uri(location), duration);
							Items.Add(item);
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
							//mediaFile = MediaFile.Load(location, length);
							//this.Files.Add(mediaFile);
						}
					}
				}
			}
		}
		#endregion

		#region Public Methods
		public override void Load()
		{
			xml = new XmlDocument();
			xml.Load(this.Path.ToString());
			foreach (XmlNode node in xml.ChildNodes)
			{
				if (string.Compare(node.Name, NODE_PLAYLIST, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
				{
					ReadPlaylistNode(node);
				}
			}
		}

		public override void Save()
		{
			XmlTextWriter writer = new XmlTextWriter(Path.LocalPath, Encoding.UTF8);
			writer.WriteStartDocument();
			writer.WriteStartElement(NODE_PLAYLIST, NAMESPACE_PLAYLIST); //<playlist>
			
			//writer.WriteStartAttribute(ATTRIB_VERSION);
			writer.WriteAttributeString(ATTRIB_VERSION, XSPF_VERSION); //version="1"
			//writer.WriteEndAttribute();
			
			writer.WriteStartElement(NODE_TRACK_LIST); //<tracklist>
			foreach(IPlaylistItem item in Items)
			{
				writer.WriteStartElement(NODE_TRACK); //<track>
				writer.WriteElementString(NODE_LOCATION, item.Path.ToString()); //<location>
				writer.WriteEndElement();
			}
			writer.WriteEndElement(); //</tracklist>
			writer.WriteEndElement(); //</playlist>
			writer.WriteEndDocument();
		}
		#endregion
	}
}
