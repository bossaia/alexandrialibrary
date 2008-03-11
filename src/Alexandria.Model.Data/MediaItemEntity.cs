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
		
		public override IMediaItem GetModel(IDictionary<string, object> tuple)
		{
			IMediaItem model = null;
			
			if (tuple != null)
			{
				Guid id = (Guid)tuple["Id"];
				string type = (string)tuple["Type"];
				string source = (string)tuple["Source"];
				int number = (int)tuple["Number"];
				string title = (string)tuple["Title"];
				string artist = (string)tuple["Artist"];
				string album = (string)tuple["Album"];
				TimeSpan duration = (TimeSpan)tuple["Duration"];
				DateTime date = (DateTime)tuple["Date"];
				string format = (string)tuple["Format"];
				Uri path = (Uri)tuple["Path"];
				
				switch (type)
				{
					case Constants.MEDIA_TYPE_AUDIO:
						model = new AudioTrack(id, source, number, title, artist, album, duration, date, format, path);
						break;
					case Constants.MEDIA_TYPE_VIDEO:
						model = new VideoClip(id, source, number, title, artist, album, duration, date, format, path);
						break;
					default:
						break;
				}
			}

			return model;
		}

		public override IDictionary<string, object> GetTuple(IMediaItem model)
		{
			IDictionary<string, object> tuple = new Dictionary<string, object>();

			if (model != null)
			{
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
			}

			return tuple;
		}

		public override Field Identifier
		{
			get { return id; }
		}
		#endregion
	}
}
