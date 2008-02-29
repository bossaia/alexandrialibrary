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
using System.Net.Mime;

using Telesophy.Alexandria.Model;
using Telesophy.Babel.Persistence;

namespace Telesophy.Alexandria.Model.Data
{
	public class MediaItemMap : MapBase<IMediaItem>
	{
		#region Constructors
		public MediaItemMap(ISchema schema) : base("MediaItem", schema, MapFunction.Entity)
		{
			Fields.Add(new Field(this, "Id", typeof(Guid), FieldFunction.Identifier));
			Fields.Add(new Field(this, "Source", typeof(string), FieldFunction.Value));
			Fields.Add(new Field(this, "Type", typeof(string), FieldFunction.TypeDescriminator));
			Fields.Add(new Field(this, "Number", typeof(int), FieldFunction.Value));
			Fields.Add(new Field(this, "Title", typeof(string), FieldFunction.Value));
			Fields.Add(new Field(this, "Artist", typeof(string), FieldFunction.Value));
			Fields.Add(new Field(this, "Album", typeof(string), FieldFunction.Value));
			Fields.Add(new Field(this, "Duration", typeof(TimeSpan), FieldFunction.Value));
			Fields.Add(new Field(this, "Date", typeof(DateTime), FieldFunction.Value));
			Fields.Add(new Field(this, "Format", typeof(string), FieldFunction.Value));
			Fields.Add(new Field(this, "Path", typeof(Uri), FieldFunction.UniqueValue));
		}
		#endregion
		
		#region Public Overrides
		public override Field Identifier
		{
			get { return Fields["Id"]; }
		}

		public override void BuildQuery(IQuery query, int currentDepth, int totalDepth)
		{
			throw new NotImplementedException();
		}

		public override void BuildDataSet(DataSet dataSet, int currentDepth, int totalDepth)
		{
			throw new NotImplementedException();
		}
		
		public override DataTable ToDataTable()
		{
			DataTable table = new DataTable(Name);
			table.Columns.Add("Id", typeof(Guid));
			table.Columns.Add("Source", typeof(string));
			table.Columns.Add("Type", typeof(string));
			table.Columns.Add("Number", typeof(int));
			table.Columns.Add("Title", typeof(string));
			table.Columns.Add("Artist", typeof(string));
			table.Columns.Add("Album", typeof(string));
			table.Columns.Add("Duration", typeof(TimeSpan));
			table.Columns.Add("Date", typeof(DateTime));
			table.Columns.Add("Format", typeof(string));
			table.Columns.Add("Path", typeof(Uri));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Id"], true));
			table.Constraints.Add(new UniqueConstraint(table.Columns["Path"]));
			return table;
		}

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
		
		public override IDictionary<Guid, IMediaItem> GetModelsById(DataSet dataSet, int currentDepth, int totalDepth)
		{
			currentDepth++;
		
			IDictionary<Guid, IMediaItem> models = new Dictionary<Guid, IMediaItem>();
			foreach (DataRow row in dataSet.Tables[Name].Rows)
			{
				IMediaItem item = GetModel(row);
				models.Add(item.Id, item);
			}
		
			//if (currentDepth < totalDepth)
			//{
			//}
		
			return models;
		}
		
		public override IEnumerable<IMediaItem> GetModels(DataSet dataSet, int currentDepth, int totalDepth)
		{
			return GetModelsById(dataSet, currentDepth, totalDepth).Values;
		}
		#endregion
	}
}
