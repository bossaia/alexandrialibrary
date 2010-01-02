using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Alexandria.Data
{
	public interface IDataMapper<T>
	{
		T LoadOne(IDataReader reader);
		IEnumerable<T> LoadAll(IDataReader reader);
	}
}
