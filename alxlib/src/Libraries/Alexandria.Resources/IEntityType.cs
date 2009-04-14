using System;

namespace Alexandria.Resources
{
	public interface IEntityType : IResource
	{
		string IdMask { get; }
		string NameMask { get; }
	}
}
