using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface ILocation
	{
		Coordinate MainCoordinate { get; set; }
		IList<Coordinate> BoundingCoordinates { get; }
		decimal Elevation { get; set; }
	}
}
