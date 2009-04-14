using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Alexandria.Persistence
{
	public interface IRepository
	{
		T GetOne<T>(IFilter filter);
		IList<T> GetMany<T>(IFilter filter);
		void SaveOne<T>(T value);
		void SaveMany<T>(IEnumerable<T> values);
		void DeleteOne<T>(T value);
		void DeleteMany<T>(IEnumerable<T> values);
		IFilter BuildFilter(FilterType type, string name, Operator op, object value);
		IFilter BuildFilter(FilterType type, string name, Operator op, object value, IFilter child);
	}
}
