using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public interface IMediaResource
	{
		ResourceFormat Format {get;}
		Uri Uri {get;}
		IList<IMediaResource> Resources {get;}
		void SaveAs(ResourceFormat format, Uri uri);
	}
}
