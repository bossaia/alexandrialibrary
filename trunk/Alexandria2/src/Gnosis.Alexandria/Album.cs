using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Collections;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria
{
	public class Album
		: NamedBase, IAlbum
	{
		public Album(ILinkRepository linkRepository, ITagRepository tagRepository, ITrackRepository trackRepository, IMediaRepository mediaRepository)
			: base(linkRepository, tagRepository)
		{
			_trackRepository = trackRepository;
			_mediaRepository = mediaRepository;
		}

		public Album(ILinkRepository linkRepository, ITagRepository tagRepository, ITrackRepository trackRepository, IMediaRepository mediaRepository, long id)
			: base(linkRepository, tagRepository, id)
		{
			_trackRepository = trackRepository;
			_mediaRepository = mediaRepository;
		}

		private readonly ITrackRepository _trackRepository;
		private readonly IMediaRepository _mediaRepository;
		private IArtist _artist;
		private AlbumType _type;
		private DateTime _date;
		private Country _country;
		private int _number;
		private Tuple<ITrack> _tracks;

		private Tuple<ITrack> TrackTuple
		{
			get
			{
				return _tracks ?? (_tracks = new Tuple<ITrack>(_trackRepository.GetByAlbum(this)));
			}
		}

		#region IAlbum Members

		public IArtist Artist
		{
			get { return _artist; }
		}

		public AlbumType Type
		{
			get { return _type; }
		}

		public DateTime Date
		{
			get { return _date; }
		}

		public Country Country
		{
			get { return _country; }
		}

		public int Number
		{
			get { return _number; }
		}

		public IEnumerable<ITrack> Tracks()
		{
			return TrackTuple;
		}

		public void ChangeArtist(IArtist artist)
		{
			_artist = artist;
		}

		public void ChangeType(AlbumType type)
		{
			_type = type;
		}

		public void ChangeDate(DateTime date)
		{
			_date = date;
		}

		public void ChangeCountry(Country country)
		{
			_country = country;
		}

		public void ChangeNumber(int number)
		{
			_number = number;
		}

		public void AddTrack(ITrack track)
		{
			TrackTuple.Add(track);
		}

		public void RemoveTrack(ITrack track)
		{
			TrackTuple.Remove(track);
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
