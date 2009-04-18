using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class LinkType : Resource, ILinkType
	{
		public LinkType(Uri id)
			: base(id)
		{
		}

		#region ILinkType Members

		public IValidationResult LinkIsValid(IResource subject, IResource obj)
		{
			throw new NotImplementedException();
		}

		public IValidationResult SubjectsAreValid(IResource root, IEnumerable<IResource> subjects)
		{
			throw new NotImplementedException();
		}

		public IValidationResult ObjectsAreValid(IResource root, IEnumerable<IResource> objects)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
