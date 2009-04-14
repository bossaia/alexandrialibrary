using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Web
{
	public struct DateAndTime
	{
		public ushort Year { get; set; }
		public byte Month { get; set; }
		public byte Day { get; set; }
		public byte Hours { get; set; }
		public byte Minutes { get; set; }
		public byte Seconds { get; set; }
		public string TimeZoneDesignator { get; set; }

		public override string ToString()
		{
			return string.Format("{0:0000}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}{6}", Year, Month, Day, Hours, Minutes, Seconds, TimeZoneDesignator);
		}
	}
}
