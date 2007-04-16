using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IImage : IMedia
	{
		IList<byte> Data { get; }
	}
}
