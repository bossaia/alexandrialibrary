using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using Alexandria;
using Alexandria.Persistence;
using Alexandria.Persistence.SQLite;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Model.Data;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	public class PersistenceController
	{
		public PersistenceController()
		{
			engine = new SQLiteEngine();
			
			mediaItemDataMap = new MediaItemDataMap();
			mediaItemDataMap.Engine = engine;
		}
		
		private SQLiteEngine engine;
		private MediaItemDataMap mediaItemDataMap;
		
		public void Initialize()
		{
			engine.CreateTable(mediaItemDataMap.Table);
		}
		
		public bool CatalogExists
		{
			get { return true; }
		}
		
		public IList<IMediaItem> ListAllMediaItems()
		{
			return mediaItemDataMap.ListModels();
		}
		
		public IList<IMediaItem> ListMediaItems(string filter)
		{
			return mediaItemDataMap.ListModels(filter);
		}
		
		[CLSCompliant(false)]
		public void SaveMediaItem(IMediaItem item)
		{
			mediaItemDataMap.SaveModel(item);
		}
		
		[CLSCompliant(false)]
		public void DeleteMediaItem(IMediaItem item)
		{
			mediaItemDataMap.DeleteModel(item);
		}
	}
}
