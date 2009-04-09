using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public interface IEntityType : IResource
	{
		string IdMask { get; }
		string NameMask { get; }
	}
}
