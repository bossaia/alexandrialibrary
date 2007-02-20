using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public interface IMediaFile
	{		
		string Path {get;}
		bool IsLocal{get;}
	}
}
