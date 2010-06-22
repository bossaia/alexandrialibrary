using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence.SQLite
{
    public abstract class CommandFactoryBase
    {
        protected string GetDatabaseType(Type type)
        {
            switch (type.Name)
            {
                case "Boolean":
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                    return "INTEGER";
                case "Float":
                case "Double":
                case "Decimal":
                    return "REAL";
                default:
                    return "TEXT";
            }
        }

        protected string GetValueString(object value)
        {
            if (value == null)
                return "''";

            if (value.GetType() == typeof(string))
                return string.Format("'{0}'", value);
            else if (value.GetType() == typeof(DateTime))
                return string.Format("'{0:d}'", value);
            else
                return value.ToString();
        }
    }
}
