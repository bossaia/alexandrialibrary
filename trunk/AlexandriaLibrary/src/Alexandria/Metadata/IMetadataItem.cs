using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface IMetadataItem
	{
		IIdentifier Identifier { get; }
		Uri Path { get; }
	}
}
