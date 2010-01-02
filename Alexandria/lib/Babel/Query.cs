using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Babel
{
	public class Query<T>
		: IQuery<T>
	{
		public Query(object context)
		{
			_context = context;
		}

		#region Private Members

		private object _context;
		private IList<Predicate> _predicates = new List<Predicate>();
		private IList<LogicalOperator> _operators = new List<LogicalOperator>();

		private void AddPredicate(Expression<Func<T, object>> expression)
		{
			Predicate predicate = ReflectionHelper.GetPredicate<T>(expression, _context);

			if (predicate == null)
				throw new ArgumentException("Lambda is not a valid expression", "expression");

			_predicates.Add(predicate);
		}

		private void AddOperator(LogicalOperator op)
		{
			_operators.Add(op);
		}

		#endregion

		#region IQuery<T> Members

		public IQuery<T> And
		{
			get
			{
				AddOperator(LogicalOperator.And);
				return this;
			}
		}

		public IQuery<T> Or
		{
			get
			{
				AddOperator(LogicalOperator.Or);
				return this;
			}
		}

		public IQuery<T> Where(Expression<Func<T, object>> expression)
		{
			AddPredicate(expression);
			return this;
		}

		#endregion

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();

			var i = 0;
			foreach (var predicate in _predicates)
			{
				builder.AppendFormat("{0} {1} {2}", predicate.Name, predicate.Operator.Symbol, predicate.Value.ToValueString());

				if (i < _predicates.Count - 1)
				{
					builder.AppendFormat(" {0} ", _operators[i].Symbol);
				}

				i++;
			}

			return builder.ToString();
		}
	}
}
