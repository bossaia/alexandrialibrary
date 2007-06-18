using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Persistance;

namespace Alexandria.SQLite
{
	internal class MappingStrategy : IMappingStrategy
	{
		#region Constructor
		internal MappingStrategy(SQLiteDataProvider provider, MappingFunction function) : this(provider, function, null)
		{
		}

		internal MappingStrategy(SQLiteDataProvider provider, MappingFunction function, IPersistant record)
		{
			this.provider = provider;
			this.function = function;
			this.record = record;
		}
		#endregion
	
		#region Private Fields
		private SQLiteDataProvider provider;
		private MappingFunction function;
		private IPersistant record;
		#endregion
	
		#region IMappingStrategy Members
		public SQLiteDataProvider Provider
		{
			get { return provider; }
		}

		public MappingFunction Function
		{
			get { return function; }
		}
		#endregion
	}
}
