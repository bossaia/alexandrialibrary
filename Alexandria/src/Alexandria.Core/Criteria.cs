using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Alexandria.Core
{
	public class Criteria<T>
		: ICriteria<T>
		where T : IEntity
	{
		public Criteria(Expression<Func<T, object>> expession, object value)
		{
			if (expession == null)
				throw new ArgumentNullException("expression");

			Predicate predicate = ReflectionHelper.GetPredicate<T, object>(expession, value);

			_predicates.Add(predicate);
		}

		#region Private Members

		private IList<Predicate> _predicates = new List<Predicate>();
		private IList<LogicalOperator> _operators = new List<LogicalOperator>();

		private void AddPredicate<V>(Expression<Func<T, V>> expression, V value)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			Predicate predicate = ReflectionHelper.GetPredicate<T, V>(expression, value);

			if (predicate == null)
				throw new ArgumentException("Lambda is not a valid property expression", "expression");

			_predicates.Add(predicate);
		}

		private void AddOperator(LogicalOperator op)
		{
			_operators.Add(op);
		}

		#endregion

		#region ICriteria<T> Members

		public ICriteria<T> And<V>(Expression<Func<T, V>> expression, V value)
		{
			_operators.Add(LogicalOperator.And);

			Predicate predicate = ReflectionHelper.GetPredicate<T, V>(expression, value);
			_predicates.Add(predicate);
			
			return this;
		}

		public ICriteria<T> Or<V>(Expression<Func<T, V>> expression, V value)
		{
			_operators.Add(LogicalOperator.Or);

			Predicate predicate = ReflectionHelper.GetPredicate<T, V>(expression, value);
			_predicates.Add(predicate);
			
			return this;
		}

		/*
		public ICriteria<T> IsNotNull
		{
			get
			{
				AddComparison(ComparisonOperator.IsNot);
				AddValue(null);
				return this;
			}
		}

		public ICriteria<T> IsNull
		{
			get
			{
				AddComparison(ComparisonOperator.Is);
				AddValue(null);
				return this;
			}
		}

		public ICriteria<T> That<V>(Expression<Func<T, V>> expression, V value)
		{
			AddProperty<V>(expression);
			object x = value;
			return this;
		}

		public ICriteria<T> IsGreaterThan(object value)
		{
			AddComparison(ComparisonOperator.GreaterThan);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsGreaterThanOrEqualTo(object value)
		{
			AddComparison(ComparisonOperator.GreaterThanOrEqualTo);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsEqualTo(object value)
		{
			AddComparison(ComparisonOperator.EqualTo);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsLessThan(object value)
		{
			AddComparison(ComparisonOperator.LessThan);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsLessThanOrEqualTo(object value)
		{
			AddComparison(ComparisonOperator.LessThanOrEqualTo);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsLike(object value)
		{
			AddComparison(ComparisonOperator.Like);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsNotEqualTo(object value)
		{
			AddComparison(ComparisonOperator.NotEqualTo);
			AddValue(value);
			return this;
		}

		public ICriteria<T> IsNotLike(object value)
		{
			AddComparison(ComparisonOperator.NotLike);
			AddValue(value);
			return this;
		}

		public bool IsValid()
		{
			//TODO: Validation needs to be more sophisticated
			if (_properties.Count != _comparisons.Count || _properties.Count != _values.Count)
				return false;

			if (_links.Count != _properties.Count - 1)
				return false;

			return true;
		}
		*/

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
