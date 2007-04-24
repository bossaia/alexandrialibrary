using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Mp3Tunes
{
	internal class Track : BaseAudioTrack
	{
		#region Constructors
		public Track(IIdentifier id, ILocation location, string name, int number, TimeSpan length, DateTime releaseDate, IAlbum album, ISong song) : base(id, location, name, number, length, releaseDate, album, song)
		{
		}
		#endregion
		
		#region IMetadata Members
		public override IDataMap CreateMap()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void LoadMap(IDataMap map)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
