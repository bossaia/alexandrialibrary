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
		private IDictionary<Type, IMap> maps = new Dictionary<Type, IMap>();
		#endregion
	
		#region Private Methods
		private Constraint GetIdentifierConstraint(IRecord record)
		{
			if (record != null)
			{
				foreach(Constraint constraint in record.Constraints.Values)
				{
					if (constraint.Type == ConstraintType.Identifier)
						return constraint;
				}
			}
			
			return default(Constraint);
		}
		
		private Model Lookup<Model>(IMap<Model> map, IList<Filter> filters)
		{
			if (map != null)
			{
				ICommand<Model> command = engine.CreateCommand<Model>(map, filters, CommandType.Lookup);
				if (command != null)
				{
					foreach(Relationship relationship in map.Record.Relationships.Values)
					{
						if (Maps.ContainsKey(relationship.DataType))
						{
							IMap additionalMap = Maps[relationship.DataType];
							if (additionalMap != null)
							{
								//Field additionalField = new Field(null, relationship.ParentFieldName, typeof(Guid));
								//IList<Filter> additionalFilters = new List<Filter>();
								//additionalFilters.Add(new Filter(additionalField, Operator.EqualTo, filters[0].Value));
								//Command additionalCommand = Engine.CreateCommand(additionalMap, 
							}
						}
					}
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

		public IDictionary<Type, IMap> Maps
		{
			get { return maps; }
		}

		public void AddMap(IMap map)
		{
			if (map != null && !Maps.ContainsKey(map.DataType))
			{
				Maps.Add(map.DataType, map);
			}
		}

		public IMap<Model> GetMap<Model>()
		{
			if (Maps.ContainsKey(typeof(Model)))
			{
				return Maps[typeof(Model)] as IMap<Model>; 
			}
			else return null;
		}

		public void Initialize()
		{
		}

		public Model Lookup<Model>(Guid id)
		{
			if (Engine != null && Maps.ContainsKey(typeof(Model)))
			{
				IMap<Model> map = GetMap<Model>();
				if (map != null)
				{
					Constraint idConstraint = GetIdentifierConstraint(map.Record);
					if (idConstraint.Fields != null && idConstraint.Fields.Count > 0)
					{
						IList<Filter> filters = new List<Filter>();
						filters.Add(new Filter(idConstraint.Fields[0], Operator.EqualTo, id));
						Model model = Lookup(map, filters);
					}
				}
			}
			
			return default(Model);
		}

		public Model Lookup<Model>(IList<Filter> filters)
		{
			return default(Model);
		}

		public IList<Model> List<Model>(IList<Filter> filters)
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
