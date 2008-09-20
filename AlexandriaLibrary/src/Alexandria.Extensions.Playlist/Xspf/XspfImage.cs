using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	public struct XspfImage
	{
		public XspfImage(Uri value)
		{
			this.value = value;
		}
		
		public XspfImage(XmlNode node)
		{
			value = new Uri(node.InnerText);
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
