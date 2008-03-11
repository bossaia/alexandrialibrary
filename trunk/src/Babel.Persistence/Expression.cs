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
	public class Expression : IExpression
	{
		#region Constructors
		public Expression(IOperator linkingOperator, object leftOperand, IOperator comparisonOperator, object rightOperand)
		{
			this.linkingOperator = linkingOperator;
			this.leftOperand = leftOperand;
			this.comparisonOperator = comparisonOperator;
			this.rightOperand = rightOperand;
		}
		
		public Expression(object leftOperand, IOperator comparisonOperator, object rightOperand) : this(null, leftOperand, comparisonOperator, rightOperand)
		{
		}
		#endregion
	
		#region Private Fields
		private IOperator linkingOperator;
		private object leftOperand;
		private IOperator comparisonOperator;
		private object rightOperand;
		#endregion
	
		#region IExpression Members
		public IOperator LinkingOperator
		{
			get { throw new NotImplementedException(); }
		}

		public object LeftOperand
		{
			get { throw new NotImplementedException(); }
		}

		public IOperator ComparisonOperator
		{
			get { throw new NotImplementedException(); }
		}

		public object RightOperand
		{
			get { throw new NotImplementedException(); }
		}
		#endregion
	}
}
