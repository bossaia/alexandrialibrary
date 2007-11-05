using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public interface IManifest
	{
		IMediaRendering Rendering { get; set; }
		//Additional data about the rendering goes here (e.g. CD TOC, DVD structure, etc.)
	}
}
