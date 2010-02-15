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
		IEnumerable<string> GetSaveCommandTexts(T entity);
		IEnumerable<string> GetDeleteCommandTexts(long id);
		IList<T> Load(IDataReader reader);
	}
}
