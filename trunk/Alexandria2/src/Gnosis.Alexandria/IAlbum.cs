using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IAlbum
		: INamedEntity, IEquatable<IAlbum>
	{
		IArtist Artist { get; }
		DateTime ReleaseDate { get; }
		Country ReleaseCountry { get; }
		int DiscNumber { get; }
		ISet<ITrack> Tracks();
		ISet<IMedia> Media();

		void ChangeArtist(IArtist artist);
		void ChangeReleaseDate(DateTime date);
		void ChangeReleaseCountry(Country country);
		void ChangeDiscNumber(int discNumber);
		void AddTrack(ITrack track);
		void RemoveTrack(ITrack track);
		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
