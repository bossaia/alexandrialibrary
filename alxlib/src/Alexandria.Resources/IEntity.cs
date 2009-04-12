using System;

namespace Alexandria.Resources
{
	public interface IEntity : IResource
	{
		IEntityType Type { get; }
		ILinkCollection Links { get; }
		void AddLink(ILinkType type, IEntity value);
		void AddLink(ILinkType type, IEntity value, int sequence);
		void RemoveLink(ILink link);
	}

	public interface IEntity<T> : IEntity
	    where T : IEntityType
	{
	    new T Type { get; }
	}
}
