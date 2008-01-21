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
		private Constraint GetIdentifierConstraint(Record record)
		{
			if (record.Name != null)
			{
				foreach(Constraint constraint in record.Constraints)
				{
					if (constraint.Type == ConstraintType.Identifier)
						return constraint;
				}
			}
			
			return default(Constraint);
		}
		
		private Model Lookup<Model>(IMap map, Query query)
		{
			if (map != null)
			{
				ICommand command = engine.GetLookupCommand(query);
				//CreateCommand<Model>(map, filters, CommandFunction.Lookup);
				if (command != null)
				{
					//foreach(Relationship relationship in map.Record.Relationships.Values)
					//{
						//if (Maps.ContainsKey(relationship.DataType))
						//{
							//IMap additionalMap = Maps[relationship.DataType];
							//if (additionalMap != null)
							//{
								//Field additionalField = new Field(null, relationship.ParentFieldName, typeof(Guid));
								//IList<Filter> additionalFilters = new List<Filter>();
								//additionalFilters.Add(new Filter(additionalField, Operator.EqualTo, filters[0].Value));
								//Command additionalCommand = Engine.CreateCommand(additionalMap, 
							//}
						//}
					//}
				}
			}
			
			return default(Model);
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
		}

		public Model Lookup<Model>(Guid id)
		{
			if (Engine != null && Maps.ContainsKey(typeof(Model)))
			{
				IMap map = Maps[typeof(Model)];
				if (map != null)
				{
					if (map.Record.PrimaryKeyFields.Count > 0)
					{
						Query query = new Query("lookup " + typeof(Model).Name);
						query.Filters.Add(new Filter(map.Record.PrimaryKeyFields[0], Operator.EqualTo, id));
						Model model = Lookup<Model>(map, query);
					}
				}
			}
			
			return default(Model);
		}

		public Model Lookup<Model>(Query query)
		{
			return default(Model);
		}

		public IList<Model> List<Model>(Query query)
		{
			return null;
		}

		public void Save<Model>(Model model)
		{
		}

		public void Delete<Model>(Model model)
		{
		}
		#endregion
	}
}
