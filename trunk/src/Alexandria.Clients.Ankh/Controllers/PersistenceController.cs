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
using Telesophy.Alexandria.Persistence;
using Telesophy.Alexandria.Persistence.SQLite;

using BabelLib=Telesophy.Babel.Persistence;

namespace Telesophy.Alexandria.Clients.Ankh.Controllers
{
	[CLSCompliant(false)]
	public class PersistenceController
	{
		public PersistenceController()
		{
			//TODO: abstract this further
			//sqlEngine = new SQLiteEngine();
			//mediaItemDataMap = new MediaItemDataMap(sqlEngine);
			//mediaSetDataMap = new MediaSetDataMap(sqlEngine, mediaItemDataMap);
			//mediaSetContentDataMap = new MediaSetContentDataMap(sqlEngine, mediaSetDataMap, mediaItemDataMap);
			
			schema = new CatalogSchema();
			engine = new BabelLib.SQLite.SQLiteEngine();
			repo = new BabelLib.Repository(schema, engine);
		}

		private BabelLib.ISchema schema;
		private BabelLib.IEngine engine;
		private BabelLib.IRepository repo;
		
		//private SQLiteEngine sqlEngine;
		//private MediaSetDataMap mediaSetDataMap;
		//private MediaItemDataMap mediaItemDataMap;
		//private MediaSetContentDataMap mediaSetContentDataMap;
		
		public void Initialize()
		{
			repo.Initialize();
			//sqlEngine.CreateTable(mediaSetDataMap.Table);
			//sqlEngine.CreateTable(mediaItemDataMap.Table);
			//sqlEngine.CreateTable(mediaSetContentDataMap.Table);
		}
		
		public bool CatalogExists
		{
			get { return true; }
		}
		
		public IList<IMediaItem> ListAllMediaItems()
		{
			//return mediaItemDataMap.ListModels();
			return new List<IMediaItem>();
		}
		
		public IList<IMediaItem> ListMediaItems(string filter)
		{
			//return mediaItemDataMap.ListModels(filter);
			return new List<IMediaItem>();
		}
		
		public IMediaSet LookupMediaSet(Guid id)
		{
			//return mediaSetDataMap.LookupModel(id, true);
			return null;
		}
		
		public void SaveMediaSet(IMediaSet model, bool cascade)
		{
			//mediaSetDataMap.SaveModel(model, cascade);
		}
		
		public void DeleteMediaSet(IMediaSet model)
		{
			//mediaSetDataMap.DeleteModel(model, false);
		}
		
		public void SaveMediaItem(IMediaItem model)
		{
			//mediaItemDataMap.SaveModel(model);
		}
		
		public void DeleteMediaItem(IMediaItem model)
		{
			//mediaItemDataMap.DeleteModel(model);
		}
	}
}
