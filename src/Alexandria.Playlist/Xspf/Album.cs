using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Album
	{
		public Album(XmlNode node)
		{
			this.value = node.Value;
		}
		
		public Album(string value)
		{
			this.value = value;
		}
		
		private string value;
		
		public string Value
		{
			get { return value; }
		}
	}
}
