using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IPerformance
	{
		string Title { get; set; }
		string Type { get; set; }
		DateTime DatePerformed { get; set; }
		ILocation Location { get; set; }
		IPiece Piece { get; set; }
		IList<IRole> Roles { get; }
		IList<IRecording> Recordings { get; }
	}
}
