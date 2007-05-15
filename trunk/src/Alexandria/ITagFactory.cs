using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagFactory : IPlugin
	{
		IList<ITagPluginCapability> Capabilities { get; }
		ITagPluginCapability GetCapability<T>() where T : ITag;
		T GetTag<T>(ILocation location) where T : ITag;
		IList<ITag> FindTags(ISearch search);
	}
}
