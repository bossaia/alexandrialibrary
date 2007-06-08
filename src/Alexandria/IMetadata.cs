using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadata
	{
		Guid Id { get; }
		IList<IIdentifier> OtherIdentifiers { get; }
		ILocation Location { get; }
		string Name { get; }
		
	}
}
