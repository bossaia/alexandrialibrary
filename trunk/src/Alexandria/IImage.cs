using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IImage : IResource
	{
		IList<byte> Data { get; }
	}
}
