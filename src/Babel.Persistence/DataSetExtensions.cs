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
using System.Data;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public static class DataSetExtensions
	{
		public static IEnumerable<T> GetModels<T>(this DataSet dataSet, IRepository repository, int depth)
		{
			IEnumerable<T> list = new List<T>();

			if (repository != null && repository.Factories.Contains(typeof(T)))
			{
				IFactory<T> factory = (IFactory<T>)repository.Factories[typeof(T)];
				return factory.GetModels(dataSet, 0, depth).Values;
			}

			return list;
		}
		
		public static void Fill<T>(this DataSet dataSet, IRepository repository, IEnumerable<T> models, int depth)
		{
			if (repository != null && repository.Factories.Contains(typeof(T)))
			{
				IFactory<T> factory = (IFactory<T>)repository.Factories[typeof(T)];
				factory.FillDataSet(dataSet, models, 0, depth);
			}
		}
	}
}
