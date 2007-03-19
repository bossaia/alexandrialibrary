using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class PlaybackStatusPlaying : PlaybackStatus
	{
		#region Constructors
		public PlaybackStatusPlaying() : base("Playing")
		{
		}		
		#endregion
		
		#region Public Methods
		public override void Play(IAudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Paused);
				player.PauseCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}
		
		public override void Pause(IAudioPlayer player)
		{
			if (player != null)
			{
				player.SetStatus(PlaybackStatus.Paused);
				player.PauseCurrentSound();
			}
			else throw new ArgumentNullException("player");
		}

		public override void Stop(IAudioPlayer player)
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
