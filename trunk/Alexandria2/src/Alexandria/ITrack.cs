using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface ITrack
		: INamedEntity, IEquatable<ITrack>
	{
		IAlbum Album { get; }
		byte DiscNumber { get; }
		byte TrackNumber { get; }
		TimeSpan Duration { get; }
		ISet<IMedia> Media();

		void ChangeDiscNumber(byte discNumber);
		void ChangeTrackNumber(byte trackNumber);
		void ChangeDuration(TimeSpan duration);
		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
