using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abraxas;

namespace Alexandria.Core
{
	public interface ITagged
	{
		IEntityMap<ITag> Tags { get; }
		void AddTag(ITag tag);
		void RemoveTag(ITag tag);
	}
}
