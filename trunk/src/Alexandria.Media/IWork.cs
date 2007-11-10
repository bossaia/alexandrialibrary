using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IWork : IPiece
	{
		IMedium Medium { get; set; }
		IList<IRecording> Recordings { get; }
		IList<IRelease> Releases { get; }
	}
}
