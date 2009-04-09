using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public interface ILink : IResource
	{
		ILinkType Type { get; }
		IEntity Subject { get; }
		IEntity Value { get; set; }
		int Sequence { get; set; }
	}
}
