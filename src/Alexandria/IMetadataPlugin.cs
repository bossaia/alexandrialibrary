using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadataPlugin : IPlugin
	{
		IList<IMetadataPluginCapability> Capabilities { get; }
		IMetadataPluginCapability GetCapability<T>() where T : IMetadata;
		T GetMetadata<T>(ILocation location) where T : IMetadata;		
		T GetMetadata<T>(IIdentifier id) where T: IMetadata;
		IList<IMetadata> FindMetadata(ISearch search);
	}
}
