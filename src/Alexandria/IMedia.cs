using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMedia
	{
		IIdentifier Id { get; }
		ILocation Location { get; }
		IFormat Format { get; }
		void Load();
	}
}
