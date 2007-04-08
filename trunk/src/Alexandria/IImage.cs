using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IImage : IProxyResource
	{
		IList<byte> Data { get; }
	}
}
