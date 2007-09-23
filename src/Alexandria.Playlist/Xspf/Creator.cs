using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Creator
	{	
		public Creator(string value)
		{
			this.value = value;
		}

		public Creator(XmlNode node)
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
