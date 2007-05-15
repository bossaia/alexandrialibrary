using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPluginRepository : IDisposable
	{
		IList<IMediaFactory> MediaPlugins { get; }
		IList<ITagFactory> TagPlugins { get; }
		IList<IMetadataFactory> MetadataPlugins { get; }
		IMediaFactory GetBestMediaPlugin(IMediaPluginCapability capability, IPluginOptions options);
		ITagFactory GetBestTagPlugin(ITagPluginCapability capability, IPluginOptions options);
		IMetadataFactory GetBestMetadataPlugin(IMetadataPluginCapability capability, IPluginOptions options);
	}
}
