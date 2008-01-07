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
using System.Data;

using Telesophy.Alexandria.Model;
using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaItemDataMap : BaseSimpleDataMap<IMediaItem>
	{
		#region Constructors
		public MediaItemDataMap()
		{
			Table = new DataTable("MediaItem");
			Table.Columns.Add("Id", typeof(Guid));
			Table.Columns.Add("Source", typeof(string));
			Table.Columns.Add("Type", typeof(string));
			Table.Columns.Add("Number", typeof(int));
			Table.Columns.Add("Title", typeof(string));
			Table.Columns.Add("Artist", typeof(string));
			Table.Columns.Add("Album", typeof(string));
			Table.Columns.Add("Duration", typeof(TimeSpan));
			Table.Columns.Add("Date", typeof(DateTime));
			Table.Columns.Add("Format", typeof(string));
			Table.Columns.Add("Path", typeof(Uri));
			Table.Constraints.Add(new UniqueConstraint(Table.Columns["Id"], true));
			Table.Constraints.Add(new UniqueConstraint(Table.Columns["Path"]));
		}
		#endregion

		#region Protected Methods
		protected override IMediaItem GetModelFromRow(DataRow row)
		{
			IMediaItem model = new AudioTrack();

			if (row != null)
			{
				model.Id = GetValue<Guid>(row[0]);
				model.Source = GetValue<string>(row[1]);
				model.Type = GetValue<string>(row[2]);
				model.Number = GetValue<int>(row[3]);
				model.Title = GetValue<string>(row[4]);
				model.Artist = GetValue<string>(row[5]);
				model.Album = GetValue<string>(row[6]);
				model.Duration = GetValue<TimeSpan>(row[7]);
				model.Date = GetValue<DateTime>(row[8]);
				model.Format = GetValue<string>(row[9]);
				model.Path = GetValue<Uri>(row[10]);
			}

			return model;
		}

		protected override DataRow GetRowFromModel(IMediaItem model)
		{
			DataRow row = Table.NewRow();

			if (model != null)
			{
				row[0] = model.Id;
				row[1] = model.Source;
				row[2] = model.Type;
				row[3] = model.Number;
				row[4] = model.Title;
				row[5] = model.Artist;
				row[6] = model.Album;
				row[7] = model.Duration;
				row[8] = model.Date;
				row[9] = model.Format;
				row[10] = model.Path;
			}

			return row;
		}
		#endregion
	}
}
