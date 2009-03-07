using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Alexandria.Core.Model
{
	public class XspfPlaylistFormatter : PlaylistFormatter
	{
		private const string DEFAULT_VERSION = "1";
		private const string NAMESPACE = "http://xspf.org/ns/0/";
		private const string ELEMENT_PLAYLIST = "playlist";
		private const string ATTRIB_VERSION = "version";

		public override Playlist LoadPlaylistFromFile(string fileName)
		{
			Playlist playlist = null;
			if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
			{
				playlist = new Playlist();
			}

			return playlist;
		}

		public override void SavePlaylistToFile(Playlist playlist, string fileName)
		{
			if (playlist != null && !string.IsNullOrEmpty(fileName))
			{
				XmlDocument xml = new XmlDocument();
				XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
				XmlElement playlistElement = xml.CreateElement(ELEMENT_PLAYLIST, NAMESPACE);
				XmlAttribute versionAttribute = xml.CreateAttribute(ATTRIB_VERSION);
				versionAttribute.Value = DEFAULT_VERSION;
				playlistElement.Attributes.Append(versionAttribute);


				xml.AppendChild(declaration);
				xml.AppendChild(playlistElement);
				xml.Save(fileName);
			}
		}
	}
}
