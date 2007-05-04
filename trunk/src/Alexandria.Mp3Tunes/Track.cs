using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Mp3Tunes
{
	internal class Track : BaseAudioTrack
	{
		#region Constructors
		public Track(IIdentifier id, ILocation location, string name, int number, TimeSpan length, DateTime releaseDate, IAlbum album, IArtist artist, ISong song) : base(id, location, name, length, releaseDate, album, artist, song)
		{
			this.number = number;
		}		
		#endregion
		
		#region Private Fields
		private int number;
		#endregion
		
		#region Public Properties
		public int Number
		{
			get { return number; }
			set { number = value; }
		}
		#endregion
	}
}
