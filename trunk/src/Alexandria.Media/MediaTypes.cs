using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	[Flags]
	public enum MediaTypes
	{
		None = 0,
		Audio = 1,
		Image = 2,
		Other = 4,
		Text = 8,
		Video = 16
	}
}
