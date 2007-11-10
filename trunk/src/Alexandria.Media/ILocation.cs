using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface ILocation
	{
		string Name { get; set; }
		string Type { get; set; }
		decimal Longitude { get; set; }
		decimal Latitude { get; set; }
	}
}
