using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Resources
{
	/// <summary>
	/// The seek state of a media stream
	/// </summary>
	public enum SeekState
	{
		None = 0,
		Backward,
		Error,
		Forward
	}
}
