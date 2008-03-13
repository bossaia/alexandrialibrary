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
	public class ArtistEntity : Entity<IArtist>
	{
		#region Constructors
		public ArtistEntity() : base("Artist")
		{
			id = new Field("Id", this, typeof(Guid), true, true);
			type = new Field("Type", this, typeof(string), true);
			name = new Field("Name", this, typeof(string), true);
			beginDate = new Field("BeginDate", this, typeof(DateTime), true);
			endDate = new Field("EndDate", this, typeof(DateTime), true);
			
			Fields.Add(id);
			Fields.Add(type);
			Fields.Add(name);
			Fields.Add(beginDate);
			Fields.Add(endDate);
		}
		#endregion
		
		#region Private Fields
		private Field id;
		private Field type;
		private Field name;
		private Field beginDate;
		private Field endDate;
		#endregion

		#region Overrides
		public override void Initialize(Telesophy.Babel.Persistence.Schema schema)
		{
			base.Initialize(schema);
		}
		
		public override IDictionary<string, IArtist> GetModels(DataTable table)
		{
			IDictionary<string, IArtist> list = new Dictionary<string, IArtist>();

			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow row in table.Rows)
				{
					IArtist model = null;
				
					Guid id = new Guid(row["Id"].ToString());
					string type = row["Type"].ToString();
					string name = row["Name"].ToString();
					DateTime beginDate = DateTime.Parse(row["BeginDate"].ToString());
					DateTime endDate = DateTime.Parse(row["EndDate"].ToString());
					
					switch (type)
					{
						case Constants.ARTIST_TYPE_PERSON:
							model = new Person(id, name, beginDate, endDate);
							break;
						case Constants.ARTIST_TYPE_GROUP:
							model = new Group(id, name, beginDate, endDate);
							break;
						default:
							break;
					}
					
					if (model != null)
						list.Add(model.Id.ToString(), model);
				}
			}

			return list;
		}

		public override Tuple GetTuple(IArtist model)
		{
			throw new NotImplementedException();
		}

		public override void AddDataRow(DataTable table, IArtist model)
		{
			if (table != null && model != null)
			{
				DataRow row = table.NewRow();
				
				row["Id"] = model.Id;
				row["Type"] = model.Type;
				row["Name"] = model.Name;
				row["BeginDate"] = model.BeginDate;
				row["EndDate"] = model.EndDate;
				
				table.Rows.Add(row);
			}
		}

		public override Field Identifier
		{
			get { return id; }
		}
		#endregion
	}
}
