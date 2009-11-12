using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ISpecification
	{
		bool IsSatisfiedBy(IResource resource);
	}
}
