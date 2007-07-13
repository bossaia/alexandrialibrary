using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Alexandria.SQLite
{
	internal struct ColumnInfo
	{
		internal ColumnInfo(int ordinal, string name, TypeAffinity type, bool isRequired, object defaultValue, bool isPrimaryKey)
		{
			this.ordinal = ordinal;
			this.name = name;
			this.type = type;
			this.isRequired = isRequired;
			this.defaultValue = defaultValue;
			this.isPrimaryKey = isPrimaryKey;
		}
		
		private int ordinal;
		private string name;
		private TypeAffinity type;
		private bool isRequired;
		private object defaultValue;
		private bool isPrimaryKey;
		
		internal int Ordinal
		{
			get { return ordinal; }
		}
		
		internal string Name
		{
			get { return name; }
		}
		
		internal TypeAffinity Type
		{
			get { return type; }
		}
		
		internal bool IsRequired
		{
			get { return isRequired; }
		}
		
		internal object DefaultValue
		{
			get { return defaultValue; }
		}
		
		internal bool IsPrimaryKey
		{
			get { return isPrimaryKey; }
		}
		
		public static bool operator !=(ColumnInfo c1, ColumnInfo c2)
		{
			return !c1.Equals(c2);
		}
		
		public static bool operator ==(ColumnInfo c1, ColumnInfo c2)
		{
			return c1.Equals(c2);
		}

		public override bool Equals(object obj)
		{
			if (obj != null)
			{
				if (obj is ColumnInfo)
				{
					ColumnInfo other = (ColumnInfo)obj;
					
					if (this.ordinal != other.ordinal) return false;
					if (this.name != other.name) return false;
					if (this.type != other.type) return false;
					if (this.isRequired != other.isRequired) return false;
					if (this.defaultValue != other.defaultValue) return false;
					if (this.isPrimaryKey != other.isPrimaryKey) return false;
					
					return true;
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
			StringBuilder columnBuilder = new StringBuilder(name);
			
			columnBuilder.AppendFormat(" {0}", SQLitePersistenceMechanism.GetSQLiteFieldType(type));
			
			string fieldRequired = SQLitePersistenceMechanism.GetSQLiteFieldRequired(isRequired);
			if (!string.IsNullOrEmpty(fieldRequired))
				columnBuilder.AppendFormat(" {0)", fieldRequired);
			
			string fieldDefault = SQLitePersistenceMechanism.GetSQLiteFieldDefault(defaultValue);
			if (!string.IsNullOrEmpty(fieldDefault))
				columnBuilder.AppendFormat(" {0}", fieldDefault);

			return columnBuilder.ToString();
		}
	}
}
