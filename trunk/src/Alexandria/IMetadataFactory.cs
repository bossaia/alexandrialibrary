using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadataFactory : IPlugin
	{
		IMetadata CreateMetadata(ILocation location);
		IMetadata CreateMetadata(IIdentifier id);
		T CreateMetadata<T>(ILocation location) where T : IMetadata;		
		T CreateMetadata<T>(IIdentifier id) where T: IMetadata;
	}
}
