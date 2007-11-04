using System;
using System.Collections.Generic;

using Alexandria.Creation;

namespace Alexandria.Media
{
	public interface IMediaRendering
	{
		IPiece Source { get; set; }
		IMedium Medium { get; set; }
	}
}
