using System;
using System.Collections.Generic;
using System.Data;

using Telesophy.Alexandria.Catalog;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Catalog.Data
{
	public class UserMap : DataMap<IUser>
	{
		public UserMap()
		{
			table = new DataTable("User");
			table.Columns.Add("Id", typeof(Uri));
			table.Columns.Add("Name", typeof(string));
			table.Columns.Add("ImagePath", typeof(Uri));
			table.Constraints.Add("Id_pk", table.Columns[0], true);
		}
		
		private DataTable table;
	
		public override DataTable Table
		{
			get { return table; }
		}
	}
}
