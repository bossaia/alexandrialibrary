using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadataPlugin : IPlugin
	{
		T GetMetadata<T>() where T : IMetadata;		
		T GetMetadata<T>(ISearch search) where T : IMetadata;
	}
}
