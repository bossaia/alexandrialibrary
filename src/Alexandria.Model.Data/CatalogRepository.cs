#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Telesophy.Babel.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class CatalogRepository : RepositoryBase
	{
		#region Constuctors
		public CatalogRepository(IEngine engine) : base(engine, new DataSet("Catalog"))
		{
		}
		#endregion
		
		#region Private Fields
		#endregion
		
		#region Private Methods
		private DataTable GetMediaSetTable()
		{
			DataTable table = new DataTable("MediaSet");
			table.Columns.Add("Id", typeof(Guid));
			table.Columns.Add("Source", typeof(string));
			table.Columns.Add("Type", typeof(string));
			table.Columns.Add("Number", typeof(int));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("Artist", typeof(string));
			table.Columns.Add("Date", typeof(DateTime));
			table.Columns.Add("Format", typeof(string));
			table.Columns.Add("Path", typeof(Uri));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Id"], true));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Path"]));
			return table;
		}
		
		private DataTable GetMediaItemTable()
		{
			DataTable table = new DataTable("MediaItem");
			table.Columns.Add("Id", typeof(Guid));
			table.Columns.Add("Source", typeof(string));
			table.Columns.Add("Type", typeof(string));
			table.Columns.Add("Number", typeof(int));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("Artist", typeof(string));
			table.Columns.Add("Album", typeof(string));
			table.Columns.Add("Duration", typeof(TimeSpan));
			table.Columns.Add("Date", typeof(DateTime));
			table.Columns.Add("Format", typeof(string));
			table.Columns.Add("Path", typeof(Uri));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Id"], true));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Path"]));
			return table;
		}
		
		private DataTable GetMediaSetMediaItemTable()
		{
			DataTable table = new DataTable("MediaSetMediaItem");
			table.Columns.Add("ParentId", typeof(Guid));
			table.Columns.Add("ChildId", typeof(Guid));
			table.Constraints.Add(new UniqueConstraint(new DataColumn[]{table.Columns["ParentId"], table.Columns["ChildId"]}, true));
			return table;
		}
		
		private DataRelation GetMediaSetMediaItemParentRelation(DataTable mediaSetTable, DataTable mediaSetMediaItemTable)
		{
			DataRelation relation = new DataRelation("MediaSetMediaItemParent", mediaSetTable.Columns["Id"], mediaSetMediaItemTable.Columns["ParentId"]);
			return relation;
		}

		private DataRelation GetMediaSetMediaItemChildRelation(DataTable mediaSetMediaItemTable, DataTable mediaItemTable)
		{
			DataRelation relation = new DataRelation("MediaSetMediaItemChild", mediaSetMediaItemTable.Columns["ChildId"], mediaItemTable.Columns["Id"]);
			return relation;
		}
		#endregion
		
		#region Public Overrides
		public override void Initialize()
		{
			DataTable mediaSetTable = GetMediaSetTable();
			DataTable mediaItemTable = GetMediaItemTable();
			DataTable mediaSetMediaItemTable = GetMediaSetMediaItemTable();
			DataRelation mediaSetMediaItemParentRelation = GetMediaSetMediaItemParentRelation(mediaSetTable, mediaSetMediaItemTable);
			DataRelation mediaSetMediaItemChildRelation = GetMediaSetMediaItemChildRelation(mediaSetMediaItemTable, mediaItemTable);
			
			DataSet.Tables.Add(mediaSetTable);
			DataSet.Tables.Add(mediaItemTable);
			DataSet.Tables.Add(mediaSetMediaItemTable);
			DataSet.Relations.Add(mediaSetMediaItemParentRelation);
			DataSet.Relations.Add(mediaSetMediaItemChildRelation);
			
			Engine.Initialize(DataSet);
		}
		#endregion
	}
}
