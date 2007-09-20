using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Title
	{
		public Title(XmlNode node)
		{
			this.value = node.Value;
		}
		
		public Title(string value)
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
