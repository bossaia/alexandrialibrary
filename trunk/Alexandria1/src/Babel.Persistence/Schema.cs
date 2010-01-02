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

namespace Telesophy.Babel.Persistence
{
	public class Schema : NamedItem, ISchema
	{
		#region Constructor
		public Schema(string name, string ns) : base(name)
		{
			this.ns = ns;
		}
		#endregion
		
		#region Private Fields
		private string ns;
		private EntityCollection entities = new EntityCollection();
		private NamedItemCollection<Aggregate> aggregates = new NamedItemCollection<Aggregate>();
		#endregion
		
		#region ISchema Members
		public string Namespace
		{
			get { return ns; }
		}
		
		public EntityCollection Entities
		{
			get { return entities; }
		}
		
		public NamedItemCollection<Aggregate> Aggregates
		{
			get { return aggregates; }
		}
		
		public virtual void Initialize()
		{
			foreach (Entity entity in Entities)
			{
				entity.Initialize(this);
			}
			
			foreach (Aggregate aggregate in Aggregates)
			{
				aggregate.Initialize(this);
			}
		}
		
		public Entity<T> GetEntity<T>()
		{
			Type key = typeof(T);
			
			if (Entities.Contains(key))
				return (Entity<T>)Entities[key];
			else return null;
		}
		
		public Aggregate<T> GetAggregate<T>(string name)
		{
			if (Aggregates.Contains(name))
			{
				return Aggregates[name] as Aggregate<T>;
			}
			
			return null;
		}
		
		public Field GetField<T>(string name)
		{
			Entity<T> entity = GetEntity<T>();
			if (entity != null)
			{
				if (entity.Fields.Contains(name))
					return entity.Fields[name];
			}
			
			return null;
		}
		
		public IExpression GetFilter<T>(string fieldName, string operatorName, object value)
		{
			return new Expression(GetField<T>(fieldName), OperatorFactory.GetOperator(operatorName), value);
		}

		public IExpression GetAndFilter<T>(string fieldName, string operatorName, object value)
		{
			return new Expression(OperatorFactory.GetOperator(Schema.FILTER_OP_AND), GetField<T>(fieldName), OperatorFactory.GetOperator(operatorName), value);
		}
		
		public IExpression GetOrFilter<T>(string fieldName, string operatorName, object value)
		{
			return new Expression(OperatorFactory.GetOperator(Schema.FILTER_OP_OR), GetField<T>(fieldName), OperatorFactory.GetOperator(operatorName), value);
		}
		#endregion

		#region Public Constants
		public const string FILTER_OP_AND = "AND";
		public const string FILTER_OP_OR = "OR";
		public const string FILTER_OP_NOT = "NOT";
		public const string FILTER_OP_LI = "~";
		public const string FILTER_OP_LIKE = "LIKE";
		public const string FILTER_OP_NE = "<>";
		public const string FILTER_OP_GE = ">=";
		public const string FILTER_OP_GT = ">";
		public const string FILTER_OP_LE = "<=";
		public const string FILTER_OP_LT = "<";
		public const string FILTER_OP_EQ = "=";
		public static readonly string[] FILTER_OPERATORS = { FILTER_OP_LI, FILTER_OP_LIKE, FILTER_OP_NE, FILTER_OP_GE, FILTER_OP_GT, FILTER_OP_LE, FILTER_OP_LT, FILTER_OP_EQ };
		#endregion
	}
}
