using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.LastFM
{
	public class AudioscrobblerTrack : IAudioscrobblerTrack
	{
		private string artistName;
		private string trackName;
		private string albumName;
		private string musicBrainzId;
		private int trackLength;
		private DateTime trackPlayed;

		public DateTime TrackPlayed
		{
			get { return trackPlayed; }
			set { trackPlayed = value; }
		}

		public int TrackLength
		{
			get { return trackLength; }
			set { trackLength = value; }
		}

		public string MusicBrainzID
		{
			get { return musicBrainzId; }
			set { musicBrainzId = value; }
		}

		public string AlbumName
		{
			get { return albumName; }
			set { albumName = value; }
		}

		public string TrackName
		{
			get { return trackName; }
			set { trackName = value; }
		}

		public string ArtistName
		{
			get { return artistName; }
			set { artistName = value; }
		}
	}
}
