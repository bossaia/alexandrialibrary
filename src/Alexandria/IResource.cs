using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public interface IResource
	{
		ResourceFormat Format {get;}
		Uri Uri {get;}
		IDictionary<object, IResource> Resources {get;}
		//void SaveAs(ResourceFormat format, Uri uri);
	}
}
