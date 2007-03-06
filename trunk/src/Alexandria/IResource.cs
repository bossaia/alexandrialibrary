using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IResource
	{
		ResourceFormat Format {get;}
		Uri Uri {get;}
		IDictionary<object, IResource> Resources {get;}
		bool IsLocal {get;}
	}
}
