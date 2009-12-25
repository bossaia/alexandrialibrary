using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public class ComparisonOperator
	{
		private ComparisonOperator(string symbol)
		{
			_symbol = symbol;
		}

		private string _symbol;

		public string Symbol
		{
			get { return _symbol; }
		}

		public override string ToString()
		{
			return _symbol;
		}

		public static readonly ComparisonOperator EqualTo = new ComparisonOperator("=");
		public static readonly ComparisonOperator GreaterThan = new ComparisonOperator(">");
		public static readonly ComparisonOperator GreaterThanOrEqualTo = new ComparisonOperator(">=");
		public static readonly ComparisonOperator In = new ComparisonOperator("IN");
		public static readonly ComparisonOperator Is = new ComparisonOperator("IS");
		public static readonly ComparisonOperator IsNot = new ComparisonOperator("IS NOT");
		public static readonly ComparisonOperator LessThan = new ComparisonOperator("<");
		public static readonly ComparisonOperator LessThanOrEqualTo = new ComparisonOperator("<=");
		public static readonly ComparisonOperator Like = new ComparisonOperator("LIKE");
		public static readonly ComparisonOperator NotEqualTo = new ComparisonOperator("<>");
		public static readonly ComparisonOperator NotLike = new ComparisonOperator("NOT LIKE");
	}
}
