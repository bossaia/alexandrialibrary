using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMedia
	{
		Guid Id { get; }
		ILocation Location { get; }
		IMediaFormat Format { get; }
		void Load();
	}
}
