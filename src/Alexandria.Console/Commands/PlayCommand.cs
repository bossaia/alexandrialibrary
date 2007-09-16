using System;
using System.Collections.Generic;

namespace Alexandria.Console.Commands
{
	public class PlayCommand : Command
	{
		public PlayCommand() : base("Play")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			if (!string.IsNullOrEmpty(option))
			{
				Uri path = new Uri(option);
				ContextFactory.PlaybackContext.Player.LoadAudioStream(path);
				ContextFactory.PlaybackContext.Player.Play();
			}
			else
			{
				ContextFactory.PlaybackContext.Player.Play();
			}

			ContextFactory.PlaybackContext.WriteStatus();
			return context;
		}
	}
}
