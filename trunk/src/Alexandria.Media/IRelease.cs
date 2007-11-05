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
	
	public interface IRelease<T> : IRelease where T: IMediaRendering
	{
		new T Rendering { get; set; }
	}
}
