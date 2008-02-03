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

namespace Telesophy.Alexandria.Persistence
{
	public interface IMap
	{
		IRepository Repository { get; }
		//IRecord Record { get; }
		RelationshipCollection Relationships { get; }
		Type Type { get; }
		Query GetRelationshipQuery(IRelationship relationship);
		void AddLookupCommand(Batch batch, Query query);
		void AddSaveCommand(Batch batch, Query query);
		void AddDeleteCommand(Batch batch, Query query);
	}
	
	public interface IMap<Model> : IMap
	{
		IRecord<Model> Record { get; }
		//Model Lookup(Query query);
		Model Lookup(IResult result);
		//IList<Model> List(Query query);
		IList<Model> List(IResult result);
		//void Save(Model model);
		//void Delete(Model model);
	}
}
