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
				context.Player.LoadAudioStream(path);
				context.Player.Play();
			}
			else
			{
				context.Player.Play();
			}
			
			context.WriteCurrentStreamStatus();
			return context;
		}
	}
}
