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
		IMediaFactory GetMediaFactoryFor<T>(IPluginOptions options) where T: IMedia;
		IMediaFactory GetMediaFactoryForEach<T>(IPluginOptions options) where T: IList<IMedia>;
		ITagFactory GetTagFactoryFor<T>(IPluginOptions options) where T: ITag;
		ITagFactory GetTagFactoryForEach<T>(IPluginOptions options) where T : IList<ITag>;
		IMetadataFactory GetMetadataFactoryFor<T>(IPluginOptions options) where T: IMetadata;
		IMetadataFactory GetMetadataFactoryForEach<T>(IPluginOptions options) where T: IList<IMetadata>;
	}
}
