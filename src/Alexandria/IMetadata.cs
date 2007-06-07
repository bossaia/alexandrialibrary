using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadata
	{
		Guid AlexandriaId { get; }
		IList<IIdentifier> OtherIdentifiers { get; }
		ILocation Location { get; }
		string Name { get; }
		
	}
}
