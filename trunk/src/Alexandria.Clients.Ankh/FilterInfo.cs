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
			if (!string.IsNullOrEmpty(column))
			{
				isStandAloneOperator = false;
				this.column = column;
				this.@operator = @operator;
				this.value = value;
			}
			else
			{
				isStandAloneOperator = true;
				this.column = null;
				this.@operator = @operator;
				this.value = null;
			}
		}
		
		public FilterInfo(string @operator) : this(null, @operator, null)
		{
		}
		#endregion
		
		#region Private Fields
		private bool isStandAloneOperator;
		private string column;
		private string @operator;
		private string value;
		#endregion
		
		#region Public Properties
		public bool IsStandAloneOperator
		{
			get { return isStandAloneOperator; }
		}
		
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
			string newOperator = Operator;
			
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
				case "Is":
					newOperator = "Not";
					break;
				case "Not":
					newOperator = "Is";
					break;
				case "Or":
					newOperator = "And";
					break;
				case "And":
					newOperator = "Or";
					break;
				default:
					break;
			}
			
			return new FilterInfo(Column, newOperator, Value); 
		}

		public string GetDescription()
		{
			if (IsStandAloneOperator)
				return Operator;
			else return string.Format("{0} {1}", Operator, Value);
		}

		public override string ToString()
		{
			if (IsStandAloneOperator)
				return Operator;
			else return string.Format("{0} {1} {2}", Column, Operator, Value);
		}
		#endregion
	}
}
