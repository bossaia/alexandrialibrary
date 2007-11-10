using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IArtist
	{
		string Name { get; set; }
		DateTime DateStarted { get; set; }
		DateTime DateStopped { get; set; }
		IList<IRole> Roles { get; }
		IList<IPiece> Pieces { get; }
	}
}
