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
using System.Data;
using System.Text;
using System.Threading;

using Telesophy.Alexandria.Clients.Ankh.Views.Data;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Model.Data;

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
		
		private Dictionary<Guid, object> mediaSetsPendingSave = new Dictionary<Guid, object>();
		private IMediaSet lastSavedMediaSet;
		
		private IExpression GetMediaItemFilter(string fieldName, string operatorName, object value)
		{
			return schema.GetFilter<IMediaItem>(fieldName, operatorName, value);
		}

		private IExpression GetMediaItemOrFilter(string fieldName, string operatorName, object value)
		{
			return schema.GetOrFilter<IMediaItem>(fieldName, operatorName, value);
		}

		private IExpression GetMediaItemAndFilter(string fieldName, string operatorName, object value)
		{
			return schema.GetAndFilter<IMediaItem>(fieldName, operatorName, value);
		}

		private IQuery GetMediaItemQuery(string search)
		{
			IQuery query = null;
		
			if (!string.IsNullOrEmpty(search))
			{
				query = new Query("MediaItem Search");
				
				int number;
				bool isNumber = int.TryParse(search, out number);
				
				DateTime date;
				bool isDate = DateTime.TryParse(search, out date);
				
				TimeSpan duration;
				bool isDuration = TimeSpan.TryParse(search, out duration);
				
				Uri path;
				bool isPath = Uri.TryCreate(search, UriKind.Absolute, out path);
				
				query.Filters.Add(GetMediaItemFilter("Title", "LIKE", search));
				query.Filters.Add(GetMediaItemOrFilter("Artist", "LIKE", search));
				query.Filters.Add(GetMediaItemOrFilter("Album", "LIKE", search));
				if (isNumber) query.Filters.Add(GetMediaItemOrFilter("Number", "=", number));
				if (isDate) query.Filters.Add(GetMediaItemOrFilter("Date", "=", date));
				if (isDuration) query.Filters.Add(GetMediaItemOrFilter("Duration", "=", duration));
				if (isPath) query.Filters.Add(GetMediaItemOrFilter("Path", "=", path));
			}
			
			return query;
		}
		
		public ISchema Schema
		{
			get { return schema; }
		}
		
		public bool IsMediaSetPendingSave(Guid id)
		{
			return mediaSetsPendingSave.ContainsKey(id);
		}
		
		public void Initialize()
		{
			repo.Initialize();
		}
		
		public IList<MediaItemData> ListMediaItemData()
		{
			ICollection<IMediaItem> items = ListAllMediaItems();
			return CreateMediaItemDataList(items);
		}
		
		public IList<MediaItemData> ListMediaItemData(string search)
		{
			IQuery query = GetMediaItemQuery(search);
			return ListMediaItemData(query);
		}
		
		public IList<MediaItemData> ListMediaItemData(IQuery query)
		{
			ICollection<IMediaItem> items = ListMediaItems(query);
			return CreateMediaItemDataList(items);
		}
		
		public ICollection<IMediaItem> ListAllMediaItems()
		{
			return repo.List<IMediaItem>(mediaItemSingleton, null);
		}
		
		public ICollection<IMediaItem> ListMediaItems(IQuery query)
		{			
			return repo.List<IMediaItem>(mediaItemSingleton, query);
		}
		
		public ICollection<IMediaSet> ListPlaylists()
		{
			Query query = new Query("List Playlists");
			query.Filters.Add(schema.GetFilter<IMediaSet>("Type", "=", ModelConstants.MEDIA_TYPE_PLAYLIST));
			
			return repo.List<IMediaSet>(mediaSetWithAllChildren, query);
		}
		
		public IMediaSet LookupMediaSet(Guid id)
		{
			IMediaSet model = null;
			
			if (lastSavedMediaSet != null && lastSavedMediaSet.Id == id)
			{
				model = lastSavedMediaSet;
			}
			else
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
						model = iter.Current;
					}
				}
			}
			
			return model;
		}
		
		//public IMediaSet LookupMediaSet(Uri path)
		//{
		//    IMediaSet model = null;
		
		//    if (lastSavedMediaSet != null && lastSavedMediaSet.Path == path)
		//    {
		//        model = lastSavedMediaSet;
		//    }
		//    else
		//    {
		//        Query query = new Query(string.Format("Search MediaSet: Path={0}", path));
		//        query.Filters.Add(schema.GetFilter<IMediaSet>("Path", "=", path.ToString()));

		//        ICollection<IMediaSet> sets = repo.List<IMediaSet>(mediaSetWithAllChildren, query);
		//        if (sets != null && sets.Count > 0)
		//        {
		//            using (IEnumerator<IMediaSet> iter = sets.GetEnumerator())
		//            {
		//                iter.Reset();
		//                iter.MoveNext();
		//                model = iter.Current;
		//            }
		//        }
		//    }

		//    return model;
		//}
		
		public IMediaItem LookupMediaItem(Guid id)
		{
			Query query = new Query(string.Format("Search MediaItem: Id={0}", id));
			query.Filters.Add(schema.GetFilter<IMediaItem>("Id", "=", id.ToString()));

			ICollection<IMediaItem> items = repo.List<IMediaItem>(mediaItemSingleton, query);
			if (items != null && items.Count > 0)
			{
				using (IEnumerator<IMediaItem> iter = items.GetEnumerator())
				{
					iter.Reset();
					iter.MoveNext();
					return iter.Current;
				}
			}

			return null;
		}
		
		public IMediaItem CreateMediaItem(MediaItemData data)
		{
			IList<MediaItemData> list = new List<MediaItemData>() { data };
			IList<IMediaItem> items = CreateMediaItems(list);
			if (items != null && items.Count > 0)
				return items[0];
			else return null;
		}
		
		public MediaItemData CreateMediaItemData(IMediaItem item)
		{
			if (item != null)
				return new MediaItemData(item.Id, item.Type, item.Source, item.Number, item.Title, item.Artist, item.Album, item.Duration, item.Date, item.Format, item.Path);
			else return null;
		}
		
		public IList<MediaItemData> CreateMediaItemDataList(IEnumerable<IMediaItem> items)
		{
			IList<MediaItemData> list = new List<MediaItemData>();
			
			if (items != null)
			{
				foreach (IMediaItem item in items)
					list.Add(CreateMediaItemData(item));
			}
			
			return list;
		}
		
		public IList<IMediaItem> CreateMediaItems(IList<MediaItemData> data)
		{
			Entity<IMediaItem> entity = schema.GetEntity<IMediaItem>();
			DataTable table = entity.GetDataTable(entity.Name);
			
			foreach (MediaItemData dataItem in data)
			{
				DataRow row = table.NewRow();
				row[ModelConstants.MEDIA_ITEM_COLUMN_ID] = dataItem.Id;
				row[ModelConstants.MEDIA_ITEM_COLUMN_TYPE] = dataItem.Type;
				row[ModelConstants.MEDIA_ITEM_COLUMN_SOURCE] = dataItem.Source;
				row[ModelConstants.MEDIA_ITEM_COLUMN_NUMBER] = dataItem.Number;
				row[ModelConstants.MEDIA_ITEM_COLUMN_TITLE] = dataItem.Title;
				row[ModelConstants.MEDIA_ITEM_COLUMN_ARTIST] = dataItem.Artist;
				row[ModelConstants.MEDIA_ITEM_COLUMN_ALBUM] = dataItem.Album;
				row[ModelConstants.MEDIA_ITEM_COLUMN_DURATION] = dataItem.Duration;
				row[ModelConstants.MEDIA_ITEM_COLUMN_DATE] = dataItem.Date;
				row[ModelConstants.MEDIA_ITEM_COLUMN_FORMAT] = dataItem.Format;
				row[ModelConstants.MEDIA_ITEM_COLUMN_PATH] = dataItem.Path;
				
				table.Rows.Add(row);
			}
			
			return CreateMediaItems(table);
		}
		
		public IList<IMediaItem> CreateMediaItems(DataTable table)
		{
			IList<IMediaItem> items = new List<IMediaItem>();
			if (table != null)
			{
				Entity<IMediaItem> entity = schema.GetEntity<IMediaItem>();
				foreach (DataRow row in table.Rows)
				{
					IMediaItem item = entity.GetModel(row);
					items.Add(item);
				}
			}
			return items;
		}
		
		private void SaveMediaSetInternal(IMediaSet model)
		{
			IList<IMediaSet> models = new List<IMediaSet>() { model };
			repo.Save<IMediaSet>(mediaSetWithAllChildren, models);
		}
		
		private void SaveMediaSetComplete(IAsyncResult result)
		{
			Guid id = (Guid)result.AsyncState;
			mediaSetsPendingSave.Remove(id);
		}
		
		private delegate void SaveMediaSetAsync(IMediaSet model);
		
		public void SaveMediaSet(IMediaSet model)
		{
			if (model != null)
			{
				//lastSavedMediaSet = model;
			
				mediaSetsPendingSave[model.Id] = model;
			
				SaveMediaSetAsync handle = new SaveMediaSetAsync(SaveMediaSetInternal);
				handle.BeginInvoke(model, new AsyncCallback(SaveMediaSetComplete), model.Id);
				
				//Thread thread = new Thread(new ParameterizedThreadStart(SaveMediaSetInternal));
				//thread.IsBackground = true;
				//thread.Start(model);
			}
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
			DeleteMediaItems(models);
		}
		
		public void DeleteMediaItems(IEnumerable<IMediaItem> models)
		{
			repo.Delete<IMediaItem>(mediaItemSingleton, models);
		}
	}
}
