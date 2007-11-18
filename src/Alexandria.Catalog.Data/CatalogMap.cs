using System;
using System.Collections.Generic;
using System.Data;

using Telesophy.Alexandria.Catalog;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Catalog.Data
{
	public class CatalogMap : DataMap<ICatalog>
	{
		public CatalogMap()
		{
			table = new DataTable("Catalog");
			table.Columns.Add("Id", typeof(Uri));
			table.Columns.Add("Name", typeof(string));
			table.Columns.Add("Description", typeof(string));
			table.Columns.Add("UserId", typeof(Uri));
			table.Constraints.Add("Id_pk", table.Columns[0], true);
		}
		
		private DataTable table;
		
		public override System.Data.DataTable Table
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
	}
}
