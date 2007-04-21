using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaTagFormat
	{
		string Name { get; }
		Version Version { get; }
	}
}
