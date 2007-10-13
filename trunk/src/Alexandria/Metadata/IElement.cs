using System;
using System.Collections.Generic;

namespace Alexandria.Metadata
{
	public interface IElement
	{
		IIdentifier Identifier { get; }
		object Value { get; set; }
		IElement Parent { get; set; }
		IList<IElement> Children { get; }
	}

	public interface IElement<T> : IElement, IEquatable<T>
	{
		T Value { get; set; }
	}
}