using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence.SQLite
{
    public class SQLiteDatabase
        : IDatabase
    {
        public SQLiteDatabase(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            _name = name;
        }

        private string _name;

        private string GetConnectionString()
        {
            return string.Format("Data Source={0}.db", _name);
        }

        #region IDatabase Members

        public string Name
        {
            get { return _name; }
        }

        public IDbCommand GetCommand(string text)
        {
            var command = GetConnection().CreateCommand();
            command.CommandText = text;
            return command;
        }

        public IDbConnection GetConnection()
        {
            return new SQLiteConnection(GetConnectionString());
        }

        #endregion
    }
}
