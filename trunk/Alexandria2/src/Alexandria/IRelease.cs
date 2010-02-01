using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IRelease
		: INamedEntity, IEquatable<IRelease>
	{
		IAlbum Album { get; }
		DateTime Date { get; }
		Country Country { get; }
		ISet<ITrack> Tracks();
		ISet<IMedia> Media();

		void ChangeDate(DateTime date);
		void ChangeCountry(Country country);
		void AddTrack(ITrack track);
		void RemoveTrack(ITrack track);
		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
