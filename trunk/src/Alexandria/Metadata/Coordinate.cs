using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public enum LatitudeType
	{
		North,
		South
	}
	
	public enum LongitudeType
	{
		East,
		West
	}

	public struct Coordinate
	{
		public Coordinate(decimal latitude, decimal longitude)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.latitudeType = (latitude >= 0) ? LatitudeType.North : LatitudeType.South;
			this.longitudeType = (longitude >= 0) ? LongitudeType.East : LongitudeType.West;
		}
	
		private decimal latitude;
		private decimal longitude;
		private LatitudeType latitudeType;
		private LongitudeType longitudeType;
		
		public decimal Latitude
		{
			get { return latitude; }
		}
		
		public LatitudeType LatitudeType
		{
			get { return latitudeType; }
		}
		
		public decimal Longitude
		{
			get { return longitude; }
		}
		
		public LongitudeType LongitudeType
		{
			get { return longitudeType; }
		}
	}
}
