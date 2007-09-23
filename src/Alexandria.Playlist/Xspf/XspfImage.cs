using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct XspfImage
	{
		public XspfImage(Uri value)
		{
			this.value = value;
		}
		
		public XspfImage(XmlNode node)
		{
			Uri.TryCreate(node.Value, UriKind.RelativeOrAbsolute, out value);
		}
		
		private Uri value;
		
		public Uri Value
		{
			get { return value; }
		}
	}
}
