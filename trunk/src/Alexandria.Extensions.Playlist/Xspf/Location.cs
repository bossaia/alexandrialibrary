using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Location : IAttributable
	{
		public Location(XmlNode node)
		{
			value = new Uri(node.InnerText);
		}
	
		public Location(Uri value)
		{
			this.value = value;
		}
		
		private Uri value;
		
		public Uri Value
		{
			get { return value; }
		}

		public override string ToString()
		{
			return (Value != null) ? Value.ToString() : string.Empty;
		}
	}
}
