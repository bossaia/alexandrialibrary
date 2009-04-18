using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class EntityType : Resource, IEntityType
	{
		public EntityType(Uri id)
			: base(id)
		{
		}

		#region IEntityType Members

		public IValidationResult EntityIsValid(IEntity entity)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
