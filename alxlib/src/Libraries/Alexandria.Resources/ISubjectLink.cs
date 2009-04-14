using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ISubjectLink<X> : ILink
		where X : IEntityType
	{
		new ISubjectLinkType<X> Type { get; }
		new IEntity<X> Subject { get; }
	}
}
