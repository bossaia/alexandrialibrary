using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITag
	{
		ITagFormat Format { get; }
		IIdentifier Id { get; }
		IMetadataMap Map { get; }
	}
}
