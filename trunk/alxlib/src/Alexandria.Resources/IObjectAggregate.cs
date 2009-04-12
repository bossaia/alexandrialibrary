using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IObjectAggregate<ObjectType>
		where ObjectType : IEntityType
	{
		IEntity<ObjectType> ObjectRoot { get; }
		IObjectLinkCollection<ObjectType> ObjectLinks { get; }
	}
}
