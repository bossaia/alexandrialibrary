using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadataIdentifier : IIdentifier, IPersistant
	{
		Guid MetadataId { get; set; }
	}
}
