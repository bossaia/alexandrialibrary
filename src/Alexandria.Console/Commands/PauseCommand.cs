using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Console.Commands
{
	public class PauseCommand : Command
	{
		public PauseCommand() : base("Pause")
		{
		}
		
		public override Context Execute(Context context, string option)
		{
			ContextFactory.PlaybackContext.Player.Play();
			ContextFactory.PlaybackContext.WriteStatus();
			return context;
		}
	}
}
