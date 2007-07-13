using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.SQLite
{
	internal struct IndexInfo
	{
		internal IndexInfo(string name, bool isUnique, IList<ColumnInfo> columns)
		{
			this.name = name;
			this.isUnique = isUnique;
			this.columns = columns;
		}
		
		private string name;
		private bool isUnique;
		private IList<ColumnInfo> columns;
		
		internal string Name
		{
			get { return name; }
		}
		
		internal bool IsUnique
		{
			get { return isUnique; }
		}
		
		internal IList<ColumnInfo> Columns
		{
			get { return columns; }
		}

		public static bool operator ==(IndexInfo i1, IndexInfo i2)
		{
			return i1.Equals(i2);
		}
		
		public static bool operator !=(IndexInfo i1, IndexInfo i2)
		{
			return !i1.Equals(i2);
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("CREATE INDEX {0}", name);
		}
	}
}
