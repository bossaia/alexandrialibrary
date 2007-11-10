using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Resources
{
	/// <summary>
	/// The buffer state of a media stream
	/// </summary>
	public enum BufferState
	{
		None = 0,
		Error,
		Loading,
		Buffering,
		Full,
		Starving
	}
}
