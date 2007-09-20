using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Location : IAttributable
	{
		public Location(XmlNode node)
		{
			this.value = new Uri(node.Value);
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
	}
}
