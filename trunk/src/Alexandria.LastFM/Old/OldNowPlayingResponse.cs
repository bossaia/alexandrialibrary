using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.LastFM
{
	public class OldNowPlayingResponse
	{
		#region Constructors
		public OldNowPlayingResponse()
		{
		}
		#endregion
	
		#region Private Fields
		private string _streaming = string.Empty;
		private string _station = string.Empty;
		private string _stationUrl = string.Empty;
		private string _stationFeed = string.Empty;
		private string _stationFeedUrl = string.Empty;
		private string _artist = string.Empty;
		private string _artistUrl = string.Empty;
		private string _track = string.Empty;
		private string _trackUrl = string.Empty;
		private string _album = string.Empty;
		private string _albumUrl = string.Empty;
		private string _albumCoverUrlSmall = string.Empty;
		private string _albumCoverUrlMedium = string.Empty;
		private string _albumCoverUrlLarge = string.Empty;
		private string _trackDuration = string.Empty;
		private string _trackProgress = string.Empty;
		private string _radioMode = string.Empty;
		private string _recordToProfile = string.Empty;
		#endregion

		#region Public Properties
		public string Streaming
		{
			get { return this._streaming; }
			set { this._streaming = value; }
		}

		public string Station
		{
			get { return this._station; }
			set { this._station = value; }
		}

		public string StationUrl
		{
			get { return this._stationUrl; }
			set { this._stationUrl = value; }
		}

		public string StationFeed
		{
			get { return this._stationFeed; }
			set { this._stationFeed = value; }
		}

		public string StationFeedUrl
		{
			get { return this._stationFeedUrl; }
			set { this._stationFeedUrl = value; }
		}

		public string Artist
		{
			get { return this._artist; }
			set { this._artist = value; }
		}

		public string ArtistUrl
		{
			get { return this._artistUrl; }
			set { this._artistUrl = value; }
		}

		public string Track
		{
			get { return this._track; }
			set { this._track = value; }
		}

		public string TrackUrl
		{
			get { return this._trackUrl; }
			set { this._trackUrl = value; }
		}

		public string Album
		{
			get { return this._album; }
			set { this._album = value; }
		}

		public string AlbumUrl
		{
			get { return this._albumUrl; }
			set { this._albumUrl = value; }
		}

		public string AlbumCoverUrlSmall
		{
			get { return this._albumCoverUrlSmall; }
			set { this._albumCoverUrlSmall = value; }
		}

		public string AlbumCoverUrlMedium
		{
			get { return this._albumCoverUrlMedium; }
			set { this._albumCoverUrlMedium = value; }
		}

		public string AlbumCoverUrlLarge
		{
			get { return this._albumCoverUrlLarge; }
			set { this._albumCoverUrlLarge = value; }
		}

		public string TrackDuration
		{
			get { return this._trackDuration; }
			set { this._trackDuration = value; }
		}

		public string TrackProgress
		{
			get { return this._trackProgress; }
			set { this._trackProgress = value; }
		}

		public string RadioMode
		{
			get { return this._radioMode; }
			set { this._radioMode = value; }
		}

		public string RecordToProfile
		{
			get { return this._recordToProfile; }
			set { this._recordToProfile = value; }
		}
		#endregion
	}
}
