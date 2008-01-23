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

namespace Telesophy.Alexandria.Persistence
{
	public class Repository : IRepository
	{
		#region Constructors
		public Repository()
		{
		}
		
		public Repository(IEngine engine)
		{
			this.engine = engine;
		}
		#endregion
		
		#region Private Fields
		private IEngine engine;
		private IList<Schema> schemas = new List<Schema>();
		private IDictionary<Type, IMap> maps = new Dictionary<Type, IMap>();
		#endregion
	
		#region Private Methods
		private IMap<Model> GetMap<Model>()
		{
			if (Engine != null && Maps.ContainsKey(typeof(Model)))
			{
				return Maps[typeof(Model)] as IMap<Model>;
			}
			else return null;
		}
		#endregion
	
		#region IRepository Members
		public IEngine Engine
		{
			get { return engine; }
			set { engine = value; }
		}

		public IList<Schema> Schemas
		{
			get { return schemas; }
		}

		public IDictionary<Type, IMap> Maps
		{
			get { return maps; }
		}

		public void Initialize()
		{
			if (Engine != null)
			{
				foreach (Schema schema in Schemas)
				{
					Batch batch = new Batch("initialize records");
					
					batch.Commands.Add(Engine.GetInitializeSchemaCommand(schema));
				
					foreach (Record record in schema.Records)
					{
						batch.Commands.Add(Engine.GetInitializeRecordCommand(record));
					}
					
					IResult result = Engine.Run(batch);
					
					if (!result.Successful)
					{
						Exception error = result.GetError();
						if (error != null)
							throw error;
					}
				}
			}
			else throw new InvalidOperationException("Cannot initialize repository: engine is undefined");
		}

		public Model Lookup<Model>(Guid id)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				IList<Field> primaryKeyFields = map.Record.GetPrimaryKeyFields();
				if (primaryKeyFields.Count > 0)
				{
					Query query = new Query("lookup " + typeof(Model).Name);
					query.Filters.Add(new Filter(primaryKeyFields[0], Operator.EqualTo, id));
					return Lookup<Model>(query);
				}
			}
			
			return default(Model);
		}

		public Model Lookup<Model>(Query query)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				return map.Lookup(query);
			}

			return default(Model);
		}

		public IList<Model> List<Model>(Query query)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				return map.List(query);
			}
			
			return new List<Model>();
		}

		public void Save<Model>(Model model)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				map.Save(model);
			}
		}

		public void Delete<Model>(Model model)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				map.Delete(model);
			}
		}
		#endregion
	}
}
