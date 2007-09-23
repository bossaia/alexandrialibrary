using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Playlist.Xspf
{
	public struct TrackNumber
	{
		public TrackNumber(int value)
		{
			this.value = value;
		}

		public TrackNumber(XmlNode node)
		{
			this.value = Convert.ToInt32(node.Value);
		}
		
		private int value;
		
		public int Value
		{
			get { return value; }
		}
	}
}
