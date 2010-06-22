using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence.SQLite
{
    public class CreateCommandFactory
        : CommandFactoryBase, ICreateCommandFactory
    {
        public CreateCommandFactory(SQLiteDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            _database = database;
        }

        private SQLiteDatabase _database;

        #region ICreateCommandFactory Members

        public IDbCommand Create(ITable table)
        {
            if (table == null)
                throw new ArgumentNullException("table");

            var sql = new StringBuilder("CREATE TABLE IF NOT EXISTS ");

            sql.AppendFormat(" {0} (Id INTEGER PRIMARY KEY AUTOINCREMENT", table.Name);

            foreach (var column in table.Columns)
                sql.AppendFormat(", {0} {1} NOT NULL DEFAULT {2}", column.Name, GetDatabaseType(column.Type), GetValueString(column.Default));

            sql.AppendFormat(")");

            return _database.GetCommand(sql.ToString());      
        }

        #endregion
    }
}
