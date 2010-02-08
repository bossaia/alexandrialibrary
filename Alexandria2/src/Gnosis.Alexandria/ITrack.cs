using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ITrack
		: INamed, IEquatable<ITrack>, IComparable<ITrack>
	{
		IArtist Artist { get; }
		IAlbum Album { get; }
		int Number { get; }
		TimeSpan Duration { get; }

		void ChangeArtist(IArtist artist);
		void ChangeAlbum(IAlbum album);
		void ChangeNumber(int number);
		void ChangeDuration(TimeSpan duration);
	}
}
