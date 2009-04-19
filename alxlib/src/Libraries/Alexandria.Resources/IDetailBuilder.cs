using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IDetailBuilder
	{
		string Name { get; }
		Uri DetailType { get; }
		Uri ValueType { get; }
		int Cardinality { get; }
		bool IsRequired { get; }
		void LoadData(string value);
		void LoadData(IEnumerable<string> values);
	}

	public interface IDetailBuilder<T>
	{
		T GetOne();
		IEnumerable<T> GetMany();
	}
}
