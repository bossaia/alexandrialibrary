using System;

namespace Alexandria.Resources
{
	public interface IValue : IResource
	{
		IValueType Type { get; }
		object Data { get; set; }
	}

	public interface IValue<T> : IValue
	{
		new T Data { get; set; }
	}
}
