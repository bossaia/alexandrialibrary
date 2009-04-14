using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public abstract class SubjectAggregate : Entity //, ISubjectAggregate<T>
	{
	    protected SubjectAggregate(Uri id, IEntityType type)
	        : base(id, type)
	    {
	    }
	}
}
