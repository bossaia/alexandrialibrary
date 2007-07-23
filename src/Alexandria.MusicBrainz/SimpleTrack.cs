#region License (MIT)
/***************************************************************************
 *  SimpleTrack.cs
 *
 *  Copyright (C) 2005 Novell
 *  Written by Aaron Bockover (aaron@aaronbock.net)
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
using System.Diagnostics;
using Alexandria;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.MusicBrainz
{
	[Record("Track")]
	[RecordType("E7915153-E5BE-47f7-855A-446FB7AF1DB8")]
    public class SimpleTrack : IAudioTrack
    {
		#region Constructors
		internal SimpleTrack()
		{
			id = Guid.NewGuid();
		}
		
		//public SimpleTrack(int index, int length)
		//{
			//this.index = index;
			//this.length = length;
		//}

		[Factory("E7915153-E5BE-47f7-855A-446FB7AF1DB8")]
		public SimpleTrack(Guid id, Uri path, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber)
		{
			this.id = id;
			this.path = path;
			this.name = name;
			this.album = album;
			this.artist = artist;
			this.duration = duration;
			this.releaseDate = releaseDate;
			this.trackNumber = trackNumber;
			this.format = "cdda";
		}
		#endregion
    
		#region Private Fields
		private Guid id;
		private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		private IRecord parent;
		private IPersistenceBroker broker;
		
		private Uri path;
		private string name;
		private string album;
        private string artist;
        private TimeSpan duration;
        private DateTime releaseDate;
        private int trackNumber;
        private string format;
        
        private string title;        
        private string asin;
        private int track_num;
        private int track_count;
        private int index;
        private int length;
        #endregion
                        
        #region Public Properties
        public string Asin
        {
			get {return asin;}
            internal set {asin = value;}
        }
        
        public string Title
        {
			get {return title;}
            internal set {title = value;}
        }
        
        public int Index
        {
			get {return index;}
            internal set {index = value;}
        }
                
        public int TrackNumber
        {
            get {return track_num;}
            internal set {track_num = value;}
        }
                
        public int TrackCount
        {
            get {return track_count;}
            internal set {track_count = value;}
        }
        
        public int Length
        {
            get {return length;}
            internal set {length = value;}
        }
        
        public int Minutes
        {
            get {return length / 60;}
        }
        
        public int Seconds
        {
            get {return length % 60;}
        }
        #endregion

		#region Public Methods
		public override string ToString()
		{
			return String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}: {1} - {2} ({3:00}:{4:00})",
				Index, Artist, Title, Minutes, Seconds);
		}
		#endregion

		#region IAudioTrack Members
		public string Artist
		{
			get { return artist; }
			internal set { artist = value; }
		}

		public string Album
		{
			get { return album; }
			internal set { album = value; }
		}
		
		public TimeSpan Duration
		{
			get { return duration; }
			internal set { duration = value; }
		}

		public DateTime ReleaseDate
		{
			get { return releaseDate; }
			internal set { releaseDate = value; }
		}

		public string Format
		{
			get { return format; }
			internal set { format = value; }
		}
		#endregion

		#region IMetadata Members
		public System.Collections.Generic.IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public string Name
		{
			get { return name; }
			internal set { name = value; }
		}
		#endregion

		#region IRecord Members
		public Guid Id
		{
			get { return id; }
		}

		public IRecord Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		public IPersistenceBroker PersistenceBroker
		{
			get { return broker; }
			set { broker = value; }
		}

		public bool IsProxy
		{
			get { return false; }
		}

		public void Save()
		{
			if (broker != null)
				broker.SaveRecord(this);
		}

		public void Delete()
		{
			if (broker != null)
				broker.DeleteRecord(this);
		}
		#endregion
	}
}