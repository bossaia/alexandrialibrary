using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using Alexandria;
using Alexandria.Metadata;
using Alexandria.Persistence;
using Alexandria.SQLite;

namespace Alexandria.Client.Controllers
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
		
		public void SaveMediaItem(IMediaItem item)
		{
			mediaItemDataMap.SaveMediaItem(item);
		}
		
		public void DeleteMediaItem(IMediaItem item)
		{
			mediaItemDataMap.DeleteMediaItem(item);
		}
	}
}
