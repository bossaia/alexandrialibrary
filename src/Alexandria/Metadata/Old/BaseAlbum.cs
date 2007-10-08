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
	[Record("Album")]
	[RecordType("5204CB07-4030-4186-8AD7-A1EC93937AEA")]
	public abstract class BaseAlbum : BaseMetadata, IOldAlbum
	{
		#region Constructors
		public BaseAlbum(string id, string path, string name, string artist, DateTime releaseDate) : this(new Guid(id), new Uri(path), name, artist, releaseDate)
		{
		}
		
		[Factory("5204CB07-4030-4186-8AD7-A1EC93937AEA")]
		public BaseAlbum(Guid id, Uri path, string name, string artist, DateTime releaseDate) : base(id, path, name)
		{
			this.artist = artist;
			this.releaseDate = releaseDate;
		}
		#endregion
		
		#region Private Fields
		private string artist;
		private DateTime releaseDate = DateTime.MinValue;
		private List<IAudioTrack> tracks = new List<IAudioTrack>();
		#endregion
	
		#region IAlbum Members
		/// <summary>
		/// Get the Artist credited with this album
		/// </summary>
		public string Artist
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		/// <summary>
		/// Get the earliest release date of this album
		/// </summary>
		public DateTime ReleaseDate
		{
			get { return releaseDate; }
		}
		
		/// <summary>
		/// Get the tracks on this album
		/// </summary>
		public IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}
		#endregion
	}
}
