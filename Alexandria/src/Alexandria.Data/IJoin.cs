using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IJoin
	{
		IColumn Source { get; }
		IColumn Target { get; }
	}
}
