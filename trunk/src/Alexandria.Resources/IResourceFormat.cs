using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Telesophy.Alexandria.Resources
{
	public interface IResourceFormat
	{
		string Name { get; }
		string Description { get; }
		IList<ContentType> ContentTypes { get; }
		IList<string> FileExtensions { get; }
		IList<ICodec> Codecs { get; }
		IList<IResourceFormat> SupportedResourceFormats { get; }
		IList<IMetadataFormat> SupportedMetadataFormats { get; }
	}
}
