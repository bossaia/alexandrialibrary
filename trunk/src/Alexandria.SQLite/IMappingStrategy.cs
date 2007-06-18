using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Persistance;

namespace Alexandria.SQLite
{
	internal interface IMappingStrategy
	{
		SQLiteDataProvider Provider { get; }
		MappingFunction Function { get; }
		MappingType Type { get; }
		IPersistant Record { get; set; }
		IList<IPersistant> Records { get; set; }
	}
}
