using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IEntityCollection : IList<IEntity>
	{
		IEntity Root { get; }
		ILinkType Type { get; }
	}
}
