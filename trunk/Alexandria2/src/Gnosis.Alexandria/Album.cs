using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public class Album
		: NamedEntityBase, IAlbum
	{
		public Album(ITrackRepository trackRepository, IMediaRepository mediaRepository)
		{
			_trackRepository = trackRepository;
			_mediaRepository = mediaRepository;
		}

		private readonly ITrackRepository _trackRepository;
		private readonly IMediaRepository _mediaRepository;
		private IArtist _artist;
		private DateTime? _releaseDate;
		private Country _releaseCountry;
		private int _discNumber;
		private Set<ITrack> _tracks;
		private Set<IMedia> _media;

		private Set<ITrack> TrackSet
		{
			get
			{
				return _tracks ?? (_tracks = new Set<ITrack>(_trackRepository.GetByAlbum(this)));
			}
		}

		private Set<IMedia> MediaSet
		{
			get
			{
				return _media ?? (_media = new Set<IMedia>(_mediaRepository.GetByParentId(Id)));
			}
		}

		#region IAlbum Members

		public IArtist Artist
		{
			get { return _artist; }
		}

		public DateTime? ReleaseDate
		{
			get { return _releaseDate; }
		}

		public Country ReleaseCountry
		{
			get { return _releaseCountry; }
		}

		public int DiscNumber
		{
			get { return _discNumber; }
		}

		public ISet<ITrack> Tracks()
		{
			return TrackSet;
		}

		public ISet<IMedia> Media()
		{
			return MediaSet;
		}

		public void ChangeArtist(IArtist artist)
		{
			_artist = artist;
		}

		public void ChangeReleaseDate(DateTime releaseDate)
		{
			_releaseDate = releaseDate;
		}

		public void ChangeReleaseCountry(Country releaseCountry)
		{
			_releaseCountry = releaseCountry;
		}

		public void ChangeDiscNumber(int discNumber)
		{
			_discNumber = discNumber;
		}

		public void AddTrack(ITrack track)
		{
			TrackSet.Add(track);
		}

		public void RemoveTrack(ITrack track)
		{
			TrackSet.Remove(track);
		}

		public void AddMedia(IMedia media)
		{
			MediaSet.Add(media);
		}

		public void RemoveMedia(IMedia media)
		{
			MediaSet.Remove(media);
		}

		#endregion

		#region IEquatable<IAlbum> Members

		public bool Equals(IAlbum other)
		{
			if (other == null)
				return false;

			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
