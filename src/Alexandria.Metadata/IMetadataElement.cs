using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface IMetadataElement
	{
		IMetadataIdentifier Id { get; }
		object Value { get; }
	}
	
	public interface IMetadataElement<T> : IMetadataElement
	{
		T Value { get; set; }
	}
}
