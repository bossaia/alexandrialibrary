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
	public abstract class BaseSimpleDataMap<Model> : BaseDataMap
		where Model: IModel
	{
		#region Constructors
		public BaseSimpleDataMap()
		{
		}
		#endregion
		
		#region Protected Methods
		protected virtual IList<Model> GetList(string filter)
		{
			IList<Model> list = new List<Model>();

			if (Engine != null)
			{
				if (!string.IsNullOrEmpty(filter))
					Engine.FillTable(Table, filter);
				else Engine.FillTable(Table, default(Guid));


				if (Table.Rows.Count > 0)
				{
					foreach (DataRow row in Table.Rows)
					{
						Model model = GetModelFromRow(row);
						list.Add(model);
					}

					Table.Rows.Clear();
				}
			}

			return list;
		}

		protected abstract Model GetModelFromRow(DataRow row);

		protected abstract DataRow GetRowFromModel(Model model);
		#endregion
		
		#region Public Methods
		public virtual Model LookupModel(Guid id)
		{
			Model model = default(Model);

			if (Engine != null)
			{
				Engine.FillTable(Table, id);
				if (Table.Rows.Count > 0)
				{
					model = GetModelFromRow(Table.Rows[0]);
					Table.Rows[0].Delete();
				}
			}

			return model;
		}

		public virtual IList<Model> ListModels()
		{
			return GetList(null);
		}

		public virtual IList<Model> ListModels(string filter)
		{
			return GetList(filter);
		}

		public virtual void SaveModel(Model model)
		{
			if (Engine != null && model != null)
			{
				DataRow row = GetRowFromModel(model);
				Engine.SaveRow(row);
				row.Delete();
			}
		}

		public virtual void DeleteModel(Model model)
		{
			if (Engine != null && model != null)
			{
				DataRow row = GetRowFromModel(model);
				Engine.DeleteRow(row);
				row.Delete();
			}
		}
		#endregion
	}
}
