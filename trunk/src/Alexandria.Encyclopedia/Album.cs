using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Encyclopedia
{
	public class Album : IAlbum
	{
		#region Private Fields
		private string name;
		private DateTime releaseDate;
		private IArtist artist;
		private string amazonAsin;
		private Uri coverArtUrl;
		List<Collaboration> collaborations = new List<Collaboration>();
		private Dictionary<uint, IAudioTrack> tracks = new Dictionary<uint, IAudioTrack>();
		private string musicBrainzId;
		private Uri musicBrainzUrl;
		#endregion	
		
		#region Constructors
		public Album()
		{
		}
		
		public Album(string id)
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
		
		public IArtist Artist
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
		public IDictionary<uint, IAudioTrack> Tracks
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

		#region IAlbum Members


		public bool HasVariousArtists
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IArtist> Performers
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		IList<IAudioTrack> IAlbum.Tracks
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IGenre> Genres
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IStyle> Styles
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IResource Members

		public IIdentifier Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public ILocation Location
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IMediaFormat Format
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region IMetadata Members
		public IDataMatrix CreateMap()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void LoadMap(IDataMatrix map)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
