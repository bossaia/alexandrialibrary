using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaFactory : IPlugin
	{
		IList<IMediaPluginCapability> Capabilities { get; }
		IMediaPluginCapability GetCapability<T>() where T: IMedia;
		IMediaPluginCapability GetCapability<T>(IMediaFormat format) where T: IMedia;
		bool CanCreate<T>() where T: IMedia;
		bool CanCreate<T>(IMediaFormat format) where T: IMedia;
		IMedia CreateMedia(ILocation location);
		IMedia CreateMedia(ILocation location, IMediaFormat format);
		T CreateMedia<T>(ILocation location) where T: IMedia;
		T CreateMedia<T>(ILocation location, IMediaFormat format) where T : IMedia;
	}
}
