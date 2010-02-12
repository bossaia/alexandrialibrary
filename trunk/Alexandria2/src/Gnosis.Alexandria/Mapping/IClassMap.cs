using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Mapping
{
	public interface IClassMap<T>
		where T : IEntity
	{
		string Key { get; }
		string Table { get; }

		string GetValue(object value);
		string GetInitializeCommandText();
		string GetSaveCommandText(T entity);
		string GetDeleteCommandText(long id);
		IList<T> Load(IDataReader reader);
	}
}
