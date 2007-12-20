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
			this.value = Convert.ToInt32(node.InnerText);
		}
		
		private int value;
		
		public int Value
		{
			get { return value; }
		}

		public override string ToString()
		{
			return string.Format("{0:00}", Value);
		}
	}
}
