using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ISubjectLinkType<X> : ILinkType
		where X: IEntityType
	{
		ISubjectLink<X> CreateLink(IEntity<X> subject, IEntity obj);
		ISubjectLink<X> CreateLink(IEntity<X> subject, IEntity obj, int sequence);
	}
}
