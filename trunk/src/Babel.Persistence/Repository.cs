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
	public class Repository : IRepository
	{
		#region Constructors
		public Repository(ISchema schema, IEngine engine)
		{
			this.schema = schema;
			this.engine = engine;
		}
		#endregion
		
		#region Private Fields
		private ISchema schema;
		private IEngine engine;
		#endregion
		
		#region IRepository Members
		public ISchema Schema
		{
			get { return schema; }
		}
		
		public IEngine Engine
		{
			get { return engine; }
		}

		public void Initialize()
		{
			Schema.Initialize();
			Engine.Initialize(Schema);
		}
		
		public IList<T> Load<T>(Aggregate<T> aggregate, IQuery query)
		{
			return Engine.Load<T>(aggregate, query);	
		}
		
		public void Save<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			Engine.Save<T>(aggregate, models);
		}
		
		public void Delete<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			Engine.Delete<T>(aggregate, models);
		}
		#endregion
	}
}