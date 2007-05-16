using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaContainer : IMedia, ITagable
	{
		IList<IAudio> Audio { get; }
		IList<IVideo> Video { get; }
		IList<IImage> Images { get; }
		IList<IText> Text { get; }
	}
}
