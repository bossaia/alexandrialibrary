using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ISubjectLinkType<T> : ILinkType
		where T: IEntityType
	{
		T SubjectType { get; }
	}
}
