using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public interface ICollectedRendering : IMediaRendering
	{
		IList<IMediaRendering> Items { get; set; }
	}
}
