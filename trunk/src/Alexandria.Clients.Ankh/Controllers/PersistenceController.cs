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
using System.Configuration;
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Model.Data;
//using Telesophy.Alexandria.Persistence;
//using Telesophy.Alexandria.Persistence.SQLite;

using Telesophy.Babel.Persistence;
using Telesophy.Babel.Persistence.SQLite;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	[CLSCompliant(false)]
	public class PersistenceController
	{
		public PersistenceController()
		{
			schema = new CatalogSchema();
			engine = new SQLiteEngine();
			repo = new Repository(schema, engine);
			
			mediaItemSingleton = schema.GetAggregate<IMediaItem>("MediaItemSingleton");
			mediaSetWithAllChildren = schema.GetAggregate<IMediaSet>("MediaSetWithAllChildren");
		}

		private CatalogSchema schema;
		private IEngine engine;
		private IRepository repo;
		
		private Aggregate<IMediaItem> mediaItemSingleton;
		private Aggregate<IMediaSet> mediaSetWithAllChildren;
		
		public void Initialize()
		{
			repo.Initialize();
		}
				
		public ICollection<IMediaItem> ListAllMediaItems()
		{
			return repo.List<IMediaItem>(mediaItemSingleton, null);
		}
		
		public ICollection<IMediaItem> ListMediaItems(string filter)
		{
			//TODO: turn the filter string into a query
			Query query = new Query("Search MediaItem: " + filter);
			
			return repo.List<IMediaItem>(mediaItemSingleton, null);
		}
		
		public IMediaSet LookupMediaSet(Guid id)
		{
			Query query = new Query(string.Format("Search MediaSet: Id={0}", id));
			query.Filters.Add(schema.GetFilter<IMediaSet>("Id", "=", id.ToString()));
			
			ICollection<IMediaSet> sets = repo.List<IMediaSet>(mediaSetWithAllChildren, query);
			if (sets != null && sets.Count > 0)
			{
				using (IEnumerator<IMediaSet> iter = sets.GetEnumerator())
				{
					iter.Reset();
					iter.MoveNext();
					return iter.Current;
				}
			}
			
			return null;
		}
		
		public void SaveMediaSet(IMediaSet model)
		{
			IList<IMediaSet> models = new List<IMediaSet>() { model };
			repo.Save<IMediaSet>(mediaSetWithAllChildren, models);
		}
		
		public void DeleteMediaSet(IMediaSet model)
		{
			IList<IMediaSet> models = new List<IMediaSet>() { model };
			repo.Delete<IMediaSet>(mediaSetWithAllChildren, models);
		}
		
		public void SaveMediaItem(IMediaItem model)
		{
			IList<IMediaItem> models = new List<IMediaItem>() { model };
			repo.Save<IMediaItem>(mediaItemSingleton, models);
		}
		
		public void DeleteMediaItem(IMediaItem model)
		{
			IList<IMediaItem> models = new List<IMediaItem>() { model };
			repo.Delete<IMediaItem>(mediaItemSingleton, models);
		}
	}
}
