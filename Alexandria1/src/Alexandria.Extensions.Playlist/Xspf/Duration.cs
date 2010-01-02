using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	public struct Duration
	{
		public Duration(XmlNode node)
		{	
			TimeSpan.TryParse(node.InnerText, out value);
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

		public override string ToString()
		{
			return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", Value.Hours, Value.Minutes, Value.Seconds, Value.Milliseconds);
		}
	}
}
