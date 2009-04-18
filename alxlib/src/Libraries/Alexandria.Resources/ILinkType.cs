using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ILinkType : IResource
	{
		IValidationResult LinkIsValid(IResource subject, IResource obj);
		IValidationResult SubjectsAreValid(IResource root, IEnumerable<IResource> subjects);
		IValidationResult ObjectsAreValid(IResource root, IEnumerable<IResource> objects);
	}
}
