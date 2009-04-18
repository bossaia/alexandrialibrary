using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IAggregate : IEntity
	{
		IEnumerable<ILinkType> GetSubjectLinkTypes();
		IEnumerable<ILinkType> GetObjectLinkTypes();
		IEntityCollection GetSubjects(ILinkType type);
		IEntityCollection GetObjects(ILinkType type);
		void SetSubjects(IEntityCollection subjects);
		void SetObjects(IEntityCollection objects);
		IValidationResult Validate();
	}
}
