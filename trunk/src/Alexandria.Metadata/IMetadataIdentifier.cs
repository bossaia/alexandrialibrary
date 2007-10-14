using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface IMetadataIdentifier
	{
		Uri Namespace { get; }
		Uri Value { get; }
	}
}
