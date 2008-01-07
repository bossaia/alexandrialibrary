#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
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
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetDataMap : BaseSimpleDataMap<IMediaSet>
	{
		#region Constructors
		public MediaSetDataMap()
		{
			Table = new DataTable("MediaSet");

			Table.Columns.Add("Id", typeof(Guid));
			Table.Columns.Add("Source", typeof(string));
			Table.Columns.Add("Type", typeof(string));
			Table.Columns.Add("Number", typeof(int));
			Table.Columns.Add("Title", typeof(string));
			Table.Columns.Add("Artist", typeof(string));
			Table.Columns.Add("Date", typeof(DateTime));
			Table.Columns.Add("Path", typeof(Uri));
			Table.Constraints.Add(new UniqueConstraint(Table.Columns["Id"], true));
			Table.Constraints.Add(new UniqueConstraint(Table.Columns["Path"]));
		}
		#endregion
		
		#region Protected Methods
		protected override IMediaSet GetModelFromRow(DataRow row)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		protected override DataRow GetRowFromModel(IMediaSet model)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
