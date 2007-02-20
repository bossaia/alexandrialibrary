using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Data
{
	public class Album : DataRecord
	{
		#region Private Fields
		private string name;
		private DateTime releaseDate;
		private Artist artist;
		private string amazonAsin;
		private Uri coverArtUrl;
		List<Collaboration> collaborations = new List<Collaboration>();
		private Dictionary<uint, Track> tracks = new Dictionary<uint,Track>();
		private string musicBrainzId;
		private Uri musicBrainzUrl;
		#endregion	
		
		#region Constructors
		public Album() : base()
		{
		}
		
		public Album(string id) : base(id)
		{
		}
		#endregion
		
		#region Public Properties
		public string Name
		{
			get {return name;}
			set {name = value;}
		}
		
		public DateTime ReleaseDate
		{
			get {return releaseDate;}
			set {releaseDate = value;}
		}
		
		public Artist Artist
		{
			get {return artist;}
			set {artist = value;}
		}
		
		public string AmazonAsin
		{
			get {return amazonAsin;}
			set {amazonAsin = value;}
		}
		
		public Uri CoverArtUrl
		{
			get {return coverArtUrl;}
			set {coverArtUrl = value;}
		}
		
		public IList<Collaboration> Collaborations
		{
			get {return collaborations;}
		}
		
		[CLSCompliant(false)]
		public IDictionary<uint, Track> Tracks
		{
			get {return tracks;}
		}

		//TODO: abstract this into RecordId 
		// and add a RecordType property to define where the Id came from
		public string MusicBrainzId
		{
			get {return musicBrainzId;}
			set {musicBrainzId = value;}
		}
		
		//TODO: abstract this into RecordUrl
		public Uri MusicBrainzUrl
		{
			get {return musicBrainzUrl;}
			set {musicBrainzUrl = value;}
		}
		#endregion
	}
}
