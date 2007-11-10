using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IPerson : IArtist
	{
		DateTime DateBorn { get; set; }
		DateTime DateDied { get; set; }
		ILocation LocationBorn { get; set; }
	}
}
