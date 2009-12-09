using System;

namespace Sophia.Core
{
	public interface IRoute
	{
		Uri Destination { get; }
		bool IsValid(IContext context);
	}
}
