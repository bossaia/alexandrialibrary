using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Data;

namespace Alexandria
{
	public interface IMetadataIdentifier : IIdentifier, IPersistant
	{
		Guid ParentId { get; set; }
	}
}
