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

using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public static class RepositoryFactory
	{		
		public static IRepository GetRepository(IEngine engine)
		{
			if (engine != null)
			{
				Repository repository = new Repository(engine);
				
				ISchema catalog = new CatalogSchema();
				
				IMap mediaSetMap = new MediaSetMap(repository, catalog.GetRecord<IMediaSet>("MediaSet"));
				IMap mediaItemMap = new MediaItemMap(repository, catalog.GetRecord<IMediaItem>("MediaItem"));
				
				repository.Schemas.Add(catalog);
				repository.Maps.Add(typeof(IMediaSet), mediaSetMap);
				repository.Maps.Add(typeof(IMediaItem), mediaItemMap);
				
				return repository;
			}
			else throw new ArgumentNullException("Could not create a repository: persistence engine is undefined");
		}
	}
}
