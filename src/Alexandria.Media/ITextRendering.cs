using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public interface ITextRendering : IMediaRendering
	{
		Language Language { get; set; }
		string Format { get; set; }
	}
}
