using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.SQLite
{
	internal struct IndexInfo
	{
		internal IndexInfo(TableInfo table, string name, bool isUnique, IList<ColumnInfo> columns)
		{
			this.table = table;
			this.name = name;
			this.isUnique = isUnique;
			this.columns = columns;
		}
		
		private TableInfo table;
		private string name;
		private bool isUnique;
		private IList<ColumnInfo> columns;
		
		internal TableInfo Table
		{
			get { return table; }
		}
		
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
			if (obj != null)
			{
				if (obj is IndexInfo)
				{
					IndexInfo other = (IndexInfo)obj;
					return (this.ToString() == other.ToString());

					/*
					if (this.table != other.table)
						return false;

					if (this.name != other.name)
						return false;
						
					if (this.isUnique != other.isUnique)
						return false;

					if (this.columns != null)
					{
						if (other.columns != null && this.columns.Count == other.columns.Count)
						{
							for (int i = 1; i <= this.columns.Count; i++)
							{
								if (this.columns[i] != other.columns[i])
									return false;
							}
						}
					}
					else if (other.columns != null)
						return false;

					return true;
					*/
				}
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			StringBuilder indexBuilder = new StringBuilder("CREATE ");
			
			if (isUnique)
				indexBuilder.Append("UNIQUE ");

			indexBuilder.AppendFormat("INDEX IF NOT EXISTS {0} ON {1} (", name, table.Name);
			if (columns != null && columns.Count > 0)
			{
				for(int i=0; i<columns.Count; i++)
				{
					if (i > 0)
						indexBuilder.Append(", ");
						
					indexBuilder.Append(columns[i].Name);
				}
			}
			indexBuilder.Append(")");
			
			return indexBuilder.ToString();
		}
	}
}
