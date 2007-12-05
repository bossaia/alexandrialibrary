using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using Alexandria;
using Alexandria.Persistence;
using Alexandria.SQLite;

namespace Alexandria.Client.Controllers
{
	public class PersistenceController
	{
		public PersistenceController()
		{
			engine = new SQLiteEngine();
		}
		
		private SQLiteEngine engine;
	}
}
