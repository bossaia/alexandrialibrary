using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPluginRepository : IDisposable
	{
		IList<IMediaPlugin> MediaPlugins { get; }
		IList<ITagPlugin> TagPlugins { get; }
		IList<IMetadataPlugin> MetadataPlugins { get; }
		IMediaPlugin GetBestMediaPlugin(IMediaCapability capability, IPluginOptions options);
		ITagPlugin GetBestTagPlugin(ITagPluginCapability capability, IPluginOptions options);
		IMetadataPlugin GetBestMetadataPlugin(IMetadataPluginCapability capability, IPluginOptions options);
	}
}
