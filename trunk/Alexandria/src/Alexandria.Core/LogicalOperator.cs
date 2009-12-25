using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public class LogicalOperator
	{
		private LogicalOperator(string symbol)
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

		public static readonly LogicalOperator And = new LogicalOperator("AND");
		public static readonly LogicalOperator Or = new LogicalOperator("OR");
	}
}
