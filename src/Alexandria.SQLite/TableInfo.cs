using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.SQLite
{
	internal struct TableInfo
	{
		internal TableInfo(string name, IDictionary<int, ColumnInfo> columns)
		{
			this.name = name;
			this.columns = columns;
			this.indices = new Dictionary<string, IndexInfo>();
		}
		
		private string name;
		private IDictionary<int, ColumnInfo> columns;
		private IDictionary<string, IndexInfo> indices;
		
		private string GetColumnList()
		{
			StringBuilder columnList = new StringBuilder(string.Empty);
			if (columns != null)
			{
				for (int i = 1; i <= columns.Count; i++)
				{
					if (i > 1) columnList.Append(", ");
					columnList.Append(columns[i].ToString());
				}
			}
			return columnList.ToString();
		}
		
		public string Name
		{
			get { return name; }
		}
		
		public IDictionary<int, ColumnInfo> Columns
		{
			get { return columns; }
		}

		public IDictionary<string, IndexInfo> Indices
		{
			get { return indices; }
		}

		public ColumnInfo GetColumnByName(string name)
		{			
			foreach(ColumnInfo column in columns.Values)
				if (column.Name == name) return column;
				
			return default(ColumnInfo);
		}		

		public static bool operator !=(TableInfo t1, TableInfo t2)
		{
			return !t1.Equals(t2);
		}

		public static bool operator ==(TableInfo t1, TableInfo t2)
		{
			return t1.Equals(t2);
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				if (obj is TableInfo)
				{
					TableInfo other = (TableInfo)obj;
					return (this.ToString() == other.ToString());
					
					/*
					if (this.name != other.name)
						return false;
					
					if (this.columns != null)
					{
						if (other.columns != null && this.columns.Count == other.columns.Count)
						{
							for(int i=1; i<=this.columns.Count; i++)
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
				else return false;
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public string GetCreateTempTableCommandText(string tableName)
		{
			string createFormat = "CREATE TEMP TABLE IF NOT EXISTS {0} ({1})";
			return string.Format(createFormat, tableName, GetColumnList());
		}
		
		public string GetInsertCommandText(string tableName)
		{
			string insertFormat = "INSERT INTO {0} ({1}) SELECT {1} FROM {2}";
			return string.Format(insertFormat, tableName, GetColumnList(), GetColumnList(), Name);
		}

		public override string ToString()
		{
			string createFormat = "CREATE TABLE IF NOT EXISTS {0} ({1})";			
			return string.Format(createFormat, name, GetColumnList());
		}
	}
}
