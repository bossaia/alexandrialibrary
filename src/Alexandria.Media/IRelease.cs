using System;
using System.Collections.Generic;

using Alexandria;
using Alexandria.Creation;

namespace Alexandria.Media
{
	public interface IRelease
	{
		IMediaRendering Rendering { get; set; }
		DateTime DateReleased { get; set; }
		string Title { get; set; }
	}
}
