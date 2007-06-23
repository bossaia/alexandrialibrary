using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Persistence;

namespace Alexandria.SQLite
{
	internal interface IMappingStrategy
	{
		SQLiteDataProvider Provider { get; }
		MappingFunction Function { get; }
		MappingType Type { get; }
		IPersistent Record { get; set; }
		IList<IPersistent> Records { get; set; }
	}
}
