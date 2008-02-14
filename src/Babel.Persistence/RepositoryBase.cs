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

namespace Telesophy.Babel.Persistence
{
	public abstract class RepositoryBase : IRepository
	{
		#region Constructors
		#endregion
		
		#region Private Fields
		private IEngine engine;
		private INamedItemCollection<ISchema> schemas = new NamedItemCollection<ISchema>();
		#endregion
	
		#region Protected Methods
		protected virtual IMap<Model> GetMap<Model>()
		{
			return GetMap<Model>(typeof(Model).Name);
		}
		
		protected virtual IMap<Model> GetMap<Model>(string name)
		{
			foreach (ISchema schema in schemas)
			{
				if (schema.Maps.Contains(name))
				{
					return (IMap<Model>)schema.Maps[name];
				}
			}
			
			return null;
		}
		#endregion
	
		#region IRepository Members
		public IEngine Engine
		{
			get { return engine; }
			set { engine = value; }
		}

		public INamedItemCollection<ISchema> Schemas
		{
			get { return schemas; }
		}

		public virtual void Initialize()
		{
			if (engine != null)
			{
				foreach (ISchema schema in schemas)
				{
					engine.Initialize(schema);
				}
			}
			else throw new InvalidOperationException("Could not initialize Schemas because the Engine is not defined");
		}

		public virtual Model Lookup<Model>(Query query)
		{
			IEnumerable<Model> list = List<Model>(query);
			if (list != null)
			{
				return list.FirstOrDefault<Model>();
			}
			
			return default(Model);
		}

		public virtual IEnumerable<Model> List<Model>(Query query)
		{
			IEnumerable<Model> models = new List<Model>();
		
			if (engine != null)
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					IResult result = engine.Lookup(map, query);
					models = map.GetModels(result.Table);
					map.LoadChildren(models, result);
				}
			}
			
			return models;
		}

		public virtual void Save<Model>(IEnumerable<Model> models)
		{
			if (engine != null)
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					engine.Save(map, models);
				}
			}
		}

		public virtual void Delete<Model>(IEnumerable<Model> models)
		{
			if (engine != null)
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					engine.Delete(map, models);
				}
			}
		}
		#endregion
	}
}
