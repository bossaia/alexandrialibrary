using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IArtist : IResource
	{
		string Name { get; }		
	}
}
