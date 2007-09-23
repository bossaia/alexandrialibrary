using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct Duration
	{
		public Duration(XmlNode node)
		{	
			TimeSpan.TryParse(node.Value, out value);
		}
		
		public Duration(TimeSpan value)
		{
			this.value = value;
		}
		
		private TimeSpan value;
		
		public TimeSpan Value
		{
			get { return value; }
		}
	}
}
