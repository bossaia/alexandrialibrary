using System;
using System.Collections.Generic;
using System.Xml;

namespace Alexandria.Extensions.Playlist.Xspf
{
	public struct Date
	{
		public Date(XmlNode node)
		{
			DateTime.TryParse(node.InnerText, out value);
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

		public override string ToString()
		{
			return string.Format("{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}", Value.Year, Value.Month, Value.Day, Value.Hour, Value.Minute, Value.Second);
		}
	}
}
