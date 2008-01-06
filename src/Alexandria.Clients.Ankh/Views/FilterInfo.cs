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
				this.value = null;
				this.delimitedValue = null;
				
				this.value = GetValue(value);
				this.delimitedValue = GetDelimitedValue(this.value);
			}
			else
			{
				isStandAloneOperator = true;
				this.column = null;
				this.columnType = null;
				this.@operator = @operator;
				this.value = null;
				this.delimitedValue = null;
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
		private string delimitedValue;
		#endregion
		
		#region Private Methods
		private string GetValue(string value)
		{
			if (!string.IsNullOrEmpty(column) && columnType != null)
			{
				switch (columnType.Name)
				{
					case "DateTime":
					{
						DateTime result;
						if (DateTime.TryParse(value, out result))
							return result.ToShortDateString();
						else return DateTime.MinValue.ToShortDateString();
					}
					case "TimeSpan":
					{
						TimeSpan result;
						if (TimeSpan.TryParse(value, out result))
							return result.ToString();
						else return TimeSpan.Zero.ToString();
					}
					default:
						return value;
				}
			}
			else return value;
		}
		
		private string GetDelimitedValue(string value)
		{
			if (!string.IsNullOrEmpty(column) && columnType != null)
			{
				switch (columnType.Name)
				{
					case "String":
						if (Operator.ToUpper() == "LIKE")
							return string.Format("'%{0}%'", value);
						else return string.Format("'{0}'", value);
					case "DateTime":
					{
						DateTime result = DateTime.MinValue;
						if (DateTime.TryParse(value, out result))
						{
							return result.ToFileTimeUtc().ToString();
						}
						else return DateTime.MinValue.ToFileTimeUtc().ToString();
					}
					case "TimeSpan":
					{
						TimeSpan result = TimeSpan.Zero;
						if (TimeSpan.TryParse(value, out result))
						{
							return result.Ticks.ToString();
						}
						else return TimeSpan.MinValue.Ticks.ToString();
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
		
		public string DelimitedValue
		{
			get { return delimitedValue; }
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
			else return string.Format("{0} {1} {2}", Column, Operator, DelimitedValue);
		}
		#endregion
	}
}
