using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IPiece
	{
		string Title { get; set; }
		DateTime DateCreated { get; set; }
		IList<IArtist> Creators { get; }
	}
}
