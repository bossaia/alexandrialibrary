using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Album
	{		
		public Album(string value)
		{
			this.value = value;
		}

		public Album(XmlNode node)
		{
			this.value = node.Value;
		}
	
		private string value;
		
		public string Value
		{
			get { return value; }
		}
	}
}
