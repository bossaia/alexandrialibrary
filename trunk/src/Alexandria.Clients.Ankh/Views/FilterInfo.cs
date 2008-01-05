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
