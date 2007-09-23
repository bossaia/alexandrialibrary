using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Info
	{
		public Info(XmlNode node)
		{
			Uri.TryCreate(node.Value, UriKind.RelativeOrAbsolute, out value);
		}
		
		public Info(Uri value)
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
