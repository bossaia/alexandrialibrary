using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadataIdentifier : IIdentifier, Data.IPersistant
	{
		Guid MetadataId { get; set; }
	}
}
