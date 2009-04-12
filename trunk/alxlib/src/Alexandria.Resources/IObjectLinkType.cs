using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IObjectLinkType<T> : ILinkType
		where T: IEntityType
	{
		T ObjectType { get; }
	}
}
