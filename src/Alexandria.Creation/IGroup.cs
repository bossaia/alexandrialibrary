using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Creation
{
	public interface IGroup : IArtist
	{
		IList<IArtist> Members { get; }
	}
}
