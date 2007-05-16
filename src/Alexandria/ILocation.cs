using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ILocation
	{
		string Path { get; }
		bool IsLocal { get; }
		
		//TODO: figure out how to handle authentication elegantly
		//bool RequiresAuthentication { get; }
	}
}