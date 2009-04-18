using System;

namespace Alexandria.Resources
{
	public interface IEntity : IResource
	{
		IEntityType Type { get; }
		string Name { get; set; }
	}
}
