using System;

namespace Sophia.Core
{
	public interface INode
	{
		Uri Context { get; }
		Uri Id { get; }
	}
}
