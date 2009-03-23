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
		private const string DELIMITER = "/";
		private const string ROOT = DELIMITER + NS;
		private const string DEFAULT_VERSION = "1";
		private const string NAMESPACE_PREFIX = "xspf";
		private const string NAMESPACE_DELIMITER = ":";
		private const string NS = NAMESPACE_PREFIX + NAMESPACE_DELIMITER;
		private const string NAMESPACE_VALUE = "http://xspf.org/ns/0/";
		private const string ELEMENT_PLAYLIST = "playlist";
		private const string ELEMENT_TITLE =  "title";
		private const string ELEMENT_CREATOR = "creator";
		private const string ATTRIB_VERSION = "version";

		private const string XPATH_PLAYLIST = ROOT + ELEMENT_PLAYLIST;
		private const string XPATH_PLAYLIST_TITLE = ROOT + ELEMENT_PLAYLIST + DELIMITER + NS + ELEMENT_TITLE;
		private const string XPATH_PLAYLIST_CREATOR = ROOT + ELEMENT_PLAYLIST + DELIMITER + NS + ELEMENT_CREATOR;

		private static string GetNodeValueString(XmlDocument doc, string xpath, XmlNamespaceManager nsmgr)
		{
			string value = string.Empty;
			if (doc != null && !string.IsNullOrEmpty(xpath))
			{
				XmlNode node = doc.SelectSingleNode(xpath, nsmgr);
				if (node != null)
					value = node.InnerText;
			}

			return value;
		}

		public override Playlist LoadPlaylistFromFile(string fileName)
		{
			Playlist playlist = null;
			if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
			{
				playlist = new Playlist();

				XmlDocument doc = new XmlDocument();
				doc.Load(fileName);

				XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
				nsmgr.AddNamespace(NAMESPACE_PREFIX, NAMESPACE_VALUE);

				XmlNode playlistNode = doc.SelectSingleNode(XPATH_PLAYLIST, nsmgr);
				if (playlistNode != null)
				{
					//playlist.Version = playlistNode.Attributes[ATTRIB_VERSION].Value;

					playlist.Name = GetNodeValueString(doc, XPATH_PLAYLIST_TITLE, nsmgr);
					playlist.Creator = GetNodeValueString(doc, XPATH_PLAYLIST_CREATOR, nsmgr);
				}
			}

			return playlist;
		}

		public override void SavePlaylistToFile(Playlist playlist, string fileName)
		{
			if (playlist != null && !string.IsNullOrEmpty(fileName))
			{
				XmlDocument xml = new XmlDocument();
				XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
				XmlElement playlistElement = xml.CreateElement(ELEMENT_PLAYLIST, NAMESPACE_VALUE);
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
