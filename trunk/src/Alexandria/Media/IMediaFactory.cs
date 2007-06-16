using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaFactory : IPlugin
	{
		bool CanCreateFor<T>() where T: IMedia;
		bool CanCreateForEach<T>() where T: IList<IMedia>;
		bool CanCreateFor<T>(IMediaFormat format) where T: IMedia;
		bool CanCreateForEach<T>(IMediaFormat format) where T: IList<IMedia>;
		IMedia CreateMedia(ILocation location);
		IMedia CreateMedia(ILocation location, IMediaFormat format);
		T CreateMediaFor<T>(ILocation location) where T: IMedia;
		IMedia CreateMediaForEach<T>(ILocation location) where T : IList<IMedia>;
		T CreateMediaFor<T>(ILocation location, IMediaFormat format) where T : IMedia;
		IMedia CreateMediaForEach<T>(ILocation location, IMediaFormat format) where T: IList<IMedia>;
	}
}
