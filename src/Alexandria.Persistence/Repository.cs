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
		private IList<ISchema> schemas = new List<ISchema>();
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

		public IList<ISchema> Schemas
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
				
					foreach (IRecord record in schema.Records)
					{
						batch.Commands.Add(Engine.GetInitializeRecordCommand(record));
					}
					
					IResult result = Engine.Run(batch);
					
					if (!result.Successful)
					{
						Exception error = result.Error;
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
				FieldCollection identifierFields = map.Record.GetIdentifierFields();
				if (identifierFields.Count > 0)
				{
					Query query = new Query("Lookup " + typeof(Model).Name);
					query.Filters.Add(new Filter(identifierFields[0], Operator.EqualTo, id));
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
				Batch batch = new Batch("Lookup " + map.Type.Name);
				map.AddLookupCommand(batch, CommandTypes.LOOKUP_ROOT, query);
				IResult result = Engine.Run(batch);
				if (result.Successful)
				{
					return Lookup<Model>(result);
				}
				else if (result.Error != null)
				{
					throw result.Error;
				}
			}

			return default(Model);
		}
		
		public Model Lookup<Model>(IResult result)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				return map.Lookup(result);
			}
			
			return default(Model);
		}

		public IList<Model> List<Model>(Query query)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				Batch batch = new Batch("List " + map.Type.Name);
				map.AddLookupCommand(batch, CommandTypes.LOOKUP_ROOT, query);
				IResult result = Engine.Run(batch);
				if (result.Successful)
				{
					return List<Model>(result);
				}
				else if (result.Error != null)
				{
					throw result.Error;
				}
			}
			
			return new List<Model>();
		}

		public IList<Model> List<Model>(IResult result)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				return map.List(result);
			}

			return new List<Model>();
		}

		public void Save<Model>(Model model)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				Batch batch = new Batch("Save " + map.Type.Name);
				map.AddSaveCommand(batch, map.Record.GetTuple(model));
				IResult result = Engine.Run(batch);
				if (!result.Successful && result.Error != null)
				{
					throw result.Error;
				}
			}
		}

		public void Delete<Model>(Model model)
		{
			IMap<Model> map = GetMap<Model>();
			if (map != null)
			{
				Batch batch = new Batch("Delete " + map.Type.Name);
				Query query = new Query("Delete by Id");
				Tuple tuple = map.Record.GetTuple(model);
				foreach (Field field in map.Record.GetIdentifierFields())
				{
					query.Filters.Add(new Filter(field, Operator.EqualTo, tuple.Data[field]));
				}
				
				map.AddDeleteCommand(batch, query);
				IResult result = Engine.Run(batch);
				if (!result.Successful && result.Error != null)
				{
					throw result.Error;
				}
			}
		}
		#endregion
	}
}
