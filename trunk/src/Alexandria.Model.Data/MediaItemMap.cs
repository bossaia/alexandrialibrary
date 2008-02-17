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
		public MediaItemMap(ISchema schema) : base("IMediaItem", schema, MapFunction.Entity)
		{
			Fields.Add(new Field(this, "Id", typeof(Guid), FieldFunction.Identifier, FieldProperties.RequiredAndUnique));
			Fields.Add(new Field(this, "Source", typeof(string), FieldFunction.OpenEndedValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Type", typeof(string), FieldFunction.TypeDescriminator, FieldProperties.Required));
			Fields.Add(new Field(this, "Number", typeof(int), FieldFunction.ComparableValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Title", typeof(string), FieldFunction.Name, FieldProperties.Required));
			Fields.Add(new Field(this, "Artist", typeof(string), FieldFunction.Name, FieldProperties.Required));
			Fields.Add(new Field(this, "Album", typeof(string), FieldFunction.Name, FieldProperties.Required));
			Fields.Add(new Field(this, "Duration", typeof(TimeSpan), FieldFunction.ComparableValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Date", typeof(DateTime), FieldFunction.ComparableValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Format", typeof(string), FieldFunction.FormattedValue, FieldProperties.Required));
			Fields.Add(new Field(this, "Path", typeof(Uri), FieldFunction.FormattedValue, FieldProperties.RequiredAndUnique));
			Fields.Add(new Field(this, "MediaItemParentId", typeof(Guid), FieldFunction.ParentIdentifier, FieldProperties.None));
		}
		#endregion
		
		#region Public Overrides
		public override Field IdentifierField
		{
			get { return Fields["Id"]; }
		}

		public override Field ParentIdentifierField
		{
			get { return Fields["MediaItemParentId"]; }
		}

		public override DataTable GetTable()
		{
			DataTable table = new DataTable("MediaItem");
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
			return table;
		}
		
		public override DataTable GetTable(IEnumerable<IMediaItem> models)
		{
			DataTable table = GetTable();
			
			foreach (IMediaItem item in models)
			{
				if (item != null)
				{
					table.Rows.Add(item.Id, item.Source, item.Type, item.Number, item.Title, item.Artist, item.Album, item.Duration, item.Date, item.Format, item.Path);
				}
			}
			
			return table;
		}

		public override IEnumerable<IMediaItem> GetModels(DataTable table)
		{
			IList<IMediaItem> list = new List<IMediaItem>();
		
			if (table != null && table.Rows.Count > 0)
			{
				foreach (DataRow row in table.Rows)
				{
					IMediaItem item = null;
					string type = row.Field<string>("Type");
					switch (type)
					{
						case Constants.TYPE_AUDIO:
							item = new AudioTrack();
							break;
						default:
							break;
					}
					
					if (item != null)
					{
						item.Id = row.Field<Guid>("Id");
						item.Source = row.Field<string>("Source");
						item.Type = type;
						item.Number = row.Field<int>("Number");
						item.Title = row.Field<string>("Title");
						item.Artist = row.Field<string>("Artist");
						item.Album = row.Field<string>("Album");
						item.Duration = row.Field<TimeSpan>("Duration");
						item.Date = row.Field<DateTime>("Date");
						item.Format = row.Field<string>("Format");
						item.Path = row.Field<Uri>("Path");
						
						list.Add(item);	
					}
				}
			}
		
			return list;
		}

		public override void LoadChildren(IEnumerable<IMediaItem> models, IResult result)
		{
		}
		#endregion
	}
}
