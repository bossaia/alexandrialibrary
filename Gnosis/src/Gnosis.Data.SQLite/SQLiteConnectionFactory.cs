﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Data.SQLite
{
    public class SQLiteConnectionFactory
        : IConnectionFactory
    {
        public IDbConnection Create(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException("connectionString");

            return new System.Data.SQLite.SQLiteConnection(connectionString);
        }
    }
}
