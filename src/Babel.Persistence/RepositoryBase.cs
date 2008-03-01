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

namespace Telesophy.Babel.Persistence
{
	public abstract class RepositoryBase : IRepository
	{
		#region Constructors
		protected RepositoryBase(IEngine engine, ISchema schema)
		{
			this.engine = engine;
			this.schema = schema;
		}
		#endregion
		
		#region Private Fields
		private IEngine engine;
		private ISchema schema;
		private int defaultDepth = 1;
		#endregion
	
		#region Protected Methods
		protected virtual IMap<Model> GetMap<Model>()
		{
			Type key = typeof(Model);
			if (schema.Maps.Contains(key))
			{
				return schema.Maps[key] as IMap<Model>;
			}
			
			return null;
		}
		
		protected virtual IQuery GetQuery(Type type, IExpression filter)
		{
			IQuery query = new Query("Lookup " + type.Name);
			query.Filters.Add(filter);
			return query;
		}
		#endregion
	
		#region IRepository Members
		public IEngine Engine
		{
			get { return engine; }
		}

		public ISchema Schema
		{
			get { return schema; }
		}

		public int DefaultDepth
		{
			get { return defaultDepth; }
			set { defaultDepth = value; }
		}

		public virtual void Initialize()
		{
			if (engine != null)
			{
				//engine.Initialize(schema);
			}
			else throw new InvalidOperationException("Could not initialize the Schema because the Engine is not defined");
		}
		
		public virtual IEnumerable<Model> Lookup<Model>(IExpression filter)
		{
			return Lookup<Model>(filter, DefaultDepth);
		}
		
		public virtual IEnumerable<Model> Lookup<Model>(IExpression filter, int depth)
		{
			IEnumerable<Model> models = new List<Model>();
		
			if (engine != null)
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					IQuery query = GetQuery(typeof(Model), filter);
					map.BuildQuery(query, 0, depth);
					//models = engine.Lookup(map, query);
				}
			}
			
			return models;
		}

		public virtual void Save<Model>(IEnumerable<Model> models)
		{
			Save<Model>(models, DefaultDepth);
		}

		public virtual void Save<Model>(IEnumerable<Model> models, int depth)
		{
			if (engine != null)
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					DataSet dataSet = new DataSet("Save " + typeof(Model).Name);
					map.BuildDataSet(dataSet, 0, depth);
					//engine.Save(map, dataSet);
				}
			}
		}

		public virtual void Delete<Model>(IEnumerable<Model> models)
		{
			Delete<Model>(models, DefaultDepth);
		}

		public virtual void Delete<Model>(IEnumerable<Model> models, int depth)
		{
			if (engine != null)
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					DataSet dataSet = new DataSet("Delete " + typeof(Model).Name);
					map.BuildDataSet(dataSet, 0, depth);
					//engine.Delete(map, dataSet);
				}
			}
		}
		#endregion
	}
}
