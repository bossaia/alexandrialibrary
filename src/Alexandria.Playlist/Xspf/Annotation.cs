using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Annotation
	{
		public Annotation(XmlNode node)
		{
			this.value = node.Value;
		}
		
		public Annotation(string value)
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
