using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IContext
	{
		IMetadataSet Settings { get; }
		IResource GetResource(Uri id);
	}
}
