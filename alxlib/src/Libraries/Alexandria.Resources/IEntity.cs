using System;

namespace Alexandria.Resources
{
	public interface IEntity : IResource
	{
		string Name { get; set; }
	}
}
