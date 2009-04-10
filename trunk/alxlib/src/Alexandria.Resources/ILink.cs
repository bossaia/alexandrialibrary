using System;

namespace Alexandria.Resources
{
	public interface ILink : IResource
	{
		ILinkType Type { get; }
		IEntity Subject { get; }
		IEntity Value { get; set; }
		int Sequence { get; set; }
	}
}
