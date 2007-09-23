using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Date
	{
		public Date(XmlNode node)
		{
			DateTime.TryParse(node.Value, out value);
		}
		
		public Date(DateTime value)
		{
			this.value = value;
		}
		
		private DateTime value;
		
		public DateTime Value
		{
			get { return value; }
		}
	}
}
