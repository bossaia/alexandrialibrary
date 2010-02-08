using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;

namespace Gnosis.Alexandria.Repositories
{
	public abstract class DatabaseRepository
	{
		protected string GetConnectionString()
		{
			return "Data Source=Catalog.db;UTF8Encoding=True;Version=3";
		}

		protected SQLiteConnection GetConnection()
		{
			return new SQLiteConnection(GetConnectionString());
		}


	}
}
