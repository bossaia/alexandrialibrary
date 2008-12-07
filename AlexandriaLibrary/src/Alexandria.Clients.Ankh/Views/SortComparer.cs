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
using System.ComponentModel;
using System.Reflection;

namespace Telesophy.Alexandria.Clients.Ankh.Views
{
	public class SortComparer<T> : IComparer<T>
	{
		private ListSortDescriptionCollection m_SortCollection = null;
		private PropertyDescriptor m_PropDesc = null;
		private ListSortDirection m_Direction = ListSortDirection.Ascending;

		public SortComparer(PropertyDescriptor propDesc, ListSortDirection direction)
		{
			m_PropDesc = propDesc;
			m_Direction = direction;
		}

		public SortComparer(ListSortDescriptionCollection sortCollection)
		{
			m_SortCollection = sortCollection;
		}

		int IComparer<T>.Compare(T x, T y)
		{
			if (m_PropDesc != null) // Simple sort
			{
				object xValue = m_PropDesc.GetValue(x);
				object yValue = m_PropDesc.GetValue(y);
				return CompareValues(xValue, yValue, m_Direction);
			}
			else if (m_SortCollection != null &&
				m_SortCollection.Count > 0)
			{
				return RecursiveCompareInternal(x, y, 0);
			}
			else return 0;
		}

		private int CompareValues(object xValue, object yValue, ListSortDirection direction)
		{

			int retValue = 0;
			if (xValue is IComparable) // Can ask the x value
			{
				retValue = ((IComparable)xValue).CompareTo(yValue);
			}
			else if (yValue is IComparable) //Can ask the y value
			{
				retValue = ((IComparable)yValue).CompareTo(xValue);
			}
			// not comparable, compare String representations
			else if (!xValue.Equals(yValue))
			{
				retValue = xValue.ToString().CompareTo(yValue.ToString());
			}
			if (direction == ListSortDirection.Ascending)
			{
				return retValue;
			}
			else
			{
				return retValue * -1;
			}
		}

		private int RecursiveCompareInternal(T x, T y, int index)
		{
			if (index >= m_SortCollection.Count)
				return 0; // termination condition

			ListSortDescription listSortDesc = m_SortCollection[index];
			
			object xValue = null;
			object yValue = null;

			//HACK: PropertyDescriptor.GetValue() is throwing an 'Object Does Not Match Target Type'
			//      exception so I'm using the PropertyInfo instead. I can't figure out the cause.
			//PropertyInfo prop = typeof(T).GetProperty(listSortDesc.PropertyDescriptor.Name);
            PropertyDescriptor prop = listSortDesc.PropertyDescriptor;
			if (prop != null)
			{
				xValue = prop.GetValue(x); //, null);
                yValue = prop.GetValue(y); //, null);
				//object xValue = listSortDesc.PropertyDescriptor.GetValue(x);
				//object yValue = listSortDesc.PropertyDescriptor.GetValue(y);
			}

			int retValue = CompareValues(xValue,
			   yValue, listSortDesc.SortDirection);
			if (retValue == 0)
			{
				return RecursiveCompareInternal(x, y, ++index);
			}
			else
			{
				return retValue;
			}
		}
	}
}
