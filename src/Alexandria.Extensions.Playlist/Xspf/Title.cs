using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	public struct Title
	{		
		public Title(string value)
		{
			this.value = value;
		}

		public Title(XmlNode node)
		{
			this.value = node.InnerText;
		}
		
		private string value;
		
		public string Value
		{
			get { return value; }
		}

		public override string ToString()
		{
			return (Value != null) ? Value : string.Empty;
		}
	}
}
