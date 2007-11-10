using System;
using System.Collections.Generic;

namespace Telesophy.Alexandria.Resources
{
	public interface ICodec
	{
		string Name { get; }
		string Description { get; }
		IList<BitRateType> BitRateTypes { get; }
		IList<CompressionType> CompressionTypes { get; }
	}
}
