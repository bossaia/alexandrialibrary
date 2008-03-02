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
using System.Data;
using System.Linq;
using System.Text;

using Telesophy.Babel.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaSetFactory : FactoryBase<IMediaSet>
	{
		#region Constructors
		public MediaSetFactory(IRepository repository) : base("MediaSetFactory", repository)
		{
		}
		#endregion
	
		#region Private Constants
		private const string TABLE_NAME = "MediaSet";
		private const string LINK_TABLE_NAME = "MediaSetMediaItem";
		#endregion
	
		#region Public Overrides
		public override IMediaSet GetModel(DataRow row)
		{
			IMediaSet model = null;

			if (row != null)
			{
				Guid id = row.Field<Guid>("Id");
				string source = row.Field<string>("Source");
				string type = row.Field<string>("Type");
				int number = row.Field<int>("Number");
				string title = row.Field<string>("Title");
				string artist = row.Field<string>("Artist");
				DateTime date = row.Field<DateTime>("Date");
				string format = row.Field<string>("Format");
				Uri path = row.Field<Uri>("Path");

				switch (type)
				{
					case Constants.TYPE_AUDIO:
						model = new Album(id, source, number, title, artist, date, format, path, null);
						break;
					case Constants.TYPE_VIDEO:
						model = new Movie(id, source, number, title, artist, date, format, path, null);
						break;
					default:
						break;
				}
			}

			return model;
		}

		public override void AddDataRows(DataSet dataSet, IEnumerable<IMediaSet> models)
		{
			if (dataSet != null && models != null)
			{
				foreach (IMediaSet set in models)
				{
					DataRow row = dataSet.Tables[TABLE_NAME].NewRow();
					
					row["Id"] = set.Id;
					row["Source"] = set.Source;
					row["Type"] = set.Type;
					row["Number"] = set.Number;
					row["Title"] = set.Title;
					row["Artist"] = set.Artist;
					row["Date"] = set.Date;
					row["Format"] = set.Format;
					row["Path"] = set.Path;
				
					dataSet.Tables[TABLE_NAME].Rows.Add(row);
					
					foreach (IMediaItem item in set.Items)
					{
						DataRow itemRow = dataSet.Tables[LINK_TABLE_NAME].NewRow();
						
						row["ParentId"] = set.Id;
						row["ChildId"] = item.Id;
					}
				}
			}
		}
		
		public override IDictionary<string, IMediaSet> GetModels(DataSet dataSet, int currentDepth, int totalDepth)
		{
			IDictionary<string, IMediaSet> models = new Dictionary<string, IMediaSet>();
			
			if (dataSet != null && dataSet.Tables.Contains(TABLE_NAME))
			{
				currentDepth++;

				foreach (DataRow row in dataSet.Tables[TABLE_NAME].Rows)
				{
					IMediaSet mediaSet = GetModel(row);

					//if (mediaItems.Contains(mediaSet.Id

					models.Add(mediaSet.Id.ToString(), mediaSet);
				}
				
				IDictionary<string, IMediaItem> mediaItems = new Dictionary<string, IMediaItem>();
				if (currentDepth < totalDepth)
				{
					IFactory<IMediaItem> mediaItemFactory = (IFactory<IMediaItem>)Repository.Factories[typeof(IMediaItem)];
					mediaItems = mediaItemFactory.GetModels(dataSet, currentDepth, totalDepth);
					
					foreach (DataRow row in dataSet.Tables[LINK_TABLE_NAME].Rows)
					{
						Guid parentId = row.Field<Guid>("ParentId");
						Guid childId = row.Field<Guid>("ChildId");
						
						if (models.ContainsKey(parentId.ToString()) && mediaItems.ContainsKey(childId.ToString()))
						{
							models[parentId.ToString()].Items.Add(mediaItems[childId.ToString()]);
							mediaItems[childId.ToString()].Parent = models[parentId.ToString()];
						}
					}
				}
			}
			
			return models;
		}

		public override void FillDataSet(DataSet dataSet, IEnumerable<IMediaSet> models, int currentDepth, int totalDepth)
		{
			if (dataSet != null && models != null)
			{
				currentDepth++;
			
				AddDataRows(dataSet, models);
				
				if (currentDepth < totalDepth)
				{
					IFactory<IMediaItem> mediaItemFactory = (IFactory<IMediaItem>)Repository.Factories[typeof(IMediaItem)];
				
					foreach (IMediaSet mediaSet in models)
					{
						mediaItemFactory.FillDataSet(dataSet, mediaSet.Items, currentDepth, totalDepth);
					}
				}
			}
		}
		#endregion
	}
}
