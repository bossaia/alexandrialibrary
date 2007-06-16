using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadata
	{
		Guid Id { get; }
		IList<IMetadataIdentifier> MetadataIdentifiers { get; }
		ILocation Location { get; }
		string Name { get; }
		
	}
}
