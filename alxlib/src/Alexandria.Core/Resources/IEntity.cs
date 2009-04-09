using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public interface IEntity : IResource
	{
		IEntityType Type { get; }
		ILinkCollection Links { get; }
		void AddLink(ILinkType type, IEntity value);
		void AddLink(ILinkType type, IEntity value, int sequence);
		void RemoveLink(ILink link);
	}
}
