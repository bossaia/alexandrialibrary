using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public enum SeekState
	{
		None = 0,
		Backward,
		Error,
		Forward
	}
}
