using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IText : IProxyResource
	{
		System.Text.Encoding Encoding { get; }
	}
}
