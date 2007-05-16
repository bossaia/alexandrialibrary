using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPluginRepository : IDisposable
	{
		IDictionary<string, IMediaFactory> MediaFactories { get; }
		IDictionary<string, ITagFactory> TagFactores { get; }
		IDictionary<string, IMetadataFactory> MetadataFactories { get; }
		IMediaFactory GetBestMediaFactoryFor<T>(IPluginOptions options) where T: IMedia;
		IMediaFactory GetBestMediaFactoryForEach<T>(IPluginOptions options) where T: IList<IMedia>;
		ITagFactory GetBestTagFactoryFor<T>(IPluginOptions options) where T: ITag;
		ITagFactory GetBestTagFactoryForEach<T>(IPluginOptions options) where T : IList<ITag>;
		IMetadataFactory GetBestMetadataFactoryFor<T>(IPluginOptions options) where T: IMetadata;
		IMetadataFactory GetBestMetadataFactoryForEach<T>(IPluginOptions options) where T: IList<IMetadata>;
	}
}
