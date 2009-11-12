using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IMetadataSet : IDictionary<string, object>
	{
		bool TryGetValue<T>(string key, out T value)
			where T : struct;

		bool TryGetInstance<T>(string key, out T value)
			where T : class;
	}
}
