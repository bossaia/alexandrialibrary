using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IText : IResource
	{
		System.Text.Encoding Encoding { get; }
		string Data { get; }
	}
}
