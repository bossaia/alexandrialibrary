using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaFile
	{		
		string Path {get;}
		bool IsLocal{get;}
	}
}
