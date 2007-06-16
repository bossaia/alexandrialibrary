using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagFactory : IPlugin
	{
		ITag CreateTag(ILocation location);
		ITag CreateTag(ILocation location, ITagFormat format);
		T CreateTag<T>(ILocation location) where T : ITag;
		T CreateTag<T>(ILocation location, ITagFormat format) where T: ITag;
		IList<ITag> CreateTags(ILocation location);
		IList<ITag> CreateTags(ILocation location, ITagFormat format);
	}
}
