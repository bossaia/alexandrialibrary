#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Persistence;

namespace Alexandria.Metadata
{	
	public class BaseAudioTrack : BaseMetadata, IAudioTrack
	{
		#region Constructors
		[Constructor("B7DE2E27-C3D5-4988-B0CB-51023819341C")]
		public BaseAudioTrack(Guid id, Uri path, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string format) : base(id, path, name)
		{
			this.album = album;
			this.artist = artist;
			this.duration = duration;
			this.releaseDate = releaseDate;
			this.trackNumber = trackNumber;
			this.format = format;
		}
		#endregion
		
		#region Private Fields
		private string album;
		private string artist;
		private TimeSpan duration;
		private DateTime releaseDate;
		private int trackNumber;
		private string format;
		#endregion
	
		#region IAudioTrack Members
		[Property(4)]
		public string Album
		{
			get { return album; }
		}

		[Property(5)]
		public string Artist
		{
			get { return artist; }
		}

		[Property(6)]
		public TimeSpan Duration
		{
			get { return duration; }
		}

		[Property(7)]
		public DateTime ReleaseDate
		{
			get { return releaseDate; }
		}

		[Property(8)]
		public int TrackNumber
		{
			get { return trackNumber; }
		}

		[Property(9)]
		public string Format
		{
			get { return format; }
		}
		#endregion
	}
}
