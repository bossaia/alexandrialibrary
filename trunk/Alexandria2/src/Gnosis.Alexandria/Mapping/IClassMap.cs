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
		string GetInitializeCommandText();
		string GetSaveCommandText(T entity);
		string GetDeleteCommandText(T entity);
		IEnumerable<T> Load(IDataReader reader);
	}
}
