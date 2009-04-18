using System;

namespace Alexandria.Resources
{
	public interface IEntityType : IResource
	{
		IValidationResult EntityIsValid(IEntity entity);
	}
}
