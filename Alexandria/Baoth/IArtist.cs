using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baoth
{
	public interface IArtist
		: IResource
	{
		IName Name { get; }
	}
}
