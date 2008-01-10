using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Model.Data;
using Telesophy.Alexandria.Persistence;
using Telesophy.Alexandria.Persistence.SQLite;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	[CLSCompliant(false)]
	public class PersistenceController
	{
		public PersistenceController()
		{
			//TODO: abstract this further
			engine = new SQLiteEngine();
			mediaItemDataMap = new MediaItemDataMap(engine);
			mediaSetDataMap = new MediaSetDataMap(engine, mediaItemDataMap);
			mediaSetContentDataMap = new MediaSetContentDataMap(engine, mediaSetDataMap, mediaItemDataMap);
		}
		
		private SQLiteEngine engine;
		private MediaSetDataMap mediaSetDataMap;
		private MediaItemDataMap mediaItemDataMap;
		private MediaSetContentDataMap mediaSetContentDataMap;
		
		public void Initialize()
		{
			engine.CreateTable(mediaSetDataMap.Table);
			engine.CreateTable(mediaItemDataMap.Table);
			engine.CreateTable(mediaSetContentDataMap.Table);
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
		
		public IMediaSet LookupMediaSet(Guid id)
		{
			return mediaSetDataMap.LookupModel(id, true);
		}
		
		public void SaveMediaSet(IMediaSet model, bool cascade)
		{
			mediaSetDataMap.SaveModel(model, cascade);
		}
		
		public void DeleteMediaSet(IMediaSet model)
		{
			mediaSetDataMap.DeleteModel(model, false);
		}
		
		public void SaveMediaItem(IMediaItem model)
		{
			mediaItemDataMap.SaveModel(model);
		}
		
		public void DeleteMediaItem(IMediaItem model)
		{
			mediaItemDataMap.DeleteModel(model);
		}		
	}
}
