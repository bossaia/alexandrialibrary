using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class PlaybackStatus
	{
		#region Private Fields
		private string name;
		#endregion

		#region Private Static Fields
		private static PlaybackStatus playing;
		private static PlaybackStatus paused;
		private static PlaybackStatus stopped;
		#endregion

		#region Constructors
		protected PlaybackStatus(string name)
		{
			this.name = name;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Get the name of the playback status
		/// </summary>
		public string Name
		{
			get { return name; }
		}
		#endregion

		#region Public Static Properties
		public static PlaybackStatus Playing
		{
			get
			{
				if (playing == null) playing = new PlaybackStatusPlaying();
				return playing;
			}
		}

		public static PlaybackStatus Paused
		{
			get
			{
				if (paused == null) paused = new PlaybackStatusPaused();
				return paused;
			}
		}

		public static PlaybackStatus Stopped
		{
			get
			{
				if (stopped == null) stopped = new PlaybackStatusStopped();
				return stopped;
			}
		}
		#endregion

		#region Public Methods
		public virtual void Play(AudioPlayer player)
		{
		}

		public virtual void Stop(AudioPlayer player)
		{
		}

		public virtual void Pause(AudioPlayer player)
		{
		}
		#endregion
	}
}
