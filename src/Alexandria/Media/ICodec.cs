using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public interface ICodec
	{
		string Name { get; }
		string Description { get; }
		MediaType MediaType { get; }
		IList<BitRateType> BitRateTypes { get; }
		IList<CompressionType> CompressionTypes { get; }
	}
}
