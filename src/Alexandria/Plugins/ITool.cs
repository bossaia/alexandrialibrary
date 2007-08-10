using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Plugins
{
	public interface ITool
	{
		Guid Id { get; }
		string Name { get; }
		string Description { get; }
		Version Version { get; }
		bool Enabled { get; set; }
		IPlugin Plugin { get; }
	}
}
