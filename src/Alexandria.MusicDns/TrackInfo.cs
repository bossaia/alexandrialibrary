#region License (MIT)
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

//using Telesophy.Alexandria.Persistence;
using Telesophy.Alexandria.Model;

namespace Alexandria.MusicDns
{
	internal class TrackInfo : IMediaItem
	{
		#region Constructors
		internal TrackInfo()
		{
		}
		#endregion
	
		#region Private Fields
		private string fileName = string.Empty;
		private string fingerprint = string.Empty;
		private string encoding = string.Empty;
		private int bitRate = 0;
		private string format = string.Empty;
		private long lengthInMS = 0;
		private string artist = string.Empty;
		private string track = string.Empty;
		private string album = string.Empty;
		private int trackNum = 0;
		private string genre = string.Empty;
		private string year = string.Empty;
		private string composer = string.Empty;
		private string conductor = string.Empty;
		private string lyricist = string.Empty;
		private string band = string.Empty;
		private string puid = string.Empty;		
		
		private Guid id = Guid.NewGuid();
		private string status;
		private string source;
		private string type;
		
		//private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		private Uri path;
		private Version version = new Version(1, 0, 0, 0);
		#endregion
	
		#region Internal Properties
		internal string FileName
		{
			get { return fileName; }
			set
			{
				fileName = value;
				
				if (!string.IsNullOrEmpty(fileName) && path == null)
					path = new Uri(fileName);
			}
		}
		
		internal string Fingerprint
		{
			get { return fingerprint; }
			set { fingerprint = value; }
		}
		
		internal string Encoding
		{
			get { return encoding; }
			set { encoding = value; }
		}
		
		internal int BitRate
		{
			get { return bitRate; }
			set { bitRate = value; }
		}

		internal string Format
		{
			get { return format; }
			set { format = value; }
		}

		internal long LengthInMS
		{
			get { return lengthInMS; }
			set { lengthInMS = value; }
		}

		internal string Artist
		{
			get { return artist; }
			set { artist = value; }
		}

		internal string Track
		{
			get { return track; }
			set { track = value; }
		}
		
		internal string Album
		{
			get { return album; }
			set { album = value; }
		}
		
		internal int TrackNum
		{
			get { return trackNum; }
			set { trackNum = value; }
		}
		
		internal string Genre
		{
			get { return genre; }
			set { genre = value; }
		}
		
		internal string Year
		{
			get { return year; }
			set { year = value; }
		}
		
		private string Composer
		{
			get { return composer; }
			set { composer = value; }
		}
		
		private string Conductor
		{
			get { return conductor; }
			set { conductor = value; }
		}
		
		private string Lyricist
		{
			get { return lyricist; }
			set { lyricist = value; }
		}
		
		private string Band
		{
			get { return band; }
			set { band = value; }
		}
		
		internal string Puid
		{
			get { return puid; }
			set
			{
				puid = value;
				
				//if (!string.IsNullOrEmpty(puid) && metadataIdentifiers.Count == 0)
					//metadataIdentifiers.Add(PuidFactory.CreatePuid(puid));
			}
		}
		
		internal Version Version
		{
			get { return version; }
			set { version = value; }
		}
		#endregion

		/*
		#region IMediaItem Members
		string IMediaItem.Title
		{
			get { return Track; }
			set { }
		}
		
		string IMediaItem.Album
		{
			get { return album; }
			set { }
		}

		string IMediaItem.Artist
		{
			get { return artist; }
			set { }
		}

		public TimeSpan Duration
		{
			get { return new TimeSpan(0, 0, 0, 0, (int)LengthInMS); }
			set { }
		}

		public DateTime Date
		{
			get
			{
				try
				{
					return Convert.ToDateTime("1/1/" + Year);
				}
				catch (FormatException)
				{
					return DateTime.MinValue;
				}
			}
			set { }
		}

		public int Number
		{
			get { return TrackNum; }
			set { }
		}

		string IMediaItem.Format
		{
			get { return Format; }
			set { }
		}
		
		string IMediaItem.Source
		{
			get { return null; }
			set { }
		}
		
		string IMediaItem.Status
		{
			get { return null; }
			set { }
		}
		
		string IMediaItem.Type
		{
			get { return null; }
			set { }
		}
		#endregion

		#region IMetadata Members
		public Guid Id
		{
			get { return id; }
			set { }
		}
				
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		public Uri Path
		{
			get { return path; }
			set { }
		}

		public string Name
		{
			get { return Track; }
		}
		#endregion
		*/

		#region IMediaItem Members
		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Status
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public string Source
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public string Type
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public int Number
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public string Title
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		string IMediaItem.Artist
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		string IMediaItem.Album
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public TimeSpan Duration
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public DateTime Date
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		string IMediaItem.Format
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public Uri Path
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		#endregion
	}
}