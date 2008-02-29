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

namespace Telesophy.Alexandria.Model
{
	public class AudioTrack : IMediaItem
	{
		#region Constructors
		public AudioTrack()
		{
		}

		public AudioTrack(Guid id, string source, int number, string title, string artist, string album, TimeSpan duration, DateTime date, string format, Uri path)
		{
			this.id = id;
			this.source = source;
			this.number = number;
			this.title = title;
			this.artist = artist;
			this.album = album;
			this.duration = duration;
			this.date = date;
			this.format = format;
			this.path = path;
		}
		#endregion

		#region Private Fields
		private Guid id;
		private string status = string.Empty;
		private string source;
		private string type = Constants.TYPE_AUDIO;
		private int number;
		private string title;
		private string artist;
		private string album;
		private TimeSpan duration;
		private DateTime date;
		private string format;
		private Uri path;
		private IMediaSet parent;
		#endregion

		#region IMediaItem Members
		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Status
		{
			get { return status; }
			set { status = value; }
		}

		public string Source
		{
			get { return source; }
			set { source = value; }
		}

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public int Number
		{
			get { return number; }
			set { number = value; }
		}

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		public string Artist
		{
			get { return artist; }
			set { artist = value; }
		}

		public string Album
		{
			get { return album; }
			set { album = value; }
		}

		public TimeSpan Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		public string Format
		{
			get { return format; }
			set { format = value; }
		}

		public Uri Path
		{
			get { return path; }
			set { path = value; }
		}
		
		public IMediaSet Parent
		{
			get { return parent; }
			set { parent = value; }
		} 
		#endregion
	}
}