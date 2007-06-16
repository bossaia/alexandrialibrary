using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITag
	{
		ITagFormat Format { get; }
		IList<IMetadata> Metadata { get; }
	}
}
