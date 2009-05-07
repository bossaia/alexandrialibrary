using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IResponse
	{
		bool IsAsync { get; }
		uint Count { get; }
		IAsyncResult AsynchResult { get; }
	}
}
