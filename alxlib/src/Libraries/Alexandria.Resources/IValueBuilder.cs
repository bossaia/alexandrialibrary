using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IValueBuilder
	{
		string Name { get; }
		Uri Type { get; }
		int Cardinality { get; }
		bool IsRequired { get; }
		void LoadData(string value);
		void LoadData(IEnumerable<string> values);
	}

	public interface IValueBuilder<T>
	{
		T GetOne();
		IEnumerable<T> GetMany();
	}
}
