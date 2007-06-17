using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPluginRepository : IDisposable
	{
		//IDictionary<string, IMediaFactory> MediaFactories { get; }
		IDictionary<string, ITagFactory> TagFactores { get; }
		//IDictionary<string, IMetadataFactory> MetadataFactories { get; }
		//IMediaFactory GetMediaFactory(IPluginOptions options);
		ITagFactory GetTagFactory(IPluginOptions options);		
		//IMetadataFactory GetMetadataFactory(IPluginOptions options);		
	}
}
