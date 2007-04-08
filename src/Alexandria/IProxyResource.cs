using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IProxyResource : IResource
	{
		void Load();
	}
}
