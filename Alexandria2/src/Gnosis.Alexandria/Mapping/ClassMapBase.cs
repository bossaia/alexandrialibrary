using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Mapping
{
	public abstract class ClassMapBase<T>
		: IClassMap<T>
		where T : IEntity
	{
		protected ClassMapBase(IContext context, string table)
		{
			_context = context;
			_table = table;
		}

		private const string COL_ID = "Id";
		private const string TYPE_INTEGER = "INTEGER";
		private const string TYPE_REAL = "REAL";
		private const string TYPE_TEXT = "TEXT";

		private readonly IContext _context;
		private readonly string _table;

		private string GetDataType(Type type)
		{
			switch (type.Name)
			{
				case "Byte":
				case "SByte":
				case "Int16":
				case "UInt16":
				case "Int32":
				case "UInt32":
				case "Int64":
				case "UInt64":
					return TYPE_INTEGER;
				case "Float":
				case "Double":
				case "Decimal":
					return TYPE_REAL;
				default:
					return TYPE_TEXT;
			}
		}

		protected IDictionary<string, Type> Columns = new Dictionary<string, Type>();

		protected IContext Context
		{
			get { return _context; }
		}

		protected string Table
		{
			get { return _table; }
		}

		protected string GetValue(object value)
		{
			if (value == null)
				return "''";

			if (value.GetType() == typeof(DateTime))
				return string.Format("{0:yyyy-MM-dd}", value);

			var type = GetDataType(value.GetType());

			switch (type)
			{
				case TYPE_INTEGER:
				case TYPE_REAL:
					return value.ToString();
				case TYPE_TEXT:
				default:
					return string.Format("'{0}'", value);
			}
		}

		protected abstract object GetValue(T entity, string column);
		protected abstract void SetValue(T entity, string column, object value);

		#region IClassMap<T> Members

		public virtual string GetInitializeCommandText()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("CREATE TABLE IF NOT EXISTS {0} ({1} {2} PRIMARY KEY AUTOINCREMENT", Table, COL_ID, GetDataType(typeof(int)));

			foreach (KeyValuePair<string, Type> column in Columns)
			{
				builder.AppendFormat(", {0} {1} NOT NULL", column.Key, GetDataType(column.Value));
			}

			builder.Append(")");

			return builder.ToString();
		}

		public virtual string GetSaveCommandText(T entity)
		{
			var cols = new StringBuilder();
			cols.AppendFormat("REPLACE INTO {0} ({1}", Table, COL_ID);
			var vals = new StringBuilder();
			vals.Append(") VALUES (");

			vals.Append(entity.Id > 0 ? GetValue(entity.Id) : "NULL");

			foreach (KeyValuePair<string, Type> column in Columns)
			{
				cols.AppendFormat(", {0}", column.Key);
				vals.AppendFormat(", {0}", GetValue(GetValue(entity, column.Key)));
			}
			
			vals.Append(")");

			return cols.ToString() + vals.ToString();
		}

		public virtual string GetDeleteCommandText(T entity)
		{
			var builder = new StringBuilder();
			builder.AppendFormat("DELETE FROM {0} WHERE {1} = {2}", Table, COL_ID, GetValue(entity.Id));

			return builder.ToString();
		}

		public virtual IEnumerable<T> Load(IDataReader reader)
		{
			List<T> list = new List<T>();

			if (reader != null)
			{
				while (reader.Read())
				{
					long id = Convert.ToInt64(reader[COL_ID]);

					T entity = _context.Get<T>(id);

					foreach (KeyValuePair<string, Type> column in Columns)
					{
						SetValue(entity, column.Key, reader[column.Key]);
					}

					list.Add(entity);
				}
			}
			return list;
		}

		#endregion
	}
}
