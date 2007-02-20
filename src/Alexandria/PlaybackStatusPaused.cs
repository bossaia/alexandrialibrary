using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public class PlaybackStatusPaused : PlaybackStatus
	{
		#region Constructors
		public PlaybackStatusPaused() : base("Paused")
		{
		}
		#endregion
		
		#region Public Methods
		public override void Play(AudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Playing);
				player.PlayCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}

		public override void Pause(AudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Playing);
				player.PlayCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}

		public override void Stop(AudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Stopped);
				player.StopCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}
		#endregion
	}
}
