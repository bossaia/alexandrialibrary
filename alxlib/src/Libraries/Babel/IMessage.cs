using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IMessage
	{
		IMetadataSet Headers { get; }
		object Body { get; }
		bool TryParse<T>(out T body)
			where T : class;
	}
}
