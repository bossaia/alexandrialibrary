using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Media
{
	public interface IRole
	{
		string Name { get; set; }
		string Type { get; set; }
		IArtist Performer { get; set; }
		IPerformance Performance { get; set; }
	}
}
