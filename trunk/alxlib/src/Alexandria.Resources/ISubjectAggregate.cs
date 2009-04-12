using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ISubjectAggregate<SubjectType>
		where SubjectType: IEntityType
	{
		IEntity<SubjectType> SubjectRoot { get; }
		ISubjectLinkCollection<SubjectType> SubjectLinks { get; }
	}
}
