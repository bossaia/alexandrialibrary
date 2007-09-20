using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Identifier : IAttributable 
	{
		public Identifier(XmlNode node)
		{
			this.value = new Uri(node.Value);
		}
		
		public Identifier(Uri value)
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
