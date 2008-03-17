#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
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
using System.Text;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public struct FilterInfo
	{
		#region Constructors
		public FilterInfo(string column, Type columnType, string @operator, string value)
		{
			if (!string.IsNullOrEmpty(column))
			{
				isStandAloneOperator = false;
				this.column = column;
				this.columnType = columnType;
				this.@operator = @operator;
				
				this.value = value;
				this.formattedValue = null;
				
				this.formattedValue = GetFormattedValue(value);
			}
			else
			{
				isStandAloneOperator = true;
				this.column = null;
				this.columnType = null;
				this.@operator = @operator;
				this.value = null;
				this.formattedValue = null;
			}
		}
		
		public FilterInfo(string @operator) : this(null, null, @operator, null)
		{
		}
		#endregion
		
		#region Private Fields
		private bool isStandAloneOperator;
		private string column;
		private Type columnType;
		private string @operator;
		private string value;
		private string formattedValue;
		#endregion
		
		#region Private Methods
		private string GetFormattedValue(string value)
		{
			if (!string.IsNullOrEmpty(column) && columnType != null)
			{
				switch (columnType.Name)
				{
					case "DateTime":
					{
						DateTime result;
						if (DateTime.TryParse(value, out result))
							return result.ToString("s");
						else return DateTime.MinValue.ToString("s");
					}
					case "TimeSpan":
					{
						TimeSpan result;
						if (!TimeSpan.TryParse(value, out result))
							result = TimeSpan.Zero;
						
						return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", result.Hours, result.Minutes, result.Seconds, result.Milliseconds);						
					}
					default:
						return value;
				}
			}
			else return value;
		}
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
		
		public Type ColumnType
		{
			get { return columnType; }
		}
		
		public string Operator
		{
			get { return @operator; }
		}
		
		public string Value
		{
			get { return value; }
		}
		
		public string FormattedValue
		{
		    get { return formattedValue; }
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
				case "IS":
					newOperator = "NOT";
					break;
				case "NOT":
					newOperator = "IS";
					break;
				case "OR":
					newOperator = "AND";
					break;
				case "AND":
					newOperator = "OR";
					break;
				default:
					break;
			}
			
			return new FilterInfo(Column, ColumnType, newOperator, Value); 
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
			else return string.Format("{0} {1} {2}", Column, Operator, FormattedValue);
		}
		#endregion
	}
}
