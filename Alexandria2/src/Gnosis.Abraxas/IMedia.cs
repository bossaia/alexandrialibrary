using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abraxas
{
	public interface IMedia
		: IResource
	{
		MediaType Type { get; }
	}
}
