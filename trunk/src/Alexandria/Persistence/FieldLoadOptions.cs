using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[Flags]
	public enum FieldLoadOptions
	{
		None = 0,
		LazyLoad = 1,
		Proxy = 2,
		LazyLoadProxy = LazyLoad | Proxy
	}
}
