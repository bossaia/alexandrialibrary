using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	/// <summary>
	/// The buffer state of a media stream
	/// </summary>
	public enum BufferState
	{
		None = 0,
		Loading,
		Buffering,
		Starving,
		Complete
	}
}
