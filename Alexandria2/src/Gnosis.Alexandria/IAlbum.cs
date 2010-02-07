using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IAlbum
		: INamed, IEquatable<IAlbum>
	{
		IArtist Artist { get; }
		AlbumType Type { get; }
		DateTime Date { get; }
		Country Country { get; }
		int Number { get; }
		ITuple<ITrack> Tracks();

		void ChangeArtist(IArtist artist);
		void ChangeType(AlbumType type);
		void ChangeDate(DateTime releaseDate);
		void ChangeCountry(Country releaseCountry);
		void ChangeNumber(int discNumber);
		void AddTrack(ITrack track);
		void RemoveTrack(ITrack track);
	}
}
