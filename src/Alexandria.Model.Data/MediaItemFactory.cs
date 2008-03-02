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
	public class MediaItemFactory : FactoryBase<IMediaItem>
	{
		#region Constructors
		public MediaItemFactory(IRepository repository) : base("MediaItemFactory", repository)
		{
		}
		#endregion
		
		#region Private Constants
		private const string TABLE_NAME = "MediaItem";
		#endregion
		
		#region Public Overrides
		public override IMediaItem GetModel(DataRow row)
		{
			IMediaItem model = null;

			if (row != null)
			{
				Guid id = row.Field<Guid>("Id");
				string source = row.Field<string>("Source");
				string type = row.Field<string>("Type");
				int number = row.Field<int>("Number");
				string title = row.Field<string>("Title");
				string artist = row.Field<string>("Artist");
				string album = row.Field<string>("Album");
				TimeSpan duration = row.Field<TimeSpan>("Duration");
				DateTime date = row.Field<DateTime>("Date");
				string format = row.Field<string>("Format");
				Uri path = row.Field<Uri>("Path");

				switch (type)
				{
					case Constants.TYPE_AUDIO:
						model = new AudioTrack(id, source, number, title, artist, album, duration, date, format, path);
						break;
					case Constants.TYPE_VIDEO:
						model = new VideoClip(id, source, number, title, artist, album, duration, date, format, path);
						break;
					default:
						break;
				}
			}

			return model;
		}

		public override void AddDataRows(System.Data.DataSet dataSet, IEnumerable<IMediaItem> models)
		{
			if (dataSet != null && models != null)
			{
				foreach (IMediaItem item in models)
				{
					DataRow row = dataSet.Tables[TABLE_NAME].NewRow();

					row["Id"] = item.Id;
					row["Source"] = item.Source;
					row["Type"] = item.Type;
					row["Number"] = item.Number;
					row["Title"] = item.Title;
					row["Artist"] = item.Artist;
					row["Album"] = item.Album;
					row["Duration"] = item.Duration;
					row["Date"] = item.Date;
					row["Format"] = item.Format;
					row["Path"] = item.Path;

					dataSet.Tables[TABLE_NAME].Rows.Add(row);
				}
			}
		}

		public override IDictionary<string, IMediaItem> GetModels(DataSet dataSet, int currentDepth, int totalDepth)
		{
			IDictionary<string, IMediaItem> models = new Dictionary<string, IMediaItem>();
			
			if (dataSet != null && dataSet.Tables.Contains(TABLE_NAME))
			{
				currentDepth++;

				foreach (DataRow row in dataSet.Tables[TABLE_NAME].Rows)
				{
					IMediaItem mediaItem = GetModel(row);

					models.Add(mediaItem.Id.ToString(), mediaItem);
				}
			}
			
			return models;
		}

		public override void FillDataSet(DataSet dataSet, IEnumerable<IMediaItem> models, int currentDepth, int totalDepth)
		{
			if (dataSet != null && models != null)
			{
				currentDepth++;
			
				AddDataRows(dataSet, models);
				
				if (currentDepth < totalDepth)
				{
					//logic for child records goes here
				}
			}
		}
		#endregion
	}
}
