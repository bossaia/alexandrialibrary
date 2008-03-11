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
		
		public override IArtist GetModel(IDictionary<string, object> tuple)
		{
			IArtist model = null;

			if (tuple != null)
			{
				Guid id = (Guid)tuple["Id"];
				string type = (string)tuple["Type"];
				string name = (string)tuple["Name"];
				DateTime beginDate = (DateTime)tuple["BeginDate"];
				DateTime endDate = (DateTime)tuple["EndDate"];
				
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
			}

			return model;
		}

		public override IDictionary<string, object> GetTuple(IArtist model)
		{
			IDictionary<string, object> tuple = new Dictionary<string, object>();

			if (model != null)
			{
				tuple["Id"] = model.Id;
				tuple["Type"] = model.Type;
				tuple["Name"] = model.Name;
				tuple["BeginDate"] = model.BeginDate;
				tuple["EndDate"] = model.EndDate;
			}

			return tuple;	
		}

		public override Field Identifier
		{
			get { return id; }
		}
		#endregion
	}
}
