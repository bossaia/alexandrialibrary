using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Plugins
{
	public interface ITool
	{
		string Name { get; }
		string Description { get; }
	}
}
