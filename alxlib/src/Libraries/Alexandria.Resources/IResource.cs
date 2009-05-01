using System;
using System.Collections.Generic;

namespace Alexandria.Resources
{
	public interface IResource
	{
		Uri Id { get; }
		string Name { get; set; }
		IDictionary<string, Type> GetSchema();
		IEnumerable<T> GetValues<T>(string name);
		void SetValues<T>(string name, IEnumerable<T> values);
	}
}
