using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Resources
{
	public interface IMetadataElement
	{
		string Name { get; }
		object Value { get; }
	}
	
	public interface IMetadataElement<T> : IMetadataElement
	{
		new T Value { get; set; }
	}
}
