using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct TrackNumber
	{
		public TrackNumber(XmlNode node)
		{
			this.value = Convert.ToInt32(node.Value);
		}
		
		public TrackNumber(int value)
		{
			this.value = value;
		}
		
		private int value;
		
		public int Value
		{
			get { return value; }
		}
	}
}
