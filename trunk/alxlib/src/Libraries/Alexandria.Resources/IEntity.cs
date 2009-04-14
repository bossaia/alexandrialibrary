using System;

namespace Alexandria.Resources
{
	public interface IEntity : IResource
	{
		IEntityType Type { get; }
	}

	public interface IEntity<T> : IEntity
	    where T : IEntityType
	{
	    new T Type { get; }
	}
}
