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
	public class MediaItemEntity : Entity<IMediaItem>
	{
		#region Constructors
		public MediaItemEntity() : base("MediaItem")
		{
			id = new Field("Id", this, typeof(Guid), true, true);
			type = new Field("Type", this, typeof(string), true);
			source = new Field("Source", this, typeof(string), true);
			number = new Field("Number", this, typeof(int), true);
			title = new Field("Title", this, typeof(string), true);
			artist = new Field("Artist", this, typeof(string), true);
			album = new Field("Album", this, typeof(string), true);
			duration = new Field("Duration", this, typeof(TimeSpan), true);
			date = new Field("Date", this, typeof(DateTime), true);
			format = new Field("Format", this, typeof(string), true);
			path = new Field("Path", this, typeof(Uri), true, true);

			Fields.Add(id);
			Fields.Add(type);
			Fields.Add(source);
			Fields.Add(number);
			Fields.Add(title);
			Fields.Add(artist);
			Fields.Add(album);
			Fields.Add(duration);
			Fields.Add(date);
			Fields.Add(format);
			Fields.Add(path);
			
			DefaultSortOrder.Add(artist, true);
			DefaultSortOrder.Add(album, true);
			DefaultSortOrder.Add(number, true);
		}
		#endregion
		
		#region Private Constants
		private const string ASSOCIATION_CREATORS = "MediaItemCreators";
		#endregion
		
		#region Private Fields
		Field id;
		Field type;
		Field source;
		Field number;
		Field title;
		Field artist;
		Field album;
		Field duration;
		Field date;
		Field format;
		Field path;
		
		private Entity<IArtist> artistEntity;
		#endregion
		
		#region Overrides
		public override void Initialize(Schema schema)
		{
			base.Initialize(schema);

			artistEntity = schema.GetEntity<IArtist>();
			
			Associations.Add(new Association(ASSOCIATION_CREATORS, this, artistEntity, Relationship.ManyToMany, false));
		}

		public override IMediaItem GetModel(DataRow row)
		{
			IMediaItem model = null;
		
			Guid id = new Guid(row["Id"].ToString());
			string type = row["Type"].ToString();
			string source = row["Source"].ToString();
			int number = Convert.ToInt32(row["Number"]);
			string title = row["Title"].ToString();
			string artist = row["Artist"].ToString();
			string album = row["Album"].ToString();
			TimeSpan duration = TimeSpan.Parse(row["Duration"].ToString());
			DateTime date = DateTime.Parse(row["Date"].ToString());
			string format = row["Format"].ToString();
			Uri path = new Uri(row["Path"].ToString());

			switch (type)
			{
				case ModelConstants.MEDIA_TYPE_AUDIO:
					model = new AudioTrack(id, source, number, title, artist, album, duration, date, format, path);
					break;
				case ModelConstants.MEDIA_TYPE_VIDEO:
					model = new VideoClip(id, source, number, title, artist, album, duration, date, format, path);
					break;
				default:
					break;
			}
			
			return model;
		}
		
		public override Tuple GetTuple(IMediaItem model)
		{
			Tuple tuple = new Tuple(Name, "Id");
			
			tuple["Id"] = model.Id;
			tuple["Type"] = model.Type;
			tuple["Source"] = model.Source;
			tuple["Number"] = model.Number;
			tuple["Title"] = model.Title;
			tuple["Artist"] = model.Artist;
			tuple["Album"] = model.Album;
			tuple["Duration"] = model.Duration;
			tuple["Date"] = model.Date;
			tuple["Format"] = model.Format;
			tuple["Path"] = model.Path;
			
			return tuple;
		}

		public override void AddDataRow(DataTable table, IMediaItem model)
		{
			if (table != null && model != null)
			{
				DataRow row = table.NewRow();
				
				row["Id"] = model.Id;
				row["Type"] = model.Type;
				row["Source"] = model.Source;
				row["Number"] = model.Number;
				row["Title"] = model.Title;
				row["Artist"] = model.Artist;
				row["Album"] = model.Album;
				row["Duration"] = model.Duration;
				row["Date"] = model.Date;
				row["Format"] = model.Format;
				row["Path"] = model.Path;
				
				table.Rows.Add(row);
			}
		}

		public override Field Identifier
		{
			get { return id; }
		}
		#endregion
	}
}
