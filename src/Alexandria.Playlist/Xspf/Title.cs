using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Title
	{		
		public Title(string value)
		{
			this.value = value;
		}

		public Title(XmlNode node)
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
