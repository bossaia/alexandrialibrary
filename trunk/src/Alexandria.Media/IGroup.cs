using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IGroup : IArtist
	{
		DateTime DateFormed { get; set; }
		DateTime DateDisbanded { get; set; }
		ILocation LocationFormed { get; set; }
		IList<IArtist> Members { get; }
	}
}
