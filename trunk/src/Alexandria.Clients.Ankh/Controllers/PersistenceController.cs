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
			
			//Guid albumId1 = new Guid("5F116994-1490-496a-81BD-5BC3796AD006");
			//DateTime date = new DateTime(1979, 11, 30);
			//string format = "audio/flac";
			//string rootPath1 = "file:///M:/audio/flac/Pink Floyd/The%20Wall%20pt.%201";
			//Guid albumId2 = new Guid("7A300079-6750-4409-9A02-1B788A23D9F2");
			//string rootPath2 = "file:///M:/audio/flac/Pink Floyd/The%20Wall%20pt.%202";
			
			//Album album1 = new Album(albumId1, "Catalog", 1, "The Wall pt. 1", "Pink Floyd", date, format, new Uri(rootPath1), null);
			//album1.Items.Add(new AudioTrack(new Guid("1EDF60AA-3C60-473b-99B0-69A2B1BA002D"), "Catalog", 1, "In The Flesh?", "Pink Floyd", "The Wall pt. 1", new TimeSpan(0, 3, 19), date, format, new Uri(rootPath1 + "/01%20In%20The%20Flesh.flac")));
			//album1.Items.Add(new AudioTrack(new Guid("455314B8-39A1-4b74-95A6-E94B94EEE840"), "Catalog", 2, "The Thin Ice", "Pink Floyd", "The Wall pt. 1", new TimeSpan(0, 2, 27), date, format, new Uri(rootPath1 + "/02%20The%20Thin%20Ice.flac")));
			//album1.Items.Add(new AudioTrack(new Guid("67B1341C-8E7D-4672-946B-36A99DC9DAD2"), "Catalog", 3, "Another Brick in the Wall (Part 1)", "Pink Floyd", "The Wall pt. 1", new TimeSpan(0, 3, 21), date, format, new Uri(rootPath1 + "/03%20Another%20Brick%20in%20the%20Wall%20(Part%201).flac")));

			//Album album2 = new Album(albumId2, "Catalog", 2, "The Wall pt. 2", "Pink Floyd", date, format, new Uri(rootPath2), null);
			//album2.Items.Add(new AudioTrack(new Guid("DC9FF6C0-AD03-4332-9D12-6F3C0883CC94"), "Catalog", 1, "Hey You", "Pink Floyd", "The Wall pt. 2", new TimeSpan(0, 4, 40), date, format, new Uri(rootPath2 + "/01%20Hey%20You.flac")));
			//album2.Items.Add(new AudioTrack(new Guid("E3BBEAF1-D1D9-4a72-AF9D-7E5DABA2CB0B"), "Catalog", 2, "Is There Anybody Out There?", "Pink Floyd", "The Wall pt. 2", new TimeSpan(0, 2, 44), date, format, new Uri(rootPath2 + "/02Is%20There%20Anybody%20Out%20There.flac")));
			//album2.Items.Add(new AudioTrack(new Guid("1AEEC93B-438B-4a9a-A611-2444CD88CBDE"), "Catalog", 3, "Nobody Home", "Pink Floyd", "The Wall pt. 2", new TimeSpan(0, 3, 26), date, format, new Uri(rootPath2 + "/03Nobody%20Home.flac")));
						
			BabelLib.Aggregate<IMediaSet> agg = new Alexandria.Model.Data.MediaSetWithAllChildren(schema);
			//IList<IMediaSet> albums = new List<IMediaSet> { album1, album2 };
			//repo.Save<IMediaSet>(agg, albums);
			BabelLib.Query query = new BabelLib.Query("Search: 'The Wall'");
			query.Filters.Add(schema.GetFilter<IMediaSet>("Title", "LIKE", "The Wall"));
			
			ICollection<IMediaSet> albums = repo.List<IMediaSet>(agg, query);
			int x = albums.Count;
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
