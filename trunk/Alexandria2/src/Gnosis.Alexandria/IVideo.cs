using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IVideo
		: INamed, IEquatable<IVideo>
	{
		IArtist Artist { get; }
		DateTime Date { get; }
		Country Country { get; }
		int Number { get; }
		IEnumerable<IMedia> Media { get; }

		void ChangeArtist(IArtist artist);
		void ChangeDate(DateTime date);
		void ChangeCountry(Country country);
		void ChangeNumber(int number);

		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
