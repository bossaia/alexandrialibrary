using System;
using System.Collections.Generic;

using Alexandria.Media;

namespace Alexandria.Resources
{
	public interface ICodec
	{
		string Name { get; }
		string Description { get; }
		MediaTypes MediaType { get; }
		IList<BitRateType> BitRateTypes { get; }
		IList<CompressionType> CompressionTypes { get; }
	}
}
