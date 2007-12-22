using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Clients.Ankh
{
	public struct FilterInfo
	{
		#region Constructors
		public FilterInfo(string column, string @operator, string value)
		{
			this.column = column;
			this.@operator = @operator;
			this.value = value;
		}
		#endregion
		
		#region Private Fields
		private string column;
		private string @operator;
		private string value;
		#endregion
		
		#region Public Properties
		public string Column
		{
			get { return column; }
		}
		
		public string Operator
		{
			get { return @operator; }
		}
		
		public string Value
		{
			get { return value; }
		}
		#endregion
		
		#region Public Methods
		public FilterInfo Negate()
		{
			string newOperator = "=";
			
			switch(Operator)
			{
				case "=":
					newOperator = "<>";
					break;
				case "<>":
					newOperator = "=";
					break;
				case ">":
					newOperator = "<=";
					break;
				case ">=":
					newOperator = "<";
					break;
				case "<":
					newOperator = ">=";
					break;
				case "<=":
					newOperator = ">";
					break;
				default:
					break;
			}
			
			return new FilterInfo(Column, newOperator, Value); 
		}

		public string GetDescription()
		{
			return string.Format("{0} {1}", Operator, Value);
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", Column, Operator, Value);
		}
		#endregion
	}
}
