using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaPlugin : IPlugin
	{
		IList<IMediaPluginCapability> Capabilities { get; }
		IMediaPluginCapability GetCapability<T>() where T: IMedia;
		T GetMedia<T>(ILocation location) where T: IMedia;
		IList<IMedia> FindMedia(ISearch search);
	}
}
