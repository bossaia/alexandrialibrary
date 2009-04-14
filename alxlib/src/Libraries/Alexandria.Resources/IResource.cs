using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IResource
	{
		Uri Id { get; }
		string Name { get; set; }
		string Hash { get; }
		Uri Creator { get; set; }
		DateTime Created { get; set; }
		Uri Modifier { get; set; }
		DateTime Modified { get; set; }
	}
}
