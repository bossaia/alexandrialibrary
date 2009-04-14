using System;

namespace Alexandria.Resources
{
	public interface ILink : IResource
	{
		ILinkType Type { get; }
		IEntity Subject { get; }
		IEntity Object { get; set; }
		int Sequence { get; set; }
	}

	public interface ILink<X, Y> : ISubjectLink<X>, IObjectLink<Y>
		where X : IEntityType 
		where Y: IEntityType
	{
		new ILinkType<X, Y> Type { get; }
	}

	public interface ILink<X, Y, T> : ILink<X, Y>
		where X : IEntityType
		where Y : IEntityType
		where T : ILinkType<X, Y>
	{
		new T Type { get; }
	}

}
