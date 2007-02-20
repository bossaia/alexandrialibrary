using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.LastFM
{
	public class QueuedTrack
	{
		#region Constructors
		/*
		public QueuedTrack(TrackInfo track, DateTime start_time)
		{
			this.artist = track.Artist;
			this.album = track.Album;
			this.title = track.Title;
			this.duration = (int)track.Duration.TotalSeconds;
			this.start_time = start_time.ToUniversalTime();
		}
		*/

		public QueuedTrack(string artist, string album, string title, int duration, DateTime start_time)
		{
			this.artist = artist;
			this.album = album;
			this.title = title;
			this.duration = duration;
			this.start_time = start_time.ToUniversalTime();
		}
		#endregion

		#region Private Fields
		private string artist;
		private string album;
		private string title;
		private int duration;
		private DateTime start_time;
		#endregion

		#region Public Properties
		public DateTime StartTime
		{
			get { return start_time; }
		}

		public string Artist
		{
			get { return artist; }
		}

		public string Album
		{
			get { return album; }
		}

		public string Title
		{
			get { return title; }
		}

		public int Duration
		{
			get { return duration; }
		}
		#endregion
	}
}
